using atf.toolbox;
using atf.toolbox.configuration;
using atf.toolbox.managers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace atf.unit.testing
{
    [TestClass]
    [Ignore]
    public class UnitTest_ATFHandler
    {
        [TestMethod]
        public void ChangeConfigTest()
        {
            System.Configuration.Configuration config =  ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            FormSettings frmSettings = (FormSettings)config.GetSection("FormSettings");
            frmSettings.ApplicationDirectory = "somethingNew";
            config.Save();
        }
        [TestMethod]
        public void ATFHandlerTest()
        {
            ATFHandler.Instance.Should().NotBeNull("because we expected ATFHandler to be instantiated.");
        }

        [TestMethod]
        [Ignore]
        // Not a unit test
        public void WebAutomationIsInstantiated()
        {
            ATFHandler.Instance.WebAutomation.Should().NotBeNull("because we expected the WebAutomation to be instantiated.");
        }

        [TestMethod]
        [Ignore]
        // not a unit test
        public void WebAutomationIsSingleton()
        {
            WebAutomationManager mgr1 = ATFHandler.Instance.WebAutomation;
            WebAutomationManager mgr2 = ATFHandler.Instance.WebAutomation;

            mgr1.Should().BeSameAs(mgr2, "we expected a single webautomationmanager.");
        }

        [TestMethod]
        //[Ignore]
        // This is not a unit test
        public void WebAutomationWebDriverStarts()
        { 
            ATFHandler.Instance.WebAutomation.WebDriver.Should().NotBeNull("we expected the webdriver to be instantiated.");
        }

        [TestMethod]
        //[Ignore]
        // This is not a unit test
        public void WebAutomationWebDriverQuits()
        {
            ATFHandler.Instance.WebAutomation.WebDriver.Navigate().GoToUrl("http://www.agiletestingframework.com");
            ATFHandler.Instance.TearDown();
        }

        [TestMethod]
        public void FormsAutomationApplicationQuits()
        {
            string name = ATFHandler.Instance.FormAutomation.ApplicationDriver.Name;
            ATFHandler.Instance.TearDown();
        }

        [TestMethod]
        public void NoConfigurationSectionForKey()
        {
            ATFHandler.Instance.Configuration("fakeKey").Should().BeNull("we expected no configuration section with the key to exist.");
        }

        [TestMethod]
        //[Ignore]
        // not a unit test
        public void FormAutomationIsSingleton()
        {
            FormAutomationManager mgr1 = ATFHandler.Instance.FormAutomation;
            FormAutomationManager mgr2 = ATFHandler.Instance.FormAutomation;

            mgr1.Should().BeSameAs(mgr2, "we expected a single formautomationmanager.");
        }

        [TestMethod]
        //[Ignore]
        // This is not a unit test
        public void MobileAutomationDriverQuits()
        {
            MobileAutomationManager mgr1 = ATFHandler.Instance.MobileAutomation;
            Thread.Sleep(TimeSpan.FromSeconds(10)); // Give enough time for the server to start fully
            Process[] process = Process.GetProcessesByName("Appium");
            Assert.IsTrue(process.Length > 0);

            ATFHandler.Instance.TearDown();
            Thread.Sleep(TimeSpan.FromSeconds(5)); // Give enough time for the server to quit fully
            process = Process.GetProcessesByName("Appium");
            Assert.IsFalse(process.Length > 0);

        }
    }
}
