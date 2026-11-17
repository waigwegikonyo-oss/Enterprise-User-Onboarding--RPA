using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class ValidateEmployee : CodedWorkflow
    {
        [Workflow]
        public ValidationResult Execute(Employee emp, AppConfig config,
            Dictionary<string, AccessGrant> accessMatrix, HashSet<string> seenIds)
        {
            string code = FirstError(emp, config, accessMatrix, seenIds);

            if (code == null)
            {
                seenIds.Add(emp.EmployeeID);   // only accepted IDs count for duplicate checks
            }

            return new ValidationResult { IsValid = code == null, ErrorCode = code ?? "" };
        }

        private static string FirstError(Employee e, AppConfig config,
            Dictionary<string, AccessGrant> accessMatrix, HashSet<string> seenIds)
        {
            if (string.IsNullOrWhiteSpace(e.EmployeeID) && string.IsNullOrWhiteSpace(e.FirstName) &&
                string.IsNullOrWhiteSpace(e.LastName) && string.IsNullOrWhiteSpace(e.Department) &&
                string.IsNullOrWhiteSpace(e.Role) && string.IsNullOrWhiteSpace(e.StartDate) &&
                string.IsNullOrWhiteSpace(e.Manager))
                return "EMPTY_RECORD";

            if (string.IsNullOrWhiteSpace(e.EmployeeID)) return "MISSING_EMPLOYEE_ID";
            if (string.IsNullOrWhiteSpace(e.FirstName)) return "MISSING_FIRSTNAME";
            if (string.IsNullOrWhiteSpace(e.LastName)) return "MISSING_LASTNAME";
            if (string.IsNullOrWhiteSpace(e.Department)) return "MISSING_DEPARTMENT";
            if (string.IsNullOrWhiteSpace(e.Role)) return "MISSING_ROLE";
            if (string.IsNullOrWhiteSpace(e.StartDate)) return "MISSING_STARTDATE";
            if (string.IsNullOrWhiteSpace(e.Manager)) return "MISSING_MANAGER";

            if (!Regex.IsMatch(e.StartDate, @"^\d{4}-\d{2}-\d{2}$")) return "INVALID_DATE_FORMAT";
            if (!DateTime.TryParseExact(e.StartDate, config.DateFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                return "INVALID_DATE_VALUE";

            if (!config.ValidDepartments.Contains(e.Department)) return "UNKNOWN_DEPARTMENT";
            if (!accessMatrix.ContainsKey(e.Department + "|" + e.Role)) return "UNKNOWN_ROLE";
            if (seenIds.Contains(e.EmployeeID)) return "DUPLICATE_EMPLOYEE_ID";

            return null;   // no error found
        }
    }
}