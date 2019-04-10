using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ActionTypeController : DataController<ActionType, ActionTypeDTO>
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypeController(IMapper mapper, IActionTypeService actionType) : base(mapper, actionType)
        {
            _actionTypeService = actionType;
        }

        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            return base.Get(offset, amount);
        }
    }
}
