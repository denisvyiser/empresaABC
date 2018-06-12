using Business;
using Business.Repositories;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresaABC.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/")]
    public class GenericController<C,T> : Controller where T : class
    {
        private RepositoryBase<DataContext,T> business;

        public GenericController(RepositoryBase<DataContext,T> business)
        {
            this.business = business;
        }

        [HttpGet("GetAll")]
        public Utilitie<List<T>> GetMultas() => this.business.GetAll();

        [HttpGet("id")]
        public Utilitie<T> Get([FromQuery] int id) => this.business.Get(id);

        [HttpPost("{id}")]
        public Utilitie<T> Post(int id, [FromBody] T Entidade) => this.business.Insert(Entidade);

        [HttpPut("{id}")]
        public Utilitie<T> Put(int id, [FromBody] T Entidade) => this.business.Update(Entidade);

        [HttpDelete("{id}")]
        public Utilitie<T> Delete(int id) => this.business.Delete(id);
    }
}
