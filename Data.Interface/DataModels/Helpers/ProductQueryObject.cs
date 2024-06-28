namespace Data.Interface.DataModels.Helpers
{
    public class ProductQueryObject
    {
        public string? SearchTerm { get; set; } = null;
        public string? SearchBrand { get; set; } = null;
        public string? SortColumn { get; set; } = null;
        public string? SortOrder { get; set; } = "asc";
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }
}
