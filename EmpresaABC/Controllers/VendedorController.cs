using Business.Repositories;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresaABC.Controllers
{
    public class VendedorController : GenericController<DataContext, Vendedores>
    {
        RVendedores business;
        public VendedorController(RVendedores business) : base(business)
        {

        }
    }
}
