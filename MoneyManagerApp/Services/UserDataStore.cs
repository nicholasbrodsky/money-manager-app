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
                    Paycheck = 2400,
                    PayPeriod = 0,
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

        public async Task<List<UserInfo>> GetItems()
        {
            return await Task.FromResult(_users);
        }
    }
}
