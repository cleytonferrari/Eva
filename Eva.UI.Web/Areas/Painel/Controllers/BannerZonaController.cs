using Eva.Aplicacao;
using Eva.Dominio;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class BannerZonaController : Controller
    {
       private readonly BannerZonaAplicacao zonaApp;

       public BannerZonaController()
        {
            zonaApp = Fabrica.BannerZonaAplicacaoMongo();
        }
        public ActionResult Index(int? page)
        {

            var numeroDaPagina = page ?? 1;
            var listaPaginada = zonaApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new BannerZonaViewModel());

            var zona = zonaApp.ListarPorId(id);
            if (zona == null)
            {
                this.Flash("Zona não encontrada!", FlashEnum.Error);
                return View(new BannerZonaViewModel());
            }

            var zonaViewModel = new BannerZonaViewModel()
            {
                Id = zona.Id,
                Nome = zona.Nome
            };

            return View(zonaViewModel);
        }

        [HttpPost]
        public ActionResult Editar(BannerZonaViewModel zona)
        {
            if (!ModelState.IsValid) 
                return View(zona);

            var zonaSalvar = new BannerZona
            {
                Id = zona.Id,
                Nome = zona.Nome
            };

            zonaApp.Salvar(zonaSalvar);
            this.Flash("Zona Salva com Sucesso!");
            return RedirectToAction("Index");
        }

        public JsonResult Excluir(string id)
        {
            Fabrica.BannerZonaAplicacaoMongo().Excluir(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
	}

    public class BannerZonaViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }

    }
}