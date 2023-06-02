using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            return _dbContext.Companies.Include(x => x.Departments).ToList();
        }

        public Company? GetById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(x => x.Id == id);
        }
        
        public Company? Create(Company entity)
        {
            _dbContext.Companies.Add(entity);
            _dbContext.SaveChanges();
            return entity;
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

        public bool Delete(int id)
        {
            Models.Company? deleteCompany = _dbContext.Companies.Include(x => x.Departments).FirstOrDefault(x => x.Id == id);
            if (deleteCompany == null || deleteCompany.Departments.Any())
            {
                return false;
            }

            _dbContext.Companies.Remove(deleteCompany);
            _dbContext.SaveChanges();
            return true;
        }       

        public bool CompanyExists(int id)
        {
            return (_dbContext.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
