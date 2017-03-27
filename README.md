# ReactiveUI.Dialogs

Allan Ritchie's [Acr.UserDialogs](https://github.com/aritchie/userdialogs) is an amazing libary that make live for any mobile devloper much easier when it comes to Alerts, Toasts or Spinners.

One of it big advantages is that you can call it from almost anywhere in your code. This can be in at the same time problematic because it misleads to violate the separation of View and ViewModel.

Another problem that can occur if you call it to the wrong time in the App lifecycle on Android is that you get an ugly exception.

While moving the Dialog code to my View and using RxUI I could solve both problems by writing:

```c#
this.WhenAnyValue(x => x.ViewModel.Message)
    .Where(message => !string.IsNullOrWhiteSpace(message))
    .Subscribe(message =>
        {
            UserDialogs.Instance.Alert(message);
            }
    );
```

or

```c#

ViewModel.GetReplay.IsExecuting.Subscribe(busy =>
    {
        if (busy)
        {
            UserDialogs.Instance.ShowLoading();
        }
        else
        {
            UserDialogs.Instance.HideLoading();
            }

        });
```

So I deciced to start writing an ReactiveUI wrapper around Acr.UserDialogs. So that I now can write:

```c#
this.AlertWhen(x => x.ViewModel.AlertMessage).DisposeWith(d);
this.ToastWhen(x => x.ViewModel.ToastMessage).DisposeWith(d);

this.ViewModel.ShowLoadingWhen(x=>x.ShowSpinner.IsExecuting).DisposeWith(d);
``` 

As soon as the observed string properties get a value assigned a Dialog/Toast is shown

Opening a new Dialog outmatically closes any currently open ones.

Before an Dialog is shown it is checked if the App is not backgrounded (Not sure yes if it would make sense to throw an optional Exception here)


Currently I support this methods:

```c# 
public static IDisposable AlertWhen<TSender>(this TSender This,
  Expression<Func<TSender, string>> property, string title = null, string okText = null)


public static IDisposable ToastWhen<TSender>(this TSender This,
    Expression<Func<TSender, string>> property, TimeSpan? dismissTimer = null)


public static void ShowLoadingWhen<TSender>(this TSender This,
    Expression<Func<TSender, bool>> property, string title = null, MaskType? maskType = null)


public static IDisposable ShowLoadingWhen<TSender>(this TSender This,
    Func<TSender,IObservable<bool>> busy, string title = null, MaskType? maskType = null)
```


### Important:
Make sure to add the NUget to your Platform project too
Call the UserDialogs Init method in your MainActivity on Android

`UserDialogs.Init(this)`


## Contribution
any PRs to complete this wrapper are very welcome.








