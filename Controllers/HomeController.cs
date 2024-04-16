using CarModelManagement.BLL.Helper;
using CarModelManagement.BLL.Models;
using CarModelManagement.FluentValidation;
using CarModelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CarModelManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        APIUtility apiUtility = new APIUtility();
        private readonly string baseUrl;
        private readonly string CarListBaseUrl;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            baseUrl = _configuration["APIUrls:CommonServiceServerURL"];
            CarListBaseUrl = baseUrl + _configuration["APIUrls:CarListBaseUrl"];
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAllCars()
        {
            try
            {
               
                List<CarDTO> carModel = new List<CarDTO>();
                var resultClient = apiUtility.GetApi(CarListBaseUrl + "/GetAllCars", "");
                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    List<Cars> jsonObjectData = JsonConvert.DeserializeObject<List<Cars>>(data);
                    foreach (var item in jsonObjectData)
                    {
                        carModel.Add(new CarDTO
                        {
                            CarId = item.CarId,
                            Brand = item.Brand,
                            Class = item.Class,
                            ModelName = item.ModelName,
                            ModelCode = item.ModelCode,
                            Description = item.Description,
                            Features = item.Features,
                            Price = item.Price,
                            ManufacturedOn = item.ManufacturedOn,
                            IsDeleted = item.IsDeleted,
                            IsActive = item.IsActive

                        });
                    }
                }
                return Json(new { data = carModel });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddCar(Cars car)
        {
            try
            {
                var success = "";
                car.IsActive = true;
                car.IsDeleted = false;
                car.CarId = 0;
                var json = JsonConvert.SerializeObject(car);
                var validator = new CarValidator();
                var validationResult = validator.Validate(car);

               // car.FilePath = new List<string>();

                foreach (var file in HttpContext.Request.Form.Files)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarModelImages", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }

                    // Add the file path to the list
                    //car.FilePath.Add(filePath);
                   // car.Images = filePath;
                }

                if (validationResult.IsValid)
                {
                    var resultClient = apiUtility.PostApi(CarListBaseUrl + "/AddCar/", car, "");
                    if (resultClient.IsSuccessStatusCode)
                    {
                        var data = resultClient.Content.ReadAsStringAsync().Result;
                        success = JsonConvert.DeserializeObject<string>(data);
                        return Json(new { success, message = "Car Data Successfully Inserted" });
                    }

                    else
                    {
                        string error = "Error";
                        return Json(new { error, message = "Error while Inserting records!!" });
                    }
                }
                else
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    return Json(new { success = false, errors });
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        

        [HttpPost]
        public ActionResult UpdateCar(Cars car)
        {
            try
            {
                var success = "";
                car.IsDeleted = false;

                var json = JsonConvert.SerializeObject(car);

                var validator = new CarValidator();
                var validationResult = validator.Validate(car);

                foreach (var file in HttpContext.Request.Form.Files)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarModelImages", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }

                    // Add the file path to the list
                    //car.FilePath.Add(filePath);
                    // car.Images = filePath;
                }

                if (validationResult.IsValid)
                {
                    var resultClient = apiUtility.PostApi(CarListBaseUrl + "/UpdateCar/", car, "");
                    if (resultClient.IsSuccessStatusCode)
                    {
                        var data = resultClient.Content.ReadAsStringAsync().Result;
                        success = JsonConvert.DeserializeObject<string>(data);
                        return Json(new { success, message = "Car Data Successfully Updated" });
                    }
                    else
                    {
                        string error = "fail";
                        return Json(new { error, message = "Error while updating Car records!!" });
                    }
                }
                else
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    return Json(new { success = false, errors });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateMenu: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult DeleteCar(int car)
        {
            try
            {
                var success = "";
                var json = JsonConvert.SerializeObject(car);
                var resultClient = apiUtility.PostApi(CarListBaseUrl + "/DeleteCar?car=", car, "");

                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    success = JsonConvert.DeserializeObject<string>(data);
                    return Json(new { success, message = "Car Data Successfully Deleted" });
                }
                else
                {
                    string error = "fail";
                    return Json(new { error, message = "Error while deleting Car records!!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteMenu: " + ex.Message);
                throw;
            }
        }
    }
}