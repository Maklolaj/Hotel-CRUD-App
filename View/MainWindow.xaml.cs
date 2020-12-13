using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using HMS.DatabaseContext;
using HMS.Model;
using HMS.ViewModel;

namespace HMS
{

    public partial class MainWindow : Window
    {

        private HotelContext Context { get; }

        public MainWindow()
        {
            InitializeComponent();

            RoomTypeCb.ItemsSource = RtCbFilter.ItemsSource = Enum.GetNames(typeof(RoomTypes));

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<HotelContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<HotelContext>());
            Context = new HotelContext();

            ClientsTab.DataContext = new ClientsTabViewModel(Context);
            RoomsTab.DataContext = new RoomTabViewModel(Context);


        }

    }
}
