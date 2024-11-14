USE WindowFactory

DROP VIEW IF EXISTS vSupplyItem
GO
CREATE VIEW vSupplyItem
AS

SELECT s.Id, 
	   o.Price,
	   s.Quantity,
	   s.CreationDate,
	   s.OfferId as 'OfferId',
	   s.SypplyOrderId,
	   m.[Name] as 'MaterialName',
	   mt.Id as 'MaterialTypeId',
	   m.[Name],
	   c.[Name] as 'CountryName',
	   mt.[Name] as 'MaterialType',
	   mt.QuantityType as 'QuantityType'
FROM SupplyItem as s
JOIN Offer as o 
	ON o.Id = s.OfferId
JOIN Material as m
	ON m.Id = s.MaterialId
JOIN MaterialType as mt
	ON mt.Id = m.MaterialTypeId
JOIN Country as c
	ON m.CountryId = c.Id

GO
SELECT * FROM vSupplyItem
