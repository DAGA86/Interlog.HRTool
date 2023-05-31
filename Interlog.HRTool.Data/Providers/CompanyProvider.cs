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
            return _dbContext.Companies.ToList();
        }

        public Company? GetById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(x => x.Id == id);
        }
        // ??admin??
        public Company? Create(Company entity)
        {
            _dbContext.Companies.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        //?????
        public bool Delete(int id)
        {
            Models.Company? deleteCompany = _dbContext.Companies.FirstOrDefault(x => x.Id == id);
            if (deleteCompany != null)
            {
                _dbContext.Companies.Remove(deleteCompany);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public Company? Update(Company entity)
        {
            Models.Company? updateCompany = _dbContext.Companies.FirstOrDefault(x => x.Id == entity.Id);
            if (updateCompany != null)
            {
                updateCompany.Name = entity.Name;
                _dbContext.SaveChanges();
            }

            return updateCompany;
        }
    }
}
