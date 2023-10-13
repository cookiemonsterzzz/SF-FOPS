namespace SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry;

public partial class CustomerEnquiry
{
    public Guid Id { get; set; }

    public string EnquiriesSubject { get; set; } = string.Empty;

    public string EnquiriesDescription { get; set; } = string.Empty;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
