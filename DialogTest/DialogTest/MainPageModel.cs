using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Acr.UserDialogs;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;

namespace DialogTest
{
    public class MainPageModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> ShowAlert { get; set; }
        public ReactiveCommand<Unit, Unit> ShowAlertConfig { get; set; }
        public ReactiveCommand<Unit, Unit> ShowToast { get; set; }
        public ReactiveCommand<Unit, long> ShowSpinner { get; set; }
        public ReactiveCommand<Unit, long> ShowSpinnerDispose { get; set; }

        [Reactive]
        public string ToastMessage { get; set; }

        [Reactive]
        public string AlertMessage { get; set; }

        [Reactive]
        public AlertConfig Alert { get; set; }

        [Reactive]
        public bool DisposeSpinner { get; set; } = false;




        public MainPageModel()
        {
            ShowAlert = ReactiveCommand.Create(() => { AlertMessage = "This is a sample Allert message!"; });

            ShowAlertConfig = ReactiveCommand.Create(() =>
            {
                Alert = new AlertConfig()
                {
                    Message = "This is a sample Allert message!",
                    Title = "This is a TestAlert",
                    OkText = "Cool!"
                };
            });

            ShowToast = ReactiveCommand.Create(() => { ToastMessage = "This is a sample Toast message!"; });

            ShowSpinner = ReactiveCommand.CreateFromObservable(()=>Observable.Timer(TimeSpan.FromSeconds(5)));
            ShowSpinner.Subscribe();

            ShowSpinnerDispose = ReactiveCommand.CreateFromObservable(()=>Observable.Timer(TimeSpan.FromSeconds(5)));
            ShowSpinnerDispose.Subscribe(l => DisposeSpinner = true);
        }

    }
}
