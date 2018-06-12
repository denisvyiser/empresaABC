using Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Repositories
{
    public class RepositoryBase<Context, TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class where Context : DbContext, new()
    {

        private Context db = new Context();

        public Context c
        {
            get { return db; }
            set { db = value; }
        }

        public Utilitie<List<TEntity>> GetAll()
        {
            Utilitie<List<TEntity>> resp;
            try
            {
                List<TEntity> query = db.Set<TEntity>().ToList();

                resp = new Utilitie<List<TEntity>>(query, "[GetAll] Lista de dados");

                //resp.Mensagem = "[GetAll] Lista de dados";
                //resp.Dados = query;
                //resp.Status = true;


            }
            catch (Exception ex)
            {
                resp = new Utilitie<List<TEntity>>("[GetAll] Erro ao Listar dados: " + ex,false);

                //resp.Mensagem = "[GetAll] Erro ao Listar dados: " + ex;
                //resp.Dados = null;
                //resp.Status = false;

            }
            return resp;
        }

        public Utilitie<TEntity> Insert(TEntity obj)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {
                db.Set<TEntity>().Add(obj);

                db.SaveChanges();

                resp.Mensagem = "[Insert] Dados Cadastrados";
                resp.Dados = null;
                resp.Status = true;
                             
            }
            catch (Exception ex)
            {
                resp.Mensagem = "[Insert] Dados Cadastrados: " + ex;
                resp.Dados = null;
                resp.Status = false;

           }
            return resp;
        }
        public Utilitie<TEntity> Delete(int id)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {
                TEntity obj = db.Set<TEntity>().Find(id);
                db.Set<TEntity>().Remove(obj);

                db.SaveChanges();

                resp.Mensagem = "[Delete] Dado Removido";
                resp.Dados = null;
                resp.Status = true;
                

            }
            catch (Exception ex)
            {                
                resp.Mensagem = "[Delete] Erro ao Cadastrar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;
                          
            }
            return resp;
        }

        //public Utilitie<TEntity> Delete(TEntity obj)
        //{
        //    Utilitie<TEntity> resp = new Utilitie<TEntity>();
        //    try
        //    {
        //        db.Set<TEntity>().Attach(obj);
        //        db.Set<TEntity>().Remove(obj);

        //        db.SaveChanges();

        //        resp.Mensagem = "[Delete] Dado Removido";
        //        resp.Dados = null;
        //        resp.Status = true;


        //    }
        //    catch (Exception ex)
        //    {
        //        resp.Mensagem = "[Delete] Erro ao Cadastrar dados: " + ex;
        //        resp.Dados = null;
        //        resp.Status = false;

        //    }
        //    return resp;
        //}

        Utilitie<TEntity> IRepositoryBase<TEntity>.DeleteBy(Expression<Func<TEntity, bool>> predicate)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {

                List<TEntity> dados = db.Set<TEntity>()
               .Where(predicate).ToList();

                dados.ForEach(del => db.Set<TEntity>().Remove(del));

                int registros = db.SaveChanges();

                resp.Mensagem = "[DeleteBy] Dados(" + registros.ToString() + ")Removido";
                resp.Dados = null;
                resp.Status = true;

            }
            catch (Exception ex)
            {
                resp.Mensagem = "[DeleteBy] Erro ao remover dados em Lote: " + ex;
                resp.Dados = null;
                resp.Status = true;


            }
            return resp;

        }
        public Utilitie<TEntity> Get(int id)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {

                TEntity dados = db.Set<TEntity>().Find(id);

                resp.Mensagem = "[Get] Procurar pelo ID";
                resp.Dados = dados;
                resp.Status = true;

          

            }
            catch (Exception ex)
            {
                resp.Mensagem = "[Get] Erro ao Buscar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;



            }
            return resp;
        }

        public Utilitie<IQueryable<TEntity>> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            Utilitie<IQueryable<TEntity>> resp = new Utilitie<IQueryable<TEntity>>();
            try
            {
                IQueryable<TEntity> query = db.Set<TEntity>().Where(predicate);

                resp.Mensagem = "[GetBy] Lista de dados";
                resp.Dados = query;
                resp.Status = true;

  
            }
            catch (Exception ex)
            {
                resp.Mensagem = "[GetBy] Erro ao Listar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;
                
            }
            return resp;
        }
             

        public Utilitie<TEntity> Update(TEntity Entidade)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {

                db.Entry(Entidade).State = EntityState.Modified;

                db.SaveChanges();

                resp.Mensagem = "[Update] Dado Atualizados";
                resp.Dados = null;
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

        //public Utilitie<TEntity> UpdateBy(List<TEntity> dados)
        //{
        //    Utilitie<TEntity> resp = new Utilitie<TEntity>();
        //    try
        //    {

        //        dados.ForEach(up =>
        //    {
        //        db.Set<TEntity>().Attach(up);
        //        db.Entry(dados).State = EntityState.Modified;
        //        db.SaveChanges();

        //        db.Entry(dados).State = EntityState.Detached;
        //    });
        //        resp.Mensagem = "[UpdateBy] Dados Atualizados";
        //        resp.Dados = null;
        //        resp.Status = true;


        //    }
        //    catch (Exception ex)
        //    {
        //        resp.Mensagem = " [UpdateBy] Erro ao Cadastrar dados: " + ex;
        //        resp.Dados = null;
        //        resp.Status = false;

        //    }
        //    return resp;
        //}

        public Utilitie<TEntity> UpdateList(List<TEntity> dados)
        {
            Utilitie<TEntity> resp = new Utilitie<TEntity>();
            try
            {

                dados.ForEach(up =>
                {
                    db.Set<TEntity>().Attach(up);
                    db.Entry(dados).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Entry(dados).State = EntityState.Detached;
                });
                resp.Mensagem = "[UpdateBy] Dados Atualizados";
                resp.Dados = null;
                resp.Status = true;


            }
            catch (Exception ex)
            {
                resp.Mensagem = " [UpdateBy] Erro ao Cadastrar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;

            }
            return resp;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        Utilitie<IQueryable<TEntity>> IRepositoryBase<TEntity>.GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            Utilitie<IQueryable<TEntity>> resp = new Utilitie<IQueryable<TEntity>>();
            try
            {
                IQueryable<TEntity> query = db.Set<TEntity>().Where(predicate);

                resp.Mensagem = "[GetBy] Lista de dados";
                resp.Dados = query;
                resp.Status = true;

//                Log.Info(resp.Mensagem);
            }
            catch (Exception ex)
            {
                resp.Mensagem = "[GetBy] Erro ao Listar dados: " + ex;
                resp.Dados = null;
                resp.Status = false;

 //               Log.Error(resp.Mensagem);

            }
            return resp;
        }

     
    }
}