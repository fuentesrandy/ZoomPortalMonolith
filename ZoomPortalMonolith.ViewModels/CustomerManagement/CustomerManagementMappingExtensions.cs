using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;

namespace ZoomPortalMonolith.ViewModels.CustomerManagement
{
    
    public static class CustomerManagementMappingExtensions
    {
        public static IEnumerable<CustomerVM> ToModels(this IEnumerable<Customer> query)
        {
            return
                from entity in query
                select new CustomerVM()
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
        }

        public static CustomerVM ToModel(this Customer entity)
        {
            return (new List<Customer>() { entity }).ToModels().FirstOrDefault();
        }

    }
}
