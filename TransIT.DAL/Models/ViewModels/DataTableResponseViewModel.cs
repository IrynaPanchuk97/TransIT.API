namespace TransIT.DAL.Models.ViewModels
{
    public class DataTableResponseViewModel
    {
        public int Draw { get; set; }
        public long RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object[][] Data { get; set; }
        public string Error { get; set; }
    }
}
