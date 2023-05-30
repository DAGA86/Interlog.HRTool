using Interlog.HRTool.Data.Models;

namespace Interlog.HRTool.Data.Providers
{
    public class EmployeeProvider : IProvider<Employee>
    {
        private Contexts.DatabaseContext _dbContext;

        public EmployeeProvider(Contexts.DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Employee> GetAll()
        {
            return _dbContext.Employee.ToList();
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employee.FirstOrDefault(x => x.Id == id);
        }

        public Employee? Create(Employee entity)
        {
            _dbContext.Employee.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            Models.Employee deleteEmployee = _dbContext.Employee.FirstOrDefault(x => x.Id == id);
            if (deleteEmployee != null)
            {
                _dbContext.Employee.Remove(deleteEmployee);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Employee? Update(Employee entity)
        {
            Models.Employee? updateEmployee = _dbContext.Employee.FirstOrDefault(x => x.Id == entity.Id);

            if (updateEmployee != null)
            {
                updateEmployee.FirstName = entity.FirstName; 
                updateEmployee.LastName = entity.LastName;
                updateEmployee.UserName = entity.UserName;
                updateEmployee.Password = entity.Password;
                // updateEmployee.Department = entity.Department; // updateEmployee.DepartmentId = entity.DepartmentId;

                _dbContext.SaveChanges();
            }

            return updateEmployee;

        }
    }
}