/**
 * 
 */
package Final;
import java.io.*;


/**
 * @author Tim Robbins
 * Abstract- Java 1: Final
 */



/**
 * method name: Customer
 * Abstract- Gathers and sets user input
 */
public class Customer {
	//Variables
	String m_strName; //Users name
	String m_strPhoneNumber; //Customer phone number
	String m_strEmail; //The Customer email
	float m_sngPrice; //Price of the vehicle
	int m_intWheels; //The amount of wheels
	String m_strHowToDrive; //How the thing is driven
	int m_intMilePerGallon = 0; //The miles per gallon
	int m_intAmountOfVehicles; //Amount of vehicles Customer is renting_
	int m_intNumberOfRentalDays; //Amount of days that will be rented
	String m_strVehicleType; //Customer vehicle type


	
	
	
//	/**
//	 * Name- Customer
//	 * Abstract- constructor
//	 */
//	public Customer() {
//		SetName();
//		SetPhoneNumber();
//		SetEmail();
//		SetVehicleCount();
//		SetRentalDays();
//		SetVehicleType();
//		SetVehicalInformation(m_strVehicleType,m_intNumberOfRentalDays);
//	}

	
	
	/**
	 * Name- SetName
	 * Abstract- Sets the name of the Customer
	 */
	public void SetName() {
		//Variables
		int intLength = 0; //Length of the input
		String strNewName; //Name of the user
		boolean blnCorrectInput = true; //Is true when user provides correct input
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			System.out.print("\nPlease enter your name: ");
			//Read the input from the user
			strNewName = ReadStringFromUser();
			//Get the length of the input
			intLength = strNewName.length();
			
			
			// Check if the string is too long before loop 
			if (intLength > 75) {
				blnCorrectInput = false;
			}
			else {
			
	
			//Check and make sure the string is not empty. If not....
			if(strNewName.isEmpty() == true) {
				blnCorrectInput = false;
			}
			else {
				
				//Loop through and make sure the name is letters
				for(char chr : strNewName.toCharArray()) {
					if (Character.isAlphabetic(chr) == false && Character.isWhitespace(chr) == false) {
						blnCorrectInput = false;
						break;
					}
				}
				
				
			}
		}
		
			if(blnCorrectInput == false) {
				System.out.println("Incorrect Input");
			}
			
		
		//While the input is incorrect
		} while(blnCorrectInput == false);
		
		//Set global variable to the name
		m_strName = strNewName.substring(0, intLength);
	}
	
	
	

	/**
	 * Name- GetName
	 * Abstract- Gets the name of the Customer
	 * @return m_strName- The name of the Customer
	 */
	public String GetName() {
		return m_strName;
	}
	
	
	/**
	 * Name- SetPhoneNumber
	 * Abstract- Sets the phone number of the Customer
	 */
	public void SetPhoneNumber() {
		//Variables
		String strPhoneNumber; //phone number of the Customer for multiple formats
		boolean blnCorrectInput = true; //Is true when user provides correct input
		int intLength = 0; //Length of the input
		
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			System.out.print("\nPlease enter your Phone number: ");
			//Read the input from the Customer
			strPhoneNumber = ReadStringFromUser();
			//Get the length of the input
			intLength = strPhoneNumber.length();
			//Check input
			blnCorrectInput = isValidPhoneNumber(strPhoneNumber, intLength);
		
		
		//While the input is incorrect
		} while(blnCorrectInput == false);
		
		//Set global variable
		m_strPhoneNumber = strPhoneNumber.substring(0, intLength);
	}
	
	/**
	 * Name- GetPhoneNumber
	 * Abstract- Gets the Phone number of the Customer
	 * @return m_strPhoneNumber- The phone number of the Customer
	 */
	public String GetPhoneNumber() {
		return m_strPhoneNumber;
	}
	
	
	
	/**
	 * Name- IsValidPhoneNumber
	 * Abstract- Checks format of the phone number
	 * @param strPhoneNumber- Takes the customers phone number
	 * @param intLength- Takes the length of the entry
	 * @return blnCorrectInput- Returns if the phone number is in a valid format
	 */
	public static boolean isValidPhoneNumber(String strPhoneNumber, int intLength) {
		//Variables
		int intSpaceCount = 0; //Counts the space
		int intDashCount = 0; //Counts the dashes
		boolean blnCorrectInput = true; //Is true when Customer provides correct input
		int intIndex = 0; //Index for loop
		
		
		//Make sure it's not too long to avoid looping
		if(intLength > 12) {
			blnCorrectInput = false;
			System.out.println("Incorrect Input");
			return blnCorrectInput;
			
		}
		

		//Check and make sure the string is not empty. If not....
		if(strPhoneNumber.isEmpty() == true) {
			blnCorrectInput = false;
			System.out.println("Incorrect Input");
			return blnCorrectInput;
		}
		else {
			
			//Loop through and make sure the name is letters
			for(char chr : strPhoneNumber.toCharArray()) {
				intIndex += 1;
				
				if (Character.isAlphabetic(chr) == true) {
					blnCorrectInput = false;
					System.out.println("Incorrect Input");
					return blnCorrectInput;
					
				}
				
				else if (Character.valueOf(chr) == '-') {
					intDashCount += 1;
					if(intIndex != 4 && intIndex != 8) {
						blnCorrectInput = false;
						System.out.println("Incorrect Input");
						return blnCorrectInput;
					}
				}
				else if (Character.isWhitespace(chr) == true) {
					intSpaceCount += 1;
					if(intIndex != 4 && intIndex != 8) {
						blnCorrectInput = false;
						System.out.println("Incorrect Input");
						return blnCorrectInput;
					}
				}
				
				
			}
			
			// If the string is too long or too short and doesnt contain correct format
			if (intLength == 12 && intSpaceCount == 2) {
				//Set to true
				blnCorrectInput = true;
			}
			else if(intLength == 12 && intDashCount == 2 ){
				//Set to true
				blnCorrectInput = true;
			}
			else if (intLength == 10) {
				blnCorrectInput = true;
			}
			else {
				blnCorrectInput = false;
				System.out.println("Incorrect Input");
				return blnCorrectInput;
			}
		}
		
		
		return blnCorrectInput; 
	}
	
	
	
	
	/**
	 * Name- SetEmail
	 * Abstract- Sets the email of the user
	 */
	public void SetEmail() {
		//Variables
		String strEmail; //email of the Customer for multiple formats
		boolean blnCorrectInput = true; //Is true when user provides correct input
		int intLength = 0; //Length of the input
		
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			System.out.print("\nPlease enter your Email: ");
			//Read the input from the Customer
			strEmail = ReadStringFromUser();
			//Get the length of the Customer
			intLength = strEmail.length();
			//Check input
			blnCorrectInput = isValidEmail(strEmail, intLength);
		
		
		//While the input is incorrect
		} while(blnCorrectInput == false);
		
		//Set global variable
		m_strEmail = strEmail.substring(0, intLength);
	}
	
	/**
	 * Name- GetEmail
	 * Abstract- Gets the email of the Customer
	 * @return m_strEmail- The Email of the Customer
	 */
	public String GetEmail() {
		return m_strEmail;
	}
	
	
	
	/**
	 * Name- isValidEmail
	 * Abstract- checks format of the email
	 * @param strEmail- Takes the entered customer email
	 * @param intLength- Takes the length of the customers entry
	 * @return blnCorrectInput- Returns if the customers entry was valid
	 */
	public static boolean isValidEmail(String strEmail, int intLength) {
		//Variables
		boolean blnCorrectInput = true; //Is true when Customer provides correct input
		int intIndex = 0; //Index for loop
		int intAtCount = 0; //Counts the @ signs
		int intExtensionCount = 0; //Counts numbers for the enxtension
		int intPeriodCount = 0; //Checks amount of periods in the extension so we dont count .com

		//Check and make sure the string is not empty. If not....
		if(strEmail.isEmpty() == true) {
			System.out.println("Incorrect Input");
			return blnCorrectInput;
		}
		else {
			
			//Make sure it's not too long to avoid looping until it breaks
			if(intLength > 50) {
				blnCorrectInput = false;
				System.out.println("Incorrect Input, too many characters.");
				return blnCorrectInput;
				
			}
			
			
			
			//Loop through and make sure the email is correct format
			for(char chr : strEmail.toCharArray()) {
				intIndex += 1;
				
				//Make sure the first letter is numeric
				if (Character.isAlphabetic(chr) == false && intPeriodCount >= 1) {
					blnCorrectInput = false;
					System.out.println("Incorrect Input");
					return blnCorrectInput;
					
				}
				//Make sure the first letter is numeric
				else if (Character.isAlphabetic(chr) == false && intIndex == 1) {
					blnCorrectInput = false;
					System.out.println("Incorrect Input");
					return blnCorrectInput;
					
				}
				//Start counting at signs if we reach one
				else if (Character.valueOf(chr) == '@') {
					intAtCount += 1;
					if(intAtCount > 1) {
						blnCorrectInput = false;
						System.out.println("Incorrect Input");
						return blnCorrectInput;
					}
				}
				//Start counting periods if we reach one after the @ sign
				else if (Character.valueOf(chr) == '.' && intAtCount >= 1) {
					intPeriodCount += 1; //Start counting the periods
					intExtensionCount = 0; //Reset the extension count
					//If the period count is greater than 1, thats a no from me son
					if(intPeriodCount > 1) {
						blnCorrectInput = false;
						System.out.println("Incorrect Input");
						return blnCorrectInput;
					}
					
					
				}
				
				//If we're after the at sign, start counting the extensions
				if (intAtCount >= 1) {
					intExtensionCount += 1;
					if(intExtensionCount > 7) {
						blnCorrectInput = false;
						System.out.println("Incorrect Input");
						return blnCorrectInput;
					}
				}
				
				
			}
			
			
			//Check the extension
			if(intExtensionCount <= 2 || intPeriodCount < 1 || intAtCount < 1) {
				blnCorrectInput = false;
				System.out.println("Incorrect Input");
				return blnCorrectInput;
			}
			
			
		}
		
		
		return blnCorrectInput; 
	}
	
	
	
	
	
	/**
	 * Name- SetVehicleCount
	 * Abstract- Sets the vehicle amount of the Customer
	 */
	public void SetVehicleCount() {
		//Variables
		int intAmount = 0;
		boolean blnCorrectInput = true; //Is true when user provides correct input
		
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			System.out.print("\nPlease enter the amount of vehicles you want to rent(1-3): ");
			//Read the input from the Customer
			intAmount = ReadIntegerFromUser();
			//If out of range, It's false
			if(intAmount > 3 || intAmount < 1) {
				System.out.println("Incorrect Input");
				blnCorrectInput = false;
			}
		
		
		//While the input is incorrect
		} while(blnCorrectInput == false);
		
		//Set global variable
		m_intAmountOfVehicles = intAmount;
	}
	
	/**
	 * Name- GetVehicleCount
	 * Abstract- Gets the vehicle amount of the Customer
	 * @return m_intAmountOfVehicles- The amount of vehicles being rented
	 */
	public int GetVehicleCount() {
		return m_intAmountOfVehicles;
	}
	
	
	
	/**
	 * Name- SetRentalDays
	 * Abstract- Sets the days to be rented by the Customer
	 */
	public void SetRentalDays() {
		//Variables
		int intAmount = 0;
		boolean blnCorrectInput = true; //Is true when user provides correct input
		
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			System.out.print("\nPlease enter the amount of days you'll be renting: ");
			//Read the input from the Customer
			intAmount = ReadIntegerFromUser();
			//If out of range, It's false
			if(intAmount < 0) {
				System.out.println("Incorrect Input");
				blnCorrectInput = false;
			}
			else if (intAmount > 730) {
				System.out.println("The maximum rental period only covers up to 2 years.");
				blnCorrectInput = false;
			}
		
		
		//While the input is incorrect
		} while(blnCorrectInput == false);
		
		//Set global variable
		m_intNumberOfRentalDays = intAmount;
	}
	
	/**
	 * Name- GetRentalDays
	 * Abstract- Gets the amount of days to rent by the Customer
	 * @return m_intNumberOfRentalDays- The amount of vehicles being rented
	 */
	public int GetRentalDays() {
		return m_intNumberOfRentalDays;
	}
	
	
	
	/**
	 * Name- SetVehicleType
	 * Abstract- Sets the Vehicle Type of the Customer
	 */
	public void SetVehicleType() {
		//Variables
		String strVehicle; //vehicles of the user
		String astrAcceptedInputs[] = {"CAR","MOTORCYCLE","TRAILER"};
		boolean blnCorrectInput = true; //Is true when user provides correct input
		int intIndex = 0; //Index for loop
		int intLength = 0; //Length of the string
		int intArraySize = 0; //Size of array
		
		
		
		//Do this...
		do {
			//Make sure the input variable is reset to true on each run through
			blnCorrectInput = true;
			
			//Ask the question
			System.out.print("\nPlease enter the type of vehicle(Car, Motorcycle, or Trailer): ");
			//Read the input from the user
			strVehicle = ReadStringFromUser();
			//Check input
			strVehicle = strVehicle.toUpperCase();
			//Get length
			intLength = strVehicle.length();
			
			//Set the length of the accepted inputs array
			intArraySize = astrAcceptedInputs.length;
			
			//Check input
			for(intIndex = 0; intIndex < intArraySize; intIndex += 1) {
				if(strVehicle.equals(astrAcceptedInputs[intIndex])) {
					blnCorrectInput = true;
					break;
				}
				else {
					blnCorrectInput = false;
				}
				
				
				
				
			}
			
			
			
			//If bad bad display no no
			if(blnCorrectInput == false) {
				System.out.println("Incorrect Input");
			}
			

			
			
			
			
		//While the input is incorrect or we need more input
		} while(blnCorrectInput == false);
		
		
		
		
		
		//Set global variable
		m_strVehicleType = strVehicle.substring(0, intLength);
		
	}
	
	
	
	
	/**
	 * Name- GetVehicleType
	 * Abstract- Gets the vehicle type of the user
	 * @return m_strVehicleType- The vehicle type of the user
	 */
	public String GetVehicleType() {
		return m_strVehicleType;
	}
	
	
	
	/**
	 * Name- SetVehicalInformation
	 * Abstract- Sets the Vehicles Information
	 * @param strVehicleType: The type of vehicle
	 * @param intRentalDays- The amount of days being rented
	 */
	public void SetVehicalInformation(String strVehicleType, int intRentalDays) {
		//Variables
		int intWheels = 0; //Amount of wheels
		int intMpg = 0; //The miles per gallon
		String strHowToDrive = ""; //The way you drive
		float sngPrice = 0; //Price
		
		if(strVehicleType.equals("CAR")) {
			CCar Vehicle = new CCar(intRentalDays);
			intWheels = Vehicle.GetWheels();
			strHowToDrive = Vehicle.GetHowToDrive();
			intMpg = Vehicle.GetMilesPerGallon();
			sngPrice = Vehicle.GetPrice();
		}
		else if(strVehicleType.equals("MOTORCYCLE")) {
			CMotorcycle Vehicle = new CMotorcycle(intRentalDays);
			intWheels = Vehicle.GetWheels();
			strHowToDrive = Vehicle.GetHowToDrive();
			intMpg = Vehicle.GetMilesPerGallon();
			sngPrice = Vehicle.GetPrice();

		} 
		else if(strVehicleType.equals("TRAILER")) {
			CTrailer Vehicle = new CTrailer(intRentalDays);
			intWheels = Vehicle.GetWheels();
			strHowToDrive = Vehicle.GetHowToDrive();
			intMpg = Vehicle.GetMilesPerGallon();
			sngPrice = Vehicle.GetPrice();
		}

		//Set global variables
		m_sngPrice = sngPrice;
		m_intWheels = intWheels;
		m_strHowToDrive = strHowToDrive;
		m_intMilePerGallon = intMpg;
	}
	
	
	

	
	
	
	/**
	 * Name - ReadFloatFromUser
	 * Abstract - Gets float input from user
	 * @return sngValue- Returns float value from buffered reader
	 */
	public static float ReadFloatFromUser() {

		float sngValue = 0;

		try {
			String strBuffer = "";

			// Input stream
			BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));

			// Read a line from the user
			strBuffer = burInput.readLine();

			// Convert from string to float
			sngValue = Float.parseFloat(strBuffer);
		} catch (Exception excError) {
			System.out.println(excError.toString());
		}

		// Return float value
		return sngValue;
	}
			
		
		/**
		 * Name: ReadStringFromUser
		 * Abstract: uses buffered reader to get string input from user
		 * @return strBuffer- string input from user
		 */
		public static String ReadStringFromUser( )
		{			  

			String strBuffer = "";	
			
			try
			{
				
				// Input stream
				BufferedReader burInput = new BufferedReader( new InputStreamReader( System.in ) ) ;

				// Read a line from the user
				strBuffer = burInput.readLine( );
		
			}
			catch( Exception excError )
			{
				System.out.println( excError.toString( ) );
			}
			
			// Return integer value
			return strBuffer;
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
