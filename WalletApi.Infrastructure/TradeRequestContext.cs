using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain;
using WalletApi.Framework;
using WalletApi.Infrastructure.Data.TypeConfigutations;

namespace WalletApi.Infrastructure
{
    public class TradeRequestContext : IdentityDbContext, IUnitOfWork
    {
        #region Constructor
        public TradeRequestContext([NotNullAttribute] DbContextOptions<TradeRequestContext> options) : base(options) { }

        public TradeRequestContext() : base() { }
        #endregion
        #region InternalMethods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TradeRequestEntityTypeConfiguration());
        }
        #endregion
        #region Propreties
        public DbSet<TradeRequest> TradeRequest { get; set; }
        #endregion
    }
}
