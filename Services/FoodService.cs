using Foods.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foods.Services
{
    public class FoodService : IFoodService
    {
        private List<Food> foodList = new List<Food>();

        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public FoodService(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }


        public async Task<List<Food>> AddFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return foodList;
        }

        public async Task<List<Food>?> DeleteFood(Guid id)
        {
            var singleFood = await _context.Foods.FindAsync(id);
            if (singleFood is null)
            {
                return null;
            }
            else
            {
                _context.Foods.Remove(singleFood);
                await _context.SaveChangesAsync();
                return foodList;
            }
        }

        public async Task<List<Food>> GetAllFoods()
        {
            var foodsList = await _context.Foods.ToListAsync();
            return foodsList;
        }

        public async Task<Food?> GetSingleFood(Guid id)
        {
            var singleFood = await _context.Foods.FindAsync(id);
            if (singleFood is null)
            {
                return null;
            }
            else
            {
                return singleFood;
            }
        }

        public async Task<List<Food>?> UpdateFood(Guid id, Food request)
        {
            var singleFood = await _context.Foods.FindAsync(id);
            if (singleFood is null)
            {
                return null;
            }
            else
            {
                singleFood.FoodName = request.FoodName;
                singleFood.FoodPrice = request.FoodPrice;
                singleFood.DateTimeUpdated = DateTime.Now;
                singleFood.FoodDescription = request.FoodDescription;

                await _context.SaveChangesAsync();

                return foodList;
            }
        }
    }
}
