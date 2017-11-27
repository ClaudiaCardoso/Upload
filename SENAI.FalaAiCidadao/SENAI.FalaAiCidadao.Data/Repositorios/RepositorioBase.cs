using SENAI.FalaAiCidadao.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Data.Repositorios
{
    public class RepositorioBase<TEntity> where TEntity : class
    {
        protected FalaAiCidadaoContext db = new FalaAiCidadaoContext();
        protected DbSet<TEntity> dbSet;

        public RepositorioBase()
        {
            dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity model)
        {
            dbSet.Add(model);
            db.SaveChanges();
        }

        public void Remove(TEntity model)
        {
            dbSet.Remove(model);
            db.SaveChanges();
        }

        public void Edit(TEntity model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public TEntity FindById(Guid id)
        {
            return dbSet.Find(id);
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
    }
}
