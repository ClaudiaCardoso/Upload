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
using SENAI.FalaAiCidadao.Util;
using SENAI.FalaAiCidadao.Dominio.Servicos;
using System.Web.Security;
using SENAI.FalaAiCidadao.UI.Site.Validacoes;

namespace SENAI.FalaAiCidadao.UI.Site.Controllers
{
    public class EleitorController : Controller
    {
        EleitorServico eleitorServico = new EleitorServico();

        // GET: Eleitors
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View(eleitorServico.GetAll());
        }

        [CustomAuthorize(Roles = "ELEITOR")]
        public ActionResult PerfilEleitor()
        {
            Eleitor eleitorSession = new Eleitor();
            eleitorSession = (Eleitor)Session["SessionEleitor"]; //Pego o eleitor Logado e retorno na view 
            Eleitor eleitor = eleitorServico.FindById(eleitorSession.EleitorId);//Para atualizar os dados
            eleitor.Postagens = eleitor.Postagens.Where(p => p.Excluido == false).ToList();//adiciono apenas as postagens que não estao excluidas
            return View(eleitor);
        }

        // GET: Eleitors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eleitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadastroEleitorViewModel model)
        {    //Fiz assim pq a variavel <httppostedfilebase foto> na model estava vindo nula
            if (Request.Files.Count == 0) // verifico se o usuario upou uma foto
            {
                ModelState.AddModelError("Foto", "Selecione uma foto");//adiciono um erro a model
            }
            if (eleitorServico.VericicarCPFCadastrado(model.Cep)) //verifico se o cpf ja foi cadastrado
            {
                ModelState.AddModelError("Cep", "CPF já cadastrado");
            }
            if (eleitorServico.VerificarEmailExiste(model.Email)) //verifico se o cpf ja foi cadastrado
            {
                ModelState.AddModelError("Email", "Email já cadastrado");
            }
            if(!eleitorServico.ValidarCPF(model.CPF))
            {
                ModelState.AddModelError("CPF", "CPF inválido.");
            }

            if (ModelState.IsValid)
            {
                Eleitor eleitor = new Eleitor();
                eleitor.Nome = model.Nome;
                eleitor.Sobrenome = model.Sobrenome;
                eleitor.Email = model.Email;
                eleitor.Senha = Criptografia.GetMD5Hash(model.Senha);
                eleitor.TituloEleitor = model.TituloEleitor;
                eleitor.CPF = model.CPF;
                eleitor.DataNascimento = model.DataNascimento;
                eleitor.DataCadastro = DateTime.Now;

                if (Request.Files[0] != null)
                {
                    model.Foto = Request.Files[0]; // pego a foto q foi upada
                    string nomeFoto = Guid.NewGuid().ToString() + model.Foto.FileName.Substring(model.Foto.FileName.IndexOf("."));
                    string path = HttpContext.Server.MapPath("~/Imagens/Eleitor/");
                    model.Foto.SaveAs(path + nomeFoto);
                    eleitor.Foto = nomeFoto;
                }
                else
                {
                    eleitor.Foto = "sem-imagem.jpeg";
                }
                eleitorServico.Add(eleitor);

                Endereco endereco = new Endereco();
                endereco = ValidarCep.buscarEndereco(model.Cep);
                endereco.Numero = model.Numero;
                endereco.Complemento = model.Complemento;
                endereco.EleitorId = eleitor.EleitorId;
                EnderecoServico enderecoServico = new EnderecoServico();
                enderecoServico.Add(endereco);
                return RedirectToAction("Login", "Home");
            }
            return View(model);

        }
        [CustomAuthorize(Roles = "ELEITOR")]
        // GET: Eleitors/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleitor eleitor = eleitorServico.FindById(id);

            if (eleitor != null)
            {
                EleitorViewModel model = new EleitorViewModel();
                model.EleitorId = eleitor.EleitorId;//de eleitor pra model
                model.Nome = eleitor.Nome;
                model.Sobrenome = eleitor.Sobrenome;
                model.TituloEleitor = eleitor.TituloEleitor;
                model.CPF = eleitor.CPF;
                model.DataNascimento = eleitor.DataNascimento;
                model.Email = eleitor.Email;
                model.Cep = eleitor.Endereco.FirstOrDefault().Cep;
                model.Numero = eleitor.Endereco.FirstOrDefault().Numero;
                model.Complemento = eleitor.Endereco.FirstOrDefault().Complemento;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Eleitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthorize(Roles = "ELEITOR")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EleitorViewModel model)
        {
            Eleitor eleitor = eleitorServico.FindById(model.EleitorId);//trago do banco os dados do eleitor
            if (model.SenhaAntiga != null) //vejo se a senha antiga foi digitada
            {
                if (eleitorServico.VerificarSenha(model.EleitorId, Criptografia.GetMD5Hash(model.SenhaAntiga)))//testo se está correta
                {
                    ModelState.AddModelError("SenhaAntiga", "Senha incorreta."); //se estiver errada retorna o erro na tela
                }
                else // se estiver certa insere a nova senha no eleitor
                {
                    eleitor.Senha = Criptografia.GetMD5Hash(model.SenhaNova);
                }
            }

            if (ModelState.IsValid)
            {
                //vou atualizando os dados
                eleitor.Nome = model.Nome;
                eleitor.Sobrenome = model.Sobrenome;
                eleitor.Email = model.Email;
                eleitor.TituloEleitor = model.TituloEleitor;
                eleitor.CPF = model.CPF;
                eleitor.DataNascimento = model.DataNascimento;
                if (Request.Files[0].FileName != "")//verifico se o file name eh diferente da url(por default eh a url mesmo sem upar foto)
                {
                    model.Foto = Request.Files[0];//pego a foto que foi upada
                    string path = HttpContext.Server.MapPath("~/Imagens/Eleitor/");
                    model.Foto.SaveAs(path + eleitor.Foto);//salvo ela em cima da outra
                }
                eleitorServico.Edit(eleitor);
                EnderecoServico endServico = new EnderecoServico();//instancio o servico
                //busco o endereco pelo id do eleitor
                Endereco endereco = endServico.GetAll().Where(m => m.EleitorId == eleitor.EleitorId).FirstOrDefault();
                Eleitor eleitorAtualizado = eleitorServico.FindById(eleitor.EleitorId);
                //Atualizo a sessao no navegador
                Session["SessionEleitor"] = eleitorAtualizado;
                return RedirectToAction("PerfilEleitor", "Eleitor");
            }
            return View(model);
        }
        // POST: Eleitors/Delete/5
        [CustomAuthorize(Roles = "ELEITOR")]
        public ActionResult DesativarEleitor(Guid id)
        {
            Eleitor eleitor = eleitorServico.FindById(id); //trago o eleitor logado do banco
            eleitor.Ativo = false; //mudo o status dele pra desativado
            eleitorServico.Edit(eleitor); //salvo no banco
            FormsAuthentication.SignOut(); //deslogo ele da session
            TempData["Info"] = "Conta desativada, será reativada assim que logar novamente.";
            return RedirectToAction("Login", "Home"); //retono pro login com a mesagem de conta desativada
        }

        //====ADMIN

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult DesativarEleitorAdmin(Guid id)
        {
            Eleitor eleitor = eleitorServico.FindById(id); //trago o eleitor do banco
            eleitor.Ativo = false; //mudo o status dele pra desativado
            eleitorServico.Edit(eleitor); //salvo no banco
            return RedirectToAction("Index", "Admin");
        }

       // [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Delete(Guid id)
        {
            Eleitor eleitor = eleitorServico.FindById(id); //trago o eleitor do banco
            eleitor.Excluido = true;
            eleitorServico.Edit(eleitor); //salvo no banco
            return RedirectToAction("Index", "Eleitor");
        }

    }
}
