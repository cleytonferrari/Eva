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
            //Todo: criar diretorio se não existir
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
                //imagem.Resize(350, 350);
                //imagem.AddTextWatermark("Cleyton Ferrari");
                //imagem.AddImageWatermark("Content/Uploads/logo.png", 50, 50, "Right", "Bottom", 50, 2);
                //imagem.Crop(100, 100, 100, 100);
                //imagem.FlipHorizontal();
                imagem.Save(path);
            }

            return fileName;
        }

        /// <summary>
        /// Envia um arquivo dividido em várias partes
        /// </summary>
        /// <param name="arquivo"></param>
        /// <param name="diretorio"></param>
        /// <param name="fileName"></param>
        /// <param name="chunk"></param>
        /// <param name="chunks"></param>
        /// <returns></returns>
        public static bool Upload(HttpPostedFileBase arquivo, string diretorio, string fileName, int? chunk, int? chunks)
        {
            if (arquivo == null) 
                return false;

            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + diretorio), fileName);

            //Checa se é arquivo em partes
            chunk = chunk ?? 0;

            //checa se é um arquivo, ou uma parte de um arquivo
            using (var fs = new FileStream(path, chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[arquivo.InputStream.Length];
                arquivo.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            return chunk + 1 == chunks;
        }
    }
}