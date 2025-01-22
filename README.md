# Results of recruitment test task
## This project implements a service for managing equipment placement contracts in production facilities. The solution includes a database deployed with Entity Framework Core, API endpoints, and a user interface for managing contracts.
### After creating a migration ProductionFacilities and TypesOfProcessEquipment tables will be created and filled with example data using EntityFrameworkCore:
#### ProductionFacility table:
![Reference Image](/Screenshots/ProductionFacilityTable.PNG)

#### ProcessEquipmentType table:
![Reference Image](/Screenshots/ProcessEquipmentTypeTable.png)

#### To connect the application to your database engine, update the *Server Name* in the *ConnectionString* located in appsettings.json:

    "ConnectionStrings": {
    "DefaultConnection":"Server=YourServerName;Database=EquipmentDb;Trusted_Connection=True;Command Timeout=300;
      MultipleActiveResultSets=true;TrustServerCertificate=true;Integrated Security=True;"}

### Replace YourServerName with the name of your SQL Server instance.

### Adding a New Contract
#### To add a new contract:

    -Navigate to the application interface.
    -Click the Add Contract button at the top of the page.
    -Fill in the required details:
        *Production Facility Code
        *Process Equipment Type Code
        *Equipment Quantity
    -Submit the form.

#### The application will:

    -Validate the input fields.
    -Check if the selected production facility has enough available area.
    -Add the contract to the EquipmentPlacementContract table if all validations pass.