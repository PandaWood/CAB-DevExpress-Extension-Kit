using System.Collections.Generic;
using System.Drawing;
using BankTellerModule.Constants;
using CABDevExpress.UIElements;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

namespace BankShell
{
#if UseRibbonForm
    internal static class BankTellerRibbonSkins
    {
        private const int imageWidth = 32;
        private const int imageHeight = 17;
        private const int hoverWidth = 70;
        private const int hoverHeight = 36;
        internal static void Add(WorkItem workItem)
        {
            RibbonPage ribbonPage = new RibbonPage(ExtensionSiteNames.RibbonLookAndFeel);
            workItem.UIExtensionSites[ExtensionSiteNames.Ribbon].Add<RibbonPage>(ribbonPage);
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonLookAndFeel, ribbonPage);

            RibbonPageGroup ribbonGroup = new RibbonPageGroup(ExtensionSiteNames.RibbonSkins);
            workItem.UIExtensionSites[ExtensionSiteNames.RibbonLookAndFeel].Add<RibbonPageGroup>(ribbonGroup);
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonSkins, ribbonGroup);

            // Next create the Gallery within the Ribbon Group
            RibbonGalleryBarItem ribbonGallery = new RibbonGalleryBarItem();
            ribbonGallery.Caption = ExtensionSiteNames.RibbonSkinGallery;
            ribbonGallery.Gallery.AllowHoverImages = true;
            ribbonGallery.Gallery.ColumnCount = 3;
            //ribbonGallery.Gallery.DistanceBetweenItems = 1;
            ribbonGallery.Gallery.ImageSize = new Size(imageWidth, imageHeight);
            ribbonGallery.Gallery.HoverImageSize = new Size(hoverWidth, hoverHeight);
            workItem.UIExtensionSites[ExtensionSiteNames.RibbonSkins].Add<BarItem>(ribbonGallery);
            // Pass the WorkItem which added the command into the RibbonGalleryUIAdapter. This allows the command to be fired.
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonSkinGallery,
                new RibbonGalleryDynamicUIAdapter(ribbonGallery, workItem));

            // Then create the group within the gallery
            GalleryItemGroup galleryGroup = new GalleryItemGroup();
            galleryGroup.Caption = ExtensionSiteNames.RibbonSkinGalleryGroup;
            workItem.UIExtensionSites[ExtensionSiteNames.RibbonSkinGallery].Add<GalleryItemGroup>(galleryGroup);
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonSkinGalleryGroup, new RibbonGalleryGroupDynamicUIAdapter(galleryGroup));

            AddAllMenuItems(workItem);
        }

        private static void AddAllMenuItems(WorkItem workItem)
        {
            foreach (string skinName in GetSortedSkinNames())
            {
                GalleryItem galleryItem = new GalleryItem();
                galleryItem.Caption = skinName;
                BuildSkinImages(skinName, galleryItem);
                galleryItem.Hint = galleryItem.Caption;
                // Set the galleryItem's tag to the skin name and event to be fired,
                galleryItem.Tag = new DynamicCommandEventLink(EventNames.RibbonSkinChange, skinName);
                workItem.UIExtensionSites[ExtensionSiteNames.RibbonSkinGalleryGroup].Add<GalleryItem>(galleryItem);
            }
        }

        private static List<string> GetSortedSkinNames()
        {
            var skinNames = new List<string>(SkinManager.Default.Skins.Count);

            foreach (SkinContainer skinContainer in SkinManager.Default.Skins)
                skinNames.Add(skinContainer.SkinName);

            skinNames.Sort();
            return skinNames;
        }

        private static void OnSwitchSkin(object sender, ItemClickEventArgs e)
        {
            var item = e.Item as BarButtonItem;
            if (item == null) return;
            UserLookAndFeel.Default.SetSkinStyle(item.Caption);
        }

        private static void BuildSkinImages(string skinName, GalleryItem galleryItem)
        {
            SimpleButton imageButton = new SimpleButton();
            imageButton.LookAndFeel.SetSkinStyle(skinName);

            galleryItem.Image = GetSkinImage(imageButton, imageWidth, imageHeight, 2);
            galleryItem.HoverImage = GetSkinImage(imageButton, hoverWidth, hoverHeight, 5);
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
    }
#endif
}
