# H_Plus_Sports Web API's using ASP.NET Core 2
An ecommerce engine for creating and viewing customer orders and assigning orders to sales reps. Implemented CRUD 
operations on cloud-based Azure SQL Server where the data is stored. Used repository layer and dependency injection 
to allow API's to be more abstract. Used MSTest for Integration testing of API endpoints. Introduced 3 different
ways for caching data to enhance performance.  
* Response caching
* In-memory caching
* Distributed caching with Redis

##Concepts
Concepts that were implemented for this project.
*Database on Azure
*Routing
*Model binding and valadation
*Inspecting and optimizing API performance
*Repository pattern
*Integration Testing
*Deployment
*Caching data

##Known Bugs
Outside of the Customer controller, every Get by Id method failed in test. Although when deployed and in dev
tools response works and gets a 200 OK message. 

##Technologies Used
ASP.NET Core, Azure, MSTest, C#

##
Rouz Majlessi
www.rouzm.com
