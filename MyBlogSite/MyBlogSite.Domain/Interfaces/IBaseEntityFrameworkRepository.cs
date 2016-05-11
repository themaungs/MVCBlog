namespace MyBlogSite.Domain.Interfaces
{
    public interface IBaseEntityFrameworkRepository<TEntity>
        where TEntity : class
    {
        TEntity Get(object id);
        void Add(TEntity entity);
        void Remove(object id);
        void Remove(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        int SaveChanges();
    }
}