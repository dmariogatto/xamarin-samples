using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareKeyboard.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> ClearItemsAsync();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
