using CarModelManagement.BLL.Helper;
using CarModelManagement.BLL.Models;
using CarModelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

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
                var token = Request.Headers["Authorization"];
                List<CarDTO> carModel = new List<CarDTO>();
                var resultClient = apiUtility.GetApi(CarListBaseUrl + "/GetAllCars", token);
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
                var token = Request.Headers["Authorization"];
                var json = JsonConvert.SerializeObject(car);

                var resultClient = apiUtility.PostApi(CarListBaseUrl + "/AddCar/", car, token);
                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    success = JsonConvert.DeserializeObject<string>(data);
                    return Json(new { success, message = "Menu Data Successfully Inserted" });
                }
               
                else
                {
                    string error = "Error";
                    return Json(new { error, message = "Error while Inserting records!!" });
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}