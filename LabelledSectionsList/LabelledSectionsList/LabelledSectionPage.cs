using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace LabelledSections
{
	//public class MyContentPage : ContentPage {}

	public class MyListView : ListView {}

	public class MyMenuButton : Button {}

    public class LabelledSectionPage : ContentPage
    {
        public LabelledSectionPage()
        {
			// My Menu button is cusomt rendered for Android, not iOS
			var button = new MyMenuButton(){
				Image = "image.png",
				HorizontalOptions = LayoutOptions.End
			};

			button.Clicked += async (object sender, EventArgs e) => {
				var action = await DisplayActionSheet ("Action Sheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
				Debug.WriteLine("Action: " + action);
			};

			// MyListView is Custom Rendered for iOS, not for Android
            var list = new MyListView
            {
                ItemTemplate = new DataTemplate(typeof(TextCell))
                {
                    Bindings = {
							{ TextCell.TextProperty, new Binding ("Name") }
						}
                },

                GroupDisplayBinding = new Binding("Title"),
                IsGroupingEnabled = true,
                ItemsSource = SetupList(),
            };

            list.ItemTapped += (sender, e) =>
            {
                var listItem = (ListItemValue)e.Item;
                DisplayAlert(listItem.Name, "You tapped " + listItem.Name, "OK", "Cancel");
				Debug.WriteLine("Item Tapped {0}, ListView {1}", listItem.Name, sender);
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { button, list }
            };
        }

        ObservableCollection<ListItemCollection> SetupList()
        {
            var allListItemGroups = new ObservableCollection<ListItemCollection>();

            foreach (var item in ListItemCollection.GetSortedData())
            {
                // Attempt to find any existing groups where theg group title matches the first char of our ListItem's name.
                var listItemGroup = allListItemGroups.FirstOrDefault(g => g.Title == item.Label);

                // If the list group does not exist, we create it.
                if (listItemGroup == null)
                {
                    listItemGroup = new ListItemCollection(item.Label);
                    listItemGroup.Add(item);
                    allListItemGroups.Add(listItemGroup);
                }
                else
                { // If the group does exist, we simply add the demo to the existing group.
                    listItemGroup.Add(item);
                }
            }
            return allListItemGroups;
        }
    }
}
