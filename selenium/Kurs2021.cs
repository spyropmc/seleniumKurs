using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace seleniumKurs
{
    public class Kurs2021
    {
        ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            //uncomment for tests: CheckInput, AddFields
            /*
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            Thread.Sleep(2000);
            var xOnPopup = driver.FindElement(By.Id("at-cv-lightbox-close"));
            xOnPopup.Click();
            */
            //uncomment for test CheckCheckbox
            //driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-checkbox-demo.html");
            //uncomment fot test: ElementInElement
            //driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/table-pagination-demo.html");
            //uncomment for test: Dropdown
            //driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html");
            //uncomment for test: Dropdown2
            //driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/jquery-dropdown-search-demo.html");
            
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void CheckInput()
        {
            //arrange
            var testText = "blablabla";           
            var enterMessage = driver.FindElement(By.Id("user-message"));
            var showMessage = driver.FindElement(By.ClassName("btn-default"));
            //act
            enterMessage.SendKeys(testText);
            showMessage.Click();
            var text = driver.FindElement(By.Id("display")).Text;
            //assert
            Assert.AreEqual(testText, text);                    
        }
        [Test]
        public void AddFields()
        {
            //arrange
            driver.FindElement(By.Id("sum1")).SendKeys("2");
            driver.FindElement(By.Id("sum2")).SendKeys("3");
            driver.FindElements(By.ClassName("btn-default"))[1].Click();
            //act           
            var text = driver.FindElement(By.Id("displayvalue")).Text;
            //assert
            Assert.AreEqual("5", text);
        }
        [Test]
        public void CheckCheckbox()
        {
            //arrange
            var checkBox = driver.FindElementByXPath("//*[@id=\"easycont\"]/div/div[2]/div[2]/div[2]/div[1]/label/input");
            var checkBox2 = driver.FindElementByCssSelector("#easycont > div > div.col-md-6.text-left > div:nth-child(5) > div.panel-body > div:nth-child(4) > label > input");
            var selenumLink = driver.FindElementByLinkText("Selenium Easy");
            //act
            checkBox.Click();
            checkBox2.Click();
            selenumLink.Click();
            //assert
            Assert.AreEqual("Learn Selenium with Best Practices and Examples | Selenium Easy", driver.Title);
        }
        [Test]
        public void ElementInElement()
        {
            //arrange
            var table = driver.FindElement(By.ClassName("table-responsive"));
            var row = table.FindElements(By.TagName("tr"));
            var cell = row[1].FindElements(By.TagName("td"));
            //act
            var secondCell = cell[1];
            //assert
            Assert.AreEqual("Table cell", secondCell.Text);
        }    
        [Test]
         public void Dropdown()
        {
            //arrange
            var SelectSelector = driver.FindElement(By.Id("select-demo"));
            var SelectADay = new SelectElement(SelectSelector);
            //act
            SelectADay.SelectByIndex(2);
            SelectADay.SelectByText("Tuesday");
            SelectADay.SelectByValue("Friday");
            var selectedValue1 = SelectADay.SelectedOption;
            var selectValue = driver.FindElement(By.ClassName("selected-value"));
            var selectedValueEdited = selectValue.Text.Substring(16);
            //assert
            Assert.AreEqual("Friday", selectedValueEdited);
        }
        [Test]
        public void Dropdown2()
        {
            //arrange
            var japanString = "Japan";
            //act
            var dropdown = driver.FindElement(By.ClassName("selection"));
            dropdown.Click();
            var countries = driver.FindElement(By.ClassName("select2-results__options")).FindElements(By.TagName("li"));
            var japan = countries.Where(d => d.Text == japanString).FirstOrDefault();
            japan.Click();
            var selectedCountry = dropdown.Text;
            //assert
            Assert.AreEqual(japanString, selectedCountry);
            Assert.IsTrue(true, japanString = selectedCountry);
            Assert.That(japanString == selectedCountry);

        }
        [Test]
        public void ImplicitWait()
        {
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            //arrange
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(d => d.FindElement(By.Id("at-cv-lightbox-close")));
            //act
            var xOnPopup = driver.FindElement(By.Id("at-cv-lightbox-close"));
            xOnPopup.Click();
        }
        [Test]
        public void DragAndDrop()
        {
            driver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/draganddrop/");
            //arrange
            var frame = driver.FindElement(By.ClassName("demo-frame"));
            driver.SwitchTo().Frame(frame);
            var image1 = driver.FindElements(By.ClassName("ui-draggable")).FirstOrDefault();
            var trash = driver.FindElement(By.Id("trash"));
            //act
            var action = new Actions(driver);
            action.DragAndDrop(image1, trash).Build().Perform();
            //assert
            var trashAfterMove = trash.FindElements(By.TagName("img"));
            Assert.That(trashAfterMove.Count == 1);
            driver.SwitchTo().DefaultContent();
        }
        [Test]
        public void NewTab()
        {
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/table-pagination-demo.html");
            //arrange
            var link = driver.FindElement(By.ClassName("cbt"));
            link.Click();
            //act
            var windowHandlesAfter = driver.WindowHandles;
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            var titleAfter = driver.Title;
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            var titleSecond = driver.Title;
            //assert
            Assert.AreEqual("Cross Browser Testing Tool: 2050+ Real Browsers & Devices", titleAfter);
            Assert.AreEqual("Selenium Easy - Table with Pagination Demo", driver.Title);
        }
    }
}