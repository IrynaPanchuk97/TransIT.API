using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,CUSTOMER,ANALYST")]
    public class SupplierController : DataController<Supplier, SupplierDTO>
    {
        private ISupplierService _supplierService;
        
        public SupplierController(IMapper mapper, ISupplierService supplierService) : base(mapper, supplierService)
        {
            _supplierService = supplierService;
        }
    }
}
