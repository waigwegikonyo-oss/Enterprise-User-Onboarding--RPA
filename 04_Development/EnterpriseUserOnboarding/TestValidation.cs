using System;
using System.Collections.Generic;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class TestValidation : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            AppConfig config = workflows.LoadConfiguration();
            var matrix = workflows.LoadAccessMatrix();
            List<Employee> employees = workflows.ReadEmployees("employees_invalid.csv");
            var seenIds = new HashSet<string>();

            string casesPath = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, "..", "..", "06_Test_Data", "invalid_test_cases.csv"));
            string[] cases = File.ReadAllLines(casesPath);

            int passed = 0, total = 0;

            for (int i = 1; i < cases.Length; i++)
            {
                string[] c = cases[i].Split(',');
                string testId = c[0];
                int dataRow = int.Parse(c[2]);
                string expectedResult = c[5];
                string expectedReason = c.Length > 6 ? c[6] : "";

                Employee emp = employees[dataRow - 1];
                ValidationResult r = workflows.ValidateEmployee(emp, config, matrix, seenIds);

                string actualResult = r.IsValid ? "Processed" : "Rejected";
                bool ok = actualResult == expectedResult && r.ErrorCode == expectedReason;

                total++;
                if (ok) passed++;
                Log($"{(ok ? "PASS" : "FAIL")} {testId}: expected {expectedResult}/{expectedReason}, got {actualResult}/{r.ErrorCode}");
            }

            Log($"Validation tests: {passed}/{total} passed");
        }
    }
}