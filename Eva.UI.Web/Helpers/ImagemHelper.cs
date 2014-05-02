using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Eva.UI.Web.Helpers
{
    public static class Imagem
    {
        public static string Upload(HttpPostedFileBase arquivo, string diretorio)
        {
            //Todo: implementar um retorno para os erros do upload
            var erros = new List<string>();
            var fileName = "";
            if (arquivo != null)
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                var fileExt = Path.GetExtension(arquivo.FileName).Substring(1);
                 fileName = Guid.NewGuid() + "." + fileExt;

                if (arquivo.ContentLength > ((1024 * 1024) * 4))
                {
                    erros.Add("O tamanho do arquivo não pode ser maior que 4Mb");
                }

                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    erros.Add("Tipo de arquivo invalido, use somente arquivos jpg, jpeg ou png");
                }

                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + diretorio), fileName);

                var imagem = new WebImage(arquivo.InputStream);
                imagem.Resize(350, 350);
                //imagem.AddTextWatermark("Cleyton Ferrari");
                //imagem.AddImageWatermark("Content/Uploads/logo.png", 50, 50, "Right", "Bottom", 50, 2);
                //imagem.Crop(100, 100, 100, 100);
                //imagem.FlipHorizontal();
                imagem.Save(path);
            }

            return fileName;
        }
    }
}