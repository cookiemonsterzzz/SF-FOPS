namespace Foods.Interfaces
{
    public interface IEnquiriesService
    {
        Task<List<CustomerEnquiry>> GetAllEnquiries();

        Task<List<CustomerEnquiry>> AddEnquiries(CustomerEnquiry enquiries);

        Task<CustomerEnquiry?> GetSingleEnquiries(Guid enquiryId, Guid customerId);
    }
}
