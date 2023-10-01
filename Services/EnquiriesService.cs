using Foods.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foods.Services
{
    public class EnquiriesService : IEnquiriesService
    {
        private List<CustomerEnquiry> enquiriesList = new List<CustomerEnquiry>();

        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public EnquiriesService(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerEnquiry>> GetAllEnquiries()
        {
            var allEnquiriesList = await _context.CustomerEnquiries.ToListAsync();
            return allEnquiriesList;
        }

        public async Task<List<CustomerEnquiry>> AddEnquiries(CustomerEnquiry enquiries)
        {
            _context.CustomerEnquiries.Add(enquiries);
            await _context.SaveChangesAsync();
            return enquiriesList;
        }

        public async Task<CustomerEnquiry?> GetSingleEnquiries(Guid enquiryId, Guid customerId)
        {
            var singleEnquiry = await _context.CustomerEnquiries.FindAsync(enquiryId);
            if (singleEnquiry is null)
            {
                return null;
            }
            else
            {
                return singleEnquiry;
            }
        }
    }
}
