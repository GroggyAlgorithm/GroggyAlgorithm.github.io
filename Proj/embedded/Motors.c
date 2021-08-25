#ifndef __MOTORS_C__
/**********************************************************************************************************************************************
 @file   Motors.c
 @brief  Program code for Motor functions

 @author   Tim Robbins
 @date     08/11/2021

 Controller AT89S52
**********************************************************************************************************************************************/
#define __MOTORS_C__

/**********************************************************************************************************************************************
    Preprocessing
**********************************************************************************************************************************************/


#ifndef __STDATMEL52_H__
#include "stdAtmel52.h" //User Atmel 52 header for macros & MCU sfr && sbit definitions
#endif



/**********************************************************************************************************************************************
    Variables
**********************************************************************************************************************************************/
//bit isMotorOn = FALSE; //bit boolean for is the motor is on

/**********************************************************************************************************************************************
    UDT's * User defined types
**********************************************************************************************************************************************/


/**********************************************************************************************************************************************
    Functions
**********************************************************************************************************************************************/



/**********************************************************************************************************************************************
 @name DacMotorControl
 @brief Controls values for motors connected to a DAC
 @param  bit isMotorOn - bit boolean for if the Motor is on
 @param  unsigned char data *DacValue - The value from the DAC
 @return  bit blnNewMotorState - The new state of the motor
**********************************************************************************************************************************************/
bit DacMotorControl(bit isMotorOn, unsigned char data *DacValue) {
    //Variables
    unsigned char data uchrNewMotorValue = 0; // The new value for the motor
    bit blnNewMotorState = 0; //The new state for the motor
    
    blnNewMotorState = isMotorOn;
    uchrNewMotorValue = *DacValue;

    //If the motors new state has a value...
    if(blnNewMotorState) {
        //if we're already at the maximum value for the motor...
        if(uchrNewMotorValue >= 225) {
            blnNewMotorState = 0; //Set the motors state to off
            uchrNewMotorValue = 0; //Set the new value to 0
        }
        //else...
        else {
            //Add the minimum voltage needed to move the motor
             //25 was used because it produced the minimum voltage for the motor to start moving
            uchrNewMotorValue += 25;
        }

    }
    //Else, if the motors new state has no value...
    else {
        blnNewMotorState = 1; //Set the motors state to on
        uchrNewMotorValue = 25; //Set the new motors value to its minimum value required to begin moving.
    }


    //Set the passed Dac value to the new motor value
    *DacValue = uchrNewMotorValue;

    //return the motors new state
    return blnNewMotorState;

}



/**********************************************************************************************************************************************
 @name SetDacValue
 @brief Sets the DAC value
 @param  void
 @return  void
**********************************************************************************************************************************************/



/**********************************************************************************************************************************************
 @name PwmMotorControl
 @brief Controls values for motors using PWM
 @param  bit isMotorOn - bit boolean for if the Motor is on
 @param  unsigned char data *PwmValue - The value from the PWM
 @return  bit blnNewMotorState - The new state of the motor
**********************************************************************************************************************************************/
bit PwmMotorControl(bit isMotorOn, unsigned char data *PwmValue) {
    //Variables
    unsigned char data uchrNewMotorValue = 0; // The new value for the motor
    bit blnNewMotorState = 0; //The new state for the motor
    
    blnNewMotorState = isMotorOn;
    uchrNewMotorValue = *PwmValue;

    //If the motors state is on...
    if(blnNewMotorState) {

        //if we're already at the maximum value for the motor...
        if(uchrNewMotorValue >= 100) {
            blnNewMotorState = 0; //Set the motors state to off
            uchrNewMotorValue = 0; //turn it off
        }
        //else...
        else {
            //Add to the motors value
            uchrNewMotorValue += 10;
        }

    }
    //Else, if it's off
    else {
        blnNewMotorState = 1; //Set the motors state to on
        uchrNewMotorValue = 20; //Set to the minimum value needed to move the motor
    }


    //Set the passed pwm value to the motors new value
    *PwmValue = uchrNewMotorValue;

    //return the motors new state
    return blnNewMotorState;

}



/**********************************************************************************************************************************************
 @name 
 @brief 
 @param  void
 @return  void
**********************************************************************************************************************************************/


















#endif