using AutoMapper;
using Erox.Application.Admin.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AdminController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("TotalSales")]
        public async Task<IActionResult> TotalSales(bool getTodayCount = true, bool getThisWeekCount = true, bool getThisMonthCount = true, bool getTotalCount = true)
        {
            var query = new TotalSalesQuery
            {
                GetTodayCount = getTodayCount,
                GetThisWeekCount = getThisWeekCount,
                GetThisMonthCount = getThisMonthCount,
                GetTotalCount = getTotalCount
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
      
    }
}
