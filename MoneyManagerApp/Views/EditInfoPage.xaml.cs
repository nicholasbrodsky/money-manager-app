using MoneyManagerApp.ViewModels;

namespace MoneyManagerApp.Views;

public partial class EditInfoPage : ContentPage
{
	private readonly BaseViewModel editInfoViewModel = new EditInfoViewModel();
	public EditInfoPage()
	{
		InitializeComponent();
		BindingContext = editInfoViewModel;
	}
}