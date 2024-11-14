USE WindowFactory
GO
CREATE FUNCTION dbo.WorkerIs 
(
	@WorkerType int, 
	@Id int
)
RETURNS bit
AS
BEGIN
    IF EXISTS (SELECT * FROM Worker WHERE Worker.TypeId = @WorkerType AND Worker.Id = @Id)
		RETURN 'true'
	ELSE
		RETURN 'false'
	RETURN 'false'
END
GO

--tests

DROP FUNCTION dbo.WorkerIs
GO 

DECLARE @res bit, @WorkerType int = 3, @Id Int = 1
SET @res = dbo.WorkerIs(@WorkerType, @Id)
SELECT @res

GO
SELECT *
FROM WorkerType

GO
SELECT *
FROM Worker