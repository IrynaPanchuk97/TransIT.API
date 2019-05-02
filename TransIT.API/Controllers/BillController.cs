using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,CUSTOMER,ANALYST")]
    public class BillController : DataController<Bill, BillDTO>
    {
        private readonly IBillService _billService;
        
        public BillController(
        IMapper mapper, 
        IBillService billService,
        IODCrudService<Bill> odService
        ) : base(mapper, billService, odService)
        {
            _billService = billService;
        }
    }
}
