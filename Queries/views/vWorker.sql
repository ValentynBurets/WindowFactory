USE WindowFactory

DROP VIEW IF EXISTS vWorker
GO
CREATE VIEW vWorker
AS

SELECT 
	w.Id,
	p.[Name],
	p.Surname,
	p.PhoneNumber,
	p.Email,
	wt.[Name] as 'WokerType',
	w.[PassWord]
FROM Worker as w
JOIN Person as p 
	ON w.PersonId = p.Id
JOIN WorkerType as wt
	ON w.TypeId = wt.Id

--test
GO
SELECT * FROM vWorker;

GO
SELECT *
FROM Worker