using MyBlogSite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Models.DBContext;

namespace MyBlogSite.Domain.Repositories
{
    public abstract class BaseEntityFrameworkRepository<TEntity> : IBaseEntityFrameworkRepository<TEntity>
           where TEntity : class
    {
        private readonly MyBlogSiteDatabaseContext context;

        protected BaseEntityFrameworkRepository(IUnitOfWork unitOfWork)
        {
            this.context = unitOfWork as MyBlogSiteDatabaseContext;
            if (this.context == null)
            {
                throw new ArgumentException(@"Parameter is not the expected DbContext type", "unitOfWork");
            }
        }

        protected MyBlogSiteDatabaseContext Context
        {
            get
            {
                return this.context;
            }
        }

        public virtual void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Get(object id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        public virtual void Remove(object id)
        {
            var entityToDelete = this.Context.Set<TEntity>().Find(id);
            this.Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            this.Attach(entityToDelete);
            this.Context.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Attach(TEntity entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
