using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IClientes
    {
        Utilitie<Clientes> GerarCodigo(int id);
    }
}
