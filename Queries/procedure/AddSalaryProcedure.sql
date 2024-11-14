DROP PROCEDURE IF EXISTS dbo.AddSalary 
GO
CREATE PROCEDURE dbo.AddSalary
	@WorkerId int, 
	@Date date,
	@Salary decimal
AS
	If NOT EXISTS(SELECT * FROM Worker WHERE Worker.Id = @WorkerId)
	BEGIN
		PRINT 'Worker not found'
		RETURN
	END

	INSERT INTO SalaryList
	(WorkerId, [Date], Salary)
	VALUES (@WorkerId, @Date, @Salary)
GO;


--tests

DECLARE @testDate date = GETDATE()

EXEC dbo.AddSalary 1, @testDate, 100 

SELECT*
FROM SalaryList
