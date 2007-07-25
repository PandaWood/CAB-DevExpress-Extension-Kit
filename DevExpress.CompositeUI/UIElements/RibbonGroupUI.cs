using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Reflection;
using DevExpress;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.Utils;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// Drag this component to your smartpart control and set the Page/group and button items
    /// </summary>

    [ToolboxItem(true)]
    [DesignTimeVisible(true)]
    [Description("non visual component used to configure a ribbonpagegroup")]
    public class RibbonGroupUI : RibbonPageGroup
    {

        public RibbonGroupUI()
        {

        }

        /// <summary>
        /// Actual base ribbon page group object
        /// </summary>
        [Browsable(false)]
        public RibbonPageGroup RibbonPageGroup
        {
            get
            {

                return this;
            }
        }

        private void InitializeComponent()
        {

        }

        private string ribbonPageName;

        [Description("Name of the Ribbon Page to Place this Ribbon Group")]
        [Category("Ribbon Data")]
        public string RibbonPageName
        {
            get { return ribbonPageName; }
            set { ribbonPageName = value; }
        }
        [Description("Used to identify Unique Page Group where BarItems will be located")]
        public string UniqueRibbonGroupName
        {
            get { return ribbonPageName + "_" + Name; }

        }
        
        private DexButtonItem[] dexButtonItems;
        [Category("Ribbon Data")]
        [Description("Button Items To appear in Ribbon Page Group")]
        public DexButtonItem[] BarButtonItems
        {
            get { return dexButtonItems; }
            set { dexButtonItems = value; }
        }

        private DexCheckItem[] barCheckItems;

        [Category("Ribbon Data")]
        [Description("Check Items To appear in Ribbon Page Group")]
        public DexCheckItem[] BarCheckItems
        {
            get { return barCheckItems; }
            set { barCheckItems = value; }
        }

    }
    
    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class DexCheckItem : BarCheckItem
    {
        private UIEvent uIIEvent;
        [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
        [Category("CAB")]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public UIEvent CabUIEvent
        {
            get
            {
                if (DesignMode)
                    uIIEvent = uIIEvent ?? new UIEvent();
                return uIIEvent;//new UIEvent();//_UIEvent;
            }
            set
            {
                uIIEvent = value;
            }
        }

        /// <summary>
        /// Actual BarCheckItem 
        /// </summary>
        [Browsable(false)]
        public BarCheckItem BarCheckItem
        {
            get
            {
                return this;
            }
        }

    }

    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class DexButtonItem : BarButtonItem
    {

        public DexButtonItem()
        {
            if (DesignMode)
            {

            }

        }

        private UIEvent _UIEvent;
        [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
        [Category("CAB")]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public UIEvent CabUIEvent
        {
            get
            {
                if (DesignMode)
                    _UIEvent = _UIEvent ?? new UIEvent();
                return _UIEvent;
            }
            set
            {
                _UIEvent = value;
            }
        }
        /// <summary>
        /// Actual BarButtonItem 
        /// </summary>
        [Browsable(false)]

        public BarButtonItem BarButtonItem
        {
            get
            {
                return this;
            }
        }
    }

    /// <summary>
    ///  Enumeration of events 
    /// </summary>
    public enum DexItemEvents
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
        private DexItemEvents _Event = DexItemEvents.ItemClick;
        /// <summary>
        /// Control Event
        /// </summary>
        [DefaultValue(1)]
        public DexItemEvents Event
        {
            get { return _Event; }
            set { _Event = value; }
        }
        private string _CommandName;
        [Description("Command Name Associated with this event to pass to CAB event handler to the decorated method  [CommandHandler(''MyCommandName'')]")]
        public string CommandName
        {
            get { return _CommandName; }
            set { _CommandName = value; }
        }
        private PublicationScope _PublicationScope;
        [Description("Publication Scope of Event -- To Do: Not wired up yet but can be in the CAB infrastructure")]
        public PublicationScope PublicationScope
        {
            get { return _PublicationScope; }
            set { _PublicationScope = value; }
        }
    }

}