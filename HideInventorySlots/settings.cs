using KSP.Localization;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace HideInventorySlots
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings


    public class HideInventorySlotsSettings : GameParameters.CustomParameterNode
    {
        public override string Title { get { return Localizer.Format("#HideInventorySlots_title"); } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.CAREER | GameParameters.GameMode.SCIENCE; } }
        public override string Section { get { return "HideInventorySlots"; } }
        public override string DisplaySection { get { return "HideInventorySlots"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }

        [GameParameters.CustomIntParameterUI("#HideInventorySlots_StartCollapsed", toolTip = "#HideInventorySlots_StartCollapsed_tooltip")]
        public bool StartCollapsed { get; private set; } = true;

        [GameParameters.CustomIntParameterUI("#HideInventorySlots_SlotsCount", toolTip = "#HideInventorySlots_SlotsCount_tooltip")]
        public bool SlotsCount { get; private set; } = true;

        [GameParameters.CustomIntParameterUI("#HideInventorySlots_ItemCount", toolTip = "#HideInventorySlots_ItemCount_tooltip")]
        public bool ItemCount { get; private set; } = true;
    }
}
