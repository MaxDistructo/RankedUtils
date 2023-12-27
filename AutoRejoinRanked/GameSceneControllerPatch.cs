using Game;
using HarmonyLib;
using Home.Shared.Enums;
using Home.Shared;
using Server.Shared.Info;
using Server.Shared.Messages;
using Server.Shared.State;
using Services;
using SML;

namespace RankedUtils.AutoRejoinRanked
{
    //GameSceneController is updated every time the game changes phase.
    //We detect if it's the post game by asking if the lobby is ShuttingDown (which is ONLY post game in a ranked match)
    //This triggers us to do what the settings menu does to send us back to home where the other patch takes over and sets us back into a new match.
    [HarmonyPatch(typeof(GameSceneController), "HandleGameInfoChanged")]
    public class GameSceneControllerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(GameSceneController __instance, GameInfo gameInfo)
        {
            System.Console.WriteLine("[AutoRejoinRanked] PATCH ENTRY (GameSceneController)");
            if (__instance == null)
            {
                System.Console.WriteLine("Instance is null");
                return;
            }
            //If we are in the post game of the lobby (aka it's shutting down in 60 seconds), requeue the player
            if (Service.Game.Sim.info.gameMode.Data.gameType == GameType.Ranked && Service.Game.Sim.info.lobby.Data.isShuttingDown)
            {
                State.setLastGameMode(Service.Game.Sim.info.gameMode.Data.gameType);
                //Leave the current game to return to the lobby.
                ApplicationController.ApplicationContext.pendingTransitionType = CutoutTransitionType.NONE;
                Service.Game.Network.Send((GameMessage)new RemovePlayerFromCellMessage(RemovedFromGameReason.EXIT_TO_MAIN_MENU, false));
                //Once loaded into the main screen, if the last game mode was Ranked, set it there.
            }
            //In other game modes, the lobby does not end but instead restarts. We check if the restart timer is running and if so, kick the player out and cause the requeue.
            else if (ModSettings.GetBool("Use for all Game Modes", "maxdistructo.RankedUtils") && Service.Game.Sim.info.lobby.Data.restartTimer.GetWholeSecondsRemaining() > 0)
            {
                State.setLastGameMode(Service.Game.Sim.info.gameMode.Data.gameType);
                //Leave the current game to return to the lobby.
                ApplicationController.ApplicationContext.pendingTransitionType = CutoutTransitionType.NONE;
                Service.Game.Network.Send((GameMessage)new RemovePlayerFromCellMessage(RemovedFromGameReason.EXIT_TO_MAIN_MENU, false));
                //Once loaded into the main screen, if the last game mode was Ranked, set it there.
            }
            return;
        }
    }
}
