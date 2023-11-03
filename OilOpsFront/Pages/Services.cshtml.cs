using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Services : PageModel
{
    public List<ServiceModel> serviceList { get; set; }
    
    [BindProperty]
    public ServiceModel service { get; set; }
    
    //List: GetAll api/services
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://localhost:7120/api/Services");

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
    // Add: Post api/services
    public async Task<IActionResult> OnPost()
    {
        string name = Request.Form["Name"];
        string description = Request.Form["Description"]; 
        string hourlyRate = Request.Form["HourlyRate"];
        string status = Request.Form["Status"];
        
        var data = new 
        { 
            name = name,
            description = description, 
            hourlyRate = hourlyRate,
            status = status
        };
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        
        using (var httpClient = new HttpClient()) 
        {
            var response = await httpClient.PostAsync("https://localhost:7120/api/Services", content);

            if (response.IsSuccessStatusCode)
            {
                await OnGetAsync();
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
    // Delete: Delete api/services
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7120/api/Services/" + id);

            if (response.IsSuccessStatusCode) 
            {
                await OnGetAsync();
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}