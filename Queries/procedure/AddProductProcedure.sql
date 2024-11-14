DROP PROCEDURE IF EXISTS dbo.AddProduct 
GO

CREATE PROCEDURE dbo.AddProduct
	@OrderNumber int,
	--Engeneer Data
	@EngeneerId int,
	--Product Data
	@ColorId int,
	@Height int,
	@Width int,

	@ProfileId int,
	@GlassId int,
	@FurnitureId int
	--
AS

	DECLARE @EngeneerTypeId int, @res bit
	SELECT @EngeneerTypeId = Id FROM WorkerType WHERE WorkerType.Name = 'engineer'
	--SELECT @EngeneerTypeId
	
	SET @res = dbo.WorkerIs(@EngeneerTypeId, @EngeneerId)

	IF(dbo.WorkerIs(@EngeneerTypeId, @EngeneerId) = 0)
	BEGIN
		PRINT 'Wrong EngeneerId'  
		RETURN
	END

	DECLARE @ProductId int
	SELECT @ProductId = MAX(Id) + 1
	FROM Product

	PRINT ISNUMERIC(@ProductId)
	IF NOT(ISNUMERIC(@ProductId) = 1)
		SET @ProductId = 1

		PRINT @ProductId

	PRINT 'INSERT VALUES(Id, ColorId, OrderId, EngineerId, Height, Width, DateComplite, Price)'
	SET IDENTITY_INSERT Product ON
	INSERT INTO Product
	(Id, ColorId, OrderId, EngineerId, Height, Width)
	VALUES(
		@ProductId,
		@ColorId, 
		@OrderNumber, 
		@EngeneerId,
		@Height,
		@Width
	)
	
	PRINT 'INSERT VALUES(Id, MaterialId, ProductId, [Date], Quantity)'

	INSERT INTO MaterialList
	(MaterialId, ProductId)
	VALUES(
		@FurnitureId,
		@ProductId
	)

	INSERT INTO MaterialList
	(MaterialId, ProductId)
	VALUES(
		@GlassId,
		@ProductId
	)

	INSERT INTO MaterialList
	(MaterialId, ProductId)
	VALUES(
		@ProfileId,
		@ProductId
	)
	PRINT 'Complited'
GO


	--@OrderNumber int,
	--@EngeneerId int,
	----Product Data
	--@ColorId int,
	--@Height int,
	--@Width int,

	--@ProfileId int,
	--@GlassId int,
	--@FurnitureId int

EXEC dbo.AddProduct 2, 3, 21, 100, 200, 1, 2, 3

GO
SELECT * FROM vWorker

GO
SELECT * FROM vMaterial

GO
SELECT * FROM Product

DELETE Product
WHERE Id = 2

GO
SELECT * FROM Color

GO
SELECT * FROM [Order]

SELECT * FROM MaterialList

	SET IDENTITY_INSERT Product ON
	INSERT INTO Product
	(Id, ColorId, OrderId, EngineerId, Height, Width)
	VALUES(
		1,
		22, 
		2, 
		3,
		100,
		50
	)