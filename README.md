# ApiGamesCatalog
## DIO GTF START 2 .NET Bootcamp
Challenge: create an API to register games, with good code pratices.
### Improvements over base project
- Added a parameter on the get that paginates, which allows to choose the column to order the list


### Script to create the required database and table in SQL Server

```
create database Games; 
use Games;
CREATE TABLE games
(
	  Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	  Name NVARCHAR(100),
	  Producer NVARCHAR(100),
	  Price FLOAT
)

```
