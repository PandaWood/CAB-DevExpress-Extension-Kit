using System;

namespace CABDevExpress.UIElements
{
    public class DynamicEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data passed into the event.</param>
        public DynamicEventArgs(T data)
        {
            this.data = data;
        }

        private T data;
        /// <summary>
        /// Gets the dynamic data for the event.
        /// </summary>
        /// <value>The dynamic data for the event.</value>
        public T Data
        {
            get { return data; }
        }
    }
}
