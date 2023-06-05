﻿using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Interlog.HRTool.Data.Providers
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetLanguages();
        Language? GetLanguageByCulture(string culture);
    }

    public class LanguageProvider : ILanguageService
    {
        private Contexts.DatabaseContext _dbContext;

        public LanguageProvider(Contexts.DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _dbContext.Languages.ToList();
        }

        public Language? GetLanguageByCulture(string culture)
        {
            return _dbContext.Languages.FirstOrDefault(x =>
                x.Culture.Trim().ToLower() == culture.Trim().ToLower());
        }
    }
}
