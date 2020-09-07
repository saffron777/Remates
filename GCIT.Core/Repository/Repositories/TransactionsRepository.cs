﻿using System;
using System.Collections.Generic;
using System.Text;
using CGIT.Core.Repository;
using GCIT.Core.Entities;
using GCIT.Core.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GCIT.Core.Repository.Repositories
{
    public class TransactionsRepository : Repository<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
