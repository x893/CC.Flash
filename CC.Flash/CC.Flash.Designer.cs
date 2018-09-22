namespace CC.Flash
{
	partial class CCFlash
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.serialPorts = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelChipID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelChipSeries = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelRevision = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.cbVerifyAfterWrite = new System.Windows.Forms.CheckBox();
            this.cbAutoConnect = new System.Windows.Forms.CheckBox();
            this.groupAllControls = new System.Windows.Forms.GroupBox();
            this.btnFlashRST = new System.Windows.Forms.Button();
            this.btnFlashDC = new System.Windows.Forms.Button();
            this.btnFlashDD = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.filename = new System.Windows.Forms.TextBox();
            this.endAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.startAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbErasePage = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnFlashCC = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.PxSELBITS = new System.Windows.Forms.TextBox();
            this.PxSEL = new System.Windows.Forms.TextBox();
            this.PxBITS = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Px = new System.Windows.Forms.TextBox();
            this.PxDIR = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label_TIMERS_OFF = new System.Windows.Forms.Label();
            this.label_DMA_PAUSE = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_TIMER_SUSPEND = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_SEL_FLASH_INFO = new System.Windows.Forms.Label();
            this.btnGetStatus = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.labelChipSize = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chipModel = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.status_CHIP_ERASE_DONE = new System.Windows.Forms.Label();
            this.status_PCON_IDLE = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.status_CPU_HALTED = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.status_POWER_MODE_0 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.status_HALT_STATUS = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.status_DEBUG_LOCKED = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.status_OSCILLATOR_STABLE = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.status_STACK_OVERFLOW = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnChipErase = new System.Windows.Forms.Button();
            this.status.SuspendLayout();
            this.groupAllControls.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPorts
            // 
            this.serialPorts.FormattingEnabled = true;
            this.serialPorts.Location = new System.Drawing.Point(7, 6);
            this.serialPorts.Name = "serialPorts";
            this.serialPorts.Size = new System.Drawing.Size(121, 21);
            this.serialPorts.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(133, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLine});
            this.status.Location = new System.Drawing.Point(0, 537);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(413, 26);
            this.status.TabIndex = 2;
            this.status.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(112, 20);
            // 
            // statusLine
            // 
            this.statusLine.AutoSize = false;
            this.statusLine.Name = "statusLine";
            this.statusLine.Size = new System.Drawing.Size(284, 21);
            this.statusLine.Spring = true;
            this.statusLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "ChipID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelChipID
            // 
            this.labelChipID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelChipID.Location = new System.Drawing.Point(68, 97);
            this.labelChipID.Name = "labelChipID";
            this.labelChipID.Size = new System.Drawing.Size(65, 22);
            this.labelChipID.TabIndex = 5;
            this.labelChipID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Chip";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelChipSeries
            // 
            this.labelChipSeries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelChipSeries.Location = new System.Drawing.Point(68, 122);
            this.labelChipSeries.Name = "labelChipSeries";
            this.labelChipSeries.Size = new System.Drawing.Size(65, 22);
            this.labelChipSeries.TabIndex = 7;
            this.labelChipSeries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Revision";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelRevision
            // 
            this.labelRevision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRevision.Location = new System.Drawing.Point(68, 147);
            this.labelRevision.Name = "labelRevision";
            this.labelRevision.Size = new System.Drawing.Size(65, 22);
            this.labelRevision.TabIndex = 9;
            this.labelRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnWrite
            // 
            this.btnWrite.Enabled = false;
            this.btnWrite.Location = new System.Drawing.Point(105, 95);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(84, 23);
            this.btnWrite.TabIndex = 10;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Enabled = false;
            this.btnRead.Location = new System.Drawing.Point(206, 95);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(84, 23);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(307, 95);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(84, 23);
            this.btnVerify.TabIndex = 12;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            // 
            // cbVerifyAfterWrite
            // 
            this.cbVerifyAfterWrite.AutoSize = true;
            this.cbVerifyAfterWrite.Checked = true;
            this.cbVerifyAfterWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVerifyAfterWrite.Location = new System.Drawing.Point(256, 47);
            this.cbVerifyAfterWrite.Name = "cbVerifyAfterWrite";
            this.cbVerifyAfterWrite.Size = new System.Drawing.Size(101, 17);
            this.cbVerifyAfterWrite.TabIndex = 6;
            this.cbVerifyAfterWrite.Text = "Verify after write";
            this.cbVerifyAfterWrite.UseVisualStyleBackColor = true;
            // 
            // cbAutoConnect
            // 
            this.cbAutoConnect.AutoSize = true;
            this.cbAutoConnect.Checked = true;
            this.cbAutoConnect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoConnect.Location = new System.Drawing.Point(252, 6);
            this.cbAutoConnect.Name = "cbAutoConnect";
            this.cbAutoConnect.Size = new System.Drawing.Size(129, 17);
            this.cbAutoConnect.TabIndex = 1;
            this.cbAutoConnect.Text = "Auto Connect on start";
            this.cbAutoConnect.UseVisualStyleBackColor = true;
            // 
            // groupAllControls
            // 
            this.groupAllControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAllControls.Controls.Add(this.btnFlashRST);
            this.groupAllControls.Controls.Add(this.btnFlashDC);
            this.groupAllControls.Controls.Add(this.btnFlashDD);
            this.groupAllControls.Controls.Add(this.groupBox4);
            this.groupAllControls.Controls.Add(this.groupBox3);
            this.groupAllControls.Controls.Add(this.groupBox2);
            this.groupAllControls.Controls.Add(this.btnGetStatus);
            this.groupAllControls.Controls.Add(this.btnExit);
            this.groupAllControls.Controls.Add(this.labelChipSize);
            this.groupAllControls.Controls.Add(this.label7);
            this.groupAllControls.Controls.Add(this.chipModel);
            this.groupAllControls.Controls.Add(this.label1);
            this.groupAllControls.Controls.Add(this.labelChipID);
            this.groupAllControls.Controls.Add(this.label2);
            this.groupAllControls.Controls.Add(this.labelRevision);
            this.groupAllControls.Controls.Add(this.labelChipSeries);
            this.groupAllControls.Controls.Add(this.label4);
            this.groupAllControls.Controls.Add(this.groupBox1);
            this.groupAllControls.Enabled = false;
            this.groupAllControls.Location = new System.Drawing.Point(0, 32);
            this.groupAllControls.Name = "groupAllControls";
            this.groupAllControls.Size = new System.Drawing.Size(413, 510);
            this.groupAllControls.TabIndex = 15;
            this.groupAllControls.TabStop = false;
            // 
            // btnFlashRST
            // 
            this.btnFlashRST.Location = new System.Drawing.Point(146, 88);
            this.btnFlashRST.Name = "btnFlashRST";
            this.btnFlashRST.Size = new System.Drawing.Size(100, 23);
            this.btnFlashRST.TabIndex = 71;
            this.btnFlashRST.Text = "Flash RST";
            this.toolTip.SetToolTip(this.btnFlashRST, "Make 5 pulses on RST line");
            this.btnFlashRST.UseVisualStyleBackColor = true;
            this.btnFlashRST.Click += new System.EventHandler(this.btnFlashRST_Click);
            // 
            // btnFlashDC
            // 
            this.btnFlashDC.Location = new System.Drawing.Point(146, 65);
            this.btnFlashDC.Name = "btnFlashDC";
            this.btnFlashDC.Size = new System.Drawing.Size(100, 23);
            this.btnFlashDC.TabIndex = 70;
            this.btnFlashDC.Text = "Flash DC";
            this.toolTip.SetToolTip(this.btnFlashDC, "Make 5 pulses on DC line");
            this.btnFlashDC.UseVisualStyleBackColor = true;
            this.btnFlashDC.Click += new System.EventHandler(this.btnFlashDC_Click);
            // 
            // btnFlashDD
            // 
            this.btnFlashDD.Location = new System.Drawing.Point(146, 42);
            this.btnFlashDD.Name = "btnFlashDD";
            this.btnFlashDD.Size = new System.Drawing.Size(100, 23);
            this.btnFlashDD.TabIndex = 69;
            this.btnFlashDD.Text = "Flash DD";
            this.toolTip.SetToolTip(this.btnFlashDD, "Make 5 pulses on DD line");
            this.btnFlashDD.UseVisualStyleBackColor = true;
            this.btnFlashDD.Click += new System.EventHandler(this.btnFlashDD_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnChipErase);
            this.groupBox4.Controls.Add(this.filename);
            this.groupBox4.Controls.Add(this.btnRead);
            this.groupBox4.Controls.Add(this.endAddress);
            this.groupBox4.Controls.Add(this.btnVerify);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.cbVerifyAfterWrite);
            this.groupBox4.Controls.Add(this.startAddress);
            this.groupBox4.Controls.Add(this.btnWrite);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.btnSelectFile);
            this.groupBox4.Controls.Add(this.cbErasePage);
            this.groupBox4.Location = new System.Drawing.Point(7, 185);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(398, 126);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "READ / WRITE";
            // 
            // filename
            // 
            this.filename.Location = new System.Drawing.Point(55, 19);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(268, 20);
            this.filename.TabIndex = 8;
            this.filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // endAddress
            // 
            this.endAddress.Location = new System.Drawing.Point(119, 69);
            this.endAddress.Name = "endAddress";
            this.endAddress.Size = new System.Drawing.Size(100, 20);
            this.endAddress.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(60, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 23);
            this.label10.TabIndex = 55;
            this.label10.Text = "End";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // startAddress
            // 
            this.startAddress.Location = new System.Drawing.Point(119, 45);
            this.startAddress.Name = "startAddress";
            this.startAddress.Size = new System.Drawing.Size(100, 20);
            this.startAddress.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(63, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 23);
            this.label9.TabIndex = 53;
            this.label9.Text = "Start";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(329, 17);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(28, 23);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // cbErasePage
            // 
            this.cbErasePage.AutoSize = true;
            this.cbErasePage.Checked = true;
            this.cbErasePage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbErasePage.Location = new System.Drawing.Point(256, 71);
            this.cbErasePage.Name = "cbErasePage";
            this.cbErasePage.Size = new System.Drawing.Size(80, 17);
            this.cbErasePage.TabIndex = 7;
            this.cbErasePage.Text = "Erase page";
            this.cbErasePage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.btnFlashCC);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.PxSELBITS);
            this.groupBox3.Controls.Add(this.PxSEL);
            this.groupBox3.Controls.Add(this.PxBITS);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.Px);
            this.groupBox3.Controls.Add(this.PxDIR);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(252, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 167);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Check CC LED";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 23);
            this.label18.TabIndex = 66;
            this.label18.Text = "PxSEL Bits";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFlashCC
            // 
            this.btnFlashCC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFlashCC.Location = new System.Drawing.Point(9, 135);
            this.btnFlashCC.Name = "btnFlashCC";
            this.btnFlashCC.Size = new System.Drawing.Size(133, 23);
            this.btnFlashCC.TabIndex = 30;
            this.btnFlashCC.Text = "5 pulses on P2.3 && P2.4";
            this.toolTip.SetToolTip(this.btnFlashCC, "Make 5 pulses on target P1.1 line");
            this.btnFlashCC.UseVisualStyleBackColor = true;
            this.btnFlashCC.Click += new System.EventHandler(this.btnFlashCC_Click);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(37, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 23);
            this.label14.TabIndex = 57;
            this.label14.Text = "PxSEL";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PxSELBITS
            // 
            this.PxSELBITS.AcceptsReturn = true;
            this.PxSELBITS.Location = new System.Drawing.Point(86, 18);
            this.PxSELBITS.Name = "PxSELBITS";
            this.PxSELBITS.Size = new System.Drawing.Size(33, 20);
            this.PxSELBITS.TabIndex = 20;
            this.PxSELBITS.Text = "0x06";
            this.PxSELBITS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PxSEL
            // 
            this.PxSEL.Location = new System.Drawing.Point(86, 41);
            this.PxSEL.Name = "PxSEL";
            this.PxSEL.Size = new System.Drawing.Size(33, 20);
            this.PxSEL.TabIndex = 21;
            this.PxSEL.Text = "0xF5";
            this.PxSEL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PxBITS
            // 
            this.PxBITS.Location = new System.Drawing.Point(86, 65);
            this.PxBITS.Name = "PxBITS";
            this.PxBITS.Size = new System.Drawing.Size(33, 20);
            this.PxBITS.TabIndex = 22;
            this.PxBITS.Text = "0x18";
            this.PxBITS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(37, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 23);
            this.label15.TabIndex = 59;
            this.label15.Text = "Px";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(39, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 23);
            this.label17.TabIndex = 63;
            this.label17.Text = "PxBits";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Px
            // 
            this.Px.Location = new System.Drawing.Point(86, 88);
            this.Px.Name = "Px";
            this.Px.Size = new System.Drawing.Size(33, 20);
            this.Px.TabIndex = 23;
            this.Px.Text = "0xA0";
            this.Px.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PxDIR
            // 
            this.PxDIR.Location = new System.Drawing.Point(86, 112);
            this.PxDIR.Name = "PxDIR";
            this.PxDIR.Size = new System.Drawing.Size(33, 20);
            this.PxDIR.TabIndex = 24;
            this.PxDIR.Text = "0xFF";
            this.PxDIR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(37, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 23);
            this.label16.TabIndex = 61;
            this.label16.Text = "PxDIR";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label_TIMERS_OFF);
            this.groupBox2.Controls.Add(this.label_DMA_PAUSE);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label_TIMER_SUSPEND);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label_SEL_FLASH_INFO);
            this.groupBox2.Location = new System.Drawing.Point(202, 397);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 103);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CC CONFIG BYTE";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(25, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 23);
            this.label13.TabIndex = 39;
            this.label13.Text = "TIMERS OFF (0x08)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_TIMERS_OFF
            // 
            this.label_TIMERS_OFF.BackColor = System.Drawing.Color.White;
            this.label_TIMERS_OFF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_TIMERS_OFF.Location = new System.Drawing.Point(7, 20);
            this.label_TIMERS_OFF.Name = "label_TIMERS_OFF";
            this.label_TIMERS_OFF.Size = new System.Drawing.Size(14, 14);
            this.label_TIMERS_OFF.TabIndex = 38;
            this.label_TIMERS_OFF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_DMA_PAUSE
            // 
            this.label_DMA_PAUSE.BackColor = System.Drawing.Color.White;
            this.label_DMA_PAUSE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_DMA_PAUSE.Location = new System.Drawing.Point(7, 40);
            this.label_DMA_PAUSE.Name = "label_DMA_PAUSE";
            this.label_DMA_PAUSE.Size = new System.Drawing.Size(14, 14);
            this.label_DMA_PAUSE.TabIndex = 40;
            this.label_DMA_PAUSE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(25, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 23);
            this.label11.TabIndex = 41;
            this.label11.Text = "DMA PAUSE (0x04)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_TIMER_SUSPEND
            // 
            this.label_TIMER_SUSPEND.BackColor = System.Drawing.Color.White;
            this.label_TIMER_SUSPEND.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_TIMER_SUSPEND.Location = new System.Drawing.Point(7, 60);
            this.label_TIMER_SUSPEND.Name = "label_TIMER_SUSPEND";
            this.label_TIMER_SUSPEND.Size = new System.Drawing.Size(14, 14);
            this.label_TIMER_SUSPEND.TabIndex = 42;
            this.label_TIMER_SUSPEND.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(25, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 23);
            this.label12.TabIndex = 43;
            this.label12.Text = "TIMER SUSPEND (0x02)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(25, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 23);
            this.label8.TabIndex = 45;
            this.label8.Text = "SEL_FLASH_INFO (0x01)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_SEL_FLASH_INFO
            // 
            this.label_SEL_FLASH_INFO.BackColor = System.Drawing.Color.White;
            this.label_SEL_FLASH_INFO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_SEL_FLASH_INFO.Location = new System.Drawing.Point(7, 80);
            this.label_SEL_FLASH_INFO.Name = "label_SEL_FLASH_INFO";
            this.label_SEL_FLASH_INFO.Size = new System.Drawing.Size(14, 14);
            this.label_SEL_FLASH_INFO.TabIndex = 44;
            this.label_SEL_FLASH_INFO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetStatus
            // 
            this.btnGetStatus.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGetStatus.Location = new System.Drawing.Point(202, 328);
            this.btnGetStatus.Name = "btnGetStatus";
            this.btnGetStatus.Size = new System.Drawing.Size(100, 23);
            this.btnGetStatus.TabIndex = 13;
            this.btnGetStatus.Text = "Get Status";
            this.btnGetStatus.UseVisualStyleBackColor = true;
            this.btnGetStatus.Click += new System.EventHandler(this.btnGetStatus_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(305, 328);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 23);
            this.btnExit.TabIndex = 40;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelChipSize
            // 
            this.labelChipSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelChipSize.Location = new System.Drawing.Point(192, 147);
            this.labelChipSize.Name = "labelChipSize";
            this.labelChipSize.Size = new System.Drawing.Size(54, 22);
            this.labelChipSize.TabIndex = 29;
            this.labelChipSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(146, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 23);
            this.label7.TabIndex = 28;
            this.label7.Text = "Size";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chipModel
            // 
            this.chipModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.chipModel.FormattingEnabled = true;
            this.chipModel.Location = new System.Drawing.Point(146, 122);
            this.chipModel.Name = "chipModel";
            this.chipModel.Size = new System.Drawing.Size(100, 21);
            this.chipModel.TabIndex = 3;
            this.chipModel.SelectedIndexChanged += new System.EventHandler(this.chipModel_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.status_CHIP_ERASE_DONE);
            this.groupBox1.Controls.Add(this.status_PCON_IDLE);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.status_CPU_HALTED);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.status_POWER_MODE_0);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.status_HALT_STATUS);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.status_DEBUG_LOCKED);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.status_OSCILLATOR_STABLE);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.status_STACK_OVERFLOW);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(7, 317);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 182);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CC STATUS BYTE";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(25, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "CHIP ERASE DONE (0x80)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_CHIP_ERASE_DONE
            // 
            this.status_CHIP_ERASE_DONE.BackColor = System.Drawing.Color.White;
            this.status_CHIP_ERASE_DONE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_CHIP_ERASE_DONE.Location = new System.Drawing.Point(7, 20);
            this.status_CHIP_ERASE_DONE.Name = "status_CHIP_ERASE_DONE";
            this.status_CHIP_ERASE_DONE.Size = new System.Drawing.Size(14, 14);
            this.status_CHIP_ERASE_DONE.TabIndex = 10;
            this.status_CHIP_ERASE_DONE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_PCON_IDLE
            // 
            this.status_PCON_IDLE.BackColor = System.Drawing.Color.White;
            this.status_PCON_IDLE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_PCON_IDLE.Location = new System.Drawing.Point(7, 40);
            this.status_PCON_IDLE.Name = "status_PCON_IDLE";
            this.status_PCON_IDLE.Size = new System.Drawing.Size(14, 14);
            this.status_PCON_IDLE.TabIndex = 12;
            this.status_PCON_IDLE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(25, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "PCON IDLE (0x40)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_CPU_HALTED
            // 
            this.status_CPU_HALTED.BackColor = System.Drawing.Color.White;
            this.status_CPU_HALTED.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_CPU_HALTED.Location = new System.Drawing.Point(7, 60);
            this.status_CPU_HALTED.Name = "status_CPU_HALTED";
            this.status_CPU_HALTED.Size = new System.Drawing.Size(14, 14);
            this.status_CPU_HALTED.TabIndex = 14;
            this.status_CPU_HALTED.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(25, 56);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(158, 23);
            this.label20.TabIndex = 15;
            this.label20.Text = "CPU HALTED (0x20)";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_POWER_MODE_0
            // 
            this.status_POWER_MODE_0.BackColor = System.Drawing.Color.White;
            this.status_POWER_MODE_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_POWER_MODE_0.Location = new System.Drawing.Point(7, 80);
            this.status_POWER_MODE_0.Name = "status_POWER_MODE_0";
            this.status_POWER_MODE_0.Size = new System.Drawing.Size(14, 14);
            this.status_POWER_MODE_0.TabIndex = 16;
            this.status_POWER_MODE_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(25, 76);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(158, 23);
            this.label21.TabIndex = 17;
            this.label21.Text = "POWER MODE 0 (0x10)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_HALT_STATUS
            // 
            this.status_HALT_STATUS.BackColor = System.Drawing.Color.White;
            this.status_HALT_STATUS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_HALT_STATUS.Location = new System.Drawing.Point(7, 100);
            this.status_HALT_STATUS.Name = "status_HALT_STATUS";
            this.status_HALT_STATUS.Size = new System.Drawing.Size(14, 14);
            this.status_HALT_STATUS.TabIndex = 18;
            this.status_HALT_STATUS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(25, 96);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(158, 23);
            this.label22.TabIndex = 19;
            this.label22.Text = "HALT STATUS (0x08)";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_DEBUG_LOCKED
            // 
            this.status_DEBUG_LOCKED.BackColor = System.Drawing.Color.White;
            this.status_DEBUG_LOCKED.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_DEBUG_LOCKED.Location = new System.Drawing.Point(7, 120);
            this.status_DEBUG_LOCKED.Name = "status_DEBUG_LOCKED";
            this.status_DEBUG_LOCKED.Size = new System.Drawing.Size(14, 14);
            this.status_DEBUG_LOCKED.TabIndex = 20;
            this.status_DEBUG_LOCKED.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(25, 116);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(158, 23);
            this.label24.TabIndex = 21;
            this.label24.Text = "DEBUG LOCKED (0x04)";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_OSCILLATOR_STABLE
            // 
            this.status_OSCILLATOR_STABLE.BackColor = System.Drawing.Color.White;
            this.status_OSCILLATOR_STABLE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_OSCILLATOR_STABLE.Location = new System.Drawing.Point(7, 140);
            this.status_OSCILLATOR_STABLE.Name = "status_OSCILLATOR_STABLE";
            this.status_OSCILLATOR_STABLE.Size = new System.Drawing.Size(14, 14);
            this.status_OSCILLATOR_STABLE.TabIndex = 22;
            this.status_OSCILLATOR_STABLE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(25, 136);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(158, 23);
            this.label25.TabIndex = 23;
            this.label25.Text = "OSCILLATOR STABLE (0x02)";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_STACK_OVERFLOW
            // 
            this.status_STACK_OVERFLOW.BackColor = System.Drawing.Color.White;
            this.status_STACK_OVERFLOW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status_STACK_OVERFLOW.Location = new System.Drawing.Point(7, 160);
            this.status_STACK_OVERFLOW.Name = "status_STACK_OVERFLOW";
            this.status_STACK_OVERFLOW.Size = new System.Drawing.Size(14, 14);
            this.status_STACK_OVERFLOW.TabIndex = 24;
            this.status_STACK_OVERFLOW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(25, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 23);
            this.label6.TabIndex = 25;
            this.label6.Text = "STACK OVERFLOW (0x01)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFile
            // 
            this.openFile.CheckFileExists = false;
            this.openFile.Filter = "BIN|*.bin|All files|*.*";
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // btnChipErase
            // 
            this.btnChipErase.Location = new System.Drawing.Point(4, 95);
            this.btnChipErase.Name = "btnChipErase";
            this.btnChipErase.Size = new System.Drawing.Size(84, 23);
            this.btnChipErase.TabIndex = 56;
            this.btnChipErase.Text = "Chip Erase";
            this.btnChipErase.UseVisualStyleBackColor = true;
            this.btnChipErase.Click += new System.EventHandler(this.btnChipErase_Click);
            // 
            // CCFlash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(413, 563);
            this.Controls.Add(this.groupAllControls);
            this.Controls.Add(this.status);
            this.Controls.Add(this.serialPorts);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbAutoConnect);
            this.Name = "CCFlash";
            this.Text = "CC.Flash";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CCFlash_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CCFlash_FormClosed);
            this.Load += new System.EventHandler(this.CCFlash_Load);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.groupAllControls.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox serialPorts;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.StatusStrip status;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.ToolStripStatusLabel statusLine;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelChipID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelChipSeries;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelRevision;
		private System.Windows.Forms.Button btnWrite;
		private System.Windows.Forms.Button btnRead;
		private System.Windows.Forms.Button btnVerify;
		private System.Windows.Forms.CheckBox cbVerifyAfterWrite;
		private System.Windows.Forms.CheckBox cbAutoConnect;
		private System.Windows.Forms.GroupBox groupAllControls;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label status_OSCILLATOR_STABLE;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label status_DEBUG_LOCKED;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label status_HALT_STATUS;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label status_POWER_MODE_0;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label status_CPU_HALTED;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label status_PCON_IDLE;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label status_CHIP_ERASE_DONE;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label status_STACK_OVERFLOW;
		private System.Windows.Forms.ComboBox chipModel;
		private System.Windows.Forms.Label labelChipSize;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label_SEL_FLASH_INFO;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label_TIMER_SUSPEND;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label_DMA_PAUSE;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label_TIMERS_OFF;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.TextBox filename;
		private System.Windows.Forms.CheckBox cbErasePage;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnGetStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox endAddress;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox startAddress;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnFlashCC;
		private System.Windows.Forms.TextBox PxSEL;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox Px;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox PxDIR;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox PxBITS;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ErrorProvider error;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox PxSELBITS;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnFlashRST;
		private System.Windows.Forms.Button btnFlashDC;
		private System.Windows.Forms.Button btnFlashDD;
		private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnChipErase;
    }
}

