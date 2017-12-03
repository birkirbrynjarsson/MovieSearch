using System;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public TabBarController()
        {
            ViewControllerSelected += (sender, args) =>
            {
                UINavigationController currentView = (UINavigationController)args.ViewController;
                if(currentView.TopViewController is MovieTopListController){
                    ((MovieTopListController)currentView.TopViewController).refreshList = true;
                }
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.TabBar.TintColor = UIColor.Orange;
            this.SelectedIndex = 0;
        }
    }
}
