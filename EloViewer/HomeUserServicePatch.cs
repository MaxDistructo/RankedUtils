using Home.Data;
using Home.Messages.Incoming;
using Home.Services;
using Newtonsoft.Json.Linq;
using HarmonyLib;
using System.Text;
namespace RankedUtils.EloViewer
{
    [HarmonyPatch(typeof(HomeUserService), "OnUserInformation")]
    public class UserInfoHandlerPatch
    {
        //The Devs currently do not decode or use the ELO field in the recieved data so we
        //catch and store this ourselves. Hopefully this becomes a part of the UserInformation object
        //eventually. Would make this less hacky.
        [HarmonyPostfix]
        public static void Postfix(HomeUserService __instance, IncomingHomeMessage msg)
        {
            var data = ((IncomingPayloadMessage<UserInformation, UserInformationPayload>)msg).Bytes;
            var json_str = Encoding.UTF8.GetString(data);
            var json = JObject.Parse(json_str);
            State.SetELO((int)json.GetValue("ELO"));
            State.userStatistics.setUserInformation(((IncomingPayloadMessage<UserInformation, UserInformationPayload>)msg).Data);
        }
    }
    [HarmonyPatch(typeof(HomeUserService), "HandleRankedResultsInfo")]
    public class RankedResultsInfoHandlerPatch
    {
        //The Devs currently do not decode or use the ELO field in the recieved data so we
        //catch and store this ourselves. Hopefully this becomes a part of the UserInformation object
        //eventually. Would make this less hacky.
        [HarmonyPostfix]
        public static void Postfix(HomeUserService __instance, IncomingHomeMessage msg)
        {
            State.userStatistics.setRankedInfo((msg as RankedResultsInfoMessage).Data);
        }
    }
    [HarmonyPatch(typeof(HomeUserService), "HandleUserStatsMessage")]
    public class HandleUserStatsMessagePatch
    {
        //The Devs currently do not decode or use the ELO field in the recieved data so we
        //catch and store this ourselves. Hopefully this becomes a part of the UserInformation object
        //eventually. Would make this less hacky.
        [HarmonyPostfix]
        public static void Postfix(HomeUserService __instance, IncomingHomeMessage msg)
        {
            State.userStatistics.setUserStats((msg as UserStatsMessage).Data);
        }
    }
}
