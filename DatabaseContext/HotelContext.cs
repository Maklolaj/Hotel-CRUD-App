using System.Data.Entity;
using HMS.Model;

namespace HMS.DatabaseContext
{
    public class HotelContext : DbContext
    {
        public HotelContext(string connectionString = "MyConnectionString") : base(connectionString)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PersonConfig());
            modelBuilder.Configurations.Add(new ClientConfig());
            modelBuilder.Configurations.Add(new RoomConfig());
        }


    }
}
