# bookingApiAlten
#developed by Carlos Yanez to Alten.
#I will be using English and French in this project as a language of documentation.

1.-Booking API Considerations
  This web application is developed to manage the reservations of a room in an hotel in Cancun.
  
  
  -Functionalities 
    *Check availability
    *Create a reservation
    *Cancel a reservation
    *Modify reservation
  
  -Constraints
    *one room available
    *stay <=3 days
    *reservation in advance <=30 days
    *No payment functionalities
    *The API is insecure.


2.-Functionalities.

  -Sign in
    *Create a simple sign in, in order to have access to the rooms reservations and check availability
    *Fields -> email,password
    *Methods -> Create user and forget password method and login.

  -Check availability
    *Calendar with available dates.
    
  -Create a reservation
    *Fields -> start date, finish date.
    
  -Cancel a reservation
    * Select the reservation code and cancel it.
    
  -Modify a reservation
    * Select the reservation code and modify the dates, checking availability.
    

3.-Database

  -This web app will be using 4 tables to manage the reservation functionality with SQL Server
  
    *Clients -> information of clients such as email, password
    
    *Rooms -> information of rooms such as idRoom, code,name, price,description, status, in this challenge only exists one row.
    
    *Status -> status of reservation (reserved, cancel)    
    
    *Reservation -> data of reservations with foreing keys of clients, rooms, status.
    


4.-Backend

  -This web app will be using 3 tiers or layers in backend using unit of work as design model (interfaces, abstract classes, add Trasient in startup file,depedency injection, DTO and json to comunicate with front-end) developing with .NetCore
  
    *Data access layer -> called bookingApi1DataAccess to manage the access to database using Entity Framework.
    
    *Business logic layer->called bookingApi2BusinessLogic to manage the constrainst and validation such as check availability, login, forget password, modify.
    
    *Services layer-> publish the services to comunicate with front-end and call the methods throught unit of work interface
    
 
 5.-Frontend
 
  - This layer will be developed with React 
    
    *Using hooks to manage the doom
       
    *Using redux to manage the data exchange.
    
    *Axios to comunicate with the webAPI.
 
******************************
Installation
I used SQL Server and Visual Studio Code for the development

Steps for installation:

  *Create a database called BookingAPI
  
  *Execute the scripts of folder scriptsDB, begining with the file 01CreateDatabase.sql until 5.CalendarAvailability.sql
    this files create tables and data needed for the application.
    
  *Modify the string connection whom is located in appsettings.json in "Default" parameter
  
       "Default":"data source=localhost;initial catalog=BookingAPI;user id=your_user;password=your_password;MultipleActiveResultSets=true"
       

*********************************

Parameters

*This webApi has parameters of string connection ("Default")

*maxDay => maximun of days in advance for a reservation according to requirement

*MaxReservation => maximon days of reservation according to requirement


*********************************
Methodology

*This web api use the unit of work design pattern, using facade and repositories with dependency injection

*In BackEnd use 3 layers

  --bookingApi1DataAccess => To connect with database using Entity Framework Core with context.
  
  --bookingApi2BusinessLogic => Develop the business logic and validations with interfaces and classes whom implements this, also DTO mapping and utilities to manage dates.
  
  --bookingApi3WebServices => Controller to publish the webAPi with an interface whom represent the initial facade to the repositories, generating responses type
  
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
  
  with json.
  The Logger also is implemented to trace the application
  
  Run the webApi acceding to bookingApi3WebServices and executing in console the command "dotnet run"
  
    Accesing to https://localhost:5001/swagger/index.html you can have access to the webapi resume with option to try each service
    
*In Front end use React using Context, useState,useEffect hooks to manage the components behavior.
   This applications have Utilitiesm DTO,context,constant,messages and components organized in folder to a good Administration.
  Run React acceding to bookingapi4-frontend folder and run in console the command "npm start"![Capture d’écran de 2021-08-20 04-33-55](https://user-images.githubusercontent.com/20178297/130217352-f4d7f4d7-86da-4cc7-aa1b-6a151473c0cb.png)
 


**************************************
Application use

*The application shows a initial page with 2 tabs 

  -the first one with an availability table, sig-up and sign-in modal screens
  
  -the second tab contains the acceso to github project
  
-Use

First you have to sign-up with an email and password 

Second with sign-in button you can access to the component to create, modify and delete reservetions funcionalities with the validations in the requeriment.



Screes of application:
Swagger:
![Capture d’écran de 2021-08-20 04-33-55](https://user-images.githubusercontent.com/20178297/130217398-ff18ad21-2d72-4ce2-a7fb-d89ec8299ccc.png)




