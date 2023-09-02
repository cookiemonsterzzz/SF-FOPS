using Foods.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foods.Controllers
{
    [Route("api/Enquiries")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly IEnquiriesService _enquiriesService;

        public EnquiriesController(IEnquiriesService enquiriesService)
        {
            _enquiriesService = enquiriesService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Enquiries>>> GetAllEnquiries()
        {
            var result = await _enquiriesService.GetAllEnquiries();
            return Ok(result);
        }

        [Route("GetSingleEnquiries")]
        [HttpPost]
        public async Task<ActionResult<List<Enquiries>>?> GetSingleEnquiries(Guid enquiryId, Guid customerId)
        {
            var result = await _enquiriesService.GetSingleEnquiries(enquiryId, customerId);
            if (result is null)
            {
                return NotFound("Enquiries not found.");
            }
            else
            {
                return Ok(result);
            }
        }

        [Route("AddEnquiries")]
        [HttpPost]
        public async Task<ActionResult<List<Enquiries>>> AddEnquiries(Enquiries enquiries)
        {
            var result = await _enquiriesService.AddEnquiries(enquiries);
            return Ok(result);
        }
    }
}
