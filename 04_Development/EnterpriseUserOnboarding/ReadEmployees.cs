using System;
using System.Collections.Generic;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class ReadEmployees : CodedWorkflow
    {
        [Workflow]
        public List<Employee> Execute(string inputFileName)
        {
            string path = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, "..", "..", "06_Test_Data", inputFileName));

            if (!File.Exists(path))
            {
                throw new Exception($"Input file not found: {path}");
            }

            string[] lines = File.ReadAllLines(path);
            var employees = new List<Employee>();

            for (int i = 1; i < lines.Length; i++)   // line 0 is the header
            {
                string[] c = lines[i].Split(',');
                string Get(int idx) => idx < c.Length ? c[idx].Trim() : "";

                employees.Add(new Employee
                {
                    RowNumber = i,
                    EmployeeID = Get(0),
                    FirstName = Get(1),
                    LastName = Get(2),
                    Department = Get(3),
                    Role = Get(4),
                    StartDate = Get(5),
                    Manager = Get(6)
                });
            }

            Log($"Read {employees.Count} employee rows from {inputFileName}");
            return employees;
        }
    }
}