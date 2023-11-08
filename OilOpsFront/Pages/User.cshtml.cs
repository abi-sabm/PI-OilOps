using System.Net.Http.Headers;
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
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);  
            
            var response = await httpClient.GetAsync("http://localhost:7120/Users");

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
    // GetById api/users/{id}
    public User Users { get; set; } = new User();
    public async Task OnGetAsync(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            Users = await httpClient.GetFromJsonAsync<User>($"Users/{id}");
        }
    }
    // Add: Post api/users
    public async Task<IActionResult> OnPostAsync(User user)
    {
        using (var httpClient = new HttpClient()) 
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            
            var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7120/api/Users", content);
            var getResponse = await httpClient.GetAsync("http://localhost:7120/Users"); 

            if (response.IsSuccessStatusCode)
            {
                userList = await getResponse.Content.ReadFromJsonAsync<List<UserModel>>();
            }
            else
            {
                userList = new List<UserModel>();
            } 
            return RedirectToPage("/Users"); 
        }
    }
    // Delete: Delete api/users
    public async Task<IActionResult> OnPostDelete(int id)
    {
        using (var httpClient = new HttpClient())
        {
            string token = HttpContext.Session.GetString("BearerToken"); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.DeleteAsync($"https://localhost:7120/api/Users/{user.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Users");
            }
            else
            {
                return Page();
            }
        }
    }
}