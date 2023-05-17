using MoneyManagerApp.ViewModels;

namespace MoneyManagerApp.Views;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage()
	{
		BindingContext = new UserInfoViewModel(Title);
		InitializeComponent();
	}
}