Implement Texas flash programming protocol via Arduino board.
See
http://www.ti.com/tool/cc-debugger
and
http://akb77.com/g/rf/program-cc-debugger-cc2511-with-arduino/
for details

1. Upload CC_Flash.pde sketch to Arduino
2. Connect
    Target          Arduino
        DC (P2.1) - pin 5
        DD (P2.2) - pin 6
        RST       - pin 7
        GND       - GND
3. Start program CC.Flash.exe
