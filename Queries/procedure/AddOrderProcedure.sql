DROP PROCEDURE IF EXISTS dbo.AddOrder 
GO
CREATE PROCEDURE dbo.AddOrder 
	--Customer Data
	@CustomerId nvarchar(50), 
	--Manager Data
	@ManagerId int
	--

AS

	DECLARE @ManagerTypeId int, @res bit
	SELECT @ManagerTypeId = Id FROM WorkerType WHERE WorkerType.Name = 'manager'
	--SELECT @ManagerTypeId
	
	SET @res = dbo.WorkerIs(@ManagerTypeId, @ManagerId)

	IF(dbo.WorkerIs(@ManagerTypeId, @ManagerId) = 0)
	BEGIN
		PRINT 'Wrong ManagerId'  
		RETURN
	END

	DECLARE @CurrentOrderNumber int

	SELECT @CurrentOrderNumber = MAX(OrderNumber) + 1
	FROM [Order]

	IF NOT(ISNUMERIC(@CurrentOrderNumber) = 1)
		SET @CurrentOrderNumber = 1

	--VALUES(Id, OrderNumber, CustomerId, ManagerId, TotalPrice, Paid, DateCreate, DateComplited, ApproximateDateComplite)
	INSERT INTO [Order]
	(CustomerId, OrderNumber, ManagerId)
	VALUES(
		@CustomerId,
		@CurrentOrderNumber,
		@ManagerId)


EXEC dbo.AddOrder 1, 5

SELECT *
FROM Orders

SELECT dbo.WorkerIs(2, 1)

SELECT *
FROM WorkerType

SELECT *
FROM Worker

DELETE FROM Orders
WHERE ManagerId = 1