# Paylocity-Interview

I decided to do a full-stack solution to showcase my C#/.NET/SQL skills as well as front end with ReactJS for technologies.
However, I do believe I got a little overambitious in my approach, and in order to implement a working solution in time, did not have time to  implement features such as having different benefit election types (401k, health, etc), as well as being able to delete the Employee/Dependent or update their names.
Put some stub implementation there, would implement if given more time.

Project is in a Visual Studio solution, with two projects as a Web and Class Library project (for seperation of concerns to data access layer)

The project relies on a SQL server backend with a LocalDB. Connect as a Database Engine, to your localDb ((localdb)\MSSQLLOCALDB), with default Windows Authentication (if using SSMS) and create a database in your localDB with the name "testDB"

This database needs two tables, named dbo.Employee and dbo.EmployeeDependent

Employee schema:
Id (PK, uniqueidentifier, not null)
FirstName (nvarchar(320), null)
LastName (nvarchar(320), null)

EmployeeDependent schema
Id (PK, uniqueidentifier, not null)
FirstName (nvarchar(320), null)
LastName (nvarchar(320), null)
EmployeeId (FK, uniqueidentifier, not null)

Employee to EmployeeDependent has a 1-to-many relationship with Employee. Foreign key constraint on Employee(Id) and EmployeeDependent(EmployeeId)

Once that is done, Open EmployeeDependents.sln in EmployeeDependents folder using Visual Studio (Ver 2022 recommended).
Build project and run project (may ask about client side certificates, say yes to those).

Once running the web app, you can add Employees/Employee dependents, and see their calculated deductions as per the business rules.
Let me know if any issues getting it up and running. Thanks for looking and consideration!
