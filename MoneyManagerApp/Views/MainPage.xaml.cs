using MoneyManagerApp.ViewModels;

namespace MoneyManagerApp.Views;

public partial class MainPage : ContentPage
{
    private readonly BaseViewModel mainViewModel = new MainViewModel();
	public MainPage()
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}
