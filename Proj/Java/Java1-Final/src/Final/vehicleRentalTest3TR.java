/**
 * 
 */
package Final;
import java.io.*;

/**
 * @author Tim Robbins
 * Abstact- Java 1: Final
 */



/** 
 * method name: vehicleRentalTest3TR
 * Abstract: Controls program
 * 
 */
public class vehicleRentalTest3TR {
	
	//Variables
	static DatabaseConnections m_Connections = new DatabaseConnections();//The database connections method
	

	/**
	 * main- main method for controlling program
	 * @param args- Method args
	 */
	public static void main(String[] args) {
		//Variables
		boolean blnListLoaded = false; //Boolean to hold return from loading the list

		
		
		//Call methods and functions
		if(m_Connections.Connect() == true) {
			Customer current_Customer = new Customer(); //The customer
			
			current_Customer.GetRentalDays();
			CreateCutomer(current_Customer);
			blnListLoaded = m_Connections.LoadLocationsList( "TLocations", "intLocationID" , "strLocationName", "strAddress", "strCity", "strState", "strZip" );
			
			m_Connections.CloseConnection();
			
		}

	}
	
	
	/**
	 * Name: CreateCutomer
	 * Abstract: Creates customer information
	 * @param current_Customer- The current customer
	 */
	public static void CreateCutomer(Customer current_Customer) {
		//Variables
		float sngTotalPrice = 0; //Total price when there's more than 1 car
		int intIndex = 0; //The index for loop
		
		current_Customer.SetName();
		String strName = current_Customer.GetName(); //Customer name
		current_Customer.SetPhoneNumber();
		String strPhoneNumber = current_Customer.GetPhoneNumber(); //Customer phonenumber
		current_Customer.SetEmail();
		String strEmail = current_Customer.GetEmail(); //Customer email
		current_Customer.SetVehicleCount();
		int intAmountOfVehicles = current_Customer.GetVehicleCount(); //Amount of vehicles Customer is renting
		
		
		for(intIndex = 0; intIndex < intAmountOfVehicles; intIndex += 1) {
			System.out.println("\n\nAdd Information for Vehicle #"+(intIndex+1));
			current_Customer.SetRentalDays();
			int intNumberOfRentalDays = current_Customer.GetRentalDays(); //Amount of days that will be rented
			current_Customer.SetVehicleType();
			String strVehicleType = current_Customer.GetVehicleType(); //Customer vehicle type
			current_Customer.SetVehicalInformation(strVehicleType,intNumberOfRentalDays);
			sngTotalPrice += current_Customer.m_sngPrice; //Add to the total price
			
			
			System.out.println("\n\nVehicle #"+(intIndex+1));
			System.out.println("_____________________________________________________________");
			System.out.println("Days Renting:					"+intNumberOfRentalDays);
			System.out.println("Vehicle Type:					"+strVehicleType);
			System.out.println("Wheels:						"+current_Customer.m_intWheels);
			System.out.println("Driven with:					"+current_Customer.m_strHowToDrive);
			System.out.println("Miles Per Gallon:				"+current_Customer.m_intMilePerGallon);
			System.out.printf("The Price for %d days:				$%.2f\n",intNumberOfRentalDays,current_Customer.m_sngPrice);
			System.out.println("_____________________________________________________________");
		}
		
		System.out.println("_____________________________________________________________");
		System.out.println("Name:					        "+strName);
		System.out.println("Phone Number:					"+strPhoneNumber);
		System.out.println("Email:					        "+strEmail);
		System.out.println("Amount Of Vehicles:				"+intAmountOfVehicles);
		System.out.printf("Estimated Total:				$%.2f\n\n",sngTotalPrice);
		
		
		
		
		
	
	
	}
	
	
	
	
	/**
	 * Name - ReadIntegerFromUser
	 * Abstract - Gets integer input from user
	 * @return intValue- Returns int value from buffered reader
	 */
	public static int ReadIntegerFromUser() {

		int intValue = -1;

		try {
			String strBuffer = "";

			// Input stream
			BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));

			// Read a line from the user
			strBuffer = burInput.readLine();

			// Convert from string to integer
			intValue = Integer.parseInt(strBuffer);
		} catch (Exception excError) {
			System.out.println(excError.toString());
		}

		// Return integer value
		return intValue;
	}
	
	

}
