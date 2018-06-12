using Business.Interfaces;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Repositories
{
    public class RVendedores : RepositoryBase<DataContext, Vendedores>, IVendedores
    {
    }
}
