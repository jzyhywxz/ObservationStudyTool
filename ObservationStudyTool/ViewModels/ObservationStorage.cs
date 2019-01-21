using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ObservationStudyTool.Helpers;
using ObservationStudyTool.Models;

namespace ObservationStudyTool.ViewModels
{
    public class ObservationStorage : ViewModelBase
    {
        #region METHODS

        public static ObservationStorage GetInstance()
        {
            return _instance ?? (_instance = new ObservationStorage());
        }

        public void ExportData()
        {
            // backup existing export
            BackupPreviousExport();

            // run export
            var res = _instance.ExportToCsv();

            // delete created backup
            DeleteBackupExport(res);

            // handle export
            if (res == true)
            {
                LastSuccessfulExport = string.Format("Last successful export: {0} to {1}", DateTime.Now, _exportObservationDataFilePath);
            }
            else
            {
                MessageBox.Show("Failed to exported the observed data to " + _exportObservationDataFilePath + ". You can still find the last backup at " + _backupExportDataFilePath + ".", "Export failed", MessageBoxButton.OK);
            }
        }


        #region Export

        public bool ExportToCsv()
        {
            // get save path
            var savePath = GetExportDataFilePath();
            _previousExportDataFilePath = savePath;

            try
            {
                var data = _instance.ObservationItems;
                using (var stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    var cs = new CsvSerializer<ObservationItem>();
                    cs.Serialize(stream, data, ';');
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "An error occurred", MessageBoxButton.OK);
                return false;
            }
        }

        private string GetExportDataFilePath()
        {
            return _exportObservationDataFilePath;
        }

        public void SetExportDataPath(string savePath)
        {
            // add file name
            var fileName = string.Format("Observation_{0}.csv", DateTime.Now.ToString("yy-MM-dd_hh-mm-ss"));
            _exportObservationDataFilePath = Path.Combine(savePath, fileName);
        }

        public string GetPreviousExportDataFilePath()
        {
            return _previousExportDataFilePath;
        }

        #endregion

        #region Backup (before export)

        private void BackupPreviousExport()
        {
            if (string.IsNullOrEmpty(_previousExportDataFilePath) || !File.Exists(_previousExportDataFilePath))
            {
                LastSuccessfulExport = "Failed to backup the last saved file.";
            }
            else
            {
                _backupExportDataFilePath = _previousExportDataFilePath.Replace(".csv", ".backup.csv");
                File.Copy(_previousExportDataFilePath, _backupExportDataFilePath, true);
                LastSuccessfulExport = "Successfully saved the last saved file to " + _backupExportDataFilePath + ".";
            }
        }

        private void DeleteBackupExport(bool exportSucceeded)
        {
            if (exportSucceeded != true || !File.Exists(_backupExportDataFilePath)) return;

            
            File.Delete(_backupExportDataFilePath);
            _backupExportDataFilePath = string.Empty;
        }

        #endregion

        public void DeleteData()
        {
            _instance.ObservationItems.Clear();
        }

        #endregion

        #region PROPERTIES & FIELDS

        private string _exportObservationDataFilePath;
        private string _previousExportDataFilePath;
        private string _backupExportDataFilePath;

        private static ObservationStorage _instance;

        private ObservableCollection<ObservationItem> _observationItems;
        public ObservableCollection<ObservationItem> ObservationItems
        {
            get { return _observationItems ?? (_observationItems = new ObservableCollection<ObservationItem>()); }
            set
            {
                if (_observationItems != value)
                {
                    _observationItems = value;
                    OnPropertyChanged("ObservationItems");
                }
            }
        }

        private string _lastSuccessfulExport;
        public string LastSuccessfulExport
        {
            get { return _lastSuccessfulExport ?? (_lastSuccessfulExport = string.Empty);
            }
            set
            {
                if (_lastSuccessfulExport != value)
                {
                    _lastSuccessfulExport = value;
                    OnPropertyChanged("LastSuccessfulExport");
                }
            }
        }

        #endregion
    }
}
