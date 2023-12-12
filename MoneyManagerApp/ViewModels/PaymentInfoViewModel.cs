using MoneyManagerApp.Models;
using MoneyManagerApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MoneyManagerApp.ViewModels
{
    public class PaymentInfoViewModel : BaseViewModel
    {
        #region Binding Vars

        private int? amount;
        public int? Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }
        private int? day;
        public int? Day
        {
            get { return day; }
            set { day = value; OnPropertyChanged(); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private string btnLabel = "Add Payment";
        public string BtnLabel
        {
            get { return btnLabel; }
            set { btnLabel = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PaymentInfo> Payments { get; }

        #endregion Binding Vars

        #region Commands
        public ICommand LoadPaymentsCommand { get; }
        public ICommand AddPaymentCommand => new Command(SavePaymentItemAsync);
        public ICommand RemovePaymentCommand { get; }
        public ICommand EditPaymentCommand { get; }
        #endregion Commands

        private bool editMode = false;
        private int paymentId = -1;

        public PaymentInfoViewModel()
        {
            Payments = new ObservableCollection<PaymentInfo>();
            Task.Run(LoadPaymentsAsync);

            LoadPaymentsCommand = new Command(LoadView);
            //AddPaymentCommand = new Command(AddPaymentItemAsync);
            RemovePaymentCommand = new Command(RemovePaymentAsync);
            EditPaymentCommand = new Command(EditPaymentAsync);
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
        public async void SavePaymentItemAsync()
        {
            if (editMode)
            {
                var payment = Payments.First(item => item.Id == paymentId);
                payment.Bill = (int)Amount;
                payment.DueDay = (int)Day;
                payment.Description = Description;
                await PaymentDataStore.UpdateItem(payment);
            }
            else
            {
                var payment = new PaymentInfo
                {
                    Id = Payments.Max(obj => obj.Id) + 1,
                    Bill = (int)Amount,
                    DueDay = (int)Day,
                    Description = Description,

                };
                await PaymentDataStore.AddItem(payment);
                Payments.Add(payment);
            }

            Amount = null;
            Day = null;
            Description = "";

            paymentId = -1;
            editMode = false;
            BtnLabel = "Add Payment";

            await LoadPaymentsAsync();
        }
        public async void RemovePaymentAsync(object id)
        {
            var payment = await PaymentDataStore.GetItem((int)id);
            await PaymentDataStore.RemoveItem((int)id);
            Payments.Remove(payment);

            await LoadPaymentsAsync();
        }
        public void EditPaymentAsync(object id)
        {
            //var payment = await PaymentDataStore.GetItem((int)id);
            var payment = Payments.First(obj => obj.Id == (int)id);
            Description = payment.Description;
            Amount = payment.Bill;
            Day = payment.DueDay;
            paymentId = payment.Id;
            editMode = true;
            BtnLabel = "Update Payment";
        }
    }
}
