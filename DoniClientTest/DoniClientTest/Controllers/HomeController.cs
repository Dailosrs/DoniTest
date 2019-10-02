using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoniClientTest.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DoniClientTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiAddress = "https://localhost:44322/api/Tour";

        // En la pagina de inicio hacemos peticion a la API para cargar todos los Tours
        public async Task<IActionResult> Index()
        {
            List<Tour> tours = new List<Tour>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiAddress))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tours = JsonConvert.DeserializeObject<List<Tour>>(apiResponse);
                }
            }
            return View(tours);
        }

        // Hacemos peticion de crear un nuevo Tour a la API enviando los datos del formulario
        public ViewResult AddTour() => View();
        [HttpPost]
        public async Task<IActionResult> AddTour(Tour tour)
        {
            Tour receivedTour = new Tour();
            using (var httpClient = new HttpClient())
            {
                // Convertimos el objeto a JSON y lo enviamos a la API
                StringContent content = new StringContent(JsonConvert.SerializeObject(tour), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiAddress, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedTour = JsonConvert.DeserializeObject<Tour>(apiResponse);
                }
            }
            return View(receivedTour);
        }

        // Pedimos los datos de un Tour especifico a la API por ID para cargarlos en la vista de Edicion
        public async Task<IActionResult> UpdateTour(int id)
        {
            Tour tour = new Tour();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiAddress + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tour = JsonConvert.DeserializeObject<Tour>(apiResponse);
                }
            }
            return View(tour);
        }

        // Hacemos peticion de actualizar un Tour a la API enviando los datos del formulario
        [HttpPost]
        public async Task<IActionResult> UpdateTour(Tour tour)
        {
            Tour receivedTour = new Tour();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(tour), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(apiAddress + "/" + tour.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedTour = JsonConvert.DeserializeObject<Tour>(apiResponse);
                }
            }
            return View(receivedTour);
        }

        // Recibimos el ID del tour a eliminar desde el boton y lo enviamos a la API en un DELETE
        [HttpPost]
        public async Task<IActionResult> DeleteTour(int TourId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(apiAddress + "/" + TourId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


        // Pedimos los datos de un Tour especifico a la API por ID para cargarlos en la vista de detalles
        public async Task<IActionResult> ShowTourDetails(int id)
        {
            Tour tour = new Tour();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiAddress + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tour = JsonConvert.DeserializeObject<Tour>(apiResponse);
                }
            }
            return View(tour);
        }

    }
}
