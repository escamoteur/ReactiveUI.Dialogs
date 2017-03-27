

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
                OpenDialog.Dispose();
                OpenDialog = null;
            }
            return !DeviceInfo.App.IsBackgrounded;
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


        public static void ShowLoadingWhen<TSender>(this TSender This,
            Expression<Func<TSender, bool>> property, string title = null, MaskType? maskType = null)
        {
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
                });
        }


        public static IDisposable ShowLoadingWhen<TSender>(this TSender This,
            Func<TSender,IObservable<bool>> busy, string title = null, MaskType? maskType = null)
        {
                return busy(This).Subscribe(show =>
                {
                    if (show)
                    {
                        UserDialogs.Instance.ShowLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
        }

    }
}
