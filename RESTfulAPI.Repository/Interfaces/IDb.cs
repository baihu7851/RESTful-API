using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IDb
    {
        IDbConnection GetDb();
    }
}