using System;
using System.Collections.Generic;

namespace Foods.Models;

public partial class Food
{
    public Guid FoodId { get; set; }

    public string FoodName { get; set; } = null!;

    public string? FoodDescription { get; set; }

    public decimal FoodPrice { get; set; }

    public DateTime? FoodLastCreated { get; set; }

    public DateTime? FoodLastUpdated { get; set; }

    public bool? FoodIsDeleted { get; set; }

    public virtual ICollection<FoodsCustomization> FoodsCustomizations { get; set; } = new List<FoodsCustomization>();
}
