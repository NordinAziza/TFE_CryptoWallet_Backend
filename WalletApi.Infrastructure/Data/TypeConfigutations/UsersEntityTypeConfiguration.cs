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
    internal class UsersEntityTypeConfiguration : IEntityTypeConfiguration<Users>
    {
        #region public Methods
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(item => item.Id);
        }
        #endregion
    }
}
