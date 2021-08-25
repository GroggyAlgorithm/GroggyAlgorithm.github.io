/**
 * 
 */
package Final;

/**
 * @author Tim Robbins
 * Abstract- Java 1: Final
 */


/**
 * Class: CMotorcycle
 * Abstract: Class of Motorcycle, child of the vehicle
 */
public class CMotorcycle extends CVehicleFinal {


	
	/**
	 * Name- CMotorcycle
	 * Abstract- The Motorcycle Constructor
	 * @param intDaysRented- Takes the days rented
	 */
	public CMotorcycle(int intDaysRented) {
		Initialize(intDaysRented);
	}
	
	
	
	/**
	* Name: Initialize
	* Abstract: Initializes Vehicle object
	* @param intDaysRented- The amount of days rented
	*/
	public void Initialize(int intDaysRented ) {
		SetWheels(2);
		SetHowToDrive("Handlebars");
		SetMilesPerGallon(88);
		SetPrice(40, intDaysRented);
	}
	

		
}
