using System;
using Xamarin.Forms.Platform.iOS;
using LabelledSections;
using LabelledSections.iOS;
using Xamarin.Forms;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;

[assembly: ExportRenderer (typeof (MyListView), typeof (MyListViewRenderer))]

namespace LabelledSections.iOS
{
    public class MyListViewRenderer : ListViewRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);
			var tableView = (UITableView)Control;
			tableView.Source = new MyTableViewSource();
		}
	}

	public class MyTableViewSource : UITableViewSource
	{
		UIPopoverController popover {get; set;}

		public override int RowsInSection(UITableView tableview, int section)
		{
			// Still use the model object from the Froms core project
			return ListItemCollection.GetSortedData().Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell("UITableViewCell");
			if (cell == null)
				// Create an instance of UITableViewCell, with default appearance
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle,"UITableViewCell");

			var item = ListItemCollection.GetSortedData()[indexPath.Row];
			cell.TextLabel.Text = item.Name;

			return cell;
		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var rectangle = tableView.RectForRowAtIndexPath(indexPath);
			var item = ListItemCollection.GetSortedData()[indexPath.Row];
			ShowPopover(item, rectangle, tableView);
		}

		public void ShowPopover(ListItemValue item, RectangleF rect, UITableView tv)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {

				UIViewController popoverViewController = new UIViewController();
				popoverViewController.View = new UITextView(new RectangleF(0, 0, 100, 50)) {
					Text = item.Name
				};
				popover = new UIPopoverController(popoverViewController);
				popover.PopoverContentSize = new SizeF(100, 50);

				popover.DidDismiss += (object pSender, EventArgs e) => {
					popover.Dismiss(true);
					popover = null;
				};
				popover.PresentFromRect(rect, tv, UIPopoverArrowDirection.Any, true);
			}
		}
	}
}

