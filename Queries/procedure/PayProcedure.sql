DROP PROCEDURE IF EXISTS dbo.Pay 
GO
CREATE PROCEDURE dbo.Pay 
	@PersonId int, 
	@OrderId int,
	@Paid decimal
AS
	BEGIN TRAN 

	If NOT EXISTS(SELECT * FROM [Order] WHERE [Order].Id = @OrderId)
	BEGIN
		PRINT 'Order not found'
		ROLLBACK
		RETURN
	END

	BEGIN TRY 
		
		PRINT 'insert new payment'		
		INSERT INTO Payment
		(OrderId, PayerId, Paid)
		VALUES (@OrderId, @PersonId, @Paid)

		DECLARE @CurrentPaid decimal

		SELECT @CurrentPaid = Paid 
		FROM [Order]
		WHERE [Order].Id = @OrderId

		IF NOT(ISNUMERIC(@CurrentPaid) = 1)
		BEGIN
			UPDATE [Order]
			SET Paid = 0
		END

		PRINT 'Udate payment in Order table'
		UPDATE [Order]
		SET Paid = Paid + @Paid
		WHERE [Order].Id = @OrderId

		COMMIT TRAN

  END TRY

  BEGIN CATCH

	  PRINT 'Cannot add new payment'
      ROLLBACK 

  END CATCH  

  --end of procedure

GO;


--tests

	--@PersonId int, 
	--@OrderId int,
	--@Paid decimal

dbo.Pay 1, 2, 100 

INSERT INTO Payment
(OrderId, PayerId, Paid)
VALUES (3, 3, 100)

DELETE FROM Payment
WHERE OrderId = 1

SELECT*
FROM Payment

DELETE Payment

UPDATE [Order]
SET Paid = 0

SELECT * 
FROM [Order] 
WHERE [Order].Id = 2
