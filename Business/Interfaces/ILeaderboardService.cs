using CustomerRankService.Models;

namespace CustomerRankService.Business.Interfaces
{
    public interface ILeaderboardService
    {
        RankRecord UpdateScore(long customerId, decimal increment);

        List<RankRecord> GetCustomersByRank(int start, int end);

        List<RankRecord> GetCustomersWithNeighbors(long customerId, int low, int high);
    }
}
