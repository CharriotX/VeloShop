namespace Data.Interface.DataModels.Helpers
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNextPage => PageNumber * PageSize < TotalRecords;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
