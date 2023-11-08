using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Service : PageModel
{
    public List<ServiceModel> serviceList { get; set; }

    [BindProperty] 
    public ServiceModel service { get; set; }
    
    //List: GetAll api/services
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
            var response = await httpClient.GetAsync("http://localhost:7120/Services");

            if (response.IsSuccessStatusCode)
            {
                serviceList = await response.Content.ReadFromJsonAsync<List<ServiceModel>>();
            }
            else
            {
                serviceList = new List<ServiceModel>();
            }
        }
    }
    // GetById api/services/{id}
    public Service Services { get; set; } = new Service();
    public async Task OnGetAsync(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            Services = await httpClient.GetFromJsonAsync<Service>($"Services/{id}");
        }
    }
    // Add: Post api/services
    public async Task<IActionResult> OnPostAsync(Service service)
    {
        using (var httpClient = new HttpClient()) 
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var content = new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:7120/Services", content);
            var getResponse = await httpClient.GetAsync("http://localhost:7120/Services");
            
            if (response.IsSuccessStatusCode)
            {
                serviceList = await getResponse.Content.ReadFromJsonAsync<List<ServiceModel>>();
            }
            else
            {
                serviceList = new List<ServiceModel>();
            }
            return RedirectToPage("/Services");
        }
    }
    // Delete: Delete api/services
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.DeleteAsync($"http://localhost:7120/Services/{service.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Services");
            }
            else
            {
                return Page();
            }
        }
    }
}