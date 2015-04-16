using System;
using LabelledSections;
using LabelledSections.iOS;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer (typeof (MyListView), typeof (MyListViewRenderer))]

namespace LabelledSections.iOS
{
    public class MyListViewRenderer : ListViewRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);
			var tableView = (UITableView)Control;
			tableView.Source = new WrapperSource(Control.Source);
		}

	}

	class WrapperSource : UITableViewSource
	{
		UIPopoverController popover {get; set;}
		private readonly UITableViewSource original;

		public WrapperSource (UITableViewSource original)
		{
			this.original = original;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return this.original.NumberOfSections (tableView);
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.original.RowsInSection(tableView, section);
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return this.original.GetCell(tableView, indexPath);
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return this.original.GetHeightForHeader(tableView, section);
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			return this.original.GetViewForHeader(tableView, section);
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return this.original.TitleForHeader(tableView, section);
		}

		public override string[] SectionIndexTitles(UITableView tableView)
		{
			return this.original.SectionIndexTitles(tableView);
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				this.original.RowSelected(tableView, indexPath);
			}
			else {
				var rectangle = tableView.RectForRowAtIndexPath(indexPath);

				nint index = 0;

				for (nint i = 0; i < indexPath.Section; i++) {
					index += tableView.NumberOfRowsInSection((int)i);
				}
				index += indexPath.Row;

				var item = ListItemCollection.GetSortedData()[(int)index];
				ShowPopover(item, rectangle, tableView);
			}
		}

		public override void RowDeselected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			this.original.RowDeselected(tableView, indexPath);
		}

		public override void Scrolled(UIScrollView scrollView)
		{
			Console.WriteLine("Scrolled");
		}

		public void ShowPopover(ListItemValue item, CGRect rect, UITableView tv)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {

				//				UIViewController popoverViewController = new UIViewController();
				//				popoverViewController.View = new UITextView(new RectangleF(0, 0, 100, 50)) {
				//					Text = item.Name
				//				};

				LabelledSections.PopoverPage popoverPage = new LabelledSections.PopoverPage();
				popoverPage.PopLabel.Text = item.Name;
				popoverPage.PopButton.Text = item.Name + "Button";


				UIViewController popoverViewController = popoverPage.CreateViewController();


				popover = new UIPopoverController(popoverViewController);

				//				popover.PopoverContentSize = new SizeF(100, 50);

				popover.DidDismiss += (object pSender, EventArgs e) => {
					popover.Dismiss(true);
					popover = null;
				};
				popover.PresentFromRect(rect, tv, UIPopoverArrowDirection.Any, true);
			}
		}

	}
}


