using System;
using Xamarin.Forms;

namespace LabelledSections
{
    public class PopoverPage : ContentPage
    {
		public Label PopLabel {get; set;}
		public Button PopButton {get; set;}

        public PopoverPage()
        {
			PopLabel = new Label {
				Text = "Forms PopoverPage"
			};

			PopButton = new Button {
				Text = "Button"
			};

			Content = new StackLayout {
				Children = {
					PopLabel,
					PopButton
				}
			};
        }
    }
}

