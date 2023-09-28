using Il2CppTLD.Cooking;
using static ItemRarities.Main;

namespace ItemRarities
{
    #region Inventory
    // The item info in the inventory to the right, when an item is selected on the right.
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    public static class ItemDescriptionPage_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (__instance.m_ItemNameLabel == null) return;

            string itemName = gi.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_ItemNameLabel);
                rarityLabel.transform.SetParent(__instance.m_ItemNameLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_ItemNameLabel.transform.localPosition.x,
                                                                  __instance.m_ItemNameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_ItemNameLabel.transform.localPosition.z);
                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
        }
    }

    // Actions menu, when you go to unload, harvest, repair, etc.
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateLabels))]
    public static class PanelInventoryExamine_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (!__instance.enabled) return;

            if (__instance.m_Item_Label == null) return;

            GearItem gi = __instance.m_GearItem;
            if (gi == null) return;

            string itemName = gi.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Item_Label);

                rarityLabel.transform.SetParent(__instance.m_Item_Label.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_Item_Label.transform.localPosition.x,
                                                                  __instance.m_Item_Label.transform.localPosition.y - -25,
                                                                  __instance.m_Item_Label.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
        }
    }
    #endregion

    #region Inventory Miscellaneous
    // Shows the rarity label above the clothing item, in the clothing ui from the inventory.
    [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.GetCurrentlySelectedGearItem))]
    public static class PanelClothing_RarityLabelPatch
    {
        private static UILabel? clothingRarityLabel;
        public static UILabel? ClothingRarityLabel
        {
            get { return clothingRarityLabel; }
            set { clothingRarityLabel = value; }
        }

        static void Postfix(Panel_Clothing __instance, ref GearItem __result)
        {
            if (__instance.m_ItemDescriptionPage == null || __instance.m_ItemDescriptionPage.m_ItemNameLabel == null) return;

            string itemName = __result.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (clothingRarityLabel == null)
            {
                clothingRarityLabel = UnityEngine.Object.Instantiate(__instance.m_ItemDescriptionPage.m_ItemNameLabel);

                clothingRarityLabel.transform.SetParent(__instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.parent, false);
                clothingRarityLabel.transform.localPosition = new Vector3(__instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.x,
                                                                  __instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.z);

                clothingRarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            clothingRarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            clothingRarityLabel.color = rarityColor;
        }
    }

    // Shows rarity label in the Crafting section within the Inventory or Workshop tables.
    [HarmonyPatch(typeof(Panel_Crafting), nameof(Panel_Crafting.RefreshSelectedBlueprint))]
    public static class PanelCrafting_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        static void Postfix(Panel_Crafting __instance)
        {
            if (!__instance.enabled) return;

            if (__instance.m_SelectedName == null) return;

            int selectedIndex = __instance.m_CurrentBlueprintIndex;

            // Always instantaite all GearItems and check for null
            GearItem gi = __instance.m_FilteredBlueprints[selectedIndex].m_CraftedResult;
            if (gi == null) return;

            string itemName = gi.name ?? "Unknown";

            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_SelectedName);
                rarityLabel.transform.SetParent(__instance.m_SelectedName.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_SelectedName.transform.localPosition.x,
                                                                  __instance.m_SelectedName.transform.localPosition.y - -25,
                                                                  __instance.m_SelectedName.transform.localPosition.z);
                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
        }
    }

    // Adds rarity label to the Cooking section within the Inventory.
    [HarmonyPatch(typeof(Panel_Cooking), nameof(Panel_Cooking.GetSelectedCookableItem))]
    public static class PanelCooking_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        static void Postfix(Panel_Cooking __instance, ref CookableItem __result)
        {
            if (!__instance.enabled) return;
            if (__instance.m_Label_CookedItemName == null) return;

            // Always instantaite all GearItems and check for null
            GearItem gi = __result.m_GearItem;
            if (gi == null) return;

            if (__result == null || gi == null)
            {
                if (rarityLabel != null)
                {
                    rarityLabel.enabled = false;
                }
                return;
            }

            string itemName = gi.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Label_CookedItemName);

                rarityLabel.transform.SetParent(__instance.m_Label_CookedItemName.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_Label_CookedItemName.transform.localPosition.x,
                                                                  __instance.m_Label_CookedItemName.transform.localPosition.y - -25,
                                                                  __instance.m_Label_CookedItemName.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
            rarityLabel.enabled = true;
        }
    }

    // This harmony patch duplicates the rarity label to the Milling interface when a milling machine is accessed.
    [HarmonyPatch(typeof(Panel_Milling), nameof(Panel_Milling.GetSelected))]
    public static class PanelMilling_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        static void Postfix(Panel_Milling __instance, ref GearItem __result)
        {
            if (!__instance.enabled) return;
            if (__instance.m_NameLabel == null) return;

            string itemName = __result.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_NameLabel);

                rarityLabel.transform.SetParent(__instance.m_NameLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_NameLabel.transform.localPosition.x,
                                                                  __instance.m_NameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_NameLabel.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
        }
    }
    #endregion

    #region Heads Up Display (HUD)
    // Adds the rarity label to the Radial Menu.
    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.GetActionText))]
    public static class PanelActionsRadial_RarityLabelPatch
    {
        static void Postfix(Panel_ActionsRadial __instance, RadialMenuArm arm)
        {
            if (!__instance.enabled) return;
            if (__instance.m_SegmentLabel == null) return;

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_SegmentLabel);

                rarityLabel.transform.SetParent(__instance.m_SegmentLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_SegmentLabel.transform.localPosition.x,
                                                                  __instance.m_SegmentLabel.transform.localPosition.y - -20,
                                                                  __instance.m_SegmentLabel.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            // Always instantaite all GearItems and check for null
            GearItem gi = arm.m_GearItem;
            if (gi == null) return;

            if (arm != null && gi != null && gi.name != "None")
            {
                string itemName = gi.name;
                Rarity itemRarity = GetRarity(itemName);
                Color rarityColor = GetRarityColor(itemRarity);

                if (!excludedNames.Contains(itemName) && !string.IsNullOrEmpty(itemName))
                {
                    rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
                    rarityLabel.color = rarityColor;
                    rarityLabel.gameObject.SetActive(true);
                    return;
                }
            }

            if (rarityLabel != null)
            {
                rarityLabel?.gameObject.SetActive(false);
            }
        }
    }

    // This patch allows the rarity label to change inside the first patch.
    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.UpdateDisplayText))]
    public static class PanelActionsRadial_UpdateDisplayText_Patch
    {
        public static void Postfix(Panel_ActionsRadial __instance)
        {
            if (!__instance.enabled) return;

            UILabel? label = RarityLabel;

            bool isHoveredOverAnyItem = false;

            for (int i = 0; i < __instance.m_RadialArms.Count; i++)
            {
                if (__instance.m_RadialArms[i] != null && __instance.m_RadialArms[i].IsHoveredOver() && !__instance.m_RadialArms[i].IsEmpty())
                {
                    isHoveredOverAnyItem = true;

                    GearItem gi = __instance.m_RadialArms[i].m_GearItem;
                    if (gi == null) continue;

                    string itemName = gi.name;
                    Rarity itemRarity = GetRarity(itemName);

                    if (itemRarity == Rarity.None)
                    {
                        label?.gameObject.SetActive(false);
                        return;
                    }

                    break;
                }
            }

            if (label != null && !isHoveredOverAnyItem)
            {
                label.gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region Miscellaneous
    // This displays the rarity label inside of the Inspect mode, when an item is picked up from the floor - or searched.
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.InitLabelsForGear))]
    public static class PlayerManager_RarityLabelPatch
    {
        private static UILabel? rarityLabel;
        public static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }
        static void Postfix(PlayerManager __instance)
        {
            Panel_HUD? actualHUDPanel = InterfaceManager.GetPanel<Panel_HUD>();
            if (actualHUDPanel == null || actualHUDPanel.m_InspectMode_Title == null) return;

            GearItem? currentGearItem = __instance.m_Gear;
            if (currentGearItem == null) return;

            string itemName = currentGearItem.name;
            Rarity itemRarity = GetRarity(itemName);
            Color rarityColor = GetRarityColor(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(actualHUDPanel.m_InspectMode_Title);
                rarityLabel.alignment = NGUIText.Alignment.Center;

                rarityLabel.transform.SetParent(actualHUDPanel.m_InspectMode_Title.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(actualHUDPanel.m_InspectMode_Title.transform.localPosition.x - 366,
                                                                  actualHUDPanel.m_InspectMode_Title.transform.localPosition.y - 290,
                                                                  actualHUDPanel.m_InspectMode_Title.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
            rarityLabel.color = rarityColor;
        }
    }
    #endregion

    #region Future Code?
    // Possible Harmony Patches which can be used in the future?
    /* [HarmonyPatch(typeof(Panel_GearSelect), nameof(Panel_GearSelect.Update))] // Need to find an alternative method. Slightly broken, all labels disapear after No Tools is selected
            public static class Panel_GearSelectAddRarityLabelPatch
            {
                public static UILabel? rarityLabel;

                private static readonly HashSet<string> excludedNames = new HashSet<string>
                {
                    "NO TOOL",
                };
                static void Postfix(Panel_GearSelect __instance)
                {
                    if (__instance.m_Label == null) return;

                    string itemName = __instance.m_Label.text;
                    Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.INVALID;
                    Color rarityColor = GetRarityColor(itemRarity);

                    if (excludedNames.Contains(itemName))
                    {
                        if (rarityLabel != null)
                        {
                            rarityLabel.gameObject.SetActive(false);
                        }
                        return;
                    }

                    if (string.IsNullOrEmpty(itemName))
                    {
                        if (rarityLabel != null)
                        {
                            rarityLabel.gameObject.SetActive(false);
                        }
                        return;
                    }

                    if (rarityLabel == null)
                    {
                        rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Label);

                        rarityLabel.transform.SetParent(__instance.m_Label.transform.parent, false);
                        rarityLabel.transform.localPosition = new Vector3(__instance.m_Label.transform.localPosition.x,
                                                                          __instance.m_Label.transform.localPosition.y - -15,
                                                                          __instance.m_Label.transform.localPosition.z);

                        rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                    }

                    rarityLabel.text = GetTranslation(itemRarity.ToString(), Localization.s_Language);
                    rarityLabel.color = rarityColor;
                }
            } */

    // Commented out because its not as visually altering as these other methods.
    // Need to find an alternative method - and a way to get the GearItem for the rarity to change.
    /* [HarmonyPatch(typeof(Panel_HUD), nameof(Panel_HUD.Update))]
    public static class PanelHUD_RarityLabelPatch
    {
        static void Postfix(Panel_HUD __instance)
        {
            if (__instance.m_Label_ObjectName == null) return;

            string itemName = __instance.m_Label_ObjectName.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.Default;
            Color rarityColor = GetRarityColor(itemRarity);

            __instance.m_Label_ObjectName.color = rarityColor;
        }
    } */

    // May be used in the future to streamline the Rarity Label process.
    public static class RarityUtilities 
    {
        #region Constants
        public const float RarityLabelScale = 0.75f;
        public const float RarityLabelYOffset = -25f;
        public const float RarityLabelZOffset = 0f;
        #endregion

        #region Utility Methods
        /* public static UILabel InitializeRarityLabel(UILabel originalLabel, Vector3 positionOffset, Vector3 scale)
        {
            var label = UnityEngine.Object.Instantiate(originalLabel);
            label.transform.SetParent(originalLabel.transform.parent, false);
            label.transform.localPosition = new Vector3(
                originalLabel.transform.localPosition.x + positionOffset.x,
                originalLabel.transform.localPosition.y + positionOffset.y,
                originalLabel.transform.localPosition.z + positionOffset.z);
            label.transform.localScale = scale;
            return label;
        }

        public static void SetRarityLabelProperties(UILabel label, string itemName)
        {
            if (gearRarities.TryGetValue(itemName, out var itemRarity))
            {
                Color rarityColor = GetRarityColor(itemRarity);
                label.text = itemRarity.ToString();
                label.color = rarityColor;
            }
            else
            {
                label.text = Rarity.INVALID.ToString();
                label.color = GetRarityColor(Rarity.INVALID);
            }
        } */
        #endregion
    }
    #endregion
}