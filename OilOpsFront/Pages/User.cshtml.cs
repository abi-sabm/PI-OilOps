using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class User : PageModel
{ 
    public List<UserModel> userList { get; set; }
    
    [BindProperty]
    public UserModel user { get; set; }
    
    // List: GetAll api/users
    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://localhost:7120/api/Users");

            if (response.IsSuccessStatusCode)
            {
                userList = await response.Content.ReadFromJsonAsync<List<UserModel>>();
            }
            else
            {
                userList = new List<UserModel>();
            }
        }
    }
    // Add: Post api/users
    public async Task<IActionResult> OnPost()
    {
        string name = Request.Form["Name"];
        string fullName = Request.Form["FullName"]; 
        string DNI = Request.Form["DNI"];
        string userName = Request.Form["UserName"];
        string password= Request.Form["Password"];
        
        var data = new 
        { 
            name = name,
            fullName = fullName,
            DNI = DNI,
            userName = userName,
            password = password
        };
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        
        using (var httpClient = new HttpClient()) 
        {
            var response = await httpClient.PostAsync("https://localhost:7120/api/Users", content);

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
    // Delete: Delete api/users
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7120/api/Users/" + id);

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