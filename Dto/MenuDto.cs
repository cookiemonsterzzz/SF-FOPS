namespace Foods.Dto
{
    public class MenuDto
    {
        public string stallName { get; set; }

        public int rating { get; set; }

        public List<FoodDto> foods { get; set; }
    }

    public class FoodDto
    {
        public string foodName { get; set; }

        public decimal price { get; set; }

        public string image { get; set; }

        public List<FoodCustomizationDto> foodCustomization { get; set; }
    }

    public class FoodCustomizationDto
    {
        public string name { get; set; }

        public decimal price { get; set; }
    }
}

