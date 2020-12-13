using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HMS.Model;

namespace HMS.DatabaseContext
{
    public class RoomConfig : EntityTypeConfiguration<Room>
    {
        public RoomConfig()
        {
            HasKey(room => room.RoomId);
            Property(room => room.Number).IsRequired().HasMaxLength(5);
            Property(room => room.Type).IsRequired();

            ToTable("Rooms");
        }
    }
}
