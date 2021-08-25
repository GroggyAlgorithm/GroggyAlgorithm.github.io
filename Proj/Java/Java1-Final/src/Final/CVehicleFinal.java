/**
 * 
 */
package Final;

/**
 * @author Tim Robbins
 * Abstract- Java 1: Final
 */


/**
 * Class: CVehicleFinal
 * Abstract: Parent class of all vehicles
 */
public class CVehicleFinal {
	//Method variables
	int m_intWheels; //The amount of wheels
	int m_intMilePerGallon; //The miles per gallon
	String m_strHowToDrive; //How the vehicle is driven
	float m_sngPrice; //Price
	
	

	/**
	 * Name- SetWheels
	 * Abstract- Sets the amount of wheels
	 * @param intAmountOfWheels- The amount of wheels on the vehicle
	 */
	public void SetWheels(int intAmountOfWheels) {
		//Check wheels
		if(intAmountOfWheels < 2 || intAmountOfWheels > 18) {
			intAmountOfWheels = 4;
		}
		
		//Set the wheels
		m_intWheels = intAmountOfWheels;
	}
	

	/**
	 * Name- GetWheels
	 * Abstract- Gets the amount of wheels
	 * @return m_intWheels- The amount of wheels
	 */
	public int GetWheels() {
		return m_intWheels;
	}
	
	
	
	/**
	 * Name- SetMilesPerGallon
	 * Abstract- Sets the mile per gallon
	 * @param intMilesPerGallon- the Miles per gallon
	 */
	public void SetMilesPerGallon(int intMilesPerGallon) {
		//Check 
		if(intMilesPerGallon < 0 || intMilesPerGallon > 100) {
			intMilesPerGallon = 0;
		}
		//Set the mpg
		m_intMilePerGallon = intMilesPerGallon;
	}
	
	
	
	/**
	 * Name- GetMilesPerGallon
	 * Abstract- Gets the mile per gallon
	 * @return m_MilePerGallon- The miles per gallon
	 */
	public int GetMilesPerGallon() {
		return m_intMilePerGallon;
	}
	
	
	
	/**
	 * Name- SetHowToDrive
	 * Abstract- Sets the method of operation of the motorcar
	 * @param strHowToDrive: How the vehicle is driven
	 */
	public void SetHowToDrive(String strHowToDrive) {
		int intLength = 0;
		intLength = strHowToDrive.length();
		
		// Too Long?
		if (intLength > 50) {
			// if longer, clip to 5o characters
			intLength = 50;
		}
		m_strHowToDrive = strHowToDrive.substring(0, intLength);
	}
	
	
	/**
	 * Name- GetHowToDrive
	 * Abstract- Gets How to drive
	 * @return m_strHowToDrive- The way you drive the dang thing
	 */
	public String GetHowToDrive() {
		return m_strHowToDrive;
	}
	
	
	/**
	 * Name- SetPrice
	 * Abstract- Sets the price
	 * @param sngPrice: The price of the vehicle
	 * @param intDaysRented: The amount of days being rented
	 */
	public void SetPrice(float sngPrice, int intDaysRented) {		
		m_sngPrice = sngPrice * intDaysRented;
	}
	
	
	
	/**
	 * Name- GetPrice
	 * Abstract Gets the price
	 * @return m_sngPrice: The price of the vehicle
	 */
	public float GetPrice() {
		return m_sngPrice;
	}
	
	
	/**
	* Name: Initialize
	* Abstract: Initializes Vehicle object
	* @param intWheels- vehicles wheels
	* @param strHowToDrive- vehicles how drive vroom
	* @param intMilesPerGallon- vehicles mpg
	* @param sngPrice- vehicles price
	* @param intDaysRented- The amount of days rented
	*/
	public void Initialize( int intWheels, String strHowToDrive,int intMilesPerGallon ,float sngPrice,int intDaysRented ) {
		SetWheels(intWheels);
		SetHowToDrive(strHowToDrive);
		SetMilesPerGallon(intMilesPerGallon);
		SetPrice(sngPrice, intDaysRented);
	}
	
	
	
	
	
	

}
