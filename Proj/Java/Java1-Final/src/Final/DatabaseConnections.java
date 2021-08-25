/**
 * 
 */
package Final;
import java.io.*;
import java.sql.*;
import com.microsoft.sqlserver.jdbc.*;




/**
 * @author Tim Robbins
 * Abstact- Java 1: Final
 */



/** method name: DatabaseConnections
 * Abstract: Hosts functions to open the database connections and interacts with it
 *
 * Uses the database file:
 * 								dbVehicleRental.sql for database dbVehicleRental
 * 
 * Include the following jar files:
 * 								commons-lang-2.6.jar
 * 								commons-logging-1.1.3.jar
 * 								sqlijdbc4.jar
 * 								ucanaccess-2.0.7.jar
 * 								jackcess-2.0.4.jar
 * 								commons-lang-2.6.jar
 * 								commons-logging-1.1.3.jar
 * 								hsqldb.jar
 */
public class DatabaseConnections {
	
	//Variables
	private static Connection m_conAdministrator = null; //Method connection variable decleration
	
		
	/**
	 * Name- Connect
	 * Abstract- Gets the connection to the database
	 * @return blnResult- The Result of the attempted connection
	 */
	public boolean Connect() {
		//Variables
		boolean blnResult = false; //boolean result of the connection
		
		try
		{
			SQLServerDataSource sdsLocations = new SQLServerDataSource( ); //Set the datasource
			sdsLocations.setServerName( "localhost\\SQLExpress" ); // Set SQL Express server name
			sdsLocations.setPortNumber( 1433 ); //Set port number
			sdsLocations.setDatabaseName( "dbVehicleRental" ); //Set the database name
			
			//Set the user to sa
			sdsLocations.setUser( "sa" );
			//And the password to your password(Removed my password)
			sdsLocations.setPassword( "" );
			
			// Open a connection to the database
			m_conAdministrator = sdsLocations.getConnection(  );
			
			// Success
			blnResult = true;
		}
		catch( Exception excError )
		{
			// Display Error Message
			System.out.println( "Could not connect - error: " + excError );

			// Warn about SQL Server JDBC Drivers
			System.out.println( "Make sure to download the MS SQL Server JDBC Drivers");
		}
		
		return blnResult;
	}
	
	
	
	
	/**
	* Name: CloseConnection
	* Abstract: Close the connection to the database
	* @return blnResult- The Result of the attempted connection close
	*/ 
	public boolean CloseConnection( ) {
		//Variables
		boolean blnResult = false; //Variable for if we were able to connect
		
		
		//Try this...
		try
		{
			// If there is a connection object
			if( m_conAdministrator != null )
			{
				// If yes, close the connection if still open
				if( m_conAdministrator.isClosed( ) == false ) 
				{
					m_conAdministrator.close( );
					
					// Prevent JVM from crashing
					m_conAdministrator = null;
				}
			}
			// If we made it this far, set the result to true
			blnResult = true;
		}
		
		//If there was an exception
		catch( Exception excError )
		{
			// Display Error Message
			System.out.println( excError );
		}
		
		//Return the result
		return blnResult;
	}
	
	
	
	/**
	* Name: LoadLocationsList
	* Abstract: Loads location list from the database
	* @param strTable- The table getting data from
	* @param strPrimaryKeyColumn- The tables Primary key
	* @param strNameColumn1- Column 1
	* @param strNameColumn2- Column 2
	* @param strNameColumn3- Column 3
	* @param strNameColumn4- Column 4
	* @param strNameColumn5- Column 5
	* @return blnResult- The Result of getting the table from the database
	*/ 
	public boolean LoadLocationsList( String strTable, String strPrimaryKeyColumn, String strNameColumn1, 
			String strNameColumn2, String strNameColumn3, String strNameColumn4, String strNameColumn5) {
		
		//Variables
		boolean blnResult = false; //boolean flag
		
		//try this
		try
		{
			//Try varaibles
			String strSelect = ""; //Select
			Statement sqlCommand = null; //sql command
			ResultSet rstTSource = null; //sql Result
			int intID = 0; //The ID value
			String strLocationName = ""; //The location name of the place
			String strAddress = ""; //The address of the place
			String strCity = ""; //The city of the place
			String strState = ""; //The state the place is in
			String strZip = ""; //The zip code of the place
			
			// Build the sql string through a select statement
			strSelect = "SELECT " + strPrimaryKeyColumn + ", " + strNameColumn1 
					    + ", " + strNameColumn2
					    + ", " + strNameColumn3
					    + ", " + strNameColumn4
					    + ", " + strNameColumn5
						+ " FROM " + strTable
						+ " ORDER BY " + strNameColumn1; 
					
			
			// Get the all the records	
			sqlCommand = m_conAdministrator.createStatement( ); //Set the command
			rstTSource = sqlCommand.executeQuery( strSelect ); //Execute the select query
			
			// Loop through all the records
			System.out.println("\n\nHere are the pickup locations - we will call you with a location confirmation\n");
			while( rstTSource.next( ) == true )
			{
				// Get the Information from the current row
				intID = rstTSource.getInt( 1 );
				strLocationName = rstTSource.getString( 2 );
				strAddress = rstTSource.getString( 3 );
				strCity = rstTSource.getString( 4 );
				strState = rstTSource.getString( 5 );
				strZip = rstTSource.getString( 6 );
				//print
				System.out.printf( "ID: %-10d 		Location: %-12s Address: %-15s City: %-15s State: %-15s Zip: %-15s%n", 
									intID,strLocationName,strAddress,strCity,strState,strZip);
			}
			// Clean up
			rstTSource.close( );
			sqlCommand.close( );
			// Set the result to true
			blnResult = true;
		}
		//Catch exception
		catch 	(Exception e) {
			System.out.println( "Error loading table" );
			System.out.println( "Error: " + e );
		}
		
		return blnResult;
	}
	
		
	
	

}
