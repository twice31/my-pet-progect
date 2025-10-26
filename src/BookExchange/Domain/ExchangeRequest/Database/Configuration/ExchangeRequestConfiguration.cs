using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ExchangeRequest;
using Domain.ExchangeRequest.VO;
using Data.Converters;

namespace Data.Configurations
{
    /// Конфигурация для маппинга агрегата ExchangeRequest на таблицу базы данных.
    public sealed class ExchangeRequestConfiguration : IEntityTypeConfiguration<ExchangeRequest>
    {
        public void Configure(EntityTypeBuilder<ExchangeRequest> builder)
        {
            builder.ToTable("ExchangeRequests");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasConversion(
                    requestId => requestId.Value,
                    value => RequestId.Create(value)
                )
                .HasColumnName("RequestId")
                .IsRequired();

            builder.Property(e => e.RequestedBookId)
                .HasConversion(
                    id => id.Value,
                    value => RequestedBookId.Create(value)
                )
                .HasColumnName("RequestedBookId")
                .IsRequired();

            builder.Property(e => e.RecipientId)
                .HasConversion(
                    id => id.Value,
                    value => RecipientId.Create(value)
                )
                .HasColumnName("RecipientId")
                .IsRequired();

            builder.Property(e => e.BookOwnerId)
                .HasConversion(
                    id => id.Value,
                    value => BookOwnerId.Create(value)
                )
                .HasColumnName("BookOwnerId")
                .IsRequired();

            builder.Property(e => e.Method)
                .HasConversion(
                    method => method.Method,
                    value => ExchangeMethod.Create(value)
                )
                .HasColumnName("ExchangeMethod")
                // Используем константу из домен слоя 
                .HasMaxLength(ExchangeMethod.MAX_LENGTH)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion(
                    status => status.Key,
                    key => ExchangeRequestStatus.FromKey(key)
                )
                .HasColumnName("RequestStatusKey")
                .IsRequired();


            builder.OwnsOne(e => e.History, historyBuilder =>
            {
                historyBuilder.Property(h => h.Events)
                    .HasConversion(new StringListConverter())
                    .HasColumnName("ExchangeHistoryEvents")
                    .IsRequired();
            });

            builder.Navigation(e => e.History).IsRequired();
        }
    }
}