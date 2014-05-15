﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Eva.Aplicacao;
using Eva.Dominio;

namespace Eva.UI.Web.Helpers
{
    public static class Imagem
    {
        public static string Upload(HttpPostedFileBase arquivo, string diretorio)
        {
            var nomeUnico = "";
            if (arquivo == null) return nomeUnico;

            var extensaoDoArquivo = Path.GetExtension(arquivo.FileName).Substring(1);

            nomeUnico = Guid.NewGuid() + "." + extensaoDoArquivo;

            var path = MontaPath(diretorio, nomeUnico);

            var imagem = new WebImage(arquivo.InputStream);
            imagem.Save(path);

            return nomeUnico;
        }

        /// <summary>
        /// Envia um arquivo dividido em várias partes
        /// </summary>
        /// <param name="arquivo">Arquivo a ser salvo</param>
        /// <param name="diretorio">Diretorio de destino</param>
        /// <param name="fileName">Nome do Arquivo</param>
        /// <param name="chunk">Numero da parte do arquivo</param>
        /// <param name="chunks">Numero de partes que o arquivo foi dividido</param>
        /// <returns>True se o arquivo é em uma unica parte, ou se é a ultima parte do arquivo enviado; Retorna false se alguma parte que não a final do arquivo</returns>
        public static bool Upload(HttpPostedFileBase arquivo, string diretorio, string fileName, int? chunk, int? chunks)
        {
            if (arquivo == null)
                return false;

            var path = MontaPath(diretorio, fileName);

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

        public static void CropFile(string name, string diretorio, List<ImagensLayout.Tamanho> tamanhos)
        {
            var path = MontaPath(diretorio, name);

            if (!File.Exists(path)) return;

            foreach (var item in tamanhos)
            {
                if (item.Nome == "Original")
                    continue;

                var imagem = new WebImage(path);
                var pathFotoCropada = MontaPath(diretorio, item.Altura + "x" + item.Largura + "_" + name);
                imagem.Resize(item.Largura, item.Altura);
                imagem.Save(pathFotoCropada);
            }
        }

        private static string MontaPath(string diretorio, string fileName)
        {
            var pathDiretorio = HttpContext.Current.Server.MapPath("~/Content/Uploads/" + diretorio);
            var dir = new DirectoryInfo(pathDiretorio);

            if (!dir.Exists)
                Directory.CreateDirectory(pathDiretorio);

            return Path.Combine(pathDiretorio, fileName);
        }

        public static void Logo(string name, string diretorio, List<PosicaoLogo> posicaoLogo)
        {
            if (posicaoLogo.Any())
            {
                var pathImagem = MontaPath(diretorio, name);
                var imagem = new WebImage(pathImagem);

                foreach (var pLogo in posicaoLogo)
                {
                    //Todo: Tirar o acesso ao bando daqui
                    var nomeLogo = Fabrica.LogoAplicacaoMongo().ListarPorId(pLogo.IdLogo).Imagem;

                    //Carrega o logo e redimenciona para 33% do tamanho da imagem
                    var logo = new WebImage(MontaPath("Logo", nomeLogo)).Resize(imagem.Width / 3, imagem.Height / 3);
                    imagem.AddImageWatermark(logo, logo.Width, logo.Height, pLogo.PosicaoHorizontal, pLogo.PosicaoVertical, 90, 0);
                }

                imagem.Save(pathImagem);

            }
        }

        public static void LimparMiniaturaCapa(string nome, string diretorio)
        {
            //Todo: Não testei a performance deste metodo

            var arquivos = Directory.GetFiles(MontaPath(diretorio, ""));

            foreach (var fi in arquivos.Select(arquivo => new FileInfo(arquivo)).Where(fi => fi.Name.Contains(nome) && fi.Name != nome))
            {
                fi.Delete();
            }
        }

        public static void ExcluirArquivo(string nome, string diretorio)
        {
            var arquivos = Directory.GetFiles(MontaPath(diretorio, ""));
            foreach (var fi in arquivos.Select(arquivo => new FileInfo(arquivo)).Where(fi => fi.Name.Contains(nome)))
            {
                fi.Delete();
            }
        }

        public static void ExcluirArquivo(List<Arquivo> arquivos, string diretorio)
        {
            foreach (var arquivo in arquivos)
                ExcluirArquivo(arquivo.Nome, diretorio);
        }

        public static List<Arquivo> OrdenarArquivos(IEnumerable<string> items, List<Arquivo> arquivos)
        {
            var arquivosRetorno = new List<Arquivo>();
            var i = 0;
            foreach (var item in items)
            {
                i++;
                var arquivo = arquivos.FirstOrDefault(x => x.Id == item);
                arquivo.Ordem = i;
                arquivosRetorno.Add(arquivo);
            }
            return arquivosRetorno;
        }


    }

    public class PosicaoLogo
    {
        public string PosicaoHorizontal { get; set; }//"Left", "Right", or "Center".
        public string PosicaoVertical { get; set; } //"Top", "Middle", or "Bottom"
        public string IdLogo { get; set; }
    }

    public struct ImagensLayout
    {
        public static List<Tamanho> Noticias = new List<Tamanho>()
        {
            new Tamanho() {Nome = "Original", Altura = 940, Largura = 529},
            new Tamanho() {Nome = "Foto01", Altura = 380, Largura = 214},
            new Tamanho() {Nome = "Foto02", Altura = 150, Largura = 85},
            new Tamanho() {Nome = "Foto03", Altura = 540, Largura = 304},
            new Tamanho() {Nome = "Foto04", Altura = 500, Largura = 281}
        };

        public static List<Tamanho> Eventos = new List<Tamanho>()
        {
            new Tamanho() {Nome = "Original", Altura = 940, Largura = 529},
            new Tamanho() {Nome = "Foto01", Altura = 150, Largura = 85},
        };

        public struct Tamanho
        {
            public string Nome { get; set; }
            public int Largura { get; set; }
            public int Altura { get; set; }
        }
    }
}