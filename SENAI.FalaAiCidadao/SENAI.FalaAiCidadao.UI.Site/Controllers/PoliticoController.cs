using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SENAI.FalaAiCidadao.Modelos.Models;
using SENAI.FalaAiCidadao.UI.Site.ViewModels;
using SENAI.FalaAiCidadao.Dominio.Servicos;
using SENAI.FalaAiCidadao.Util;

namespace SENAI.FalaAiCidadao.UI.Site.Controllers
{
    public class PoliticoController : Controller
    {
        PoliticoServico politicoServico = new PoliticoServico();

        [Authorize(Roles = "POLITICO")]
        public ActionResult PerfilPolitico()
        {
            Politico politico = (Politico)Session["SessionPolitico"];
            return View(politico);
        }

        // GET: Politico
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View(politicoServico.GetAll().ToList().OrderByDescending(m => m.DataCadastro));
        }

        // GET: Politico/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Politico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PoliticoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Politico politico = new Politico();
                    politico.DataCadastro = DateTime.Now;
                    politico.CPF = model.CPF;
                    politico.DataNascimento = model.DataNascimento;
                    politico.Email = "_" + model.Email;
                    politico.Senha = Criptografia.GetMD5Hash(model.Senha);
                    politico.Nome = model.Nome;
                    politico.Partido = model.Partido;
                    politico.Ativo = true;

                    model.Foto = Request.Files[0]; // pego a foto q foi upada
                    string nomeFoto = Guid.NewGuid().ToString() + model.Foto.FileName.Substring(model.Foto.FileName.IndexOf("."));
                    string path = HttpContext.Server.MapPath("~/Imagens/Politico/");
                    model.Foto.SaveAs(path + nomeFoto);
                    politico.Foto = nomeFoto;

                    politicoServico.Add(politico);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception erro)
            {
                return View(model);
            }
        }

        // GET: Politico/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politico politico = politicoServico.FindById(id);
            if (politico == null)
            {
                return HttpNotFound();
            }
            PoliticoViewModel model = new PoliticoViewModel()
            {
                Nome = politico.Nome,
                Email = politico.Email,
                CPF = politico.CPF,
                DataNascimento = politico.DataNascimento,
                Partido = politico.Partido,
                PoliticoId = politico.PoliticoId
            };
            return View(model);
        }

        // POST: Politico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PoliticoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Politico politico = politicoServico.FindById(model.PoliticoId);
                politico.PoliticoId = model.PoliticoId;
                politico.DataCadastro = DateTime.Now;
                politico.CPF = model.CPF;
                politico.DataNascimento = model.DataNascimento;
                politico.Email = model.Email;
                politico.Senha = Criptografia.GetMD5Hash(model.Senha);
                politico.Nome = model.Nome;
                politico.Partido = model.Partido;
                politico.Ativo = model.Ativo;

                model.Foto = Request.Files[0]; // pego a foto q foi upada
                string path = HttpContext.Server.MapPath("~/Imagens/Politico/");
                model.Foto.SaveAs(path + politico.Foto);

                politicoServico.Edit(politico);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "ADMIN")]
        public ActionResult DesativarPolitico(Guid id)
        {
            Politico politico = politicoServico.FindById(id);
            politico.PoliticoId = id;
            politico.Ativo = false;
            politicoServico.Edit(politico);
            return RedirectToAction("Index");
        }
    }
}
