# Results of recruitment test task
### Production facilities and Types of process equipment tables were filled with example data using EntityFrameworkCore:

#### ProductionFacility table:
![Reference Image](/Screenshots/ProductionFacilityTable.PNG)

#### ProcessEquipmentType table:
![Reference Image](/Screenshots/ProcessEquipmentTypeTable.PNG)

#### After creating migration using:
    -dotnet ef migrations add YourMigrationName
    -dotnet ef database update
#### Database will be deployed and created in MS SQL Server. In order to establish a connection between aplication and database engine it is necesarry to change *Server Name* in *ConnectionString*:

    "ConnectionStrings": {
    "DefaultConnection":"Server=YourServerName;Database=EquipmentDb;Trusted_Connection=True;Command Timeout=300;
      MultipleActiveResultSets=true;TrustServerCertificate=true;Integrated Security=True;"}


#### EquipmentPlacementContract table is empty by default. In order to add new contract to the table it is necessary to click button *Add Contract* on the top of the page.