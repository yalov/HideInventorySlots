using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HideInventorySlots
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, false)]
    public class HideInventorySlots : MonoBehaviour
    {
        public void Start()
        {
            GameEvents.onPartActionUIShown.Add(OnPartActionWindowCreated);
        }

        private void OnPartActionWindowCreated(UIPartActionWindow window, Part part)
        {
            List<UIPartActionItem> items = window.ListItems.Where(item => item.PartModule is ModuleInventoryPart).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                ModuleInventoryPart moduleInventoryPart = items[i].PartModule as ModuleInventoryPart;
                BasePAWGroup group = new BasePAWGroup($"{i}", $"Inventory {(items.Count == 1 ? string.Empty : $"{i}")}", true);
                moduleInventoryPart.Fields["InventorySlots"].group = group;
            }

            window.displayDirty = true;
        }

        public void OnDestroy()
        {
            GameEvents.onPartActionUIShown.Remove(OnPartActionWindowCreated);
        }
    }
}
