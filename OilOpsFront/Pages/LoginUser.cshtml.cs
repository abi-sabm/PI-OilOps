using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OilOpsFront.Models;

namespace OilOpsFront.Pages;

public class LoginUser : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    
    public LoginUser(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    [BindProperty]
    public LoginUserModel loginUser { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
           return Page(); 
        }
        
        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7120/api/login");
        request.Content = new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json");

        var client = _clientFactory.CreateClient();
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            var token = responseObject["token"];

            HttpContext.Session.SetString("BearerToken", token);

            TempData["Token"] = token;
            TempData.Keep("Token");
            return LocalRedirect(Url.Content("/Index"));
        }
        else
        {
            ModelState.AddModelError("Error", "Usuario o contraseña incorrectos.");
            return Page();
        }
    }
}