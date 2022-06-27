using ApiClientes.Infra.Data.Entities;
using ApiClientes.Infra.Data.Interfaces;
using ApiClientes.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost]
        public IActionResult Post(ClientePostRequest request)
        {
            try
            {
                if (_clienteRepository.GetByEmail(request.Email) != null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new
                    { mensagem = "O email informado já está cadastrado." });

                if (_clienteRepository.GetByCpf(request.Cpf) != null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new
                    { mensagem = "O CPF informado já está cadastrado." });


                var cliente = new Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Cpf = request.Cpf,
                    DataNascimento = request.DataNascimento,

                };
                _clienteRepository.Create(cliente);

                //HTTP 201 - CREATE (Sucesso!)
                return StatusCode(201, new { mensagem = "Empresa cadastrada com sucesso.", cliente });

            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
        [HttpPut]
        public IActionResult Put(ClientePutRequest request)
        {
            try
            {
                if (_clienteRepository.GetById(request.IdCliente) == null)

                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhum cliente cadastrado." });

                var clienteByCpf = _clienteRepository.GetByCpf(request.Cpf);

                if (clienteByCpf != null && clienteByCpf.IdCliente != request.IdCliente)

                    return StatusCode(422, new { mensagem = "O CPF informado já encontra-se cadastrado para outro cliente." });

                var clienteByEmail = _clienteRepository.GetByEmail(request.Email);

                if (clienteByEmail != null && clienteByEmail.IdCliente != request.IdCliente)

                    return StatusCode(422, new { mensagem = "O email informado já encontra-se cadastrado para outro cliente." });

                var cliente = new Cliente
                {
                    IdCliente = request.IdCliente,

                    Nome = request.Nome,
                    Email = request.Email,
                    Cpf = request.Cpf,
                    DataNascimento = request.DataNascimento,
                };

                _clienteRepository.Update(cliente);
                return StatusCode(200, new { mensagem = "Cliente atualizado com sucesso.", cliente });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
        [HttpDelete("{idCliente}")]
        public IActionResult Delete(Guid idCliente)
        {
            try
            {
                var cliente = _clienteRepository.GetById(idCliente);
                if (cliente == null)

                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhum cliente cadastrado." });

                _clienteRepository.Delete(cliente);
                return StatusCode(200, new { mensagem = "Cliente excluído com sucesso.", cliente });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();
                if (clientes.Count > 0)

                    return StatusCode(200, clientes);

                else

                    return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
        [HttpGet("{idCliente}")]
        public IActionResult GetById(Guid idCliente)
        {
            try
            {
                var cliente = _clienteRepository.GetById(idCliente);

                if (cliente != null)
                    //HTTP 200 - OK (Sucesso!)
                    return StatusCode(200, cliente);
                else
                    //HTTP 204 - NO CONTENT (Vazio)
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}
