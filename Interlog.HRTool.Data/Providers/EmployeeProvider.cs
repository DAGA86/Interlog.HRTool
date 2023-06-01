using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Interlog.HRTool.Data.Providers
{
    public class EmployeeProvider 
    {
        private Contexts.DatabaseContext _dbContext;

        public EmployeeProvider(Contexts.DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employees
                .Include(x => x.Profiles)
                .FirstOrDefault(x => x.Id == id);
        }

        public Employee? GetByUsername(string username)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Username == username);
        }

        public Employee? Create(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Employee? Update(Employee entity)
        {
            Models.Employee? updateEmployee = _dbContext.Employees.FirstOrDefault(x => x.Id == entity.Id);

            if (updateEmployee != null)
            {
                updateEmployee.FirstName = entity.FirstName;
                updateEmployee.LastName = entity.LastName;
                updateEmployee.Email = entity.Email;
                updateEmployee.DepartmentId = entity.DepartmentId;

                _dbContext.SaveChanges();
            }

            return updateEmployee;

        }

        public Employee? UpdateProfiles(int id, int[] profileIds)
        {
            Models.Employee? updateEmployee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (updateEmployee != null)
            {
                _dbContext.EmployeeProfiles.RemoveRange(_dbContext.EmployeeProfiles.Where(x => x.EmployeeId == id));

                foreach (int profileId in profileIds) 
                {
                    _dbContext.EmployeeProfiles.Add(new EmployeeProfile()
                    {
                        EmployeeId = id, ProfileId = profileId
                    });
                }

                _dbContext.SaveChanges();
            }

            return updateEmployee;
        }

        public bool EmployeeExists(int id)
        {
            return (_dbContext.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}