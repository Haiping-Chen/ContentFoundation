using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Quickflow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentFoundation.RestApi
{
    public static class QuickflowBuilderExtensions
    {
        public static void UseWorkflowEngine(this IApplicationBuilder app, IConfiguration configuration, String contentRootPath, String[] assembles)
        {
            WorkflowEngine.Configuration = configuration;
            WorkflowEngine.Assembles = assembles;
            WorkflowEngine.ContentRootPath = contentRootPath;
        }
    }
}
