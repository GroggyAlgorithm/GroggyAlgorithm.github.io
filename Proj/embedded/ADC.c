#ifndef __ADC_C__
/**********************************************************************************************************************************************
 @file   ADC.c
 @brief  Program code for Analog to digital conversion functions. Uses Keil library for the INTRINS.H no processing function.

 @author   Tim Robbins
 @date     08/11/2021

 Controller AT89S52
**********************************************************************************************************************************************/
#define __ADC_C__

/**********************************************************************************************************************************************
    Preprocessing
**********************************************************************************************************************************************/


#ifndef __STDATMEL52_H__
#include "stdAtmel52.h" //User Atmel 52 header for macros & Keil MCU sfr && sbit definitions
#endif



#ifndef __INTRINS_H__
#include <INTRINS.H> //Keil intrinsic functions header file
#endif 


#ifndef _ADC_VERSION
//Which version of the ADC functions that are being used
#define _ADC_VERSION	0
#endif








 //Definitions for the adc chip pins. Used definitions to easily change in project files


#ifndef adcDataPort
 //The port the data pins are on
 #define adcDataPort	P1 
#endif


#ifndef adcMSB
//The adcs Most significant bit
 #define adcMSB			P1_7
#endif

#ifndef adcLSB
//The adcs least significant bit
 #define adcLSB  		P1_0
#endif

#ifndef adcA
 //ADC input select pin A
 #define adcA			P1_0
#endif

#ifndef adcB
 //ADC input select pin b
 #define adcB			P1_1

#endif

#ifndef adcC
 //ADC input SelectPin C
 #define adcC			P1_2
#endif

#ifndef adcOE
 //ADC data output enable signal, input terminal, high level effective. 
// When the A/D conversion ends, a high level is input at this 
// terminal to output the digital quantity;
 #define adcOE			P2_1
#endif

#ifndef adcALE
 //ADC Address latch enable pin for input terminal
 //set to the same MCU pin as the start pin
 #define adcALE  		P3_2
#endif

#ifndef adcSTART
 //ADC A/D conversion start pulse, input positive pulse for at least 100ns to start
 //set to the same MCU pin as the ALE pin
 #define adcSTART		P3_2
#endif

#ifndef adcEOC
 //ADC A/D conversion end signal, output terminal, 
// when A/D conversion ends, this terminal outputs a 
// high level (always low level during conversion)
 #define adcEOC 		 P3_3
#endif

#ifndef adcIN0
 //Adc chips code for adc input 0
 #define adcIN0   0xF8
#endif //adcIN0

#ifndef  adcIN1
 //Adc chips code for adc input 1
 #define adcIN1   0xF9
#endif //adcIN1

#ifndef  adcIN2
 //Adc chips code for adc input 2
 #define adcIN2   0xFA
#endif //adcIN2

#ifndef  adcIN3
 //Adc chips code for adc input 3
 #define adcIN3   0xFB
#endif //adcIN3

#ifndef  adcIN4
 //Adc chips code for adc input 4
 #define adcIN4   0xFC
#endif //adcIN4

#ifndef adcIN5
 //Adc chips code for adc input 5
 #define adcIN5   0xFD
#endif //adcIN5

#ifndef  adcIN6
 //Adc chips code for adc input 6
 #define adcIN6   0xFE
#endif //adcIN6

#ifndef adcIN7
 //Adc chips code for adc input 7
 #define adcIN7   0xFF
#endif //adcIN7


#ifndef _ADCSTART
	//Toggles the Ale and start pins to load the address and to start conversion
	#define _ADCSTART()	P3 |= 0x0C; _nop_(); P3 &= ~0x0C
#endif


/**********************************************************************************************************************************************
    Variables
**********************************************************************************************************************************************/







/**********************************************************************************************************************************************
    Functions
**********************************************************************************************************************************************/




/**********************************************************************************************************************************************
 @name  GetAdcChar
 @brief Gets the ADC value and returns as an unsigned char
 @param  unsigned char data uchrAdcAddress - The address for the adc in channel
 @return  unsigned char of the ADC data port
**********************************************************************************************************************************************/
unsigned char GetAdcChar(unsigned char data uchrAdcAddress) {

	//Select ADC in channel
	adcDataPort &= uchrAdcAddress;

	_ADCSTART(); //Toggle the Pins to start conversion and load the IN address

	//Reset the ADC data port to 0xFF
	adcDataPort |= 0xFF;

	//Set the output enable pin
	adcOE = HIGH;

	//Wait for conversion to end.
	while (adcEOC); //While the end of conversion signal is showing that it is converting

	//Let the processor chill for a moment
	_nop_();


	return adcDataPort;

} //End function



/**********************************************************************************************************************************************
 @name  SetVoltDisplay
 @brief Calculates the ADC value and converts to Ascii
 @param  unsigned char data ucharAdcValue - The value of the ADC
 @return  void
**********************************************************************************************************************************************/
void SetVoltDisplay(unsigned char data strBuffer[], unsigned short int uintAdcValue) {

	//Set the first value to the adc's number at the 100s position and convert to ascii
	strBuffer[0] = (0x30 + (uintAdcValue / 100));

	strBuffer[1] = '.'; //Set the buffers decimal

	//Set the second value to the adc's number that the 10s position and convert to ascii
	strBuffer[2] = (0x30 + ((uintAdcValue % 100) / 10));

	//Set the third value to the adc's number at the 1s position and convert to ascii
	strBuffer[3] = (0x30 + (uintAdcValue % 10));



}



#if !_ADC_VERSION





/**********************************************************************************************************************************************
 @name  CalcAdc5V
 @brief Calculates the ADC value and returns as an unsigned char
 @param  unsigned char data uchrAdcValue - The value of the ADC
 @return  unsigned short int of the Calculated ADC value
**********************************************************************************************************************************************/
unsigned short int CalcAdc5V(unsigned char data uchrAdcValue) {

	//Variables
	unsigned short int data uintTempAdc = 0; //The temporary value of the adc as an unsigned short int

	//Convert to unsigned short int
	uintTempAdc = (short int) uchrAdcValue;

	//Multiply to give the appropriate amount of values/numbers for the display
	uintTempAdc = uintTempAdc * 100; 

	//divide by the Adcs max value
	uintTempAdc = uintTempAdc / 255; 

	//ADC takes 5v, multiply by 5
	uintTempAdc = uintTempAdc * 5; 
	
	//return adc value
	return uintTempAdc;

}






#elif _ADC_VERSION == 1



/**********************************************************************************************************************************************
 @name  CalcAdc5V
 @brief Calculates the ADC value and returns as an unsigned char
 @param  unsigned char data uchrAdcValue - The value of the ADC
 @return  unsigned short int of the Calculated ADC value
**********************************************************************************************************************************************/
unsigned short int CalcAdc5V(unsigned char data uchrAdcValue) {

	//Variables
	unsigned short int data uintTempAdc = 0; //The temporary value of the adc as an unsigned short int

	//Convert to unsigned short int
	uintTempAdc = (short int) uchrAdcValue;

	//Multiply to give the appropriate amount of values/numbers for the display
	uintTempAdc = uintTempAdc * 100; 

	//divide by the Adcs max value ADC and multiple by the voltage. takes 5v, multiply by 5
	//Getting 3 positions, 255 / 5   = 50   = value / 50
	uintTempAdc = uintTempAdc / 50; 
	
	//return adc value
	return uintTempAdc;

}






#elif _ADC_VERSION == 2



/**********************************************************************************************************************************************
 @name  CalcAdc5V
 @brief Calculates the ADC value and returns as an unsigned char
 @param  unsigned char data uchrAdcValue - The value of the ADC
 @return  unsigned short int of the Calculated ADC value
**********************************************************************************************************************************************/
unsigned short int CalcAdc5V(unsigned char data uchrAdcValue) {

	//Variables
	unsigned short int data uintTempAdc = 0; //The temporary value of the adc as an unsigned short int

	//Convert to unsigned short int
	uintTempAdc = (short int) uchrAdcValue;

	//Multiply to give the appropriate amount of values/numbers for the display
	uintTempAdc = uintTempAdc * 10; 

	uintTempAdc = uintTempAdc / 5; 
	
	//return adc value
	return uintTempAdc;

}







#endif




#endif
