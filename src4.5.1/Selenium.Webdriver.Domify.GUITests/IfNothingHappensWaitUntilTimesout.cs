using System;
using NUnit.Framework;

using Selenium.Webdriver.Domify.GUITests.Core;
using Selenium.Webdriver.Domify.GUITests.Pages;

namespace Selenium.Webdriver.Domify.GUITests
{
    [TestFixture]
    public class IfNothingHappensWaitUntilTimesout : BrowserScenario
    {
        protected override void Given()
        {
            Document.Navigation.GoTo<HomeIndex>();
        }

        protected override void When()
        {
            Document.Navigation.GetCurrentPage<HomeIndex>().DelayedTextBox.Value = "";
        }

        [Then]
        [ExpectedException(typeof(TimeoutException))]
        public void TimeoutExceptionShouldBeThrown()
        {
            Document.Navigation.GetCurrentPage<HomeIndex>().DelayedTextBox.WaitUntil(c => !string.IsNullOrEmpty(c.Value), TimeSpan.FromSeconds(3));
        }

    }
}