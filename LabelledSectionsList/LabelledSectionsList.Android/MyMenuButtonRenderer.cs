using System;
using Xamarin.Forms.Platform.Android;
using LabelledSections;
using LabelledSections.Droid;
using Android.Widget;

[assembly: Xamarin.Forms.ExportRenderer (typeof (MyMenuButton), typeof (MyMenuButtonRenderer))]

namespace LabelledSections.Droid
{
    public class MyMenuButtonRenderer : ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);
			Button button = (Button)Control;
			button.Click += (object buttonsender, EventArgs btnArgs) => {

				// Sending the calling button as the second argument places the menu right under the button
				PopupMenu menu = new PopupMenu(Android.App.Application.Context, button);
				menu.MenuInflater.Inflate(Resource.Menu.popup_menu, menu.Menu);

				menu.MenuItemClick += (object menusender, PopupMenu.MenuItemClickEventArgs menuArgs) => {
					Console.WriteLine("Menu item cliecked: {0}", menuArgs.Item.TitleFormatted);
				};

				menu.DismissEvent += (menusender2, menuArgs2) => {
					Console.WriteLine ("menu dismissed"); 
				};

				menu.Show();
			};
		}
    }
}

