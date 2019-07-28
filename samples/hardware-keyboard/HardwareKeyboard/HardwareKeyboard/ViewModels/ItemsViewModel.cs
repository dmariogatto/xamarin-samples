using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HardwareKeyboard.Models;

namespace HardwareKeyboard.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command<Item> AddItemCommand { get; set; }
        public Command ClearItemsCommand { get; set; }

        private bool _isEmpty = false;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        public ItemsViewModel()
        {
            Title = "Key Commands";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command<Item>(async (i) => await AddItem(i));
            ClearItemsCommand = new Command(async () => await ClearItems());
        }

        private async Task LoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                IsEmpty = Items.Count == 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddItem(Item item)
        {
            Items.Insert(0, item);
            await DataStore.AddItemAsync(item);

            IsEmpty = false;
        }

        private async Task ClearItems()
        {
            Items.Clear();
            await DataStore.ClearItemsAsync();
        }
    }
}