using Foods.Dto;

namespace Foods.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetMenu();
    }
}
