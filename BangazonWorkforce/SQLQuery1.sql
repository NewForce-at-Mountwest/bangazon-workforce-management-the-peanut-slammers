 SELECT e.Id, e.FirstName, e.LastName, d.Name AS 'Department', c.Make AS 'Computer', t.Name AS 'Trainings'
 FROM Employee e JOIN Department d 
 ON e.DepartmentId = d.Id 
 JOIN ComputerEmployee x ON e.Id = x.EmployeeId
 JOIN Computer c ON x.ComputerId = c.Id
 JOIN EmployeeTraining y ON e.Id = y.EmployeeId
 JOIN TrainingProgram t ON y.TrainingProgramId = t.Id
 WHERE e.Id = 1;

-- , c.Make, c.Model 

 