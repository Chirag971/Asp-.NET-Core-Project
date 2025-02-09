using System;
using System.Collections.Generic;

namespace StudentDatabase.Models;

public partial class StudentDatum
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public int? StudentAge { get; set; }
}
