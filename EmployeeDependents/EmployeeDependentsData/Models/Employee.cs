using System;
using System.Collections.Generic;

namespace EmployeeDependentsData.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; } = new List<EmployeeDependent>();
}
