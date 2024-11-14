DROP PROCEDURE IF EXISTS dbo.AddToSupplyItem 
GO

CREATE PROCEDURE dbo.AddToSupplyItem 
	@MaterialId int,
	@QuantityNeed int
AS  
	DECLARE @SupplyItemId int
	SELECT @SupplyItemId = Id 
	FROM SupplyItem as S
	WHERE S.MaterialId = @MaterialId

	IF(ISNUMERIC(@SupplyItemId) = 1)
		BEGIN
			PRINT 'Updated row SupplyItem' 
			UPDATE SupplyItem
			SET Quantity = Quantity + @QuantityNeed
		END
	ELSE
		BEGIN
			PRINT 'Added row into SupplyItem'
			INSERT INTO SupplyItem
				(Quantity, MaterialId)
			VALUES
				(@QuantityNeed, @MaterialId)
		END
GO;


--tests 

	--@MaterialId int,
	--@Quantity int

EXEC dbo.AddToSupplyItem 3, 2

GO
SELECT *
FROM Product

GO
SELECT *
FROM vMaterial

GO 
SELECT *
FROM SupplyItem