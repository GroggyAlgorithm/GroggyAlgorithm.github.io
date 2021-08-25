#ifndef __TIMER0_C__
/**********************************************************************************************************************************************
 @file   Timer0.c
 @brief  Program code for Timer 0 Wait timer and overflow interrupt

 @author   Tim Robbins
 @date     08/11/2021


 Controller AT89S52
**********************************************************************************************************************************************/
#define __TIMER0_C__

/**********************************************************************************************************************************************
    Preprocessing
**********************************************************************************************************************************************/


#ifndef __STDATMEL52_H__
#include "stdAtmel52.h" //User Atmel 52 header for macros & MCU sfr && sbit definitions
#endif




/**********************************************************************************************************************************************
    Variables
**********************************************************************************************************************************************/
unsigned char data T0WaitCount = 0; //Wait count for timer 0
bit T0IsTimeUp = 0; //Bit boolean for if time is up for timer 0


/**********************************************************************************************************************************************
    UDT's * User defined types
**********************************************************************************************************************************************/
//Typedef Union Short_ByteSplits: Used to splits shorts value into Shared Memories Byte Value.
//Values: name.Total, name.Byte[0], name.Byte[1]
 typedef union Short_ByteSplits{
	 unsigned short int Total; //The total value of values. Unsigned short for total of 16 bits
	 unsigned char Byte[2]; //The value split into its high[0] and low[1] bytes

 }Short_ByteSplit;

 //Short int union, Splits into high and low bytes for the Timer 0's reload values
 Short_ByteSplit Timer0_Reload = {0}; 




/**********************************************************************************************************************************************
    Functions
**********************************************************************************************************************************************/


/**********************************************************************************************************************************************
 @name  Timer0_Wait10th
 @brief  Wait for args 10ths of a second using timer 0
 @param  unsigned char x10thSeconds - number of a 10th of a second
 @return  void
**********************************************************************************************************************************************/
void Timer0_Wait10th(unsigned char x10thSeconds) {
	
	//Set the global wait count to the passed value
	T0WaitCount = x10thSeconds;
	
	//Set the timer 0's high and low bytes
	TH0 = Timer0_Reload.Byte[0]; // High Byte
	TL0 = Timer0_Reload.Byte[1]; //Low Byte

	//Set the times up boolean
	T0IsTimeUp = FALSE; //Set to false
	
	//Start timer 0
	TR0 = 1;
	
	//Loop While T0IsTimeUp is false. Once the Timer overflows, 
	//it goes to the interrupt function for timer 0 which sets to true and exits the loop
	//when appropriate 
	while(T0IsTimeUp == FALSE);

}



/**********************************************************************************************************************************************
 @name  TIMER0_INT
 @brief  Interrupt function for Interrupt 1 (ET0 Pin, TF0 flag interrupt), Timer 0
 @param  void
 @return  void
**********************************************************************************************************************************************/
static void TIMER0_INT() interrupt TF0_VECTOR {

    //Reload the timers bytes
	TH0 = Timer0_Reload.Byte[0]; //High Byte
	TL0 = Timer0_Reload.Byte[1]; //Low Byte


	//If predecrementing the wait count is equal to 0....
	if(--T0WaitCount == 0) {
		T0IsTimeUp = TRUE; //Set the time boolean to true
		TR0 = 0; //Turn the timer off
	}

	//Get outta the interrupt!
	return;
}










#endif