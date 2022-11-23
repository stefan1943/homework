using CustomerRankService.Models;

namespace CustomerRankService.Memory
{
    public static class MemoryHelper
    {
        /// <summary>
        /// The real time leaderboard for reading
        /// </summary>
        public static List<RankRecord> Leaderboard { get; set; } = new ();


        /// <summary>
        /// For modification
        /// </summary>
        public static LinkedList<BaseData> LeaderboardLink { get; set; } = new ();


        /// <summary>
        /// For one less traversal of the leaderboard link
        /// </summary>
        public static HashSet<long> CustomerIdSet { get; set; } = new();


        #region Update leaderboard
        public static RankRecord Add(long customerId, decimal score)
        {
            var current = LeaderboardLink.First;
            if (current == null)
            {
                return Init(customerId, score);
            }

            int counter = 0;
            bool hasUpdated = false;
            RankRecord currentRecord = new() { CustomerId = customerId, Score = score };
            //Reset the 'real time' leaderboard
            Leaderboard = new();

            while (current != null)
            {
                var value = new BaseData { CustomerId = customerId, Score = score };
                if (current.Value.Score > score)
                {
                    AppendRecord(current.Value.CustomerId, current.Value.Score, ++counter);

                    if (current.Next != null)
                    {
                        current = current.Next;
                        continue;
                    }
                    //The tail node
                    LeaderboardLink.AddLast(value);
                    return AppendRecord(customerId, score, ++counter);
                }
                //Same score, lower(customer id) is first
                else if (current.Value.Score == score && current.Value.CustomerId > customerId
                        || current.Value.Score < score)
                {
                    if (!hasUpdated)
                    {
                        LeaderboardLink.AddBefore(current, value);
                        currentRecord = AppendRecord(customerId, score, ++counter);
                        hasUpdated = true;
                    }
                }
                AppendRecord(current.Value.CustomerId, current.Value.Score, ++counter);
                //Tail node
                if (current.Next == null&& !hasUpdated)
                {
                    LeaderboardLink.AddAfter(current, value);
                    currentRecord = AppendRecord(customerId, score, ++counter);
                    return currentRecord;
                }
                current = current.Next;
                
            }
            return currentRecord;
        }

        public static RankRecord Update(long customerId, decimal increment)
        {
            decimal socre = 0;
            var current = LeaderboardLink.First;
            while (current != null)
            {
                if (current.Value.CustomerId != customerId)
                {
                    current = current.Next;
                    continue;
                }

                socre = current.Value.Score + increment;

                LeaderboardLink.Remove(current);
                break;
            }

            return Add(customerId, socre);
        }


        private static RankRecord AppendRecord(long customerId, decimal score, int rank)
        {
            RankRecord record = new()
            {
                CustomerId = customerId,
                Score = score,
                Rank = rank
            };
            Leaderboard.Add(record);
            return record;
        }

        private static RankRecord Init(long customerId, decimal score)
        {
            LeaderboardLink.AddLast(new BaseData()
            {
                CustomerId = customerId,
                Score = score
            });

            RankRecord record = new()
            {
                CustomerId = customerId,
                Score = score,
                Rank = 1
            };
            Leaderboard.Add(record);
            return record;
        }

        #endregion
    }
}
