using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="Bars"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class BarsUIAdapter : UIElementAdapter<Bar>
	{
		private readonly Bars bars;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarsUIAdapter"/> class.
		/// </summary>
		/// <param name="bars"></param>
		public BarsUIAdapter(Bars bars)
		{
			Guard.ArgumentNotNull(bars, "Bars");
			this.bars = bars;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override Bar Add(Bar uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "Bar");

			bars.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(Bar uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "Bar");

			int index = bars.IndexOf(uiElement); 
			if (index > -1)
				bars.RemoveAt(index);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the bars.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return bars.Count;
		}
	}
}