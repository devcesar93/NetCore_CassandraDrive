using System.Collections.Generic;
using ApplicationTest.Models;
using System;
using System.Linq;
using Cassandra;

namespace ApplicationTest.Services
{
    public class ClienteService : IClienteService
    {
        List<Cliente> ListaClientes = new List<Cliente>();

        //conexao com a instancia
        Cluster cluster = Cluster.Builder().AddContactPoints("localhost").Build();
        // acessar keyspace
        ISession session;
        public ClienteService()
        {
            session = cluster.Connect("contatos");
            // query
            var rs = session.Execute("SELECT * FROM cliente");
            //iteração
            foreach (var row in rs)
            {
                Cliente c = new Cliente();
                c.Id = row.GetValue<Guid>("id").ToString();
                c.Nome = row.GetValue<string>("nome");
                c.DataNascimento = Convert.ToDateTime(row.GetValue<LocalDate>("datanascimento").ToString());
                c.Email = row.GetValue<string>("email");
                c.CPF = row.GetValue<string>("cpf");
                ListaClientes.Add(c);
            }
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return ListaClientes;
        }

        public Cliente GetCliente(string id)
        {
            Cliente c = ListaClientes.FirstOrDefault(x => x.Id == id);

            return c;
        }

        public void Edit(Cliente p_cliente)
        {
            Guid id = Guid.Parse(p_cliente.Id);
            LocalDate date = new LocalDate(p_cliente.DataNascimento.Year, p_cliente.DataNascimento.Month, p_cliente.DataNascimento.Day);

            var ps = session.Prepare("UPDATE cliente SET nome=?, email=?, cpf=?, datanascimento=? WHERE id=?");

            var statement = ps.Bind(p_cliente.Nome, p_cliente.Email, p_cliente.CPF, date, id);
            session.Execute(statement);
        }

        public void Add(Cliente p_cliente)
        {
            Guid id = Guid.NewGuid();
            LocalDate date = new LocalDate(p_cliente.DataNascimento.Year, p_cliente.DataNascimento.Month, p_cliente.DataNascimento.Day);
            var userTrackStmt = session.Prepare(@"INSERT INTO cliente (id, nome, email, cpf, datanascimento) 
                                                VALUES (?, ?, ?, ?, ?)");

            var batch = new BatchStatement()
              .Add(userTrackStmt.Bind(id, p_cliente.Nome, p_cliente.Email, p_cliente.CPF, date));
            session.Execute(batch);
        }

        public void Delete(string id)
        {
             Guid idCliente = Guid.Parse(id);

            var ps = session.Prepare("DELETE From Cliente WHERE  id= ?");

            var statement = ps.Bind(idCliente);
            session.Execute(statement);
        }
    }
}