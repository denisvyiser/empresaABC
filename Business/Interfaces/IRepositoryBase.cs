using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Utilitie<List<TEntity>> GetAll();
        Utilitie<TEntity> Get(int id);
        Utilitie<TEntity> Insert(TEntity obj);
        Utilitie<TEntity> Update(TEntity Entidade);
        Utilitie<TEntity> Delete(int id);
        Utilitie<IQueryable<TEntity>> GetBy(Expression<Func<TEntity, bool>> predicate);
        Utilitie<TEntity> UpdateList(List<TEntity> dados);
        Utilitie<TEntity> DeleteBy(Expression<Func<TEntity, bool>> predicate);

    }
}
