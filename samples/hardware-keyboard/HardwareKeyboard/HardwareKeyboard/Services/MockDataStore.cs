using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareKeyboard.Models;

namespace HardwareKeyboard.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        private List<Item> _items;

        public MockDataStore()
        {
            _items = new List<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            _items.Insert(0, item);
            return await Task.FromResult(true);
        }

        public async Task<bool> ClearItemsAsync()
        {
            _items.Clear();
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}