using System;
using System.Threading;
using System.Web.Http;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.ApiControllers
{
  [RoutePrefix("api/Data")]
  public class DataController : ApiController
  {
    public static readonly DateTime SampleDateTime = new DateTime(2012, 04, 29);

    [HttpGet]
    [Route("sample-data/{name}")]
    public SampleApiData GetSampleData(string name)
    {
      return new SampleApiData {
        DateAndTime = SampleDateTime,
        Name = name
      };
    }

    [HttpPost]
    [Route("slow-sample-data/{name}")]
    public SampleApiData GetSlowSampleData(string name)
    {
      Thread.Sleep(TimeSpan.FromSeconds(3));

      return new SampleApiData {
        DateAndTime = SampleDateTime,
        Name = name
      };
    }
  }
}
