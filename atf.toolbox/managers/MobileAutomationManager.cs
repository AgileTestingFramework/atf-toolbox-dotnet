using atf.toolbox.configuration;
using atf.toolbox;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace atf.toolbox.managers
{
    public class MobileAutomationManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MobileAutomationManager));

        private Process AppiumServer;
        private ProcessStartInfo AppiumServerProcessStartInfo;

        public IWebDriver MobileDriver { get; set; }

        /// <summary>
        /// WebDriverConfiguration
        /// Configuration section WebDriverSettings
        /// </summary>
        public MobileSettings MobileConfiguration
        {
            get { return (MobileSettings)ConfigurationManager.GetSection("MobileSettings"); }
        }

        public MobileAutomationManager()
        {
            if (MobileConfiguration.StartAppiumServer != null && MobileConfiguration.StartAppiumServer.Value)
            {
                StartAppiumServer();                
            }

            MobileDriver = StartMobileDriver();
        }

        public void TearDown()
        {
            if (MobileDriver != null)
            {
                // Shut down the webdriver
                _log.Info("MobileDriver teardown started.");
                MobileDriver.Quit();
                MobileDriver = null;
                _log.Info("MobileDriver teardown complete.");
            }

            StopAppiumServer();
        }

        private IWebDriver StartMobileDriver()
        {
            IWebDriver localwebDriver = null;

            Uri addressUri = new Uri(MobileConfiguration.GridUrl);

            DesiredCapabilities caps = new DesiredCapabilities();

            if (MobileConfiguration.Platform.ToLower().Trim() == "ios")
            {
                if (MobileConfiguration.DeviceType.ToLower().Contains("iphone"))
                    caps = DesiredCapabilities.IPhone();
                else caps = DesiredCapabilities.IPad();

                SetCommonCapabilities(caps);
                SetIOSCapabilities(caps);

                localwebDriver = new IOSDriver(addressUri, caps, TimeSpan.FromSeconds(120));
                _log.Info("IOS Driver loaded.");

                return localwebDriver;
            }
            else // Android is the default
            {
                caps = DesiredCapabilities.Android(); // Set Android Defaults to start

                SetCommonCapabilities(caps);
                SetAndroidCapabilities(caps);

                localwebDriver = new AndroidDriver(addressUri, caps, TimeSpan.FromSeconds(500));
                _log.Info("Android Driver loaded.");
                return localwebDriver;
            }
        }

        private void SetIOSCapabilities(DesiredCapabilities caps)
        {
            if (MobileConfiguration.IOSCalendarFormat.Length != 0)
                caps.SetCapability("calendarFormat", MobileConfiguration.IOSCalendarFormat);
            if (MobileConfiguration.IOSBundleId.Length != 0)
                caps.SetCapability("bundleId", MobileConfiguration.IOSBundleId);
            if (MobileConfiguration.IOSLaunchTimeout != null)
                caps.SetCapability("launchTimeout", MobileConfiguration.IOSLaunchTimeout);
            if (MobileConfiguration.IOSLocationServicesEnabled != null)
                caps.SetCapability("locationServicesEnabled", MobileConfiguration.IOSLocationServicesEnabled.Value);
            if (MobileConfiguration.IOSLocationServicesAuthorized != null)
                caps.SetCapability("locationServicesAuthorized", MobileConfiguration.IOSLocationServicesAuthorized.Value);
            if (MobileConfiguration.IOSAutoAcceptAlerts != null)
                caps.SetCapability("autoAcceptAlerts", MobileConfiguration.IOSAutoAcceptAlerts.Value);
            if (MobileConfiguration.IOSAutoDismissAlerts != null)
                caps.SetCapability("autoDismissAlerts", MobileConfiguration.IOSAutoDismissAlerts.Value);
            if (MobileConfiguration.IOSNativeInstrumentsLib != null)
                caps.SetCapability("nativeInstrumentsLib", MobileConfiguration.IOSNativeInstrumentsLib.Value);
            if (MobileConfiguration.IOSNativeWebTap != null)
                caps.SetCapability("nativeWebTap", MobileConfiguration.IOSNativeWebTap.Value);
            if (MobileConfiguration.IOSSafariInitialUrl.Length != 0)
                caps.SetCapability("safariInitialUrl", MobileConfiguration.IOSSafariInitialUrl);
            if (MobileConfiguration.IOSSafariAllowPopups != null)
                caps.SetCapability("safariAllowPopups", MobileConfiguration.IOSSafariAllowPopups.Value);
            if (MobileConfiguration.IOSSafariIgnoreFraudWarnings != null)
                caps.SetCapability("safariIgnoreFraudWarning", MobileConfiguration.IOSSafariIgnoreFraudWarnings.Value);
            if (MobileConfiguration.IOSSafariOpenLinksInBackground != null)
                caps.SetCapability("safariOpenLinksInBackground", MobileConfiguration.IOSSafariOpenLinksInBackground.Value);
            if (MobileConfiguration.IOSKeepKeyChains != null)
                caps.SetCapability("keepKeyChains", MobileConfiguration.IOSKeepKeyChains.Value);
            if (MobileConfiguration.IOSLocalizableStringsDirectory.Length != 0)
                caps.SetCapability("localizableStringsDir", MobileConfiguration.IOSLocalizableStringsDirectory);
            if (MobileConfiguration.IOSProcessArguments.Length != 0)
                caps.SetCapability("processArguments", MobileConfiguration.IOSProcessArguments);
            if (MobileConfiguration.IOSInterKeyDelay != null)
                caps.SetCapability("interKeyDelay", MobileConfiguration.IOSInterKeyDelay.Value);
            if (MobileConfiguration.IOSShowIOSLog != null)
                caps.SetCapability("showIOSLog", MobileConfiguration.IOSShowIOSLog.Value);
            if (MobileConfiguration.IOSSendKeyStrategy.Length != 0)
                caps.SetCapability("sendKeyStrategy", MobileConfiguration.IOSSendKeyStrategy);
            if (MobileConfiguration.IOSScreenShotWaitTimeout != null)
                caps.SetCapability("screenshotWaitTimeout", MobileConfiguration.IOSScreenShotWaitTimeout);
            if (MobileConfiguration.IOSWaitForAppScript != null)
                caps.SetCapability("waitForAppScript", MobileConfiguration.IOSWaitForAppScript.Value);	
        }

        private void SetAndroidCapabilities(DesiredCapabilities caps)
        {
            if (MobileConfiguration.AndroidAppActivity.Length != 0)
                caps.SetCapability("appActivity", MobileConfiguration.AndroidAppActivity);
            if (MobileConfiguration.AndroidAppPackage.Length != 0)
                caps.SetCapability("appPackage", MobileConfiguration.AndroidAppPackage);
            if (MobileConfiguration.AndroidAppWaitActivity.Length != 0)
                caps.SetCapability("appWaitActivity", MobileConfiguration.AndroidAppWaitActivity);
            if (MobileConfiguration.AndroidAppWaitPackage.Length != 0)
                caps.SetCapability("appWaitPackage", MobileConfiguration.AndroidAppWaitPackage);
            if (MobileConfiguration.AndroidReadyTimeout != null)
                caps.SetCapability("deviceReadyTimeout", MobileConfiguration.AndroidReadyTimeout);
            if (MobileConfiguration.AndroidCoverage != null)
                caps.SetCapability("androidCoverage", MobileConfiguration.AndroidCoverage.Value);
            if (MobileConfiguration.AndroidEnablePerformanceLogging != null)
                caps.SetCapability("enablePerformanceLogging", MobileConfiguration.AndroidEnablePerformanceLogging.Value);
            if (MobileConfiguration.AndroidReadyTimeoutAfterBoot != null)
                caps.SetCapability("androidDeviceReadyTimeout", MobileConfiguration.AndroidReadyTimeoutAfterBoot);
            if (MobileConfiguration.AndroidDeviceSocket != null)
                caps.SetCapability("androidDeviceSocket", MobileConfiguration.AndroidDeviceSocket);

            if (MobileConfiguration.AndroidAVDName != null && MobileConfiguration.AndroidAVDName.Length > 0)
            {
                caps.SetCapability("avd", MobileConfiguration.AndroidAVDName);
                if (MobileConfiguration.AndroidAVDLaunchTimeout != null)
                    caps.SetCapability("avdLaunchTimeout", (MobileConfiguration.AndroidAVDLaunchTimeout * 1000));
                if (MobileConfiguration.AndroidAVDReadyTimeout != null)
                    caps.SetCapability("avdReadyTimeout", (MobileConfiguration.AndroidAVDReadyTimeout * 1000));
                if (MobileConfiguration.AndroidAVDArguments.Length != 0)
                    caps.SetCapability("avdArgs", MobileConfiguration.AndroidAVDArguments);
            }

            if (MobileConfiguration.AndroidUseKeystore != null && MobileConfiguration.AndroidUseKeystore.Value)
            {
                caps.SetCapability("useKeystore", MobileConfiguration.AndroidUseKeystore.Value);
                if (MobileConfiguration.AndroidKeystorePath.Length != 0)
                    caps.SetCapability("keystorePath", MobileConfiguration.AndroidKeystorePath);
                if (MobileConfiguration.AndroidKeystorePassword.Length != 0)
                    caps.SetCapability("keystorePassword", MobileConfiguration.AndroidKeystorePassword);
            }

            if (MobileConfiguration.AndroidKeyAlias.Length != 0)
                caps.SetCapability("keyAlias", MobileConfiguration.AndroidKeyAlias);
            if (MobileConfiguration.AndroidKeyPassword.Length != 0)
                caps.SetCapability("keyPassword", MobileConfiguration.AndroidKeyPassword);
            if (MobileConfiguration.AndroidChromeDriverExecutable.Length != 0)
                caps.SetCapability("chromedriverExecutable", MobileConfiguration.AndroidChromeDriverExecutable);
            if (MobileConfiguration.AndroidAutoWebViewTimeout != null)
                caps.SetCapability("autoWebviewTimeout", MobileConfiguration.AndroidAutoWebViewTimeout);
            if (MobileConfiguration.AndroidIntentAction.Length != 0)
                caps.SetCapability("intentAction", MobileConfiguration.AndroidIntentAction);
            if (MobileConfiguration.AndroidIntentCategory.Length != 0)
                caps.SetCapability("intentCategory", MobileConfiguration.AndroidIntentCategory);
            if (MobileConfiguration.AndroidIntentFlags.Length != 0)
                caps.SetCapability("intentFlags", MobileConfiguration.AndroidIntentFlags);
            if (MobileConfiguration.AndroidOptionalIntentArguments.Length != 0)
                caps.SetCapability("optionalIntentArguments", MobileConfiguration.AndroidOptionalIntentArguments);
            if (MobileConfiguration.AndroidStopAppOnReset != null)
                caps.SetCapability("stopAppOnReset", MobileConfiguration.AndroidStopAppOnReset.Value);
            if (MobileConfiguration.AndroidEnableUnicodeInput != null)
                caps.SetCapability("unicodeKeyboard", MobileConfiguration.AndroidEnableUnicodeInput.Value);
            if (MobileConfiguration.AndroidResetKeyboard != null)
                caps.SetCapability("resetKeyboard", MobileConfiguration.AndroidResetKeyboard.Value);
            if (MobileConfiguration.AndroidNoSigning != null)
                caps.SetCapability("noSign", MobileConfiguration.AndroidNoSigning.Value);
            if (MobileConfiguration.AndroidIgnoreUnimportantViews != null)
                caps.SetCapability("ignoreUnimportantViews", MobileConfiguration.AndroidIgnoreUnimportantViews.Value);	
        }

        private void SetCommonCapabilities(DesiredCapabilities caps)
        {
            // COMMON CAPABILITIES
            if (MobileConfiguration.UseSauceLabs != null && MobileConfiguration.UseSauceLabs.Value)
            {
                caps.SetCapability("username", MobileConfiguration.SauceLabsUserName); // supply sauce labs username
                caps.SetCapability("accessKey", MobileConfiguration.SauceLabsAccessKey);  // supply sauce labs account key
            }

            if (ATFHandler.Instance.TestContext != null)
                caps.SetCapability("name", ATFHandler.Instance.TestContext.TestName);
            if (MobileConfiguration.AppiumVersion.Length != 0)
                caps.SetCapability("appiumVersion", MobileConfiguration.AppiumVersion);
            if (MobileConfiguration.AutomationName.Length != 0)
                caps.SetCapability("automationName", MobileConfiguration.AutomationName);
            if (MobileConfiguration.Platform.Length != 0)
                caps.SetCapability("platformName", MobileConfiguration.Platform);
            if (MobileConfiguration.Version.Length != 0)
                caps.SetCapability("platformVersion", MobileConfiguration.Version);
            if (MobileConfiguration.DeviceName.Length != 0)
                caps.SetCapability("deviceName", MobileConfiguration.DeviceName);
            if (MobileConfiguration.DeviceType.Length != 0)
                caps.SetCapability("deviceType", MobileConfiguration.DeviceType);
            if (MobileConfiguration.App.Length != 0)
                caps.SetCapability("app", MobileConfiguration.App);
            if (MobileConfiguration.BrowserName.Length != 0)
                caps.SetCapability("browserName", MobileConfiguration.BrowserName);
            if (MobileConfiguration.NewCommandTimeout != null)
                caps.SetCapability("newCommandTimeout", MobileConfiguration.NewCommandTimeout);
            if (MobileConfiguration.Language.Length != 0)
                caps.SetCapability("language", MobileConfiguration.Language);
            if (MobileConfiguration.Locale.Length != 0)
                caps.SetCapability("locale", MobileConfiguration.Locale);
            if (MobileConfiguration.UDID.Length != 0)
                caps.SetCapability("udid", MobileConfiguration.UDID);
            if (MobileConfiguration.Orientation.Length != 0)
                caps.SetCapability("orientation", MobileConfiguration.Orientation);
            if (MobileConfiguration.AutoLaunch != null)
                caps.SetCapability("autoLaunch", MobileConfiguration.AutoLaunch.Value);
            if (MobileConfiguration.AutoWebview != null)
                caps.SetCapability("autoWebview", MobileConfiguration.AutoWebview.Value);
            if (MobileConfiguration.NoReset != null)
                caps.SetCapability("noReset", MobileConfiguration.NoReset.Value);
            if (MobileConfiguration.FullReset != null)
                caps.SetCapability("fullReset", MobileConfiguration.FullReset.Value);
        }

        private void StartAppiumServer()
        {
            _log.Info("KILL ANY OPEN APPIUM INSTANCE RUNNING");

            StopAppiumServer();

            _log.Info("START CLEAN APPIUM");
            AppiumServerProcessStartInfo = new ProcessStartInfo();
            AppiumServerProcessStartInfo.UseShellExecute = false; // <-enable enviroment variables

            AppiumServerProcessStartInfo.FileName = MobileConfiguration.AppiumServerPath;
            AppiumServerProcessStartInfo.Arguments = MobileConfiguration.AppiumServerArguments;
            AppiumServer = new Process();
            AppiumServer.StartInfo = AppiumServerProcessStartInfo;

            try
            {
                bool startSuccess = AppiumServer.Start();

                if (!startSuccess)
                {
                    _log.Error("Appium Server Failed to start.");
                }
                else
                {
                    Thread.Sleep(MobileConfiguration.AppiumStartWaitTime.Value*1000); // Give the node server 10 seconds to load up then check
                }
            }
            catch (Exception ioe)
            {
                _log.Error("Appium Server Failed to start.", ioe);
            }
        }

        private void StopAppiumServer()
        {
            _log.Info("Stopping appium server...");
            try
            {
                Process[] allRunningAppiumServers = Process.GetProcessesByName(MobileConfiguration.AppiumExecutableName);
                foreach (Process appium in allRunningAppiumServers)
                {
                    appium.Kill();
                }

                AppiumServer = null;
            }
            catch (Exception ex)
            {
                _log.Error("Error stopping appium server.", ex);
            }

            _log.Info("Appium server stopped.");
        }
    }

}
