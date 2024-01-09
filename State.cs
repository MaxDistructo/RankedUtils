using Server.Shared.Info;
using RankedUtils.EloViewer;
using Home.HomeScene;
using System.Runtime.CompilerServices;
using SML;

namespace RankedUtils
{
    public class State
    {
        private static bool isInitalized = false;
        private static GameType lastGameMode;
        public static bool hasRegisteredAction = false;
        public static UserStatistics userStatistics = new UserStatistics();
        public static HomeSceneController homeSceneController = null;
        public static UnityEngine.Sprite sprite;
        public static void Init()
        {
            if (!isInitalized)
            {
                isInitalized = true;
                lastGameMode = GameType.None;
                sprite = FromResources.LoadSprite("RankedUtils._modinfo.RankedUtils.resources.images.thumbnail.png");
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
