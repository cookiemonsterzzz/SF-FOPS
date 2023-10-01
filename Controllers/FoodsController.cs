using Foods.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foods.Controllers
{
    [Route("api/Foods")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetAllFoods()
        {
            return await _foodService.GetAllFoods();
        }

        [Route("GetSingleFood")]
        [HttpPost]
        public async Task<ActionResult<List<Food>>> GetSingleFood(Guid id)
        {
            var result = await _foodService.GetSingleFood(id);
            if (result is null)
            {
                return NotFound("Food not found.");
            }
            else
            {
                return Ok(result);
            }
        }

        [Route("AddFood")]
        [HttpPost]
        public async Task<ActionResult<List<Food>>> AddFood(Food food)
        {
            var result = await _foodService.AddFood(food);
            return Ok(result);
        }


        [HttpPut("{FoodId}")]
        public async Task<ActionResult<List<Food>>> UpdateFood(Guid id, Food request)
        {
            var result = await _foodService.UpdateFood(id, request);
            if (result is null)
            {
                return NotFound("No Food Updated.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpDelete("{FoodId}")]
        public async Task<ActionResult<List<Food>>> DeleteFood(Guid id)
        {
            var result = await _foodService.DeleteFood(id);
            if (result is null)
            {
                return NotFound("No Food Deleted.");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
