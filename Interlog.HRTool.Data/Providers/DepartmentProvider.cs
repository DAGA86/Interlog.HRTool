using Interlog.HRTool.Data.Models;

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
            return _dbContext.Departments.ToList();
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
               
                _dbContext.SaveChanges();
            }

            return updateDepartment;

        }
    }
}
