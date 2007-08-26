using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;

namespace BankShell
{
	public class SkinMenu : BarSubItem
	{
		private readonly Bar bar;

		public SkinMenu(Bar bar)
		{
			this.bar = bar;
			Manager = bar.Manager;
			Caption = "&Skins";
			AddItems();
		}

		private void AddItems()
		{
			foreach (SkinContainer cnt in SkinManager.Default.Skins)
			{
				BarCheckItem barButtonItem = new BarCheckItem(bar.Manager, SkinManager.DefaultSkinName == cnt.SkinName ? true : false);
				barButtonItem.Caption = cnt.SkinName;
				//SkinManager.DefaultSkinName
				barButtonItem.ItemClick += OnSwitchSkin;
				AddItem(barButtonItem);
			}
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
				if (item.Item is BarCheckItem)
					((BarCheckItem) item.Item).Checked = UserLookAndFeel.Default.ActiveSkinName == item.Caption;
		}
	}
}