using System;
using System.Collections.Generic;

namespace SoftwareTest.Models;

public partial class TodolostTb
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public string? Items { get; set; }

    public virtual Cpr? User { get; set; }
}
