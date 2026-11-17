using System.Collections.Generic;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class TestReadEmployees : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            List<Employee> employees = workflows.ReadEmployees("employees_valid.csv");

            foreach (Employee e in employees)
            {
                Log($"Row {e.RowNumber}: {e.EmployeeID} | {e.FirstName} {e.LastName} | {e.Department} | {e.Role} | {e.StartDate}");
            }
        }
    }
}