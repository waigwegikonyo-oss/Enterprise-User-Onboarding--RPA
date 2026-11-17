using System;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class GenerateWelcomeMessage : CodedWorkflow
    {
        [Workflow]
        public string Execute(Employee emp, string username, string email, AccessGrant grant)
        {
            string[] lines = new string[]
            {
                $"Welcome to the team, {emp.FirstName}!",
                "",
                $"Start date: {emp.StartDate}",
                $"Department: {emp.Department} ({emp.Role})",
                $"Manager: {emp.Manager}",
                "",
                $"Username: {username}",
                $"Email: {email}",
                $"Systems: {grant.Systems.Replace(";", ", ")}",
                $"Access level: {grant.AccessLevel}"
            };

            return string.Join(Environment.NewLine, lines);
        }
    }
}