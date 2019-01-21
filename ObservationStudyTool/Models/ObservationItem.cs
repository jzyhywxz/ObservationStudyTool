
using System;
using ObservationStudyTool.Helpers;

namespace ObservationStudyTool.Models
{
    public class ObservationItem : ViewModelBase
    {
        public ObservationItem()
        {
            // set default values
            Created = DateTime.Now;
            TaskType = Models.TaskType.Task_Other;
            TaskDescription = string.Empty;
            //TaskNumber = TaskNumberEnum.T1;
            //IsSelfInitiatedCs = true;
        }

        #region PROPERTIES

        private DateTime _created;
        public DateTime Created
        {
            get
            {
                return _created;
            }
            set
            {
                if (_created != value)
                {
                    _created = value;
                    OnPropertyChanged("Created");
                }
            }
        }

        private TimeSpan _taskDuration;
        public TimeSpan TaskDuration
        {
            get
            {
                return _taskDuration;
            }
            set
            {
                if (_taskDuration != value)
                {
                    _taskDuration = value;
                    OnPropertyChanged("TaskDuration");
                }
            }
        }

        //public bool IsHardCs { get; set; }
        //public bool IsSelfInitiatedCs { get; set; }
        //public ReasonForCsEnum ReasonForCs { get; set; }
        //public ActivityCategoryEnum ActivityCategory { get; set; }
        public string ActivityCategory { get; set; }

        //public TaskNumberEnum TaskNumber { get; set; }
        //public string TaskNumber { get; set; }
        public TaskType TaskType { get; set; }
        public string TaskDescription { get; set; }
        //public string ObserverComment { get; set; }

        #endregion

        public override string ToString()
        {
            //Created; TaskDuration; TaskDescription; TaskNumber; Application; ActivityCategory; ReasonForCs

            //var tempApplication = Mapper.GetApplication(TaskDescription.Trim()); //disabled mapper
            //var tempApplication = TaskDescription.Trim();
            //var tempActivityCategory = Mapper.GetActivityCategory(tempApplication, TaskDescription.Trim());
            var tempTaskIdentifier = Mapper.GetTaskIdentifier(TaskType, TaskDescription);

            return Created.ToLongTimeString() + ";" +
                   TaskDuration + ";" +
                   TaskType + ";" + 
                   TaskDescription.Replace(';', ',') + ";" +
                   tempTaskIdentifier + ";";
                    //";" + //TaskNumber by hand in Excel
                    //tempApplication + ";" + //Application by hand in Excel
                    //tempActivityCategory + ";" + //ActivityCategory by hand in Excel
                    //ReasonForCs + ";";
        }
    }

    //public enum ReasonForCsEnum
    //{
    //    None, Completed_Task, Blocked_Task, Self_Interruption, External_Interruption, Other
    //}

    //public enum ActivityCategoryEnum
    //{
    //    Working_In_The_Ide, Reading_Editing_Documents, Email_Planning, Social_Media_Communication, Meetings_Planned, Meetings_Informal_Adhoc, WebBrowsing_WorkRelated, WebBrowsing_WorkUnRelated, Other
    //}

    //public enum TaskNumberEnum
    //{
    //    T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10
    //}
}
