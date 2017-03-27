using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DialogTest
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    var mainPage = new DialogTest.MainPage();   
            mainPage.ViewModel = new MainPageModel();
		    MainPage = mainPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
