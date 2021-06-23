using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KSP.Localization;

namespace HideInventorySlots
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, false)]
    public class HideInventorySlots : MonoBehaviour
    {
        public void Start()
        {
            GameEvents.onPartActionUIShown.Add(OnPartActionWindowCreated);
            GameEvents.onModuleInventoryChanged.Add(OnModuleInventoryChanged);
        }


        private void OnModuleInventoryChanged(ModuleInventoryPart moduleInventoryPart)
        {
            //Debug.Log("OnModuleInventoryChanged");
            String name = moduleInventoryPart.Fields["InventorySlots"].group.name;

            UIPartActionWindow window = UIPartActionController.Instance.GetItem(moduleInventoryPart.part, false);
            if (window != null)
            {
                UIPartActionGroup group = window.parameterGroups[name];
                group.Initialize(name, GroupDisplayName(moduleInventoryPart), false, window);
            }
        }


        private void OnPartActionWindowCreated(UIPartActionWindow window, Part part)
        {
            List<UIPartActionItem> items = window.ListItems.Where(item => item.PartModule is ModuleInventoryPart).ToList();
            for (int i = 0; i < items.Count; i++)
            {
                ModuleInventoryPart moduleInventoryPart = items[i].PartModule as ModuleInventoryPart;

                BasePAWGroup group = new BasePAWGroup($"{i}", GroupDisplayName(moduleInventoryPart), false);
                moduleInventoryPart.Fields["InventorySlots"].group = group;
            }

            window.displayDirty = true;
        }


        String GroupDisplayName(ModuleInventoryPart moduleInventoryPart, int? index = null)
        {
            return String.Format("{0}{1} ({2}/{3})", Localizer.Format("#autoLOC_8320000"),
                    index is null ? string.Empty : " " + index,
                    moduleInventoryPart.InventoryItemCount, moduleInventoryPart.InventorySlots);
        }

 
        public void OnDestroy()
        {
            GameEvents.onPartActionUIShown.Remove(OnPartActionWindowCreated);
            GameEvents.onModuleInventoryChanged.Remove(OnModuleInventoryChanged);
        }
    }
}
