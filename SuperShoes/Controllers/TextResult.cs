using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SuperShoes.Controllers
{
    public class TextResult : IHttpActionResult
    {
        System.Net.HttpStatusCode _status;
        IQueryable<object> _elements;
        HttpRequestMessage _request;
        object _message;

        public TextResult(HttpStatusCode status, HttpRequestMessage request, object message)
        {
            _status = status;
            _message = message;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_status == HttpStatusCode.OK)
            {
                return Task.FromResult(
                    _request.CreateResponse(HttpStatusCode.OK, _message)
                );
            }
            else
            {
                if (_status == HttpStatusCode.NotFound)
                {
                    var message = new
                    {
                        error_msg = "Record not Found",
                        error_code = "404",
                        success = "false"
                    };

                    return Task.FromResult(
                        _request.CreateResponse(HttpStatusCode.NotFound, message)
                    );
                }
                else
                {

                    var message = new
                    {
                        error_msg = "Bad request",
                        error_code = "400",
                        success = "false"
                    };

                    return Task.FromResult(
                        _request.CreateResponse(HttpStatusCode.BadRequest, message)
                    );
                }
            }
        }
    }
}