using System;
using System.Collections.Generic;
using System.Drawing;
using BankTellerModule.Constants;
using CABDevExpress.UIElements;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

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
                menuItem.Hint = skinName;
                BuildSkinImages(skinName, menuItem);
                menuItem.Tag = new DynamicCommandEventLink(EventNames.SwitchSkin, skinName);
                workItem.UIExtensionSites[ExtensionSiteNames.SkinsDropDown].Add<BarItem>(menuItem);
                workItem.UIExtensionSites[ExtensionSiteNames.SkinsToolBar].Add<BarItem>(menuItem);
			}
		}

        private static void BuildSkinImages(string skinName, BarItem item)
        {
            SimpleButton imageButton = new SimpleButton();
            imageButton.LookAndFeel.SetSkinStyle(skinName);

            item.Glyph = GetSkinImage(imageButton, 20, 20, 2);
        }

        /// <summary>
        /// Gets the skin image. This method closely follows the one from the DevExpress Simple 
        /// Ribbon Pad demo. Thanks to DevExpress for letting us use this routine!
        /// </summary>
        private static Bitmap GetSkinImage(SimpleButton button, int width, int height, int indent)
        {
            Bitmap image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image))
            {
                StyleObjectInfoArgs info = new StyleObjectInfoArgs(new GraphicsCache(g));
                info.Bounds = new Rectangle(0, 0, width, height);
                button.LookAndFeel.Painter.GroupPanel.DrawObject(info);
                button.LookAndFeel.Painter.Border.DrawObject(info);
                info.Bounds = new Rectangle(indent, indent, width - indent * 2, height - indent * 2);
                button.LookAndFeel.Painter.Button.DrawObject(info);
            }
            return image;
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
        public void ModifyLookAndFeel(Object sender, DataEventArgs<DynamicCommandEventLink> e)
        {
            UserLookAndFeel.Default.SetSkinStyle((e.Data as DynamicCommandEventLink).Data.ToString());
            SetSkinsChecked();
        }

        private void SetSkinsChecked()
        {
            foreach (BarItemLink item in skinMenu.ItemLinks)
            {
                if (item.Item is BarCheckItem)
                    ((BarCheckItem)item.Item).Checked = UserLookAndFeel.Default.ActiveSkinName == item.Caption;
            }
        }

        // The subscriber to the EventNames.SkinPopup event
        public void SkinMenuPopup(object sender, System.EventArgs e)
        {
            SetSkinsChecked();
        }
    }
}
