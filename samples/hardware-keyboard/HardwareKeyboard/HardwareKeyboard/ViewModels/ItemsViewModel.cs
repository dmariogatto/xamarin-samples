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
            get { return _isEmpty; }
            set { SetProperty(ref _isEmpty, value); }
        }

        public ItemsViewModel()
        {
            Title = "Key Commands";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command<Item>(async (i) => await ExecuteAddItemCommand(i));
            ClearItemsCommand = new Command(async () => await ExecuteClearItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
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

        private async Task ExecuteAddItemCommand(Item item)
        {
            Items.Insert(0, item);
            await DataStore.AddItemAsync(item);

            IsEmpty = false;
        }

        private async Task ExecuteClearItemsCommand()
        {
            Items.Clear();
            await DataStore.ClearItemsAsync();
        }
    }
}