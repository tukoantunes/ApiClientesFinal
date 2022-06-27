using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using Bogus;
using Bogus.Extensions.Brazil;
using ApiClientes.Tests;
using ApiClientes.Services.Requests;
using System;
using ApiClientes.Infra.Data.Entities;

namespace ClientesApi.Tests
{
    public class ClientesTests
    {
        private readonly string _endpoint;

        public ClientesTests()
        {
            _endpoint = ApiConfig.GetEndpoint() + "/Clientes";
        }

        [Fact]
        public async Task<ClienteResult> Test_Post_Returns_Ok()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(_endpoint, CreateCliente());

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<ClienteResult>
               (response.Content.ReadAsStringAsync().Result);

            return result;            
        }

        [Fact]
        public async Task Test_Put_Returns_OK()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var faker = new Faker("pt_BR");

            var request = new ClientePutRequest
            {
                IdCliente = result.cliente.IdCliente,
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                DataNascimento = DateTime.Now.AddYears(-19),
                Email = faker.Person.Email.ToLower()
            };

            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(_endpoint, content);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_Delete_Returns_OK()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(_endpoint + "/" + result.cliente.IdCliente);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_GetAll_Returns_OK()
        {
            await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(_endpoint);

            var result = JsonConvert.DeserializeObject<List<Cliente>>
               (response.Content.ReadAsStringAsync().Result);

            result.
                Should()
                .NotBeNullOrEmpty();
        }
        private StringContent CreateCliente()
        {
            var faker = new Faker("pt_BR");

            var request = new ClientePostRequest()
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                DataNascimento = DateTime.Now.AddYears(-19),
                Email = faker.Person.Email.ToLower()
            };

            return new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }

    }
    public class ClienteResult
    {
        public string message { get; set; }
        public Cliente cliente { get; set; }
    }
}