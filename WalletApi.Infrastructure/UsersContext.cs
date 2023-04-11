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
    public class UsersContext :DbContext , IUnitOfWork, 
    {
        #region Constructor
        public UsersContext([NotNullAttribute] DbContextOptions options) : base(options) { }

        public UsersContext() : base() { }
        #endregion
        #region InternalMethods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsersEntityTypeConfiguration());
        }
        #endregion
        #region Propreties
        public DbSet<Users> Users { get; set; }
        #endregion
    }
}
