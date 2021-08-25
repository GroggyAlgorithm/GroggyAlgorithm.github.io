#ifndef __STDATMEL52LCD_H__
/**********************************************************************************************************************************************
 @file stdAtmel52lcd.h
 @brief Header file for Keil and Atmel LCD things.
        Many of the macros are for reference.

 @author Tim Robbins
 @date 2021
 @version 2.0.0.0
**********************************************************************************************************************************************/
#define __STDATMEL52LCD_H__


//If it controls the display module itself. Leaves out annoying auto complete
#ifndef _IS_DISPLAY_MODULE
#define _IS_DISPLAY_MODULE   1
#endif


//LCD
//---------------------------------------------------------------------------------------------------------------------------------------------




#ifndef CLEARSCREEN
 //Clear screen - Hex code for clearing the screen.
 #define CLEARSCREEN                  0x0C
#endif

#ifndef LINESTART
 //Line start('\r') ascii hex code, returns to line start. AKA Carriage return.
 #define LINESTART				   	       0x0D
#endif

#ifndef NEXTLINE
 //Next line('\n') ascii hex code. Moves to the next line. AKA Line feed.
 #define NEXTLINE				           0x0A
#endif

#ifndef CLEAR
//The hex string clear signal for LCD for string
#define CLEAR				            '\x0C'
#endif

 #ifndef BSD
 //Backspace Alt definition. Backspace delete for string
 #define BSD 			'\b'
 #endif

 #ifndef LS
 //Carriage return alt definition. Moves to the Line start for string
 #define LS 			'\r'
 #endif

 #ifndef NL
 //New line. Line feed to start alt definition. for string
 #define NL				'\n'
 #endif

 #ifndef VT
 //Vertical tab down, down with no return for string
 #define VT			   	'\v'
 #endif

  #ifndef CS
 //Clear screen alt def, Form Feed alt definition for string
 #define CS             '\f'
 #endif



 #ifndef BS
 //Backspace ascii hex code.
 #define BS				0x08
 #endif

 #ifndef TAB
 //Horizontal Tab ascii hex code.
 #define TAB			0x09
 #endif

 #ifndef LF
 //Linefeed ascii hex code. Moves to the next line
 #define LF				0x0A
 #endif

 #ifndef FF
 //Formfeed alt def, new form, new screen
 #define FF				0x0C
 #endif



 #ifndef CR
 //Carriage return alt def, ascii hex code, returns to line start.
 #define CR				0x0D
 #endif

 #ifndef UP
 //Up 1 line ascii hex code
 #define UP				0x1C
 #endif

 #ifndef DN
 //Down 1 line ascii hex code
 #define DN				0x19
 #endif

 #ifndef RT
 //Right 1 char ascii hex code
 #define RT				0x1A
 #endif

 #ifndef LFT
 //Left 1 char ascii hex code
 #define LFT			0x1D
 #endif

 #ifndef EOL
 //End of Line ascii hex code
 #define EOL			0x17
 #endif

 #ifndef LINE_END
 //String line end. EOL alt def for easy string things
 #define LINE_END     '\x17'
 #endif


 //Serial--------------------------------------------------------------------------------------------
#ifndef CLEAR_SCREEN
//Example of clearing the display modules LCD screen through the serial port
#define CLEAR_SCREEN()                while(TI == 0); TI = 0; SBUF = CLEARSCREEN 
#endif



#if _IS_DISPLAY_MODULE

 #ifndef DATA_BUS_PORT
 //The port the LCD's Databus pins are on
 #define DATA_BUS_PORT		P0
 #endif

 #ifndef COMMAND_PORT
 //The port the LCD's Command Pins are on(RS, RW, E)
 #define COMMAND_PORT		P2
 #endif

 #ifndef LCD_LINES
 //The amount of lines(rows) on the LCD
 #define LCD_LINES			4
 #endif

 #ifndef LCD_COLUMNS
 //The amount of columns(width/possible characters per line) on the LCD
 #define LCD_COLUMNS		20
 #endif

 #ifndef SET_DATA_PINS
 //Sets the Data Bus Pins on the LCD from the port to the pins passed.
 //Used for laziness and quick decleration
 #define SET_DATA_PINS(port, d0, d1, d2, d3, d4, d5, d6, d7)  	sbit DataPin0 = port^d0; sbit DataPin1 = port^d1; sbit DataPin2 = port^d2; sbit DataPin3 = port^d3; sbit DataPin4 = port^d4; sbit DataPin5 = port^d5; sbit DataPin6 = port^d6; sbit DataPin7 = port^d7
 #endif

 #ifndef SET_LCD_COMMAND_PINS
 //Sets the pins that are used for commands(EPin, RSPin, RWPin). RW = Read or Write pin, RSPin = Data Input or Command/instruction input, EPin Enable signal pin
 //Used for laziness and quick decleration
 #define SET_LCD_COMMAND_PINS(port, e, rs, rw)   					sbit EPin = port^e; sbit RSPin = port^rs; sbit RWPin = port^rw
 #endif


 #ifndef CURSOR_ON
 //Sets cursor on
 #define CURSOR_ON()				RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin3 = 1; DataPin2 = 1; DataPin1 = 1; DataPin0 = 0; WaitForBusyFlag()
 #endif

 #ifndef CURSOR_BLINK_ON
 //Sets cursor on with blink
 #define CURSOR_BLINK_ON()		RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin3 = 1; DataPin2 = 1; DataPin1 = 1; DataPin0 = 1; WaitForBusyFlag()
 #endif

 #ifndef CURSOR_OFF
 //Sets cursor off
 #define CURSOR_OFF()			RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin3 = 1; DataPin2 = 1; DataPin1 = 0; DataPin0 = 0; WaitForBusyFlag()
 #endif
 
 #ifndef CURSOR_BLINK_OFF
 //Sets cursor blink off. It's an alt def of CursorOn()
 #define CURSOR_BLINK_OFF()		RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin3 = 1; DataPin2 = 1; DataPin1 = 1; DataPin0 = 0; WaitForBusyFlag()
 #endif

 #ifndef CURSOR_SHIFT_RIGHT
 //Shifts the cursor right
 #define CURSOR_SHIFT_RIGHT()		RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin4 = 1; DataPin2 = 1; WaitForBusyFlag()
 #endif 
 
 #ifndef CURSOR_SHIFT_LEFT
 //Shifts the cursor left
 #define CURSOR_SHIFT_LEFT()		RSPin = 0; RWPin = 0; Data_Bus_Port = 0x00; DataPin4 = 1; WaitForBusyFlag()
 #endif


 
 #ifndef SET_DD_ADDRESS
 //Sets the display ram address to the passed address.
 //Sets the command pins, Data pin 7 to high, loads the address
 //into the data bus port, and calls the wait for busy flag function.
 #define SET_DD_ADDRESS(address)    RSPin = 0; RWPin = 0; Data_Bus_Port = address; DataPin7 = 1; WaitForBusyFlag()
 #endif

 #ifndef SET_CG_ADDRESS
 //Sets the character generator ram address to the passed address.
 //Sets the command pins, Data pin 6 to high, loads the address
 //into the data bus port, and calls the wait for busy flag function.
 #define SET_CG_ADDRESS(address)    RSPin = 0; RWPin = 0; Data_Bus_Port = address; DataPin7 = 0;  DataPin6 = 1; WaitForBusyFlag()
 #endif

 #ifndef WRITE_DD_ADDRESS
 //Sets the display ram address and writes to the display ram address at the passed position.
 //Sets RSPin high and loads the data into the data bus port.
 //Calls the wait for busy flag function.
 #define WRITE_DD_ADDRESS(position, data)    SET_DD_ADDRESS(position); RSPin = 1; RWPin = 0; Data_Bus_Port = data; WaitForBusyFlag()
 #endif

 #ifndef WRITE_CG_ADDRESS
 //Sets the character generator ram address and writes to the CG ram.
 //Sets RSPin high and loads the data into the data bus port.
 //Calls the wait for busy flag function after.
 #define WRITE_CG_ADDRESS(address, data)    SET_CG_ADDRESS(address); RSPin = 1; RWPin = 0; Data_Bus_Port = data; WaitForBusyFlag()
 #endif 

 #ifndef WRITE_TO_RAM
 //Writes to the last ram address set.
 //Sets RSPin high and loads the data into the data bus port.
 //Calls the wait for busy flag function after.
 #define WRITE_TO_RAM(data)        RSPin = 1; RWPin = 0; Data_Bus_Port = data; WaitForBusyFlag()
 #endif 


 #ifndef NEW_RAM_POSITION
 // Finds and Sets the Appropriate display ram address, the active column, and line.
 // Takes the lcd, Line to display on and the Column to display at.
 #define NEW_RAM_POSITION(_lcd, _line, _column)     _lcd.ActiveLine = (_line - 1); _lcd.ActiveColumn = (_column - 1); _lcd.DisplayRamAddress = ((LineStartAddress[_lcd.ActiveLine]) + _lcd.ActiveColumn); SetDDAddress(_lcd.DisplayRamAddress)
 #endif


 #ifndef FIND_RAM_POSITION
 // Finds the Appropriate display address. 
 // Takes an array of line start addresses, the Line to display on and the Column to display at. 
 // Returns the display address.
 #define FIND_RAM_POSITION(line_start_addresses, _line, _column)    ((line_start_addresses[_line - 1]) + (_column - 1))
 #endif // FindRamPosition


 #ifndef FIND_RAM_ADDRESS
 //Finds the Hex address of the LCD by taking the Starting address
 //and adding the column - 1
 //Returns the Hex display address
 #define FIND_RAM_ADDRESS(start_address, _column)    (start_address + (_column - 1))
 #endif


 #ifndef FIND_LINE_END
 //Finds and returns the end of a display line
 //Takes an array of line start addresses and the line to find
 #define FIND_LINE_END(line_start_addresses, _line)    (line_start_addresses[_line - 1] + (LCD_Columns - 1))
 #endif

 #ifndef GET_LCD_POSITION
 //Gets the passed lcd position from the lcd's active line and active column
 #define GET_LCD_POSITION(_lcd)		(_lcd.ActiveLine + _lcd.ActiveColumn)
 #endif

 #ifndef GET_LCD_RAM_POSITION
 //Gets the passed lcd position from the lcd's active line and active column
 #define GET_LCD_RAMP_OSITION(_lcd)		(LineStartAddress[_lcd.ActiveLine] + _lcd.ActiveColumn)
 #endif

#endif





#endif //__STDATMEL52LCD_H__