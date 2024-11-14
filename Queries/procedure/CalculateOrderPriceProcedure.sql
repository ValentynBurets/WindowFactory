DROP PROCEDURE IF EXISTS dbo.CalculateOrderPrice 
GO

CREATE PROCEDURE dbo.CalculateOrderPrice 
	@OrderId int
AS  
	DECLARE @TotalPrice decimal = 0--, @ProductId int = 1 
	
	SELECT @TotalPrice = @TotalPrice + p.Price
	FROM Product AS p
	WHERE p.OrderId = @OrderId

	PRINT 'Total price of Order is'
	PRINT @TotalPrice
	--SELECT * FROM vMaterial

	IF(@TotalPrice != 0)
		BEGIN
			PRINT 'TotalPrice is correct Update price in table Order'
			UPDATE [Order]
			SET TotalPrice = @TotalPrice
			WHERE [Order].Id = @OrderId
		END
	ELSE
		BEGIN
			PRINT 'TotalPrice calculated incorrectly'
			RETURN
		END
GO;

EXEC dbo.CalculateOrderPrice 2 

SELECT * FROM Product