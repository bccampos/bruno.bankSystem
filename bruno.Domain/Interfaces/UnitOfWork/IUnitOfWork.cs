﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(); 
    }
}
