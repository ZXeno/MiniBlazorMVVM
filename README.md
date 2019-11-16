# MiniBlazorMVVM
A very tiny but flexible MVVM library that is well-commented and extensible for all your needs.

# Usage
Implementation and usage of the classes is very, very simple. 

There is the `ViewModelBase.cs` class that you build all of your view models from. 

Example:

`public class MyFancyViewModel : ViewModelBase
{
	private string myFancyStringPropertyField = "My Fancy String Value!";
	public string MyFancyStringProperty 
	{ 
		get => this.myFancyStringPropertyField;
		set
		{
			this.OnPropertyChanging();
			this.myFancyStringPropertyField = value;
			this.OnPropertyChanged();
		}
	}
}`

In  your `Startup.cs` class, be sure to register your viewmodel with the dependency injection system.
`services.AddScoped<MyFancyViewModel>();`

For your view, there is the `ViewComponentBase.cs` class. You can directly inhert this in your Blazor component/page, or you can extend it to create your custom view logic.

This first example shows directly utilizing the ViewComponentBase to inject your viewmodel directly into a Blazor page

	@page = "/fancypantspage"
	@using MBMVVM
	@inherits ViewComponentBase<MyFancyViewModel>

	@if (this.ViewModel != null)
	{
		<p>@this.ViewModel.MyFancyStringProperty</p>
	}

If you have implemented custom view logic and extended the ViewComponentBase class, you can still use the same pattern to inject the view model:

	@using MBMVVM
	@inherits MyFancyViewComponent

	@if (this.ViewModel != null)
	{
		<p>@this.ViewModel.MyFancyStringProperty</p>
	}

And when we define our custom view component/page logic:

	public class MyFancyViewComponent : ViewComponentBase<CodeBehindExampleViewModel>
	{
		// View logic here
	}

See the example project for a working example of both the in-line and code-behind pattern using this MVVM implemenation.

That is all that is needed! It really is that simple. 

# Notes
This should work for both server-side and client-side Blazor.

The `ViewComponentBase<T>` class relies on Dependency Injection to grab the view model. The `ViewModel` property of the `ViewComponentBase<T>` class uses the `[Inject]` attribute to signal to the .Net DI library to get the requested view model type instance.

The `ViewComponentBase<T>` class subscribes to the OnPropertyChanged class in an override to `OnInitializedAsync()`. If you wish to override `OnInitializedAsync()`, you will need to make a call to `await base.OnInitializedAsync()` or be sure to subscribe to the view model's `PropertyChanged` event manually with `NotifyStateChanged()` method or another method that invokes the `StateHasChanged()` function. If your call will notify `StateHasChanged()` asynchronously, be sure to call the method with `InvokeAsync(StateHasChanged)` to prevent SignalR from dropping the connection.

`ViewModelBase.cs` implements `INotifyPropertyChanging` should you need the changing event.

# Contribute
Contributions and discussions are welcome!
If you have ideas or would like to contribute, submit a pull request or post an issue.
