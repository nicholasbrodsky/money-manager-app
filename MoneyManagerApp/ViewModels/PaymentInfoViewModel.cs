using MoneyManagerApp.Models;
using MoneyManagerApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MoneyManagerApp.ViewModels
{
    public class PaymentInfoViewModel : BaseViewModel
    {
        public ObservableCollection<PaymentInfo> Payments { get; }
        public ICommand LoadPaymentsCommand { get; }
        public PaymentInfoViewModel()
        {
            Payments = new ObservableCollection<PaymentInfo>();
            Task.Run(LoadPaymentsAsync);

            LoadPaymentsCommand = new Command(LoadView);
        }
        public async void LoadView()
        {
            await LoadPaymentsAsync();
            Refresh = false;
        }
        public async Task LoadPaymentsAsync()
        {
            var payments = (await PaymentDataStore.GetItems())
                .ToList()
                .OrderBy(p => p.DueDay);
            Payments.Clear();
            foreach (var item in payments)
            {
                Payments.Add(item);
            }
        }
    }
}
