using atf.toolbox.interfaces;
using System;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class WebDriverSettings : ConfigurationSection, IWebDriverSettings
    {
        private static IWebDriverSettings _settings = ConfigurationManager.GetSection("ReportSettings") as IWebDriverSettings;

        public static IWebDriverSettings WebDriverTestSettings
        {
            get
            {
                return _settings;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
        [ConfigurationProperty("common-browser-name" , IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserName
        {
            get { return this["common-browser-name"].ToString().Trim().ToLower(); }
            set { this["common-browser-name"] = value; }
        }

        [ConfigurationProperty("common-browser-version", IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserVersion
        {
            get { return this["common-browser-version"].ToString().Trim().ToLower(); }
            set { this["common-browser-version"] = value; }
        }

        [ConfigurationProperty("common-browser-download-path", IsRequired = false)]
        public string BrowserDownloadPath
        {
            get { return this["common-browser-download-path"].ToString().Trim(); }
            set { this["common-browser-download-path"] = value; }
        }

        [ConfigurationProperty("common-browser-platform", IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserPlatform
        {
            get { return this["common-browser-platform"].ToString().Trim().ToLower(); }
            set { this["common-browser-platform"] = value; }
        }

        [ConfigurationProperty("common-takes-screenshot", IsRequired = false)]
        public Boolean? TakesScreenshot
        {
            get { return (Boolean?)this["common-takes-screenshot"]; }
            set { this["common-takes-screenshot"] = value; }
        }

        [ConfigurationProperty("common-handles-alerts", IsRequired = false)]
        public Boolean? HandlesAlerts
        {
            get { return (Boolean?)this["common-handles-alerts"]; }
            set { this["common-handles-alerts"] = value; }
        }

        [ConfigurationProperty("common-css-selectors-enabled", IsRequired = false)]
        public Boolean? CSSSelectorsEnabled
        {
            get { return (Boolean?)this["common-css-selectors-enabled"]; }
            set { this["common-css-selectors-enabled"] = value; }
        }

        [ConfigurationProperty("common-javascript-enabled", IsRequired = false)]
        public Boolean? JavascriptEnabled
        {
            get { return (Boolean?)this["common-javascript-enabled"]; }
            set { this["common-javascript-enabled"] = value; }
        }

        [ConfigurationProperty("common-database-enabled", IsRequired = false)]
        public Boolean? DatabaseEnabled
        {
            get { return (Boolean?)this["common-database-enabled"]; }
            set { this["common-database-enabled"] = value; }
        }

        [ConfigurationProperty("common-location-context-enabled", IsRequired = false)]
        public Boolean? LocationContextEnabled
        {
            get { return (Boolean?)this["common-location-context-enabled"]; }
            set { this["common-location-context-enabled"] = value; }
        }

        [ConfigurationProperty("common-application-cache-enabled", IsRequired = false)]
        public Boolean? ApplicationCacheEnabled
        {
            get { return (Boolean?)this["common-application-cache-enabled"]; }
            set { this["common-application-cache-enabled"] = value; }
        }

        [ConfigurationProperty("common-browser-connection-enabled", IsRequired = false)]
        public Boolean? BrowserConnectionEnabled
        {
            get { return (Boolean?)this["common-browser-connection-enabled"]; }
            set { this["common-browser-connection-enabled"] = value; }
        }

        [ConfigurationProperty("common-web-storage-enabled", IsRequired = false)]
        public Boolean? WebStorageEnabled
        {
            get { return (Boolean?)this["common-web-storage-enabled"]; }
            set { this["common-web-storage-enabled"] = value; }
        }

        [ConfigurationProperty("common-rotatable", IsRequired = false)]
        public Boolean? Rotatable
        {
            get { return (Boolean?)this["common-rotatable"]; }
            set { this["common-rotatable"] = value; }
        }

        [ConfigurationProperty("common-accept-ssl-certs", IsRequired = false)]
        public Boolean? AcceptSSLCerts
        {
            get { return (Boolean?)this["common-accept-ssl-certs"]; }
            set { this["common-accept-ssl-certs"] = value; }
        }

        [ConfigurationProperty("common-native-events", IsRequired = false)]
        public Boolean? NativeEvents
        {
            get { return (Boolean?)this["common-native-events"]; }
            set { this["common-native-events"] = value; }
        }
        [ConfigurationProperty("common-proxy", IsRequired = false)]
        public string Proxy
        {
            get { return this["common-proxy"].ToString().Trim(); }
            set { this["common-proxy"] = value; }
        }
        [ConfigurationProperty("common-unexpected-alert-behavior", IsRequired = false)]
        public string UnexpectedAlertBehavior
        {
            get { return this["common-unexpected-alert-behavior"].ToString().Trim(); }
            set { this["common-unexpected-alert-behavior"] = value; }
        }
        [ConfigurationProperty("common-element-scroll-behavior", IsRequired = false)]
        public string ElementScrollBehavior
        {
            get { return this["common-element-scroll-behavior"].ToString().Trim(); }
            set { this["common-element-scroll-behavior"] = value; }
        }

        [ConfigurationProperty("common-json-proxy-type", IsRequired = false)]
        public string JSONProxyType
        {
            get { return this["common-json-proxy-type"].ToString().Trim(); }
            set { this["common-json-proxy-type"] = value; }
        }

        [ConfigurationProperty("common-json-proxy-auto-config-url", IsRequired = false)]
        public string JSONProxyAutoConfigURL
        {
            get { return this["common-json-proxy-auto-config-url"].ToString().Trim(); }
            set { this["common-json-proxy-auto-config-url"] = value; }
        }

        [ConfigurationProperty("common-json-proxy", IsRequired = false)]
        public string JSONProxy
        {
            get { return this["common-json-proxy"].ToString().Trim(); }
            set { this["common-json-proxy"] = value; }
        }

        [ConfigurationProperty("common-json-socks-username", IsRequired = false)]
        public string JSONSocksUsername
        {
            get { return this["common-json-socks-username"].ToString().Trim(); }
            set { this["common-json-socks-username"] = value; }
        }

        [ConfigurationProperty("common-json-socks-password", IsRequired = false)]
        public string JSONSocksPassword
        {
            get { return this["common-json-socks-password"].ToString().Trim(); }
            set { this["common-json-socks-password"] = value; }
        }
        [ConfigurationProperty("common-json-no-proxy", IsRequired = false)]
        public Boolean? JSONNoProxy
        {
            get { return (Boolean?)this["common-json-no-proxy"]; }
            set { this["common-json-no-proxy"] = value; }
        }
        [ConfigurationProperty("common-json-logging-component", IsRequired = false)]
        public string JSONLoggingComponent
        {
            get { return this["common-json-logging-component"].ToString().Trim(); }
            set { this["common-json-logging-component"] = value; }
        }
        [ConfigurationProperty("common-remote-webdriver-remote-quiet-execptions", IsRequired = false)]
        public Boolean? RemoteWebdriverRemoteQuietExceptions
        {
            get { return (Boolean?)this["common-remote-webdriver-remote-quiet-execptions"]; }
            set { this["common-remote-webdriver-remote-quiet-execptions"] = value; }
        }

        [ConfigurationProperty("common-grid-use", IsRequired = false)]
        public Boolean? UseGrid
        {
            get { return (Boolean?)this["common-grid-use"]; }
            set { this["common-grid-use"] = value; }
        }
        [ConfigurationProperty("common-grid-url", IsRequired = false)]
        public string GridURL
        {
            get { return this["common-grid-url"].ToString().Trim(); }
            set { this["common-grid-url"] = value; }
        }
        [ConfigurationProperty("common-grid-selenium-protocol", IsRequired = false)]
        public string GridSeleniumProtocol
        {
            get { return this["common-grid-selenium-protocol"].ToString().Trim(); }
            set { this["common-grid-selenium-protocol"] = value; }
        }
        [ConfigurationProperty("common-grid-max-instances", IsRequired = false)]
        public int? GridMaxInstances
        {
            get { return (int?)this["common-grid-max-instances"]; }
            set { this["common-grid-max-instances"] = value; }
        }

        // PHANTOMJS
        [ConfigurationProperty("phantomjs-executable-path-property", IsRequired = false)]
        public string PhantomJSExecutablePathProperty
        {
            get { return this["phantomjs-executable-path-property"].ToString().Trim(); }
            set { this["phantomjs-executable-path-property"] = value; }
        }
        [ConfigurationProperty("phantomjs-page-settings-prefix", IsRequired = false)]
        public string PhantomJSPageSettingPrefix
        {
            get { return this["phantomjs-page-settings-prefix"].ToString().Trim(); }
            set { this["phantomjs-page-settings-prefix"] = value; }
        }
        [ConfigurationProperty("phantomjs-page-customheaders-prefix", IsRequired = false)]
        public string PhantomJSPageCustomHeadersPrefix
        {
            get { return this["phantomjs-page-customheaders-prefix"].ToString().Trim(); }
            set { this["phantomjs-page-customheaders-prefix"] = value; }
        }
        [ConfigurationProperty("phantomjs-cli-args", IsRequired = false)]
        public string PhantomJSCLIArgs
        {
            get { return this["phantomjs-cli-args"].ToString().Trim(); }
            set { this["phantomjs-cli-args"] = value; }
        }
        [ConfigurationProperty("ghostdriver-path-property", IsRequired = false)]
        public string GhostDriverPathProperties
        {
            get { return this["ghostdriver-path-property"].ToString().Trim(); }
            set { this["ghostdriver-path-property"] = value; }
        }
        [ConfigurationProperty("ghostdriver-cli-args", IsRequired = false)]
        public string GhostDriverCLIArgs
        {
            get { return this["ghostdriver-cli-args"].ToString().Trim(); }
            set { this["ghostdriver-cli-args"] = value; }
        }

        // OPERA
        [ConfigurationProperty("opera-binary", IsRequired = false)]
        public string OperaBinary
        {
            get { return this["opera-binary"].ToString().Trim(); }
            set { this["opera-binary"] = value; }
        }
        [ConfigurationProperty("opera-guess-binary-path", IsRequired = false)]
        public Boolean? OperaGuessBinaryPath
        {
            get { return (Boolean?)this["opera-guess-binary-path"]; }
            set { this["opera-guess-binary-path"] = value; }
        }
        [ConfigurationProperty("opera-no-restart", IsRequired = false)]
        public Boolean? OperaNoRestart
        {
            get { return (Boolean?)this["opera-no-restart"]; }
            set { this["opera-no-restart"] = value; }
        }
        [ConfigurationProperty("opera-product", IsRequired = false)]
        public string OperaProduct
        {
            get { return this["opera-product"].ToString().Trim(); }
            set { this["opera-product"] = value; }
        }
        [ConfigurationProperty("opera-no-quit", IsRequired = false)]
        public Boolean? OperaNoQuit
        {
            get { return (Boolean?)this["opera-no-quit"]; }
            set { this["opera-no-quit"] = value; }
        }
        [ConfigurationProperty("opera-autostart", IsRequired = false)]
        public Boolean? OperaAutoStart
        {
            get { return (Boolean?)this["opera-autostart"]; }
            set { this["opera-autostart"] = value; }
        }
        [ConfigurationProperty("opera-display", IsRequired = false)]
        public int? OperaDisplay
        {
            get { return (int?)this["opera-display"]; }
            set { this["opera-display"] = value; }
        }
        [ConfigurationProperty("opera-idle", IsRequired = false)]
        public Boolean? OperaIdle
        {
            get { return (Boolean?)this["opera-idle"]; }
            set { this["opera-idle"] = value; }
        }
        [ConfigurationProperty("opera-profile", IsRequired = false)]
        public string OperaProfile
        {
            get { return this["opera-profile"].ToString().Trim(); }
            set { this["opera-profile"] = value; }
        }
        [ConfigurationProperty("opera-launcher", IsRequired = false)]
        public string OperaLauncher
        {
            get { return this["opera-launcher"].ToString().Trim(); }
            set { this["opera-launcher"] = value; }
        }
        [ConfigurationProperty("opera-port", IsRequired = false)]
        public int? OperaPort
        {
			get { return (int?)this["opera-port"]; }
            set { this["opera-port"] = value; }
        }
        [ConfigurationProperty("opera-host", IsRequired = false)]
        public string OperaHost
        {
            get { return this["opera-host"].ToString().Trim(); }
            set { this["opera-host"] = value; }
        }
        [ConfigurationProperty("opera-arguments", IsRequired = false)]
        public string OperaArguments
        {
            get { return this["opera-arguments"].ToString().Trim(); }
            set { this["opera-arguments"] = value; }
        }
        [ConfigurationProperty("opera-logging-file", IsRequired = false)]
        public string OperaLoggingFile
        {
            get { return this["opera-logging-file"].ToString().Trim(); }
            set { this["opera-logging-file"] = value; }
        }
        [ConfigurationProperty("opera-logging-level", IsRequired = false)]
        public string OperaLoggingLevel
        {
            get { return this["opera-logging-level"].ToString().Trim(); }
            set { this["opera-logging-level"] = value; }
        }
        
        // CHROME
        [ConfigurationProperty("chrome-proxy", IsRequired = false)]
        public string ChromeProxy
        {
            get { return this["chrome-proxy"].ToString().Trim(); }
            set { this["chrome-proxy"] = value; }
        }
        [ConfigurationProperty("chrome-options-args", IsRequired = false)]
        public string ChromeOptionsArgs
        {
            get { return this["chrome-options-args"].ToString().Trim(); }
            set { this["chrome-options-args"] = value; }
        }
        [ConfigurationProperty("chrome-options-binary", IsRequired = false)]
        public string ChromeOptionsBinary
        {
            get { return this["chrome-options-binary"].ToString().Trim(); }
            set { this["chrome-options-binary"] = value; }
        }
        [ConfigurationProperty("chrome-options-extentions", IsRequired = false)]
        public string ChromeOptionsExtentions
        {
            get { return this["chrome-options-extentions"].ToString().Trim(); }
            set { this["chrome-options-extentions"] = value; }
        }
        
        // FIREFOX
        [ConfigurationProperty("firefox-profile-dir-and-filename", IsRequired = false)]
        public string FirefoxProfileDirAndFilename
        {
            get { return this["firefox-profile-dir-and-filename"].ToString().Trim(); }
            set { this["firefox-profile-dir-and-filename"] = value; }
        }
        [ConfigurationProperty("firefox-logging-prefs", IsRequired = false)]
        public string FirefoxLoggingPrefs
        {
            get { return this["firefox-logging-prefs"].ToString().Trim(); }
            set { this["firefox-logging-prefs"] = value; }
        }
        [ConfigurationProperty("firefox-binary", IsRequired = false)]
        public string FirefoxBinary
        {
            get { return this["firefox-binary"].ToString().Trim(); }
            set { this["firefox-binary"] = value; }
        }
        [ConfigurationProperty("firefox-rc-mode", IsRequired = false)]
        public Boolean? FirefoxRCMode
        {
            get { return (Boolean?)this["firefox-rc-mode"]; }
            set { this["firefox-rc-mode"] = value; }
        }
        [ConfigurationProperty("firefox-rc-capture-network-traffic", IsRequired = false)]
        public Boolean? FirefoxRCCaptureNetworkTraffic
        {
            get { return (Boolean?)this["firefox-rc-capture-network-traffic"]; }
            set { this["firefox-rc-capture-network-traffic"] = value; }
        }
        [ConfigurationProperty("firefox-rc-add-custom-request-header", IsRequired = false)]
        public Boolean? FirefoxRCAddCustomRequestHeader
        {
            get { return (Boolean?)this["firefox-rc-add-custom-request-header"]; }
            set { this["firefox-rc-add-custom-request-header"] = value; }
        }
        [ConfigurationProperty("firefox-trust-all-ssl-certs", IsRequired = false)]
        public Boolean? FirefoxTrustAllSSLCerts
        {
            get { return (Boolean?)this["firefox-trust-all-ssl-certs"]; }
            set { this["firefox-trust-all-ssl-certs"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-accept-untrusted-certs", IsRequired = false)]
        public Boolean? FirefoxWebdriverAcceptUntrustedCerts
        {
            get { return (Boolean?)this["firefox-webdriver-accept-untrusted-certs"]; }
            set { this["firefox-webdriver-accept-untrusted-certs"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-assume-untrusted-issuer", IsRequired = false)]
        public Boolean? FirefoxWebdriverAssumeUntrustedIssuer
        {
            get { return (Boolean?)this["firefox-webdriver-assume-untrusted-issuer"]; }
            set { this["firefox-webdriver-assume-untrusted-issuer"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-log-driver", IsRequired = false)]
        public string FirefoxWebdriverLogDriver
        {
            get { return this["firefox-webdriver-log-driver"].ToString().Trim(); }
            set { this["firefox-webdriver-log-driver"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-log-file", IsRequired = false)]
        public string FirefoxWebdriverLogFile
        {
            get { return this["firefox-webdriver-log-file"].ToString().Trim(); }
            set { this["firefox-webdriver-log-file"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-load-strategy", IsRequired = false)]
        public string FirefoxWebdriverLoadStrategy
        {
            get { return this["firefox-webdriver-load-strategy"].ToString().Trim(); }
            set { this["firefox-webdriver-load-strategy"] = value; }
        }
        [ConfigurationProperty("firefox-webdriver-port", IsRequired = false)]
        public int? FirefoxWebdriverPort
        {
			get { return (int?)this["firefox-webdriver-port"]; }
            set { this["firefox-webdriver-port"] = value; }
        }

        // INTERNET EXPLORER
        [ConfigurationProperty("ie-ignore-protected-mode-settings", IsRequired = false)]
        public Boolean? IEIgnoreProtectedModeSettings
        {
            get { return (Boolean?)this["ie-ignore-protected-mode-settings"]; }
            set { this["ie-ignore-protected-mode-settings"] = value; }
        }
        [ConfigurationProperty("ie-ignore-zoom-setting", IsRequired = false)]
        public Boolean? IEIgnoreZoomSetting
        {
            get { return (Boolean?)this["ie-ignore-zoom-setting"]; }
            set { this["ie-ignore-zoom-setting"] = value; }
        }
        [ConfigurationProperty("ie-initial-browser-url", IsRequired = false)]
        public string IEInitialBrowserURL
        {
            get { return this["ie-initial-browser-url"].ToString().Trim(); }
            set { this["ie-initial-browser-url"] = value; }
        }
        [ConfigurationProperty("ie-enable-persistent-hover", IsRequired = false)]
        public Boolean? IEEnablePersistentHover
        {
            get { return (Boolean?)this["ie-enable-persistent-hover"]; }
            set { this["ie-enable-persistent-hover"] = value; }
        }
        [ConfigurationProperty("ie-enable-element-cache-cleanup", IsRequired = false)]
        public Boolean? IEEnableElementCacheCleanup
        {
            get { return (Boolean?)this["ie-enable-element-cache-cleanup"]; }
            set { this["ie-enable-element-cache-cleanup"] = value; }
        }
        [ConfigurationProperty("ie-require-window-focus", IsRequired = false)]
        public Boolean? IERequireWindowFocus
        {
            get { return (Boolean?)this["ie-require-window-focus"]; }
            set { this["ie-require-window-focus"] = value; }
        }
        [ConfigurationProperty("ie-browser-attach-timeout", IsRequired = false)]
        public int? IEBrowserAttachTimeout
        {
			get { return (int?)this["ie-browser-attach-timeout"]; }
            set { this["ie-browser-attach-timeout"] = value; }
        }
        [ConfigurationProperty("ie-force-create-process-api", IsRequired = false)]
        public Boolean? IEForceCreateProcessAPI
        {
            get { return (Boolean?)this["ie-force-create-process-api"]; }
            set { this["ie-force-create-process-api"] = value; }
        }
        [ConfigurationProperty("ie-browser-cmd-line-switches", IsRequired = false)]
        public string IEBrowserCMDLineSwitches
        {
            get { return this["ie-browser-cmd-line-switches"].ToString().Trim(); }
            set { this["ie-browser-cmd-line-switches"] = value; }
        }
        [ConfigurationProperty("ie-use-per-process-proxy", IsRequired = false)]
        public Boolean? IEUsePerProcessProxy
        {
            get { return (Boolean?)this["ie-use-per-process-proxy"]; }
            set { this["ie-use-per-process-proxy"] = value; }
        }
        [ConfigurationProperty("ie-ensure-clean-session", IsRequired = false)]
        public Boolean? IEEnsureCleanSession
        {
            get { return (Boolean?)this["ie-ensure-clean-session"]; }
            set { this["ie-ensure-clean-session"] = value; }
        }
        [ConfigurationProperty("ie-log-file", IsRequired = false)]
        public string IELogFile
        {
            get { return this["ie-log-file"].ToString().Trim(); }
            set { this["ie-log-file"] = value; }
        }
        [ConfigurationProperty("ie-log-level", IsRequired = false)]
        public string IELogLevel
        {
            get { return this["ie-log-level"].ToString().Trim(); }
            set { this["ie-log-level"] = value; }
        }
        [ConfigurationProperty("ie-host", IsRequired = false)]
        public string IEHost
        {
            get { return this["ie-host"].ToString().Trim(); }
            set { this["ie-host"] = value; }
        }
        [ConfigurationProperty("ie-extract-path", IsRequired = false)]
        public string IEExtractPath
        {
            get { return this["ie-extract-path"].ToString().Trim(); }
            set { this["ie-extract-path"] = value; }
        }
        [ConfigurationProperty("ie-silent", IsRequired = false)]
        public Boolean? IESilent
        {
            get { return (Boolean?)this["ie-silent"]; }
            set { this["ie-silent"] = value; }
        }
        [ConfigurationProperty("ie-set-proxy-by-server", IsRequired = false)]
        public Boolean? IESetProxyByServer
        {
            get { return (Boolean?)this["ie-set-proxy-by-server"]; }
            set { this["ie-set-proxy-by-server"] = value; }
        }
        [ConfigurationProperty("ie-rc-mode", IsRequired = false)]
        public string IERCMode
        {
            get { return this["ie-rc-mode"].ToString().Trim(); }
            set { this["ie-rc-mode"] = value; }
        }
        [ConfigurationProperty("ie-rc-kill-processes-by-name", IsRequired = false)]
        public Boolean? IERCKillProcessesByName
        {
            get { return (Boolean?)this["ie-rc-kill-processes-by-name"]; }
            set { this["ie-rc-kill-processes-by-name"] = value; }
        }
        [ConfigurationProperty("ie-rc-honor-system-proxy", IsRequired = false)]
        public Boolean? IERCHonorSystemProxy
        {
            get { return (Boolean?)this["ie-rc-honor-system-proxy"]; }
            set { this["ie-rc-honor-system-proxy"] = value; }
        }
        [ConfigurationProperty("ie-rc-ensure-clean-session", IsRequired = false)]
        public Boolean? IERCEnsureCleanSession
        {
            get { return (Boolean?)this["ie-rc-ensure-clean-session"]; }
            set { this["ie-rc-ensure-clean-session"] = value; }
        }
		
		// SAFARI
        [ConfigurationProperty("safari-use-options", IsRequired = false)]
        public Boolean? SafariUseOptions
        {
            get { return (Boolean?)this["safari-use-options"]; }
            set { this["safari-use-options"] = value; }
        }
        [ConfigurationProperty("safari-data-dir", IsRequired = false)]
        public string SafariDataDir
        {
            get { return this["safari-data-dir"].ToString().Trim(); }
            set { this["safari-data-dir"] = value; }
        }
        [ConfigurationProperty("safari-driver-extension", IsRequired = false)]
        public string SafariDriverExtension
        {
            get { return this["safari-driver-extension"].ToString().Trim(); }
            set { this["safari-driver-extension"] = value; }
        }
        [ConfigurationProperty("safari-skip-extension-installation", IsRequired = false)]
        public Boolean? SafariSkipExtensionInstallation
        {
            get { return (Boolean?)this["safari-skip-extension-installation"]; }
            set { this["safari-skip-extension-installation"] = value; }
        }
        [ConfigurationProperty("safari-port", IsRequired = false)]
        public Boolean? SafariPort
        {
            get { return (Boolean?)this["safari-port"]; }
            set { this["safari-port"] = value; }
        }
        [ConfigurationProperty("safari-use-clean-session", IsRequired = false)]
        public int? SafariUseCleanSession
        {
			get { return (int?)this["safari-use-clean-session"]; }
            set { this["safari-use-clean-session"] = value; }
        }
        [ConfigurationProperty("safari-rc-mode", IsRequired = false)]
        public string SafariRCMode
        {
            get { return this["safari-rc-mode"].ToString().Trim(); }
            set { this["safari-rc-mode"] = value; }
        }
        [ConfigurationProperty("safari-rc-honor-system-proxy", IsRequired = false)]
        public Boolean? SafariRCHonorSystemProxy
        {
            get { return (Boolean?)this["safari-rc-honor-system-proxy"]; }
            set { this["safari-rc-honor-system-proxy"] = value; }
        }
    }
}
