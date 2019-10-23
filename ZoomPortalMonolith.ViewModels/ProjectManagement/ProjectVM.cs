using System;
using System.Collections.Generic;
using System.Text;
using ZoomPortalMonolith.ViewModels.CustomerManagement;

namespace ZoomPortalMonolith.ViewModels.ProjectManagement
{
    public class ProjectVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CustomerId { get; set; }
    }
}
