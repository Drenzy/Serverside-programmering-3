using System;
using System.Collections.Generic;

namespace SoftwareTest.Models;

public partial class TodolostTb
{
    public int? Id { get; set; }

    public int? UserId { get; set; }

    public string? Item { get; set; }

    public virtual Cpr? IdNavigation { get; set; }
}
