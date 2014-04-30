using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;

namespace Eva.UI.Web.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            ViewBag.usuarios =  Fabrica.PalestraAplicacaoMongo().ListarTodos().OrderBy(x => x.Nome).ToList();
            return View();
        }

        public string Add()
        {
            var usuario = new Usuario
            {
                Email = "cleyton@w7br.com",
                Nome = "Cleyton Ferrari",
                Senha = "171099"
            };
            Fabrica.PalestraAplicacaoMongo().Salvar(usuario);
            return "Salvo";
        }
	}
}