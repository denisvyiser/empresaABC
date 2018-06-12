using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class Utilitie<T> where T : class
    {
        ILogger log;
        public string Mensagem { get; set; }
        public bool Status { get; set; }
        //public T Dados { get; set; }
        public T Dados { get; set; }

        public Utilitie()
        {
        }

        public Utilitie(ILoggerFactory loggerFactory)
        {
            this.log = loggerFactory.CreateLogger(this.GetType().Name);
        }

        public Utilitie(string mensagem, bool status = true)
        {
            this.Mensagem = mensagem;
            this.Status = status;
            this.Dados = null;

            this.log.LogInformation(mensagem);
        }

        public Utilitie(T dados, string mensagem = null)
        {
            //string teste = JsonConvert.SerializeObject(dados);

            this.Dados = dados;
            this.Mensagem = mensagem;
            this.Status = true;

            this.log.LogInformation(mensagem);
        }

        
    }
}