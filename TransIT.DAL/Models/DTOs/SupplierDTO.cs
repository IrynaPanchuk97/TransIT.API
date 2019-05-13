using System;

namespace TransIT.DAL.Models.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public int? Country { get; set; }
        public int? Currency { get; set; }
        public string Edrpou { get; set; }
    }
}
