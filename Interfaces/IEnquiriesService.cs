namespace Foods.Interfaces
{
    public interface IEnquiriesService
    {
        Task<List<Enquiries>> GetAllEnquiries();

        Task<List<Enquiries>> AddEnquiries(Enquiries enquiries);

        Task<Enquiries?> GetSingleEnquiries(Guid enquiryId, Guid customerId);
    }
}
