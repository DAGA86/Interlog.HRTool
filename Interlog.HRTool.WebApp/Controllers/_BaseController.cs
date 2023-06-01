using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Interlog.HRTool.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly DatabaseContext _context;

        public BaseController(DatabaseContext context)
        {
            _context = context;
        }
    }
}