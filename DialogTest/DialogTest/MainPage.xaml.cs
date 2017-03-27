using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Dialogs;
using Xamarin.Forms;

namespace DialogTest
{
	public partial class MainPage 
	{
		public MainPage()
		{
			InitializeComponent();

		    this.WhenActivated(d =>
		    {
		        this.BindCommand(ViewModel, vm => vm.ShowAlert, v => v.ShowAlert).DisposeWith(d);
		        this.BindCommand(ViewModel, vm => vm.ShowToast, v => v.ShowToast).DisposeWith(d);
		        this.BindCommand(ViewModel, vm => vm.ShowSpinner, v => v.ShowSpinner).DisposeWith(d);

		        this.AlertWhen(x => x.ViewModel.AlertMessage).DisposeWith(d);
		        this.ToastWhen(x => x.ViewModel.ToastMessage).DisposeWith(d);

		        this.ViewModel.ShowLoadingWhen(x=>x.ShowSpinner.IsExecuting).DisposeWith(d);
		    });
		}
	}
}
