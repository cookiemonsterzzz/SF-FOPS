using Foods.Dto;
using Foods.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foods.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [Route("getMenu")]
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenu()
        {
            var result = await _menuService.GetMenu();
            if (result is null)
            {
                return NotFound("Menu not found.");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
