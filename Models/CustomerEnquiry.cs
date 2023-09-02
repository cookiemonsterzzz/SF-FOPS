using System;
using System.Collections.Generic;

namespace Foods.Models;

public partial class Enquiries
{
    public Guid EnquiriesId { get; set; }

    public string EnquiriesSubject { get; set; } = null!;

    public string EnquiriesDescription { get; set; } = null!;

    public DateTime? EnquiriesLastCreated { get; set; }

    public DateTime? EnquiriesLastUpdated { get; set; }

    public bool? EnquiriesIsDeleted { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
