using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RankedUtils.EloViewer.UI
{
   public class SlotUIController : MonoBehaviour
    {
        public List<Slot> Slots = new List<Slot>();
        private VisualElement m_Root;
        private VisualElement m_SlotContainer;

        private void Awake()
        {
            //Store the root from the UI Document component
            m_Root = GetComponent<UIDocument>().rootVisualElement;

            //Search the root for the SlotContainer Visual Element
            m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");

            //Create InventorySlots and add them as children to the SlotContainer
            
        }
    }
}
