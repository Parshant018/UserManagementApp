namespace UserManager.Models
{
    public class SortFilterModel
    {
        public string SortOrder { get; set; }
        public int SortDirection { get; set; }
        public string SearchText { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public SearchFilterModel FilterOptions { get; set; } = new SearchFilterModel();
    }
}