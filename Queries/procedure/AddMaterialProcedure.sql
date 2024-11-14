DROP PROCEDURE IF EXISTS dbo.AddMaterial
GO
CREATE PROCEDURE dbo.AddMaterial 
(
	@TypeId int,
	@Name nvarchar(50),
	@Price decimal,
	@Quantity int,
	@CountryId int
)
AS
	DECLARE @MaterialId int
	
	SELECT @MaterialId = Id 
	FROM Material
	WHERE Material.MaterialTypeId = @TypeId AND 
		  Material.CountryId = @CountryId AND 
		  Material.[Name] = @Name

	IF NOT (ISNUMERIC(@MaterialId) = 1)
		BEGIN
			PRINT 'Material do not exist in table Material'

			INSERT INTO Material
			(MaterialTypeId, CountryId, [Name])
			VALUES
			(@TypeId, @CountryId, @Name)

			SELECT @MaterialId = Id
			FROM Material
			WHERE Material.MaterialTypeId = @TypeId AND
			  Material.[Name] = @Name AND
			  Material.CountryId = @CountryId
		END

	PRINT 'This Material exists in Table Material'

	DECLARE @StorageId int

	SELECT @StorageId = Id
	FROM Storage
	WHERE Storage.MaterialId = @MaterialId AND
		Storage.Price = @Price

	IF (ISNUMERIC(@StorageId) = 1)
		BEGIN
			PRINT 'Material exist in Table Storage with index'
			PRINT 'Update quantity of material in Table Storage' 
			
			UPDATE Storage
			SET Quantity = Quantity + @Quantity
			WHERE Id = @StorageId
		END
	ELSE
		BEGIN
			PRINT 'Material do not exist in Table Storage'
			PRINT 'Insert new material in Table Storage' 
		
			INSERT Storage
			(Price, Quantity, MaterialId)
			VALUES
			(@Price, @Quantity, @MaterialId)
		END
	PRINT 'Material data updated'
	

	--@TypeId int,
	--@Name int,
	--@Price decimal,
	--@Quantity int,
	--@CountryId int

EXEC dbo.AddMaterial 1, 'test material', 100, 4, 52



SELECT *
FROM Material

SELECT *
FROM Storage

GO



DECLARE @MaterialId int = 1
	
SELECT @MaterialId = Id 
FROM Material
WHERE MaterialTypeId = 1 AND CountryId = 50

	IF (NOT ISNUMERIC(@MaterialId) = 1)
		BEGIN
			INSERT INTO Material
			(MaterialTypeId, CountryId, [Name])
			VALUES
			(@TypeId, @CountryId, @Name)

			SELECT @MaterialId = Id
			FROM Material
			WHERE Material.MaterialTypeId = @TypeId AND
			  Material.[Name] = @Name AND
			  Material.CountryId = @CountryId
		END