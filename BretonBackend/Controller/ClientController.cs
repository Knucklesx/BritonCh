using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BretonBackend.Data;
using BretonBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BretonBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        //CREATE => POST
        //READ => GET
        //UPDATE => PUT (Or PATCH => Partial Update)
        //DELETE => DELETE
        private readonly BretonContext _bretonContext;
        private readonly ILogger<ClientController> _logger;

        public ClientController(BretonContext bretonContext, ILogger<ClientController> logger)
        {
            _bretonContext = bretonContext;
            _logger = logger;
        }


        //GET api/client
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clientes = _bretonContext.Clientes.ToList();
            return Ok(clientes);
        }

        //GET api/client/{id}
        // [HttpGet("{id}")]
        // public ActionResult<Cliente> Get(int id)
        // {
        //     var cliente = _bretonContext.Clientes.FirstOrDefault(c => c.Id == id);
        //     if (cliente == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(cliente);
        // }
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            _logger.LogInformation($"Recebendo solicitação para obter cliente com ID: {id}");

            var cliente = _bretonContext.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID: {id} não encontrado.");
                return NotFound();
            }

            _logger.LogInformation($"Cliente com ID: {id} encontrado.");
            return Ok(cliente);
        }


        //POST api/client
        [HttpPost]
        public ActionResult Post([FromBody] Cliente cliente)
        {


            _bretonContext.Clientes.Add(cliente);
            _bretonContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        //PUT api/client/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var clienteExistente = _bretonContext.Clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Cpf = cliente.Cpf;
            clienteExistente.Telefone = cliente.Telefone;
            clienteExistente.Data_de_Nascimento = cliente.Data_de_Nascimento;
            clienteExistente.Cep = cliente.Cep;
            clienteExistente.Logradouro = cliente.Logradouro;
            clienteExistente.Numero = cliente.Numero;
            clienteExistente.Complemento = cliente.Complemento;
            clienteExistente.Bairro = cliente.Bairro;
            clienteExistente.Cidade = cliente.Cidade;
            clienteExistente.Estado = cliente.Estado;

            _bretonContext.SaveChanges();

            return NoContent();
        }

        //DELETE api/client/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cliente = _bretonContext.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            _bretonContext.Clientes.Remove(cliente);
            return NoContent();
        }
    }
}