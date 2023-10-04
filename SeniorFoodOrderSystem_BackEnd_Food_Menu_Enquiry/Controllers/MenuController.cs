using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public MenuController(SeniorFoodOrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [Route("getMenu")]
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenu()
        {
            var userId = await GetUserIdByToken();

            var stalls = await _context.Stalls
                .Include(x => x.StallRatings)
                .ToListAsync();

            if (stalls == null || stalls.Count == 0)
            {
                return NotFound("No stalls found.");
            }

            var menuList = stalls.Select(stall => new MenuDto
            {
                stallId = stall.Id,
                stallName = stall.StallName,
                rating = stall.StallRatings?.FirstOrDefault(rating => rating.StallId == stall.Id)?.Rating ?? 0,
                foods = _context.Foods
                    .Where(food => food.StallId == stall.Id)
                    .Select(food => new FoodDto
                    {
                        foodName = food.FoodName,
                        price = food.FoodPrice,
                        image = "",
                        foodCustomization = _context.FoodsCustomizations
                            .Where(customization => customization.FoodId == food.Id)
                            .Select(customization => new FoodCustomizationDto
                            {
                                name = customization.FoodCustomizationName,
                                price = customization.FoodCustomizationPrice,
                            }).ToList()
                    }).ToList()
            }).ToList();

            if (menuList == null || menuList.Count == 0)
            {
                return NotFound("Menu not found.");
            }

            menuList = userId is not null
                ? menuList.OrderByDescending(menu => CalculateOrderHistoryScore(menu.stallId, (Guid)userId)).ToList()
                : menuList.ToList();

            return Ok(menuList);
        }

        private async Task<double> CalculateOrderHistoryScore(Guid stallId, Guid userId)
        {
            // Get all orders for the given stallId
            var numberOfOrdersInThisStall = await _context.Orders
                .Where(o => o.StallId == stallId && o.UserId == userId)
                .CountAsync();

            return numberOfOrdersInThisStall;
        }

        private async Task<Guid?> GetUserIdByToken()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""); // Remove the "Bearer " prefix

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var phoneNo = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "PhoneNO")?.Value;
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
