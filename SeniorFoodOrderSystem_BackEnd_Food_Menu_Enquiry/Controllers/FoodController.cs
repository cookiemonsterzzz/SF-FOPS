using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public FoodController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetAllFoods()
        {
            var foodsList = await _context.Foods.ToListAsync();
            return foodsList;
        }

        [Route("getFoodById")]
        [HttpGet]
        public async Task<ActionResult<Food>> GetFoodById(Guid id)
        {
            var singleFood = await _context.Foods.FirstOrDefaultAsync(x => x.Id == id);
            if (singleFood == null)
            {
                return NotFound("Food not found.");
            }
            else
            {
                return Ok(singleFood);
            }
        }

        [Route("AddFood")]
        [HttpPost]
        public async Task<ActionResult<Food>> AddFood([FromBody] Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return Ok(food);
        }

        [HttpPut("{FoodId}")]
        public async Task<ActionResult<Food>> UpdateFood(Guid id, [FromBody] Food request)
        {
            var singleFood = await _context.Foods.FindAsync(id);
            if (singleFood == null)
            {
                return NotFound("No Food Updated.");
            }
            else
            {
                singleFood.FoodName = request.FoodName;
                singleFood.FoodPrice = request.FoodPrice;
                singleFood.DateTimeUpdated = DateTime.Now;
                singleFood.ImageUrl = request.ImageUrl;

                await _context.SaveChangesAsync();

                return Ok(singleFood);
            }
        }

        [HttpDelete("{FoodId}")]
        public async Task<ActionResult<Food>> DeleteFood(Guid id)
        {
            var singleFood = await _context.Foods.FindAsync(id);
            if (singleFood == null)
            {
                return NotFound("No Food Deleted.");
            }
            else
            {
                _context.Foods.Remove(singleFood);
                await _context.SaveChangesAsync();
                return Ok(singleFood);
            }
        }
    }
}
