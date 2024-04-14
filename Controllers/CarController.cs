//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System;
//using CarModelManagement.BLL.Helper;
//using CarModelManagement.BLL.Models;

//namespace CarModelManagement.Controllers
//{
//    public class CarController : Controller
//    {
//        private readonly IConfiguration _configuration;

//        APIUtility apiUtility = new APIUtility();
//        private readonly string baseUrl;
//        private readonly string CountyBaseUrl;
//        private readonly ILogger<CarController> _logger;
//        public CarController(IConfiguration configuration, ILogger<CarController> logger)
//        {
//            _configuration = configuration;
//            baseUrl = _configuration["APIUrls:CommonServiceServerURL"];
//            CountyBaseUrl = baseUrl + _configuration["APIUrls:CountyBaseUrl"];
//            _logger = logger;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet] 
//        public JsonResult GetCounties(int? id = 0)
//        {
//            List<Cars> jsonObjectData = new List<Cars>();
//            try
//            {
//                var token = Request.Headers["Authorization"];
//                var resultClient = apiUtility.GetApi(CountyBaseUrl + "?id=" + id, token);
//                if (resultClient.IsSuccessStatusCode)
//                {
//                    var data = resultClient.Content.ReadAsStringAsync().Result;
//                    jsonObjectData = JsonConvert.DeserializeObject<List<Cars>>(data);
//                    var result = jsonObjectData;
//                    var jsonResult = Json(new { aaData = result });
//                    return jsonResult;
//                }
//                return Json(new { aaData = jsonObjectData });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { aaData = jsonObjectData });
//            }
//        }
//        [HttpGet]
//        public JsonResult GetCountiesByStateID(int id = 0)
//        {
//            List<Cars> jsonObjectData = new List<Cars>();
//            try
//            {
//                var token = Request.Headers["Authorization"];
//                var resultClient = apiUtility.GetApi(CountyBaseUrl + "/GetByState?id=" + id, token);
//                if (resultClient.IsSuccessStatusCode)
//                {
//                    var data = resultClient.Content.ReadAsStringAsync().Result;
//                    jsonObjectData = JsonConvert.DeserializeObject<List<Cars>>(data);
//                    var result = jsonObjectData;
//                    var jsonResult = Json(new { aaData = result });
//                    return jsonResult;
//                }
//                return Json(new { aaData = jsonObjectData });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { aaData = jsonObjectData });
//            }
//        }
//        [HttpGet]
//        public JsonResult GetCountry()
//        {
//            List<Cars> jsonObjectData = new List<Cars>();
//            try
//            {
//                var token = Request.Headers["Authorization"];
//                var resultClient = apiUtility.GetApi(CountyBaseUrl + "/GetCountry", token);
//                if (resultClient.IsSuccessStatusCode)
//                {
//                    var data = resultClient.Content.ReadAsStringAsync().Result;
//                    jsonObjectData = JsonConvert.DeserializeObject<List<Car>>(data);
//                    var result = jsonObjectData;
//                    var jsonResult = Json(new { aaData = result });
//                    return jsonResult;
//                }
//                return Json(new { aaData = jsonObjectData });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { aaData = jsonObjectData });
//            }
//        }
//        //[HttpPost]
//        //public ActionResult AddCountry(Counties obj)
//        //{
//        //    try
//        //    {
//        //        var success = "";
//        //        var token = Request.Headers["Authorization"];
//        //        var json = JsonConvert.SerializeObject(obj);

//        //        var resultClient = apiUtility.PostApi(CountyBaseUrl, obj, token);
//        //        if (resultClient.IsSuccessStatusCode)
//        //        {
//        //            var data = resultClient.Content.ReadAsStringAsync().Result;
//        //            success = JsonConvert.DeserializeObject<string>(data);
//        //            if (success == "Inserted")
//        //            {
//        //                return Json(new { success, message = "Data has been inserted successfully" });
//        //            }
//        //            else
//        //            {
//        //                string warning = "Not Inserted";
//        //                return Json(new { warning, message = "Data  Already exists!!!" });
//        //            }
//        //        }
//        //        else
//        //        {
//        //            string error = "Error";
//        //            LogFileCreateClass.LogFilecreate("Error while Inserting records!!");
//        //            return Json(new { error, message = "Error while Inserting records!!" });
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        LogFileCreateClass.LogFilecreate("Counties:AddCountry:" + ex);
//        //        _logger.LogError("Add: " + ex.Message);
//        //        return Json(null);
//        //    }
//        //}
//        //[HttpPost]
//        //public ActionResult UpdateCountry(Counties obj)
//        //{
//        //    try
//        //    {
//        //        var success = "";
//        //        var token = Request.Headers["Authorization"];
//        //        var json = JsonConvert.SerializeObject(obj);

//        //        var resultClient = apiUtility.PutApi(CountyBaseUrl, obj, token);
//        //        if (resultClient.IsSuccessStatusCode)
//        //        {
//        //            var data = resultClient.Content.ReadAsStringAsync().Result;
//        //            success = JsonConvert.DeserializeObject<string>(data);
//        //            if (success == "Updated")
//        //            {
//        //                return Json(new { success, message = "Data has been updated successfully" });
//        //            }
//        //            else
//        //            {
//        //                string warning = "Warning";
//        //                return Json(new { warning, message = "Data Already exists!!!" });
//        //            }
//        //        }
//        //        else
//        //        {
//        //            string error = "Error";
//        //            LogFileCreateClass.LogFilecreate("Error while Updating records!!");
//        //            return Json(new { error, message = "Error while Updating records!!" });
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        LogFileCreateClass.LogFilecreate("Counties:UpdateCountry:" + ex);
//        //        _logger.LogError("UpdateRatePer: " + ex.Message);
//        //        return Json(null);
//        //    }
//        //}

//        //[HttpGet]
//        //public ActionResult DeleteCar(int id, Guid userId)
//        //{
//        //    try
//        //    {
//        //        var success = "";
//        //        var token = Request.Headers["Authorization"];
//        //        var resultClient = apiUtility.DeleteApi(CountyBaseUrl + "?CountyId=" + id + "&Guid=" + userId, token);

//        //        if (resultClient.IsSuccessStatusCode)
//        //        {
//        //            var data = resultClient.Content.ReadAsStringAsync().Result;
//        //            success = JsonConvert.DeserializeObject<string>(data);
//        //            return Json(new { success, message = "Data Successfully Deleted" });
//        //        }
//        //        return Json(null);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return Json(null);
//        //    }

//        //}
//        //[HttpGet]
//        //public ActionResult GetCityByState(string IDs)
//        //{
//        //    List<City> jsonObjectData = new List<City>();
//        //    try
//        //    {
//        //        var success = "";
//        //        var token = Request.Headers["Authorization"];                
//        //        var resultClient = apiUtility.GetApi(CountyBaseUrl + "/GetMultiRecord?id=" + IDs, token);

//        //        if (resultClient.IsSuccessStatusCode)
//        //        {
//        //            var data = resultClient.Content.ReadAsStringAsync().Result;
//        //            jsonObjectData = JsonConvert.DeserializeObject<List<City>>(data);
//        //            var jsonResult = Json(new { aaData = jsonObjectData });
//        //            return jsonResult;
//        //        }
//        //        return Json(null);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        LogFileCreateClass.LogFilecreate("Counties:GetCityByState:" + ex);
//        //        _logger.LogError("DeleteCountry: " + ex.Message);
//        //        return Json(null);
//        //    }

//        //}
//        //[HttpGet]
//        //public ActionResult GetCountiesBystate(string IDs)
//        //{
//        //    List<Counties> jsonObjectData = new List<Counties>();
//        //    try
//        //    {
//        //        var success = "";
//        //        var token = Request.Headers["Authorization"];                
//        //        var resultClient = apiUtility.GetApi(CountyBaseUrl + "/GetRecord?id=" + IDs, token);

//        //        if (resultClient.IsSuccessStatusCode)
//        //        {
//        //            var data = resultClient.Content.ReadAsStringAsync().Result;
//        //            jsonObjectData = JsonConvert.DeserializeObject<List<Counties>>(data);
//        //            var jsonResult = Json(new { aaData = jsonObjectData });
//        //            return jsonResult;
//        //        }
//        //        return Json(null);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        LogFileCreateClass.LogFilecreate("Counties:GetCountiesBystate:" + ex);
//        //        _logger.LogError("DeleteCountry: " + ex.Message);
//        //        return Json(null);
//        //    }

//        //}

//    }
//}
