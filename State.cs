using Home.Messages.Incoming;
using Server.Shared.Info;
using RankedUtils.EloViewer;

namespace RankedUtils
{
    public class State
    {
        private static bool isInitalized = false;
        private static GameType lastGameMode;
        public static bool hasRegisteredAction = false;
        public static UserStatistics userStatistics = new UserStatistics();
        public static void Init()
        {
            if (!isInitalized)
            {
                isInitalized = true;
                lastGameMode = GameType.None;
            }
        }
        public static GameType getLastGameMode() { return lastGameMode; }
        public static void setLastGameMode(GameType gameMode) { lastGameMode = gameMode; }

        public static void SetELO(int elo)
        {
            userStatistics.setElo(elo);
        }
        public static int GetELO()
        {
            return userStatistics.getElo();
        }
    }
}
