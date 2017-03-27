using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;

namespace DialogTest
{
    public class MainPageModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> ShowAlert { get; set; }
        public ReactiveCommand<Unit, Unit> ShowToast { get; set; }
        public ReactiveCommand<Unit, long> ShowSpinner { get; set; }

        [Reactive]
        public string ToastMessage { get; set; }

        [Reactive]
        public string AlertMessage { get; set; }



        public MainPageModel()
        {
            ShowAlert = ReactiveCommand.Create(() => { AlertMessage = "This is a sample Allert message!"; });

            ShowToast = ReactiveCommand.Create(() => { ToastMessage = "This is a sample Toast message!"; });

            ShowSpinner = ReactiveCommand.CreateFromObservable(()=>Observable.Timer(TimeSpan.FromSeconds(5)));

            ShowSpinner.Subscribe();




        }
    }
}
