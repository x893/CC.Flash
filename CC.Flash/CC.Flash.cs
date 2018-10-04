#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using System.Drawing;
using CC.Flash.Properties;
using System.IO;
using System.Configuration;
using System.Diagnostics;
#endregion

namespace CC.Flash
{
	public partial class CCFlash : Form
	{
		#region Variables 
		const byte SEL_FLASH_INFO = 0x01;

		const byte CHIP_ERASE_DONE = 0x80;
		const byte PCON_IDLE = 0x40;
		const byte CPU_HALTED = 0x20;
		const byte POWER_MODE_0 = 0x10;
		const byte HALT_STATUS = 0x08;
		const byte DEBUG_LOCKED = 0x04;
		const byte OSCILATOR_STABLE = 0x02;
		const byte STACK_OVERFLOW = 0x01;

		char[] DELIM_CRLF = new char[] { '\r', '\n' };
		char[] DELIM_SPACE = new char[] { ' ' };
		char[] DELIM_SEMICOMMA = new char[] { ';' };
		// Chip Name, FlashSize, FlashPageSize, FlashWordSize, Generation
		string[] ChipDescriptors = new string[] {
			"CC2541F256;40000;800;2;2",
			"CC2541F128;20000;800;2;2",
			"CC2540F256;40000;800;2;2",
			"CC2540F128;20000;800;2;2",
			"CC2533F96;18000;400;4;2",
			"CC2533F64;10000;400;4;2",
			"CC2533F32;8000;400;4;2",
			"CC2531F256;40000;800;4;2",
			"CC2531F128;20000;800;4;2",
			"CC2530F256;40000;800;4;2",
			"CC2530F128;20000;800;4;2",
			"CC2530F64;10000;800;4;2",
			"CC2530F32;8000;800;4;2",
			"CC2511F32;8000;400;2;1",
			"CC2511F16;4000;400;2;1",
			"CC2511F08;2000;400;2;1",
			"CC2510F32;8000;400;2;1",
			"CC2510F16;4000;400;2;1",
			"CC2510F08;2000;400;2;1",
			"CC2431F128;20000;800;4;1",
			"CC2431F64;10000;800;4;1",
			"CC2431F32;8000;800;4;1",
			"CC2430F128;20000;800;4;1",
			"CC2430F64;10000;800;4;1",
			"CC2430F32;8000;800;4;1",
			"CC1110F32;8000;400;2;1",
			"CC1110F16;4000;400;2;1",
			"CC1110F08;2000;400;2;1",
			null
		};

		bool waiting = false;
		bool chipDefined = false;
		bool inDebugMode = false;
		SerialPort port = null;

		long FLASH_SIZE;
		int FLASH_PAGE_SIZE;
		int FLASH_WORD_SIZE;
		int GENERATION;
		#endregion

		public CCFlash()
		{
			InitializeComponent();
		}

		#region CCFlash_Load(object sender, FormClosedEventArgs e)
		private void CCFlash_Load(object sender, EventArgs e)
		{
			#region Fix up user.config
			string path = null;
			try
			{
				string port = Settings.Default.COM;
			}
			catch (ConfigurationErrorsException ex)
			{
				path = ex.InnerException.Message;
				int i = path.IndexOf("'");
				if (i > 0)
					path = path.Substring(i + 1, path.IndexOf("'", i + 1) - i - 1);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			if (path != null)
			{
				string path1 = string.Empty;
				foreach (string sp in path.Split(new char[] { '\\' }))
				{
					if (sp != "user.config")
					{
						path1 += (sp + "\\");
						if (!string.IsNullOrEmpty(sp) && !sp.EndsWith(":") && path1 != "\\\\" && !Directory.Exists(path1))
							Directory.CreateDirectory(path1);
					}
					else
					{
						path1 += sp;
						break;
					}
				}
				TextWriter fs = new StreamWriter(path1, false, Encoding.UTF8);
				fs.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
</configuration>");
				fs.Close();
				MessageBox.Show("Application config initialized.\nPlease restart application.");
				this.Close();
				return;
			}
			#endregion

			string auto = Settings.Default.AutoConnect.ToLower();
			cbAutoConnect.Checked = (auto == "true" || auto == "yes" || auto == "on" || auto == "1");
			serialPorts.Items.Clear();
			bool enableAuto = false;
			foreach (string portName in SerialPort.GetPortNames())
			{
				serialPorts.Items.Add(portName);
				if (portName == Settings.Default.COM)
				{
					enableAuto = true;
					serialPorts.SelectedIndex = serialPorts.Items.Count - 1;
				}
			}
			if (enableAuto && cbAutoConnect.Checked)
			{
				this.BeginInvoke((MethodInvoker)delegate { btnConnect_Click(sender, e); });
			}
			else if (serialPorts.Items.Count > 0 && serialPorts.SelectedIndex < 0)
				serialPorts.SelectedIndex = 0;
		}
		#endregion

		#region CCFlash_FormClosed(object sender, FormClosedEventArgs e)
		private void CCFlash_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (port != null)
			{
				port.DtrEnable = false;
				port.Close();
				port = null;
			}
		}
		#endregion

		#region CCFlash_FormClosing(object sender, FormClosingEventArgs e)
		private void CCFlash_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (waiting)
			{
				waiting = false;
			}
		}
		#endregion


		#region prepareCommand(string cmd) 
		private string prepareCommand(string cmd)
		{
			if (!string.IsNullOrEmpty(cmd))
			{
				byte csum = 0;
				foreach (byte c in cmd)
					csum += c;
				csum = (byte)(0 - (~csum));
				cmd += string.Format("{0:X2}", csum);
			}
			return (cmd + "\r");
		}
		#endregion

		#region sendCommand(string command, string msg) 
		private string sendCommand(string command, string msg)
		{
			if (port == null)
				return null;

			string response = string.Empty;
			const int retryMax = 1000;
			int retry = retryMax;

			if (msg != null)
				statusLine.Text = msg;
			waiting = true;

			while (port.BytesToRead > 0)
				port.ReadChar();

			command = prepareCommand(command);
			port.Write(command);

			int retryCmd = 10;
			while (--retry > 0)
			{
				if (!waiting)
					break;

				if (port.BytesToRead == 0)
				{
					Application.DoEvents();
					Thread.Sleep(1);
					if (command == "\r" && retryCmd-- == 0)
					{
						retryCmd = 10;
						port.Write(command);
						Thread.Sleep(1);
					}
				}
				else
				{
					++retry;
					response += (char)port.ReadChar();
					if (response.EndsWith("\r\n:"))
					{
						statusLine.Text = "";
						waiting = false;
						return response;
					}
				}
			}
			if (waiting)
			{
				statusLine.Text += " - Timeout";
				waiting = false;
			}
			return null;
		}
		#endregion

		#region parseOK(string response) 
		private bool parseOK(string response)
		{
			if (!string.IsNullOrEmpty(response) && response.EndsWith("\r\nOK\r\n:"))
				return true;
			return false;
		}
		#endregion

		#region getTokenREAD(string response) 
		private string getTokenREAD(string response)
		{
			return getToken("READ:", response);
		}
		#endregion

		#region getToken(string token, string response) 
		private string getToken(string token, string response)
		{
			foreach (string s in response.Split(DELIM_CRLF, StringSplitOptions.RemoveEmptyEntries))
			{
				if (s.StartsWith(token))
					return s.Substring(token.Length);
			}
			return null;
		}
		#endregion

		#region getDataByte(int index, string data, out byte value) 
		private bool getDataByte(int index, string data)
		{
			byte value;
			return getDataByte(index, data, out value);
		}

		private bool getDataByte(int index, string data, out byte value)
		{
			value = 0;
			if (string.IsNullOrEmpty(data))
				return false;
			if (index * 2 + 2 >= data.Length)
				return false;
			data = data.Substring(index * 2, 2);
			if (byte.TryParse(data, NumberStyles.HexNumber, null, out value))
				return true;
			return false;
		}
		#endregion

		#region isPageEmpty(byte[] buffer) 
		private bool isPageEmpty(byte[] buffer)
		{
			for (int i = 0; i < FLASH_PAGE_SIZE; i++)
				if (buffer[i] != 0xFF)
					return false;
			return true;
		}
		#endregion

		#region loadChipModel(string items) 
		private void loadChipModel(string items)
		{
			chipModel.Items.Clear();
			foreach (string item in items.Split(DELIM_SEMICOMMA, StringSplitOptions.RemoveEmptyEntries))
			{
				chipModel.Items.Add(item);
			}
			if (chipModel.SelectedIndex < 0 && chipModel.Items.Count > 0)
				chipModel.SelectedIndex = 0;
		}
		#endregion

		#region addRoutine(List<byte> routine, params byte[] bytes) 
		private void addRoutine(List<byte> routine, params byte[] bytes)
		{
			if (bytes != null)
				for (int i = 0; i < bytes.Length; i++)
					routine.Add(bytes[i]);
		}
		#endregion

		#region parseHexByte(TextBox textBox, out byte value, byte def, bool canEmpty, bool valid) 
		private bool parseHexByte(TextBox textBox, out byte value, byte def, bool canEmpty, bool valid)
		{
			value = def;
			error.SetError(textBox, null);
			string str = textBox.Text.Trim();
			if (string.IsNullOrEmpty(str) && !canEmpty)
			{
				error.SetError(textBox, "Can't be empty");
				if (valid)
				{
					statusLine.Text = string.Format("Field {0} can't be empty", textBox.Name);
					textBox.Focus();
				}
			}
			else
			{
				if (str.StartsWith("0x"))
					str = str.Substring(2, str.Length - 2);
				if (byte.TryParse(str, NumberStyles.HexNumber, null, out value))
				{
					return true;
				}
				else
				{
					error.SetError(textBox, "Must be hex format");
					if (valid)
					{
						statusLine.Text = string.Format("Field {0} must be in hex format", textBox.Name);
						textBox.Focus();
					}
				}
			}
			return false;
		}
		#endregion


		#region DEBUG_INIT() 
		private bool DEBUG_INIT()
		{
			return DEBUG_INIT(true);
		}

		private bool DEBUG_INIT(bool soft)
		{
			if (soft && inDebugMode)
				return true;

			string response = sendCommand("D", "Sending ENTER_DEBUG_MODE ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				inDebugMode = true;
				return true;
			}
			statusLine.Text = "ENTER_DEBUG_MODE error:" + response;
			return false;
		}
		#endregion

		#region READ_STATUS() 
		private bool READ_STATUS()
		{
			byte status;
			if (READ_STATUS(out status))
			{
				setStatusLabels(status);
				return true;
			}
			return false;

		}

		private bool READ_STATUS(out byte status)
		{
			status = 0;
			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("XW134R1", "Sending GET_STATUS ...");
			if (parseOK(response))
			{
				if (getDataByte(0, getTokenREAD(response), out status))
				{
					statusLine.Text = "";
					return true;
				}
			}
			statusLine.Text = "GET_STATUS error:" + response;
			return false;
		}
		#endregion

		#region RESET(bool state) 
		/// <summary>
		/// Activate/Deactivate RESET line
		/// </summary>
		/// <param name="state">true - activate RESET line (set to low), false - deactivate RESET line (set to high)</param>
		/// <returns></returns>
		private bool RESET(bool state)
		{
			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("R" + (state ? "1" : "0"), "Sending RESET ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "RESET error:" + response;
			return false;
		}
		#endregion

		#region LED(bool state) 
		/// <summary>
		///
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		private bool LED(bool state)
		{
			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("L" + (state ? "1" : "0"), "Sending LED ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "LED error:" + response;
			return false;
		}
		#endregion

		#region FLASHDC(bool state) 
		/// <summary>
		/// Set to LOW or toggle DC line
		/// </summary>
		/// <param name="state">true = set to LOW, false = toggle</param>
		/// <returns></returns>
		private bool FLASHDC(bool state)
		{
			string response = sendCommand("L" + (state ? "3" : "4"), "Sending Flash DC ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "Flash DC error:" + response;
			return false;
		}
		#endregion

		#region FLASHDD(bool state) 
		/// <summary>
		/// Set to LOW or toggle DD line
		/// </summary>
		/// <param name="state">true = set to LOW, false = toggle</param>
		/// <returns></returns>
		private bool FLASHDD(bool state)
		{
			string response = sendCommand("L" + (state ? "5" : "6"), "Sending Flash DD ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "Flash DD error:" + response;
			return false;
		}
		#endregion

		#region FLASHRST(bool state) 
		/// <summary>
		/// Set to LOW or toggle RST line
		/// </summary>
		/// <param name="state">true = set to LOW, false = toggle</param>
		/// <returns></returns>
		private bool FLASHRST(bool state)
		{
			string response = sendCommand("L" + (state ? "7" : "8"), "Sending Flash RST ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "Flash RST error:" + response;
			return false;
		}
		#endregion

		#region READ_CONFIG(out int config) 
		private bool READ_CONFIG()
		{
			byte config;
			return dbg_ReadConfig(out config);
		}

		private bool dbg_ReadConfig(out byte config)
		{
			config = 0;

			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("XW124R1", "Sending RD_CONFIG ...");
			if (parseOK(response))
			{
				if (getDataByte(0, getTokenREAD(response), out config))
				{
					setConfigLabels(config);
					statusLine.Text = "";
					return true;
				}
			}
			statusLine.Text = "RD_CONFIG error:" + response;
			return false;
		}
		#endregion

		#region WRITE_CONFIG(byte config) 
		private bool WRITE_CONFIG(byte config)
		{
			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("XW21D" + config.ToString("X2") + "R1", "Sending WR_CONFIG ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "WR_CONFIG error:" + response;
			return false;
		}
		#endregion

		#region GET_CHIP_ID() 
		private bool GET_CHIP_ID()
		{
			byte id, revision;
			return GET_CHIP_ID(out id, out revision);
		}

		private bool GET_CHIP_ID(out byte id, out byte revision)
		{
			id = 0;
			revision = 0;
			if (!DEBUG_INIT())
				return false;

			string response = sendCommand("XW168R2", "Sending GET_CHIP_ID ...");
			if (parseOK(response))
			{
				string readData = getTokenREAD(response);
				if (getDataByte(0, readData, out id) && getDataByte(1, readData, out revision))
				{
					if (id == 0 || id == 0xFF)
					{
						statusLine.Text = "GET_CHIP_ID error: Bad value " + id.ToString("X2");
						return false;
					}

					labelChipID.Text = id.ToString("X2");
					labelChipSeries.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
					labelRevision.Text = revision.ToString("X2");

					chipDefined = true;

					switch (id)
					{
						case 0x41:
							labelChipSeries.Text = "CC2541";
							loadChipModel("CC2541F256;CC2541F128");
							chipModel.Enabled = true;
							break;
						case 0x8D:
							labelChipSeries.Text = "CC2540";
							loadChipModel("CC2540F256;CC2540F128");
							chipModel.Enabled = true;
							break;
						case 0x95:
							labelChipSeries.Text = "CC2533";
							loadChipModel("CC2533F96;CC2533F64;CC2533F32");
							chipModel.Enabled = true;
							break;
						case 0xB5:
							labelChipSeries.Text = "CC2531";
							loadChipModel("CC2531F256;CC2531F128");
							chipModel.Enabled = true;
							break;
						case 0xA5:
							labelChipSeries.Text = "CC2530";
							loadChipModel("CC2530F256;CC2530F128;CC2530F64;CC2530F32");
							chipModel.Enabled = true;
							break;
						case 0x91:
							labelChipSeries.Text = "CC2511";
							loadChipModel("CC2511F32;CC2511F16;CC2511F08");
							chipModel.Enabled = true;
							break;
						case 0x81:
							labelChipSeries.Text = "CC2510";
							loadChipModel("CC2510F32;CC2510F16;CC2510F08");
							chipModel.Enabled = true;
							break;
						case 0x89:
							labelChipSeries.Text = "CC2431";
							loadChipModel("CC2431F128;CC2431F64;CC2431F32");
							chipModel.Enabled = true;
							break;
						case 0x85:
							labelChipSeries.Text = "CC2430";
							loadChipModel("CC2430F128;CC2430F64;CC2430F32");
							chipModel.Enabled = true;
							break;
						case 0x01:
							labelChipSeries.Text = "CC1110";
							loadChipModel("CC1110F32;CC1110F16;CC1110F08");
							chipModel.Enabled = true;
							break;
						default:
							chipDefined = false;
							labelChipSeries.Text = "UNKNOWN";
							labelChipSeries.ForeColor = Color.Red;
							statusLine.Text = "GET_CHIP_ID error: Unknown CHIP ID " + id.ToString("X2");
							return false;
					}
					if (chipDefined)
					{
						if (READ_STATUS())
						{
							statusLine.Text = "";
							return true;
						}
						return false;
					}
				}
			}
			statusLine.Text = "GET_CHIP_ID error:" + response;
			return false;
		}
		#endregion

		#region HALT() 
		private bool HALT()
		{
			if (!DEBUG_INIT())
				return false;
			string response = sendCommand("XW144R1", "Sending HALT ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "HALT error:" + response;
			return false;
		}
		#endregion

		#region RESUME() 
		private bool RESUME()
		{
			if (!DEBUG_INIT())
				return false;
			string response = sendCommand("XW14CR1", "Sending RESUME ...");
			if (parseOK(response))
			{
				statusLine.Text = "";
				return true;
			}
			statusLine.Text = "RESUME error:" + response;
			return false;
		}
		#endregion

		#region MASS_ERASE_FLASH() 
		private bool MASS_ERASE_FLASH()
		{
			bool result = true;
			result = result ? DEBUG_INIT() : false;
			result = result ? DEBUG_INSTR(0x00) : false;
			result = result ? CHIP_ERASE() : false;
			int retry = 10;
			byte status;
			while (retry-- > 0)
			{
				if (!READ_STATUS(out status))
					return false;
				if ((status & CHIP_ERASE_DONE) == CHIP_ERASE_DONE)
					return true;
				Application.DoEvents();
				Thread.Sleep(1);
			}
			return result;
		}
		#endregion

		#region CHIP_ERASE() 
		private bool CHIP_ERASE()
		{
			string response = sendCommand("XW114R1", "Sending CHIP_ERASE ...");
			if (parseOK(response))
				return true;
			statusLine.Text = "CHIP_ERASE error:" + response;
			return false;
		}
		#endregion

		#region DEBUG_INSTR(...) 
		private bool DEBUG_INSTR(byte in0)
		{
			byte data;
			return DEBUG_INSTR(in0, out data);
		}
		private bool DEBUG_INSTR(byte in0, out byte data)
		{
			byte[] cmds = new byte[] { in0 };
			return dbg_DebugInstr(cmds, out data);
		}
		private bool dbg_DebugInstr(byte in0, byte in1)
		{
			byte data;
			byte[] cmds = new byte[] { in0, in1 };
			return dbg_DebugInstr(cmds, out data);
		}
		private bool dbg_DebugInstr(byte in0, byte in1, out byte data)
		{
			byte[] cmds = new byte[] { in0, in1 };
			return dbg_DebugInstr(cmds, out data);
		}
		private bool DEBUG_INSTR(byte in0, byte in1, byte in2)
		{
			byte data;
			byte[] cmds = new byte[] { in0, in1, in2 };
			return dbg_DebugInstr(cmds, out data);
		}
		private bool dbg_DebugInstr(byte in0, byte in1, byte in2, out byte data)
		{
			byte[] cmds = new byte[] { in0, in1, in2 };
			return dbg_DebugInstr(cmds, out data);
		}

		private bool dbg_DebugInstr(byte[] cmds, out byte data)
		{
			data = 0;

			if (!DEBUG_INIT())
				return false;

			if (cmds.Length > 3)
			{
				statusLine.Text = "DEBUG_INSTR error: Too long commands, maximum 3";
				return false;
			}

			string cmd = string.Empty;
			for (int i = 0; i < cmds.Length; ++i)
				cmd += cmds[i].ToString("X2");
			cmd = "XW" + string.Format("{0}{1:X2}", 1 + cmds.Length, 0x54 + cmds.Length) + cmd + "R1";
			string response = sendCommand(cmd, "Sending DEBUG_INSTR ...");
			if (parseOK(response))
			{
				if (getDataByte(0, getTokenREAD(response), out data))
				{
					statusLine.Text = "";
					return true;
				}
			}
			statusLine.Text = "DEBUG_INSTR error:" + response;
			return false;
		}
		#endregion

		#region CLOCK_INIT() 
		private bool CLOCK_INIT()
		{
			if (DEBUG_INSTR(0x75, 0xC6, 0x00))					//MOV CLKCON, 0x00
			{
				int retry = 5;
				byte sleepReg;
				while (retry-- > 0)
				{
					if (!dbg_DebugInstr(0xE5, 0xBE, out sleepReg))	//MOV A, SLEEP; (sleepReg = A)
						return false;
					if (GENERATION == 2) {
						return true; // TODO???
					} else if (GENERATION == 1) {
						if ((sleepReg & 0x40) == 0x40)
							return true;
					}
				}
				statusLine.Text = "No 0x40 in CLKCON";
			}
			return false;
		}
		#endregion

		#region WRITE_XDATA_MEMORY(int address, byte[] buffer, int length) 
		private bool WRITE_XDATA_MEMORY(int address, byte[] buffer)
		{
			const int PACKET_SIZE = 120;

			if (!DEBUG_INIT())
				return false;

			int i = 0;
			int nextAddress = address;
			int length = buffer.Length;
			while (length > 0)
			{
				int sentBytes = length > PACKET_SIZE ? PACKET_SIZE : length;
				string response = string.Format("MW{0:X4}{1:X2}", address, sentBytes);
				nextAddress += sentBytes;
				length -= sentBytes;
				while (sentBytes-- > 0)
					response += ((byte)(buffer[i++])).ToString("X2");

				response = sendCommand(response, string.Format("Sending WRITE_XDATA ({0:X4}) ...", address));
				address = nextAddress;
				if (parseOK(response))
					continue;
				else
				{
					MessageBox.Show("WRITE_XDATA error:" + response);
					statusLine.Text = "WRITE_XDATA error:" + response;
					return false;
				}
			}
			return true;
		}
		#endregion

		#region READ_XDATA_MEMORY(int address, byte[] buffer, int length) 
		private bool READ_XDATA_MEMORY(int address, int length, out byte[] buffer)
		{
			const int PACKET_SIZE = 120;
			buffer = null;
			if (!DEBUG_INIT())
				return false;

			int i = 0;
			int nextAddress = address;
			buffer = new byte[length];
			byte data;

			while (length > 0)
			{
				int sentBytes = length > PACKET_SIZE ? PACKET_SIZE : length;
				string response = string.Format("MR{0:X4}{1:X2}", address, sentBytes);
				nextAddress += sentBytes;
				length -= sentBytes;
				response = sendCommand(response, string.Format("Sending READ_XDATA ({0:X4}, {1}) ...", address, sentBytes));
				address = nextAddress;
				if (parseOK(response))
				{
					int j = 0;
					response = getTokenREAD(response);
					while (sentBytes-- > 0)
						if (getDataByte(j++, response, out data))
							buffer[i++] = data;
						else
						{
							MessageBox.Show("READ_XDATA error: Not a hex " + response.Substring(j * 2, 2));
							statusLine.Text = "READ_XDATA error: Not a hex " + response.Substring(j * 2, 2);
							return false;
						}
				}
				else
				{
					MessageBox.Show(statusLine.Text = "READ_XDATA error:" + response);
					statusLine.Text = "READ_XDATA error:" + response;
					return false;
				}
			}
			return true;
		}
		#endregion

		#region READ_CODE_MEMORY(int address, byte bank, int length, out byte[] code) 
		private bool READ_CODE_MEMORY(int address, byte bank, int length, out byte[] buffer)
		{
			const int PACKET_SIZE = 64;
			buffer = null;
			if (!DEBUG_INIT())
				return false;

			int i = 0;
			int nextAddress = address;
			buffer = new byte[length];
			byte data;

			bool valid = true;
			if (GENERATION == 2) {
				valid = DEBUG_INSTR(0x75, 0xC7, bank); // Map 32 kB bank to 0x8000-0xFFFF in XDATA
			} else if (GENERATION == 1 && bank > 0) {
				valid = DEBUG_INSTR(0x75, 0xC7, (byte)(((bank << 4) | 0x01) & 0xFF)); // Map 32 kB bank to 0x8000-0xFFFF in CODE (FMAP[1:0] in MEMCTR[5:4])
			}
			while(length > 0)
			{
				int sentBytes = length > PACKET_SIZE ? PACKET_SIZE : length;
				string response = "";
				if (GENERATION == 2) {
					response = string.Format("MR{0:X4}{1:X2}", address + 0x8000, sentBytes); // Read from XDATA
				} else if (GENERATION == 1) {
					response = string.Format("MC{0:X4}{1:X2}", bank == 0 ? address : address + 0x8000, sentBytes); // Read from CODE
				}
				nextAddress += sentBytes;
				length -= sentBytes;
				response = sendCommand(response, string.Format("Sending READ_CODE (bank:{0}, addr:{1:X4}, len:{2}) ...", bank, address, sentBytes));
				address = nextAddress;
				if (parseOK(response))
				{
					int j = 0;
					response = getTokenREAD(response);
					while (sentBytes-- > 0)
						if (getDataByte(j++, response, out data))
							buffer[i++] = data;
						else
						{
							statusLine.Text = "READ_CODE error: Not a hex " + response.Substring(j * 2, 2);
							return false;
						}
				}
				else
				{
					statusLine.Text = "READ_XDATA error:" + response;
					return false;
				}
			}
			return true;
		}
		#endregion

		#region SET_PC(int address) 
		private bool SET_PC(int address)
		{
			if (!DEBUG_INIT())
				return false;
			return DEBUG_INSTR(0x02, (byte)((address >> 8) & 0xFF), (byte)(address & 0xFF));		//LJMP iAddr
		}
		#endregion

		#region GET_PC(out address) 
		private bool GET_PC(out int address)
		{
			address = 0;
			if (!DEBUG_INIT())
				return false;
			string response = sendCommand("XW128R2", "Sending GET_PC ...");
			if (parseOK(response))
			{
				response = getTokenREAD(response);
				if (response != null)
				{
					byte bh, bl;
					if (getDataByte(0, response, out bh) && getDataByte(1, response, out bl))
					{
						address = ((bh << 8) & 0xFF00) | bl;
						return true;
					}
				}
			}
			statusLine.Text = "GET_PC error:" + response;
			return false;
		}
		#endregion

		#region WRITE_PAGE_FLASH(long iPageAddress, byte[] buffer, bool erasePage) 
		private bool WRITE_PAGE_FLASH(long iPageAddress, byte[] buffer, bool erasePage)
		{
			bool valid = true;

      if (GENERATION == 2) {

        List<byte> routine = new List<byte>(1024);
      	addRoutine(routine, 0x90, 0x62, 0x71);                                                    //  mov DPTR, #FADDRL;
      	addRoutine(routine, 0x74, (byte)((iPageAddress/FLASH_WORD_SIZE) & 0xFF));                 //  mov A, #page address lo;
        addRoutine(routine, 0xF0);                                                                //  movx @DPTR, A;
        addRoutine(routine, 0xA3);                                                                //  inc DPTR;
        addRoutine(routine, 0x74, (byte)(((iPageAddress/FLASH_WORD_SIZE) >> 8) & 0xFF));          //  mov A, #page address hi;
        addRoutine(routine, 0xF0);                                                                //  movx @DPTR, A;
        addRoutine(routine, 0x75, 0x92, 0x01);                                                    //  mov DPS, #0x01;
        addRoutine(routine, 0x90, 0x62, 0x70);                                                    //  mov DPTR1, #FCTL;
        if (erasePage) {
          addRoutine(routine, 0x74, 0x01);                                                        //  mov A, #0x01;       // ERASE
          addRoutine(routine, 0xF0);                                                              //  movx @DPTR1, A;
          addRoutine(routine, 0xE0);                                          // eraseWaitLoop:   //  movx A, @DPTR1;
          addRoutine(routine, 0x20, 0xE7, 0xFC);                                                  //  jb ACC_BUSY, eraseWaitLoop;
        }
        addRoutine(routine, 0x74, 0x02);                                                          //  mov A, #0x02;       // WRITE
        addRoutine(routine, 0xF0);                                                                //  movx @DPTR1, A;
        addRoutine(routine, 0x90, 0x62, 0x73);                                                    //  mov DPTR1, #FWDATA;
        addRoutine(routine, 0x75, 0x92, 0x00);                                                    //  mov DPS, #0x00;
        addRoutine(routine, 0x90, 0x00, 0x00);                                                    //  mov DPTR0, #0x0000; // Initialize the data pointer
        addRoutine(routine, 0x7F, (byte)(((FLASH_PAGE_SIZE / FLASH_WORD_SIZE) >> 8) & 0xFF));     //  mov R7, #imm;
        addRoutine(routine, 0x7E, (byte)((FLASH_PAGE_SIZE / FLASH_WORD_SIZE) & 0xFF));            //  mov R6, #imm;
        addRoutine(routine, 0x7D, (byte)FLASH_WORD_SIZE);                     // writeLoop:       //  mov R5, #imm;
        addRoutine(routine, 0xE0);                                            // writeWordLoop:   //  movx A, @DPTR0;
        addRoutine(routine, 0xA3);                                                                //  inc DPTR0;
        addRoutine(routine, 0x75, 0x92, 0x01);                                                    //  mov DPS, #0x01;
        addRoutine(routine, 0xF0);                                                                //  movx @DPTR1, A;
        addRoutine(routine, 0x75, 0x92, 0x00);                                                    //  mov DPS, 0x00;
        addRoutine(routine, 0xDD, 0xF5);                                                          //  djnz R5, writeWordLoop;
        addRoutine(routine, 0x75, 0x92, 0x01);                                                    //  mov DPS, 0x01;
        addRoutine(routine, 0x90, 0x62, 0x70);                                                    //  mov DPTR1, #FCTL;
        addRoutine(routine, 0xE0);                                            // writeWaitLoop:   //  movx A, @DPTR1;     // Wait for flash write to complete
        addRoutine(routine, 0x20, 0xE6, 0xFC);                                                    //  jb ACC_SWBSY, writeWaitLoop;
        addRoutine(routine, 0x90, 0x62, 0x73);                                                    //  mov DPTR1, #FWDATA;
        addRoutine(routine, 0x75, 0x92, 0x00);                                                    //  mov DPS, 0x00;
      	addRoutine(routine, 0xDE, 0xE1);                                                          //  djnz R6, writeLoop;
        addRoutine(routine, 0xDF, 0xDF);                                                          //  djnz R7, writeLoop;
        addRoutine(routine, 0x90, 0x62, 0x70);                                                    //  mov DPTR1, #FCTL;
        addRoutine(routine, 0x74, 0x0);                                                           //  mov A, #0x00;
        addRoutine(routine, 0xF0);                                                                //  movx @DPTR1, A;
        addRoutine(routine, 0xA5);                                            // loop:            //  db 0xA5;            // Done, fake a breakpoint
      	addRoutine(routine, 0x80, 0xFD);                                                          //  jmp loop;

        valid = valid ? WRITE_XDATA_MEMORY(0x0000, buffer) : false;
        valid = valid ? WRITE_XDATA_MEMORY(0x0000 + FLASH_PAGE_SIZE, routine.ToArray()) : false;
        valid = valid ? DEBUG_INSTR(0x75, 0xC7, 0x08) : false; // MEMCTR: Enable XDATA map to code, XMAP = 1 (leave everything else the default values)
        valid = valid ? SET_PC(0x8000 + FLASH_PAGE_SIZE) : false;
        valid = valid ? RESUME() : false;

      } else { // GENERATION == 1

        List<byte> routine = new List<byte>(1024);
        addRoutine(routine, 0x75, 0xAD, (byte)(((iPageAddress >> 8) / FLASH_WORD_SIZE) & 0x7E));  //  mov FADDRH, #imm;
        addRoutine(routine, 0x75, 0xAC, 0x00);                                                    //  mov FADDRL, #00;
        if (erasePage) {
          addRoutine(routine, 0x75, 0xAE, 0x01);                                                  //  mov FLC, #01H;      // ERASE
          addRoutine(routine, 0xE5, 0xAE);                                    // eraseWaitLoop:   //  mov A, FLC;         // Wait for flash erase to complete
          addRoutine(routine, 0x20, 0xE7, 0xFB);                                                  //  jb ACC_BUSY, eraseWaitLoop
        }
        addRoutine(routine, 0x90, 0xF0, 0x00);                                                    //  mov DPTR, #F000H;   // Initialize the data pointer
        addRoutine(routine, 0x7F, (byte)(((FLASH_PAGE_SIZE / FLASH_WORD_SIZE) >> 8) & 0xFF));     //  mov R7, #imm;
        addRoutine(routine, 0x7E, (byte)((FLASH_PAGE_SIZE / FLASH_WORD_SIZE) & 0xFF));            //  mov R6, #imm;
        addRoutine(routine, 0x75, 0xAE, 0x02);                                                    //  mov FLC, #02H;      // WRITE
        addRoutine(routine, 0x7D, (byte)FLASH_WORD_SIZE);                     // writeLoop:       //  mov R5, #imm;
        addRoutine(routine, 0xE0);                                            // writeWordLoop:   //  movx A, @DPTR;
        addRoutine(routine, 0xA3);                                                                //  inc DPTR;
        addRoutine(routine, 0xF5, 0xAF);                                                          //  mov FWDATA, A;
        addRoutine(routine, 0xDD, 0xFA);                                                          //  djnz R5, writeWordLoop;
        addRoutine(routine, 0xE5, 0xAE);                                      // writeWaitLoop:   //  mov A, FLC;         // Wait for flash write to complete
        addRoutine(routine, 0x20, 0xE6, 0xFB);                                                    //  jb ACC_SWBSY, writeWaitLoop
        addRoutine(routine, 0xDE, 0xF1);                                                          //  djnz R6, writeLoop;
        addRoutine(routine, 0xDF, 0xEF);                                                          //  djnz R7, writeLoop;
        addRoutine(routine, 0xA5);                                                                //  db 0xA5;            // Done, fake a breakpoint

        valid = valid ? WRITE_XDATA_MEMORY(0xF000, buffer) : false;
        valid = valid ? WRITE_XDATA_MEMORY(0xF000 + FLASH_PAGE_SIZE, routine.ToArray()) : false;
        valid = valid ? DEBUG_INSTR(0x75, 0xC7, 0x51) : false; // MEMCTR: Enable Unified mapping, MUNIF = 1 (leave everything else the default values)
        valid = valid ? SET_PC(0xF000 + FLASH_PAGE_SIZE) : false;
        valid = valid ? RESUME() : false;
      }
			if (valid)
			{
				byte status;
				int retry = 10;
				do
				{
					READ_STATUS(out status);
					if ((status & CPU_HALTED) == CPU_HALTED)
						return true;
					Application.DoEvents();
				} while (retry-- > 0);
			}
			return false;
		}
		#endregion

		#region READ_FLASH_PAGE(long iPageAddress, int length, out byte[] code) 
		private bool READ_FLASH_PAGE(long iPageAddress, int length, out byte[] code)
		{
			return READ_CODE_MEMORY((int)(iPageAddress & 0x7FFF), (byte)((iPageAddress >> 15) & 0x07), length, out code);
		}
		#endregion


		#region connect() 
		/// <summary>
		/// Check and open serial port to Arduino
		/// </summary>
		/// <returns></returns>
		private bool connect()
		{
			if(port != null)
				return true;

			if (serialPorts.SelectedIndex >= 0)
			{
				groupAllControls.Enabled = false;
				btnConnect.Text = "Connecting ...";
				serialPorts.Enabled = btnConnect.Enabled = false;
				Application.DoEvents();

				port = new SerialPort(serialPorts.Text);
				try
				{
					port.BaudRate = Settings.Default.Baudrate;
					port.Handshake = Handshake.None;
					port.DtrEnable = true;
					port.Open();
				}
				catch
				{
					MessageBox.Show("Can't open port: " + serialPorts.Text);
					setDisconnected();
					return false;
				}

				if (sendCommand("", "Wait for start") != null)
					return true;
			}
			return false;
		}
		#endregion

		#region Set Status/Config Labels 
		private void setStatusLED(int state, Label label)
		{
			if (state == 0)
			{
				label.BackColor = Color.White;
			}
			else
			{
				label.BackColor = Color.DarkGreen;
			}
		}

		private void setStatusLabels(int status)
		{
			setStatusLED(status & 0x80, status_CHIP_ERASE_DONE);
			setStatusLED(status & 0x40, status_PCON_IDLE);
			setStatusLED(status & 0x20, status_CPU_HALTED);
			setStatusLED(status & 0x10, status_POWER_MODE_0);
			setStatusLED(status & 0x08, status_HALT_STATUS);
			setStatusLED(status & 0x04, status_DEBUG_LOCKED);
			setStatusLED(status & 0x02, status_OSCILLATOR_STABLE);
			setStatusLED(status & 0x01, status_STACK_OVERFLOW);
		}

		private void setConfigLabels(int config)
		{
			setStatusLED(config & 0x08, label_TIMERS_OFF);
			setStatusLED(config & 0x04, label_DMA_PAUSE);
			setStatusLED(config & 0x02, label_TIMER_SUSPEND);
			setStatusLED(config & 0x01, label_SEL_FLASH_INFO);
		}
		#endregion

		#region Set Connected/Disconnected 
		private void setConnected()
		{
			serialPorts.Enabled = false;
			btnConnect.Enabled = true;
			btnConnect.Text = "Disconnect";
		}

		private void setDisconnected()
		{
			if (port != null)
			{
				try
				{
					port.DtrEnable = false;
					port.Close();
				}
				catch { }
				port = null;
			}

			inDebugMode = false;

			chipModel.Items.Clear();
			chipModel.Text = "";
			chipModel.Enabled = false;

			setStatusLabels(0);
			setConfigLabels(0);

			chipDefined = false;

			btnVerify.Enabled = btnWrite.Enabled = btnRead.Enabled = false;
			labelChipSize.Text = labelRevision.Text = labelChipID.Text = labelChipSeries.Text = "";

			groupAllControls.Enabled = false;
			btnConnect.Text = "Connect";
			btnConnect.Enabled = serialPorts.Enabled = true;
		}
		#endregion

		#region btnConnect_Click(object sender, EventArgs e) 
		private void btnConnect_Click(object sender, EventArgs e)
		{
			if (port != null)
			{
				setDisconnected();
				return;
			}

			if (connect())
			{
				bool changed = false;
				string auto = Settings.Default.AutoConnect.ToLower();

				changed |= ((auto == "true" || auto == "yes" || auto == "on" || auto == "1") != cbAutoConnect.Checked);
				changed |= (Settings.Default.COM != serialPorts.Text);

				if (changed)
				{
					Settings.Default.AutoConnect = (cbAutoConnect.Checked ? "True" : "False");
					Settings.Default.COM = serialPorts.Text;
					try
					{
						Settings.Default.Save();
					}
					catch (Exception ex)
					{
						if (MessageBox.Show(ex.Message + "\n\nContinue connecting ?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
						{
							btnConnect.Text = "Connect";
							groupAllControls.Enabled = true;
							return;
						}
					}
				}

				if (GET_CHIP_ID() && READ_STATUS()) {
					setConnected();
					groupAllControls.Enabled = true;
				} else
					setDisconnected();
			}
			else
				setDisconnected();
		}
		#endregion

		#region chipModel_SelectedIndexChanged(object sender, EventArgs e) 
		private void chipModel_SelectedIndexChanged(object sender, EventArgs e)
		{
			statusLine.Text = "";
			if (chipModel.SelectedIndex >= 0)
			{
				string model = chipModel.Text + ";";
				foreach (string descriptor in ChipDescriptors)
				{
					if (string.IsNullOrEmpty(descriptor))
						continue;
					if (descriptor.StartsWith(model))
					{
						string[] param = descriptor.Split(DELIM_SEMICOMMA, StringSplitOptions.RemoveEmptyEntries);
						if (param.Length >= 5)
						{
							if (
								long.TryParse(param[1], NumberStyles.HexNumber, null, out FLASH_SIZE) &&
								int.TryParse(param[2], NumberStyles.HexNumber, null, out FLASH_PAGE_SIZE) &&
								int.TryParse(param[3], NumberStyles.HexNumber, null, out FLASH_WORD_SIZE) &&
								int.TryParse(param[4], NumberStyles.HexNumber, null, out GENERATION)
								)
								labelChipSize.Text = string.Format("{0:d}K", FLASH_SIZE / 1024);

							btnVerify.Enabled = btnWrite.Enabled = btnRead.Enabled = true;
							return;
						}
						statusLine.Text = "Bad chip descriptor(s)";
						break;
					}
				}
				if (statusLine.Text.Length == 0)
					statusLine.Text = string.Format("Chip {0} not found in chip descriptors", chipModel.Text);
			}
			btnVerify.Enabled = btnWrite.Enabled = btnRead.Enabled = false;
			labelChipSize.Text = "";
		}
		#endregion

		#region btnFlashDD_Click(object sender, EventArgs e) 
		/// <summary>
		/// Make 5 pulses on DD line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFlashDD_Click(object sender, EventArgs e)
		{
			if (connect())
			{
				int times = 5;
				while (--times > 0)
				{
					FLASHDD(false);
					Thread.Sleep(2000);
					FLASHDD(false);
					Thread.Sleep(2000);
				}
				setDisconnected();
				groupAllControls.Enabled = true;
			}
		}
		#endregion

		#region btnFlashDC_Click(object sender, EventArgs e) 
		/// <summary>
		/// Make 5 pulses on DC line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFlashDC_Click(object sender, EventArgs e)
		{
			if (connect())
			{
				int times = 5;
				while (--times > 0)
				{
					FLASHDC(false);
					Thread.Sleep(2000);
					FLASHDC(false);
					Thread.Sleep(2000);
				}
				setDisconnected();
				groupAllControls.Enabled = true;
			}
		}
		#endregion

		#region btnFlashRST_Click(object sender, EventArgs e) 
		/// <summary>
		/// Make 5 pulses on RST line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFlashRST_Click(object sender, EventArgs e)
		{
			if (connect())
			{
				int times = 5;
				while (--times > 0)
				{
					FLASHRST(false);
					Thread.Sleep(2000);
					FLASHRST(false);
					Thread.Sleep(2000);
				}
				setDisconnected();
				groupAllControls.Enabled = true;
			}
		}
		#endregion

		#region btnSelectFile_Click(object sender, EventArgs e) 
		private void btnSelectFile_Click(object sender, EventArgs e)
		{
			if (openFile.ShowDialog() == DialogResult.OK)
			{
				filename.Text = openFile.FileName;
				FileInfo fi = new FileInfo(filename.Text);
				startAddress.Text = "0x00000";
				endAddress.Text = "0x" + fi.Length.ToString("X5");
			}
		}
		#endregion

		#region btnWrite_Click(object sender, EventArgs e) 
		private void btnWrite_Click(object sender, EventArgs e)
		{
			filename.Text = filename.Text.Trim();
			if (filename.Text.Length == 0)
			{
				if (openFile.ShowDialog() == DialogResult.OK)
					filename.Text = openFile.FileName.Trim();
				else
					return;
			}
			if (File.Exists(filename.Text))
			{
				FileStream fs = null;
				Stopwatch time = null;
				bool noError = true;
				try
				{
					groupAllControls.Enabled = false;
					fs = new FileStream(filename.Text, FileMode.Open, FileAccess.Read, FileShare.Read, FLASH_PAGE_SIZE);
					byte[] buffer = new byte[FLASH_PAGE_SIZE];
					bool valid = true;
					int readed;
					long length = fs.Length;

					if (length > FLASH_SIZE)
					{
						MessageBox.Show(string.Format("File too big, maximum {0} bytes", FLASH_SIZE));
						valid = false;
					}

					valid = valid ? DEBUG_INIT(false) : false;
					valid = valid ? CLOCK_INIT() : false;

					progressBar.Minimum = 0;
					progressBar.Maximum = (int)(length / (long)FLASH_PAGE_SIZE) + 1;
					progressBar.Value = progressBar.Minimum;

					time = Stopwatch.StartNew();
					long pageAddress = 0;
					while (valid && length > 0)
					{
						progressBar.Value++;

						if (length >= (long)FLASH_PAGE_SIZE)
						{
							valid = ((readed = fs.Read(buffer, 0, FLASH_PAGE_SIZE)) == FLASH_PAGE_SIZE);
							length -= (long)readed;
						}
						else
						{
							valid = ((readed = fs.Read(buffer, 0, (int)length)) == (int)length);
							length = 0;
						}
						if (valid)
						{
							while (readed < FLASH_PAGE_SIZE)
								buffer[readed++] = 0xFF;

							if (!isPageEmpty(buffer))
							{
								valid = WRITE_PAGE_FLASH(pageAddress, buffer, cbErasePage.Checked);
								if (!valid) {
									MessageBox.Show("Write Failed");
								}
								if (valid && cbVerifyAfterWrite.Checked)
								{
									byte[] code;
									valid = READ_FLASH_PAGE(pageAddress, FLASH_PAGE_SIZE, out code);
									for (int i = 0; i < FLASH_PAGE_SIZE; ++i)
									{
										if (buffer[i] != code[i])
										{
											MessageBox.Show("Verify Failed");
											valid = false;
											break;
										}
									}
								}
							}
							pageAddress += (long)FLASH_PAGE_SIZE;
						}
						else
							MessageBox.Show("File read not persistance");
					}
					noError = valid;
				}
				catch (Exception ex)
				{
					noError = false;
					MessageBox.Show("Error: " + ex.Message);
				}
				finally
				{
					if (fs != null)
						fs.Close();
					RESET(true);
					RESET(false);
					inDebugMode = false;
					//DEBUG_INIT(false);
					progressBar.Value = progressBar.Minimum;
					groupAllControls.Enabled = true;
					time.Stop();
					if (noError)
						statusLine.Text = "Wrote Flash in " + time.ElapsedMilliseconds / 1000.0 + "s";
				}
			}
			else
				MessageBox.Show("File not exists");
		}
		#endregion

		#region btnRead_Click(object sender, EventArgs e) 
		private void btnRead_Click(object sender, EventArgs e)
		{
			filename.Text = filename.Text.Trim();
			if (filename.Text.Length == 0)
			{
				if (openFile.ShowDialog() == DialogResult.OK)
					filename.Text = openFile.FileName.Trim();
				else
					return;
			}
			FileStream fs = null;
			Stopwatch time = null;
			try
			{
				groupAllControls.Enabled = false;
				fs = new FileStream(filename.Text, FileMode.Create, FileAccess.Write, FileShare.Write, FLASH_PAGE_SIZE);
				bool valid = true;
				valid = valid ? DEBUG_INIT(false) : false;
				valid = valid ? CLOCK_INIT() : false;

				progressBar.Minimum = 0;

				progressBar.Maximum = (int)(FLASH_SIZE / (long)FLASH_PAGE_SIZE);
				progressBar.Value = progressBar.Minimum;

				time = Stopwatch.StartNew();
				long pageAddress = 0;
				byte[] buffer = null;
				while (valid && pageAddress < FLASH_SIZE)
				{
					progressBar.Value++;
					valid = valid ? READ_FLASH_PAGE(pageAddress, FLASH_PAGE_SIZE, out buffer) : false;
					if (valid)
						fs.Write(buffer, 0, buffer.Length);
					pageAddress += FLASH_PAGE_SIZE;
				}
				if (!valid)
					MessageBox.Show("Read Failed");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				if (fs != null)
					fs.Close();
				DEBUG_INIT(false);
				progressBar.Value = progressBar.Minimum;
				groupAllControls.Enabled = true;
				time.Stop();
				statusLine.Text = "Read Flash in " + time.ElapsedMilliseconds / 1000.0 + "s";
			}
		}
		#endregion

		#region btnExit_Click(object sender, EventArgs e) 
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region btnGetStatus_Click(object sender, EventArgs e) 
		private void btnGetStatus_Click(object sender, EventArgs e)
		{
			READ_STATUS();
		}
		#endregion

		#region btnCheckCC_Click(object sender, EventArgs e) 
		private void btnFlashCC_Click(object sender, EventArgs e)
		{
			DEBUG_INIT(false);

			bool valid = true;
			byte pxselbits, pxsel, pxbits, px, pxdir;

			valid &= parseHexByte(PxSELBITS, out pxselbits, 0, false, valid);
			valid &= parseHexByte(PxSEL, out pxsel, 0, false, valid);
			valid &= parseHexByte(PxBITS, out pxbits, 0, false, valid);
			valid &= parseHexByte(Px, out px, 0, false, valid);
			valid &= parseHexByte(PxDIR, out pxdir, 0, false, valid);
			valid = valid ? DEBUG_INSTR(0x53, pxsel, (byte)(~pxselbits)) : false;
			valid = valid ? DEBUG_INSTR(0x53, px, (byte)(~pxbits)) : false;
			valid = valid ? DEBUG_INSTR(0x43, pxdir, pxbits) : false;

			for (int i = 0; i < 10; i++)
			{
				if (!valid)
					break;
				valid = valid ? DEBUG_INSTR(0x63, px, pxbits) : false;
				Thread.Sleep(500);
			}

			DEBUG_INIT(false);
		}
		#endregion

		private void btnChipErase_Click(object sender, EventArgs e)
		{
			string response = sendCommand("XW110", "Sending CHIP_ERASE ...");
			if (parseOK(response))
			{
				MessageBox.Show("Chip Erased");
			}
		}
	}
}