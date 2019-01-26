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

        public IWebDriver WebDriver { get; set; }
        public ITakesScreenshot TakesScreenShot { get { return ((ITakesScreenshot)WebDriver); } }

        /// <summary>
        /// WebDriverConfiguration
        /// Configuration section WebDriverSettings to be used to setup the webdriver for the test run
        /// </summary>
        public WebDriverSettings WebDriverConfiguration
        {
            get { return (WebDriverSettings)ConfigurationManager.GetSection("WebDriverSettings"); }
        }

        /// <summary>
        /// WebConfiguration
        /// Configuration section WebSettings
        /// </summary>
        public WebSettings WebConfiguration
        {
            get { return (WebSettings)ConfigurationManager.GetSection("WebSettings"); }
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
            IWebDriver driver = null;
            DriverOptions options = new FirefoxOptions();

            // Configure Candidate Browser Information
            String browserName = WebDriverConfiguration.BrowserName;
            switch (browserName.ToLower())
            {
                case "firefox":
                    OpenQA.Selenium.DriverOptions ffOptions = new FirefoxOptions();
                    ffOptions = SetCommonOptions(ffOptions);

                    if (WebDriverConfiguration.FirefoxBinary != string.Empty) {
                        //binary = new FirefoxBinary(WebDriverConfiguration.FirefoxBinary);
                        ((FirefoxOptions)ffOptions).BrowserExecutableLocation = WebDriverConfiguration.FirefoxBinary;
                    }

                    FirefoxProfile profile = null;
                    if (WebDriverConfiguration.BrowserDownloadPath != string.Empty) { profile = new FirefoxProfile(WebDriverConfiguration.BrowserDownloadPath); }

                    if (WebDriverConfiguration.FirefoxWebdriverAcceptUntrustedCerts != null) profile.SetPreference("webdriver_accept_untrusted_certs", WebDriverConfiguration.FirefoxWebdriverAcceptUntrustedCerts.Value);
                    if (WebDriverConfiguration.FirefoxWebdriverAssumeUntrustedIssuer != null) profile.SetPreference("webdriver_assume_untrusted_issuer", WebDriverConfiguration.FirefoxWebdriverAssumeUntrustedIssuer.Value);
                    if (WebDriverConfiguration.FirefoxWebdriverLogDriver != string.Empty) profile.SetPreference("webdriver.log.driver", WebDriverConfiguration.FirefoxWebdriverLogDriver);
                    if (WebDriverConfiguration.FirefoxWebdriverLogFile != string.Empty) profile.SetPreference("webdriver.log.file", WebDriverConfiguration.FirefoxWebdriverLogFile);
                    if (WebDriverConfiguration.FirefoxWebdriverLoadStrategy != string.Empty) profile.SetPreference("webdriver.load.strategy", WebDriverConfiguration.FirefoxWebdriverLoadStrategy);
                    if (WebDriverConfiguration.FirefoxWebdriverPort != null) profile.SetPreference("webdriver_firefox_port", WebDriverConfiguration.FirefoxWebdriverPort.Value);

                    ((FirefoxOptions)ffOptions).Profile = profile;

			        if (WebDriverConfiguration.FirefoxRCMode != null) ffOptions.AddAdditionalCapability("mode", WebDriverConfiguration.FirefoxRCMode.Value);
			        if (WebDriverConfiguration.FirefoxRCCaptureNetworkTraffic != null) ffOptions.AddAdditionalCapability("captureNetworkTraffic", WebDriverConfiguration.FirefoxRCCaptureNetworkTraffic.Value);
			        if (WebDriverConfiguration.FirefoxRCAddCustomRequestHeader != null) ffOptions.AddAdditionalCapability("addCustomRequestHeaders", WebDriverConfiguration.FirefoxRCAddCustomRequestHeader.Value);
			        if (WebDriverConfiguration.FirefoxTrustAllSSLCerts != null) ffOptions.AddAdditionalCapability("trustAllSSLCertificates", WebDriverConfiguration.FirefoxTrustAllSSLCerts.Value);

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        driver = new FirefoxDriver((FirefoxOptions)ffOptions);
			        }

                    break;
                    
                case "internetexplorer":
                    DriverOptions ieOptions = new InternetExplorerOptions();
                    ieOptions = SetCommonOptions(ieOptions);




			        if (WebDriverConfiguration.IEIgnoreProtectedModeSettings != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("ignoreProtectedModeSettings", WebDriverConfiguration.IEIgnoreProtectedModeSettings.Value);
			        if (WebDriverConfiguration.IEIgnoreZoomSetting != null) ((InternetExplorerOptions)ieOptions).IgnoreZoomLevel = WebDriverConfiguration.IEIgnoreZoomSetting.Value;
			        if (WebDriverConfiguration.IEInitialBrowserURL != string.Empty) ((InternetExplorerOptions)ieOptions).InitialBrowserUrl = WebDriverConfiguration.IEInitialBrowserURL;
			        if (WebDriverConfiguration.IEEnablePersistentHover != null) ((InternetExplorerOptions)ieOptions).EnablePersistentHover = WebDriverConfiguration.IEEnablePersistentHover.Value;
			        if (WebDriverConfiguration.IEEnableElementCacheCleanup != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("enableElementCacheCleanup", WebDriverConfiguration.IEEnableElementCacheCleanup.Value);
			        if (WebDriverConfiguration.IERequireWindowFocus != null) ((InternetExplorerOptions)ieOptions).RequireWindowFocus = WebDriverConfiguration.IERequireWindowFocus.Value;
			        if (WebDriverConfiguration.IEBrowserAttachTimeout != null) ((InternetExplorerOptions)ieOptions).BrowserAttachTimeout = TimeSpan.FromSeconds(WebDriverConfiguration.IEBrowserAttachTimeout.Value);
			        if (WebDriverConfiguration.IEForceCreateProcessAPI != null) ((InternetExplorerOptions)ieOptions).ForceCreateProcessApi = WebDriverConfiguration.IEForceCreateProcessAPI.Value;
			        if (WebDriverConfiguration.IEBrowserCMDLineSwitches != string.Empty) ((InternetExplorerOptions)ieOptions).BrowserCommandLineArguments = WebDriverConfiguration.IEBrowserCMDLineSwitches;
			        if (WebDriverConfiguration.IEUsePerProcessProxy != null) ((InternetExplorerOptions)ieOptions).UsePerProcessProxy = WebDriverConfiguration.IEUsePerProcessProxy.Value;
			        if (WebDriverConfiguration.IEEnsureCleanSession != null) ((InternetExplorerOptions)ieOptions).EnsureCleanSession = WebDriverConfiguration.IEEnsureCleanSession.Value;
			        if (WebDriverConfiguration.IELogFile !=string.Empty) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("logFile", WebDriverConfiguration.IELogFile);
			        if (WebDriverConfiguration.IELogLevel !=string.Empty) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("logLevel", WebDriverConfiguration.IELogLevel);
			        if (WebDriverConfiguration.IEHost !=string.Empty) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("host", WebDriverConfiguration.IEHost);
			        if (WebDriverConfiguration.IEExtractPath !=string.Empty) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("extractPath", WebDriverConfiguration.IEExtractPath);
			        if (WebDriverConfiguration.IESilent != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("silent", WebDriverConfiguration.IESilent.Value);
			        if (WebDriverConfiguration.IESetProxyByServer != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("ie.setProxyByServer", WebDriverConfiguration.IESetProxyByServer.Value);

			        if (WebDriverConfiguration.IERCMode !=string.Empty) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("mode", WebDriverConfiguration.IERCMode);
			        if (WebDriverConfiguration.IERCKillProcessesByName != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("killProcessesByName", WebDriverConfiguration.IERCKillProcessesByName.Value);
			        if (WebDriverConfiguration.IERCHonorSystemProxy != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("honorSystemProxy", WebDriverConfiguration.IERCHonorSystemProxy.Value);
			        if (WebDriverConfiguration.IERCEnsureCleanSession != null) ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("ensureCleanSession", WebDriverConfiguration.IERCEnsureCleanSession.Value);

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        if (WebDriverConfiguration.BrowserDownloadPath != string.Empty)
                        {
                            ((InternetExplorerOptions)ieOptions).AddAdditionalCapability("webdriver.ie.driver", WebDriverConfiguration.BrowserDownloadPath);
                            driver = new InternetExplorerDriver(WebDriverConfiguration.BrowserDownloadPath, ((InternetExplorerOptions)ieOptions));
                        }
                        else driver = new InternetExplorerDriver(((InternetExplorerOptions)ieOptions));
			        }
                    break;

                case "chrome":
                    DriverOptions chromeOptions = new ChromeOptions();
                    chromeOptions = SetCommonOptions(chromeOptions);

			        //ChromeOptions options = new ChromeOptions();
			        if (WebDriverConfiguration.ChromeOptionsArgs != string.Empty) ((ChromeOptions)chromeOptions).AddArguments(WebDriverConfiguration.ChromeOptionsArgs);
			        if (WebDriverConfiguration.ChromeOptionsBinary != string.Empty) ((ChromeOptions)chromeOptions).BinaryLocation = WebDriverConfiguration.ChromeOptionsBinary;
			        if (WebDriverConfiguration.ChromeOptionsExtentions != null)
			        {
				        List<String> fileExtensions = new List<String>();
				        foreach (String extension in WebDriverConfiguration.ChromeOptionsExtentions.Split(','))
				        {
					        if (extension != "")
					        {
						        fileExtensions.Add(extension);
					        }
				        }
                        ((ChromeOptions)chromeOptions).AddExtensions(fileExtensions);
			        }

			        if (WebDriverConfiguration.ChromeProxy !=string.Empty)
			        {
				        Proxy proxy = new Proxy();
				        proxy.HttpProxy = WebDriverConfiguration.ChromeProxy;
                        ((ChromeOptions)chromeOptions).AddAdditionalCapability("proxy", proxy);
			        }

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        if (WebDriverConfiguration.BrowserDownloadPath != string.Empty)
                        {
                            driver = new ChromeDriver(WebDriverConfiguration.BrowserDownloadPath, (ChromeOptions)chromeOptions);
                        }
                        else driver = new ChromeDriver((ChromeOptions)chromeOptions);
			        }
                    break;

                case "safari":
			        	SafariOptions safOptions = new SafariOptions();

                        if (WebDriverConfiguration.SafariUseCleanSession != null) safOptions.AddAdditionalCapability("ensureCleanSession", WebDriverConfiguration.SafariUseCleanSession.Value);

				        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
				        {
					        driver = new SafariDriver(safOptions);
				        }

                    break;
                default:
                    // Nothing matched above, Opera, HTMLUnit are both remote creations
                    break;

            }

            // We need to use a remote webdriver instead
            if (WebDriverConfiguration.UseGrid != null && WebDriverConfiguration.UseGrid.Value)
            {
                driver = new RemoteWebDriver(options);
            }

             _log.Info(string.Format("Using browser: {0}, with getCapabilities: {1}", driver.ToString(), ((RemoteWebDriver)driver).Capabilities.ToString()));

            ITimeouts timeouts = driver.Manage().Timeouts();
            timeouts.ImplicitWait = TimeSpan.FromSeconds(1);
            timeouts.AsynchronousJavaScript = TimeSpan.FromSeconds(AtfConstant.WAIT_TIME_LARGE);
            timeouts.PageLoad=TimeSpan.FromSeconds(AtfConstant.WAIT_TIME_EXTRA_LARGE);

            driver.Manage().Window.Maximize();

            // Return the newly created webdriver
            return driver;
        }

         

        private DriverOptions SetCommonOptions(DriverOptions options)
        {
            string browserName = WebDriverConfiguration.BrowserName;
            string version = WebDriverConfiguration.BrowserVersion;
            Platform platform = new Platform(PlatformType.Any);

            if (browserName == string.Empty) browserName = BrowserType.FIREFOX.ToString();
            if (version == string.Empty) version = platform.MajorVersion.ToString();
            if (WebDriverConfiguration.BrowserPlatform.Length > 0)
            {
                if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("win"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Windows));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("linux"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Linux));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("mac"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Mac));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("XP"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.XP));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("unix"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Unix));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("Vista"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Vista));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("android"))
                {
                    options.AddAdditionalCapability("platform", new Platform(PlatformType.Android));
                }
            }

            //options = new DesiredCapabilities(browserName, version, platform);

            if (WebDriverConfiguration.BrowserVersion != string.Empty) options.AddAdditionalCapability("browserVersion", WebDriverConfiguration.BrowserVersion);
            if (WebDriverConfiguration.TakesScreenshot != null) options.AddAdditionalCapability("takesScreenshot", WebDriverConfiguration.TakesScreenshot);
            if (WebDriverConfiguration.HandlesAlerts != null) options.AddAdditionalCapability("handlesAlerts", WebDriverConfiguration.HandlesAlerts);
            if (WebDriverConfiguration.CSSSelectorsEnabled != null) options.AddAdditionalCapability("cssSelectorsEnabled", WebDriverConfiguration.CSSSelectorsEnabled);
            if (WebDriverConfiguration.JavascriptEnabled != null) options.AddAdditionalCapability("javascriptEnabled", WebDriverConfiguration.JavascriptEnabled);
            if (WebDriverConfiguration.DatabaseEnabled != null) options.AddAdditionalCapability("databaseEnabled", WebDriverConfiguration.DatabaseEnabled);
            if (WebDriverConfiguration.LocationContextEnabled != null) options.AddAdditionalCapability("locationContextEnabled", WebDriverConfiguration.LocationContextEnabled);
            if (WebDriverConfiguration.ApplicationCacheEnabled != null) options.AddAdditionalCapability("applicationCacheEnabled", WebDriverConfiguration.ApplicationCacheEnabled);
            if (WebDriverConfiguration.BrowserConnectionEnabled != null) options.AddAdditionalCapability("browserConnectionEnabled", WebDriverConfiguration.BrowserConnectionEnabled);
            if (WebDriverConfiguration.WebStorageEnabled != null) options.AddAdditionalCapability("webStorageEnabled", WebDriverConfiguration.WebStorageEnabled);
            if (WebDriverConfiguration.AcceptSSLCerts != null) options.AddAdditionalCapability("acceptSslCerts", WebDriverConfiguration.AcceptSSLCerts);
            if (WebDriverConfiguration.Rotatable != null) options.AddAdditionalCapability("rotatable", WebDriverConfiguration.Rotatable);
            if (WebDriverConfiguration.NativeEvents != null) options.AddAdditionalCapability("nativeEvents", WebDriverConfiguration.NativeEvents);
            if (WebDriverConfiguration.UnexpectedAlertBehavior != null) options.AddAdditionalCapability("unexpectedAlertBehaviour", WebDriverConfiguration.UnexpectedAlertBehavior);
            if (WebDriverConfiguration.ElementScrollBehavior != null) options.AddAdditionalCapability("elementScrollBehavior", WebDriverConfiguration.ElementScrollBehavior);

            // JSON Proxy
            if (WebDriverConfiguration.JSONProxyType != string.Empty) options.AddAdditionalCapability("proxyType", WebDriverConfiguration.JSONProxyType);
            if (WebDriverConfiguration.JSONProxyAutoConfigURL != string.Empty) options.AddAdditionalCapability("proxyAutoconfigUrl", WebDriverConfiguration.JSONProxyAutoConfigURL);
            if (WebDriverConfiguration.JSONProxy != string.Empty) options.AddAdditionalCapability(WebDriverConfiguration.JSONProxy, WebDriverConfiguration.JSONProxy);
            if (WebDriverConfiguration.JSONSocksUsername != string.Empty) options.AddAdditionalCapability("socksUsername", WebDriverConfiguration.JSONSocksUsername);
            if (WebDriverConfiguration.JSONSocksPassword != string.Empty) options.AddAdditionalCapability("socksPassword", WebDriverConfiguration.JSONSocksPassword);
            if (WebDriverConfiguration.JSONNoProxy != null) options.AddAdditionalCapability("noProxy", WebDriverConfiguration.JSONNoProxy);
            if (WebDriverConfiguration.JSONLoggingComponent != string.Empty) options.AddAdditionalCapability("component", WebDriverConfiguration.JSONLoggingComponent);
            if (WebDriverConfiguration.RemoteWebdriverRemoteQuietExceptions != null) options.AddAdditionalCapability("webdriver.remote.quietExceptions", WebDriverConfiguration.RemoteWebdriverRemoteQuietExceptions);

            return options;
        }
    }
}
