using MoneyManagerApp.Models;
using MoneyManagerApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoneyManagerApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<PaymentInfo> PaymentDataStore = new DataStore();

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
