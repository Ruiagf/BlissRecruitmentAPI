# Bliss Recruitment API


### Prerequisites

In order to run, the application requires that local machine has the .NET 4.7 Framework installed.

Additionally, it requires access to a Microsoft SQL Server instance.

### Installing

To install the application in a local development environment, follow these steps:

1. Access local SQL Server instance and create a new database (e.g. BlissRecruitmentDB) If you prefer, run script '01.Database.sql', present on SQL Scripts folder.
2. Run all other scripts on SQL Scripts folder, namely: '02.Tables.sql', '03.Inserts.sql' and '04.StoredProcedures.sql'.
3. Open the solution with Visual Studio and edit web.config file to add connection string information for local server
```
<add name="DefaultConnection" connectionString="{connectionString goes here}" providerName="System.Data.SqlClient" />
```

4. If you want to test the e-mail sending feature of Share, don't forget to fill mailSettings on web.config 
5. You're ready to go


## Authors

* **Rui Freitas**
