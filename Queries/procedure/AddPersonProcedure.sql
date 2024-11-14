DROP PROCEDURE IF EXISTS dbo.AddPerson
GO
CREATE PROCEDURE dbo.AddPerson
(
	@PersonName nvarchar(50), 
	@PersonSurName nvarchar(50),
	@PersonPhoneNumber nvarchar(10),
	@PersonEmail nvarchar(50)
)
AS
BEGIN
	
	DECLARE @PersonId int = 0
	DECLARE @uperr int  
	DECLARE @maxerr int  

	SELECT @PersonId = Person.Id
				FROM Person
				WHERE @PersonName = Person.Name AND
				@PersonSurName = Person.Surname

	IF (@PersonId != 0)
		BEGIN
			
			IF EXISTS (SELECT *
				FROM Person
				WHERE @PersonName = Person.Name AND
					@PersonSurName = Person.Surname AND
					@PersonPhoneNumber = Person.PhoneNumber AND
					@PersonEmail = Person.Email)
				BEGIN
					PRINT 'Person exist in table'  
					RETURN @PersonId
				END
			ELSE
				PRINT 'Person exist in table Try update person data. ' +  CHAR(13) + ' CustomerId = ' + CAST(@PersonId AS nvarchar);  
			
				BEGIN TRAN
					
					UPDATE Person
					SET PhoneNumber = @PersonPhoneNumber,
						Email = @PersonEmail
					WHERE @PersonId = Person.Id

					BEGIN
						SET @uperr = @@error  
						IF @uperr > @maxerr  
						SET @maxerr = @uperr  
  
						-- If an error occurred, roll back  
						IF @maxerr <> 0  
							BEGIN  
								ROLLBACK  
								PRINT 'did bot updated ' +  CHAR(13) + ' Transaction rolled back'  
							END
						ELSE  
							BEGIN  
								COMMIT  
								PRINT 'updated successfully ' +  CHAR(13) + ' Transaction committed'  
							END
					END

					RETURN @PersonId
		END
			
	ELSE
		BEGIN 
		--Person don't exist in table
		--Insert person

		PRINT 'Person did not exist in table ' +  CHAR(13) + ' Try to Insert in table'  

		BEGIN TRAN

		DECLARE @InsertedPersonId int = (SELECT MAX(Id) + 1 FROM Person)		

		INSERT INTO Person
		(Id, [Name], Surname, PhoneNumber, Email)
		VALUES (
			@InsertedPersonId,
			@PersonName, 
			@PersonSurName,
			@PersonPhoneNumber,
			@PersonEmail
			)

		BEGIN
			SET @uperr = @@error  
			IF @uperr > @maxerr  
			SET @maxerr = @uperr  
  
			-- If an error occurred, roll back  
			IF @maxerr <> 0  
				BEGIN  
					ROLLBACK  
					PRINT 'did bot updated ' +  CHAR(13) + ' Transaction rolled back'  
				END
			ELSE  
				BEGIN  
					COMMIT  
					PRINT 'updated successfully ' +  CHAR(13) + ' Transaction committed'  
				END
		END

		RETURN @InsertedPersonId

	END
END

--tests
--PRINT dbo.AddCustomer('Valentyn', 'Ivanov', '0943573523', 'Ivano3v@gmail.com')

DECLARE  @returnId int  
EXEC @returnId= dbo.AddPerson 'Sergey', 'Petrov', '09433332544', 'Serg@gmail.com'  
SELECT @returnId;  

SELECT * FROM Person

INSERT INTO Person
		(Id, [Name], Surname, PhoneNumber, Email)
		VALUES ( 7, 'Valentyn', 'Petrov', '0943334523', 'PetroV@gmail.com' )


SELECT *
FROM Person