CREATE FUNCTION dbo.GetFreeEngineer
()

RETURNS @Engineer table
	  (
		Id int identity(1,1),
		EngineerId int
	  )
AS
BEGIN
	
	DECLARE @TypeId int

	SELECT @TypeId = w.Id
	FROM WorkerType AS w
	WHERE w.Name = 'engineer'
      
      INSERT INTO @Engineer
		SELECT w.Id
		FROM Worker AS w
		WHERE w.TypeId = @TypeId AND  w.Id NOT IN(
			SELECT EngineerId
			FROM Product
		)

	RETURN
END



--tests

DROP FUNCTION dbo.GetFreeEngineer

SELECT * FROM GetFreeEngineer()  

SELECT * FROM Storage

DECLARE @MaterialId int = 1

SELECT Id
		FROM Storage
		WHERE MaterialId IN(
			SELECT Id
			FROM Material
			WHERE MaterialTypeId = @MaterialId
		)


 DECLARE @Materials table
(
	Id int identity(1,1),
	MaterialId int
)

INSERT INTO @Materials
SELECT Storage.Id
		FROM Storage
		WHERE MaterialId IN(
			SELECT Material.Id
			FROM Material
			WHERE MaterialTypeId = 1
		)

SELECT *
FROM @Materials