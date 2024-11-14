CREATE FUNCTION dbo.GetMaterial
(
      @MaterialNeed INT
)

RETURNS @Materials table
	  (
		Id int identity(1,1),
		MaterialId int
	  )
AS
BEGIN
      
      INSERT INTO @Materials
		SELECT Storage.Id
		FROM Storage
		WHERE MaterialId IN(
			SELECT Material.Id
			FROM Material
			WHERE MaterialTypeId = @MaterialNeed
		)

	RETURN
END






--tests


SELECT * FROM GetMaterial(1)  

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