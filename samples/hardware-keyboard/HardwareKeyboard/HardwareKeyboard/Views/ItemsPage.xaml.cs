﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

using HardwareKeyboard.Models;
using HardwareKeyboard.ViewModels;
using HardwareKeyboard.Controls;

namespace HardwareKeyboard.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : KeyboardPage
    {
        private ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        public override void OnKeyUp(string text, string description)
        {
            var newItem = new Item() { Text = text, Description = description };
            _viewModel.AddItemCommand.Execute(newItem);
        }

        public override void OnKeyCommand(KeyCommand command)
        {
            var newItem = new Item() { Text = command.ToString(), Description = string.Empty };
            _viewModel.AddItemCommand.Execute(newItem);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            _viewModel.ClearItemsCommand.Execute(null);
        }
    }
}