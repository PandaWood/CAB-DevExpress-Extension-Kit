using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// Drag this component to your smartpart control and set the Page/group and button items
	/// </summary>
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	[Description("non visual component used to configure a ribbonpagegroup")]
	public class RibbonGroupUI : RibbonPageGroup
	{
		/// <summary>
		/// Actual base ribbon page group object
		/// </summary>
		[Browsable(false)]
		public RibbonPageGroup RibbonPageGroup
		{
			get { return this; }
		}

		private string ribbonPageName;

		///<summary>
		///</summary>
		[Description("Name of the Ribbon Page to Place this Ribbon Group")]
		[Category("Ribbon Data")]
		public string RibbonPageName
		{
			get { return ribbonPageName; }
			set { ribbonPageName = value; }
		}

		///<summary>
		///</summary>
		[Description("Used to identify Unique Page Group where BarItems will be located")]
		public string UniqueRibbonGroupName
		{
			get { return ribbonPageName + "_" + Name; }
		}

		///<summary>
		///</summary>
		[Category("Ribbon Data")]
		[Description("Button Items To appear in Ribbon Page Group")]
		public DXButtonItem[] BarButtonItems { get; set; }

		///<summary>
		///</summary>
		[Category("Ribbon Data")]
		[Description("Check Items To appear in Ribbon Page Group")]
		public DXCheckItem[] BarCheckItems { get; set; }
	}

	///<summary>
	///</summary>
	[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
	public class DXCheckItem : BarCheckItem
	{
		private UIEvent uiEvent;

		///<summary>
		///</summary>
		[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
		[Category("CAB")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public UIEvent CabUIEvent
		{
			get
			{
				if (DesignMode)
					uiEvent = uiEvent ?? new UIEvent();
				return uiEvent;
			}
			set { uiEvent = value; }
		}

		/// <summary>
		/// Actual BarCheckItem 
		/// </summary>
		[Browsable(false)]
		public BarCheckItem BarCheckItem
		{
			get { return this; }
		}
	}

	///<summary>
	///</summary>
	[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
	public class DXButtonItem : BarButtonItem
	{
		///<summary>
		///</summary>
		public DXButtonItem()
		{
			if (DesignMode)
			{ }
		}

		private UIEvent uiEvent;

		///<summary>
		///</summary>
		[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
		[Category("CAB")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public UIEvent CabUIEvent
		{
			get
			{
				if (DesignMode)
					uiEvent = uiEvent ?? new UIEvent();
				return uiEvent;
			}
			set { uiEvent = value; }
		}

		/// <summary>
		/// Actual BarButtonItem 
		/// </summary>
		[Browsable(false)]
		public BarButtonItem BarButtonItem
		{
			get { return this; }
		}
	}

	/// <summary>
	///  Enumeration of events 
	/// </summary>
	public enum DXItemEvents
	{
		None,
		ItemClick,
		ItemDoubleClick,
		ItemPress,
		OnClick,
		OnDoubleClick,
		OnPress
	}

	/// <summary>
	/// Class to define UI Events to attach to CAB Bar Items
	/// </summary>
	/// 
	//[TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
	[DesignTimeVisible(false)]
	[Description("Class to define UI Events to attach to CAB Bar Items")]
	[ToolboxItem(false)]
	public class UIEvent //: Component
	{
		private DXItemEvents itemEvent = DXItemEvents.ItemClick;

		/// <summary>
		/// Control Event
		/// </summary>
		[DefaultValue(1)]
		public DXItemEvents Event
		{
			get { return itemEvent; }
			set { itemEvent = value; }
		}

		[Description("Command Name Associated with this event to pass to CAB event handler to the decorated method  [CommandHandler(''MyCommandName'')]"
			)]
		public string CommandName { get; set; }

		[Description("Publication Scope of Event -- To Do: Not wired up yet but can be in the CAB infrastructure")]
		public PublicationScope PublicationScope { get; set; }
	}
}