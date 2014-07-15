using Eva.Aplicacao;
using Eva.Dominio;
using PagedList;
using System.Web.Mvc;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class AnuncianteController : Controller
    {
       private readonly AnuncianteAplicacao anuncianteApp;

        public AnuncianteController()
        {
            anuncianteApp = Fabrica.AnuncianteAplicacaoMongo();
        }
        public ActionResult Index(int? page)
        {

            var numeroDaPagina = page ?? 1;
            var listaPaginada = anuncianteApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new AnuncianteViewModel());

            var anunciante = anuncianteApp.ListarPorId(id);
            if (anunciante == null)
            {
                this.Flash("Anunciante não encontrado!", FlashEnum.Error);
                return View(new AnuncianteViewModel());
            }

            var anuncianteViewModel = new AnuncianteViewModel()
            {
                Id = anunciante.Id,
                Nome = anunciante.Nome,
                Telefone = anunciante.Telefone,
                Email = anunciante.Email,
                Descricao = anunciante.Descricao
            };

            return View(anuncianteViewModel);
        }

        [HttpPost]
        public ActionResult Editar(AnuncianteViewModel anunciante)
        {
            if (!ModelState.IsValid) 
                return View(anunciante);

            var anuncianteSalvar = new Anunciante
            {
                Id = anunciante.Id,
                Nome = anunciante.Nome,
                Telefone = anunciante.Telefone,
                Email = anunciante.Email,
                Descricao = anunciante.Descricao
            };

            anuncianteApp.Salvar(anuncianteSalvar);
            this.Flash("Anunciante Salvo com Sucesso!");
            return RedirectToAction("Index");
        }

        public JsonResult Excluir(string id)
        {
            //Todo: nâo excluir anunciante que possua banners, ou, setar uma anunciante padrao para as banners ao excluir a anunciante dela
            Fabrica.AnuncianteAplicacaoMongo().Excluir(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
	}

    public class AnuncianteViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }

    }
}