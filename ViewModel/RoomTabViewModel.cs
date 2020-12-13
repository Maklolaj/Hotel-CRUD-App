using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.DatabaseContext;
using HMS.Model;
using System.Data.Entity;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace HMS.ViewModel
{
    public class RoomTabViewModel : ViewModelBase
    {
        private Room _selectedRoom;
        private IList<Room> _filteredRoomList;

        public HotelContext Context { get; }

        public Room RoomInfo { get; set; } = new Room();

        public Room RoomFilter { get; set; } = new Room();

        public int RoomFreedomFilterIndex { get; set; }

        public RoomTabViewModel(HotelContext context)
        {
            Context = context;
            Context.Rooms.Load();
        }

        public IList<Room> FilteredRoomList
        {
            get => _filteredRoomList;
            set
            {
                _filteredRoomList = value;
                RaisePropertyChanged();
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                RaisePropertyChanged();
            }
        }


        private RelayCommand _addRoomCommand;
        private RelayCommand _updateRoomCommand;
        private RelayCommand _deleteRoomCommand;
        private RelayCommand _roomsGridSelectionChangedCommand;
        private RelayCommand _roomsFilterChangedCommand;


        public ICommand AddRoomCommand =>
            _addRoomCommand ??
            (_addRoomCommand = new RelayCommand(
                () =>
                {
                    Context.Rooms.Add(new Room
                    {
                        Number = RoomInfo.Number,
                        Type = RoomInfo.Type
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(RoomInfo.Number) || RoomInfo.Type == RoomTypes.None)
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateRoomCommand =>
            _updateRoomCommand ??
            (_updateRoomCommand = new RelayCommand(
                () =>
                {
                    SelectedRoom.Number = RoomInfo.Number;
                    SelectedRoom.Type = RoomInfo.Type;
                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedRoom == null) return false;
                    if (string.IsNullOrEmpty(RoomInfo.Number) || RoomInfo.Type == RoomTypes.None)
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteRoomCommand =>
            _deleteRoomCommand ??
            (_deleteRoomCommand = new RelayCommand(
                () =>
                {
                    Context.Rooms.Remove(SelectedRoom);
                    Context.SaveChanges();
                },
                () => SelectedRoom != null));

        public ICommand RoomsGridSelectionChangedCommand =>
            _roomsGridSelectionChangedCommand ??
            (_roomsGridSelectionChangedCommand =
                new RelayCommand(
                    () =>
                    {
                        RoomInfo.Number = SelectedRoom.Number;
                        RoomInfo.Type = SelectedRoom.Type;
                    },
                    () => SelectedRoom != null));

        public ICommand RoomsFilterChangedCommand =>
            _roomsFilterChangedCommand ?? (_roomsFilterChangedCommand =
                new RelayCommand(() =>
                {
                    IEnumerable<Room> queryResult = Context.Rooms.Local;
                    if (!string.IsNullOrEmpty(RoomFilter.Number))
                    {
                        queryResult = queryResult.Where(room => room.Number.Contains(RoomFilter.Number));
                    }
                    if (RoomFilter.Type != RoomTypes.None)
                    {
                        queryResult = queryResult.Where(room => room.Type == RoomFilter.Type);
                    }
                    if (RoomFreedomFilterIndex == 1)
                    {
                        queryResult = queryResult.Where(room => room.Clients.Count == 0);
                    }
                    if (RoomFreedomFilterIndex == 2)
                    {
                        queryResult = queryResult.Where(room => room.Clients.Count > 0);
                    }
                    FilteredRoomList = queryResult?.ToList();
                }));

    }
}
