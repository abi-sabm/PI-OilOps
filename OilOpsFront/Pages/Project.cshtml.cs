using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class Project : PageModel
{
    public List<ProjectModel> projectList { get; set; }
    
    [BindProperty]
    public ProjectModel project { get; set; }
    
    //List: GetAll api/projects
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.GetAsync("http://localhost:7120/Projects");

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
    // GetById api/projects/{id}
    public Project Projects { get; set; } = new Project();
    public async Task OnGetAsync(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            Projects = await httpClient.GetFromJsonAsync<Project>($"Projects/{id}");
        }
    }
    // Add: Post api/projects
    public async Task<IActionResult> OnPostAsync(Project Project)
    {
        using (var httpClient = new HttpClient()) 
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
             var content = new StringContent(JsonConvert.SerializeObject(Project), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:7120/Projects", content);
            var getResponse = await httpClient.GetAsync("http://localhost:7120/Projects");
            
            if (response.IsSuccessStatusCode)
            {
                projectList = await getResponse.Content.ReadFromJsonAsync<List<ProjectModel>>();
            }
            else
            {
                projectList = new List<ProjectModel>();
            }
            return RedirectToPage("/Projects");
        }
    }
    // Delete: Delete api/projects
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.DeleteAsync($"http://localhost:7120/Projects/{project.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Projects");
            }
            else
            {
                return Page();
            }
        }
    }
}