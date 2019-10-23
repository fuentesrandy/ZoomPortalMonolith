using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;
using ZoomPortalMonolith.Domain.ProjectManagement.Entities;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Context;
using ZoomPortalMonolith.ViewModels.ProjectManagement;

namespace ZoomPortalMonolith.WebApp.Controllers.ProjectManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IDomainUnitOfWork _unitOfWork;

        public ProjectController(IDomainUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ProjectVM> GetCustomer(string id)
        {
            var project = await _unitOfWork.Projects.GetAsync(Guid.Parse(id));

            return project.ToModel();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ProjectVM>> Customers()
        {
            var projects = await _unitOfWork.Projects.AllAsync();

            return projects.ToModels();
        }

        [HttpPost("add")]
        public async Task<ProjectVM> AddProject([FromBody] ViewModels.ProjectManagement.ProjectVM model)
        {

            var customer = await _unitOfWork.Customers.GetAsync(model.CustomerId);

            var project = new Project(model.Title, customer);

            _unitOfWork.Projects.Add(project);

            await _unitOfWork.CommitAsync();

            return project.ToModel();
        }
    }
}