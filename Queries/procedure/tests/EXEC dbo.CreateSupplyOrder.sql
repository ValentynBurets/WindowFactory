CREATE TYPE ItemList AS TABLE
(
    Id int Identity(1, 1) PRIMARY KEY,
    ItemId int
)
Go
DECLARE @CarTableType CarTableType

INSERT INTO @CarTableType VALUES (1, 'Corrolla', 'Toyota')

DECLARE @Items ItemList

INSERT INTO @Items
VALUES
(1),
(2),
(3)

GO
	--@Items ItemList READONLY,
	--@ManagerId int
EXEC dbo.CreateSupplyOrder  @Items, 4 

