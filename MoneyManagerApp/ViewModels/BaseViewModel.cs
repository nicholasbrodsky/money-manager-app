using MoneyManagerApp.Models;
using MoneyManagerApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoneyManagerApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<PaymentInfo> PaymentDataStore = new PaymentDataStore();
        public IDataStore<UserInfo> UserDataStore = new UserDataStore();

        private bool refresh;
        public bool Refresh
        {
            get { return refresh; }
            set { refresh = value; OnPropertyChanged(); }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
