#ifndef __STRINGFUNCTIONS_C__
/**********************************************************************************************************************************************
 @file   StringFunctions.c
 @brief  Functions For Strings

 @author   Tim Robbins
 @date     08/11/2021


 Controller AT89S52
**********************************************************************************************************************************************/
#define __STRINGFUNCTIONS_C__



#ifndef STRING_FUNC_ALT

#ifndef  EOL
    //End of Line ascii hex code
    #define EOL			0x17
#endif

#ifndef DisplayBufferLength
    #define DisplayBufferLength     50
#endif

extern unsigned char data DisplayBuffer[];

/**********************************************************************************************************************************************
 @name CopySubtring
 @brief Copies from the source string to the global DisplayBuffer string
 @since  v2.8.0.0
 @param  unsigned char* strSource - The source string we're copying from
 @param  unsigned unsigned char data sourceStart - The source strings starting point
 @return  unsigned char i - the index value were the display string ended
**********************************************************************************************************************************************/
unsigned char CopySubString(unsigned char* strSource, unsigned char data sourceStart) {
	unsigned char data i = 0; //index
	unsigned char data y = 0; //subindex

	y = sourceStart;

	while(strSource[y] != EOL && strSource[y] != '\0' && i < DisplayBufferLength) {
		DisplayBuffer[i] = strSource[y];
		y += 1;
		i += 1;
	}
	i += 1; 
	DisplayBuffer[i] = '\x17';
	return i;
}



/**********************************************************************************************************************************************
 @name ClearBuffer
 @brief Clears the global DisplayBuffer string. May possible change to clear string passed
 @since  v2.8.0.0
 @param  void
 @return  void
**********************************************************************************************************************************************/
void ClearBuffer() {
	unsigned char data i = 0; //index
	for(i = 0; i < DisplayBufferLength; i++) {
		DisplayBuffer[i] = 0;
	}
}





#elif STRING_FUNC_ALT == 1

/**********************************************************************************************************************************************
 @name CopySubString
 @brief Copies substring from the source string to the global destination string
 @since  v2.8.0.0
 @param  unsigned char* strSource - The source string we're copying from
 @param  unsigned unsigned char data sourceStart - The source strings starting point
 @return  unsigned char i - the index value were the display string ended
**********************************************************************************************************************************************/
unsigned char CopySubString(unsigned char data strDestination[], unsigned char data uchrDestinationLength, unsigned char strSource[], unsigned char data uchrSourceStart) {
	unsigned char data i = 0; //index
	unsigned char data y = 0; //subindex

    for(i = 0, y = uchrSourceStart; strSource[y] != '\0' && i < uchrDestinationLength; i++, y++) {
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
	for(i = 0; i < uchrDisplayBufferLength; i++) {
		strDisplayBuffer[i] = 0;
	}
}






#endif











#endif