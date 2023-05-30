using System.Runtime.Serialization;

namespace UserManagementApi.DataObject
{
    public enum SortOption { Ascending , Descending};

    [DataContract]
    public class SortFilterData
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
        public SearchFilterData FilterOptions { get; set; }
    }
}