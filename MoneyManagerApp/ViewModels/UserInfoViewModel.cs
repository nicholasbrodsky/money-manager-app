namespace MoneyManagerApp.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        string test;
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        public UserInfoViewModel()
        {
            Title = "Test";
        }
        public UserInfoViewModel(string test)
        {
            this.test = test;
            Title = "Test";
        }
    }
}
