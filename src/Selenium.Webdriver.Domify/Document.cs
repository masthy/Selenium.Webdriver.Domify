using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using Selenium.Webdriver.Domify.Elements;

namespace Selenium.Webdriver.Domify
{
    public interface IDocument : ISearchContext
    {
        Uri Uri { get; }
        IWebElement Body { get; }
        IWebDriver Driver { get; }
        IList<Span> Spans { get; }
        IList<Frame> Frames { get; }
        IList<Div> Divs { get; }
        IList<HyperLink> Links { get; }
        IList<Table> Tables { get; }
        IList<CheckBox> CheckBoxes { get; }
        IEnumerable<IWebElement> ElementsWithTag(string tagName);
        string PageSource { get; }
        INavigationService Navigation { get; }
        void ClearCache();

    }

    public interface INavigationService
    {
        IDocument Document { get; }
    }

    public class NavigationService : INavigationService
    {
        public IDocument Document { get; private set; }

        internal NavigationService(IDocument document)
        {
            Document = document;
        }
    }

    public class Document : IDocument
    {
        private readonly IWebDriver _driver;

        public Document(IWebDriver driver)
        {
            _driver = driver;
        }

        public Uri Uri
        {
            get
            {
                return new Uri(_driver.Url);
            }
        }

        public IWebElement Body
        {
            get { return _driver.FindElement(By.TagName("Body")); }
        }

        public IList<Span> Spans
        {
            get { return _driver.FindElements(By.TagName("span")).Select(Span.Create).ToList(); }
        }

        public IList<Frame> Frames
        {
            get { return _driver.FindElements(By.TagName("iframe")).Select(Frame.Create).ToList(); }
        }
        public IList<Div> Divs
        {
            get { return _driver.FindElements(By.TagName("div")).Select(Div.Create).ToList(); }
        }

        public IList<HyperLink> Links
        {
            get { return _driver.FindElements(By.TagName("a")).Select(HyperLink.Create).ToList(); }
        }

        public IList<Table> Tables
        {
            get { return _driver.FindElements(By.TagName("table")).Select(Table.Create).ToList(); }
        }

        public IList<CheckBox> CheckBoxes
        {
            get { return _driver.FindElements(By.TagName("input")).Where(i => i.GetAttribute("type").Equals("checkbox")).Select(CheckBox.Create).ToList(); }
        }

        public string PageSource
        {
            get { return _driver.PageSource; }

        }

        public INavigationService Navigation
        {
            get
            {
                return new NavigationService(this);
            }
        }

        public IWebDriver Driver
        {
            get { return _driver; }
        }

        public void ClearCache()
        {

        }

        public IEnumerable<IWebElement> ElementsWithTag(string tagName)
        {
            return _driver.FindElements(By.TagName(tagName));
        }

        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);

        }

        public IWebElement FindElement(By @by)
        {
            return _driver.FindElement(@by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return _driver.FindElements(@by);
        }
    }
}