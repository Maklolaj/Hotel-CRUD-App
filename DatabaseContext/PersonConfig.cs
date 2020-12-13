using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HMS.Model;


namespace HMS.DatabaseContext
{
    public class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            HasKey(person => person.PersonId);
            Property(person => person.FirstName).IsRequired().HasMaxLength(50);
            Property(person => person.LastName).IsRequired().HasMaxLength(50);
            Property(person => person.BirthDate).HasColumnType("datetime2").IsOptional();

            ToTable("People");

        }
        
    }
}
