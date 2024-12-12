using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParkingLibTests.Services
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class SeleniumUITest
    {
        private static IWebDriver driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            //Adds an option where the tests run without a browser opening
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(@"https://vueparkingtest-gubahjgadyavb6hq.northeurope-01.azurewebsites.net/");
        }

        [ClassCleanup]
        public static void TearDown()
        {

            driver.Quit();
        }

        [TestMethod]
        public void TestLoginButtonExist()
        {
            IWebElement button = driver.FindElement(By.Id("loginTest"));

            string actualBtnText = button.Text;

            Assert.AreEqual("Login", actualBtnText);

            //button.Click();
            // Assert.AreEqual(driver.Url, "https://www.selenium.dev/");
        }

    }
}
