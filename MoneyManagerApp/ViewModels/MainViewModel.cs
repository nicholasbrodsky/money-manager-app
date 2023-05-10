using MoneyManagerApp.Models;
using System.Collections.ObjectModel;

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
		
		#endregion Binding Vars

		public MainViewModel()
		{
			_ = GetCurrentInfo();
		}

		public async Task GetCurrentInfo()
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
			var nextPaidDate = lastPaidDate.AddDays(14);

			var payments = await PaymentDataStore.GetItems();
            int lastPaidDay = User.LastPaidDate.Day;
			int nextPaidDay = User.LastPaidDate.AddDays(14).Day;
			int currentDay = DateTime.Now.Day;

			TotalOwed = 0;
			MoneySpent = 0;
			RemainingOwed = 0;

			PaymentsMade.Clear();
			PaymentsRemaining.Clear();

            foreach (var payment in payments)
			{

				if (payment.DueDay >= lastPaidDay || payment.DueDay <= currentDay)
				{
					MoneySpent += payment.Bill;
					PaymentsMade.Add(payment);
                }
                if (payment.DueDay > currentDay || payment.DueDay < nextPaidDay)
                {
                    RemainingOwed += payment.Bill;
                    PaymentsRemaining.Add(payment);
                }
				if (payment.DueDay >= lastPaidDay || payment.DueDay < nextPaidDay)
				{
					TotalOwed += payment.Bill;
				}
            }
        }
    }
}
