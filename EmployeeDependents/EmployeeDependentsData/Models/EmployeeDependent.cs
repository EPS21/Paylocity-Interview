using System;
using System.Collections.Generic;

namespace EmployeeDependentsData.Models;

public partial class EmployeeDependent
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Guid EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
