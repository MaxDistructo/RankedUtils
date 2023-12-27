using Home.Common;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using SML;
using System.Reflection;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using BMG.UI;
using SalemModLoader;
using Mono.Cecil;

namespace RankedUtils.EloViewer
{
    //UI Button Patch for the Ranked Stats Menu
    //Thanks Curtis for a baseline of what we need to do to add ourselves to the menu. See SalemModLoader/AddHomeSceneButtons
    [HarmonyPatch(typeof(SafeAreaController), "OnEnable")]
    public class SafeAreaControllerPatch
    {
        [HarmonyPostfix]
        public static void Postfix(SafeAreaController __instance)
        {
            if (SceneManager.GetActiveScene().name != "HomeScene")
            {
                return;
            }
            GameObject aboveObj = __instance.transform.Find("LeftButtons/Personalize/").gameObject;
            GameObject belowObj = __instance.transform.Find("LeftButtons/Mod Actions").gameObject;
            GameObject ourObj = Object.Instantiate<GameObject>(aboveObj, aboveObj.transform.parent);
            GameObject gameModeChoiceElementsUI = __instance.transform.Find("GameModeChoiceElementsUI").gameObject;
            //Create our submenu of stats we can display
            GameObject statsElements = Object.Instantiate<GameObject>(gameModeChoiceElementsUI, __instance.transform);
            Object.Destroy((Object)ourObj.transform.Find("Text").GetComponent<TMProLocalizedTextController>());
            statsElements.transform.SetSiblingIndex(__instance.transform.Find("GameModeChoiceElementsUI").GetSiblingIndex());
            statsElements.transform.Find("GameModePanel/GameBrowserRoleListElementsUI").gameObject.SetActive(false);
            statsElements.transform.Find("GameModePanel/LobbyDescriptionPanel").gameObject.SetActive(false);
            statsElements.SetActive(false);
            ourObj.transform.SetSiblingIndex(aboveObj.transform.GetSiblingIndex());
            ourObj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "User Statistics";
            ourObj.transform.Find("Icon").GetComponent<Image>().sprite = FromResources.LoadSprite("RankedUtils.resources.images.Logo128.png", Assembly.GetExecutingAssembly());
            ourObj.name = "User Statistics";
            ourObj.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
            ourObj.AddComponent<PlayClickSoundOnPointerClick>();
            ourObj.GetComponent<Button>().onClick.AddListener((UnityAction)(() =>
            {
                statsElements.SetActive(true);
                gameModeChoiceElementsUI.SetActive(false);
            }));
            belowObj.GetComponent<Button>().onClick.AddListener((UnityAction)(() => statsElements.SetActive(false)));

            //Add our buttons now

        }
        public static void AddModActions(GameObject modActionsChoice)
        {
            GameObject gameObject1 = modActionsChoice.transform.Find("GameModePanel/GameModeChoicePanel").gameObject;
            GameObject gameObject2 = gameObject1.transform.Find("Classic").gameObject;
            gameObject2.SetActive(false);
            Object.Destroy((Object)gameObject2.GetComponentInChildren<TMProLocalizedTextController>());
            foreach (Transform transform in gameObject1.transform)
            {
                if (transform.name != "Classic" && transform.name != "ShadowFrame")
                    Object.Destroy((Object)transform.gameObject);
            }
            createObject(gameObject2, "Overall Statistics", "RankedUtils.RankedIcon.png", () => { });
            createObject(gameObject2, "Ranked Statistics", "RankedUtils.RankedIcon.png", () => { });
            gameObject1.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        }

        public static void createObject(GameObject parentObject, string buttonName, string iconPath, UnityAction onClick) 
        {
            GameObject gameObject3 = Object.Instantiate<GameObject>(parentObject, parentObject.transform.parent);
            gameObject3.transform.SetSiblingIndex(parentObject.transform.GetSiblingIndex() + 1);
            gameObject3.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
            gameObject3.transform.Find("Icon").GetComponent<Image>().sprite = FromResources.LoadSprite(iconPath, Assembly.GetExecutingAssembly()); 
            Object.Destroy((Object)gameObject3.GetComponent<BMG_Button>());
            gameObject3.AddComponent<Button>().onClick.AddListener(onClick);
            gameObject3.SetActive(true);
        }
    }
}
