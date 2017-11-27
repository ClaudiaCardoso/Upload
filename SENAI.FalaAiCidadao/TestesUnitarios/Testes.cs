using Microsoft.VisualStudio.TestTools.UnitTesting;
using SENAI.FalaAiCidadao.Dominio.Servicos;
using SENAI.FalaAiCidadao.Modelos.Models;

namespace TestesUnitarios
{
    [TestClass]
    public class Testes
    {
        [TestMethod]
        public void TestMethodLoginCorreto()
        {
            EleitorServico eleitorServico = new EleitorServico();
            string email = "claudia@gmail.com";//tem de existir esse usuario no banco
            string senha = "123456";
            Eleitor eleitor = eleitorServico.Login(email, senha);
            Assert.IsTrue(eleitor != null);
        }
        [TestMethod]
        public void TestMethodLoginIncorreto()
        {
            EleitorServico eleitorServico = new EleitorServico();
            string email = "claudia@gmail.com";
            string senha = "000000";
            Eleitor eleitor = eleitorServico.Login(email, senha);
            Assert.IsFalse(eleitor != null);
        }
    }
}
