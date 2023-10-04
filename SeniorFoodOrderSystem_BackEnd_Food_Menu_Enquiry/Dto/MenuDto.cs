namespace SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.Dto
{
    public class MenuDto
    {
        public Guid stallId { get; set; }

        public string stallName { get; set; } = string.Empty;

        public int rating { get; set; }

        public List<FoodDto> foods { get; set; } = new List<FoodDto>();
    }

    public class FoodDto
    {
        public string foodName { get; set; } = string.Empty;

        public decimal price { get; set; }

        public string image { get; set; } = string.Empty;

        public List<FoodCustomizationDto> foodCustomization { get; set; } = new List<FoodCustomizationDto>();
    }

    public class FoodCustomizationDto
    {
        public string name { get; set; } = string.Empty;

        public decimal price { get; set; }
    }
}
