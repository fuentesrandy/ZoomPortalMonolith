using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Context;
using ZoomPortalMonolith.ViewModels.CustomerManagement;

namespace ZoomPortalMonolith.WebApp.Controllers.CustomerManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IDomainUnitOfWork _unitOfWork;

        public CustomerController(IDomainUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<CustomerVM> GetCustomer(string id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(Guid.Parse(id));

            return customer.ToModel();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<CustomerVM>> Customers()
        {
            var customers = await _unitOfWork.Customers.AllAsync();

            return customers.ToModels();
        }

        [HttpPost("add")]
        public async Task<CustomerVM> AddCustomer([FromBody] ViewModels.CustomerManagement.CustomerVM model)
        {

            var customer = new Customer(model.Name);

            _unitOfWork.Customers.Add(customer);

            await _unitOfWork.CommitAsync();

            return customer.ToModel();

        }

    }
}