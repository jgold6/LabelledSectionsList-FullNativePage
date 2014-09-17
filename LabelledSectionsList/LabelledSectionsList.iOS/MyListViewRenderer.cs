using System;
using Xamarin.Forms.Platform.iOS;
using LabelledSections;
using LabelledSections.iOS;
using Xamarin.Forms;
using MonoTouch.UIKit;
using System.Collections.Generic;

[assembly: ExportRenderer (typeof (MyListView), typeof (MyListViewRenderer))]

namespace LabelledSections.iOS
{
    public class MyListViewRenderer : ListViewRenderer
    {
		//public UITableViewCell[] visibleCells;

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);
			var tableView = (UITableView)Control;
			tableView.BackgroundColor = UIColor.Green;
		}
	}
}

