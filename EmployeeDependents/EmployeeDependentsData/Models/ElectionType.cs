using System;
using System.Collections.Generic;

namespace EmployeeDependentsData.Models;

public partial class ElectionType
{
    public Guid Id { get; set; }

    public string ElectionType1 { get; set; } = null!;

    public double Cost { get; set; }
}
