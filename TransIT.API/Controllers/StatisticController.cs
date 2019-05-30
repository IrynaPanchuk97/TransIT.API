using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ANALYST")]
    public class StatisticController : Controller
    {
        protected readonly IStatisticService _statisticService;
        protected readonly IMapper _mapper;

        public StatisticController(IStatisticService statisticService, IMapper mapper)
        {
            _statisticService = statisticService;
            _mapper = mapper;
        }


        [HttpGet("group")]
        public virtual IActionResult GetGroupInfo() =>
            Json(
                     //_mapper.Map<IEnumerable<VehicleTypeMalfunctionGroupViewModel>>(
                     _statisticService.GetStatisticGroup().Select(x => new VehicleTypeMalfunctionGroupViewModel {
                         VehicleType = _mapper.Map<VehicleTypeDTO>(x.VehicleType),
                         Group = _mapper.Map <MalfunctionGroupDTO>(x.Group),
                         Count = x.Count })
                    //)
                );

        [HttpGet("group/{id}/subgroup")]
        public virtual async Task<IActionResult> GetSubgroupInfo(int id) =>
             Json(
                _mapper.Map<IEnumerable<VehicleTypeMalfunctionSubGroupViewModel>>(
                    await _statisticService.GetStatisticSubGroup(id)
                    )
                );



    }
}
