using System;
using System.Net;
using System.Web.Http;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.ApiControllers
{
  [RoutePrefix("api/Execution")]
  public class ExecutionController : ApiController
  {
    public static readonly string ValidName = "Valid name";

    static int myNumber;

    [HttpPost]
    [Route("SetMyNumber")]
    public void SetMyNumber(SampleApiData data)
    {
      if(data == null)
        throw new ArgumentNullException(nameof(data));
      
      myNumber = data.NewNumber;
    }

    [HttpGet]
    [Route("GetMyNumber")]
    public int GetMyNumber()
    {
      return myNumber;
    }

    [HttpPut]
    [Route("CheckData")]
    public void CheckData(SampleApiData data)
    {
      if(data == null)
        throw new ArgumentNullException(nameof(data));

      if(data.Name != ValidName)
        throw new HttpResponseException(HttpStatusCode.BadRequest);
    }
  }
}
