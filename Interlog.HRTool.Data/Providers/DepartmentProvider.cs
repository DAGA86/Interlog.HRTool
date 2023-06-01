using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Interlog.HRTool.Data.Providers
{
    public class DepartmentProvider : IProvider<Department>
    {
        private Contexts.DatabaseContext _dbContext;

        public DepartmentProvider(Contexts.DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Department> GetAll()
        {
            return _dbContext.Departments.Include(x => x.Company).ToList();
        }

        public Department? GetById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(x => x.Id == id);
        }

        public Department? Create(Department entity)
        {
            _dbContext.Departments.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            Models.Department deleteDepartment = _dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (deleteDepartment != null)
            {
                _dbContext.Departments.Remove(deleteDepartment);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Department? Update(Department entity)
        {
            Models.Department? updateDepartment = _dbContext.Departments.FirstOrDefault(x => x.Id == entity.Id);

            if (updateDepartment != null)
            {
                updateDepartment.Name = entity.Name;  
                updateDepartment.CompanyId = entity.CompanyId;
               
                _dbContext.SaveChanges();
            }

            return updateDepartment;

        }
        public bool DepartmentExists(int id)
        {
            return (_dbContext.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
