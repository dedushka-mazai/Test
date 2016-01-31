**MassiveTest Solution**
-----------------------------------

Solution has been created in VS2012 and consists of such projects:

 - MassiveTest.Interface - contains all used interfaces
 - MassiveTest.Graphs - implements graph logic
 - MassiveTest.DataProvider - contains several data providers (TestDataProvider, DbDataProvider etc)
 - MassiveTest.DbEngine.MySql - contains implementation of IDbEngine interface for MySQL database
 - MassiveTest.Wcf.Services - contains WCF graph services
 - MassiveTest.Wcf.App - web application
 - MassiveTest.Wcf.Client - contains WCF client classes to access WCF services
 - MassiveTest.DataLoader - console client application for uploading nodes to the database
 - MassiveTest.GraphViewer - WPF UI client to display graph and find shortest path between nodes
 - MassiveTest.Tests - contains unit-tests


Used 3rd party libraries:

 - xUnit - unit testing
 - GraphX - graph visualization
 - Unity - IoC container for WCF service
 - MySQL .NET Connector - MySQL client

Installation:

 - MySQL Server 5.7.10 available here:  http://cdn.mysql.com//Downloads/MySQLInstaller/mysql-installer-web-community-5.7.10.0.msi
 - Graphs database: unpack file ./DB/MySql/Release/MySQL_Graphs_Installer.zip, then run my_install.cmd
 - Web application: create application (with pool "ASP .NET v4.0") in IIS with the name "Graphs" and path to some empty folder on the disk; rebuild solution, then publish web application to this folder; in the db.config file from the web service folder specify password for user 'root' (which was entered during MySQL server installation)
 - DataLoader: build project, then run (specify folder with nodes xml files as the first parameter)
 - GraphViewer: build project, then run. To load graph from the service and display it press button "Load", to calc shortest path between nodes select two and press "Calc" button

