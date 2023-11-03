using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Works : PageModel
{
    public List<WorkModel> workList { get; set; }
    
    [BindProperty]
    public WorkModel work { get; set; }
    
    //List: GetAll api/works
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://localhost:7120/api/Works");

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
    // Add: Post api/works
    public async Task<IActionResult> OnPost()
    {

        string date = Request.Form["Date"]; 
        string hoursService = Request.Form["HoursService"];
        string hourlyRate = Request.Form["HourlyRate"];
        string value = Request.Form["Value"];
        string idProject = Request.Form["IdProject"];
        string idService = Request.Form["IdService"];
        
        var data = new 
        { 
            date = date,
            hoursService = hoursService,
            hourlyRate = hourlyRate,
            value = value,
            idProject = idProject,
            idService = idService
        };
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        
        using (var httpClient = new HttpClient()) 
        {
            var response = await httpClient.PostAsync("https://localhost:7120/api/Works", content);

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
    // Delete: Delete api/works
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7120/api/Works/" + id);

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