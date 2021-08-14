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
  -This web app will be using 4 tables to manage the reservation functionality with Mysql.
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
 

