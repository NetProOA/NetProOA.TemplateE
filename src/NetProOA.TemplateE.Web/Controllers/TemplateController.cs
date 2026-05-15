using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetProOA.Framework.Authorization.Interceptor;
using NetProOA.Framework.Authorization.Services;
using NetProOA.Framework.Identity.ClientSdk;
using NetProOA.Framework.Identity.ClientSdk.Models;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Application.Queries;

namespace NetProOA.TemplateE.Web.Controllers
{
    [Authorize]
    [Intercept(typeof(AuthorizationUserInterceptor))]
    public class TemplateController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IExampleProductQueries _exampleProductQueries;
        private readonly IIdentityClientService _identityClientService;
        private readonly IIdentityService _identityService;

        public TemplateController(IMediator mediator, IExampleProductQueries exampleProductQueries, IIdentityClientService identityClientService, IIdentityService identityService)
        {
            _mediator = mediator;
            _exampleProductQueries = exampleProductQueries;
            _identityClientService = identityClientService;
            _identityService = identityService;
        }

        public virtual async Task<IActionResult> Index()
        {
            var user = HttpContext.GetCurrentUser();
            return View();

        }
    }

}