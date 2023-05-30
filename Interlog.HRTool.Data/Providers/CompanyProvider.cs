using Interlog.HRTool.Data.Models;

namespace Interlog.HRTool.Data.Providers
{
    public class CompanyProvider : IProvider<Company>
    {
        private Contexts.DatabaseContext _dbContext;

        public CompanyProvider(Contexts.DatabaseContext context)
        {
            _dbContext = context;
        }

        public List<Company> GetAll()
        {
            return _dbContext.Company.ToList();
        }

        public Company? GetById(int id)
        {
            return _dbContext.Company.FirstOrDefault(x => x.Id == id);
        }
        // ??admin??
        public Company? Create(Company entity)
        {
            _dbContext.Company.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        //?????
        public bool Delete(int id)
        {
            Models.Company? deleteCompany = _dbContext.Company.FirstOrDefault(x => x.Id == id);
            if (deleteCompany != null)
            {
                _dbContext.Company.Remove(deleteCompany);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public Company? Update(Company entity)
        {
            Models.Company? updateCompany = _dbContext.Company.FirstOrDefault(x => x.Id == entity.Id);
            if (updateCompany != null)
            {
                updateCompany.Name = entity.Name;
                _dbContext.SaveChanges();
            }

            return updateCompany;
        }
    }
}
