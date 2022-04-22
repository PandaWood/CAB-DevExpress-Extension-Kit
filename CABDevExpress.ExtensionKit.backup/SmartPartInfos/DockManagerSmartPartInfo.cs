using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using CABDevExpress.Workspaces;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace CABDevExpress.SmartPartInfos
{
    /// <summary>
    /// Provides information to show smartparts in the <see cref="DockManagerWorkspace"/>
    /// </summary>
    public class DockManagerSmartPartInfo : SmartPartInfo
    {
        ///<summary>
        /// Gets or sets a value specifying how the dock panel is aligned within its
        ///  parent control
        ///</summary>
        [Category("Appearance")]
        public DockingStyle Dock { get; set; }

        ///<summary>
        /// Gets or sets the floating panel's location
        ///</summary>
        [Category("Appearance")]
        public Point FloatLocation { get; set; }

        private Size floatSize = new Size(200, 200);

        ///<summary>
		/// Gets or sets the size of the panel when it is floating
        ///</summary>
        [Category("Appearance")]
        public Size FloatSize
        {
            get { return floatSize; }
            set { floatSize = value; }
        }

        ///<summary>
        /// Gets or sets whether the current panel's children should be arranged vertically
        ///     or horizontally when the panel is floated
        ///</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool FloatVertical { get; set; }

        private string hint = "";

        ///<summary>
		/// Gets or sets the dock panel's hint text
        ///</summary>
        [DefaultValue("")]
        [Localizable(true)]
        [Category("Appearance")]
        public string Hint
        {
            get { return hint; }
            set { hint = value; }
        }

        ///<summary>
        /// Gets or sets the dock panel's unique identifier
        ///</summary>
        [Browsable(false)]
        public Guid ID { get; set; }

        private int imageIndex = -1;

        ///<summary>
		/// Gets or sets the index of an image associated with the dock panel
		/// //TODO is it really necessary to hard-code the dll version?
        ///</summary>
        [DefaultValue(-1)]
        [Category("Appearance")]
        [ImageList("Images")]
        //[Editor("DevExpress.Utils.Design.ImageIndexesEditor, DevExpress.Utils.v18.1", typeof(UITypeEditor))]
        [Editor("DevExpress.Utils.Design.ImageIndexesEditor", typeof(UITypeEditor))]
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        private int index = -1;

        ///<summary>
		/// Gets or sets the index of an image associated with the dock panel
		/// //TODO is it really necessary to hard-code the dll version?
        ///</summary>
        [DefaultValue(-1)]
        [Category("Appearance")]
        [ImageList("Images")]
        //[Editor("DevExpress.Utils.Design.ImageIndexesEditor, DevExpress.Utils.v18.1", typeof(UITypeEditor))]
        [Editor("DevExpress.Utils.Design.ImageIndexesEditor", typeof(UITypeEditor))]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        ///<summary>
        /// Gets or sets the docking style applied to a panel before it's made to float
        /// or is hidden
        ///</summary>
        [Browsable(false)]
        public DockingStyle SavedDock { get; set; }

        private int savedIndex = -1;

        ///<summary>
		/// Gets or sets the panel's index before it's made to float or is hidden
        ///</summary>
        [Browsable(false)]
        [DefaultValue(-1)]
        public int SavedIndex
        {
            get { return savedIndex; }
            set { savedIndex = value; }
        }

        ///<summary>
        /// Gets or sets the panel's parent before it's made to float or is hidden.
        ///</summary>
        [Browsable(false)]
        [DefaultValue("")]
        public DockPanel SavedParent { get; set; }

        ///<summary>
        /// Gets or sets a value indicating whether the panel was a tab container, before
        /// it was made to float or was hidden.
        ///</summary>
        [Browsable(false)]
        [DefaultValue(false)]
        public bool SavedTabbed { get; set; }

        ///<summary>
        /// Gets or sets whether the current panel represents a tab container
        ///</summary>
        [DefaultValue(false)]
        [Category("Appearance")]
        public bool Tabbed { get; set; }

        private TabsPosition tabsPosition = TabsPosition.Bottom;

        ///<summary>
		/// Gets or sets the position of tabs
        ///</summary>
        [Category("Appearance")]
        public TabsPosition TabsPosition
        {
            get { return tabsPosition; }
            set { tabsPosition = value; }
        }

        ///<summary>
        /// Specifies whether tab navigation buttons are displayed when tabs don't fit
        /// into the tab container's width/height.
        ///</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool TabsScroll { get; set; }

        private string tabText = "";

        ///<summary>
		/// Gets or sets the panel's short caption
        ///</summary>
        [DefaultValue("")]
        [Localizable(true)]
        [Category("Appearance")]
        public string TabText
        {
            get { return tabText; }
            set { tabText = value; }
        }

        ///<summary>
        /// Gets or sets the panel's visible state
        ///</summary>
        [Category("Appearance")]
        public DockVisibility Visibility { get; set; }

        ///<summary>
        /// Gets or sets whether the dock panel is visible.
        ///</summary>
        [DesignerSerializationVisibility(0)]
        [Browsable(false)]
        public bool Visible { get; set; }

        ///<summary>
        /// Gets or sets the dock panel's bounds.
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public Rectangle XtraBounds { get; set; }

        private string parentPanelName = string.Empty;

        ///<summary>
		/// Gets or sets the Panel which the current panel will be docked in.
		/// Leave blank if no ParentPanel is needed
        ///</summary>
        public string ParentPanelName
        {
            get { return parentPanelName; }
            set { parentPanelName = value; }
        }

        ///<summary>
        /// Gets or sets the DockPanel's name.
        ///</summary>
        public string Name { get; set; }
    }
}