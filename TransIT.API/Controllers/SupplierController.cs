using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class SupplierController : DataController<Supplier, SupplierDTO>
    {
        private readonly ISupplierService _supplierService;
        
        public SupplierController(
            IMapper mapper, 
            ISupplierService supplierService,
            IFilterService<Supplier> odService
            ) : base(mapper, supplierService, odService)
        {
            _supplierService = supplierService;
        }
        
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] SupplierDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] SupplierDTO obj)
        {
            return base.Update(id, obj);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
