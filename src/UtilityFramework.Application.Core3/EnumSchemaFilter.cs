using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UtilityFramework.Application.Core3
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var allMemberValue = Enum.GetNames(context.Type).Select((n, i) => $"{context.Type.GetEnumMemberValue(n)}: {(int)Enum.Parse(context.Type, n)}");

                var descriptionStr = string.Join("<br>", allMemberValue);
                schema.Description = descriptionStr;
            }
        }

    }
}
