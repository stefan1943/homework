using CustomerRankService.Business.Interfaces;
using CustomerRankService.Memory;
using CustomerRankService.Models;

namespace CustomerRankService.Business.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        public RankRecord UpdateScore(long customerId, decimal increment)
        {
            var newRecord = !MemoryHelper.CustomerIdSet.Contains(customerId)
                ? MemoryHelper.Add(customerId, increment)
                : MemoryHelper.Update(customerId, increment);
            MemoryHelper.CustomerIdSet.Add(customerId);
            return newRecord;
        }


        public List<RankRecord> GetCustomersByRank(int start, int end) //Rank is index plus 1 
            => MemoryHelper.Leaderboard.GetRange(start - 1, end - start + 1);


        public List<RankRecord> GetCustomersWithNeighbors(long customerId, int low, int high)
        {
            if (!MemoryHelper.CustomerIdSet.Contains(customerId))
            {
                return new();
            }
            var leaderboard = MemoryHelper.Leaderboard;
            var currentIndex = leaderboard.FindIndex(p => p.CustomerId == customerId);
            int start = currentIndex - low, end = currentIndex + high;
            if (start < 0) start = 0;
            if (end > leaderboard.Count - 1) end = leaderboard.Count - 1;
            return leaderboard.GetRange(start < 0 ? 0 : start, end - start + 1);
        }

    }
}
