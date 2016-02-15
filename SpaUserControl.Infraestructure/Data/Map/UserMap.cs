using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SpaUserControl.Domain.Models;

namespace SpaUserControl.Infraestructure.Data.Map
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            // HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Email)
                .HasMaxLength(160)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Email", 1) { IsUnique = true }))
                .IsRequired();

            Property(x => x.Password)
                .HasMaxLength(32)
                .IsFixedLength();
        }
    }
}