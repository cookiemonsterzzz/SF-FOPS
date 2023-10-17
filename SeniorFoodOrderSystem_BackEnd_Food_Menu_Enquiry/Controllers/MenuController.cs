using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Dto;

namespace SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly SeniorFoodOrderSystemDatabaseContext _context;
        private Dictionary<Guid, int> _stallOrderCountDict = new();
        private Dictionary<Guid, int> _stallRatingsDict = new();
        private Dictionary<Guid, Dictionary<string, FoodCustomizationDto>> _foodCustomizationsDict = new();
        private Dictionary<Guid, List<FoodDto>> _stallFoodsDict = new();

        public MenuController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [Route("getMenu")]
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenu()
        {
            var userId = await GetUserIdByToken();
            _stallOrderCountDict = await GetStallOrderCountByUserId(userId);
            _foodCustomizationsDict = await LoadFoodCustomizations();
            _stallFoodsDict = await LoadStallFoods();
            var stalls = await GetStallsWithRatingsAndFoods();

            if (stalls == null || stalls.Count == 0)
            {
                return NotFound("No stalls found.");
            }

            _stallRatingsDict = await GetAverageRatingsDictionary(stalls); // Get average ratings

            var menuList = BuildMenuList(stalls, userId);

            if (menuList == null || menuList.Count == 0)
            {
                return NotFound("Menu not found.");
            }

            return Ok(menuList);
        }

        private async Task<Dictionary<Guid, int>> GetStallOrderCountByUserId(Guid? userId)
        {
            var result = await _context.Orders
                .Where(o => o.UserId == userId)
                .GroupBy(o => o.StallId)
                .ToListAsync();
            return result.ToDictionary(g => g.Key, g => g.Count());
        }

        private async Task<List<Stall>> GetStallsWithRatingsAndFoods()
        {
            // Fetch all stalls with ratings from the database
            var stallsWithRatings = await _context.Stalls
                .Include(x => x.StallRatings)
                .ToListAsync();

            // Filter stalls that have associated food items in the _stallFoodsDict
            var stallsWithFoods = stallsWithRatings
                .Where(stall => _stallFoodsDict.ContainsKey(stall.Id))
                .ToList();

            return stallsWithFoods;
        }

        private List<MenuDto> BuildMenuList(List<Stall> stalls, Guid? userId)
        {
            var menuList = stalls.Select(stall => new MenuDto
            {
                stallId = stall.Id,
                stallName = stall.StallName,
                rating = GetStallRating(stall.Id),
                foods = _stallFoodsDict.TryGetValue(stall.Id, out var foods)
                        ? foods.OrderBy(x => x.foodName).ToList()
                        : new List<FoodDto>()
            }).ToList();

            if (userId != null)
            {
                menuList = menuList
                    .OrderByDescending(menu => GetStallOrderCount(menu.stallId))
                    .ThenByDescending(menu => menu.rating)
                    .ToList();
            }

            return menuList;
        }

        private int GetStallOrderCount(Guid stallId)
        {
            return _stallOrderCountDict.TryGetValue(stallId, out var count) ? count : 0;
        }

        private int GetStallRating(Guid stallId)
        {
            return _stallRatingsDict.TryGetValue(stallId, out var rating) ? rating : 0;
        }

        private async Task<Dictionary<Guid, List<FoodDto>>> LoadStallFoods()
        {
            var result = await _context.Foods
                .GroupBy(food => food.StallId)
                .ToListAsync();

            var stallFoodsDictionary = result.ToDictionary(
                    group => group.Key,
                    group => group.Select(food => new FoodDto
                    {
                        foodName = food.FoodName,
                        price = food.FoodPrice,
                        image = food.ImageUrl ?? string.Empty,
                        foodCustomization = _foodCustomizationsDict.TryGetValue(food.Id, out var customizations)
                            ? customizations.Values.ToList()
                            : new List<FoodCustomizationDto>()
                    }).ToList()
                );

            return stallFoodsDictionary;
        }

        private async Task<Dictionary<Guid, Dictionary<string, FoodCustomizationDto>>> LoadFoodCustomizations()
        {
            var result = await _context.FoodsCustomizations
                .GroupBy(customization => customization.FoodId)
                .ToListAsync();

            return result.ToDictionary(
                  group => group.Key,
                  group => group.ToDictionary(
                      customization => customization.FoodCustomizationName,
                      customization => new FoodCustomizationDto
                      {
                          name = customization.FoodCustomizationName,
                          price = customization.FoodCustomizationPrice,
                      }
                  )
              );
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

        private async Task<Dictionary<Guid, int>> GetAverageRatingsDictionary(List<Stall> stalls)
        {
            var averageRatingsDict = new Dictionary<Guid, int>();

            foreach (var stall in stalls)
            {
                var ratings = await _context.StallRatings
                    .Where(sr => sr.StallId == stall.Id)
                    .Select(sr => sr.Rating)
                    .ToListAsync();

                if (ratings.Any())
                {
                    int averageRating = (int)Math.Abs(ratings.Average());
                    averageRatingsDict[stall.Id] = averageRating;
                }
                else
                {
                    // Set default rating to 0 if no ratings are available
                    averageRatingsDict[stall.Id] = 5;
                }
            }
            return averageRatingsDict;
        }
    }
}
