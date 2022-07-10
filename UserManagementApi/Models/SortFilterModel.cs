using System.Runtime.Serialization;

namespace UserServices
{
    public enum SortOption { Ascending , Descending};

    [DataContract]
    public class SortFilterModel
    {
        [DataMember]
        public string SortOrder { get; set; }

        [DataMember]
        public SortOption SortDirection { get; set; }

        [DataMember]
        public string SearchText { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public SearchFilterModel FilterOptions { get; set; }
    }
}