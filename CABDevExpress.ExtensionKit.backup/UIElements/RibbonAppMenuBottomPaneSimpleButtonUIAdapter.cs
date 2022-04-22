using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// An adapter that wraps the bottom pane of an ApplicationMenu for use
    /// as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class RibbonAppMenuBottomPaneSimpleButtonUIAdapter
        : UIElementAdapter<SimpleButton>
    {
        private int spacing;
        private WorkItem workItem;
        private PopupControlContainer bottomPane;
        private ApplicationMenu applicationMenu;

        /// <summary>
        /// Keeps an ordered list of the buttons added to the bottom pane. This
        /// keeps the adapter following the CAB standard of all UIElements being
        /// added to the end of the adapter.
        /// </summary>
        private List<Control> buttonList = new List<Control>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonApplicationMenuBottomPaneSimpleButtonUIAdapter"/> class.
        /// </summary>
        /// <param name="workItem">The work item. Since Events are used this can
        /// be any work item including the root work item.</param>
        /// <param name="applicationMenu">The application menu.</param>
        /// <param name="bottomPaneHeight">Height of the bottom pane (default is 26).</param>
        /// <param name="spacing">Spacing between the buttons (default is 3).</param>
        public RibbonAppMenuBottomPaneSimpleButtonUIAdapter(WorkItem workItem,
            ApplicationMenu applicationMenu, int bottomPaneHeight, int spacing)
        {
            Guard.ArgumentNotNull(applicationMenu, "ApplicationMenu");
            Guard.ArgumentNotNull(applicationMenu.Ribbon, "ApplicationMenu.Ribbon");
            Guard.ArgumentNotNull(workItem, "workItem");

            this.spacing = spacing;
            bottomPane = new PopupControlContainer();
            bottomPane.Height = bottomPaneHeight;
            bottomPane.BackColor = System.Drawing.Color.Transparent;
            applicationMenu.BottomPaneControlContainer = bottomPane;

            this.applicationMenu = applicationMenu;
            this.workItem = workItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonApplicationMenuBottomPaneSimpleButtonUIAdapter"/> class.
        /// </summary>
        /// <param name="workItem">The work item. Since Events are used this can
        /// be any work item including the root work item.</param>
        /// <param name="applicationMenu">The application menu.</param>
        public RibbonAppMenuBottomPaneSimpleButtonUIAdapter(WorkItem workItem,
            ApplicationMenu applicationMenu)
            : this(workItem, applicationMenu, 26, 3) { }

        /// <summary>
        /// Adds the specified UI element.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        /// <returns></returns>
        protected override SimpleButton Add(SimpleButton uiElement)
        {
            ValidateUiElement(uiElement);
            uiElement.Click += ButtonClick;
            bottomPane.Controls.Add(uiElement);
            buttonList.Insert(0, uiElement);
            RearrangeButtons();
            return uiElement;
        }

        /// <summary>
        /// Validates a UIElement to ensure that the SimpleButton.Tag property
        /// contains a non-empty string value. This value is assumed to be a
        /// EventTopicName associated with an instantiated CAB event.
        /// </summary>
        /// <param name="uiElement">The UIElement to be validated.</param>
        private void ValidateUiElement(SimpleButton uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            // not using Guard here so that we can supply the additional
            // information in the message.
            if (uiElement.Tag == null)
                throw new ArgumentNullException("uiElement.Tag cannot null."
                    + " It must contain the EventTopicName to be fired.");

            if (uiElement.Tag is string)
            {
                if (String.IsNullOrEmpty(uiElement.Tag as string))
                    throw new ArgumentException("uiElement.Tag cannot be empty."
                        + " It must contain the EventTopicName to be fired.");
            }
            else
            {
                throw new ArgumentException("uiElement.Tag must be a string and"
                    + " must contain the EventTopicName to be fired.");
            }
        }

        /// <summary>
        /// Fired when any <see cref="SimpleButton"/> contained in the ApplicationMenu
        /// bottom pane is clicked. If the SimpleButton.Tag property contains an 
        /// active EventTopicName string, that event is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>
        /// instance containing the event data.</param>
        private void ButtonClick(object sender, EventArgs e)
        {
            /* •———————————————————————————————————————————————————————————————•
               | Here is the whole reason for handling the ApplicationMenu     |
               | bottom pane with events. We need to hide the application      |
               | menu popup if any button will cause a modal dialog to appear. |
               | Since the standard two items added to the bttom pane are the  |
               | application exit and application options, we need to close    |
               | the application menu popup prior to launching the modal       |
               | options dialog.                                               |
               •———————————————————————————————————————————————————————————————• */
            applicationMenu.HidePopup();

            if (!(sender is SimpleButton))
                return;
            SimpleButton button = sender as SimpleButton;
            if (button.Tag != null && button.Tag is string)
            {
                string eventTopicName = button.Tag as string;
                EventTopic eventTopic = workItem.EventTopics[eventTopicName];
                if (eventTopic != null)
                    eventTopic.Fire(sender, new EventArgs(), null, PublicationScope.Global);
            }
        }

        private void RearrangeButtons()
        {
            // spacing can be negative which may squeeze the buttons a bit.
            // However, we want just an approximate size here - the transparency
            // allows us a bit of flexibility if the bottom pane is too wide but
            // no flexibility if the bottom pane is too narrow. So use absolute
            // value of spacing for determining the bottom pane width.
            bottomPane.Width = Math.Abs(spacing);
            foreach (Control item in buttonList)
                bottomPane.Width += item.Width + Math.Abs(spacing);

            int leftMost = bottomPane.Width - spacing;
            foreach (Control item in buttonList)
            {
                item.Left = leftMost - item.Width + spacing;
                leftMost -= item.Width + spacing;
                item.Top = (bottomPane.Height - item.Height) / 2;
            }
        }

        /// <summary>
        /// Removes the specified UI element.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        protected override void Remove(SimpleButton uiElement)
        {
            if (bottomPane == null)
                return;

            if (bottomPane.Controls.Contains(uiElement))
                bottomPane.Controls.Remove(uiElement);
            RearrangeButtons();
        }
    }
}
