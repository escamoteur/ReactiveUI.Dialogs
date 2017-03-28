

using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reactive.Linq;
using System.ServiceModel.Channels;
using Acr.DeviceInfo;
using Acr.UserDialogs;


namespace ReactiveUI.Dialogs
{
    public static class Dialogs
    {
        private static IDisposable OpenDialog = null;

        private static bool CheckPreCondition()
        {
            if (OpenDialog != null)
            {
                OpenDialog?.Dispose();
                OpenDialog = null;
            }
            return !DeviceInfo.App.IsBackgrounded;
        }


        public static void CloseOpenDialog()
        {

            if (OpenDialog != null)
            {
                OpenDialog?.Dispose();
                OpenDialog = null;
            }
        }


        public static IDisposable AlertWhen<TSender>(this TSender This,
	        Expression<Func<TSender, string>> property, string title = null, string okText = null)
	    {
	        return This.WhenAnyValue(property)
	             .Subscribe(message =>
	            {
	                if (CheckPreCondition() && !string.IsNullOrWhiteSpace(message))
	                    {
	                        UserDialogs.Instance.Alert(message, title, okText);
	                    }
	            });
	    }


        public static IDisposable AlertWhen<TSender>(this TSender This,
            Expression<Func<TSender, AlertConfig>> property, string title = null, string okText = null)
        {
            return This.WhenAnyValue(property)
                .Subscribe(config =>
                {
                    if (CheckPreCondition() && config != null)
                    {
                        UserDialogs.Instance.Alert(config);
                    }
                });
        }




        public static IDisposable ToastWhen<TSender>(this TSender This,
            Expression<Func<TSender, string>> property, TimeSpan? dismissTimer = null)
        {
            return This.WhenAnyValue(property)
                .Subscribe(message =>
                {
                    if (CheckPreCondition() && !string.IsNullOrWhiteSpace(message))
                    {
                        UserDialogs.Instance.Toast(message,dismissTimer);
                    }
                });
        }



        public static IDisposable ToastWhen<TSender>(this TSender This,
            Expression<Func<TSender, ToastConfig>> property, TimeSpan? dismissTimer = null)
        {
            return This.WhenAnyValue(property)
                .Subscribe(config =>
                {
                    if (CheckPreCondition() && config!= null)
                    {
                        UserDialogs.Instance.Toast(config);
                    }
                });
        }



        public static LoadingDisposable ShowLoadingWhen<TSender>(this TSender This,
            Expression<Func<TSender, bool>> property, string title = null, MaskType? maskType = null)
        {
            return new LoadingDisposable( 
                This.WhenAnyValue(property)
                    .Subscribe(show =>
                    {
                        if (show)
                        {
                            UserDialogs.Instance.ShowLoading();
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    }));
        }


        public static LoadingDisposable ShowLoadingWhen<TSender>(this TSender This,
            Func<TSender,IObservable<bool>> busy, string title = null, MaskType? maskType = null)
        {
                return new LoadingDisposable(busy(This).Subscribe(show =>
                {
                    if (show)
                    {
                        UserDialogs.Instance.ShowLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                }));
        }


        
    }

    public class LoadingDisposable : IDisposable
    {
        public LoadingDisposable(IDisposable subscription)
        {
            this.subscription = subscription;
        }
        private IDisposable subscription;

        public void Dispose()
        {   subscription.Dispose();
            UserDialogs.Instance.HideLoading();
        }
    }
}
