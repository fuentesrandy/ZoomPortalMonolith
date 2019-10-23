using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomPortalMonolith.Domain.ProjectManagement.Entities;
using ZoomPortalMonolith.ViewModels.CustomerManagement;

namespace ZoomPortalMonolith.ViewModels.ProjectManagement
{
    public static class ProjectManagementMappingExtensions
    {
        public static IEnumerable<ProjectVM> ToModels(this IEnumerable<Project> query)
        {
            return
                from entity in query
                select new ProjectVM()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    CustomerId = entity.CustomerId
                };
        }

        public static ProjectVM ToModel(this Project entity)
        {
            return (new List<Project>() { entity }).ToModels().FirstOrDefault();
        }
    }
}
