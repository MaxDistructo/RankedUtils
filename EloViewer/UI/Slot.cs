using UnityEngine.UIElements;

namespace RankedUtils.EloViewer.UI
{
    public class Slot : VisualElement
    {
        public Image Icon;
        public string Description;
        public Slot()
        {
            Icon = new();
            Add(Icon);

            Icon.AddToClassList("slotIcon");
            AddToClassList("slotContainer");
        }
    }
}
