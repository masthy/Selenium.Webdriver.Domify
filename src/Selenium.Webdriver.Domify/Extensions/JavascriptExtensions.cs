﻿using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using Selenium.Webdriver.Domify.Elements;

namespace Selenium.Webdriver.Domify
{
    public static class JavascriptExtensions
    {
        public static object ExecuteJavascript(this IWebElement element, string script)
        {
            return ((IWrapsDriver)element).WrappedDriver.ExecuteJavascript(script);
        }

        public static string GetElementXPath(this IWebDriver driver, IWebElement element) {
            SetIdForElementAtPoint(driver, element.Location, GenerateRandomId());

            var result = driver.ExecuteJavascript("gPt=function(c){	if(c.id!=='')	{		return'id(\"'+c.id+'\")'	}	if(c===document.body)	{		return 'HTML/'+c.tagName	}	var a=0;	var e=c.parentNode.childNodes;	for(var b=0;b<e.length;b++)	{		var d=e[b];		if(d===c)		{			return gPt(c.parentNode)+'/'+c.tagName+'['+(a+1)+']'		}		if(d.nodeType===1&&d.tagName===c.tagName)		{			a++		}	}};return gPt(arguments[0]);", ((WebElement)element).SeleniumElement);

            return (string) result;
        }

        private static string GenerateRandomId()
        {
            Random random = new Random();
            var idNumber = random.Next(1000000);
            string id = string.Format("___s_w_d_{0}", idNumber);
            return id;
        }

        public static void SetIdForElementAtPoint(this IWebDriver driver, Point location, string id)
        {
            string javascript = string.Format("document.elementFromPoint({0},{1}).id = '{2}'", location.X, location.Y, id);
            driver.ExecuteJavascript(javascript);
        }

        public static object ExecuteJavascript(this IWebDriver driver, string script, params object[] arguments)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, arguments);
        }

        public static void TriggerJavascriptEvent(this IWebDriver driver, string elementId, string eventName)
        {
            driver.ExecuteJavascript(string.Format("var element = document.getElementById('{0}');" +
                                                   " if ('createEvent' in document) " +
                                                   "{{ " +
                                                   "var evt = document.createEvent('HTMLEvents'); " +
                                                   "evt.initEvent('{1}', false, true);" +
                                                   "element.dispatchEvent(evt);" +
                                                   "}}" +
                                                   "else element.fireEvent('on{1}');", elementId, eventName));
        }

        public static void TriggerJavascriptChange(this IWebDriver driver, string elementId)
        {
            driver.TriggerJavascriptEvent(elementId, "keydown");
            driver.TriggerJavascriptEvent(elementId, "keyup");
            driver.TriggerJavascriptEvent(elementId, "keypress");
            driver.TriggerJavascriptEvent(elementId, "change");
        }
    }

    
}