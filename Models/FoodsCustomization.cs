using System;
using System.Collections.Generic;

namespace Foods.Models;

public partial class FoodsCustomization
{
    public Guid FoodCustomizationId { get; set; }

    public string FoodCustomizationName { get; set; } = null!;

    public decimal FoodCustomizationPrice { get; set; }

    public DateTime? FoodCustomizationLastCreated { get; set; }

    public DateTime? FoodCustomizationLastUpdated { get; set; }

    public bool? FoodCustomizationIsDeleted { get; set; }

    public Guid FoodId { get; set; }

    public virtual Food Food { get; set; } = null!;
}
