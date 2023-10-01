using Foods.Dto;
using Foods.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foods.Services
{
    public class MenuService : IMenuService
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;

        public MenuService(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> GetMenu()
        {
            var menuList =
                 await _context.Stalls.Select(stall => new MenuDto
                 {

                     stallName = stall.StallName,
                     rating = 1,
                     foods =
                        _context.Foods.Where(food => food.StallId == stall.Id).Select(food =>
                        new FoodDto
                        {
                            foodName = food.FoodName,
                            price = food.FoodPrice,
                            image = "",
                            foodCustomization =
                                _context.FoodsCustomizations.Where(customization => customization.FoodId == food.Id)
                                .Select(customization =>
                                    new FoodCustomizationDto
                                    {
                                        name = customization.FoodCustomizationName,
                                        price = customization.FoodCustomizationPrice,
                                    }
                            ).ToList()
                        }
                    ).ToList()
                 }
                ).ToListAsync();

            return menuList ??= new List<MenuDto>();
        }
    }
}
