﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.HttpAggregator.Repositories;

namespace Web.HttpAggregator.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        private readonly IIdentityRepository _repository;

        public CustomAuthorization(IIdentityRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// This will Authorize User
        /// </summary>
        /// <returns></returns>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("authToken", out authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    string authToken = _token;
                    if (authToken == null || string.IsNullOrEmpty(authToken))
                    {
                        InvalidToken(filterContext: filterContext);
                    }

                    if (IsValidToken(authToken))
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                        filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                        return;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        filterContext.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Invalid Token"
                            },
                        };
                    }
                }
                else
                {
                    InvalidToken(filterContext: filterContext);
                }
            }
        }

        private void InvalidToken(AuthorizationFilterContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
            filterContext.Result = new JsonResult("Please Provide authToken")
            {
                Value = new
                {
                    Status = "Error",
                    Message = "Please Provide authToken"
                },
            };
        }

        public bool IsValidToken(string authToken)
        {
            var task = Task.Run(async () => await _repository.Validate(authToken)).Result;
            return task;
        }
    }
}