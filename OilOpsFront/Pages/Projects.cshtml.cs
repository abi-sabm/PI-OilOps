using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Projects : PageModel
{
    public List<ProjectModel> projectList { get; set; }
    
    [BindProperty]
    public ProjectModel project { get; set; }
    
    //List: GetAll api/projects
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://localhost:7120/api/Projects");

            if (response.IsSuccessStatusCode)
            {
                projectList = await response.Content.ReadFromJsonAsync<List<ProjectModel>>();
            }
            else
            {
                projectList = new List<ProjectModel>();
            }
        }
    }
    // Add: Post api/projects
    public async Task<IActionResult> OnPost()
    {
        string name = Request.Form["Name"];
        string address = Request.Form["Address"]; 
        string status = Request.Form["Status"];
        
        var data = new 
        { 
            name = name,
            address = address,
            status = status
        };
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        
        using (var httpClient = new HttpClient()) 
        {
            var response = await httpClient.PostAsync("https://localhost:7120/api/Projects",content);

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
    // Delete: Delete api/projects
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7120/api/Projects/" + id);

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