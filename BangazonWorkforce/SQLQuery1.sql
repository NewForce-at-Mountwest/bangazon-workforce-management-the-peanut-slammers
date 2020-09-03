 SELECT e.Id, e.FirstName, e.LastName, d.Name AS 'Department', c.Make AS 'Computer', t.Name AS 'Trainings'
 FROM Employee e JOIN Department d 
 ON e.DepartmentId = d.Id 
 JOIN ComputerEmployee x ON e.Id = x.EmployeeId
 JOIN Computer c ON x.ComputerId = c.Id
 JOIN EmployeeTraining y ON e.Id = y.EmployeeId
 JOIN TrainingProgram t ON y.TrainingProgramId = t.Id
 WHERE e.Id = 4 AND x.UnassignDate is null;

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1, 2);
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1, 3, '2010-09-01', '2015-09-01');
--SELECT * FROM ComputerEmployee;



 