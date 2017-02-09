# supershoes

## Software versions used

1. Web API 2.1
2. Visual Studio 2015
3. Entity Framework 6
4. .NET 4.5
5. Knockout.js 3.1

## Deploy

This Application was published on Azure. For publish de Application, follow the next steps:

1. Set up the development environment:

	Download the latest Azure SDK for Visual Studio 2015


2. Create the Azure resources:

	a. Create an empty DataBase SuperShoes on Azure
	

3. Publish the Application to Azure Azure App Service:

	b.  In Solution Explorer, right-click the project and select Publish

		<img src="Images/Publishing1.png" alt>

		Clicking Publish invokes the Publish Web dialog.

	c.  Configure the connection and settings

		<img src="Images/Publishing2.png" alt>

	d. Just click the Settings tab and check "Execute Code First Migrations"

	e. To deploy the Application, click Publish. You can view the publishing progress in the Web Publish Activity window. 

4. Open the Application in the Browser

	http://supershoe.azurewebsites.net


## API Rest

	The Api uses HTTP Basic Authentication. The username is "my_user" and the password is "my_password" for the proof. Encoding this whit https://www.base64encode.org/ the result looks like this bXlfdXNlcjpteV9wYXNzd29yZA==

	The Api offers the nex Services:

	1. http://supershoe.azurewebsites.net/Help

	It's possible to use Postman to test the API Services:

	<img src="Images/Postman1.png" alt>




