using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teste.Services;
using Teste.Models;
using Teste.Models.ViewModels;
using Teste.Services.Exceptions;
using System.Diagnostics;

namespace Teste.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            List<Seller> list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) // Coloca interrogação para indicar que é opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
                var obj = _sellerService.FindById(id.Value);       // Utiliza o value pq ele é um nullable, um valor opcional,

            if (obj == null)                                   //  pra pegar o valor dele, caso exista, tem que urilizqar o value
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)                    //executa
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)                   //retorna a view
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);       // Ele pega o departamento tambem  por causa do 
            if (obj == null)                                   //Include(obj => obj.Department)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)                     //retorna a view
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);       // Ele pega o departamento tambem  por causa do 
            if (obj == null)                                   //Include(obj => obj.Department)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]                                       ///executa
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) 
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error(string message)
        {
            var errorViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete do framework pra pegar o Id interno da requisição.
            };
            return View(errorViewModel);

        }
            
    }


}
     
