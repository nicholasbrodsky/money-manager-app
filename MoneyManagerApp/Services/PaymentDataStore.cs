using MoneyManagerApp.Models;

namespace MoneyManagerApp.Services
{
    public class PaymentDataStore : IDataStore<PaymentInfo>
    {
        private readonly List<PaymentInfo> _payments;
        public PaymentDataStore()
        {
            _payments = new List<PaymentInfo>()
            {
                new PaymentInfo()
                {
                    Id = 0,
                    Bill = 85,
                    Description = "Internet",
                    DueDay = 21,
                },
                new PaymentInfo()
                {
                    Id = 1,
                    Bill = 200,
                    Description = "SCE",
                    DueDay = 1,
                },
                new PaymentInfo()
                {
                    Id = 2,
                    Bill = 30,
                    Description = "SoCalGas",
                    DueDay = 21,
                },
                new PaymentInfo()
                {
                    Id = 3,
                    Bill = 75,
                    Description = "TV",
                    DueDay = 16,
                },
                new PaymentInfo()
                {
                    Id = 4,
                    Bill = 27,
                    Description = "Gym",
                    DueDay = 3,
                },
                new PaymentInfo()
                {
                    Id = 5,
                    Bill = 150,
                    Description = "AAA",
                    DueDay = 24,
                },
                new PaymentInfo()
                {
                    Id = 6,
                    Bill = 80,
                    Description = "Charlie's Meds (chewy)",
                    DueDay = 3,
                },
                new PaymentInfo()
                {
                    Id = 7,
                    Bill = 50,
                    Description = "Charlie's Meds (vet)",
                    DueDay = 12,
                },
                new PaymentInfo()
                {
                    Id = 8,
                    Bill = 17,
                    Description = "Hulu/Disney+/ESPN+",
                    DueDay = 11,
                },
                new PaymentInfo()
                {
                    Id = 9,
                    Bill = 8,
                    Description = "Paramount+",
                    DueDay = 10,
                },
                new PaymentInfo()
                {
                    Id = 10,
                    Bill = 5,
                    Description = "Peacock",
                    DueDay = 3,
                },
                new PaymentInfo()
                {
                    Id = 11,
                    Bill = 10,
                    Description = "DoorDash",
                    DueDay = 10,
                },
                new PaymentInfo()
                {
                    Id = 12,
                    Bill = 15,
                    Description = "NordVPM",
                    DueDay = 11,
                },
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

        public async Task<IEnumerable<PaymentInfo>> GetItems()
        {
            return await Task.FromResult(_payments);
        }

        public async Task<bool> RemoveItem(int id)
        {
            var payment = await GetItem(id);
            return await Task.FromResult(_payments.Remove(payment));
        }

        public async Task<bool> UpdateItem(PaymentInfo item)
        {
            var payment = GetItem(item.Id);
            int index = _payments.FindIndex(p => p.Id == item.Id);
            _payments[index] = item;
            return await Task.FromResult(true);
        }
    }
}
