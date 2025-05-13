using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Tests
{
    public class RegisterModelSignInSelenium : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private const string registerUrl = "https://localhost:7001";

        /*
         *   Message: 
                OpenQA.Selenium.UnknownErrorException : unknown error: net::ERR_CONNECTION_REFUSED
                 (Session info: chrome=136.0.7103.93)
        */
        public RegisterModelSignInSelenium()
        {

            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            //options.AddArgument("--remote-allow-origins=*");
            //options.AddArgument("--disable-infobars");
            //options.AddArgument("--disable-popup-blocking");
            //options.AddArgument("--disable-gpu");
            //options.AddArgument("--no-sandbox");
            //options.AddArgument("--disable-dev-shm-usage");

            //_driver = new ChromeDriver(options);
            _driver = new ChromeDriver(options);

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        //[Fact]
        public void Register_WithValidData_RedirectsAway()
        {
            _driver.Navigate().GoToUrl($"{registerUrl}/Identity/Account/Register");

            _driver.FindElement(By.Id("Input_FirstName")).SendKeys("TestUser");
            _driver.FindElement(By.Id("Input_LastName")).SendKeys("Smith");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Password123!");
            _driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Password123!");
            _driver.FindElement(By.Id("Input_City")).SendKeys("Test City");
            _driver.FindElement(By.Id("Input_UserHandle")).SendKeys("Anonymous" + new Random().Next(100, 500));
            _driver.FindElement(By.Id("Input_Birthdate")).SendKeys("04/10/1955");
            _driver.FindElement(By.Id("Input_SobrietyDate")).SendKeys("01/01/1970");

            _driver.FindElement(By.CssSelector("#registerForm button[type='submit']")).Click();

            _wait.Until(d => !d.Url.Contains("/Identity/Account/Register"));

            Assert.False(_driver.Url.EndsWith("/Identity/Account/Register",
                StringComparison.OrdinalIgnoreCase), "If successful, should not be on Register page.");
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
