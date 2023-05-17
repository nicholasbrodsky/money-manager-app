namespace MoneyManagerApp.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Paycheck { get; set; }
        public enum Pay
        {
            Biweekly,
            Weekly,
            Monthly
        }
        public Pay PayPeriod { get; set; }
        public DateTime LastPaidDate { get; set; }
        //public DateTime NextPaidDate { get; set; }
        public int PayPeriodPayment { get; set; }
    }
}
