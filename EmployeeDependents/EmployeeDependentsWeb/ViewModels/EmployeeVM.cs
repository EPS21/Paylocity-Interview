using EmployeeDependentsData.Models;

namespace EmployeeDependentsWeb.ViewModels
{
    public class EmployeeVM
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<EmployeeDependentVM>? Dependents { get; set; }

        //public List<ElectionTypeVM> ElectionTypes { get; set; }

    }
}
