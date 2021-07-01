# ApiGamesCatalog
## DIO GTF START 2 .NET Bootcamp

### Script to use SQL Server dabatase

create database Games; <br>
use Games; <br>

```
CREATE TABLE games
(
	  Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	  Name NVARCHAR(100),
	  Producer NVARCHAR(100),
	  Price FLOAT
)<br>

```
