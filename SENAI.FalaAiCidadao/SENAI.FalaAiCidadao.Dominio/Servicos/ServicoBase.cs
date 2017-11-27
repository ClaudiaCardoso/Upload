using SENAI.FalaAiCidadao.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Dominio.Servicos
{
    public class ServicoBase<TEntity> where TEntity : class
    {
        private readonly RepositorioBase<TEntity> repositorioBase = new RepositorioBase<TEntity>();

        public void Add(TEntity model)
        {
            repositorioBase.Add(model);
        }

        public void Remove(TEntity model)
        {
            repositorioBase.Remove(model);
        }

        public void Edit(TEntity model)
        {
            repositorioBase.Edit(model);
        }

        public TEntity FindById(Guid id)
        {
            return repositorioBase.FindById(id);
        }

        public List<TEntity> GetAll()
        {
            return repositorioBase.GetAll();
        }
    }
}
