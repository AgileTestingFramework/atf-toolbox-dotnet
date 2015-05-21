using atf.toolbox.interfaces;
using System;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class MobileSettings : ConfigurationSection, IMobileSettings
    {
        private static IMobileSettings _settings = ConfigurationManager.GetSection("MobileSettings") as IMobileSettings;

        public static IMobileSettings WebDriverTestSettings { get { return _settings; } }

        public override bool IsReadOnly() { return false; }

        //******* COMMON **********//        
        [ConfigurationProperty("common-automationName", IsRequired = false)]
        public string AutomationName
        {
            get { return this["common-automationName"].ToString().Trim().ToLower(); }
            set { this["common-automationName"] = value; }
        }
        [ConfigurationProperty("common-appium-version", IsRequired = false)]
        public string AppiumVersion
        {
            get { return this["common-appium-version"].ToString().Trim().ToLower(); }
            set { this["common-appium-version"] = value; }
        }
        [ConfigurationProperty("common-appium-start-node-server", IsRequired = false)]
        public Boolean? StartAppiumServer
        {
            get { return (Boolean?)this["common-appium-start-node-server"]; }
            set { this["common-appium-start-node-server"] = value; }
        }
        [ConfigurationProperty("common-appium-node-server-executable-name", IsRequired = false)]
        public string AppiumExecutableName
        {
            get { return this["common-appium-node-server-executable-name"].ToString().Trim().ToLower(); }
            set { this["common-appium-node-server-executable-name"] = value; }
        }
        [ConfigurationProperty("common-appium-node-start-wait-time", DefaultValue = 10,  IsRequired = false)]
        public int? AppiumStartWaitTime
        {
            get { return (int?)this["common-appium-node-start-wait-time"]; }
            set { this["common-appium-node-start-wait-time"] = value; }
        }
        [ConfigurationProperty("common-appium-node-server-commandline", IsRequired = false)]
        public string AppiumServerPath
        {
            get { return this["common-appium-node-server-commandline"].ToString().Trim().ToLower(); }
            set { this["common-appium-node-server-commandline"] = value; }
        }
        [ConfigurationProperty("common-appium-node-server-arguments", IsRequired = false)]
        public string AppiumServerArguments
        {
            get { return this["common-appium-node-server-arguments"].ToString().Trim().ToLower(); }
            set { this["common-appium-node-server-arguments"] = value; }
        }
        [ConfigurationProperty("common-platform-name", IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Platform
        {
            get { return this["common-platform-name"].ToString().Trim().ToLower(); }
            set { this["common-platform-name"] = value; }
        }
        [ConfigurationProperty("common-platform-version", IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Version
        {
            get { return this["common-platform-version"].ToString().Trim().ToLower(); }
            set { this["common-platform-version"] = value; }
        }
        [ConfigurationProperty("common-device-name", IsRequired = false)]
        public string DeviceName
        {
            get { return this["common-device-name"].ToString().Trim(); }
            set { this["common-device-name"] = value; }
        }
        [ConfigurationProperty("common-device-type", IsRequired = false)]
        public string DeviceType
        {
            get { return this["common-device-type"].ToString().Trim(); }
            set { this["common-device-type"] = value; }
        }
        [ConfigurationProperty("common-browser-name", IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserName
        {
            get { return this["common-browser-name"].ToString().Trim().ToLower(); }
            set { this["common-browser-name"] = value; }
        }
        [ConfigurationProperty("common-new-command-line-timeout", IsRequired = false)]
        public int? NewCommandTimeout
        {
            get { return (int?)this["common-new-command-line-timeout"]; }
            set { this["common-new-command-line-timeout"] = value; }
        }
        [ConfigurationProperty("common-grid-url", IsRequired = false)]
        public string GridUrl
        {
            get { return this["common-grid-url"].ToString().Trim(); }
            set { this["common-grid-url"] = value; }
        }
        [ConfigurationProperty("common-use-saucelabs", IsRequired = false)]
        public Boolean? UseSauceLabs
        {
            get { return (Boolean?)this["common-use-saucelabs"]; }
            set { this["common-use-saucelabs"] = value; }
        }

        [ConfigurationProperty("common-saucelabs-username", IsRequired = false)]
        public string SauceLabsUserName
        {
            get { return this["common-saucelabs-username"].ToString().Trim(); }
            set { this["common-saucelabs-username"] = value; }
        }
        [ConfigurationProperty("common-saucelabs-accesskey", IsRequired = false)]
        public string SauceLabsAccessKey
        {
            get { return this["common-saucelabs-accesskey"].ToString().Trim(); }
            set { this["common-saucelabs-accesskey"] = value; }
        }

        [ConfigurationProperty("common-app", IsRequired = false)]
        public string App
        {
            get { return this["common-app"].ToString().Trim(); }
            set { this["common-app"] = value; }
        }
        
        [ConfigurationProperty("common-autoLaunch", IsRequired = false)]
        public Boolean? AutoLaunch
        {
            get { return (Boolean?)this["common-autoLaunch"]; }
            set { this["common-autoLaunch"] = value; }
        }        
        [ConfigurationProperty("common-language", IsRequired = false)]
        public string Language
        {
            get { return this["common-language"].ToString().Trim(); }
            set { this["common-language"] = value; }
        }
        [ConfigurationProperty("common-locale", IsRequired = false)]
        public string Locale
        {
            get { return this["common-locale"].ToString().Trim(); }
            set { this["common-locale"] = value; }
        }
        [ConfigurationProperty("common-udid", IsRequired = false)]
        public string UDID
        {
            get { return this["common-udid"].ToString().Trim(); }
            set { this["common-udid"] = value; }
        }
        [ConfigurationProperty("common-orientation", IsRequired = false)]
        public string Orientation
        {
            get { return this["common-orientation"].ToString().Trim(); }
            set { this["common-orientation"] = value; }
        }
        [ConfigurationProperty("common-autoWebview", IsRequired = false)]
        public Boolean? AutoWebview
        {
            get { return (Boolean?)this["common-autoWebview"]; }
            set { this["common-autoWebview"] = value; }
        }
        [ConfigurationProperty("common-noReset", IsRequired = false)]
        public Boolean? NoReset
        {
            get { return (Boolean?)this["common-noReset"]; }
            set { this["common-noReset"] = value; }
        }
        [ConfigurationProperty("common-fullReset", IsRequired = false)]
        public Boolean? FullReset
        {
            get { return (Boolean?)this["common-fullReset"]; }
            set { this["common-fullReset"] = value; }
        }

        //******* ANDROID **********//
        //******* ANDROID **********//
        //******* ANDROID **********//
        [ConfigurationProperty("android-appActivity", IsRequired = false)]
        public string AndroidAppActivity
        {
            get { return this["android-appActivity"].ToString().Trim(); }
            set { this["android-appActivity"] = value; }
        }
        [ConfigurationProperty("android-appPackage", IsRequired = false)]
        public string AndroidAppPackage
        {
            get { return this["android-appPackage"].ToString().Trim(); }
            set { this["android-appPackage"] = value; }
        }
        [ConfigurationProperty("android-appWaitActivity", IsRequired = false)]
        public string AndroidAppWaitActivity
        {
            get { return this["android-appWaitActivity"].ToString().Trim(); }
            set { this["android-appWaitActivity"] = value; }
        }
        [ConfigurationProperty("android-appWaitPackage", IsRequired = false)]
        public string AndroidAppWaitPackage
        {
            get { return this["android-appWaitPackage"].ToString().Trim(); }
            set { this["android-appWaitPackage"] = value; }
        }
        [ConfigurationProperty("android-deviceReadyTimeout", IsRequired = false)]
        public int? AndroidReadyTimeout
        {
            get { return (int?)this["android-deviceReadyTimeout"]; }
            set { this["android-deviceReadyTimeout"] = value; }
        }
        [ConfigurationProperty("android-androidCoverage", IsRequired = false)]
        public Boolean? AndroidCoverage
        {
            get { return (Boolean?)this["android-androidCoverage"]; }
            set { this["android-androidCoverage"] = value; }
        }
        [ConfigurationProperty("android-androidDeviceReadyTimeout", IsRequired = false)]
        public int? AndroidReadyTimeoutAfterBoot
        {
            get { return (int?)this["android-androidDeviceReadyTimeout"]; }
            set { this["android-androidDeviceReadyTimeout"] = value; }
        }

        [ConfigurationProperty("android-avd", IsRequired = false)]
        public string AndroidAVDName
        {
            get { return this["android-avd"].ToString().Trim(); }
            set { this["android-avd"] = value; }
        }
        [ConfigurationProperty("android-avdLaunchTimeout", IsRequired = false)]
        public int? AndroidAVDLaunchTimeout
        {
            get { return (int?)this["android-avdLaunchTimeout"]; }
            set { this["android-avdLaunchTimeout"] = value; }
        }
        [ConfigurationProperty("android-avdReadyTimeout", IsRequired = false)]
        public int? AndroidAVDReadyTimeout
        {
            get { return (int?)this["android-avdReadyTimeout"]; }
            set { this["android-avdReadyTimeout"] = value; }
        }
        [ConfigurationProperty("android-avdArgs", IsRequired = false)]
        public string AndroidAVDArguments
        {
            get { return this["android-avdArgs"].ToString().Trim(); }
            set { this["android-avdArgs"] = value; }
        }
        [ConfigurationProperty("android-useKeystore", IsRequired = false)]
        public Boolean? AndroidUseKeystore
        {
            get { return (Boolean?)this["android-useKeystore"]; }
            set { this["android-useKeystore"] = value; }
        }
        [ConfigurationProperty("android-keystorePath", IsRequired = false)]
        public string AndroidKeystorePath
        {
            get { return this["android-keystorePath"].ToString().Trim(); }
            set { this["android-keystorePath"] = value; }
        }
        [ConfigurationProperty("android-keystorePassword", IsRequired = false)]
        public string AndroidKeystorePassword
        {
            get { return this["android-keystorePassword"].ToString().Trim(); }
            set { this["android-keystorePassword"] = value; }
        }
        [ConfigurationProperty("android-keyAlias", IsRequired = false)]
        public string AndroidKeyAlias
        {
            get { return this["android-keyAlias"].ToString().Trim(); }
            set { this["android-keyAlias"] = value; }
        }
        [ConfigurationProperty("android-keyPassword", IsRequired = false)]
        public string AndroidKeyPassword
        {
            get { return this["android-keyPassword"].ToString().Trim(); }
            set { this["android-keyPassword"] = value; }
        }
        [ConfigurationProperty("android-autoWebviewTimeout", IsRequired = false)]
        public int? AndroidAutoWebViewTimeout
        {
            get { return (int?)this["android-autoWebviewTimeout"]; }
            set { this["android-autoWebviewTimeout"] = value; }
        }
        [ConfigurationProperty("android-intentAction", IsRequired = false)]
        public string AndroidIntentAction
        {
            get { return this["android-intentAction"].ToString().Trim(); }
            set { this["android-intentAction"] = value; }
        }

        [ConfigurationProperty("android-intentCategory", IsRequired = false)]
        public string AndroidIntentCategory
        {
            get { return this["android-intentCategory"].ToString().Trim(); }
            set { this["android-intentCategory"] = value; }
        }
        [ConfigurationProperty("android-intentFlags", IsRequired = false)]
        public string AndroidIntentFlags
        {
            get { return this["android-intentFlags"].ToString().Trim(); }
            set { this["android-intentFlags"] = value; }
        }
        [ConfigurationProperty("android-optionalIntentArguments", IsRequired = false)]
        public string AndroidOptionalIntentArguments
        {
            get { return this["android-optionalIntentArguments"].ToString().Trim(); }
            set { this["android-optionalIntentArguments"] = value; }
        }
        [ConfigurationProperty("android-stopAppOnReset", IsRequired = false)]
        public Boolean? AndroidStopAppOnReset
        {
            get { return (Boolean?)this["android-stopAppOnReset"]; }
            set { this["android-stopAppOnReset"] = value; }
        }
        [ConfigurationProperty("android-unicodeKeyboard", IsRequired = false)]
        public Boolean? AndroidEnableUnicodeInput
        {
            get { return (Boolean?)this["android-unicodeKeyboard"]; }
            set { this["android-unicodeKeyboard"] = value; }
        }

        [ConfigurationProperty("android-resetKeyboard", IsRequired = false)]
        public Boolean? AndroidResetKeyboard
        {
            get { return (Boolean?)this["android-resetKeyboard"]; }
            set { this["android-resetKeyboard"] = value; }
        }

        [ConfigurationProperty("android-noSign", IsRequired = false)]
        public Boolean? AndroidNoSigning
        {
            get { return (Boolean?)this["android-noSign"]; }
            set { this["android-noSign"] = value; }
        }

        [ConfigurationProperty("android-ignoreUnimportantViews", IsRequired = false)]
        public Boolean? AndroidIgnoreUnimportantViews
        {
            get { return (Boolean?)this["android-ignoreUnimportantViews"]; }
            set { this["android-ignoreUnimportantViews"] = value; }
        }
        [ConfigurationProperty("android-enablePerformanceLogging", IsRequired = false)]
        public Boolean? AndroidEnablePerformanceLogging
        {
            get { return (Boolean?)this["android-enablePerformanceLogging"]; }
            set { this["android-enablePerformanceLogging"] = value; }
        }
        [ConfigurationProperty("android-chromedriverExecutable", IsRequired = false)]
        public string AndroidChromeDriverExecutable
        {
            get { return this["android-chromedriverExecutable"].ToString().Trim(); }
            set { this["android-chromedriverExecutable"] = value; }
        }
        [ConfigurationProperty("android-androidDeviceSocket", IsRequired = false)]
        public int? AndroidDeviceSocket
        {
            get { return (int?)this["android-androidDeviceSocket"]; }
            set { this["android-androidDeviceSocket"] = value; }
        }

        //******* IOS ***********//
        //******* IOS ***********//
        //******* IOS ***********//
        [ConfigurationProperty("ios-calendarFormat", IsRequired = false)]
        public string IOSCalendarFormat
        {
            get { return this["ios-calendarFormat"].ToString().Trim(); }
            set { this["ios-calendarFormat"] = value; }
        }
        [ConfigurationProperty("ios-bundleId", IsRequired = false)]
        public string IOSBundleId
        {
            get { return this["ios-bundleId"].ToString().Trim(); }
            set { this["ios-bundleId"] = value; }
        }
        [ConfigurationProperty("ios-launchTimeout", IsRequired = false)]
        public int? IOSLaunchTimeout
        {
            get { return (int?)this["ios-launchTimeout"]; }
            set { this["ios-launchTimeout"] = value; }
        }
        [ConfigurationProperty("ios-locationServicesEnabled", IsRequired = false)]
        public Boolean? IOSLocationServicesEnabled
        {
            get { return (Boolean?)this["ios-locationServicesEnabled"]; }
            set { this["ios-locationServicesEnabled"] = value; }
        }
        [ConfigurationProperty("ios-locationServicesAuthorized", IsRequired = false)]
        public Boolean? IOSLocationServicesAuthorized
        {
            get { return (Boolean?)this["ios-locationServicesAuthorized"]; }
            set { this["ios-locationServicesAuthorized"] = value; }
        }
        [ConfigurationProperty("ios-autoAcceptAlerts", IsRequired = false)]
        public Boolean? IOSAutoAcceptAlerts
        {
            get { return (Boolean?)this["ios-autoAcceptAlerts"]; }
            set { this["ios-autoAcceptAlerts"] = value; }
        }
        [ConfigurationProperty("ios-autoDismissAlerts", IsRequired = false)]
        public Boolean? IOSAutoDismissAlerts
        {
            get { return (Boolean?)this["ios-autoDismissAlerts"]; }
            set { this["ios-autoDismissAlerts"] = value; }
        }
        [ConfigurationProperty("ios-nativeInstrumentsLib", IsRequired = false)]
        public Boolean? IOSNativeInstrumentsLib
        {
            get { return (Boolean?)this["ios-nativeInstrumentsLib"]; }
            set { this["ios-nativeInstrumentsLib"] = value; }
        }
        [ConfigurationProperty("ios-nativeWebTap", IsRequired = false)]
        public Boolean? IOSNativeWebTap
        {
            get { return (Boolean?)this["ios-nativeWebTap"]; }
            set { this["ios-nativeWebTap"] = value; }
        }
        [ConfigurationProperty("ios-safariInitialUrl", IsRequired = false)]
        public string IOSSafariInitialUrl
        {
            get { return this["ios-safariInitialUrl"].ToString().Trim(); }
            set { this["ios-safariInitialUrl"] = value; }
        }    
        [ConfigurationProperty("ios-safariAllowPopups", IsRequired = false)]
        public Boolean? IOSSafariAllowPopups
        {
            get { return (Boolean?)this["ios-safariAllowPopups"]; }
            set { this["ios-safariAllowPopups"] = value; }
        }
        [ConfigurationProperty("ios-safariIgnoreFraudWarning", IsRequired = false)]
        public Boolean? IOSSafariIgnoreFraudWarnings
        {
            get { return (Boolean?)this["ios-safariIgnoreFraudWarning"]; }
            set { this["ios-safariIgnoreFraudWarning"] = value; }
        }
        [ConfigurationProperty("ios-safariOpenLinksInBackground", IsRequired = false)]
        public Boolean? IOSSafariOpenLinksInBackground
        {
            get { return (Boolean?)this["ios-safariOpenLinksInBackground"]; }
            set { this["ios-safariOpenLinksInBackground"] = value; }
        }
        [ConfigurationProperty("ios-keepKeyChains", IsRequired = false)]
        public Boolean? IOSKeepKeyChains
        {
            get { return (Boolean?)this["ios-keepKeyChains"]; }
            set { this["ios-keepKeyChains"] = value; }
        }
        [ConfigurationProperty("ios-localizableStringsDir", IsRequired = false)]
        public string IOSLocalizableStringsDirectory
        {
            get { return this["ios-localizableStringsDir"].ToString().Trim(); }
            set { this["ios-localizableStringsDir"] = value; }
        } 
        [ConfigurationProperty("ios-processArguments", IsRequired = false)]
        public string IOSProcessArguments
        {
            get { return this["ios-processArguments"].ToString().Trim(); }
            set { this["ios-processArguments"] = value; }
        } 
        [ConfigurationProperty("ios-interKeyDelay", IsRequired = false)]
        public int? IOSInterKeyDelay
        {
            get { return (int?)this["ios-interKeyDelay"]; }
            set { this["ios-interKeyDelay"] = value; }
        }
        [ConfigurationProperty("ios-showIOSLog", IsRequired = false)]
        public Boolean? IOSShowIOSLog
        {
            get { return (Boolean?)this["ios-showIOSLog"]; }
            set { this["ios-showIOSLog"] = value; }
        }
        [ConfigurationProperty("ios-sendKeyStrategy", IsRequired = false)]
        public string IOSSendKeyStrategy
        {
            get { return this["ios-sendKeyStrategy"].ToString().Trim(); }
            set { this["ios-sendKeyStrategy"] = value; }
        }
        [ConfigurationProperty("ios-screenshotWaitTimeout", IsRequired = false)]
        public int? IOSScreenShotWaitTimeout
        {
            get { return (int?)this["ios-screenshotWaitTimeout"]; }
            set { this["ios-screenshotWaitTimeout"] = value; }
        }
        [ConfigurationProperty("ios-waitForAppScript", IsRequired = false)]
        public Boolean? IOSWaitForAppScript
        {
            get { return (Boolean?)this["ios-waitForAppScript"]; }
            set { this["ios-waitForAppScript"] = value; }
        }
    }
}
