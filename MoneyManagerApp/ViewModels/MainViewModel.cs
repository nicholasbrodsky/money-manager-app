using MoneyManagerApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MoneyManagerApp.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		#region Binding Vars

		private UserInfo user;
		public UserInfo User
		{
			get { return user; }
			set { user = value; OnPropertyChanged(); }
		}

		private string lastPayDay;
		public string LastPayDay
		{
			get { return lastPayDay; }
			set { lastPayDay = value; OnPropertyChanged(); }
		}

		private string nextPayDay;
		public string NextPayDay
		{
			get { return nextPayDay; }
			set { nextPayDay = value; OnPropertyChanged(); }
		}

		private int totalOwed;
		public int TotalOwed
		{
			get { return totalOwed; }
			set { totalOwed = value; OnPropertyChanged(); }
		}

		private int moneyAvailable;
		public int MoneyAvailable
		{
			get { return moneyAvailable; }
			set { moneyAvailable = value; OnPropertyChanged(); }
		}

		private int moneySpent;
		public int MoneySpent
		{
			get { return moneySpent; }
			set { moneySpent = value; OnPropertyChanged(); }
		}

		private ObservableCollection<PaymentInfo> paymentsMade;
		public ObservableCollection<PaymentInfo> PaymentsMade
		{
			get { return paymentsMade; }
			set { paymentsMade = value; OnPropertyChanged(); }
		}

		private int remainingOwed;
		public int RemainingOwed
		{
			get { return remainingOwed; }
			set { remainingOwed = value; OnPropertyChanged(); }
		}

		private ObservableCollection<PaymentInfo> paymentsRemaining;
		public ObservableCollection<PaymentInfo> PaymentsRemaining
		{
			get { return paymentsRemaining; }
			set { paymentsRemaining = value; OnPropertyChanged(); }
		}

		#region Temp Info
		private string tempDescription;
		public string TempDescription
		{
			get { return tempDescription; }
			set { tempDescription = value; OnPropertyChanged(); }
		}

		private int? tempAmount;
		public int? TempAmount
		{
			get { return tempAmount; }
			set { tempAmount = value; OnPropertyChanged(); }
		}

		private int? tempDay;
		public int? TempDay
		{
			get { return tempDay; }
			set { tempDay = value; OnPropertyChanged(); }
		}
		#endregion Temp Info

		#endregion Binding Vars

		#region Commands
		public ICommand loadCommand;
		public ICommand LoadCommand
		{
			get { return loadCommand; }
			set { loadCommand = value; }
		}
		private ICommand addTempCommand;
		public ICommand AddTempCommand
		{
			get { return addTempCommand; }
			set { addTempCommand = value; }
		}
		#endregion Commands

		public MainViewModel()
		{
			//GetCurrentInfo();
			Task.Run(SetCurrentInfo);
			//Thread test = new Thread(new ThreadStart(GetCurrentInfo))
			//{
			//	IsBackground = true,

			//};
			//test.Start();

			LoadCommand = new Command(LoadPaymentCollections);
			AddTempCommand = new Command(AddTempPayment);
		}

		public async Task SetCurrentInfo()
		{
			User = await UserDataStore.GetItem(0);

			PaymentsMade = new ObservableCollection<PaymentInfo>();
			PaymentsRemaining = new ObservableCollection<PaymentInfo>();

			LastPayDay = User.LastPaidDate.ToShortDateString();
			NextPayDay = User.LastPaidDate.AddDays(14).ToShortDateString();

			await GetPayments();
		}

		public async Task GetPayments()
		{
			var lastPaidDate = User.LastPaidDate;
			var currentDate = DateTime.Now;
			var nextPaidDate = lastPaidDate.AddDays(14);

			int lastPaidDay = User.LastPaidDate.Day;
			//int nextPaidDay = User.LastPaidDate.AddDays(14).Day;
			//int currentDay = DateTime.Now.Day;

			TotalOwed = 0;
			MoneySpent = 0;
			RemainingOwed = 0;

			PaymentsMade.Clear();
			PaymentsRemaining.Clear();

			var payments = await PaymentDataStore.GetItems();
			foreach (var payment in payments)
			{
				DateTime billDate;
				if (payment.DueDay >= lastPaidDay)
				{
					billDate = new DateTime(lastPaidDate.Year, lastPaidDate.Month, payment.DueDay);
				}
				else /*if (payment.DueDay < nextPaidDay)*/
				{
					billDate = new DateTime(nextPaidDate.Year, nextPaidDate.Month, payment.DueDay);
				}

				if (billDate >= lastPaidDate && billDate <= currentDate)
				{
					MoneySpent += payment.Bill;
					PaymentsMade.Add(payment);
				}
				if (billDate > currentDate && billDate < nextPaidDate)
				{
					RemainingOwed += payment.Bill;
					PaymentsRemaining.Add(payment);
				}
				if (billDate >= lastPaidDate && billDate < nextPaidDate)
				{
					TotalOwed += payment.Bill;
				}
			}
			MoneyAvailable = User.Paycheck - TotalOwed;
		}

		public async void LoadPaymentCollections()
        {
            await GetPayments();
            ClearTempFields();
            Refresh = false;
        }

		public async void AddTempPayment()
        {
            if (TempAmount == null || TempDay is null || string.IsNullOrEmpty(TempDescription))
                return;

            await PaymentDataStore.AddItem(new PaymentInfo
            {
                Id = 1,
                Description = TempDescription,
                Bill = (int)TempAmount,
                DueDay = (int)TempDay,
            });
            await GetPayments();

            ClearTempFields();
            //Refresh = true;
        }

        public void ClearTempFields()
        {
            TempAmount = null;
            TempDay = null;
            TempDescription = "";
        }
    }
}
