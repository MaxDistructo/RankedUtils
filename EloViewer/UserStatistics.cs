using Home.Data;
using Home.Messages.Incoming;

namespace RankedUtils.EloViewer
{
    //Internal Mod Storage of all UserStatistics we may want
    //Using this incase any of the fields the game gives us are not wanted.
    public class UserStatistics
    {
        private UserInformation userInformation;
        private UserStats userStats;
        private RankedResultsInfoPayload rankedInfo;
        private int elo;

        public void setUserInformation(UserInformation userInformation) 
        { 
            this.userInformation = userInformation; 
        }
        public void setUserStats(UserStats userStats) 
        {
            this.userStats = userStats;
        }
        public void setRankedInfo(RankedResultsInfoPayload rankedInfo) 
        {
            this.rankedInfo = rankedInfo;
        }
        public void setElo(int elo)
        {
            this.elo = elo;
        }
        public UserInformation GetUserInformation()
        {
            return userInformation;
        }
        public int getElo()
        {
            if (rankedInfo != null)
            {
                return rankedInfo.CurrentELO;
            }
            else 
            {
                return elo;
            }
        }
        public int getRankedGamesPlayed()
        {
            if (userStats != null)
            {
                return userStats.RankedGamesPlayed;
            }
            else 
            {
                return 0;
            }
        }
        public int getRankedWins() 
        {
            if (userStats != null)
            {
                return userStats.RankedGamesWon;
            }
            else
            {
                return 0;
            }
        }
        public int getRankedLosses()
        {
            if (userStats != null)
            {
                return userStats.RankedGamesLost;
            }
            else
            {
                return 0;
            }
        }
        public int getRankedDraws()
        {
            if (userStats != null)
            {
                return userStats.RankedGamesDrawn;
            }
            else
            {
                return 0;
            }
        }
        public int getSeasonHigh()
        {
            if (rankedInfo != null)
            {
                return rankedInfo.SeasonHighELO;
            }
            else 
            {
                return 0;
            }
        }

    }
}
