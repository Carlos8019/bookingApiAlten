# bookingApiAlten
#developed by Carlos Yanez to Alten.
#I will be using English and French in this project as a language of documentation.

1.-Booking API Considerations
  This web application is developed to manage the reservations of a room in an hotel in Cancun.
  
  
  -Functionalities 
    *Check availability
    *Sign-up
    *Sign-in
    *Create a reservation
    *Cancel a reservation
    *Modify a reservation
  
  -Constraints
    *one room available
    *stay <=3 days
    *reservation in advance <=30 days
    *No payment functionalities
    *The API is insecure.


2.-Functionalities.

  -Sign in
    *Create a simple sign in, in order to have access to the rooms reservations to create, modify or delete.
    *Fields -> email,password
    *Methods -> Create user method and login.

  -Check availability
    *Calendar with available dates the next 30 days, this data is parametrized from webapi.
    
  -Create a reservation
    *Fields -> start date, finish date.
    
  -Cancel a reservation
    * Select the reservation and cancel it.
    
  -Modify a reservation
    * Select the reservation and modify the dates, checking availability.
    

3.-Database

  -This web app will be using 4 tables to manage the reservation functionality with SQL Server
  
    *BTClients -> information of clients such as email, password
    
    *BTRooms -> information of rooms such as idRoom, code,name, price,description, status, in this challenge only exists one row.
    
    *BTReservations -> data of reservations with foreing keys of clients and rooms.

    *BTCalendarAvailability->Dates with a flag representing the availability
    
  **Dates are used with yyyymmdd format for easy use

![Capture d’écran de 2021-08-20 12-40-30](https://user-images.githubusercontent.com/20178297/130275532-6420c8ae-98c7-409e-8a85-456c8dc627ef.png)


*********************************
Methodology

4.-Backend

  -This web app will be using 3 tiers or layers in backend using unit of work as design model (interfaces, abstract classes, add Trasient in startup file,depedency injection, DTO and json to comunicate with front-end) developing with .NetCore
  
    *Data access layer -> called bookingApi1DataAccess to manage the access to database using Entity Framework Core with entities.
    
      -The class GenericRepository in floder Classes manage the interaction with database is used with an Interface IGenericRespository, in folder Interfaces, to generate the dependency injecton approach.

      -Folder Models contains tables representation in classes usin data notations to hide the names of tables and columns.

      -BApiContext is used with entity framework core to manage crud operations
    
    

![Capture d’écran de 2021-08-20 12-45-33](https://user-images.githubusercontent.com/20178297/130275700-064a3de9-4197-4ab1-a41c-bcdcaa3dd001.png)


    
    
    *Business logic layer->called bookingApi2BusinessLogic to manage the constrainst and validation such as check availability, login, modify and delete.

      -Dto Folder contains the mapping in string to receive the data transfer in json fro react this data will be tranformed to Class Models.

      -Interfaces Folder contains the interfaces to be used in the facade with dependency injection, represents the contract and structure of methods in repository.Those interfaces will be invoqued in unit of work pattern.

      -Repositories Folder contains the interfaces implementations of each repository making this decouple of eacho other.

      -Utilities Folder contains methods to be used in the application, in this case managind dates format to centralice the functionality.

      -Dependency Injection File contains the administration of objects using trasient and scoped methods this file is called in startup im web layer.

      -UnitOfWork File represents the facade to acces to different repositories as a subsystems.
    
    
![Capture d’écran de 2021-08-20 12-54-30](https://user-images.githubusercontent.com/20178297/130275887-082d54de-67a6-457a-8b05-230364164179.png)
    
    

    *Services layer-> publish the services to comunicate with front-end and call the methods throught unit of work interface.
    --Controllers Folder contain the controller with code of each service reproducing status codes according to functionality:

       StatusCodes.Status200OK
       StatusCodes.Status404NotFound
       StatusCodes.Status400BadRequest

    --startup File contains the general configuration of application connecting to the database and inplementing the dependency injection and enabling swagger

    --swagger shows the information of web services as a webpage.

    --appsettings.json contains the parameters of the webAPI

       --string connection ("Default")

       --maxDay => maximun of days in advance for a reservation according to requirement

       --MaxReservation => maximon days of reservation according to requirement

  --The Logger also is implemented to trace the application in business logic and webApi
  
  Run the webApi acceding to bookingApi3WebServices and executing in console the command "dotnet run"
  
  Accesing to https://localhost:5001/swagger/index.html you can have access to the webapi resume with option to try each service
  
  
![Capture d’écran de 2021-08-20 13-01-56](https://user-images.githubusercontent.com/20178297/130275992-b77ec7d5-ae10-43ad-b0b5-922815d75d84.png)
  
  
 
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
       

 
  
    
*In Front end use React using Context, useState,useEffect hooks to manage the components behavior.
   This applications have Utilitiesm DTO,context,constant,messages and components organized in folder to a good Administration.
  Run React acceding to bookingapi4-frontend folder and run in console the command "npm start"
 


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


Initial Page:
![Capture d’écran de 2021-08-20 04-28-12](https://user-images.githubusercontent.com/20178297/130217820-5cab1372-d013-4d64-b53e-96fbe3bfa525.png)

Availability :
![Capture d’écran de 2021-08-20 04-28-02](https://user-images.githubusercontent.com/20178297/130217955-6c4eec21-49d0-40e9-b277-4787f1cdc5bc.png)

Create User sign-up:

![Capture d’écran de 2021-08-20 04-28-34](https://user-images.githubusercontent.com/20178297/130218072-9c179e42-c527-43e4-82b7-75e2887bf268.png)


Login sing-in:

![Capture d’écran de 2021-08-20 04-28-44](https://user-images.githubusercontent.com/20178297/130218173-7cb2896f-647f-479a-9f59-50f52589f498.png)

Page of reservations:

![Capture d’écran de 2021-08-20 04-30-35](https://user-images.githubusercontent.com/20178297/130218277-0541748e-4424-4fad-9118-763588e5a0c9.png)


Create a reservation:

![Capture d’écran de 2021-08-20 04-28-59](https://user-images.githubusercontent.com/20178297/130218399-cbde076f-e3a4-41af-8035-c3afd02b260a.png)

Edit a reservation:

![Capture d’écran de 2021-08-20 04-29-08](https://user-images.githubusercontent.com/20178297/130218481-c0b77918-083a-476a-9540-f39bbcc178a6.png)


Delete a reservation:

![Capture d’écran de 2021-08-20 04-30-35](https://user-images.githubusercontent.com/20178297/130218600-4c150f4f-5189-4723-8b4e-6e1c742e7a65.png)


Validation messages:

![Capture d’écran de 2021-08-20 04-29-38](https://user-images.githubusercontent.com/20178297/130218665-cc3ff23b-a13b-4ec6-af81-85c26f5a59cc.png)

![Capture d’écran de 2021-08-20 04-29-20](https://user-images.githubusercontent.com/20178297/130218735-30d1617a-4d58-46f9-8926-da614e2b6bd0.png)
![Capture d’écran de 2021-08-20 04-30-29](https://user-images.githubusercontent.com/20178297/130218916-a8383518-b4cb-47c1-ab25-d7d7ab514bdb.png)

Project Folders:

![Capture d’écran de 2021-08-20 04-31-35](https://user-images.githubusercontent.com/20178297/130219014-622df494-43cb-4e7f-afca-af927de6a174.png)


