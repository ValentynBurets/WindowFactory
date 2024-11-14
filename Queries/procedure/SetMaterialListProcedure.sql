DROP PROCEDURE IF EXISTS dbo.SetMaterialList 
GO

CREATE PROCEDURE dbo.SetMaterialList 
	@ProductId int,
	@MaterialId int,
	@Quantity int
AS  
BEGIN TRAN 

	DECLARE @CurrentQuantity int
	SELECT @CurrentQuantity = Quantity 
	FROM Storage as S
	WHERE S.MaterialId = @MaterialId

	IF(@Quantity > @CurrentQuantity)
	BEGIN
		PRINT 'Not enough material in the storage'
		ROLLBACK TRAN  
		RETURN 0
	END

	-------
	DECLARE @MaterialListId int

	SELECT @MaterialListId = Id
	FROM MaterialList
	WHERE ProductId = @ProductId AND
		  MaterialId = @MaterialId

	PRINT 'Material Id'
	PRINT @MaterialListId

	IF (ISNUMERIC(@MaterialListId) = 1)
		BEGIN
			PRINT 'UP DATE Material Quantity in Material List'
			UPDATE MaterialList
			SET Quantity = @Quantity
			WHERE MaterialId = @MaterialId AND
				  ProductId = @ProductId
		END
	ELSE
		BEGIN	
			PRINT 'Insert Materials in Material List'
			--Id, ColorId, OrderId, EngineerId, Height, Width, DateComplite, Price
			INSERT INTO MaterialList
			(MaterialId, ProductId, Quantity)
			VALUES(
				@MaterialId,
				@ProductId,
				@Quantity
			)
			PRINT 'Update Storage'
			UPDATE Storage
			SET Quantity = Quantity - @Quantity 
			WHERE Storage.MaterialId = @MaterialId

			PRINT 'Complited'
		END
	
	SELECT @CurrentQuantity = Quantity  FROM Storage WHERE MaterialId = @MaterialId
	IF( @CurrentQuantity > 0)
		COMMIT TRAN
	ELSE
		ROLLBACK
	
GO;


--tests 

	--@ProductId int,
	--@MaterialId int,
	--@Quantity int

EXEC dbo.SetMaterialList 1, 1, 2
EXEC dbo.SetMaterialList 1, 2, 2
EXEC dbo.SetMaterialList 1, 3, 2

SELECT *
FROM MaterialList

--DELETE MaterialList
--WHERE ProductId = 1

SELECT *
FROM Product

SELECT *
FROM vMaterial


INSERT INTO MaterialList
	(MaterialId, ProductId, Quantity)
	VALUES(
		2,
		1,
		3
	)