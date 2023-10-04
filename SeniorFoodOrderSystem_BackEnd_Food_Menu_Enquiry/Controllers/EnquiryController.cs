using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Controllers
{
    [Route("api/enquiry")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public EnquiriesController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerEnquiry>>> GetAllEnquiries()
        {
            var allEnquiriesList = await _context.CustomerEnquiries.ToListAsync();
            return allEnquiriesList;
        }

        [Route("getEnquiryById")]
        [HttpGet]
        public async Task<ActionResult<CustomerEnquiry>> GetEnquiryById(Guid enquiryId)
        {
            var enquiry = await _context.CustomerEnquiries.FindAsync(enquiryId);
            if (enquiry is null)
            {
                return NotFound();
            }

            return Ok(enquiry);
        }

        [Route("addEnquiry")]
        [HttpPost]
        public async Task<ActionResult<List<CustomerEnquiry>>> AddEnquiries(CustomerEnquiry enquiries)
        {
            _context.CustomerEnquiries.Add(enquiries);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
