using SENAI.FalaAiCidadao.Dominio.Servicos;
using SENAI.FalaAiCidadao.Modelos.Models;
using SENAI.FalaAiCidadao.UI.Site.Validacoes;
using SENAI.FalaAiCidadao.UI.Site.ViewModels;
using SENAI.FalaAiCidadao.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SENAI.FalaAiCidadao.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            EleitorServico eleitorServico = new EleitorServico();
            PoliticoServico politicoServico = new PoliticoServico();

            if (login.Email.Substring(0, 1) == "_")
            {
                Politico politico = politicoServico.Login(login.Email, Criptografia.GetMD5Hash(login.Senha));

                if (politico != null)
                {
                    if (politico.Ativo == true) //verifico se a conta esta desativada
                    {
                        FormsAuthentication.SetAuthCookie(politico.Email, false);
                        var authTicket = new FormsAuthenticationTicket(1, politico.Email,
                        DateTime.Now, DateTime.MaxValue, false, politico.Permissao);
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);
                        Session.Add("SessionPolitico", politico);
                        return RedirectToAction("PerfilPolitico", "Politico");
                    }
                    else
                    {
                        ViewBag.Erro = "Politico desativado. Entre em contado com o administrador para saber mais.";
                        return View(login);
                    }
                }
            }
            else
            {
                Eleitor eleitor = eleitorServico.Login(login.Email, login.Senha);

                if (eleitor != null)
                {
                    if (!eleitor.Ativo) //verifico se a conta esta desativada
                    {
                        eleitor.Ativo = true; // a ativo
                        eleitorServico.Edit(eleitor); // e salvo no banco
                    }
                    if(eleitor.Excluido == true)
                    {
                        ViewBag.Erro = "Eleitor excluido. Entre em contado com o administrador para saber mais.";
                        return View(login);
                    }

                    FormsAuthentication.SetAuthCookie(eleitor.Email, false);
                    var authTicket = new FormsAuthenticationTicket(1, eleitor.Email,
                    DateTime.Now, DateTime.MaxValue, false, eleitor.Permissao);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    Session.Add("SessionEleitor", eleitor);
                    return RedirectToAction("PerfilEleitor", "Eleitor");
                }
            }

            ViewBag.Erro = "E-mail e/ou senha inválidos.";
            return View(login);

        }
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        
        public ActionResult _LoginRoot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _LoginRoot(LoginViewModel login)
        {
            AdminServico adminServico = new AdminServico();
            Admin adm = adminServico.Login(login.Email, login.Senha);
            if(adm != null)
            {
                FormsAuthentication.SetAuthCookie(adm.Email, false);
                var authTicket = new FormsAuthenticationTicket(1, adm.Email,
                DateTime.Now, DateTime.MaxValue, false, adm.Permissao);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                Session.Add("SessionAdmin", adm);
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Erro = "E-mail e/ou senha inválidos.";
            return View(login);
        }
    }
}