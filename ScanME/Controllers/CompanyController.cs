using Microsoft.AspNetCore.Mvc;
using ScanME.Contexts;
using ScanME.Models;
using ScanME.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {

        public readonly ApplicationUnitOfWork<Company> _unitOfWork;
        public ApplicationDbContext _context;
        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new ApplicationUnitOfWork<Company>(_context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var company = new
            {
                name = HttpContext.Items["UserId"]
            };
            return Json(company);
        }

        [HttpPost]
        public  int Store([FromForm] Company company)
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                company.UsersId = (int)HttpContext.Items["UserId"];
                company.Logo = dbPath;
                _unitOfWork.ModelRepository.Store(company);
                _unitOfWork.Save();

            }
            return 1;
            
        }
    }
}
