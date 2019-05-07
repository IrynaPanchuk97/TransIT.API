namespace TransIT.DAL.Models.ViewModels
{
    public class DataTableRequestViewModel
    {
        public const string DataTableAscending = "asc";
        public const string DataTableDescending = "desc";
        
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SearchType Search { get; set; }
        public ColumnType[] Columns { get; set; }
        public OrderType[] Order { get; set; }

        public class OrderType
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }

        public class SearchType
        {
            public string Value { get; set; }
            public bool Regex { get; set; }
        }

        public class ColumnType
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public string Data { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public SearchType Search { get; set; }
        }
    }
}
