using atf.toolbox.configuration;
using JSErrorCollector;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace atf.toolbox.managers
{
    public class WebAutomationManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(WebAutomationManager));

        public IWebDriver WebDriver {get; set;}

        /// <summary>
        /// WebDriverConfiguration
        /// Configuration section WebDriverSettings to be used to setup the webdriver for the test run
        /// </summary>
        public WebDriverSettings WebDriverConfiguration
        {
            get { return (WebDriverSettings)ConfigurationManager.GetSection("WebDriverSettings"); }
        }

        public WebAutomationManager()
        {
            WebDriver = WebDriverSetup();
        }

        public void TearDown()
        {
            if (WebDriver != null)
            {
                // Shut down the webdriver
                _log.Info("Webdriver teardown started.");
                WebDriver.Quit();
                WebDriver = null;
                _log.Info("Webdriver teardown complete.");
            }
        }

        /// <summary>
        /// WebDriverSetup
        /// Loads and sets up the webdriver based on configuration
        /// </summary>
        /// <returns>webDriver</returns>
        private IWebDriver WebDriverSetup()
        {
            // Configure Candidate Browser Information
            string browserName = WebDriverConfiguration.BrowserName;
            Platform platform;
            string version = WebDriverConfiguration.BrowserVersion;

            DesiredCapabilities capabilities;

            if (browserName == string.Empty) browserName = BrowserType.FIREFOX.ToString();

            switch (WebDriverConfiguration.BrowserPlatform.ToLower())
            {
                case "android":
                    platform = new Platform(PlatformType.Android);
                    break;
                case "linux":
                    platform = new Platform(PlatformType.Linux);
                    break;
                case "windows":
                    platform = new Platform(PlatformType.Windows);
                    break;
                default:
                    platform = new Platform(PlatformType.Any);
                    break;
            }

            if (version == string.Empty) version = platform.MajorVersion.ToString();

            IWebDriver webDriver = null;

            capabilities = new DesiredCapabilities(browserName, version, platform);
            capabilities.SetCapability("nativeEvents", false);
            capabilities.SetCapability(CapabilityType.TakesScreenshot, true);

            if (WebDriverConfiguration.UseGrid)
            {
                webDriver = CreateRemoteWebDriver(capabilities);
            }
            else
            {
                webDriver = CreateLocalWebDriver(capabilities);
            }

            _log.Info(string.Format("Using browser: {0}, with getCapabilities: {1}", webDriver.ToString(), ((RemoteWebDriver)webDriver).Capabilities.ToString()));

            ITimeouts timeouts = webDriver.Manage().Timeouts();
            timeouts.ImplicitlyWait(TimeSpan.FromSeconds(1));
            timeouts.SetScriptTimeout(TimeSpan.FromSeconds(AtfConstant.WAIT_TIME_LARGE));
            timeouts.SetPageLoadTimeout(TimeSpan.FromSeconds(AtfConstant.WAIT_TIME_EXTRA_LARGE));

            webDriver.Manage().Window.Maximize();

            // Return the newly created webdriver
            return webDriver;
        }

        /// <summary>
        /// CreateRemoteWebDriver
        /// </summary>
        /// <param name="capabilities"></param>
        /// <returns>instance of the webdriver created</returns>
        private IWebDriver CreateRemoteWebDriver(ICapabilities capabilities)
        {
            IWebDriver remoteWebDriver = null;

            try
            {
                if (WebDriverConfiguration.GridURL == string.Empty) throw new Exception("Missing Grid URL for remote webdriver.");

                remoteWebDriver = new RemoteWebDriver(new Uri(WebDriverConfiguration.GridURL), capabilities);
            }
            catch (Exception ex)
            {
                _log.Error("Error creating REMOTE webdriver :" + capabilities.BrowserName, ex);
            }

            return remoteWebDriver;
        }

        /// <summary>
        /// CreateLocalWebDriver
        /// </summary>
        /// <param name="capabilities"></param>
        /// <returns>instance of the webdriver created</returns>
        private IWebDriver CreateLocalWebDriver(ICapabilities capabilities)
        {
            IWebDriver localwebDriver = null;

            // Configure Candidate Browser Information
            string browserName = WebDriverConfiguration.BrowserName;

            if (browserName == string.Empty) browserName = BrowserType.FIREFOX.ToString();

            switch (browserName.ToLower())
            {
                case "firefox":
                    FirefoxBinary ffBinary = new FirefoxBinary(WebDriverConfiguration.DriverPath);
                    FirefoxProfile fireFoxProfile = new FirefoxProfile(WebDriverConfiguration.BrowserDownloadPath);
                    localwebDriver = new FirefoxDriver(ffBinary, fireFoxProfile);

                    capabilities = (DesiredCapabilities)((FirefoxDriver)localwebDriver).Capabilities;
                    fireFoxProfile.EnableNativeEvents = false;

                    // Reporting
                    fireFoxProfile.SetPreference("browser.download.folderList", 2);
                    fireFoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);

                    fireFoxProfile.SetPreference("browser.download.dir", WebDriverConfiguration.BrowserDownloadPath);
                    fireFoxProfile.SetPreference("browser.download.defaultFolder", WebDriverConfiguration.BrowserDownloadPath);
                    fireFoxProfile.SetPreference("browser.download.downloadDir", WebDriverConfiguration.BrowserDownloadPath);

                    String pdfMimeTypes = "application/pdf,application/pdfp,application/vnd.adobe.xfdf,application/vnd.fdf,application/vnd.adobe.xdp+xml";
                    String csvMimeTypes = "text/comma-separated-values,text/csv,application/csv,application/excel,application/vnd.ms-excel,application/vnd.msexcel,text/anytext,application/octet-stream,application/download";
                    fireFoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", pdfMimeTypes + ", " + csvMimeTypes);

                    fireFoxProfile.SetPreference("pdfjs.disabled", true);
                    fireFoxProfile.SetPreference("plugin.disable_full_page_plugin_for_types", pdfMimeTypes);
                    fireFoxProfile.SetPreference("plugin.scan.plid.all", false);
                    fireFoxProfile.SetPreference("plugin.scan.Acrobat", "99.0");

                    fireFoxProfile.SetPreference("accessibility.accesskeycausesactivation", false);
                    fireFoxProfile.SetPreference("plugin.importedState", true);

                    if (WebDriverConfiguration.UseFireBug)
                    {
                        try
                        {
                            fireFoxProfile.AddExtension(WebDriverConfiguration.FirebugFile);
                            fireFoxProfile.SetPreference("extensions.firebug.currentVersion", "2.0.4");
                            fireFoxProfile.SetPreference("extensions.firebug.console.enableSites", true);
                            fireFoxProfile.SetPreference("extensions.firebug.commandEditor", true);
                            fireFoxProfile.SetPreference("extensions.firebug.net.enableSites", true);
                        }
                        catch (IOException e)
                        {
                            _log.Warn("Unable to locate FireBug Plugin into browser. Proceeding without FireBug.", e);
                        }
                        catch (NullReferenceException nre)
                        {
                            _log.Warn("Unable to load FireBug Plugin into browser. Missing or bad profile configuration. Proceeding without FireBug.", nre);
                        }
                        catch (Exception e)
                        {
                            _log.Warn("Unable to load FireBug Plugin into browser. Proceeding without FireBug. Unknown exception: ", e);
                        }
                    }

                    break;

                case "internetexplorer":
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.AddAdditionalCapability("webdriver.ie.driver", WebDriverConfiguration.DriverPath + WebDriverConfiguration.WebDriverBrowser);

                    if (WebDriverConfiguration.BrowserProxy != null && WebDriverConfiguration.BrowserProxy != string.Empty)
                    {
                        ieOptions.AddAdditionalCapability(CapabilityType.Proxy, WebDriverConfiguration.BrowserProxy);
                    }

                    localwebDriver = new InternetExplorerDriver(WebDriverConfiguration.DriverPath, ieOptions);
                    break;

                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();

                    if (WebDriverConfiguration.BrowserProxy != null && WebDriverConfiguration.BrowserProxy != string.Empty)
                    {
                        chromeOptions.AddAdditionalCapability(CapabilityType.Proxy, WebDriverConfiguration.BrowserProxy);
                    }

                    localwebDriver = new ChromeDriver(WebDriverConfiguration.DriverPath, chromeOptions);
                    break;

                case "safari":
                    SafariOptions safOptions = new SafariOptions();
                    safOptions.AddAdditionalCapability("webdriver.safari.driver", WebDriverConfiguration.DriverPath + WebDriverConfiguration.WebDriverBrowser);

                    if (WebDriverConfiguration.BrowserProxy != null && WebDriverConfiguration.BrowserProxy != string.Empty)
                    {
                        safOptions.AddAdditionalCapability(CapabilityType.Proxy, WebDriverConfiguration.BrowserProxy);
                    }

                    localwebDriver = new SafariDriver(safOptions);
                    break;

                default:
                    // Nothing matched above, Opera, HTMLUnit are both remote creations
                    localwebDriver = CreateRemoteWebDriver(capabilities);
                    break;

            }

            return localwebDriver;
        }
    }
}
