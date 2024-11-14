USE WindowFactory

DROP VIEW IF EXISTS vMaterial
GO
CREATE VIEW vMaterial
AS

SELECT s.Id, 
	   mt.Id as 'MaterialTypeId',
	   mt.[Name] as 'MaterialType',
	   m.[Name],
	   s.Price,
	   s.Quantity,
	   c.[Name] as 'Country',
	   mt.QuantityType as 'QuantityType'
FROM Storage as s
JOIN Material as m 
	ON s.MaterialId = m.Id
JOIN MaterialType as mt
	ON m.MaterialTypeId = mt.Id
JOIN Country as c
	ON m.CountryId = c.Id

GO
SELECT * FROM vMaterial;

