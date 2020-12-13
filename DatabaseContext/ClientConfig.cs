using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HMS.Model;

namespace HMS.DatabaseContext
{
    public class ClientConfig : EntityTypeConfiguration<Client>
    {
        public ClientConfig()
        {
            // HasKey(client => client.ClientId);
            Property(client => client.Account).IsOptional().HasMaxLength(20);
            HasRequired(client => client.Room).WithMany(room => room.Clients).WillCascadeOnDelete(true);

            ToTable("Clients");
        }
    }
}
