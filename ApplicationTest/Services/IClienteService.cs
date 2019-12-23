using System.Collections.Generic;
using ApplicationTest.Models;

namespace ApplicationTest.Services
{
    public interface IClienteService
    {
         IEnumerable<Cliente> GetClientes();

         Cliente GetCliente(string id);

         void Edit(Cliente cliente);

         void Add(Cliente cliente);

         void Delete(string id);
    }
}