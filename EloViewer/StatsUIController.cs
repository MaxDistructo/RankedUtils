using Home.Common;
using TMPro;

namespace RankedUtils.EloViewer
{
    //This is based on PatchNotesController though we want to do our own thing
    public class StatsUIController : UIController
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI message;

        public void ShowPopup(string title, string message) 
        {
            this.gameObject.SetActive(true);
            this.title.SetText(title);
            this.message.SetText(message);
        }

        public void HandleOnCloseButtonClick()
        {
            this.PlaySound("Audio/UI/ClickSound.wav");
            this.Close();
        }

        public void Close() => UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject);
    }
}
