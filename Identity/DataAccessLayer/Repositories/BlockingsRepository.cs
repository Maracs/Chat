﻿using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BlockingsRepository:Repository<Blocked>
    {

        public BlockingsRepository(DatabaseContext databaseContext) 
            : base(databaseContext)
        {

        }

    }
}