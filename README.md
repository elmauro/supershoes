# supershoes

## Software versions used

1. Web API 2.1
2. Visual Studio 2015
3. Entity Framework 6
4. .NET 4.5
5. Knockout.js 3.1

## Deploy

This Application was published on Azure. For publish de Application, follow the next steps:

<br />
1. Set up the development environment:

Download the latest Azure SDK for Visual Studio 2015

<br />
2. Create the Azure resources:

a. Create an empty DataBase SuperShoes on Azure
	
<img src="https://github.com/elmauro/supershoes/blob/documentation/SuperShoes/Images/Database1.png?raw=true">

<br />
3. Publish the Application to Azure Azure App Service:

a. In Solution Explorer, right-click the project and select Publish. Clicking Publish invokes the Publish Web dialog.

<img src="https://github.com/elmauro/supershoes/blob/documentation/SuperShoes/Images/Publishing1.png?raw=true">

b.  Configure the connection and settings

<img src="https://github.com/elmauro/supershoes/blob/documentation/SuperShoes/Images/Publishing2.png?raw=true">

   c. Just click the Settings tab, check "Use this connection string ..." and check "Execute Code First Migrations"

   d. To deploy the Application, click Publish. You can view the publishing progress in the Web Publish Activity window. 

<br />
4. Open the Application in the Browser

http://supershoe.azurewebsites.net


## API Rest

The Api uses HTTP Basic Authentication. The username is "my_user" and the password is "my_password" for the proof. Encoding this whit https://www.base64encode.org/ the result looks like this bXlfdXNlcjpteV9wYXNzd29yZA==

<br />
1. The Api offers the nex Services:

a. http://supershoe.azurewebsites.net/Help

b. It's possible to use Postman to test the API Services:

<img src="https://github.com/elmauro/supershoes/blob/documentation/SuperShoes/Images/Postman1.png?raw=true">


## Future Work

1. To improve the Grid presentation of the data with pagination and to implement popups.
<br/>
2. To improve data validation of input data. 