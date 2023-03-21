using DenemeAPIMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DenemeAPIMVC.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Product> productList = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                //API'ye bağlanmak için localhost portuna bağlanılır.
                using(var response = await httpClient.GetAsync("https://localhost:7016/api/Product"))
                {
                    //Json olarak gelen veriyi string değere dönüştür.
                    string apiResponse = await response.Content.ReadAsStringAsync(); 

                    //string olarak gelen bu veriyi Product tipinde veri alan bir listeye dönüştür.
                    productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(productList);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product product = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7016/api/Product/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    //string olarak gelen bu veriyi product nesnesine dönüştürür.
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7016/api/Product/" + id))
                {
                    string x = await response.Content.ReadAsStringAsync();

                    //string olarak gelen bu veriyi product nesnesine dönüştürür.
                    //product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product guncellenecekProduct = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7016/api/Product/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    guncellenecekProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(guncellenecekProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            using (var httpClient = new HttpClient())
            {
                //düzenlenecek Product sayfasındaki inputlardan girilen değerlerden
                //bir product objesi oluşturuluyor
                Product guncellenecekProduct = new Product()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Image = product.Image,
                    Insert_Date = product.Insert_Date
                };
                //bu product objesini back-end projesindeki ilgili end pointe gönderir
                httpClient.BaseAddress = new Uri("https://localhost:7016/");
                //https://localhost:7016/api/Product adresine şöyle bir veri gider 
                //{id:product.ID,Name:product.Name}
                var response = httpClient.PutAsJsonAsync("api/Product", guncellenecekProduct).Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return NotFound();
            }
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            Product eklenenProduct = new Product();
            using (var httpClient = new HttpClient())
            {
                StringContent serializeEdilecekProduct = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7016/api/Product/",serializeEdilecekProduct))
                {
                }
                return RedirectToAction("Index");
            }
        }


    }
}
