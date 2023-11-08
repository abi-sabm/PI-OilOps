using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Work : PageModel
{
    public List<WorkModel> workList { get; set; }
    
    [BindProperty]
    public WorkModel work { get; set; }
    
    //List: GetAll api/works
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.GetAsync("http://localhost:7120/Works");

            if (response.IsSuccessStatusCode)
            {
                workList = await response.Content.ReadFromJsonAsync<List<WorkModel>>();
            }
            else
            {
                workList = new List<WorkModel>();
            }
        }
    }
    // GetById api/works/{id}
    public Work Works { get; set; } = new Work();
    public async Task OnGetAsync(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            Works = await httpClient.GetFromJsonAsync<Work>($"Works/{id}");
        }
    }
    // Add: Post api/works
    public async Task<IActionResult> OnPostAsync(Work Work)
    {
        using (var httpClient = new HttpClient()) 
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);             
            
            var content = new StringContent(JsonConvert.SerializeObject(Work), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:7120/Works", content);
            var getResponse = await httpClient.GetAsync("http://localhost:7120/Works");

            if (getResponse.IsSuccessStatusCode)
            {
                workList = await getResponse.Content.ReadFromJsonAsync<List<WorkModel>>();
            }
            else
            {
                workList = new List<WorkModel>();
            }
        }
        return RedirectToPage("/Works");
    }
    // Delete: Delete api/works
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        { 
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            
            var response = await httpClient.DeleteAsync($"https://localhost:7120/Works/{work.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Works");
            }
            else
            {
                return Page();
            }
        }
    }
}