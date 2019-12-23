using System.Collections.Generic;
using ApplicationTest.Models;
using ApplicationTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ApplicationTest.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            this._clienteService = clienteService;
        }

        public ActionResult ListarClientes()
        {
            IEnumerable<Cliente> ListaClientes = _clienteService.GetClientes();

            return View(ListaClientes);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Cliente Cliente = _clienteService.GetCliente(id);

            return View("Edit", Cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            _clienteService.Edit(cliente);
            return RedirectToAction("ListarClientes");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Cliente cliente)
        {
            _clienteService.Add(cliente);

            return RedirectToAction("ListarClientes");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _clienteService.Delete(id);
            return RedirectToAction("ListarClientes");
        }
    }
}