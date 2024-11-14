DROP PROCEDURE IF EXISTS AddWorker
GO
CREATE PROCEDURE dbo.AddWorker
(
	@PersonName nvarchar(50), 
	@PersonSurName nvarchar(50),
	@PersonPhoneNumber nvarchar(10),
	@PersonEmail nvarchar(50),
	@WorkerType nvarchar(50),
	@PassWord nvarchar(50)
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @uperr int  
	DECLARE @maxerr int  
	DECLARE  @PersonId int 
	EXEC @PersonId= dbo.AddPerson @PersonName, @PersonSurName, @PersonPhoneNumber, @PersonEmail;
	
	IF(ISNUMERIC (@WorkerType) = 1)
		BEGIN
			PRINT 'you entered wrong data about worker type' +  CHAR(13) + 'rollback trun' 
			ROLLBACK
			RETURN
		END

	DECLARE @CheckPassWord int

	SELECT @CheckPassWord = Id
	FROM Worker
	WHERE [PassWord] = @PassWord

	IF(ISNUMERIC (@CheckPassWord) = 1)
		BEGIN
			PRINT 'you entered wrong data about worker password!!! Password must be unique!!!' +  CHAR(13) + 'rollback trun' 
			ROLLBACK
			RETURN
		END


	DECLARE @WorkerTypeId int = 0
					
	SELECT @WorkerTypeId = w.Id
	FROM WorkerType as w
	WHERE w.[Name] = @WorkerType

	IF(@WorkerTypeId = 0)
		BEGIN
			INSERT INTO WorkerType([Name])
				VALUES (@WorkerType)

			SELECT @WorkerTypeId = w.Id
				FROM WorkerType as w
		END

	IF EXISTS(SELECT * FROM Worker WHERE PersonId = @PersonId)
		BEGIN
			IF EXISTS(SELECT * 
					  FROM Worker AS w 
					  WHERE PersonId = @PersonId AND 
							w.TypeId IN (
								SELECT Id 
								FROM WorkerType 
								WHERE [Name] = @WorkerType)
					)
				BEGIN
					PRINT 'data about worker is exist in table' +  CHAR(13) + 'Commit trun' 
					COMMIT
				END
			ELSE
				BEGIN
					UPDATE Worker
					SET TypeId = @WorkerTypeId,
						[PassWord] = @PassWord
					WHERE PersonId = @PersonId

					UPDATE Worker
					SET TypeId = @WorkerTypeId
					WHERE PersonId = @PersonId
					
					BEGIN
						SET @uperr = @@error  
						IF @uperr > @maxerr  
						SET @maxerr = @uperr  
  
						-- If an error occurred, roll back  
						IF @maxerr <> 0  
							BEGIN  
								ROLLBACK  
								PRINT 'did bot updated ' +  CHAR(13) + ' Transaction rolled back' 
								RETURN
							END
						ELSE  
							BEGIN  
								COMMIT  
								PRINT 'updated successfully ' +  CHAR(13) + ' Transaction committed'  
							END
					END

				END
		END
	ELSE 
		BEGIN
			PRINT 'data about worker is NOT exist in table' 
			INSERT INTO Worker (TypeId, PersonId, [PassWord])
			VALUES (@WorkerTypeId, @PersonId, @PassWord);
			
			BEGIN
				SET @uperr = @@error  
				IF @uperr > @maxerr  
				SET @maxerr = @uperr  
  
				-- If an error occurred, roll back  
				IF @maxerr <> 0  
					BEGIN  
						ROLLBACK  
						PRINT 'did bot inserted ' +  CHAR(13) + ' Transaction rolled back'
						RETURN
					END
				ELSE  
					BEGIN  
						COMMIT  
						PRINT 'inserted successfully ' +  CHAR(13) + ' Transaction committed'  
					END
			END
		END
	
END  


--tests

DECLARE  @returnId int  
EXEC @returnId= dbo.AddWorker 'Sergey', 'Petrov', '09433332544', 'Serg@gmail.com', 'accountant', '952'  
SELECT @returnId;  

SELECT * FROM Person
SELECT * FROM Worker
SELECT * FROM vWorker 
SELECT * FROM WorkerType

DELETE FROM Worker
WHERE TypeId IN (SELECT Id 
				 FROM WorkerType
				 WHERE ISNUMERIC(Name) = 1)

DELETE FROM WorkerType
WHERE ISNUMERIC(Name) = 1
