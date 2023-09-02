namespace Foods.Interfaces
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllFoods();
        Task<Food?> GetSingleFood(Guid id);
        Task<List<Food>> AddFood(Food food);
        Task<List<Food>?> UpdateFood(Guid id, Food request);
        Task<List<Food>?> DeleteFood(Guid id);
    }
}
