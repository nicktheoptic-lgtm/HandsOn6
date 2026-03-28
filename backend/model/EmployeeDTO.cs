namespace Model
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? YearsOfService { get; set; }
        public int DepartmentID { get; set; }
        public DepartmentDTO? Department { get; set; } // Nested object for joined data
    }

    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string? OfficeNumber { get; set; }
    }
}