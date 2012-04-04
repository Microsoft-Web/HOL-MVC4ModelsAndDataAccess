<a name="HOLTop" />

# ASP.NET MVC 4 Models and Data Access #
---

<a name="Overview" />
## Overview ##

>**Note:** This Hands-on Lab assumes you have basic knowledge of **ASP.NET MVC**. If you have not used **ASP.NET MVC** before, we recommend you to go over **ASP.NET MVC 4 Fundamentals** Hand-on Lab.

In **ASP.NET MVC Fundamentals** Hand-on Lab, you have been passing hard-coded data from the Controllers to the View templates. But in order to build a real web application you might want to use a real database.

This Hands-on Lab will show you how to use the free SQL Server Express as a database engine in order to store and retrieve the data needed for the Music Store application. To accomplish that, you will start with an already created database from which you will create the Entity Data Model for the application. Through this lab, you will meet the **Database First** approach and the **Code First** Approach as well.

However, you could use a **Model First** approach, by creating the same model using the tools and then generating a database from it.

 ![Database First vs. Model First](./images/Database-First-vs.-Model-First.png?raw=true "Database First vs. Model First")
 
_Database First vs. Model First_

After generating the Model, you will make the proper adjustments in the StoreController to provide the Store Views with the data taken from the database instead of hard-coded one. You will not need to make any change to the View templates because the StoreController will be returning to the View templates the same ViewModels as before, although this time the data will come from the database.

**The Code First Approach**

The Code First approach allows us to define the model from the code without generating classes that are generally coupled with the framework.

In code first, Model objects are defined with POCOs , "Plain Old CLR Objects". POCOs are defined as simple plain classes that have no inheritance and do not implement interfaces. We can automatically generate the database from them, or we can use an existing database and generate the class mapping from the code.

The benefit of using this approach is that the Model remains independent from the persistence framework (in this case, Entity Framework), as the POCOs classes are not coupled with the mapping framework.

> **Note:** Although that this Hands-on Lab will cover the use of the free SQL Server Express, the code will also work with the full version of SQL Server.

This Lab is based on ASP.NET MVC 4.

If you wish to explore the whole Music Store tutorial application you can find it in [http://mvcmusicstore.codeplex.com/](http://mvcmusicstore.codeplex.com/).

 
<a name="SystemRequirements" />
### System Requirements ###

You must have the following items to complete this lab:

- Visual Studio 11 Express Beta for Web

<a name="Setup" />
### Setup ###

#### Installing Code Snippets####
For convenience, much of the code you will be managing along this lab is available as Visual Studio code snippets. To install the code snippets run the **.\Source\Assets\CodeSnippets.vsi** file.

####Installing Web Platform Installer####
This section assumes that you don't have some or all the system requirements installed. In case you do, you can simply skip this section.

Microsoft Web Platform Installer (WebPI) is a tool that manages the installation of the prerequisites for this Lab.

> **Note:** As well as the Microsoft Web Platform, WebPI can also install many of the open source applications that are available like Umbraco, Kentico, DotNetNuke and many more.  These are very useful for providing a foundation from which to customize an application to your requirements, dramatically cutting down your development time.

Please follow these steps to downloads and install Microsoft Visual Studio 11 Express Beta for Web:

1. Install **Visual Studio 11 Express Beta for Web**. To do this, Navigate to [http://www.microsoft.com/web/gallery/install.aspx?appid=VWD11_BETA&prerelease=true](http://www.microsoft.com/web/gallery/install.aspx?appid=VWD11_BETA&prerelease=true) using a web browser. 

	![Web Platform Installer 4.0 window](./images/Microsoft-Web-Platform-Installer-4.png?raw=true "Web Platform Installer 4.0 download")

	_Web Platform Installer 4.0 download_

1. The Web Platform Installer launches and shows Visual Studio 11 Express Beta for Web Installation. Click on **Install**.

 	![Visual Studio 11 Express Beta for Web Installer window](./images/Microsoft-VS-11-Install.png?raw=true "Visual Studio 11 Express Beta for Web Installer window")
 
	_Visual Studio 11 Express Beta for Web Installer window_

1. The **Web Platform Installer** displays the list of software to be installed. Accept by clicking **I Accept**.

 	![Web Platform Installer window](./images/Microsoft-Web-Platform-Installer-Prerequisites.png?raw=true "Web Platform Installer window")
 
	_Web Platform Installer window_

1. The appropriate components will be downloaded and installed.

 	![Web Platform Installation - Download progress](./images/Web-Platform-Installation-Download-progress.png?raw=true "Web Platform Installation - Download progress")
 
	_Web Platform Installation - Download progress_

1. The **Web Platform Installer** will resume downloading and installing the products. When this process is finished, the Installer will show the list of all the software installed. Click **Finish**.

 	![Web Platform Installer](./images/Web-Platform-Installer.png?raw=true "Web Platform Installer")
 
	_Web Platform Installer_

---

<a name="Exercises" />
## Exercises ##

This Hands-On Lab is comprised by the following exercises:

1. [Exercise 1: Adding a Database](#Exercise1)

1. [Exercise 2: Adding a Database using Code First](#Exercise2)

1. [Exercise 3: Querying the Database with Parameters](#Exercise3)

1. [Exercise 4: Using Asynchronous Controllers](#Exercise4)
 
Estimated time to complete this lab: **35 minutes**.

> **Note:** Each exercise is accompanied by an **End** folder containing the resulting solution you should obtain after completing the exercises. You can use this solution as a guide if you need additional help working through the exercises.

 
<a name="Exercise1" />
### Exercise 1: Adding a Database ###

In this exercise, you will learn how to add a database with the tables of the MusicStore application to the solution in order to consume its data. Once adding the database and generating the Model it will represent, you will make the proper adjustments in the StoreController class to provide the View template with the data taken from the database instead of hard-coded one.

<a name="Ex1Task1" />
#### Task 1 - Adding a Database ####

1. In this task, you will add an already created database with the main tables of the MusicStore application to the solution. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex01-AddingADatabaseDBFirst\Begin, select **MvcMusicStore.sln** and click **Open**.

1.	Follow these steps to install the **NuGet** package dependencies.

	a.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	b.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	c.	After installing the package, type **Enable-PackageRestore**.

	d.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

	>**Note:** One of the advantages of using NuGet is that you don't have to ship all the libraries in your project, reducing the project size. With NuGet Power Tools, by specifying the package versions in the Packages.config file, you will be able to download all the required libraries the first time you run the project. This is why you will have to run these steps after you open an existing solution from this lab.
	
	>For more information, see this article: <http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages>.

1. Add an **App_Data** folder to the project to hold the SQL Server Express database files. **App_Data** is a special folder in ASP.NET which already has the correct security access permissions for database access. To add the folder, right-click **MvcMusicStore** project, point to **Add** then to **Add ASP.NET Folder** and finally click **App_Data**.

 	![Adding an AppData folder](./images/Adding-an-AppData-folder.png?raw=true "Adding an AppData folder")
 
	_Adding an App_Data folder_

1. Add **MvcMusicStore** database file. In this hands-On Lab, you will use an already created database called **MvcMusicStore.mdf**. To do that, right-click the new **App_Data** folder, point to **Add** and then click **Existing Item**. Browse to **\Source\Assets\** and select the **MvcMusicStore.mdf** file.

 	![Adding an Existing Item](./images/Adding-an-Existing-Item.png?raw=true "Adding an Existing Item")
 
	_Adding an Existing Item_

 	![MvcMusicStore.mdf database file](./images/MvcMusicStore.mdf-database-file.png?raw=true "MvcMusicStore.mdf database file")
 
	_MvcMusicStore.mdf database file_

	The database has been added to the project. Even when the database is located inside the solution, you can query and update it as it was hosted in a different database server.

	![MvcMusicStore database in Solution Explorer](./images/MvcMusicStore-database-in-Solution-Explorer.png?raw=true "MvcMusicStore database in Solution Explorer")
 
	_MvcMusicStore database in Solution Explorer_

1. Verify the connection to the database. To do this, double-click the **MvcMusicStore.mdf**. The connection is established.

 	![Connecting to MvcMusicStore.mdf](./images/Connecting-to-MvcMusicStore.mdf.png?raw=true "Connecting to MvcMusicStore.mdf")
 
	_Connecting to MvcMusicStore.mdf_


<a name="Ex1Task2" />
#### Task 2 - Creating a Data Model ####

In this task, you will create a data model to interact with the database added in the previous task. 

1. Create a data model that will represent the added database. To do this, in Solution Explorer right-click the **Models** folder, point to **Add** and then click **New Item**. In the **Add New Item** dialog, select the **Data** template and then the **ADO.NET Entity Data Model** item. Change the data model name to **StoreDB.edmx** and click **Add**.

 	![Adding the StoreDB ADO.NET Entity Data Model](./images/Adding-the-StoreDB-ADO.NET-Entity-Data-Model.png?raw=true "Adding the StoreDB ADO.NET Entity Data Model")
 
	_Adding the StoreDB ADO.NET Entity Data Model_

1. The **Entity Data Model Wizard** appears. This wizard will guide you through the creation of the model layer. Since the model should be created based on the existing database added in the last task, select **Generate from database** and click **Next**.

 	![Choosing the model content](./images/Choosing-the-model-content.png?raw=true "Choosing the model content")
 
	_Choosing the model content_

1. Since you are generating a model from a database, you will need to specify which database to use. This should be done by selecting the database **MvcMusicStore.mdf** from the dropdown, so it fills in the correct connection information for that database. The generated class will have the same name as the entity connection string, so change it to **MusicStoreEntities** and click **Next**.

 	![Choosing the data connection](./images/Choosing-the-data-connection.png?raw=true "Choosing the data connection")
 
	_Choosing the data connection_

1. Choose the database objects to use. Since the Entity Model will use just the database's tables, check the **Tables** checkbox and make sure that the **Include foreign key columns in the model** and **Pluralize or singularize generated object names** checkboxes are also checked. Change the Model Namespace to **MvcMusicStoreModel** and click **Finish**.

 	![Choosing the database objects](./images/Choosing-the-database-objects.png?raw=true "Choosing the database objects")
 
	_Choosing the database objects_

1. An entity diagram for the database appears. A separate class that maps to each table within the database will be created. For example, the **Albums** table will be represented by an **Album** class with each column in the table mapping to a property on the class. This will allow you to query and work with objects that represent rows within the database. You will see other classes that you might not use in the Hands-on Lab but belong to the Music Store application.

 	![Entity diagram](./images/Entity-diagram.png?raw=true "Entity diagram")
 
	_Entity diagram_

 
<a name="Ex1Task3" />
#### Task 3 - Building the Application ####

In this task, you will check that although you have removed the **Album** and **Genre** model classes, the project gets built successfully, by using the classes in the data model. 

1. Delete the placeholder **Album** and **Genre** classes. To do this, in the **Solution Explorer**, expand the **Models** folder, right-click **Album** and select **Delete**. Repeat this procedure with the **Genre** class.

 	![Deleting placeholder classes](./images/Deleting-placeholder-classes.png?raw=true "Deleting placeholder classes")
 
	_Deleting placeholder classes_

1. Build the project by selecting the **Debug** menu item and then **Build MvcMusicStore**.

 	![Building the project](./images/Building-the-project.png?raw=true "Building the project")
 
	_Building the project_

1. The project builds successfully. Why does still work? It works because the database tables have fields which include the properties you were using in the earlier **Album** and **Genre** classes manually removed. Data model classes are a drop-in replacement.

 	![Builds succeeded](./images/Builds-succeeded.png?raw=true "Builds succeeded")
 
	_Builds succeeded_

1. While the designer displays the entities in a diagram format, they are really C# classes. Expand the **StoreDB.edmx** node in the Solution Explorer, and you will see a file called **StoreDB.Designer.cs**.

 	![StoreDB.Designer.cs file](./images/StoreDB.Designer.cs-file.png?raw=true "StoreDB.Designer.cs file")
 
	_StoreDB.Designer.cs file_

 
<a name="Ex1Task4" />
#### Task 4 - Querying the Database ####

In this task, you will update the StoreController class so that instead of using hard-coded data, it queries the database to retrieve all its information. 

1. Open **Controllers\StoreController.cs** and add the following field to the class to hold an instance of the **MusicStoreEntities** class, named **storeDB**:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex1 storeDB_)

	<!-- mark:3-4 -->
	````C#
	public class StoreController : Controller
	{
	    MusicStoreEntities storeDB = new MusicStoreEntities();
	````

1. The **MusicStoreEntities** class exposes a collection property for each table in the database. Update **StoreController**'s **Index** action method to retrieve all **Genre** names in the database. This was done previously by hard-coding string data. Now you can instead write a LINQ query expression like below which retrieves the **Name** property of each Genre within the database:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex1 Store Index_)

	<!-- mark:6-9 -->
	````C#
	//
	// GET: /Store/
	
	public ActionResult Index()
	{
	    // Retrieve the list of genres
	    var genres = from genre in storeDB.Genres
	                 select genre.Name;
	
	    // Create your view model
	}
	````

	>**Note:** You are using a capability of .NET called **LINQ** (language-integrated query) to write strongly-typed query expressions against these collections - which will execute code against the database and return objects that you can program against.

	> For more information about LINQ, please visit the [msdn site](http://msdn.microsoft.com/en-us/library/bb397926&#040;v=vs.110&#041;.aspx).

1. Transform the collection of genres to a list. To do this, replace the following code:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex1 Store Index ToList_)

	<!-- mark:10-12 -->
	````C#
	public ActionResult Index()
	{
	    // Retrieve the list of genres
	    var genres = from genre in storeDB.Genres
	                 select genre.Name;
	
	    // Create your view model
	    var viewModel = new StoreIndexViewModel
	    {
	        Genres = genres.ToList(),
	        NumberOfGenres = genres.Count()
	    };
	
	    return View(viewModel);
	}
	````
 
<a name="Ex1Task5" />
#### Task 5 - Running the Application ####

In this task, you will check that the Store Index page will now display the Genres stored in the database instead of the hard-coded ones. There is no need of changing the View template because the **StoreController** is returning the same **StoreIndexViewModel** as before, although this time the data will come from the database. 

1. Press **F5** to run the Application.

1. The project starts in the Home page. Change the URL to **/Store** to verify that the list of **Genres** is no longer the hard-coded list, else the ones taken from the database.

	![BrowsingGenresFromDataBase](images/browsinggenresfromdatabase.png?raw=true)

	_Browsing Genres from the database_
 

<a name="Exercise2" />
### Exercise 2: Adding a Database Using Code First ###

In this exercise, you will learn how to use the Code First approach to add a database with the tables of the MusicStore application to consume its data.

Once adding the database and generating the model, you will make the proper adjustments in the StoreController to provide the View template with the data taken from the database instead of hardcoding it.

> **Note:** If you have completed Exercise 1 and have already worked with Database approach, you will now learn how to get the same results with a different process. Some tasks will be repeated with Exercise 1, so they are marked appropriately to make your reading easier. If you have not completed Exercise 1 but would like to learn the Code First approach, you can start from this exercise and get a full coverage of the topic.

<a name="Ex2Task1" />
#### Task 1 - Adding a Database ####

In this task, you will add an already created database with the main tables of the MusicStore application to the solution.

> **Note:** This task is in common with Exercise 1. 

1. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex02-AddingADatabaseCodeFirst\Begin, select **MvcMusicStore.sln** and click **Open**.

1.	Follow these steps to install the **NuGet** package dependencies.

	a.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	b.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	c.	After installing the package, type **Enable-PackageRestore**.

	d.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

	>**Note:** One of the advantages of using NuGet is that you don't have to ship all the libraries in your project, reducing the project size. With NuGet Power Tools, by specifying the package versions in the Packages.config file, you will be able to download all the required libraries the first time you run the project. This is why you will have to run these steps after you open an existing solution from this lab.
	
	>For more information, see this article: <http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages>.

1. Add an **App_Data** folder to the project to hold the SQL Server Express database files. **App_Data** is a special folder in ASP.NET which already has the correct security access permissions for database access. To add the folder, right-click **MvcMusicStore** project, point to **Add** then to **Add ASP.NET Folder** and finally click **App_Data**.

 	![Adding an AppData folder](./images/Adding-an-AppData-folder.png?raw=true "Adding an AppData folder")
 
	_Adding an App_Data folder_

1. Add **MvcMusicStore** database file. In this lab, you will use an already created database called **MvcMusicStore.mdf**. To do that, right-click the new **App_Data** folder, point to **Add** and then click **Existing Item**. Browse to **\Source\Assets\** and select the **MvcMusicStore.mdf** file.

 	![Adding an Existing Item](./images/Adding-an-Existing-Item.png?raw=true "Adding an Existing Item")
 
	_Adding an Existing Item_

 	![MvcMusicStore.mdf database file](./images/MvcMusicStore.mdf-database-file.png?raw=true "MvcMusicStore.mdf database file")
 
	_MvcMusicStore.mdf database file_

	The database has been added to the project. Even when the database is located inside the solution, you can query and update it as it was hosted in a different database server.

 	![MvcMusicStore database in Solution Explorer](./images/MvcMusicStore-database-in-Solution-Explorer.png?raw=true "MvcMusicStore database in Solution Explorer")
 
	_MvcMusicStore database in Solution Explorer_

1. Verify the connection to the database. To do this, open the **Database Explorer** (CTRL+ALT+S), and then double-click the **MvcMusicStore.mdf**. The connection is established.

 	![Connecting to MvcMusicStore.mdf](./images/Connecting-to-MvcMusicStore.mdf.png?raw=true "Connecting to MvcMusicStore.mdf")
 
	_Connecting to MvcMusicStore.mdf_

1. Close the connection now. To do that, in Database Explorer right-click on the MvcMusicStore database and select **Close Connection**.

 	![Closing the connection](./images/Closing-the-connection.png?raw=true "Closing the connection")
 
	_Closing the connection_

 
<a name="Ex2Task2" />
#### Task 2 - Configuring the connection to the Database ####

Now that we have already added a database to our project, we will write in the Web.config the connection string.

1. Add a connection string at **Web.config**. To do that, open **Web.config** at project root and replace the connection string named DefaultConnection with this line in the **&lt;connectionStrings&gt;** section:

 	![Web.config file location](./images/Web.config-file-location.png?raw=true "Web.config file location")
 
	_Web.config file location_

	<!-- mark:3-3 -->
	````XML
	<configuration>
	...
	  <connectionStrings>
		<add name="MusicStoreEntities" connectionString="data source=(LocalDb)\v11.0;initial catalog=MvcMusicStore;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\MvcMusicStore.mdf" providerName="System.Data.SqlClient" />
	  </connectionStrings>
	...
	
	````

 
<a name="Ex2Task3" />
#### Task 3 - Working with the Model ####

Now that we have already configured the connection to the database, we will link the model with the database tables. In this task, we will create a class that will be linked to the database with Code First. Remember we already have a POCO model class that should be modified.

> **Note:** If you completed Exercise 1, you will note that this step was performed by a wizard. By doing Code First, you will manually create classes that will be linked to data entities.



1. Open the POCO model class **Genre** from **Models** project folder and include an ID, a description attribute, and also an album's collection.

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 Code First Genre_)

	<!-- mark:10,12-14 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	
	namespace MvcMusicStore.Models
	{
	    public class Genre
	    {
	        public int GenreId { get; set; }
	        public string Name { get; set; }
	        public string Description { get; set; }
	        public virtual ICollection<Album> Albums { get; set; }
	    }
	}
	````

	> **Note:** To work with Code First conventions, Genre must have a primary key property that will be automatically detected.

	> You can read more about Code First Conventions in this [msdn article](http://msdn.microsoft.com/en-us/library/hh161541&#040;v=vs.103&#041;.aspx).

1. Now, open the POCO model class **Album** from **Models** project folder and include the AlbumId and GenreId properties.

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 Code First Album_)
	<!-- mark:10,12 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	
	namespace MvcMusicStore.Models
	{
	    public class Album
	    {
	        public int AlbumId { get; set; }
	        public string Title { get; set; }
	        public int GenreId { get; set; }
	        public virtual Genre Genre { get; set; }
	    }
	}
	````

1. Right-click the **Models** project folder point to **Add** and then click **New Item**. Under **Code** choose the **Class** item and name it **MusicStoreEntities.cs**, then click **Add.**

	![Adding a class](./images/Adding-a-class.png?raw=true "Adding a class")

	_Adding a new item_

 	![Adding a class2](./images/Adding-a-class2.png?raw=true "Adding a class2")
 
	_Adding a class_

1. Open the class you have just created, **MusicStoreEntities.cs**, and include the namespaces **System.Data.Entity** and **System.Data.Entity.Infrastructure**.

	<!-- mark:5-7 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	...
	````

1. Replace the class declaration to extend the **DbContext** class: declare a public **DBSet** and override **OnModelCreating** method. After this step you will get a domain class that will link your model with the Entity Framework. In order to do that, replace the class code with the following:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 Code First MusicStoreEntities_)

	<!-- mark:10-27 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	
	namespace MvcMusicStore.Models
	{
	    public class MusicStoreEntities : DbContext
	    {
	        public DbSet<Genre> Genres { get; set; }
	        public DbSet<Album> Albums { get; set; }
	
		    public MusicStoreEntities() 
		    {
				Database.SetInitializer<MusicStoreEntities>(null);
		    }

	        protected override void OnModelCreating(DbModelBuilder modelBuilder)
	        {
	            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
	            modelBuilder.Entity<Genre>().ToTable("Genre");
	            modelBuilder.Entity<Album>().ToTable("Album");
	            base.OnModelCreating(modelBuilder);
	        }
	    }
	}
	````

	> **Note:** With Entity Framework **DbContext** and **DBSet** you will be able to query the POCO class Genre. By extending OnModelCreating we are specifying in the **code** how Genre will be mapped to a database table. You can find more information about DBContext and DBSet in this msdn article: [link](http://msdn.microsoft.com/en-us/library/system.data.entity.dbcontext&#040;v=vs.103&#041;.aspx)

	> **Note:** By default, entity framework will try to create the database, in order to avoid that, we configure the database initializer in the **MusicStoreEntities** constructor to a null value. This way, no database initialization will be executed.

 
<a name="Ex2Task4" />
#### Task 4 - Querying the Database ####

In this task, you will update the StoreController class so that instead of using hard-coded data, it can consume it from the database.

> **Note:** This task is in common with Exercise 1.

> If you completed Exercise 1 you will note these steps are the same, independently from the approach (Database first or Code first). They are different in how the data is linked with the model, but the access to data entities has to be the transparent from the controller.

1. Open **Controllers\StoreController.cs** and add the following field to hold an instance of the **MusicStoreEntities** class, named **storeDB**:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 Code First storeDB_)

	<!-- mark:3-4 -->
	````C#
	public class StoreController : Controller
	{
	    MusicStoreEntities storeDB = new MusicStoreEntities();
	````

1. The **MusicStoreEntities** class exposes a collection property for each table in the database. Update **StoreController**'s **Index** action to retrieve all **Genre** names in the database. This was done previously by hard-coding string data. Now you can instead write a LINQ query expression like the one below which retrieves the **Name** property of each Genre within the database:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 code First Store Index_)

	<!-- mark:6-9 -->
	````C#
	//
	// GET: /Store/
	
	public ActionResult Index()
	{
	    // Retrieve the list of genres
	    var genres = from genre in storeDB.Genres
	                 select genre.Name;
	
	    // Create your view model
	}
	````

	> **Note:** You are using a capability of .NET called **LINQ** (language-integrated query) to write strongly-typed query expressions against these collections - which will execute code against the database and return objects that you can program against. 

	> For more information about LINQ, please visit the [msdn site](http://msdn.microsoft.com/en-us/library/bb397926&#040;v=vs.110&#041;.aspx).

1. Transform the collection of genres to a list. To do this, replace the following code:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex2 Code First Genres to List_)

	<!-- mark:10-12 -->
	````C#
	public ActionResult Index()
	{
	    // Retrieve the list of genres
	    var genres = from genre in storeDB.Genres
	                 select genre.Name;
	
	    // Create your view model
	    var viewModel = new StoreIndexViewModel
	    {
	        Genres = genres.ToList(),
	        NumberOfGenres = genres.Count()
	    };
	
	    return View(viewModel);
	}
	````

 
<a name="Ex2Task5" />
#### Task 5 - Running the Application ####

In this task, you will check that the Store Index page will now display the Genres stored in the database instead of the hard-coded ones. There is no need of changing the View template because the **StoreController** is returning the same **StoreIndexViewModel** as before, although this time the data will come from the database. 

1. Press **F5** to run the Application.

1. The project starts in the Home page. Change the URL to **/Store** to verify that the list of **Genres** is no longer the hard-coded list, else the ones taken from the database.

	![browsinggenresfromdatabase](images/browsinggenresfromdatabase.png?raw=true)

	_Browsing Genres from the database_
 

<a name="Exercise3" />
### Exercise 3: Querying the Database with Parameters ###

 In this exercise, you will learn how to query the database using parameters and how to use the Query Result Shaping, a feature that reduces the number of accesses to the database to retrieve data in a more efficient way.

> **Note:** For further information on Query Result Shaping, visit the following [msdn article](http://msdn.microsoft.com/en-us/library/bb896272&#040;v=vs.100&#041;.aspx).

<a name="Ex3Task1" />
#### Task 1 - Modifying StoreController to Retrieve Albums from Database ####

In this task, you will change the **StoreController** class to access the database to retrieve albums from a specific genre.

1. If not already open, start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex03-QueryingTheDatabaseWithParametersDBFirst\Begin (or Ex03-QueryingTheDatabaseWithParametersCodeFirst\Begin if you want to use a Code First approach), select **MvcMusicStore.sln** and click **Open**. Alternatively, you may continue with the solution that you obtained after completing any of the previous exercises.

1.	Follow these steps to install the **NuGet** package dependencies.

	a.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	b.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	c.	After installing the package, type **Enable-PackageRestore**.

	d.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

	>**Note:** One of the advantages of using NuGet is that you don't have to ship all the libraries in your project, reducing the project size. With NuGet Power Tools, by specifying the package versions in the Packages.config file, you will be able to download all the required libraries the first time you run the project. This is why you will have to run these steps after you open an existing solution from this lab.
	
	>For more information, see this article: <http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages>.

1. Open the **StoreController** class to change the **Browse** action method. To do this, in the **Solution Explorer**, expand the **Controllers** folder and double-click **StoreController.cs**.

1. Change the **Browse** action method to retrieve albums for a specific genre. To do this, replace the following code:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex3 StoreController BrowseMethod_)

	<!-- mark:6-16 -->
	````C#
	//
	// GET: /Store/Browse?genre=Disco
	
	public ActionResult Browse(string genre)
	{
	    // Retrieve Genre and its Associated Albums from database
	
	    var genreModel = storeDB.Genres.Include("Albums")
	        .Single(g => g.Name == genre);
	
	    var viewModel = new StoreBrowseViewModel()
	    {
	        Genre = genreModel,
	        Albums = genreModel.Albums.ToList()
	    };
	
	    return View(viewModel);
	}
	````

	> **Note:** You can use the .**Single()** extension in LINQ because in this case only one genre is expected for an album. The **Single()** method takes a Lambda expression as a parameter, which in this case specifies a single Genre object such that its name matches the value defined.

	> **Note:** You will take advantage of a feature that allows you to indicate other related entities you want loaded as well when the Genre object is retrieved. This feature is called **Query Result Shaping**, and enables you to reduce the number of times needed to access the database to retrieve information. In this scenario, you will want to pre-fetch the Albums for the Genre you retrieve.

	> The query includes **Genres.Include("Albums")** to indicate that you want related albums as well. This will result in a more efficient application, since it will retrieve both Genre and Album data in a single database request.

 
<a name="Ex3Task2" />
#### Task 2 - Running the Application ####

In this task, you will try out the Application in a web browser and obtain albums for a specific genre from the database.

1. Press **F5** to run the Application.

1. The project starts in the Home page. Change the URL to **/Store/Browse?genre=Jazz** to verify that the results are being pulled from the database.

 	![Browsing StoreBrowsegenre=Jazz](./images/Browsing-StoreBrowsegenre=Jazz.png?raw=true "Browsing StoreBrowsegenre=Jazz")
 
	_Browsing /Store/Browse?genre=Jazz_

 
<a name="Ex3Task3" />
#### Task 3 - Accessing Albums by Id ####

In this task, you will repeat the previous procedure, in this case, to obtain albums by Id.

1. Close the browser if needed, to return to Visual Studio. Open the **StoreController** class to change the **Details** action method. To do this, in the **Solution Explorer**, expand the **Controllers** folder and double-click **StoreController.cs**.

1. Change the **Details** action method to retrieve albums details based on their **Id**. To do this, replace the following code:

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex3 StoreController DetailsMethod_)
	<!-- mark:6-7 -->
	````C#
	//
	// GET: /Store/Details/5
	
	public ActionResult Details(int id)
	{
	    var album = storeDB.Albums.Single(a => a.AlbumId == id);
	
	    return View(album);
	}
	````

 
<a name="Ex3Task4" />
#### Task 4 - Running the Application ####

In this task, you will try out the Application in a web browser and obtain album details based on its Id.

1. Press **F5** to run the Application.

1. The project starts in the Home page. Change the URL to **/Store/Details/500** to verify that the results are being pulled from the database.

 	![Browsing StoreDetails500](./images/Browsing-StoreDetails500.png?raw=true "Browsing StoreDetails500")
 
	_Browsing /Store/Details/500_

<a name="Exercise4" />
### Exercise 4 - Using Asynchronous Controllers###

Microsoft .NET Framework 4.5 introduces new language features in to provide a new foundation for asynchrony in .NET programming. This new foundation makes asynchronous programming similar to - and about as straightforward as - synchronous programming.
You are now able to write asynchronous action methods in ASP.NET MVC 4 by using the **AsyncController** class. You can use asynchronous action methods for long-running, non-CPU bound requests. This avoids blocking the Web server from performing work while the request is being processed. The AsyncController class is typically used for long-running Web service calls.

In this exercise, you will implement an Async controller
to query the database asynchronously.

<a name="Ex4Task1" />
#### Task 1: Modifying the Home controller to perform an asynchronous query operation ####

1. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1. Open the **MVCMusicStore.sln** solution located in **Source\Ex4-Async\Begin** from this lab’s folder. Alternatively, you can continue working with the solution obtained in the previous exercise.

1.	Follow these steps to install the **NuGet** package dependencies.

	a.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	b.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	c.	After installing the package, type **Enable-PackageRestore**.

	d.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

	>**Note:** One of the advantages of using NuGet is that you don't have to ship all the libraries in your project, reducing the project size. With NuGet Power Tools, by specifying the package versions in the Packages.config file, you will be able to download all the required libraries the first time you run the project. This is why you will have to run these steps after you open an existing solution from this lab.
	
	>For more information, see this article: <http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages>.

	> **Note:** By enabling the package restore, Visual Studio will automatically download the missing packages when the solution compiles for the first time.

1. Open the **StoreController** class within **Controllers** folder. 

1. Add the following namespace declarations to import the types contained in **System.Collection.ObjectModel** and **System.Threading.Tasks** namespaces.

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex4 Namespace Declarations_)

	<!-- mark:1-2 -->
	````C#
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;
	````
	
1. In the **StoreController** class, locate the **Index** Action Method. Add the **async** keyword before the return type and make it return the type **Task\<ActionResult\>**.

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex4 Async method_)

	<!-- mark:1 -->
	````C#
	public async Task<ActionResult> Index()
	````

	> **Note:** By adding the **async** keyword, you indicate the compiler that the method contains asynchronous code.

1. In the **Index** method, replace the **genres** variable declaration with the following code.
	
	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex4 GenresDeclaration_)

	<!-- mark:1 -->
	````C#	
	var genres = new List<string>();
	````

1. Add a new **Task** for retreiving the genres' names using the **await** keyword before the task call. To do this, insert the highlighted code after the **genres** declaration.

	(Code Snippet - _ASP.NET MVC 4 Models and Data Access - Ex4 Await Task_)
	
	<!-- mark:5-10 -->
	````C#
	public async Task<ActionResult> Index()
	{     
		var genres = new List<string>();

		await Task.Run(() => {
			 var result = (from genre in storeDB.Genres
							  select genre.Name);

			 genres = result.ToList();
		});
	````
	
	> **Note:** By adding the **await** keyword before the task call, you are telling the compiler to asynchronously wait for the task returned from the method call.

	The complete **Index** method should look like the following code:

	````C#
	public async Task<ActionResult> Index()
	{
		var genres = new List<string>();

		await Task.Run(() => {
			 var result = (from genre in storeDB.Genres
							  select genre.Name);

			 genres = result.ToList();
		});

		var viewModel = new StoreIndexViewModel
		{
			 Genres = genres,
			 NumberOfGenres = genres.Count()
		};                
						
		return View(viewModel);
	}
	````

---

<a name="Summary" />
## Summary ##

By completing this Hands-On Lab you have learned the fundamentals of ASP.NET MVC Models and Data Access, using a **Database First** approach:

- How to add a database to the solution in order to consume its data

- How to update Controllers to provide View templates with the data taken from the database instead of hard-coded one

- How to query the database using parameters

- How to use the Query Result Shaping, a feature that reduces the number of accesses to the database to retrieve data in a more efficient way

- How to use both Database First and Code First approaches in Microsoft Entity Framework to link the database with the model

- How to implement an Async controller to query the database asynchronously