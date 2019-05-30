using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
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
        public virtual async Task<IActionResult> GetGroupInfo() =>
            Json(
                _mapper.Map<IEnumerable<VehicleTypeMalfunctionGroupViewModel>>(
                    await _statisticService.GetStatisticGroup()
                    )
                );

        [HttpGet("group/{id}/subgroup")]
        public virtual async Task<IActionResult> GetSubgroupInfo(int id) =>
             Json(
                _mapper.Map<IEnumerable<VehicleTypeMalfunctionGroupViewModel>>(
                    await _statisticService.GetStatisticSubGroup(id)
                    )
                );



    }
}
