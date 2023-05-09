using MoneyManagerApp.Models;
using MoneyManagerApp.Services;
using System.Collections.ObjectModel;

namespace MoneyManagerApp.ViewModels
{
    public class EditInfoViewModel : BaseViewModel
    {
        public ObservableCollection<PaymentInfo> Payments { get; }
        public EditInfoViewModel()
        {
            Payments = new ObservableCollection<PaymentInfo>();
            _ = LoadPayments();
        }
        public async Task LoadPayments()
        {
            var payments = await PaymentDataStore.GetItems();
            Payments.Clear();
            foreach (var item in payments)
            {
                Payments.Add(item);
            }
        }
    }
}
