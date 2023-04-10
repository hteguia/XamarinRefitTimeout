using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Refit;

namespace XamarinRefitTimeout
{
    [Headers("Content-Type: application/json", "Authorization: Bearer")]
    public interface ICallApi
    {
        [Get("/weatherforecast")]
        Task<HttpResponseMessage> GetAll(CancellationToken cancellationToken);
    }
}
