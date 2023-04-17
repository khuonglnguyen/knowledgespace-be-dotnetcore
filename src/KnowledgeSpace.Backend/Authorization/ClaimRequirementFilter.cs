using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using KnowledgeSpace.Backend.Constants;
using System.Linq;

namespace KnowledgeSpace.Backend.Authorization
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly FunctionCode _functionCode;
        private readonly CommandCode _commandCode;

        public ClaimRequirementFilter(FunctionCode functionCode, CommandCode commandCode)
        {
            _functionCode = functionCode;
            _commandCode = commandCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionsClaim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == SystemConstants.Claims.Permissions);
            if (permissionsClaim != null)
            {
                var permissions = JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
                if (!permissions.Contains(_functionCode + "_" + _commandCode))
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
