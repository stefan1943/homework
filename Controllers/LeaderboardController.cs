using CustomerRankService.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Net;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }



        /// <summary>
        /// Get customer by rank range
        /// </summary>
        /// <param name="start">Starting position</param>
        /// <param name="end">Ending position</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCustomersByRank([FromQuery] int start, [FromQuery] int end)
        {
            if (start > end)
            {
                return BadRequest();
            }

            var customers = _leaderboardService.GetCustomersByRank(start, end);

            return customers.Count > 0 ? Ok(customers) : NoContent();
        }



        /// <summary>
        /// Get customers wiht (low or high) neighbors
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="low"</param>
        /// <param name="high"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public IActionResult GetCustomersWithNeighbors([FromRoute] long customerId,
            [FromQuery] int low = 0, [FromQuery] int high = 0)
        {
            var customers = _leaderboardService.GetCustomersWithNeighbors(customerId, low, high);

            return customers.Count > 0 ? Ok(customers) : NoContent();

        }
    }
}
