using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Business.Repositories;
using Data;
using CrossCutting.EmailService;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using CrossCutting;

namespace EmpresaABC.Controllers
{
    public class ClienteController : GenericController<DataContext, Clientes>
    {
        RClientes business;

        private readonly IEmail _emailService;

        IHostingEnvironment _env;
        public ClienteController(RClientes business, IHostingEnvironment env, IEmail emailService) : base(business)
        {
            _env = env;
            _emailService = emailService;
        }

        [HttpGet("GerarCodigo")]
        public Utilitie<Clientes> GetCodigo([FromQuery] int id) => this.business.GerarCodigo(id);

        [HttpPut("UpdateBy/{id}")]
        public Utilitie<Clientes> Put(int id, [FromBody] List<Clientes> clientes) => this.business.UpdateList(clientes);

        [HttpPost]
        [Route("account/send-email")]
        public async Task<IActionResult> SendEmailAsync([FromBody]string email, string subject, string message)
        {
            await _emailService.SendEmail(email, subject, message);
            return Ok();
        }

        public Utilitie<Clientes> OnPostImport()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _env.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
//            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();

                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);

                    return new InteropExcel().ClientReadData(stream);



                }
            }
            else
            {
                return new Utilitie<Clientes>("Nenhum dados Recebido", false);
            }
        }
    }
}
