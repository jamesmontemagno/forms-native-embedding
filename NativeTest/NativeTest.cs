using System;

using Xamarin.Forms;
using System.Threading.Tasks;
#if __ANDROID__
using Xamarin.Forms.Platform.Android;
using NativeTest.Droid;
using Android.Views;
#elif __IOS__
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
#endif

namespace NativeTest
{
    public class App : Application
    {
        public App()
        {
            var list = new ListView();
            list.ItemsSource = new[] { "Hello", "World", "This", "Is", "Native", "Embedding" };

            // Main page layout
            var pageLayout = new StackLayout
            {
                Children =
                {
                    list
                }
            };

            var absolute = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(pageLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(pageLayout, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(pageLayout);


            var stack = new StackLayout
            {
                Padding = 8,
                HorizontalOptions = LayoutOptions.Center,
            };

#if __ANDROID__
            var fab = new CheckableFab(Forms.Context);
           
            fab.SetImageResource(Droid.Resource.Drawable.ic_fancy_fab_icon);
            fab.Click += async (sender, e) =>
            {
                await Task.Delay(3000);
                await MainPage.DisplayAlert("Native FAB Clicked", 
                                            "Whoa!!!!!!", "OK");
            };


            fab.UseCompatPadding = true;
            stack.Children.Add(fab);
            absolute.Children.Add(stack);
            // Overlay the FAB in the bottom-right corner
            AbsoluteLayout.SetLayoutFlags(stack, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(stack, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

#elif __IOS__
            var segmentControl = new UISegmentedControl();
            segmentControl.Frame = new CGRect(20, 20, 280, 40);
            segmentControl.InsertSegment("One", 0, false);
            segmentControl.InsertSegment("Two", 1, false);
            segmentControl.SelectedSegment = 1;
            segmentControl.ValueChanged += async (sender, e) =>
            {
                var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
                await MainPage.DisplayAlert($"Native Segmented Control Clicked {selectedSegmentId}",
                                            "Whoa!!!!!!", "OK");
            };
            stack.Children.Add(segmentControl);
            pageLayout.Children.Insert(0, stack);
#endif

            // The root page of your application
            var content = new ContentPage
            {
                Title = "NativeTest",
                Content = absolute
            };
            MainPage = new NavigationPage(content);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

