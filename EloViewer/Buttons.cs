using Services;
using SML;

namespace RankedUtils.EloViewer
{
    
    public class Buttons
    {
        public static StatsUIController StatsUIControllerPrefab;
        public static StatsUIController statsUIControllerInstance;
        public static void ShowStatsMessage(string title, string message)
        {
            if ((UnityEngine.Object)statsUIControllerInstance != (UnityEngine.Object)null)
                statsUIControllerInstance.Close();
            //Nullchecks
            if (StatsUIControllerPrefab == null)
            {
                System.Console.WriteLine("Prefab is null");
                StatsUIControllerPrefab = new();
                System.Console.WriteLine("Fixing null issue!");
            }
            if (State.homeSceneController == null)
            {
                System.Console.WriteLine("Homescene is null");
                return;
            }
            if (State.homeSceneController.SafeArea == null)
            {
                System.Console.WriteLine("SafeArea is null");
                return;
            }
            statsUIControllerInstance = State.homeSceneController.CreatePrefab<StatsUIController>(StatsUIControllerPrefab, State.homeSceneController.SafeArea);
            if ((UnityEngine.Object)statsUIControllerInstance != (UnityEngine.Object)null)
                statsUIControllerInstance.ShowPopup(title, message);
        }
    }
}
