using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CapitanConfiguration: IEntityTypeConfiguration<Capitan>
    {
        public void Configure(EntityTypeBuilder<Capitan> builder)
        {
            builder.HasData(
                new Capitan
                {
                    Id = new Guid("305a8736-8187-4854-8686-f6869493b302"),
                    Name= "Jmishenko Valeriy",
                    Address= "Voroshilova 5"
                },
                new Capitan 
                {
                    Id = new Guid("27feac3d-b9d9-429f-8ca4-a520513fa714"),
                    Name = "Denis Tkacev",
                    Address = "Volgogradskaya 74"
                }
            );
        }
    }
}
