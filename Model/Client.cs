
namespace HMS.Model
{
    public class Client:Person
    {
        private string _account;
        private Room _room;
       
        public string Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged();
         
            }
        }

        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }
    }
}
