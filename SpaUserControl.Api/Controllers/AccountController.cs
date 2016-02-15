using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SpaUserControl.Api.Attributes;
using SpaUserControl.Api.Models.Account;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Common.Resources;
using WebApi.OutputCache.V2;

namespace SpaUserControl.Api.Controllers
{
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        private IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> Get() // int skip, int take)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.GetByRange(0, 25);//(skip, take);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        [Route("")]
        [HttpPost]
        [DeflateCompression]
        public Task<HttpResponseMessage> Register(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Register(model.Name, model.Email, model.Password, model.ConfirmPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, new {name = model.Name, email = model.Email});
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        [Route("")]
        [HttpPut]
        [Authorize]
        [DeflateCompression]
        public Task<HttpResponseMessage> ChangeInformation(ChangeInformationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ChangeInformation(User.Identity.Name, model.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = model.Name });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        [Route("changepassword")]
        [HttpPost]
        [Authorize]
        [DeflateCompression]
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ChangePassword(User.Identity.Name, model.Password, model.NewPassword, model.ConfirmNewPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.PasswordSuccessfulyChanges);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        [HttpPost]
        [Route("resetpassword")]
        [DeflateCompression]
        public Task<HttpResponseMessage> ResetPassword(ResetPasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ResetPassword(model.Email);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.ResetPasswordEmailBody);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
    }
}