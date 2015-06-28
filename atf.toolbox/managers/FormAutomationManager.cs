using atf.toolbox.configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace atf.toolbox.managers
{
    public class FormAutomationManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(FormAutomationManager));

        public Application ApplicationDriver { get; set; }

        /// <summary>
        /// FormsConfiguration
        /// Configuration section FormSettings to be used to setup the application driver for the test run
        /// </summary>
        public FormSettings FormsConfiguration
        {
            get { return (FormSettings)ConfigurationManager.GetSection("FormSettings"); }
        }

        public FormAutomationManager()
        {
            ApplicationDriver = ApplicationSetup();
        }

        public void TearDown()
        {
            if (ApplicationDriver != null)
            {
                ApplicationDriver.Close();
            }
        }

        /// <summary>
        /// ApplicationLaunch
        /// ApplicationDriver will be set to the application launched
        /// </summary>
        /// <param name="alwaysLaunchNewInstance">Launch a new instance if TRUE, will only launch a new instance if one is not found if FALSE</param>
        /// <param name="applicationDirectory">location of the application to launch</param>
        /// <param name="applicationStartFileName">filename of the application to launch</param>
        /// <param name="applicationLaunchParameters">launch parameters of the application to launch</param>
        public void ApplicationLaunch(string applicationDirectory, string applicationStartFileName, string applicationLaunchParameters, bool alwaysLaunchNewInstance)
        { 
        
        }

        public void ApplicationLaunch()
        {
            TearDown();
            ApplicationDriver = ApplicationSetup();
        }
        
        private Application ApplicationSetup()
        { 
            return ApplicationSetup(true, null, null, null, null);
        }

        private Application ApplicationSetup(bool useConfiguration, string applicationDirectory, string applicationStartFileName, string applicationLaunchParameters, bool? alwaysLaunchNewInstance)
        {
            Application application = null;
            String _applicationDirectory;
            String _applicationName;
            String _applicationLaunchParameters;
            bool? _alwaysLaunchNewInstance;

            try
            {
                if (useConfiguration && FormsConfiguration == null)
                {
                    _log.Warn("No forms settings configuration loaded.");
                    return application;
                }

                if (useConfiguration)
                {
                    _applicationDirectory = FormsConfiguration.ApplicationDirectory;
                    _applicationName = FormsConfiguration.ApplicationStartFileName;
                    _applicationLaunchParameters = FormsConfiguration.ApplicationLaunchParameters;
                    _alwaysLaunchNewInstance = FormsConfiguration.AlwaysLaunchNewInstance;
                }
                else 
                {
                    _applicationDirectory = applicationDirectory;
                    _applicationName = applicationStartFileName;
                    _applicationLaunchParameters = applicationLaunchParameters;
                    _alwaysLaunchNewInstance = alwaysLaunchNewInstance;
                }

                String applicationPath = Path.Combine(_applicationDirectory, _applicationName);

                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = applicationPath;

                if (_applicationLaunchParameters != String.Empty)
                {
                    processInfo.Arguments = _applicationLaunchParameters;
                }

                if (_alwaysLaunchNewInstance == null || _alwaysLaunchNewInstance.Value == true)
                {
                    application = Application.Launch(processInfo);

                }
                else
                {
                    application = Application.AttachOrLaunch(processInfo);
                }

            }
            catch (ArgumentNullException ane)
            {
                _log.Error("Error initializing form driver. Null encountered.", ane);
                throw ane;
            }
            catch (WhiteException we)
            {
                _log.Error("Error initializing form driver.", we);
                throw we;
            }
            catch (Exception ex)
            {
                _log.Error("Unknown Error initializing form driver.", ex);
                throw ex;
            }

            return application;
        }

        #region Screen Object Helpers

        /// <summary>
        /// GetActiveWindow
        /// </summary>
        /// <returns>Returns the currently active window for the application</returns>
        public Window GetActiveWindow()
        {
            List<Window> windows = ApplicationDriver.GetWindows();

            foreach (Window window in windows)
            {
                if (window.IsCurrentlyActive) return window;
            }

            return null;
        }

        /// <summary>
        /// ClickButton
        /// Focus and click a button
        /// </summary>
        /// <param name="btnToClick">Button to focus and click on</param>
        public void ClickButton(Button btnToClick)
        {
            btnToClick.Focus();
            btnToClick.Click();
        }

        /// <summary>
        /// GetWithWait - Implicit wait One Second
        /// </summary>
        /// <param name="window">Window to locate UIItem</param>
        /// <param name="criteria">Criteria to locate UIItem</param>
        /// <returns>UIItem if located within the wait time, NULL if unable to locate within the wait time</returns>
        public T GetWithWait<T>(Window window, SearchCriteria criteria) where T : IUIItem
        {
            return GetWithWait<T>(window, criteria, TimeSpan.FromSeconds(1));

        }

        /// <summary>
        /// GetWithWait - Implicit wait
        /// </summary>
        /// <param name="window">Window to locate UIItem</param>
        /// <param name="criteria">Criteria to locate UIItem</param>
        /// <param name="waitTime">Time to wait and try to locate the UIItem</param>
        /// <returns>UIItem if located within the wait time, NULL if unable to locate within the wait time</returns>
        public T GetWithWait<T>(Window window, SearchCriteria criteria, TimeSpan waitTime) where T : IUIItem
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            do
            {
                try
                {
                    T control = window.Get<T>(criteria);
                    return control; // item was found, stop waiting and return
                }
                catch (Exception) { }
            } while (stopWatch.Elapsed.Seconds <= waitTime.Seconds);
            stopWatch.Stop();

            return default(T); // item was not found within the wait timer
        }

        #endregion
    }
}
