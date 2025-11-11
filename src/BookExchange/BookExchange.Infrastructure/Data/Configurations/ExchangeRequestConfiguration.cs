using Domain.ExchangeRequest;
using Domain.ExchangeRequest.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BookExchange.Infrastructure.Data.Configurations
{
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
                    bookId => bookId.Value,
                    value => RequestedBookId.Create(value)
                )
                .HasColumnName("RequestedBookId")
                .IsRequired();

            builder.Property(e => e.RecipientId)
                .HasConversion(
                    recipientId => recipientId.Value,
                    value => RecipientId.Create(value)
                )
                .HasColumnName("RecipientId")
                .IsRequired();

            builder.Property(e => e.BookOwnerId)
                .HasConversion(
                    ownerId => ownerId.Value,
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
                .HasMaxLength(ExchangeMethod.MAX_LENGTH)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion(
                    status => status.Key,
                    key => ExchangeRequestStatus.FromKey(key)
                )
                .HasColumnName("StatusKey")
                .IsRequired();


            builder.OwnsOne(e => e.History, historyBuilder =>
            {
                historyBuilder.Property(h => h.Events)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<IReadOnlyList<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>().AsReadOnly()
                    )
                    .HasColumnName("ExchangeEvents")
                    .IsRequired();

                historyBuilder.Property(h => h.Events)
                    .Metadata.SetValueComparer(
                        new ValueComparer<IReadOnlyList<string>>(
                            (c1, c2) => c1!.SequenceEqual(c2!), 
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), 
                            c => c.ToList().AsReadOnly() 
                        )
                    );
            });
            builder.Navigation(e => e.History).IsRequired();
        }
    }
}