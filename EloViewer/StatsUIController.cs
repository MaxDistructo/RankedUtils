using Home.Common;
using TMPro;

namespace RankedUtils.EloViewer
{
    //This is based on PatchNotesController though we want to do our own thing
    public class StatsUIController : UIController
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI message;

        public void ShowPopup(string message) 
        {
            this.gameObject.SetActive(true);
            this.title.SetText("Ranked Profile");
            this.message.SetText(message);
        }

        public void HandleOnCloseButtonClick()
        {
            this.PlaySound("Audio/UI/ClickSound.wav");
        }

        public void Close() => UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject);
    }
}
