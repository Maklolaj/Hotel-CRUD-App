using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HMS.DatabaseContext;
using HMS.Model;
using Microsoft.Win32;

namespace HMS.ViewModel
{
    class ClientsTabViewModel : ViewModelBase
    {
        private IList<Client> _filteredClientList;

        public HotelContext Context { get; }
        public Client ClientInfo { get; set; } = new Client();
        public Client ClientFilter { get; set; } = new Client();
        public Client SelectedClient { get; set; }

        public ClientsTabViewModel(HotelContext context)
        {
            Context = context;
            Context.Clients.Load();
        }

        public IList<Client> FilteredClientList
        {
            get => _filteredClientList;
            set
            {
                _filteredClientList = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _addClientCommand;
        private RelayCommand _updateClientCommand;
        private RelayCommand _deleteClientCommand;
        private RelayCommand _clientsGridSelectionChangedCommand;
        private RelayCommand _clientsFilterChangedCommand;


        public ICommand AddClientCommand =>
            _addClientCommand ??
            (_addClientCommand = new RelayCommand(
                () =>
                {
                    Context.Clients.Add(new Client
                    {
                        FirstName = ClientInfo.FirstName,
                        LastName = ClientInfo.LastName,
                        BirthDate = ClientInfo.BirthDate,
                        Account = ClientInfo.Account,
                        Room = ClientInfo.Room
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(ClientInfo.FirstName)
                        || string.IsNullOrEmpty(ClientInfo.LastName)
                        || ClientInfo.Room == null)
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateClientCommand =>
            _updateClientCommand ??
            (_updateClientCommand = new RelayCommand(
                () =>
                {
                    SelectedClient.FirstName = ClientInfo.FirstName;
                    SelectedClient.LastName = ClientInfo.LastName;
                    SelectedClient.BirthDate = ClientInfo.BirthDate;
                    SelectedClient.Account = ClientInfo.Account;
                    SelectedClient.Room = ClientInfo.Room;
                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedClient == null) return false;
                    if (string.IsNullOrEmpty(ClientInfo.FirstName)
                        || string.IsNullOrEmpty(ClientInfo.LastName)
                        || ClientInfo.Room == null)
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteClientCommand =>
            _deleteClientCommand ??
            (_deleteClientCommand = new RelayCommand(
                () =>
                {
                    Context.Clients.Remove(SelectedClient);
                    Context.SaveChanges();
                },
                () => SelectedClient != null));


        public ICommand ClientsGridSelectionChangedCommand =>
            _clientsGridSelectionChangedCommand ??
            (_clientsGridSelectionChangedCommand =
                new RelayCommand(
                    () =>
                    {
                        ClientInfo.FirstName = SelectedClient.FirstName;
                        ClientInfo.LastName = SelectedClient.LastName;
                        ClientInfo.BirthDate = SelectedClient.BirthDate;
                        ClientInfo.Account = SelectedClient.Account;
                        ClientInfo.Room = SelectedClient.Room;
                    },
                    () => SelectedClient != null));


        public ICommand ClientsFilterChangedCommand =>
            _clientsFilterChangedCommand ?? (_clientsFilterChangedCommand =
                new RelayCommand(() =>
                {
                    IEnumerable<Client> queryResult = Context.Clients.Local;
                    if (!string.IsNullOrEmpty(ClientFilter.FirstName))
                    {
                        queryResult = queryResult.Where(client => client.FirstName.Contains(ClientFilter.FirstName));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.LastName))
                    {
                        queryResult = queryResult.Where(client => client.LastName.Contains(ClientFilter.LastName));
                    }
                    if (ClientFilter.BirthDate != null)
                    {
                        queryResult = queryResult.Where(client => client.BirthDate == ClientFilter.BirthDate);
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.Account))
                    {
                        queryResult = queryResult.Where(client => client.Account.Contains(ClientFilter.Account));
                    }
                    if (ClientFilter.Room != null)
                    {
                        queryResult = queryResult.Where(client => client.Room == ClientFilter.Room);
                    }
                    FilteredClientList = queryResult?.ToList();
                }));


    }

    

}
