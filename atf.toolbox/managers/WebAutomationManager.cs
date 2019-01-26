using atf.toolbox.configuration;
using JSErrorCollector;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
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

            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();

            // Configure Candidate Browser Information
            String browserName = WebDriverConfiguration.BrowserName;
            switch (browserName.ToLower())
            {
                case "firefox":
                    capabilities = DesiredCapabilities.Firefox();
                    capabilities = SetCommonCapabilities(capabilities);

                    FirefoxBinary binary = null;
                    if (WebDriverConfiguration.FirefoxBinary != string.Empty) { binary = new FirefoxBinary(WebDriverConfiguration.FirefoxBinary); }

			        FirefoxProfile profile = null;
                    if (WebDriverConfiguration.BrowserDownloadPath != string.Empty) { profile = new FirefoxProfile(WebDriverConfiguration.BrowserDownloadPath); }

                    if (WebDriverConfiguration.FirefoxWebdriverAcceptUntrustedCerts != null) profile.SetPreference("webdriver_accept_untrusted_certs", WebDriverConfiguration.FirefoxWebdriverAcceptUntrustedCerts.Value);
                    if (WebDriverConfiguration.FirefoxWebdriverAssumeUntrustedIssuer != null) profile.SetPreference("webdriver_assume_untrusted_issuer", WebDriverConfiguration.FirefoxWebdriverAssumeUntrustedIssuer.Value);
                    if (WebDriverConfiguration.FirefoxWebdriverLogDriver != string.Empty) profile.SetPreference("webdriver.log.driver", WebDriverConfiguration.FirefoxWebdriverLogDriver);
                    if (WebDriverConfiguration.FirefoxWebdriverLogFile != string.Empty) profile.SetPreference("webdriver.log.file", WebDriverConfiguration.FirefoxWebdriverLogFile);
                    if (WebDriverConfiguration.FirefoxWebdriverLoadStrategy != string.Empty) profile.SetPreference("webdriver.load.strategy", WebDriverConfiguration.FirefoxWebdriverLoadStrategy);
			        if (WebDriverConfiguration.FirefoxWebdriverPort != null) profile.SetPreference("webdriver_firefox_port", WebDriverConfiguration.FirefoxWebdriverPort.Value);
			        capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, profile);

			        if (WebDriverConfiguration.FirefoxRCMode != null) capabilities.SetCapability("mode", WebDriverConfiguration.FirefoxRCMode.Value);
			        if (WebDriverConfiguration.FirefoxRCCaptureNetworkTraffic != null) capabilities.SetCapability("captureNetworkTraffic", WebDriverConfiguration.FirefoxRCCaptureNetworkTraffic.Value);
			        if (WebDriverConfiguration.FirefoxRCAddCustomRequestHeader != null) capabilities.SetCapability("addCustomRequestHeaders", WebDriverConfiguration.FirefoxRCAddCustomRequestHeader.Value);
			        if (WebDriverConfiguration.FirefoxTrustAllSSLCerts != null) capabilities.SetCapability("trustAllSSLCertificates", WebDriverConfiguration.FirefoxTrustAllSSLCerts.Value);

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        if (binary !=null || profile != null) {driver = new FirefoxDriver(binary, profile);}
                        else {driver = new FirefoxDriver(capabilities);}
			        }

                    break;
                    
                case "internetexplorer":
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    capabilities = DesiredCapabilities.InternetExplorer();
			        capabilities = SetCommonCapabilities(capabilities);




			        if (WebDriverConfiguration.IEIgnoreProtectedModeSettings != null) ieOptions.AddAdditionalCapability("ignoreProtectedModeSettings", WebDriverConfiguration.IEIgnoreProtectedModeSettings.Value);
			        if (WebDriverConfiguration.IEIgnoreZoomSetting != null) ieOptions.IgnoreZoomLevel = WebDriverConfiguration.IEIgnoreZoomSetting.Value;
			        if (WebDriverConfiguration.IEInitialBrowserURL != string.Empty) ieOptions.InitialBrowserUrl = WebDriverConfiguration.IEInitialBrowserURL;
			        if (WebDriverConfiguration.IEEnablePersistentHover != null) ieOptions.EnablePersistentHover = WebDriverConfiguration.IEEnablePersistentHover.Value;
			        if (WebDriverConfiguration.IEEnableElementCacheCleanup != null) ieOptions.AddAdditionalCapability("enableElementCacheCleanup", WebDriverConfiguration.IEEnableElementCacheCleanup.Value);
			        if (WebDriverConfiguration.IERequireWindowFocus != null) ieOptions.RequireWindowFocus = WebDriverConfiguration.IERequireWindowFocus.Value;
			        if (WebDriverConfiguration.IEBrowserAttachTimeout != null) ieOptions.BrowserAttachTimeout = TimeSpan.FromSeconds(WebDriverConfiguration.IEBrowserAttachTimeout.Value);
			        if (WebDriverConfiguration.IEForceCreateProcessAPI != null) ieOptions.ForceCreateProcessApi = WebDriverConfiguration.IEForceCreateProcessAPI.Value;
			        if (WebDriverConfiguration.IEBrowserCMDLineSwitches != string.Empty) ieOptions.BrowserCommandLineArguments = WebDriverConfiguration.IEBrowserCMDLineSwitches;
			        if (WebDriverConfiguration.IEUsePerProcessProxy != null) ieOptions.UsePerProcessProxy = WebDriverConfiguration.IEUsePerProcessProxy.Value;
			        if (WebDriverConfiguration.IEEnsureCleanSession != null) ieOptions.EnsureCleanSession = WebDriverConfiguration.IEEnsureCleanSession.Value;
			        if (WebDriverConfiguration.IELogFile !=string.Empty) ieOptions.AddAdditionalCapability("logFile", WebDriverConfiguration.IELogFile);
			        if (WebDriverConfiguration.IELogLevel !=string.Empty) ieOptions.AddAdditionalCapability("logLevel", WebDriverConfiguration.IELogLevel);
			        if (WebDriverConfiguration.IEHost !=string.Empty) ieOptions.AddAdditionalCapability("host", WebDriverConfiguration.IEHost);
			        if (WebDriverConfiguration.IEExtractPath !=string.Empty) ieOptions.AddAdditionalCapability("extractPath", WebDriverConfiguration.IEExtractPath);
			        if (WebDriverConfiguration.IESilent != null) ieOptions.AddAdditionalCapability("silent", WebDriverConfiguration.IESilent.Value);
			        if (WebDriverConfiguration.IESetProxyByServer != null) ieOptions.AddAdditionalCapability("ie.setProxyByServer", WebDriverConfiguration.IESetProxyByServer.Value);

			        if (WebDriverConfiguration.IERCMode !=string.Empty) ieOptions.AddAdditionalCapability("mode", WebDriverConfiguration.IERCMode);
			        if (WebDriverConfiguration.IERCKillProcessesByName != null) ieOptions.AddAdditionalCapability("killProcessesByName", WebDriverConfiguration.IERCKillProcessesByName.Value);
			        if (WebDriverConfiguration.IERCHonorSystemProxy != null) ieOptions.AddAdditionalCapability("honorSystemProxy", WebDriverConfiguration.IERCHonorSystemProxy.Value);
			        if (WebDriverConfiguration.IERCEnsureCleanSession != null) ieOptions.AddAdditionalCapability("ensureCleanSession", WebDriverConfiguration.IERCEnsureCleanSession.Value);

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        if (WebDriverConfiguration.BrowserDownloadPath != string.Empty)
                        {
                            ieOptions.AddAdditionalCapability("webdriver.ie.driver", WebDriverConfiguration.BrowserDownloadPath);
                            driver = new InternetExplorerDriver(WebDriverConfiguration.BrowserDownloadPath, ieOptions);
                        }
                        else driver = new InternetExplorerDriver(ieOptions);
			        }
                    break;

                case "chrome":
                    capabilities = DesiredCapabilities.Chrome();
			        capabilities = SetCommonCapabilities(capabilities);

			        ChromeOptions options = new ChromeOptions();
			        if (WebDriverConfiguration.ChromeOptionsArgs != string.Empty) options.AddArguments(WebDriverConfiguration.ChromeOptionsArgs);
			        if (WebDriverConfiguration.ChromeOptionsBinary != string.Empty) options.BinaryLocation = WebDriverConfiguration.ChromeOptionsBinary;
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
				        options.AddExtensions(fileExtensions);
			        }
			        capabilities.SetCapability(ChromeOptions.Capability, options);

			        if (WebDriverConfiguration.ChromeProxy !=string.Empty)
			        {
				        Proxy proxy = new Proxy();
				        proxy.HttpProxy = WebDriverConfiguration.ChromeProxy;
				        capabilities.SetCapability("proxy", proxy);
			        }

			        if (WebDriverConfiguration.UseGrid != null && !WebDriverConfiguration.UseGrid.Value)
			        {
                        if (WebDriverConfiguration.BrowserDownloadPath != string.Empty)
                        {
                            driver = new ChromeDriver(WebDriverConfiguration.BrowserDownloadPath, options);
                        }
                        else driver = new ChromeDriver(options);
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
                driver = new RemoteWebDriver(capabilities);
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

        private DesiredCapabilities SetCommonCapabilities(DesiredCapabilities capabilities)
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
                    capabilities.SetCapability("platform", new Platform(PlatformType.Windows));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("linux"))
                {
                    capabilities.SetCapability("platform", new Platform(PlatformType.Linux));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("mac"))
                {
                    capabilities.SetCapability("platform",new Platform(PlatformType.Mac));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("XP"))
                {
                    capabilities.SetCapability("platform", new Platform(PlatformType.XP));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("unix"))
                {
                    capabilities.SetCapability("platform", new Platform(PlatformType.Unix));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("Vista"))
                {
                    capabilities.SetCapability("platform", new Platform(PlatformType.Vista));
                }
                else if (WebDriverConfiguration.BrowserPlatform.ToLower().Contains("android"))
                {
                    capabilities.SetCapability("platform", new Platform(PlatformType.Android));
                }
            }

            capabilities = new DesiredCapabilities(browserName, version, platform);

            if (WebDriverConfiguration.BrowserVersion != string.Empty) capabilities.SetCapability("browserVersion", WebDriverConfiguration.BrowserVersion);
            if (WebDriverConfiguration.TakesScreenshot != null) capabilities.SetCapability("takesScreenshot", WebDriverConfiguration.TakesScreenshot);
            if (WebDriverConfiguration.HandlesAlerts != null) capabilities.SetCapability("handlesAlerts", WebDriverConfiguration.HandlesAlerts);
            if (WebDriverConfiguration.CSSSelectorsEnabled != null) capabilities.SetCapability("cssSelectorsEnabled", WebDriverConfiguration.CSSSelectorsEnabled);
            if (WebDriverConfiguration.JavascriptEnabled != null) capabilities.SetCapability("javascriptEnabled", WebDriverConfiguration.JavascriptEnabled);
            if (WebDriverConfiguration.DatabaseEnabled != null) capabilities.SetCapability("databaseEnabled", WebDriverConfiguration.DatabaseEnabled);
            if (WebDriverConfiguration.LocationContextEnabled != null) capabilities.SetCapability("locationContextEnabled", WebDriverConfiguration.LocationContextEnabled);
            if (WebDriverConfiguration.ApplicationCacheEnabled != null) capabilities.SetCapability("applicationCacheEnabled", WebDriverConfiguration.ApplicationCacheEnabled);
            if (WebDriverConfiguration.BrowserConnectionEnabled != null) capabilities.SetCapability("browserConnectionEnabled", WebDriverConfiguration.BrowserConnectionEnabled);
            if (WebDriverConfiguration.WebStorageEnabled != null) capabilities.SetCapability("webStorageEnabled", WebDriverConfiguration.WebStorageEnabled);
            if (WebDriverConfiguration.AcceptSSLCerts != null) capabilities.SetCapability("acceptSslCerts", WebDriverConfiguration.AcceptSSLCerts);
            if (WebDriverConfiguration.Rotatable != null) capabilities.SetCapability("rotatable", WebDriverConfiguration.Rotatable);
            if (WebDriverConfiguration.NativeEvents != null) capabilities.SetCapability("nativeEvents", WebDriverConfiguration.NativeEvents);
            if (WebDriverConfiguration.UnexpectedAlertBehavior != null) capabilities.SetCapability("unexpectedAlertBehaviour", WebDriverConfiguration.UnexpectedAlertBehavior);
            if (WebDriverConfiguration.ElementScrollBehavior != null) capabilities.SetCapability("elementScrollBehavior", WebDriverConfiguration.ElementScrollBehavior);

            // JSON Proxy
            if (WebDriverConfiguration.JSONProxyType != string.Empty) capabilities.SetCapability("proxyType", WebDriverConfiguration.JSONProxyType);
            if (WebDriverConfiguration.JSONProxyAutoConfigURL != string.Empty) capabilities.SetCapability("proxyAutoconfigUrl", WebDriverConfiguration.JSONProxyAutoConfigURL);
            if (WebDriverConfiguration.JSONProxy != string.Empty) capabilities.SetCapability(WebDriverConfiguration.JSONProxy, WebDriverConfiguration.JSONProxy);
            if (WebDriverConfiguration.JSONSocksUsername != string.Empty) capabilities.SetCapability("socksUsername", WebDriverConfiguration.JSONSocksUsername);
            if (WebDriverConfiguration.JSONSocksPassword != string.Empty) capabilities.SetCapability("socksPassword", WebDriverConfiguration.JSONSocksPassword);
            if (WebDriverConfiguration.JSONNoProxy != null) capabilities.SetCapability("noProxy", WebDriverConfiguration.JSONNoProxy);
            if (WebDriverConfiguration.JSONLoggingComponent != string.Empty) capabilities.SetCapability("component", WebDriverConfiguration.JSONLoggingComponent);
            if (WebDriverConfiguration.RemoteWebdriverRemoteQuietExceptions != null) capabilities.SetCapability("webdriver.remote.quietExceptions", WebDriverConfiguration.RemoteWebdriverRemoteQuietExceptions);

            return capabilities;
        }
    }
}
