using System;
using System.Collections.Generic;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class TestGeneration : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            AppConfig config = workflows.LoadConfiguration();
            var matrix = workflows.LoadAccessMatrix();
            List<Employee> employees = workflows.ReadEmployees("employees_valid.csv");

            var expected = new Dictionary<string, string>
            {
                { "E1001", "ahassan" },
                { "E1002", "botieno" },
                { "E1003", "cmutua" },
                { "E1004", "dkiptoo" },
                { "E1005", "fwanjiru" },
                { "E1006", "jkamau" },
                { "E1007", "jkamau2" },
                { "E1008", "gmwangiotieno" }
            };

            var seenIds = new HashSet<string>();
            var usedUsernames = new HashSet<string>();
            int passed = 0, total = 0;

            foreach (Employee emp in employees)
            {
                ValidationResult v = workflows.ValidateEmployee(emp, config, matrix, seenIds);
                string username = workflows.GenerateUsername(emp, config, usedUsernames);
                string email = workflows.GenerateEmail(username, config);
                AccessGrant grant = workflows.AssignAccess(emp, matrix);

                bool ok = v.IsValid && username == expected[emp.EmployeeID];
                total++;
                if (ok) passed++;

                Log($"{(ok ? "PASS" : "FAIL")} {emp.EmployeeID}: expected {expected[emp.EmployeeID]}, got {username} | {email} | {grant.AccessLevel}");
            }

            Log($"Generation tests: {passed}/{total} passed");

            Employee sample = employees[0];
            string msg = workflows.GenerateWelcomeMessage(sample, "ahassan", "ahassan@" + config.EmailDomain,
                workflows.AssignAccess(sample, matrix));
            Log("Sample welcome message:" + Environment.NewLine + msg);
        }
    }
}