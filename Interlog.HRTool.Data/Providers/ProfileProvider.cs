using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interlog.HRTool.Data.Providers
{
    public class ProfileProvider : IProvider<Profile>
    {
        private Contexts.DatabaseContext _dbContext;

        public ProfileProvider(Contexts.DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Profile> GetAll()
        {
            return _dbContext.Profile.ToList();
        }

        public Profile? GetById(int id)
        {
            return _dbContext.Profile.FirstOrDefault(x => x.Id == id);
        }

        public Profile? Create(Profile entity)
        {
            _dbContext.Profile.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            Models.Profile deleteProfile = _dbContext.Profile.FirstOrDefault(x => x.Id == id);
            if (deleteProfile != null)
            {
                _dbContext.Profile.Remove(deleteProfile);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Profile? Update(Profile entity)
        {
            Models.Profile? updateProfile = _dbContext.Profile.FirstOrDefault(x => x.Id == entity.Id);

            if (updateProfile != null)
            {
                updateProfile.Name = entity.Name; 
                _dbContext.SaveChanges();
            }

            return updateProfile;

        }
    }
}
