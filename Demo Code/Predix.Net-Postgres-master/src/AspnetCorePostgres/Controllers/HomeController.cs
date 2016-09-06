using System;
using System.Linq;
using AspnetCorePostgres.Postgres;
using Microsoft.AspNetCore.Mvc;
using AspnetCorePostgres.Postgres.POCO;

namespace AspnetCorePostgres.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthorsContext _context;

        public HomeController(AuthorsContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            try
            {
                _context.Authors.Add(new Author()
                {
                    Id = id,
                    FirstName = "Eugene",
                    LastName = "Lahansky"
                });
                _context.SaveChanges();

                var added = _context.Authors.FirstOrDefault(x => x.Id == id);
                return new JsonResult(string.Format("Inserted author: {0} {1}", added.FirstName, added.LastName));
            }
            catch (Exception ex)
            {
                return new JsonResult(string.Format("Error: {0}\r\n {1}", ex.Message, ex.InnerException));
            }
        }
    }
}
