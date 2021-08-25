-- --------------------------------------------------------------------------------
-- Name:		dbVehicleRental
-- Class:		Java 1
-- Abstract:    DB for final
-- --------------------------------------------------------------------------------



-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbVehicleRental;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- ----------------------------------------------------------------------
-- Drops
-- ----------------------------------------------------------------------
--IF OBJECT_ID( 'TVehicleRental' )					IS NOT NULL 	DROP TABLE	TVehicles
IF OBJECT_ID( 'TCustomers' )						IS NOT NULL		DROP TABLE TCustomers
IF OBJECT_ID( 'TLocations' )						IS NOT NULL		DROP TABLE TLocations



GO

-- ----------------------------------------------------------------------
-- Tables
-- ----------------------------------------------------------------------
CREATE TABLE TLocations
(
	 intLocationID		INTEGER			NOT NULL
	,strLocationName	VARCHAR(50)		NOT NULL
	,strAddress			VARCHAR(50)		NOT NULL
	,strCity			VARCHAR(50)		NOT NULL
	,strState			VARCHAR(2)		NOT NULL
	,strZip				VARCHAR(5)		NOT NULL
	,CONSTRAINT TLocations_PK PRIMARY KEY ( intLocationID )
)


CREATE TABLE TCustomers
(
	intCustomerID                    INTEGER        NOT NULL,
	intLocationID                    INTEGER        NOT NULL,
	strFirstName                     VARCHAR(50)    NOT NULL,
	strLastName                      VARCHAR(50)    NOT NULL,
	strPhoneNumber     		         VARCHAR(50)    NOT NULL,
	intNumberOfRentalDays            INTEGER        NOT NULL,
	intNumberOfVehicles       	     INTEGER        NOT NULL,
	strVehicleType 	                 VARCHAR(50)    NOT NULL,
	CONSTRAINT TCustomers_PK PRIMARY KEY CLUSTERED ( intCustomerID )
)
	

	

-- ----------------------------------------------------------------------
-- Foreign keys
-- ----------------------------------------------------------------------
ALTER TABLE TCustomers ADD CONSTRAINT TCustomers_TLocations_FK
FOREIGN KEY (intLocationID) REFERENCES TLocations (intLocationID)
	
	
-- ----------------------------------------------------------------------
-- Inserts
-- ----------------------------------------------------------------------
-- TLocations
INSERT INTO TLocations( intLocationID, strLocationName, strAddress, strCity, strState, strZip)
VALUES		 (1, 'Northwest', '10 Colerain', 'Cinti', 'OH', '45241')
			,(2, 'Downtown', '2010 Vine', 'Cinti', 'OH', '45201')
			,(3, 'Loveland', '202 Main St ', 'Loveland', 'OH', '45140')
			,(4, 'Hamilton', '9010 C Street', 'Hamilton', 'OH', '45013')

INSERT INTO TCustomers( intCustomerID,intLocationID ,strFirstName, strLastName, strPhoneNumber, intNumberOfRentalDays, intNumberOfVehicles,strVehicleType)
VALUES		 (1,2,'Tim','Robbins','5131231234',3,1,'Nissan Sentra')

GO

-- ----------------------------------------------------------------------
-- Testing
-- ----------------------------------------------------------------------
-- SELECT * FROM TLocations WHERE intVehiclesID = '1'
-- delete from tlocations