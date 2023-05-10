namespace MoneyManagerApp.Services
{
    public interface IDataStore<T>
    {
        public Task<T> GetItem(int id);
        public Task<IEnumerable<T>> GetItems();
        public Task<bool> AddItem(T item);
    }
}
