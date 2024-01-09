using HarmonyLib;
using Home.HomeScene;
using Server.Shared.Info;
using SML;
using System.Reflection;

namespace RankedUtils.AutoRejoinRanked
{
    //We patch the HomeScene controller to setup our data storage state and joining a gamemode once we are sent back here by the GameScene controller patch
    [HarmonyPatch(typeof(HomeSceneController), "Start")]
    public class HomeSceneControllerStartPatch
    {
        [HarmonyPrefix]
        public static void Postfix(HomeSceneController __instance)
        {
            State.Init();
            if (State.getLastGameMode() == GameType.Ranked || ModSettings.GetBool("Use for all Game Modes", "maxdistructo.RankedUtils"))
            {
                //Use the built in methods to set us to be a ranked game then click join for the user.
                MethodInfo methodInfo = typeof(HomeSceneController).GetMethod("SetSelectedGameMode", BindingFlags.NonPublic | BindingFlags.Instance);
                var parameters = new object[] { State.getLastGameMode() };
                methodInfo.Invoke(__instance, parameters);
                __instance.HandleClickJoinSelectedGameMode();
                State.setLastGameMode(GameType.None);
                //Let AutoAcceptRanked take over from here in the RankedQueueController
            }
            State.homeSceneController = __instance;
        }
    }
}
