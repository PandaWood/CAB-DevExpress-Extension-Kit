using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace CABDevExpress
{
    /// <summary>
    /// A CAB shell application class used to start an application using a specified Form.
    /// </summary>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type of the form for the shell to use. Should be an XtraForm for skin support.</typeparam>
    public class XtraFormApplication<TWorkItem, TShell> : XtraFormApplicationBase<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
    {
        /// <summary>
        /// Used to start a message pump using the specified shell form.
        /// </summary>
        protected override void Start()
        {
            Application.Run(Shell);
        }
    }
}