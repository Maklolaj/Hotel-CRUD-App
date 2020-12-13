using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace HMS.Model
{
    public enum RoomTypes
    {
        None,
        StandardRoom,
        FamilyRoom,
        DeluxeRoom,
        PresidentialSuite,
    }

    public class Room:INotifyPropertyChanged
    {
 
        private int _roomId;
        private string _number;
        private RoomTypes _type;
        public IList<Client> _clients;

        public RoomTypes Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public IList<Client> Clients //add virtual?
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();

            }
        }

        public int RoomId
        {
            get => _roomId;
            set
            {
                _roomId = value;
                OnPropertyChanged();
            }
        }
        
        public Room()
        {
            _clients = new List<Client>();
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
