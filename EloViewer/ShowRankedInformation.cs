using Game;
using HarmonyLib;
using Home.Common;
using Home.HomeScene;
using Server.Shared.Info;
using Server.Shared.State;
using Services;
using SML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RankedUtils.EloViewer
{
    //Another HomeSceneController patch? YUP! Set the ranked information for the user to show in the Ranked text.
    //[HarmonyPatch(typeof(HomeSceneController), "SetSelectedGameMode")]
    public class ShowRankedInformation
    {
        //[HarmonyPrefix]
        public static bool Prefix(HomeSceneController __instance, GameType a_gameType, ref GameType ___gameModeSelection)
        {
                //THIS IS NOT CUSTOM BUT FROM THE ORIGINAL METHOD!!!!! REQUIRED FOR US TO TAKE OVER THE GAME TEXT!
                __instance.joinWithCodeObj.SetActive(false);
                __instance.joinSelectedGameModeButton.gameObject.SetActive(true);
                //this.ValidateJoinSelectedGameModeButtonState();
                MethodInfo methodInfo = typeof(HomeSceneController).GetMethod("ValidateJoinSelectedGameModeButtonState", BindingFlags.NonPublic | BindingFlags.Instance);
                var parameters = new object[] { };
                methodInfo.Invoke(__instance, parameters);
                __instance.gameModeListView.gameObject.SetActive(false);
                ___gameModeSelection = a_gameType;
                methodInfo.Invoke(__instance, parameters);
                __instance.gameModDescPopup.SetActive(true);
                //__instance.ValidateClassicButtons();
                MethodInfo methodInfo2 = typeof(HomeSceneController).GetMethod("ValidateClassicButtons", BindingFlags.NonPublic | BindingFlags.Instance);
                var parameters2 = new object[] { };
                methodInfo2.Invoke(__instance, parameters2);
                RoleDeckBuilder roleDeckBuilder = new RoleDeckBuilder();
                RoleListData roleListData = Service.Home.GameMode.GetRoleListData(a_gameType);
                if (roleListData != null)
                {
                    roleDeckBuilder.bannedRoles = roleListData.bans;
                    roleDeckBuilder.modifierCards = roleListData.hostOptions;
                    roleDeckBuilder.roles = roleListData.roles;
                }
                if (roleDeckBuilder.roles.Count > 0)
                {
                    __instance.gameModeListView.gameObject.SetActive(true);
                    __instance.gameModeListView.SetData(roleDeckBuilder);
                }
            //END ORIGINAL CODE!
            if (___gameModeSelection == GameType.Ranked)
            {
                //Sadly, the game early returns where we want to set this text to exist and overwrites before we hit a return. As such, we need to do what the game does, set it, and return.
                var temp = __instance.l10n("GUI_GAME_TYPE_DESC_" + ((int)___gameModeSelection).ToString());
                temp += "\n\n";
                //var temp = "";
                Console.WriteLine("ELO: " + State.GetELO() + ", SeasonHigh: " + State.userStatistics.getSeasonHigh());
                //You're in placements. Don't reveal their ELO till they have completed placements (even though we know this information)
                //This is compliance related.
                if (Service.Home.RankedService.RankedResultsInfo.Get().PlacementGamesRequired > 0)
                {
                    temp += "Placement Games Required: " + Service.Home.RankedService.RankedResultsInfo.Get().PlacementGamesRequired;
                }
                else
                {
                    temp += "W/L/D:" + State.userStatistics.getRankedWins() + "/" + State.userStatistics.getRankedLosses() + "/" + State.userStatistics.getRankedDraws();
                    temp += "\n";
                    temp += "Rank: " + __instance.l10n(Service.Home.RankedService.GetRankByElo(State.GetELO()).LocKey);
                    temp += "\n";
                    temp += "ELO: " + State.GetELO();
                    temp += "\n";
                    temp += "Season High: " + State.userStatistics.getSeasonHigh();
                    temp += "\n";
                    temp += "Next Rank At: " + (Service.Home.RankedService.GetRankByElo(State.GetELO()).MaximumElo + 1);
                    temp += "\n";
                }
                Console.WriteLine("Setting text to: " + temp);
                __instance.gameModDescText.SetText(temp);
                return false;
            }
            return true;
        }
    }
}
