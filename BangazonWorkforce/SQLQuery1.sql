 --SELECT e.Id, e.FirstName, e.LastName, d.Name AS 'Department', c.Make AS 'Computer', t.Name AS 'Trainings'
 --FROM Employee e LEFT JOIN Department d 
 --ON e.DepartmentId = d.Id 
 --LEFT JOIN ComputerEmployee x ON e.Id = x.EmployeeId
-- LEFT JOIN Computer c ON x.ComputerId = c.Id
-- LEFT JOIN EmployeeTraining y ON e.Id = y.EmployeeId
-- LEFT JOIN TrainingProgram t ON y.TrainingProgramId = t.Id
-- WHERE e.Id = 1 AND x.UnassignDate is null;

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1, 2);
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1, 3, '2010-09-01', '2015-09-01');
--SELECT * FROM ComputerEmployee;

--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) Values (4, 3, '2015-10-10')

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) Values (4, 3);

--SELECT * FROM Employee WHERE Employee.Id = 4;

--Insert into Employee (FirstName, LastName, DepartmentId, IsSupervisor) Values ('Rick', 'James', 3, 0);
 --SELECT * FROM Employee;
  
  
 -- SELECT e.Id, e.FirstName, e.LastName, t.Name AS 'Trainings'
--- FROM Employee e LEFT JOIN EmployeeTraining y ON e.Id = y.EmployeeId
 --LEFT JOIN TrainingProgram t ON y.TrainingProgramId = t.Id
 --WHERE e.Id = 5

 SELECT * FROM Department;

 --SELECT Department.Id, Department.Name, Department.Budget, COUNT(Employee.Id) AS NumberOfEmployees 
                   -- FROM Department Left JOIN Employee ON Employee.DepartmentId=Department.Id 
                   -- GROUP BY Department.Id, Department.Name, Department.Budget 



 
