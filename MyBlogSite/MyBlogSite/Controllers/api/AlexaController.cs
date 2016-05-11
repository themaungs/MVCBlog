using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using AlexaSkillsKit;
using System.Threading.Tasks;
using MyBlogSite.ViewModels;
using System.Web;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Repositories;
using MyBlogSite.Domain.Models.DBContext;

namespace MyBlogSite.Controllers.api
{
    //[EnableCors("*", "*", "GET")]
    [AllowAnonymous]
    public class AlexaController : ApiController
    {
        private IAlexaSensorRepository _alexaRepo;
        public AlexaController()
        {
            _alexaRepo = new AlexaSensorRepository(new MyBlogSiteDatabaseContext());
        }
        //[HttpPost]
        //[System.Web.Mvc.RequireHttps]
        //[ActionName("SampleSession")]
        //public HttpResponseMessage SampleSession()
        //{
        //    var speechlet = new SampleSessionSpeechlet();
        //    return speechlet.GetResponse(Request);
        //}
        //[Route("alexa/sample-session")]
        //[HttpPost]
        //public HttpResponseMessage SampleSessions()
        //{
        //    var speechlet = new SampleSessionSpeechlet();
        //    return speechlet.GetResponse(Request);
        //}
        //[AllowAnonymous]
        //[System.Web.Mvc.RequireHttps]
        //[HttpGet]
        //[ActionName("GetHello")]
        //public async Task<HttpResponseMessage> GetHello()
        //{
        //    return new HttpResponseMessage (HttpStatusCode.OK);
        //}
        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<HttpResponseMessage> MyAppEndpoint()
        //{
        //    HttpRequestMessage httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        //    var speechlet = new SampleSessionSpeechlet();
        //    return speechlet.GetResponse(httpRequestMessage);
        //}
        [AllowAnonymous]
        [HttpGet]
        [ActionName("Sensor")]
        public async Task<HttpResponseMessage> Sensor(string SensorName, string SensorValue)
        {
            try {
                AlexaSensor newSensorData = new AlexaSensor();
                newSensorData.SensorName = SensorName;
                newSensorData.SensorValue = SensorValue;
                newSensorData.CreatedBy = "Alexa's Sensors";
                newSensorData.CreatedDate = DateTime.Now;
                newSensorData.LastUpdatedBy = "Alexa's Sensors";
                newSensorData.LastUpdated = DateTime.Now;
                _alexaRepo.Add(newSensorData);
                _alexaRepo.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [ActionName("Sensor")]
        public AlexaSensor GetSensor(string SensorName)
        {
            return _alexaRepo.GetLatest(SensorName);
        }

    }
}
