using Foods.Data;
using Foods.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foods.Services
{
    public class EnquiriesService : IEnquiriesService
    {
        private List<Enquiries> enquiriesList = new List<Enquiries>();

        private readonly FopsContext _context;

        public EnquiriesService(FopsContext context)
        {
            _context = context;
        }

        public async Task<List<Enquiries>> GetAllEnquiries()
        {
            var allEnquiriesList = await _context.CustomerEnquiries.ToListAsync();
            return allEnquiriesList;
        }

        public async Task<List<Enquiries>> AddEnquiries(Enquiries enquiries)
        {
            _context.CustomerEnquiries.Add(enquiries);
            await _context.SaveChangesAsync();
            return enquiriesList;
        }

        public async Task<Enquiries?> GetSingleEnquiries(Guid enquiryId, Guid customerId)
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
