using CustomerRankService.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        public CustomerController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpPost("{customerId}/score/{score}")]
        /// <summary>
        /// Update score
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="score">Score to update</param>
        /// <returns></returns>
        public IActionResult UpdateScore([FromRoute] long customerId, [FromRoute] decimal score)
        {
            if (customerId < 0 || score < -1000 || score > 1000)
            {
                return BadRequest();
            }

            var rankRecord = _leaderboardService.UpdateScore(customerId, score);
            return Ok(rankRecord);
        }
    }
}
