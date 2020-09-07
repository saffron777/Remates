using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GCIT.Core.Entities;

namespace GCIT.Core.Repository
{
    public class RematesContext: DbContext
    {
        public RematesContext(DbContextOptions<RematesContext> options) :base(options)
        {

        }

        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<UserCredentials> UserCredentials { get; set; }        
        
    }
}
