#ifndef __STRINGFUNCTIONS_C__
/**********************************************************************************************************************************************
 @file   StringFunctions.c
 @brief  Functions For Strings

 @author   Tim Robbins
 @date     08/11/2021


 Controller AT89S52
**********************************************************************************************************************************************/
#define __STRINGFUNCTIONS_C__






/**********************************************************************************************************************************************
 @name	IsAsciiCommand
 @brief Checks if the char passed is an ascii command
 @param  unsigned char uchrValue - the char value to check
 @return  bit blnIsCommand - if it's a command value
**********************************************************************************************************************************************/
bit IsAsciiCommand(unsigned char uchrValue) {
	//Variables
	bit blnIsCommand = 0;

	//If it's less than the SPACE ascii value, set to true
	if (uchrValue < 0x20) {
		blnIsCommand = 1;
	}

	return blnIsCommand;
}



/**********************************************************************************************************************************************
 @name CopySubString
 @brief Copies substring from the source string to the global destination string
 @since  v2.8.0.0
 @param  unsigned char* strSource - The source string we're copying from
 @param  unsigned unsigned char data sourceStart - The source strings starting point
 @return  unsigned char i - the index value were the display string ended
**********************************************************************************************************************************************/
unsigned char CopySubString(unsigned char data strDestination[], unsigned char data uchrDestinationLength, unsigned char strSource[], unsigned char uchrSourceStart) {
	unsigned char data i = 0; //index
	unsigned char data y = 0; //subindex

	for (i = 0, y = uchrSourceStart; strSource[y] != '\0' && i < uchrDestinationLength; i++, y++) {
		strDestination[i] = strSource[y];
	}

	return i;
}



/**********************************************************************************************************************************************
 @name ClearBuffer
 @brief Clears the  DisplayBuffer string. May possible change to clear string passed
 @since  v2.8.0.0
 @param  void
 @return  void
**********************************************************************************************************************************************/
void ClearBuffer(unsigned char data strDisplayBuffer[], unsigned char data uchrDisplayBufferLength) {
	unsigned char data i = 0; //index
	for (i = 0; i < uchrDisplayBufferLength; i++) {
		strDisplayBuffer[i] = 0;
	}
}











#endif