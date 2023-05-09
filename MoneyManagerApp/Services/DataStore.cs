using MoneyManagerApp.Models;

namespace MoneyManagerApp.Services
{
    public class DataStore : IDataStore<PaymentInfo>
    {
        private readonly List<PaymentInfo> _payments;
        public DataStore()
        {
            _payments = new List<PaymentInfo>()
            {
                new PaymentInfo()
                {
                    Id = 0,
                    Bill = 100,
                    Description = "Internet",
                    DueDay = 21,
                },
                new PaymentInfo()
                {
                    Id = 1,
                    Bill = 300,
                    Description = "Utilities",
                    DueDay = 3,
                },
                new PaymentInfo()
                {
                    Id = 2,
                    Bill = 75,
                    Description = "TV",
                    DueDay = 16,
                }
            };
        }
        public async Task<bool> AddItem(PaymentInfo item)
        {
            _payments.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<PaymentInfo> GetItem(int id)
        {
            var item = _payments.Find(p => p.Id == id);
            return await Task.FromResult(item);
        }

        public async Task<List<PaymentInfo>> GetItems()
        {
            return await Task.FromResult(_payments);
        }
    }
}
