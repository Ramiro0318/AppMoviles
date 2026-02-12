using RecetasAPI.Data;

namespace RecetasAPI.Repositories
{
    public class Repository<T> where T : class
    {
        public Repository(GourmetRecetasContext context)
        {
            Context = context;
        }

        public GourmetRecetasContext Context { get; }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T? Get(object id) 
        {
            return Context.Find<T>(id);
        }

        public IQueryable<T> Query() 
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}
