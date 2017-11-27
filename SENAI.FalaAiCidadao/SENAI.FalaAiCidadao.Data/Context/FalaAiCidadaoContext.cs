using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Data.Context
{
    public class FalaAiCidadaoContext : DbContext
    {
        public FalaAiCidadaoContext()
            :base ("DefaultConnection")
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Eleitor> Eleitores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ImagemPost> ImagensPost { get; set; }
        public DbSet<Politico> Politicos { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        public override int SaveChanges()
        {
            //copiado da aula
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //para corrigir o erro: "may cause cycles or multiple cascade paths" (update-database)
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }
    }
}
