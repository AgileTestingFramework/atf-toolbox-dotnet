using atf.toolbox.configuration;
using atf.toolbox.managers;
using JSErrorCollector;
using log4net;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace atf.toolbox
{
    /// <summary>
    /// ATFHandler
    /// </summary>
    public class ATFHandler
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ATFHandler));

        private TestContext _testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region ATFHandler Singleton Implementation
        private static readonly ATFHandler instance = new ATFHandler();
        private ATFHandler() { }
        public static ATFHandler Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region Configuration
        
        private Dictionary<string, ConfigurationSection> _Configuration = new Dictionary<string, ConfigurationSection>();
        
        /// <summary>
        /// AddConfiguration
        /// If the key already exists, the Configuration Section will be replaced within the collection
        /// </summary>
        /// <param name="key">Name of configuration section being added</param>
        /// <param name="config">Configuration Section</param>
        public void AddConfiguration(string key, ConfigurationSection config)
        {
            if (_Configuration.ContainsKey(key))
            {
                _log.Info("Replaced configuration section for key :" + key);
                _Configuration[key] = config;
            }
            else
            {
                _log.Info("Added configuration section with key: " + key);
                _Configuration.Add(key, config);
            }
        }
        
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="key">Name of the configuration section being requested</param>
        /// <returns>Configuration Section for the key requested</returns>
        public ConfigurationSection Configuration(string key)
        {
            if (_Configuration.ContainsKey(key))
            {
                return _Configuration[key];
            }
            else
            {
                _log.Warn("Unable to locate Configuration Section for key: " + key + " returning null.");
                return null;
            }
        }

        /// <summary>
        /// ReportConfiguration
        /// Configuration section reporting
        /// </summary>
        public ReportSettings ReportConfiguration
        {
            get { return (ReportSettings)ConfigurationManager.GetSection("ReportSettings"); }
        }

        #endregion Configuration

        #region WebDriver

        private volatile WebAutomationManager _webAutomationInstance;
        private object _syncRootWebDriver = new Object();

        /// <summary>
        /// WebAutomation
        /// Instance of Web Automation class to automation web applications and websites
        /// </summary>
        public WebAutomationManager WebAutomation
        {
            get
            {
                if (_webAutomationInstance == null)
                {
                    _log.Info("Attempting to get new instance of WebAutomation.");
                    lock (_syncRootWebDriver)
                    {
                        if (_webAutomationInstance == null)
                        {
                            _log.Info("Attempting to setup new webdriver.");
                            _webAutomationInstance = new WebAutomationManager();
                        }
                    }
                }

                return _webAutomationInstance;
            }
        }


        #endregion

        #region Forms

        private volatile FormAutomationManager _formsAutomationInstance;
        private object _syncRootFormsApplication = new Object();

        /// <summary>
        /// FormAutomation
        /// Instance of Form Automation class to automation winform and wpf applications
        /// </summary>
        public FormAutomationManager FormAutomation
        {
            get
            {
                if (_formsAutomationInstance == null)
                {
                    _log.Info("Attempting to get new instance of FormAutomation.");
                    lock (_syncRootFormsApplication)
                    {
                        if (_formsAutomationInstance == null)
                        {
                            _log.Info("Attempting to setup new application driver.");
                            _formsAutomationInstance = new FormAutomationManager();
                        }
                    }
                }

                return _formsAutomationInstance;
            }
        }

        #endregion

        #region Mobile

        private volatile MobileAutomationManager _mobileDriverInstance;
        private object _syncRootMobile = new Object();

        /// <summary>
        /// MobileAutomation
        /// Instance of Mobile Automation class to automation mobile android and IOS
        /// </summary>
        public MobileAutomationManager MobileAutomation
        {
            get
            {
                if (_mobileDriverInstance == null)
                {
                    _log.Info("Attempting to get new instance of MobileAutomation.");
                    lock (_syncRootDB)
                    {
                        if (_mobileDriverInstance == null)
                        {
                            _log.Info("Attempting to setup new mobile driver.");
                            _mobileDriverInstance = new MobileAutomationManager();
                        }
                    }
                }

                return _mobileDriverInstance;
            }
        }

        #endregion

        #region Database

        private volatile DatabaseAutomationManager _databaseAutomationInstance;
        private object _syncRootDB = new Object();

        /// <summary>
        /// MobileAutomation
        /// Instance of Mobile Automation class to automation mobile android and IOS
        /// </summary>
        public DatabaseAutomationManager DatabaseAutomation
        {
            get
            {
                if (_databaseAutomationInstance == null)
                {
                    _log.Info("Attempting to get new instance of DatabaseAutomation.");
                    lock (_syncRootDB)
                    {
                        if (_databaseAutomationInstance == null)
                        {
                            _log.Info("Attempting to setup new database driver.");
                            _databaseAutomationInstance = new DatabaseAutomationManager();
                        }
                    }
                }

                return _databaseAutomationInstance;
            }
        }

        #endregion

        #region ImageRecognition

        private volatile ImageAutomationManager _imageRecognitionInstance;
        private object _syncRootImage = new Object();

        /// <summary>
        /// ImageAutomation
        /// Instance of Image Automation class to automation images
        /// </summary>
        public ImageAutomationManager ImageAutomation
        {
            get
            {
                if (_imageRecognitionInstance == null)
                {
                    _log.Info("Attempting to get new instance of ImageAutomation.");
                    lock (_syncRootImage)
                    {
                        if (_imageRecognitionInstance == null)
                        {
                            _log.Info("Attempting to setup new image driver.");
                            _imageRecognitionInstance = new ImageAutomationManager();
                        }
                    }
                }

                return _imageRecognitionInstance;
            }
        }

        #endregion

        /// <summary>
        /// TearDown
        /// </summary>
        public void TearDown()
        {
            // Web
            if (_webAutomationInstance != null)
            {
                if (_webAutomationInstance.WebDriver != null)
                {
                    // Log the js error collected if this reporting is on and we were using a firefox driver
                    if (_webAutomationInstance.WebDriver is FirefoxDriver && ReportConfiguration.JSErrorCollectionWithFireFox)
                    {
                        List<JavaScriptError> jsErrors = JavaScriptError.ReadErrors(_webAutomationInstance.WebDriver) as List<JavaScriptError>;
                        foreach (JavaScriptError jsErr in jsErrors)
                        {
                            _log.Error("JS ERROR CAPTURED: LINE: " + jsErr.LineNumber + " SOURCE: " + jsErr.SourceName + " ERROR: " + jsErr.ErrorMessage);
                        }
                    }
                }

                WebAutomation.TearDown();
            }

            // Forms
            if (_formsAutomationInstance != null)
            {
                _formsAutomationInstance.TearDown();
            }

            // Mobile
            if (_mobileDriverInstance != null)
            {
                _mobileDriverInstance.TearDown();
            }

            // Image
            if (_imageRecognitionInstance != null)
            {
                _imageRecognitionInstance.TearDown();
            }
        }
    }
}
