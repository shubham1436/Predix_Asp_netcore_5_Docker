using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCorePostgres.Repositories;
using AspnetCorePostgres.Models;
using Microsoft.Extensions.Configuration;
using AspnetCorePostgres.Postgres.POCO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCorePostgres.Controllers
{
    public class PartFamilyController : Controller
    {
        private readonly PartFamilyRepository partFamilyRepository;

        public PartFamilyController(PartFamilyContext context)//IConfiguration configuration)
        {
            partFamilyRepository = new PartFamilyRepository(context);
        }


        public IActionResult Index()
        {
            //return new JsonResult("Hello" + partFamilyRepository.FindAll().ToString());
            return View(partFamilyRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(PartFamily partFamily)
        {
            if (ModelState.IsValid)
            {
                partFamilyRepository.Add(partFamily);
                return RedirectToAction("Index");
            }
            return View(partFamily);

        }

        // GET: /Customer/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PartFamily obj = partFamilyRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(PartFamily obj)
        {

            if (ModelState.IsValid)
            {
                partFamilyRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Customer/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            partFamilyRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}

