#ifndef __SERIALGETCHAR_C__
/**********************************************************************************************************************************************
 @file   SerialGetChar.c
 @brief  Function code for Getting a Char through the serial port

 @author   Tim Robbins
 @date     08/11/2021


 Controller AT89S52
**********************************************************************************************************************************************/
#define __SERIALGETCHAR_C__



#ifndef __STDATMEL52_H__
#include "stdAtmel52.h" //User Atmel 52 header for macros & MCU sfr && sbit definitions
#endif


/**********************************************************************************************************************************************
 @name  SerialGetChar
 @brief Gets Char value from the serial SBUF
 @since  v2.8.0.0
 @param  bit blnWaitForKeyPress - If the program will wait while there is no key pressed
 @return  unsigned char data uchrKeypress - The key Received from the Serial port
**********************************************************************************************************************************************/
unsigned char SerialGetChar(bit blnWaitForKeyPress) {
	//Variables
    unsigned char data uchrKeypress = 0; //The key Received from the Serial port

    //if we're waiting until a key is pressed...
    if (blnWaitForKeyPress) {
        while (RI == 0); //loop until a key is pressed
    }
    
    //If a key was pressed...
    if(RI == 1) {
        RI = 0; //Clear the RI flag
        uchrKeypress = SBUF; //Set the keypressed to the serial buf
        uchrKeypress &= 0x7F; //Clear the last bit
    }

    //Return the keypressed
    return uchrKeypress;

}


#endif