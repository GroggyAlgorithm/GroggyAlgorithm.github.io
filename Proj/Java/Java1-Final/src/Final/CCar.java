/**
 * 
 */
package Final;

/**
 * @author Tim Robbins
 * Abstract- Java 1: Final
 */


/**
 * Class: CCar
 * Abstract: Parent class of Car, child of the vehicle
 */
public class CCar extends CVehicleFinal {
	

	
	
		
	/**
	 * Name- CCar
	 * Abstract- The Default Car Constructor
	 * @param intDaysRented- Takes in the amount of days rented
	 */
	public CCar(int intDaysRented) {
		Initialize("Steering Wheel",intDaysRented);
	}
	
	
	
	
	/**
	 * Name- CCar
	 * Abstract- The Parameterized Car Constructor for children
	 * @param intWheels- Takes in the amount of wheels
	 * @param intMilesPerGallon- Takes in the MPG
	 * @param sngPrice- Takes in the price
	 * @param intDaysRented- Takes in the amount of days rented
	 */
	public CCar(int intWheels,int intMilesPerGallon,float sngPrice,int intDaysRented) {
		Initialize(intWheels,"Steering Wheel",intMilesPerGallon ,sngPrice,intDaysRented);
	}
	
	
	
	/**
	* Name: Initialize
	* Abstract: Initializes Vehicle object
	* @param strHowToDrive- The way you drive the car
	* @param intDaysRented- The amount of days rented
	*/
	public void Initialize(String strHowToDrive,int intDaysRented ) {
		SetWheels(4);
		SetHowToDrive(strHowToDrive);
		SetMilesPerGallon(32);
		SetPrice(60, intDaysRented);
	}
	
		

}
