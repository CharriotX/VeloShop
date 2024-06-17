using Data.Interface.DataModels.Products;

namespace Data.Interface.DataModels.Subcategories
{
    public class SubcategoryIdPagePesponse
    {
        public SubcategoryData Subcategory { get; set; }
        public List<ProductData> Products { get; set; } 
    }
}
