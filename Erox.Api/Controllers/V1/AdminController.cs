using AutoMapper;
using Erox.Application.Admin.QueriesHandler;
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
        private readonly TopSalesHandler _topSalesHandler;
        public AdminController(IMediator mediator, IMapper mapper, TopSalesHandler topSalesHandler)
        {
            _mediator = mediator;
            _mapper = mapper;
            _topSalesHandler = topSalesHandler;
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

        [HttpGet]
        [Route("top-selling-products")]
        public async Task<IActionResult> GetTopSellingProducts([FromQuery] int topCount = 10)
        {
            var topProducts = await _topSalesHandler.GetTopSellingProductsAsync(topCount);
            return Ok(topProducts);
        }

        [HttpGet]
        [Route("revenue-by-period")]
        public async Task<IActionResult> GetRevenueByPeriod([FromQuery] PeriodType periodType)
        {
            
            var query = new RevenueByPeriodQuery(periodType);

            
            var revenue = await _mediator.Send(query);

            return Ok(new { Revenue = revenue });
        }

    }
}
