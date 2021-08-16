#ifndef __STDATMEL52_H__
/**********************************************************************************************************************************************
 @file stdAtmel52.h
 @brief Header file for Keil and Atmel 8052 things. Contains Timer 2 definitions.
        Many of the macros are for reference.
 @author Tim Robbins
 @date 2021
 @version 2.0.0.0
**********************************************************************************************************************************************/
#define __STDATMEL52_H__


/*  BYTE Register Addresses  */
sfr P0    = 0x80; //Bit addressable Port 0                Address  0x80
sfr SP    = 0x81; //Stack pointer                         Address  0x81
sfr DPL   = 0x82; //Data pointer low?                     Address  0x82
sfr DPH   = 0x83; //Data pointer high?                    Address  0x83
sfr PCON  = 0x87; //Power Control Register                Address  0x87
sfr TCON  = 0x88; //Bit addressable values Timers 0, 1    Address  0x88
sfr TMOD  = 0x89; //Timer modes for timers 0, 1           Address  0x89
sfr TL0   = 0x8A; //Timer 0 low byte                      Address  0x8A
sfr TL1   = 0x8B; //Timer 1 low byte                      Address  0x8B
sfr TH0   = 0x8C; //Timer 0 high byte                     Address  0x8C
sfr TH1   = 0x8D; //Timer 1 high byte                     Address  0x8D

sfr P1    = 0x90; //Bit addressable Port 1                Address 0x90

sfr SCON  = 0x98; //Bit addressable Serial pot control register      Address 0x98   
sfr SBUF  = 0x99; //Serial buffer                                   Address 0x99    

sfr P2    = 0xA0; //Bit addressable Port 2                          Address 0xA0                            
sfr IE    = 0xA8; //Bit addressable Interrupt Enable register   Address 0xA8                            

sfr P3    = 0xB0; //Bit addressable Port 3                       Address  0xB                                   
sfr IP    = 0xB8; //Bit addressable Interrupt Priority register   Address 0xB8                                  

sfr PSW   = 0xD0; //Bit addressable. Program status word register, reflects the current state of the cpu  Address 0xD0              

sfr ACC   = 0xE0; //Bit addressable. The accumulator  Address 0xE0                                  

sfr B     = 0xF0; //Bit addressable b, used for multiplication and division  Address 0xF0                                                   


#define __WatchDogUsed    0

    #if __WatchDogUsed

    //Atmel AT89S52 manual shows the register for the watch dog timer reset at 0xA6
    sfr WDTRST  = 0xA6; //The watch dog timer reset register. Write only register, cannot read.
    #ifndef EnableWatchDog
    //Enables the watch dog timer. Watch dog timer is defaulted to disable from exiting reset
    #define EnableWatchDog()    WDTRST = 0x1E; WDTRST = 0x1E    
    #endif

#endif


/*  8052 Extensions  -----------------------------------------------------------------------*/
sfr T2CON  = 0xC8; //Bit addressable Timer 2
sfr T2MOD  = 0xC9; //Timer modes for timer 2
sfr RCAP2L = 0xCA; //Timer 2 reload capture low byte
sfr RCAP2H = 0xCB; //Timer 2 reload capture high byte
sfr TL2    = 0xCC; //Timer 2 low byte
sfr TH2    = 0xCD; //Timer 2 High byte

/*------------------------------------------------
T2CON Bit Registers
------------------------------------------------*/
sbit TF2  =   T2CON^7;          /* Timer 2 overflow flag */
sbit EXF2 =   T2CON^6;          /* Timer 2 external flag */
sbit RCLK =   T2CON^5;          /*Receive clock enable. 0=Serial clock uses Timer 1 overflow, 1=Timer 2 */
sbit TCLK =   T2CON^4;          /*Transmit clock enable. 0=Serial clock uses Timer 1 overflow, 1=Timer 2 */
sbit EXEN2=   T2CON^3;          /* Timer 2 external enable. If 0, Timer 2 ignores T2EX events*/
sbit TR2  =   T2CON^2;          /*Timer 2 Start/Stop control bit. 0=Stop timer, 1=Start timer */
sbit C_T2 =   T2CON^1;          /*Timer 2 Counter/Timer control bit. 0=Timer, 1=Counter */
sbit CP_RL2 = T2CON^0;          /*Timer 2 Capture/Reload select bit. 0=Reload, 1=Capture select */

/*------------------------------------------------
T2MOD Bit Values. Bits can be set with sfr ^= (value << n)
------------------------------------------------*/
#ifndef DCEN_
#define DCEN_   0x01    /* 1=Timer 2 can be configured as up/down counter */
#endif

#ifndef T2OE_
#define T2OE_   0x02    /* Timer 2 output enable */
#endif
/*  End 8052 Extensions  -----------------------------------------------------------------------*/



/*------------------------------------------------
PSW at 0xD0 Bit Registers
------------------------------------------------*/
sbit CY    = PSW^7; //Carry flag
sbit AC    = PSW^6; //Auxillary carry flag (for BCD operations)
sbit F0    = PSW^5; //User Flag 0, for general purposes
sbit RS1   = PSW^4; //Register bank select control bit 1
sbit RS0   = PSW^3; //Register bank select control bit 0
sbit OV    = PSW^2; //Overflow flag
sbit F1    = PSW^1; //User Flag 1, for general purposes
sbit P     = PSW^0; //8052 only, parity flag. Reflects the # of 1's in the Accumultor 



/*------------------------------------------------
PCON Bit Values. Bits can be set with sfr ^= (value << n)
------------------------------------------------*/
#ifndef IDL_
//Idle mode bit. Cleared by hardware when interrupt or reset occurs. Set to enter Idle mode
#define IDL_    0
#endif

#ifndef STOP_
//Alt Definition for PD(Power down mode bit). Cleared on reset, set to enter power down mode
#define STOP_   1
#endif

#ifndef PD_
//Power down mode bit. Cleared on reset, set to enter power down mode
#define PD_     1    
#endif

#ifndef GF0_
//General purpose Flag. Cleared by user for general purpose usage. 
//Set by user for general purpose usage.
#define GF0_    2
#endif

#ifndef GF1_
//General purpose Flag. Cleared by user for general purpose usage. 
//Set by user for general purpose usage.
#define GF1_    3
#endif

#ifndef POF_
// Power-Off Flag
// Cleared to recognize next reset type.
// Set by hardware when VCC rises from 0 to its nominal voltage. Can also be set by software.
#define POF_    4
#endif


#ifndef SMOD_0
//Serial port Mode bit 0 for UART
//Cleared to select SM0 bit in SCON register.
//Set to select FE bit in SCON register.
#define SMOD_0   6
#endif

#ifndef SMOD_1
//Serial port Mode bit 1 for UART
//Set to select double baud rate in mode 1, 2 or 3.
#define SMOD_1   7
#endif

#ifndef SETBIT_POWERCONTROL
//Sets the power control bits at N location
//Example: SETBIT_POWERCONTROL(SMOD_1)      =  PCON |= (1 << 7);
#define SETBIT_POWERCONTROL(n)           PCON |= (1 << n)
#endif


#ifndef CLEARBIT_POWERCONTROL
//Clears the power control bits at N location
//Example: CLEARBIT_POWERCONTROL(SMOD_1)      =  PCON  &= ~(1 << 7)
#define CLEARBIT_POWERCONTROL(n)           PCON  &= ~(1 << n)
#endif

/*------------------------------------------------
TCON Bit Values
------------------------------------------------*/
sbit TF1   = TCON^7; //Timer 1 overflow flag
sbit TR1   = TCON^6; //Timer 1 Run control bit
sbit TF0   = TCON^5; //Timer 0 overflow flag
sbit TR0   = TCON^4; //Timer 0 Run control bit
sbit IE1   = TCON^3; //Interupt 1 external edge interrupt
sbit IT1   = TCON^2; //Interrupt 1 type control bit
sbit IE0   = TCON^1; //Interrupt 0 external edge interrupt
sbit IT0   = TCON^0; //Interrupt 0 type control bit

/*------------------------------------------------
TMOD Bit Values. Bits can be set with sfr ^= (value << n)
------------------------------------------------*/

#ifndef T0_M0
#define T0_M0_      0x01
#endif

#ifndef T0_M1_
#define T0_M1_      0x02
#endif

#ifndef T0_M2_
#define T0_M2_      0x03
#endif

#ifndef T0_M3_
#define T0_M3_      0x04
#endif

#ifndef T0_GATE_
#define T0_GATE_    0x08
#endif

#ifndef T0_MASK_
#define T0_MASK_    0x0F
#endif

#ifndef T1_M0_
#define T1_M0_   0x10
#endif

#ifndef T1_M1_
#define T1_M1_   0x20
#endif

#ifndef T1_CT_
#define T1_CT_   0x40
#endif

#ifndef T1_GATE_
#define T1_GATE_ 0x80
#endif

#ifndef T1_MASK_
#define T1_MASK_ 0xF0
#endif





/*------------------------------------------------
IE Bit Registers
------------------------------------------------*/
sbit EX0  = IE^0;       /* 1=Enable External interrupt 0 */
sbit ET0  = IE^1;       /* 1=Enable Timer 0 interrupt */
sbit EX1  = IE^2;       /* 1=Enable External interrupt 1 */
sbit ET1  = IE^3;       /* 1=Enable Timer 1 interrupt */
sbit ES   = IE^4;       /* 1=Enable Serial port interrupt */
sbit ET2  = IE^5;       /* 1=Enable Timer 2 interrupt */
sbit EA   = IE^7;       /* 0=Disable all interrupts */


/*------------------------------------------------
P0
------------------------------------------------*/
sbit P0_0 = P0^0; //Port IO Pin
sbit P0_1 = P0^1; //Port IO Pin
sbit P0_2 = P0^2; //Port IO Pin
sbit P0_3 = P0^3; //Port IO Pin
sbit P0_4 = P0^4; //Port IO Pin
sbit P0_5 = P0^5; //Port IO Pin
sbit P0_6 = P0^6; //Port IO Pin
sbit P0_7 = P0^7; //Port IO Pin



/*------------------------------------------------
P1
------------------------------------------------*/
sbit P1_0 = P1^0; //Port IO Pin
sbit P1_1 = P1^1; //Port IO Pin
sbit P1_2 = P1^2; //Port IO Pin
sbit P1_3 = P1^3; //Port IO Pin
sbit P1_4 = P1^4; //Port IO Pin
sbit P1_5 = P1^5; //Port IO Pin
sbit P1_6 = P1^6; //Port IO Pin
sbit P1_7 = P1^7; //Port IO Pin

#ifndef T2EX
#define T2EX  P1_1 // 8052 only
#endif

#ifndef T2
#define T2    P1_0 // 8052 only
#endif

// sbit T2EX  = P1^1; // 8052 only
// sbit T2    = P1^0; // 8052 only


/*------------------------------------------------
P2
------------------------------------------------*/
sbit P2_0 = P2^0; //Port IO Pin
sbit P2_1 = P2^1; //Port IO Pin
sbit P2_2 = P2^2; //Port IO Pin
sbit P2_3 = P2^3; //Port IO Pin
sbit P2_4 = P2^4; //Port IO Pin
sbit P2_5 = P2^5; //Port IO Pin
sbit P2_6 = P2^6; //Port IO Pin
sbit P2_7 = P2^7; //Port IO Pin




/*------------------------------------------------
P3
------------------------------------------------*/
sbit P3_0 = P3^0; //Port IO Pin
sbit P3_1 = P3^1; //Port IO Pin
sbit P3_2 = P3^2; //Port IO Pin
sbit P3_3 = P3^3; //Port IO Pin
sbit P3_4 = P3^4; //Port IO Pin
sbit P3_5 = P3^5; //Port IO Pin
sbit P3_6 = P3^6; //Port IO Pin
sbit P3_7 = P3^7; //Port IO Pin

sbit RXD  = 0xB0;       /* Serial data input */
sbit TXD  = 0xB1;       /* Serial data output */
sbit INT0 = 0xB2;       /* External interrupt 0 */
sbit INT1 = 0xB3;       /* External interrupt 1 */
sbit T0   = 0xB4;       /* Timer 0 external input */
sbit T1   = 0xB5;       /* Timer 1 external input */
sbit WR   = 0xB6;       /* External data memory write strobe */
sbit RD   = 0xB7;       /* External data memory read strobe */


/*------------------------------------------------
SCON Bit Registers
------------------------------------------------*/
sbit SM0   = SCON^7; //Serial port Mode 0 bit. Same bit as the FE bit.
sbit FE    = SCON^7; //Serial port framing error bit. Sets by hardware when an invalid stop bit is detected. Clear to reset the error state.
sbit SM1   = SCON^6; //Serial port Mode 1 bit.
sbit SM2   = SCON^5; //Serial port mode 2 bit. Multiprocessor communication enable bit
sbit REN   = SCON^4; //Serial Reception Enable bit. Ren = 0, prohibited, 1 = allowed.
sbit TB8   = SCON^3; //Serial Transmitter Bit 8 / Ninth bit to transmit in modes 2 and 3. o transmit a logic 0 in the 9th bit. Set to transmit a logic 1 in the 9th bit.
sbit RB8   = SCON^2; //Serial Receiver Bit 8 / Ninth bit received in modes 2 and 3. Cleared by hardware if 9th bit received is a logic 0. Set by hardware if 9th bit received is a logic 1. In mode 1, if SM2 = 0, RB8 is the received stop bit. In mode 0 RB8 is not used
sbit TI    = SCON^1; //Transmit interrupt flag
sbit RI    = SCON^0; //Receive interrupt flag



/*------------------------------------------------
IP Bit Registers
------------------------------------------------*/
sbit PX0  = IP^0;
sbit PT0  = IP^1;
sbit PX1  = IP^2;
sbit PT1  = IP^3;
sbit PS   = IP^4;
sbit PT2  = IP^5;
             



/*------------------------------------------------
Interrupt Vectors:
Interrupt Addresses  : Address = Interrupt number * 8 + 3

                0 EXTERNAL INT 0 0003h 
                1 TIMER/COUNTER 0 000Bh 
                2 EXTERNAL INT 1 0013h 
                3 TIMER/COUNTER 1 001Bh 
                4 SERIAL PORT 0023h 

                Interrupt
                Number Address 
                0 0003h 
                1 000Bh 
                2 0013h 
                3 001Bh 
                4 0023h 
                5 002Bh 
                6 0033h 
                7 003Bh 
                8 0043h 
                9 004Bh 
                10 0053h 
                11 005Bh 
                12 0063h 
                13 006Bh 
                14 0073h 
                15 007Bh 
                16 0083h 
                17 008Bh 
                18 0093h 
                19 009Bh 
                20 00A3h 
                21 00ABh 
                22 00B3h 
                23 00BBh 
                24 00C3h 
                25 00CBh 
                26 00D3h 
                27 00DBh 
                28 00E3h 
                29 00EBh 
                30 00F3h 
                31 00FBh 
 

------------------------------------------------*/
#ifndef IE0_VECTOR
#define IE0_VECTOR	0  /* 0x03 External Interrupt 0 */
#endif

#ifndef TF0_VECTOR
#define TF0_VECTOR	1  /* 0x0B Timer 0 */
#endif

#ifndef IE1_VECTOR
#define IE1_VECTOR	2  /* 0x13 External Interrupt 1 */
#endif

#ifndef TF1_VECTOR
#define TF1_VECTOR	3  /* 0x1B Timer 1 */
#endif

#ifndef SIO_VECTOR
#define SIO_VECTOR	4  /* 0x23 Serial port */
#endif

#ifndef TF2_VECTOR
#define TF2_VECTOR	5  /* 0x2B Timer 2 */
#endif

#ifndef EX2_VECTOR
#define EX2_VECTOR	5  /* 0x2B External Interrupt 2 */
#endif




/**********************************************************************************************************************************************
    General Macros
**********************************************************************************************************************************************/
//General
#ifndef HIGH
#define HIGH			1
#endif // HIGH

#ifndef LOW
#define LOW    	        0
#endif // LOW

#ifndef TRUE
#define TRUE   	        1
#endif // TRUE

#ifndef FALSE
#define FALSE  	        0
#endif // FALSE

#ifndef ON
#define ON     	        1
#endif // ON

#ifndef OFF
#define OFF    	        0
#endif // OFF

#ifndef uchar
//unsigned char
#define uchar              unsigned char
#endif // uchar

#ifndef uint
//unsigned short int
#define uint               unsigned short int
#endif // uint

//Pins
#ifndef PIN_HIGH
#define PIN_HIGH           1
#endif // PIN_HIGH

#ifndef PIN_LOW
#define PIN_LOW            0
#endif // PIN_LOW


//Ports
#ifndef PORT_HIGH
#define PORT_HIGH          0xFF
#endif // PORT_HIGH

#ifndef PORT_LOW
#define PORT_LOW           0x00
#endif // PORT_LOW

#ifndef PORT_INPUT
#define PORT_INPUT			    0xFF
#endif // PORT_INPUT

#ifndef PORT_OUTPUT
#define PORT_OUTPUT		    0x00
#endif // PORT_OUTPUT




//General Use inline Functions
//---------------------------------------------------------------------------------------------------------------------------------------------

#ifndef TOKENIZEX2
//Tokenizes  b to a's ends
#define TOKENIZEX2(a,b)      			  a##b
#endif

#ifndef TOKENIZEX3
//Tokenizes a  b  c
#define TOKENIZEX3(a,b,c)      		      a##b##c
#endif

#ifndef STRINGIZE
//Stringersizes a 
#define STRINGIZE(a)				      #a
#endif

#ifndef STRINGIZEX2
//Stringersized a and b
#define STRINGIZEX2(a,b)				      STRINGIZE(a##b)
#endif

#ifndef STRINGIZEX3
//Stringersized a and b
#define STRINGIZEX3(a,b,c)				      STRINGIZEX2(a##b,c)
#endif

#ifndef STRINGIZEHEX
//Stringersized a and b with b set up as a ascii hex value
#define STRINGIZEHEX(a)	              STRINGIZEX2("\x",a)          		      
#endif

#ifndef ToggleHighLow
//Toggles the pin passed from high to low
#define ToggleHighLow(pin)	pin = 1; pin = 0
#endif // ToggleHighLow

#ifndef ClearLowerBits
//Clears the lower bits of a passed byte
#define ClearLowerBits(byte)	byte &= ~0x0F
#endif // ClearLowerBits

#ifndef ClearUpperBits
//Clears the upper bits of a passed byte
#define ClearUpperBits(byte)	byte &= ~0xF0
#endif // ClearUpperBits

#ifndef ToggleNthBit
//Toggles the bit at the nth position
#define ToggleNthBit(value, n)         value ^= (1 << n)
#endif

#ifndef ClearNthBit
//Clears the bit at the nth position
#define ClearNthBit(value, n)         value &= ~(1 << n)
#endif

#ifndef SetNthBit
//Sets the bit at the nth position
#define SetNthBit(value, n)         value |= (1 << n)
#endif

#ifndef DeclarePortPins
//Sets the passed ports pins to the name passed at the pin position.
//Used for laziness and full port decleration
#define DeclarePortPins(port,name0,name1,name2,name3,name4,name5,name6,name7)  sbit  name0 = port^0; sbit  name1 = port^1; sbit  name2 = port^2; \
sbit  name3 = port^3; sbit  name4 = port^4; sbit  name5 = port^5; sbit  name6 = port^6; sbit name7 = port^7
#endif

#ifndef SetStackPointer
//Sets the stack pointers value
#define SetStackPointer(val)        SP = val
#endif




//Timers------------------------------------------------------


#ifndef SetTimerModes
//Sets the upper and lower bits of TMOD to the passed values
#define SetTimerModes(Timer1Mode, Timer0Mode)           TMOD = 0x##Timer1Mode##Timer0Mode
#endif

#ifndef SetTimer0Bytes
//Sets timer 0's bytes
#define SetTimer0Bytes(highbyte, lowbyte)     TH0 = highbyte; TL0 = lowbyte
#endif // SetTimer0Bytes

#ifndef SetTimer1Bytes
//Sets timer 1's bytes
#define SetTimer1Bytes(highbyte, lowbyte)     TH1 = highbyte; TL1 = lowbyte
#endif // SetTimer1Bytes

#ifndef StartTimer0
//Starts timer 0
#define StartTimer0()                          TR0 = 1
#endif

#ifndef StartTimer1
//Starts timer 1
#define StartTimer1()                          TR1 = 1
#endif


#ifndef StopTimer0
//Stops timer 0
#define StopTimer0()                          TR0 = 0
#endif

#ifndef StopTimer1
//Stops timer 1
#define StopTimer1()                          TR1 = 0
#endif



//Serial---------------------------------------------------------------------

#ifndef EnableSerialReception
//Enables Serial Reception. When Ren = 1, reception is allowed
#define EnableSerialReception()           REN = 1
#endif

#ifndef DisableSerialReception
//Disables Serial Reception. When Ren = 0, reception is not allowed
#define DisableSerialReception()           REN = 0
#endif


#ifndef SerialSend
//Sends the parameter through the serial port.
#define SerialSend(data)        while(TI == 0); TI = 0; SBUF = data
#endif

#ifndef WaitForSerialReceive
//Receives data through the serial port and sets to the parameter.
#define WaitForSerialReceive(data)     while(RI == 0); RI = 0; data = SBUF
#endif

#ifndef SerialMode3
//Sets the serial port to Mode 3.
//Sets transmitter bit 8 to 1, 9th bit to transmit in modes 2 and 3
#define SerialMode3()           SCON = 0xC8
#endif


#ifndef ToAscii
//Converts to ascii char
#define ToAscii(c)  ( (c) & 0x7F )
#endif

#ifndef CharToVolts
//Converts char to voltage value. Takes the supplied voltage ref and the char value
#define CharToVolts(voltRef, value)      (voltRef * value / 255) 
#endif

//2 spaces for 5v, value / 5
//3 spaces for 5v, value * 10 / 5
//4 spaces for 5v, value * 100 / 5
// (value, voltRef)      ((value * 10) / voltRef) 
// AdcValue = AdcValue * 10; 
// AdcValue = AdcValue / 5;


#endif