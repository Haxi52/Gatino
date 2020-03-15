using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Data
{
    public class ModelMapper
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            MapTopic(modelBuilder);
            MapMedia(modelBuilder);
        }

        private static void MapMedia(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Media>(e =>
            {
                e.ToTable(nameof(Entities.Media), "dbo");
                e.HasKey(i => i.MediaId).IsClustered(false);
                e.Property(p => p.MediaId)
                    .ValueGeneratedNever();

                e.Property(p => p.Path)
                    .HasMaxLength(250)
                    .IsRequired()
                    .IsUnicode(false);

                e.Property(p => p.MimeType)
                    .HasMaxLength(250)
                    .IsRequired()
                    .IsUnicode(false);

                e.Property(p => p.Data)
                    .HasMaxLength(250)
                    .IsRequired();

                MapAuditColumns(e);
            });
        }

        private static void MapTopic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Topic>(e =>
            {
                e.ToTable(nameof(Entities.Topic), "dbo");
                e.HasKey(i => i.TopicId);
                e.Property(i => i.TopicId)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                e.Property(i => i.Name)
                    .HasMaxLength(250)
                    .IsRequired()
                    .IsUnicode(false);

                e.Property(i => i.Path)
                    .HasMaxLength(250)
                    .IsRequired()
                    .IsUnicode(false);

                e.Property(i => i.Data)
                    .IsRequired()
                    .IsUnicode(false);

                MapAuditColumns(e);
            });
        }


        private static void MapAuditColumns<T>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> e)
            where T : class, IAuditEntity
        {
            e.Property(i => i.CreatedOn)
                .IsRequired();

            e.Property(i => i.CreatedBy)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode(false);

            e.Property(i => i.LastModifiedOn)
                .IsRequired();

            e.Property(i => i.LastModifiedBy)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}
