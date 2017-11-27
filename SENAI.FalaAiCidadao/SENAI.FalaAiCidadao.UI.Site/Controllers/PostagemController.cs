using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SENAI.FalaAiCidadao.Modelos.Models;
using SENAI.FalaAiCidadao.Dominio.Servicos;
using SENAI.FalaAiCidadao.UI.Site.ViewModels;
using SENAI.FalaAiCidadao.UI.Site.Validacoes;

namespace SENAI.FalaAiCidadao.UI.Site.Controllers
{
    public class PostagemController : Controller
    {
        EleitorServico eleitorServico = new EleitorServico();
        PostagemServico postagemServico = new PostagemServico();
        RegiaoServico regiaoServico = new RegiaoServico();
        TipoServico tiposServico = new TipoServico();
        ImagemPostServico imagemPostServico = new ImagemPostServico();
        ComentarioServico comentarioServico = new ComentarioServico();

        // GET: Postagem
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            //var postagems = db.Postagems.Include(p => p.Eleitor).Include(p => p.Regiao).Include(p => p.Tipo);
            var postagens = postagemServico.GetAll();

            //return View(postagems.ToList());
            return View(postagens);
        }

        public ActionResult TimeLinePostagens()
        {
            List<Postagem> postagens = postagemServico.GetAll().Where(p => p.Excluido == false).ToList();//pego todas as postagens
            return View(postagens.OrderByDescending(p => p.Data));//mostro por ordem de data
        }

        [CustomAuthorize(Roles = "ELEITOR")]
        public ActionResult Details(Guid id)
        {
            Postagem post = postagemServico.FindById(id);
            Eleitor eleitor = (Eleitor)Session["SessionEleitor"];
            if (eleitor.EleitorId == post.EleitorId)// verifico se foi o eleitor q esta logado q criou esta postagem
                ViewBag.Eleitor = true;//se sim viewbag recebe true
            else
                ViewBag.Eleitor = false;

            return View(post);
        }
        //[CustomAuthorize(Roles ="POLITICO")]
        public ActionResult TimeLinePostagensPolitico()
        {
            List<Postagem> postagens = postagemServico.GetAll().Where(p => p.Excluido == false).ToList();//pego todas as postagens que nao esteja marcadas como excluido
            return View(postagens.OrderByDescending(p => p.Data));//mostro por ordem de data
        }

        //[CustomAuthorize(Roles = "ELEITOR")]
        public ActionResult DeletePost(Guid id)
        {
            Postagem postagem = postagemServico.FindById(id);
            postagem.Excluido = true;
            postagemServico.Edit(postagem);
            return RedirectToAction("TimeLinePostagens", "Postagem");
        }

        //[CustomAuthorize(Roles ="POLITICO")]
        public ActionResult DetailsPolitico(Guid id)
        {
            Postagem post = postagemServico.FindById(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult SalvarNota(Postagem model)
        {
            Postagem post = postagemServico.FindById(model.PostagemId); //traz a postagem do banco
            Guid comentId = post.Comentaios.FirstOrDefault().ComentarioId;//pego o id do comentario do politico
            Comentario coment = comentarioServico.FindById(comentId);//trago esse comentario do banco
            coment.NumeroAvaliacao = model.NumAvaliacao;//Pego o numero da avaliaçao do eleitor q veio da model
            comentarioServico.Edit(coment);//salvo

            if (Session["SessionEleitor"] != null)
            {
                Eleitor eleitor = (Eleitor)Session["SessionEleitor"];
                if (eleitor.EleitorId == post.EleitorId)//mesma coisa da details
                    ViewBag.Eleitor = true;
                else
                    ViewBag.Eleitor = false;
            }
            return RedirectToAction("Details", "Postagem", new { id = post.PostagemId });
        }


        // GET: Postagem/Create
        public ActionResult Create()
        {
            PostagemViewModel post = new PostagemViewModel();
            post.Regioes = regiaoServico.GetAll().ToList();
            post.Tipos = tiposServico.GetAll().ToList();
            return View(post);
        }
        // POST: Postagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostagemViewModel model)
        {
            if (Request.Files.Count == 0)
            {
                ModelState.AddModelError("Fotos", "Faça o upload de pelo menos uma foto");
            }
            if (ModelState.IsValid)
            {
                Postagem postagem = new Postagem();

                Eleitor eleitor = new Eleitor();
                eleitor = (Eleitor)Session["SessionEleitor"];
                postagem.EleitorId = eleitor.EleitorId;

                postagem.Data = DateTime.Now;
                postagem.RegiaoId = model.RegiaoId;
                postagem.TipoId = model.TipoId;
                postagem.TextoPost = model.TextoPost;
                postagem.TituloPost = model.TituloPost;
                postagemServico.Add(postagem);

                HttpFileCollectionBase arquivos = Request.Files;//pego os arquivos updo e insiro na variavel
                for (int i = 0; i < arquivos.Count; i++)
                {
                    ImagemPost imagem = new ImagemPost();
                    imagem.NomeImagem = Guid.NewGuid().ToString() + arquivos[i].FileName.Substring(arquivos[i].FileName.IndexOf("."));
                    string path = HttpContext.Server.MapPath("~/Imagens/Postagem/");
                    arquivos[i].SaveAs(path + imagem.NomeImagem);
                    imagem.PostagemId = postagem.PostagemId;
                    imagemPostServico.Add(imagem);
                }
                return RedirectToAction("TimeLinePostagens", "Postagem");
            }
            model.Regioes = regiaoServico.GetAll().ToList();
            model.Tipos = tiposServico.GetAll().ToList();
            return View(model);

        }

        // GET: Postagem/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ExcluirPostagem(Guid id)
        {
            Postagem postagem = postagemServico.FindById(id);
            postagem.PostagemId = id;
            postagem.Excluido = true;
            postagemServico.Edit(postagem);
            return RedirectToAction("Index", "Postagem");
        }

        [CustomAuthorize(Roles = "POLITICO")]
        public ActionResult InserirComentario(Postagem model)
        {
            Postagem postagem = postagemServico.FindById(model.PostagemId);
            Comentario coment = new Comentario();
            Politico politico = (Politico)Session["SessionPolitico"];
            coment.PoliticoId = politico.PoliticoId;
            postagem.PostagemId = model.PostagemId;
            coment.TextoComentario = model.Comentario;
            coment.PostagemId = postagem.PostagemId;
            coment.Data = DateTime.Now;
            coment.Excluido = false;
            comentarioServico.Add(coment);

            return RedirectToAction("DetailsPolitico", "Postagem", new { id = postagem.PostagemId });
        }
    }
}
