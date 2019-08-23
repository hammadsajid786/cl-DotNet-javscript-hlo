using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLRMIS.Web.AdminUI.ViewModel
{
    public class LoginViewModel : PageModel
    {
        AppSettings appSettings;

        public void OnGet()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //foreach (var cookie in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(cookie);
            //}
            //    bool isUserLoggedIn = false;
            //    foreach (var cookie in Request.Cookies.Keys)
            //    {
            //        if (cookie.ToLower().Trim() == "token")
            //        {
            //            isUserLoggedIn = true;
            //            break;
            //        }
            //    }
            //    if (isUserLoggedIn)
            //        return Redirect(appSettings.RootUrl);
            //    else
            //        return Page();
        }
        public LoginViewModel(IOptions<AppSettings> settings)
        {
            appSettings = settings.Value;
        }

        [BindProperty]
        public LoginModel User { get; set; }
        public IActionResult OnPostAsync(string returnUrl = "")
        {
            try
            {
                ViewData["ErrorMessage"] = string.Empty;
                if (!ModelState.IsValid)
                {
                    ViewData["ErrorMessage"] = ErrorCodeEnum.INVALID_CREDENTIALS.ToString();
                    return Page();
                }
                var response = GetLoginAPIResponse(appSettings.APIBaseUrl + "/Login").Result;

                if (!response.IsSuccessStatusCode)
                {
                    ViewData["ErrorMessage"] = ErrorCodeEnum.INVALID_CREDENTIALS.ToString();
                    return Page();
                }
                var result = response.Content.ReadAsAsync<LoginResponseModel>();
                var jsonToken = parseToken(result.Result.Token);
                var userName = jsonToken.Claims.First(c => c.Type == "UserName").Value;
                var functionNames = jsonToken.Claims.Where(c => c.Type == ClaimEnum.FUNCTION.ToString()).ToList();
                var identity = new ClaimsIdentity(functionNames, CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                identity.AddClaim(new Claim("FirstName", jsonToken.Claims.First(c => c.Type == "FirstName").Value));
                identity.AddClaim(new Claim("LastName", jsonToken.Claims.First(c => c.Type == "LastName").Value));
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Response.Cookies.Append(
                    "token",
                    result.Result.Token,
                    new CookieOptions()
                    {
                        Path = "/",
                        HttpOnly = false,
                        IsEssential = true, //<- there
                        Expires = DateTime.Now.AddHours(this.appSettings.AuthCookieExpiryInHours),
                    });
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect(appSettings.RootUrl);
            }
            catch
            {
                ViewData["ErrorMessage"] = ErrorCodeEnum.SOMETHING_WENT_WRONG.ToString();
                return Page();
            }
        }

        private JwtSecurityToken parseToken(string jwtToken)
        {
            var stream = jwtToken;
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(stream) as JwtSecurityToken;
        }

        private async Task<HttpResponseMessage> GetLoginAPIResponse(string apiUrl)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            var data = new { UserName = User.UserName, Password = User.Password };
            var payload = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, stringContent);
            return response;
        }
    }
}
