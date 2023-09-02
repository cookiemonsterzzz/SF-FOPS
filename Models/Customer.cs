using System;
using System.Collections.Generic;

namespace Foods.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? CustomerEmail { get; set; }

    public string CustomerContact { get; set; } = null!;

    public DateTime? CustomerLastCreated { get; set; }

    public DateTime? CustomerLastUpdated { get; set; }

    public bool? CustomerIsDeleted { get; set; }

    public virtual ICollection<Enquiries> CustomerEnquiries { get; set; } = new List<Enquiries>();
}
