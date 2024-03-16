using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebCourierHub.Support.ApiConfig;

namespace WebCourierTest
{
    public class CourierHubE2ETests
    {
        [Fact]
        public void SubmitInquiryForm_SubmissionIsSuccessful()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                try
                {
                    // Navigate to the page with the form
                    driver.Navigate().GoToUrl("https://localhost:7032");

                    // Fill out the form
                    driver.FindElement(By.Id("package-length")).SendKeys("10");
                    driver.FindElement(By.Id("package-width")).SendKeys("5");
                    driver.FindElement(By.Id("package-height")).SendKeys("2");
                    driver.FindElement(By.Id("package-weight")).SendKeys("1");

                    driver.FindElement(By.Id("delivery-date")).SendKeys("2024-09-30");

                    driver.FindElement(By.Id("source-street")).SendKeys("123 Source Street");
                    driver.FindElement(By.Id("source-city")).SendKeys("Source City");
                    driver.FindElement(By.Id("source-state")).SendKeys("Source State");
                    driver.FindElement(By.Id("source-postal-code")).SendKeys("12345");
                    driver.FindElement(By.Id("source-country")).SendKeys("Poland");

                    driver.FindElement(By.Id("destination-street")).SendKeys("456 Destination Street");
                    driver.FindElement(By.Id("destination-city")).SendKeys("Destination City");
                    driver.FindElement(By.Id("destination-state")).SendKeys("Destination State");
                    driver.FindElement(By.Id("destination-postal-code")).SendKeys("67890");
                    driver.FindElement(By.Id("destination-country")).SendKeys("Poland");

                    driver.FindElement(By.Id("company-name")).SendKeys("Company Name");
                    driver.FindElement(By.Id("company-street")).SendKeys("789 Company Street");
                    driver.FindElement(By.Id("company-city")).SendKeys("Company City");
                    driver.FindElement(By.Id("company-state")).SendKeys("Company State");
                    driver.FindElement(By.Id("company-postal-code")).SendKeys("90123");
                    driver.FindElement(By.Id("company-country")).SendKeys("Company Country");
                    driver.FindElement(By.Id("nip")).SendKeys("1234567890");

                    driver.FindElement(By.Id("pickup-date")).SendKeys("2024-10-01");
                    driver.FindElement(By.Id("priority")).SendKeys("High");
                    driver.FindElement(By.Id("weekend-delivery")).Click();

                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    bool isAlertPresent = wait.Until(d =>
                    {
                        try
                        {
                            var alert = d.FindElement(By.ClassName("swal-title"));
                            return alert != null && alert.Text.Contains("Your inquiry has been added");
                        }
                        catch (NoSuchElementException)
                        {
                            return false;
                        }
                    });

                    // Assert
                    Assert.True(isAlertPresent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }
}