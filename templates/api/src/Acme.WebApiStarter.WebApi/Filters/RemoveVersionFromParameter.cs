﻿using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Acme.WebApiStarter.WebApi.Filters {
    public class RemoveVersionFromParameter : IOperationFilter {
        public void Apply(OpenApiOperation operation, OperationFilterContext context) {
            if (!operation.Parameters.Any()) {
                return;
            }

            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
