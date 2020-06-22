using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using setVideo.Model;
using setVideo.Service;

namespace setVideo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller , ICustomerController
    {
        private readonly ICustomerServices _customerServices;
        public CustomerController(ICustomerServices customersService)
        {
            _customerServices = customersService;
        }
        [HttpGet("{id}")]
        public Customer Get(int id) => _customerServices.GetById(id);

        [HttpPost()]
        public string Post([FromBody] Customer customer)
        { return _customerServices.Add(customer); }

        [HttpPost("{id}")]
        public void DeleteUser(int id) => _customerServices.DeleteUser(id);


    }

    public interface ICustomerController
    {
        Customer Get(int id);
        string Post(Customer customer);
        void DeleteUser(int id);

    }
}
