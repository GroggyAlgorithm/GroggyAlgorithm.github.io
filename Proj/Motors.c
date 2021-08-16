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
 @return  bit MotorNewState - The new state of the motor
**********************************************************************************************************************************************/
bit DacMotorControl(bit isMotorOn, unsigned char data *DacValue) {
    //Variables
    unsigned char data newMotorValue = 0; // The new value for the motor
    bit MotorNewState = OFF;
    
    MotorNewState = isMotorOn;
    newMotorValue = *DacValue;

    if(MotorNewState) {

        //if we're already at the maximum value for the motor...
        if(newMotorValue >= 225) {
            MotorNewState = OFF; //Set the motors state to off
            newMotorValue = 0; //turn it off
        }
        //else...
        else {
            //Add the minimum voltage needed to move the motor
             //25 was used because it produced the minimum voltage for the motor to start moving
            newMotorValue += 25;
        }

    }
    else {
        MotorNewState = ON; //Set the motors state to off
        newMotorValue = 25;
    }



    *DacValue = newMotorValue;

    return MotorNewState;

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
 @return  bit MotorNewState - The new state of the motor
**********************************************************************************************************************************************/
bit PwmMotorControl(bit isMotorOn, unsigned char data *PwmValue) {
    //Variables
    unsigned char data newMotorValue = 0; // The new value for the motor
    bit MotorNewState = OFF;
    
    MotorNewState = isMotorOn;
    newMotorValue = *PwmValue;

    if(MotorNewState) {

        //if we're already at the maximum value for the motor...
        if(newMotorValue >= 100) {
            MotorNewState = OFF; //Set the motors state to off
            newMotorValue = 0; //turn it off
        }
        //else...
        else {
            //Add to the motors value
            newMotorValue += 10;
        }

    }
    else {
        MotorNewState = ON; //Set the motors state to off
        newMotorValue = 20; //Set to the minimum value needed to move the motor
    }



    *PwmValue = newMotorValue;

    return MotorNewState;

}



/**********************************************************************************************************************************************
 @name 
 @brief 
 @param  void
 @return  void
**********************************************************************************************************************************************/


















#endif