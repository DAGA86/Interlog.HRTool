
namespace Interlog.HRTool.Data.Providers
{
    internal interface IProvider<T>
    {
        public List<T> GetAll();
        public T? GetById(int id);
        public T? Create(T entity);
        public T? Update(T entity);
        public bool Delete(int id);
    }
}
