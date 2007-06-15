using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace DevExpress.CompositeUI.SmartPartInfos
{
    // <summary>
    /// Provides information to show smartparts in the <see cref="DockManagerWorkspace"/>.
    /// </summary>
    public class DockManagerSmartPartInfo : SmartPartInfo
    {
        private DockingStyle dock;
        //
        // Summary:
        //     Gets or sets a value specifying how the dock panel is aligned within its
        //     parent control.
        [Category("Appearance")]
        public DockingStyle Dock
        {
            get { return dock; }
            set { dock = value; }
        }

        private Point floatLocation;
        //
        // Summary:
        //     Gets or sets the floating panel's location.
        [Category("Appearance")]
        public Point FloatLocation
        {
            get { return floatLocation; }
            set { floatLocation = value; }
        }

        private Size floatSize = new Size(200, 200);
        //
        // Summary:
        //     Gets or sets the size of the panel when it is floating.
        [Category("Appearance")]
        public Size FloatSize
        {
            get { return floatSize; }
            set { floatSize = value; }
        }

        private bool floatVertical = false;
        //
        // Summary:
        //     Gets or sets whether the current panel's children should be arranged vertically
        //     or horizontally when the panel is floated.
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool FloatVertical
        {
            get { return floatVertical; }
            set { floatVertical = value; }
        }

        private string hint = "";
        //
        // Summary:
        //     Gets or sets the dock panel's hint text.
        [DefaultValue("")]
        [Localizable(true)]
        [Category("Appearance")]
        public string Hint
        {
            get { return hint; }
            set { hint = value; }
        }

        private Guid id;
        //
        // Summary:
        //     Gets or sets the dock panel's unique identifier.
        [Browsable(false)]
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        private int imageIndex = -1;
        //
        // Summar
        //     Gets or sets the index of an image associated with the dock panel.
        [DefaultValue(-1)]
        [Category("Appearance")]
        [ImageList("Images")]
        [Editor("DevExpress.Utils.Design.ImageIndexesEditor, DevExpress.Utils.v6.1", typeof(UITypeEditor))]
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        private int index = -1;
        //
        // Summary:
        //     Gets or sets the index of an image associated with the dock panel.
        [DefaultValue(-1)]
        [Category("Appearance")]
        [ImageList("Images")]
        [Editor("DevExpress.Utils.Design.ImageIndexesEditor, DevExpress.Utils.v6.1", typeof(UITypeEditor))]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private DockingStyle savedDock;
        //
        // Summary:
        //     Gets or sets the docking style applied to a panel before it's made to float
        //     or is hidden.
        [Browsable(false)]
        public DockingStyle SavedDock
        {
            get { return savedDock; }
            set { savedDock = value; }
        }

        private int savedIndex = -1;
        //
        // Summary:
        //     Gets or sets the panel's index before it's made to float or is hidden.  
        [Browsable(false)]
        [DefaultValue(-1)]
        public int SavedIndex
        {
            get { return savedIndex; }
            set { savedIndex = value; }
        }

        private DockPanel savedParent;
        //
        // Summary:
        //     Gets or sets the panel's parent before it's made to float or is hidden. 
        [Browsable(false)]
        [DefaultValue("")]
        public DockPanel SavedParent
        {
            get { return savedParent; }
            set { savedParent = value; }
        }

        private bool savedTabbed = false;
        //
        // Summary:
        //     Gets or sets a value indicating whether the panel was a tab container, before
        //     it was made to float or was hidden.
        [Browsable(false)]
        [DefaultValue(false)]
        public bool SavedTabbed
        {
            get { return savedTabbed; }
            set { savedTabbed = value; }
        }

        private bool tabbed = false;
        //
        // Summary:
        //     Gets or sets whether the current panel represents a tab container.
        [DefaultValue(false)]
        [Category("Appearance")]
        public bool Tabbed
        {
            get { return tabbed; }
            set { tabbed = value; }
        }

        private TabsPosition tabsPosition = TabsPosition.Bottom;
        //
        // Summary:
        //     Gets or sets the position of tabs.
        [Category("Appearance")]
        public TabsPosition TabsPosition
        {
            get { return tabsPosition; }
            set { tabsPosition = value; }
        }

        private bool tabsScroll = false;
        //
        // Summary:
        //     Specifies whether tab navigation buttons are displayed when tabs don't fit
        //     into the tab container's width/height.
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool TabsScroll
        {
            get { return tabsScroll; }
            set { tabsScroll = value; }
        }

        private string tabText = "";
        //
        // Summary:
        //     Gets or sets the panel's short caption.
        [DefaultValue("")]
        [Localizable(true)]
        [Category("Appearance")]
        public string TabText
        {
            get { return tabText; }
            set { tabText = value; }
        }

        private DockVisibility visibility;
        //
        // Summary:
        //     Gets or sets the panel's visible state.
        [Category("Appearance")]
        public DockVisibility Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        private bool visible;
        //
        // Summary:
        //     Gets or sets whether the dock panel is visible.
        [DesignerSerializationVisibility(0)]
        [Browsable(false)]
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private Rectangle xtraBounds;
        //
        // Summary:
        //     Gets or sets the dock panel's bounds.
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public Rectangle XtraBounds
        {
            get { return xtraBounds; }
            set { xtraBounds = value; }
        }

        private string parentPanelName = string.Empty;
        //
        // Summary:
        //     Gets or sets the Panel which the current panel will be coked in.
        //      Leave blank if no ParentPanel is needed
        public string ParentPanelName
        {
            get { return parentPanelName; }
            set { parentPanelName = value; }
        }

        private string name;
        //
        // Summary:
        //     Gets or sets the DockPanel's name.
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}