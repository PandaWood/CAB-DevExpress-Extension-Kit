using DevExpress.XtraBars.Docking2010;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CABDevExpress
{
    public partial class CustomDocumentsHost : DevExpress.XtraEditors.XtraForm, DevExpress.XtraBars.Docking2010.IDocumentsHostWindow
    {
        public CustomDocumentsHost()
        {
            InitializeComponent();
            floatDocHost = new DocumentManager();
            floatDocHost.ContainerControl = this;
            floatDocHost.View.FloatingDocumentContainer = DevExpress.XtraBars.Docking2010.Views.FloatingDocumentContainer.DocumentsHost;
            floatDocHost.View.DocumentAdded += View_DocumentsChanged;
            floatDocHost.View.DocumentRemoved += View_DocumentsChanged;
            floatDocHost.View.CustomDocumentsHostWindow += View_CustomDocumentsHostWindow;
        }

        void View_CustomDocumentsHostWindow(object sender, CustomDocumentsHostWindowEventArgs e)
        {
            e.Constructor = new DocumentsHostWindowConstructor(CreateCustomHost);
        }
        private CustomDocumentsHost CreateCustomHost()
        {
            return new CustomDocumentsHost();
        }
        void View_DocumentsChanged(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            CloseBox = true;
            foreach (DevExpress.XtraBars.Docking2010.Views.Tabbed.Document doc in floatDocHost.View.Documents)
                if (doc.Properties.AllowClose == DevExpress.Utils.DefaultBoolean.False)
                    CloseBox = false;
        }

        DocumentManager floatDocHost;
        public bool DestroyOnRemovingChildren
        {
            get { return true; }
        }

        public DocumentManager DocumentManager
        {
            get { return floatDocHost; }
        }
    }
}
