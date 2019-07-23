using System;
using System.IO;
using System.Threading.Tasks;
using Examples.Assessment.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Examples.Assessment.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Member Variables
        private readonly IDataStore _dataStore;

        private readonly string _uploadsPath;
        #endregion

        #region Constructor
        public HomeController(
            IHostingEnvironment host, 
            IDataStore dataStore)
        {
            _dataStore = dataStore;
            _uploadsPath = Path.Combine(host.WebRootPath, "uploads");
        }
        #endregion

        public async Task<IActionResult> Index(string sort)
        {
            if(!Enum.TryParse(typeof(SortMethod), sort, out Object sortMethod))
                sortMethod = SortMethod.Undefined;

            return View(
                await
                    _dataStore
                        .GetDataAsync(
                            _uploadsPath,
                            (SortMethod)sortMethod));
        }
    }
}
