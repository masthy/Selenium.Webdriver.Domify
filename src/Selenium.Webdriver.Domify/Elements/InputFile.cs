using OpenQA.Selenium;
using Selenium.Webdriver.Domify.Attributes;
using Selenium.Webdriver.Domify.Core;

namespace Selenium.Webdriver.Domify.Elements
{
    [DOMElement("input", Type="file")]
    public class InputFile : WebElement
    {

        public string File
        {
            get { return GetAttributeValue("value"); }
            set{ SendKeys(value);}
        }
    }
}