using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Configuration.ConfigurationBuilders;

namespace EnvVariablesV4._8.Controllers
{
    public class HomeController : Controller
    {
        public async Task<string> GetKeyVault()
        {
            var output = string.Empty;
            try
            {
                var appsettings = ConfigurationManager.AppSettings;
                //AzureKeyVaultConfigBuilder test = new AzureKeyVaultConfigBuilder("https://kvmedianonprodwestus3.vault.azure.net/",new DefaultAzureCredential());
 //string userAssignedClientId = "6eba1142-4b68-48a6-88cf-dc8d12a83fdf";
                string userAssignedClientId = appsettings["UserAssignedClientId"];
              
                var client = new SecretClient(new Uri(appsettings["KeyVault_URI"]), new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId }));
              
                KeyVaultSecret secret = await client.GetSecretAsync(appsettings["SecretName"]);
                output = secret.Value;
            }
            catch (AuthenticationFailedException e)
            {
                output = e.Message;
            }
            return output;
        }
        public async Task<ActionResult> Index()
        {
            // Create a secret client using the DefaultAzureCredential
           
            
                var appsettings = ConfigurationManager.AppSettings;
                ViewBag.SMTP_URL = appsettings["SMTP_URL"];
                ViewBag.SMTP_EMAIL = appsettings["SMTP_EMAIL"];
                ViewBag.URI = appsettings["uri"];
                
                
               ViewBag.Secret= await GetKeyVault();
            
            
            
           // ViewBag.Secret = appsettings["ACOM--DSN--AstrologyDotCom"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}