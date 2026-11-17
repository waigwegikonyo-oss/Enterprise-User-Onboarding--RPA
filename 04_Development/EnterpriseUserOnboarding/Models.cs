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
}