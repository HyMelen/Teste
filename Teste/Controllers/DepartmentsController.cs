using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teste.Models; //Add

namespace Teste.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
            /* Foi criado uma lista da classe Department*/
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Electronics" });
            list.Add(new Department { Id = 2, Name = "Clothes" });

            return View(list);
        }
    }
}