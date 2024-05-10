﻿namespace Data.Interface.Models
{
    public class ProductSpecification : BaseModel
    {
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public string Value { get; set; }
    }
}
