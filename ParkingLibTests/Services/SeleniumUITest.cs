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
            //TearDown();

            //button.Click();
            // Assert.AreEqual(driver.Url, "https://www.selenium.dev/");
        }
        [TestMethod]
        public void TestLogudButtonExist()
        {
            IWebElement input1 = driver.FindElement(By.Id("brugernavnTest"));
            input1.SendKeys("Admin");

            IWebElement input2 = driver.FindElement(By.Id("passwordTest"));
            input2.SendKeys("123");

            IWebElement button = driver.FindElement(By.Id("loginTest"));
            button.Click();

            IWebElement buttonLogOut = driver.FindElement(By.Id("logudTest"));

            string actualBtnText = buttonLogOut.Text;
            Assert.AreEqual("Log ud", actualBtnText);

        }


        [TestMethod]
        public void TestSeParkeringerButtonExist()
        {
            IWebElement input1 = driver.FindElement(By.Id("brugernavnTest"));
            input1.SendKeys("Admin");

            IWebElement input2 = driver.FindElement(By.Id("passwordTest"));
            input2.SendKeys("123");

            IWebElement button = driver.FindElement(By.Id("loginTest"));
            button.Click();

            IWebElement seTidligereParkering = driver.FindElement(By.Id("seTidligereParkeringerTest"));

            string actualBtnText = seTidligereParkering.Text;
            Assert.AreEqual("Se tidligere parkeringer", actualBtnText);
        }


    }
}
