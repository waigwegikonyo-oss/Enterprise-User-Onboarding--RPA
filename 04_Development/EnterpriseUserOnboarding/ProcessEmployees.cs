using System;
using System.Collections.Generic;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class ProcessEmployees : CodedWorkflow
    {
        [Workflow]
        public List<RecordOutcome> Execute(string inputFileName)
        {
            DateTime start = DateTime.Now;
            string runId = workflows.Initialize(inputFileName);

            AppConfig config = workflows.LoadConfiguration();
            var matrix = workflows.LoadAccessMatrix();
            List<Employee> employees = workflows.ReadEmployees(inputFileName);

            var seenIds = new HashSet<string>();
            var usedUsernames = new HashSet<string>();
            var outcomes = new List<RecordOutcome>();

            foreach (Employee emp in employees)
            {
                var outcome = new RecordOutcome
                {
                    RowNumber = emp.RowNumber,
                    EmployeeID = emp.EmployeeID,
                    FullName = (emp.FirstName + " " + emp.LastName).Trim()
                };

                try
                {
                    ValidationResult v = workflows.ValidateEmployee(emp, config, matrix, seenIds);

                    if (!v.IsValid)
                    {
                        outcome.Status = "Rejected";
                        outcome.ErrorCode = v.ErrorCode;
                    }
                    else
                    {
                        string username = workflows.GenerateUsername(emp, config, usedUsernames);
                        string email = workflows.GenerateEmail(username, config);
                        AccessGrant grant = workflows.AssignAccess(emp, matrix);
                        string message = workflows.GenerateWelcomeMessage(emp, username, email, grant);

                        string messagePath = Path.Combine(OutputPaths.OutputDir(),
                            $"welcome_{runId}_{username}.txt");
                        File.WriteAllText(messagePath, message);

                        outcome.Status = "Processed";
                        outcome.ErrorCode = "";
                        outcome.Username = username;
                        outcome.Email = email;
                        outcome.AccessLevel = grant.AccessLevel;
                        outcome.Systems = grant.Systems;
                    }
                }
                catch (Exception ex)
                {
                    outcome.Status = "Failed";
                    outcome.ErrorCode = "SYSTEM_ERROR";
                    Log($"Row {emp.RowNumber} failed: {ex.Message}");
                }

                outcomes.Add(outcome);
                workflows.WriteAuditLog(runId, outcome);
            }

            workflows.GenerateReport(runId, start, outcomes);
            workflows.CloseProcess(runId, start, outcomes);
            return outcomes;
        }
    }
}