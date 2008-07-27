using System;
using System.Collections.Generic;
using BankTellerModule.Constants;
using CABDevExpress.UIElements;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace BankShell
{
    public class BankTellerDynamicSkins
    {
        private static readonly BankTellerDynamicSkins instance = new BankTellerDynamicSkins();
        private DevExpress.XtraBars.BarSubItem skinMenu;

        private BankTellerDynamicSkins() { }

        public static BankTellerDynamicSkins Skins
        {
            get { return instance; }
        }

        public void AddSkins(WorkItem workItem)
        {
            skinMenu = new DevExpress.XtraBars.BarSubItem();
            // named this menu Look & Feel so that it was differentiated between
            // the SkinMenu option.
            skinMenu.Caption = "Look && Feel";
            workItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add(skinMenu);
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.SkinsDropDown, 
                new BarLinksCollectionDynamicUIAdapter(skinMenu.ItemLinks, skinMenu.Manager.Items, workItem));
            AddSkinItems(workItem);

            EventTopic eventTopic = workItem.EventTopics[EventNames.SkinPopup];
            eventTopic.AddPublication(skinMenu, "Popup", workItem, PublicationScope.Global);
            eventTopic.AddSubscription(this, "SkinMenuPopup", workItem, ThreadOption.UserInterface);

            workItem.EventTopics[EventNames.SwitchSkin].AddSubscription(this, "ModifyLookAndFeel", workItem, ThreadOption.UserInterface);
        }

        private void AddSkinItems(WorkItem workItem)
        {
			foreach (string skinName in GetSortedSkinNames())
			{
				BarItem menuItem = new BarCheckItem(skinMenu.Manager, SkinManager.DefaultSkinName == skinName ? true : false);
				menuItem.Caption = skinName;
                menuItem.Tag = new DynamicCommandEventLink(EventNames.SwitchSkin, skinName);
                workItem.UIExtensionSites[ExtensionSiteNames.SkinsDropDown].Add<BarItem>(menuItem);
			}
		}

		private static List<string> GetSortedSkinNames()
		{
			List<string> skinNames = new List<string>(SkinManager.Default.Skins.Count);

			foreach (SkinContainer skinContainer in SkinManager.Default.Skins)
				skinNames.Add(skinContainer.SkinName);

			skinNames.Sort();
			return skinNames;
		}

        // The subscriber to the EventNames.SwitchSkin event
        public void ModifyLookAndFeel(Object sender, DynamicEventArgs<DynamicCommandEventLink> e)
        {
            UserLookAndFeel.Default.SetSkinStyle((e.Data as DynamicCommandEventLink).Data.ToString());
        }

        // The subscriber to the EventNames.SkinPopup event
        public void SkinMenuPopup(object sender, System.EventArgs e)
        {
            foreach (BarItemLink item in skinMenu.ItemLinks)
            {
                if (item.Item is BarCheckItem)
                    ((BarCheckItem)item.Item).Checked = UserLookAndFeel.Default.ActiveSkinName == item.Caption;
            }
        }
    }
}
