using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class RClientes : RepositoryBase<DataContext, Clientes>, IClientes
    {

        DataContext db;
        public Utilitie<Clientes> GerarCodigo(int id)
        {

            db = new DataContext();
            Utilitie<Clientes> resp = new Utilitie<Clientes>();
            try
            {
                Clientes cliente = db.clientes.Where(c => c.id == id).FirstOrDefault();

                cliente.codigo_acesso = Guid.NewGuid().ToString();

                db.Entry(cliente).State = EntityState.Modified;

                db.SaveChanges();

                resp.Mensagem = "[Update] Dado Atualizados";
                resp.Dados = cliente;
                resp.Status = true;



            }
            catch (Exception ex)
            {
                resp.Mensagem = "[Update] Erro ao Cadastrar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;

            }
            return resp;
        }
    }
}
