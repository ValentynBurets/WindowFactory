DROP PROCEDURE IF EXISTS dbo.CalculateProductPrice 
GO

CREATE PROCEDURE dbo.CalculateProductPrice 
	@ProductId int
AS  
	DECLARE @TotalPrice decimal = 0--, @ProductId int = 1 
	
	SELECT @TotalPrice = @TotalPrice + s.Price
	FROM Storage AS s
	WHERE s.Id IN (
		SELECT MaterialId
		FROM MaterialList
		WHERE MaterialList.ProductId = @ProductId
		)

	PRINT 'Total price is'
	PRINT @TotalPrice
	--SELECT * FROM vMaterial

	IF(@TotalPrice != 0)
		BEGIN
			PRINT 'TotalPrice is correct Update price in table Product'
			UPDATE Product
			SET Price = @TotalPrice
			WHERE Product.Id = @ProductId
		END
	ELSE
		BEGIN
			PRINT 'TotalPrice calculated incorrectly'
			RETURN
		END
GO;

EXEC dbo.CalculateProductPrice 1 

SELECT * FROM Product