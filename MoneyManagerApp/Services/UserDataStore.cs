using MoneyManagerApp.Models;

namespace MoneyManagerApp.Services
{
    public class UserDataStore : IDataStore<UserInfo>
    {
        private readonly List<UserInfo> _users;
        public UserDataStore()
        {
            _users = new List<UserInfo>
            {
                new UserInfo
                {
                    Id = 0,
                    Name = "Nicholas",
                    Paycheck = 1200,
                    PayPeriod = UserInfo.Pay.Biweekly,
                    LastPaidDate = new DateTime(2023, 11, 23),
                    PayPeriodPayment = 1200,
                }
            };
        }
        public async Task<bool> AddItem(UserInfo item)
        {
            _users.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<UserInfo> GetItem(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<UserInfo>> GetItems()
        {
            return await Task.FromResult(_users);
        }

        public Task<bool> RemoveItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItem(UserInfo item)
        {
            throw new NotImplementedException();
        }
    }
}
