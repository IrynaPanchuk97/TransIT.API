namespace TransIT.DAL.Models.ViewModels
{
    public class DataTableResponseViewModel
    {
        public ulong Draw { get; set; }
        public ulong RecordsTotal { get; set; }
        public ulong RecordsFiltered { get; set; }
        public object[] Data { get; set; }
        public string Error { get; set; }
    }
}
