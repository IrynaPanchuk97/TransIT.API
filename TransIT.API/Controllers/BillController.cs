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
    public class BillController : DataController<Bill, BillDTO>
    {
        private IBillService _billService;
        
        public BillController(IMapper mapper, IBillService billService) : base(mapper, billService)
        {
            _billService = billService;
        }
    }
}
