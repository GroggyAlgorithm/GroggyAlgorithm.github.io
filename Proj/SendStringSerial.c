#ifndef __SENDSTRINGSERIAL_C__
/**********************************************************************************************************************************************
 @file   SendStringSerial.c
 @brief  Function code for Sending a string through the serial port

 @author   Tim Robbins
 @date     08/11/2021


 Controller AT89S52
**********************************************************************************************************************************************/
#define __SENDSTRINGSERIAL_C__


/**********************************************************************************************************************************************
    Preprocessing
**********************************************************************************************************************************************/
#ifndef __STDATMEL52_H__
    #include "stdAtmel52.h" //User Atmel 52 header for macros & MCU sfr && sbit definitions
#endif

#ifndef __STDATMEL52LCD_H__
    #include "stdAtmel52lcd.h" //User Atmel 52 header for macros & MCU sfr && sbit definitions
#endif



/**********************************************************************************************************************************************
    Functions
**********************************************************************************************************************************************/

#if _IS_DISPLAY_MODULE

/**********************************************************************************************************************************************
 @name  SendStringSerial
 @brief  Sends strings through serial port
 @param  unsigned char strMsg[]
 @return  void
**********************************************************************************************************************************************/
void SendStringSerial(unsigned char strMsg[]) {
	//Variables
	unsigned char data i = 0; //Index

	//Loop through the length of the message, starting at the externally set string start
	for(i = 0; strMsg[i] != '\0'; i++) {

		while (TI == 0);
		TI = 0;
		SBUF = strMsg[i];
		
		//If the value was a next line value...
		if(strMsg[i] == '\n') {
			while (TI == 0);
			TI = 0;
			SBUF = '\r'; //Send the line start hex code
		}

	}

}

#else 


/**********************************************************************************************************************************************
 @name  SendStringSerial
 @brief  Sends strings through serial port
 @param  unsigned char strMsg[]
 @return  void
**********************************************************************************************************************************************/
void SendStringSerial(unsigned char strMsg[]) {
	//Variables
	unsigned char data i = 0; //Index


	//Loop through the length of the message, starting at the externally set string start
	for(i = 0; strMsg[i] != '\0'; i++) {

		while (TI == 0);
		TI = 0;
		SBUF = strMsg[i];

	}

}




#endif






#endif