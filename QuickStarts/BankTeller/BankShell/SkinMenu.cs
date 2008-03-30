using System.Collections.Generic;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;

namespace BankShell
{
	public class SkinMenu : BarSubItem
	{
		private readonly Bar bar;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bar"></param>
		public SkinMenu(Bar bar)
		{
			this.bar = bar;
			Manager = bar.Manager;
			AddAllMenuItems();
		}

		private void AddAllMenuItems()
		{
			Caption = "Skins";

			foreach (string skinName in GetSortedSkinNames())
			{
				BarItem menuItem = new BarCheckItem(bar.Manager, SkinManager.DefaultSkinName == skinName ? true : false);
				menuItem.Caption = skinName;
				menuItem.ItemClick += OnSwitchSkin;
				AddItem(menuItem);
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

		private static void OnSwitchSkin(object sender, ItemClickEventArgs e)
		{
			BarCheckItem item = e.Item as BarCheckItem;
			if (item == null) return;
			UserLookAndFeel.Default.SetSkinStyle(item.Caption);
		}

		protected override void OnPopup()
		{
			base.OnPopup();
			foreach (BarItemLink item in ItemLinks)
			{
				if (item.Item is BarCheckItem)
					((BarCheckItem) item.Item).Checked = UserLookAndFeel.Default.ActiveSkinName == item.Caption;
			}
		}
	}
}