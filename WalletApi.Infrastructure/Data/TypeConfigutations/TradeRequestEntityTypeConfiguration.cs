using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain;

namespace WalletApi.Infrastructure.Data.TypeConfigutations
{
    internal class TradeRequestEntityTypeConfiguration : IEntityTypeConfiguration<TradeRequest>
    {
        #region public Methods
        public void Configure(EntityTypeBuilder<TradeRequest> builder)
        {
            builder.ToTable("traderequest");
            builder.HasKey(item => item.Id);
        }
        #endregion
    }
}
