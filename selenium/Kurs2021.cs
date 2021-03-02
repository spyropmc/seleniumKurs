using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;

namespace seleniumKurs
{
    public class Kurs2021
    {
        ChromeDriver driver;

        [Test]

        public void CheckInput()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("at-cv-lightbox-close")).Click();
            driver.FindElement(By.Id("user-message")).SendKeys("blablabla");
            driver.FindElement(By.ClassName("btn-default")).Click();

            var text = driver.FindElement(By.Id("display")).Text;

            Assert.AreEqual("blablabla", text);

            driver.Quit();
        }
        [Test]

        public void AddFields()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("at-cv-lightbox-close")).Click();
            driver.FindElement(By.Id("sum1")).SendKeys("2");
            driver.FindElement(By.Id("sum2")).SendKeys("3");
            driver.FindElements(By.ClassName("btn-default"))[1].Click();

            var text = driver.FindElement(By.Id("displayvalue")).Text;

            Assert.AreEqual("5", text);

            driver.Quit();
        }
    }
}