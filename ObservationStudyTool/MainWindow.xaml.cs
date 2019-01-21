using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ObservationStudyTool.Models;
using ObservationStudyTool.ViewModels;

namespace ObservationStudyTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// Update V1.7 (28.02.2017): Added TaskType column to observation for easier task logging
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Version = "1.7";
        private bool _isPaused = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ObservationStorage.GetInstance();
            InitializeObservation();
        }

        /// <summary>
        /// Called on first start-up
        /// </summary>
        private void InitializeObservation()
        {
            // Enable / Disable Buttons
            StartObservationBtn.IsEnabled = true;
            EndObservationBtn.IsEnabled = false;
            LogGrid.IsEnabled = false;
            //PauseObservationBtn.IsEnabled = false;
            ManualSaveBtn.IsEnabled = false;
            //PauseObservationBtn.Content = "Continue";
        }

        #region SetUp, Pause, Continue, ShutDown Logging

        private void SetUpLogging()
        {
            DeleteData();

            ObservationStorage.GetInstance().ObservationItems.Add(new ObservationItem()
            {
                Created = DateTime.Now,
                //ActivityCategory = ActivityCategoryEnum.Other,
                //IsHardCs = true,
                //IsSelfInitiatedCs = false,
                //ObserverComment = String.Empty,
                //ReasonForCs = ReasonForCsEnum.Other,
                TaskType = Models.TaskType.Task6_StudyRelated,
                TaskDescription = "Observation Started",
                //TaskNumber = TaskNumberEnum.T0
            });

            // Enable / Disable Buttons
            StartObservationBtn.IsEnabled = false;
            EndObservationBtn.IsEnabled = true;
            LogGrid.IsEnabled = true;
            //PauseObservationBtn.IsEnabled = true;
            ManualSaveBtn.IsEnabled = true;
            //PauseObservationBtn.Content = "Pause";

            // Ask for Export Location
            SetExportDataLocation();
            ExportData();
        }

        private void PauseLogging()
        {
            //PauseObservationBtn.Content = "Continue";
            LogGrid.IsEnabled = false;

            ObservationStorage.GetInstance().ObservationItems.Add(new ObservationItem()
            {
                Created = DateTime.Now,
                //ActivityCategory = ActivityCategoryEnum.Other,
                //IsHardCs = true,
                //IsSelfInitiatedCs = false,
                //ObserverComment = String.Empty,
                TaskType = Models.TaskType.Task6_StudyRelated,
                TaskDescription = "Observation Paused",
                //TaskNumber = TaskNumberEnum.T0
            });

            ExportData();
        }

        private void ContinueLogging()
        {
            //PauseObservationBtn.Content = "Pause";
            LogGrid.IsEnabled = true;

            ObservationStorage.GetInstance().ObservationItems.Add(new ObservationItem()
            {
                Created = DateTime.Now,
                //ActivityCategory = ActivityCategoryEnum.Other,
                //IsHardCs = true,
                //IsSelfInitiatedCs = false,
                //ObserverComment = String.Empty,
                TaskType = Models.TaskType.Task6_StudyRelated,
                TaskDescription = "Observation continued",
                //TaskNumber = TaskNumberEnum.T0
            });

            ExportData();
        }

        private void ShutDownLogging()
        {
            // add timestamp to last entry
            var list = ObservationStorage.GetInstance().ObservationItems;
            var size = list.Count;

            if (size > 1)
            {
                var lastItem = list[list.Count-1];

                if (lastItem != null)
                {
                    lastItem.TaskDuration = (DateTime.Now - lastItem.Created);
                }
            }

            // add hint to logfile
            ObservationStorage.GetInstance().ObservationItems.Add(new ObservationItem()
            {
                Created = DateTime.Now,
                //ActivityCategory = ActivityCategoryEnum.Other,
                //IsHardCs = true,
                //IsSelfInitiatedCs = false,
                //ObserverComment = String.Empty,
                TaskType = Models.TaskType.Task6_StudyRelated,
                TaskDescription = "Observation ended",
                //TaskNumber = TaskNumberEnum.T0
            });

            // save
            ExportData();

            // Enable / Disable Buttons
            StartObservationBtn.IsEnabled = true;
            EndObservationBtn.IsEnabled = false;
            LogGrid.IsEnabled = false;
            //PauseObservationBtn.IsEnabled = false;
            ManualSaveBtn.IsEnabled = false;
            //PauseObservationBtn.Content = "Continue";

            // Observation finished
            MessageBox.Show("You find the exported observation data here: " +
                    ObservationStorage.GetInstance().GetPreviousExportDataFilePath() +
                    ".", "Observation finished", MessageBoxButton.OK);
        }

        #endregion

        /// <summary>
        /// This method is called always when a new ObservationItem is created 
        /// or the creation timestamp is changed. It calculates the duration for
        /// the previous entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="initializingNewItemEventArgs"></param>
        private void InitializingNewItemEventHandler(object sender, InitializingNewItemEventArgs initializingNewItemEventArgs)
        {
            // fill fields
            var list = ObservationStorage.GetInstance().ObservationItems;
            var size = list.Count;

            if (size > 1)
            {
                for (int i = 1; i < size; i++)
                {
                    var thisItem = list[i];
                    var previousItem = list[i - 1];

                    if (thisItem == null || previousItem == null) continue;
                    previousItem.TaskDuration = (thisItem.Created - previousItem.Created);
                }
            }

            // save all previous inputs
            ExportData();
        }

        private void SetExportDataLocation()
        {
            // dialog to get path to exported file
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Select the destination where you want to export the observation file to:";
            var result = dialog.ShowDialog();

            var savePath = result == System.Windows.Forms.DialogResult.OK
                ? dialog.SelectedPath
                : Directory.GetCurrentDirectory();

            ObservationStorage.GetInstance().SetExportDataPath(savePath);
        }

        private void ExportData()
        {
            ObservationStorage.GetInstance().ExportData();
        }

        private void DeleteData()
        {
            ObservationStorage.GetInstance().DeleteData();
        }

        private void ShowInfo()
        {
            MessageBox.Show("This tool was developed in 2014 by André Meyer, S.E.A.L., " +
                "University of Zurich\n\n\nYou can use this tool to log and annotate the " +
                "context switches you observe. Click 'Start' to start the observation. " +
                "You can pause and continue the logging at any time. If you 'stop' the " +
                "observation, you are (once) asked to export the observed data. " +
                "Otherwise, it will be deleted. Double click the last line or click " +
                "enter to create a new observation item.\n\n\nVersion: " + Version, "Info & Help", MessageBoxButton.OK);
        }

        #region Click Events

        private void StartObservationClicked(object sender, RoutedEventArgs e)
        {
            SetUpLogging();
        }

        private void PauseObservationClicked(object sender, RoutedEventArgs e)
        {
            if (_isPaused)
            {
                _isPaused = false;
                ContinueLogging();
            }
            else
            {
                _isPaused = true;
                PauseLogging();
            }
        }

        private void EndObservationClicked(object sender, RoutedEventArgs e)
        {
            ShutDownLogging();
        }

        private void InfoClicked(object sender, RoutedEventArgs e)
        {
            ShowInfo();
        }

        private void SaveObservationClicked(object sender, RoutedEventArgs e)
        {
            ExportData();
        }

        #endregion

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (ObservationStorage.GetInstance().ObservationItems.Count == 0) return;

            // Ask for export & delete data
            var ans = MessageBox.Show("Do you want to save the observed data before you close the application? It will be deleted otherwise.", "Export", MessageBoxButton.YesNo);

            if (ans == MessageBoxResult.Yes)
            {
                ExportData();
            }

            DeleteData();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                ExportData();
            }
        }
    }
}
