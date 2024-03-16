using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebCourierHub.Support.ApiConfig;
using Xunit;

namespace WebCourierTest
{
    public class NewsletterSubscriptionTest
    {
        [Fact]
        public void SubscribeToNewsletter_FormSubmission()
        {
            using (var driver = new ChromeDriver())
            {
                try
                {
                    // Navigate to the page with the form
                    driver.Navigate().GoToUrl("https://localhost:7032");

                    driver.FindElement(By.Id("newsletter-email")).SendKeys("user@example.com");
                    driver.FindElement(By.Id("subscribe-button")).Click(); var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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