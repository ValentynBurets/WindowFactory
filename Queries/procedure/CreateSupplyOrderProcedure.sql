
DROP Type ItemList

CREATE TYPE ItemList AS TABLE
(
    Id int Identity(1, 1) PRIMARY KEY,
    ItemId int
)
Go

DROP PROCEDURE IF EXISTS dbo.CreateSupplyOrder 
GO

CREATE PROCEDURE dbo.CreateSupplyOrder 
	@Items ItemList READONLY,
	@ManagerId int
AS  
	DECLARE @ManagerTypeId int, @res bit
	SELECT @ManagerTypeId = Id FROM WorkerType WHERE WorkerType.Name = 'manager'
	--SELECT @EngeneerTypeId
	
	SET @res = dbo.WorkerIs(@ManagerTypeId, @ManagerId)

	IF(dbo.WorkerIs(@ManagerTypeId, @ManagerId) = 0)
	BEGIN
		PRINT 'Wrong ManagerId'  
		RETURN
	END

	DECLARE @CurrentOrderNumber int, @CurrentOrderId int

	SELECT @CurrentOrderNumber = MAX(SupplyOrder.OrderNumber) + 1
	FROM SupplyOrder

	IF NOT(ISNUMERIC(@CurrentOrderNumber) = 1)
	BEGIN
		SET @CurrentOrderNumber = 0
	END

	INSERT INTO SupplyOrder
	(OrderNumber, ManagerId)
	VALUES
	(@CurrentOrderNumber, @ManagerId)

	SELECT @CurrentOrderId = Id
	FROM SupplyOrder AS s
	WHERE s.OrderNumber = @CurrentOrderNumber

DECLARE @ItemId int

DECLARE OrderCursor CURSOR FOR
SELECT
ItemId
FROM @Items

OPEN OrderCursor

FETCH NEXT FROM OrderCursor
INTO
@ItemId

WHILE @@FETCH_STATUS = 0 
BEGIN

	UPDATE SupplyItem
	SET SupplyItem.SypplyOrderId = @CurrentOrderId
	WHERE SupplyItem.Id = @ItemId

END
FETCH NEXT FROM OrderCursor
INTO
@ItemId

CLOSE OrderCursor
DEALLOCATE OrderCursor

----END OF Procedure

SELECT * 
FROM vWorker

SELECT * 
FROM WorkerType

SELECT *
FROM SupplyItem
