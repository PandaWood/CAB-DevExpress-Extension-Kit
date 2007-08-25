using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace CABDevExpress.SmartPartInfos
{
    public class XtraImageSmartPartInfo : SmartPartInfo
    {
        #region Private Members

        private Image smallImage;
        private Image largeImage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the smart part info without any values.
        /// </summary>
        public XtraImageSmartPartInfo()
        {
        }

        /// <summary>
        /// Initializes the smart part info with the title and description values.
        /// </summary>
        /// <param name="title">Display name used by the <see cref="IWorkspace"/> for the associated smart part</param>
        /// <param name="description">String used to describe the associated smart part.</param>
        public XtraImageSmartPartInfo(string title, string description)
            : this(title, description, null)
        {
        }

        /// <summary>
        /// Initializes the smart part info with the title, description and LargeImage values.
        /// </summary>
        /// <param name="title">Display name used by the <see cref="IWorkspace"/> for the associated smart part</param>
        /// <param name="description">String used to describe the associated smart part.</param>
        /// <param name="largeImage">LargeImage displayed by the <see cref="IWorkspace"/> to represent the associated smart part.</param>
        public XtraImageSmartPartInfo(string title, string description, Image largeImage)
            : base(title, description)
        {
            this.largeImage = largeImage;
        }

        /// <summary>
        /// Initializes the smart part info with the title, description and LargeImage values.
        /// </summary>
        /// <param name="title">Display name used by the <see cref="IWorkspace"/> for the associated smart part</param>
        /// <param name="description">String used to describe the associated smart part.</param>
        /// <param name="largeImage">LargeImage displayed by the <see cref="IWorkspace"/> to represent the associated smart part.</param>
        /// <param name="smallImage">SmallImage displayed by the <see cref="IWorkspace"/> to represent the associated smart part.</param>
        public XtraImageSmartPartInfo(string title, string description, Image largeImage, Image smallImage)
            : base(title, description)
        {
            this.largeImage = largeImage;
            this.smallImage = smallImage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns or sets the LargeImage that is displayed in associated with the smart part.
        /// </summary>
        [Category("Appearance"), Localizable(true), DefaultValue(null)]
        [Description("Specifies the LargeImage displayed within the element.")]
        public Image LargeImage
        {
            get { return largeImage; }
            set { largeImage = value; }
        }

        /// <summary>
        /// Returns or sets the SmallImage that is displayed in associated with the smart part.
        /// </summary>
        [Category("Appearance"), Localizable(true), DefaultValue(null)]
        [Description("Specifies the SmallImage displayed within the element.")]
        public Image SmallImage
        {
            get { return smallImage; }
            set { smallImage = value; }
        }

        #endregion
    }
}