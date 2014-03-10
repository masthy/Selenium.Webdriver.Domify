﻿using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.Webdriver.Domify.Attributes;
using Selenium.Webdriver.Domify.Core;

namespace Selenium.Webdriver.Domify.Elements
{
    [DOMElement("select")]
    public class SelectList : WebElement
    {
        private SelectElement _wrappedSelectElement;

        public IList<Option> Options
        {
            get { return this.SelectListItems(); }
        }

        protected override void Created(IWebElement element)
        {
            _wrappedSelectElement = new SelectElement(element);
        }

        public IWebElement SelectedOption
        {
            get { return _wrappedSelectElement.SelectedOption; }
        }

        public void SelectByText(string textOfOptionToSelect)
        {
            _wrappedSelectElement.SelectByText(textOfOptionToSelect);
        }

        public void SelectByValue(string valueOfOptionToSelect)
        {
            _wrappedSelectElement.SelectByValue(valueOfOptionToSelect);
        }
        public void SelectByIndex(int indexOfOptionToSelect)
        {
            _wrappedSelectElement.SelectByIndex(indexOfOptionToSelect);
        }
    }
}