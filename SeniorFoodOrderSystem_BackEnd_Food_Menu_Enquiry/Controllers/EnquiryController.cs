using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Dto;

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
        public async Task<ActionResult<CustomerEnquiry>> AddEnquiry([FromBody] EnquiryDto enquiry)
        {
            var userId = await GetUserIdByToken();

            if (userId is null)
            {
                return NotFound("User not found.");
            }

            var newEnquiry = new CustomerEnquiry
            {
                Id = Guid.NewGuid(),
                EnquiriesSubject = enquiry.EnquiriesSubject,
                EnquiriesDescription = enquiry.EnquiriesDescription,
                DateTimeCreated = DateTimeOffset.Now,
                UserId = (Guid)userId
            };

            _context.CustomerEnquiries.Add(newEnquiry);
            await _context.SaveChangesAsync();

            return Ok(enquiry);
        }

        private async Task<Guid?> GetUserIdByToken()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""); // Remove the "Bearer " prefix

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var phoneNo = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "PhoneNo")?.Value;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNo == phoneNo);
                if (user is not null)
                {
                    return user.Id;
                }
            }
            return null;
        }
    }
}
