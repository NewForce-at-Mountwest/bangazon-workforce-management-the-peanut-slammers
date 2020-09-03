SELECT Department.Name, Department.Budget, COUNT(Employee.Id) AS NumberOfEmployees FROM Department JOIN Employee ON Employee.DepartmentId=Department.Id GROUP BY Department.Name, Department.Budget

