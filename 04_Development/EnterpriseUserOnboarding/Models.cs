using System;
using System.IO;

namespace EnterpriseUserOnboarding
{
    public class Employee
    {
        public int RowNumber { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string StartDate { get; set; }
        public string Manager { get; set; }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorCode { get; set; }
    }

    public class AccessGrant
    {
        public string Systems { get; set; }
        public string AccessLevel { get; set; }
    }

    public class RecordOutcome
    {
        public int RowNumber { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }      // Processed, Rejected or Failed
        public string ErrorCode { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessLevel { get; set; }
        public string Systems { get; set; }
    }

    public static class OutputPaths
    {
        public static string OutputDir()
        {
            string dir = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, "..", "..", "07_Output"));
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}