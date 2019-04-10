using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class ActionTypeController : DataController<ActionType, ActionTypeDTO>
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypeController(IMapper mapper, IActionTypeService actionType) : base(mapper, actionType)
        {
            _actionTypeService = actionType;
        }
    }
}
