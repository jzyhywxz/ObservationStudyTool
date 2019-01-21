
using ObservationStudyTool.Models;
using System.Linq;

namespace ObservationStudyTool.Helpers
{
    public class Mapper
    {
        public static string GetApplication(string desc)
        {
            var colonIndex = desc.IndexOf(':');

            if (colonIndex > 0)
            {
                return desc.Substring(0, colonIndex);
            } 


            //if (desc.StartsWith("chrome") && desc.Contains("atlassin") ||
            //    desc.StartsWith("chrome") && desc.Contains("atlassian") ||
            //    desc.StartsWith("PlanningApp1"))
            //{
            //    return "PlanningApp1";
            //}
            //if (desc.StartsWith("chrome") && desc.Contains("gmail") ||
            //    desc.StartsWith("EmailApp1"))
            //{
            //    return "EmailApp1";
            //}
            //if (desc.StartsWith("chrome") && desc.Contains("google docs") ||
            //    desc.StartsWith("OfficeApp1"))
            //{
            //    return "OfficeApp1";
            //}
            //if (desc.StartsWith("chrome") && desc.Contains("calendar"))
            //{
            //    return "Calendar (Chrome)";
            //}
            //if (desc.StartsWith("chrome") && desc.Contains("rally"))
            //{
            //    return "Rally (Chrome)";
            //}
            //if (desc.StartsWith("chrome") && desc.Contains("gerrit"))
            //{
            //    return "Gerrit (Chrome)";
            //}
            //if (desc.StartsWith("notepad"))
            //{
            //    return "Notepad++";
            //}
            //if (desc.StartsWith("chrome"))
            //{
            //    return "Chrome";
            //}
            //if (desc.StartsWith("skype"))
            //{
            //    return "Skype";
            //}
            //if (desc.StartsWith("lync"))
            //{
            //    return "Lync";
            //}
            //if (desc.StartsWith("eclipse"))
            //{
            //    return "Eclipse";
            //}
            //if (desc.StartsWith("vs") || desc.StartsWith("visual studio"))
            //{
            //    return "Visual Studio";
            //}
            //if (desc.StartsWith("outlook"))
            //{
            //    return "Outlook";
            //}
            //if (desc.StartsWith("tasktop sync"))
            //{
            //    return "Tasktop Sync Studio";
            //}
            //if (desc.StartsWith("hp application lifecycle management") ||
            //    desc.StartsWith("hp alm"))
            //{
            //    return "HP ALM";
            //}
            //if (desc.StartsWith("explorer"))
            //{
            //    return "Explorer";
            //}
            //if (desc.StartsWith("sublime"))
            //{
            //    return "Sublime Text";
            //}
            //if (desc.StartsWith("console") ||
            //    desc.StartsWith("bash"))
            //{
            //    return "Console";
            //}
            //if (desc.StartsWith("webstorm"))
            //{
            //    return "WebStorm";
            //}
            //if (desc.StartsWith("finder"))
            //{
            //    return "Finder";
            //}
            //if (desc.StartsWith("snipping tool"))
            //{
            //    return "Snipping Tool";
            //}
            //if (desc.StartsWith("task manager"))
            //{
            //    return "Task Manager";
            //}
            

            return "?";
        }

        //public static string GetActivityCategory(string tempApplication, string desc)
        //{
        //    if (tempApplication.Contains("Atlassian") || tempApplication.Contains("PlanningApp1"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Rally") || tempApplication.Contains("PlanningApp5"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Gmail") || tempApplication.Contains("EmailApp1"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Calendar") || tempApplication.Contains("CalendarApp1"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Google Docs") || tempApplication.Contains("OfficeApp1"))
        //    {
        //        return "ReadingWriting";
        //    }
        //    if (tempApplication.Contains("Skype") || tempApplication.Contains("ImApp1"))
        //    {
        //        return "InformalMeeting";
        //    }
        //    if (tempApplication.Contains("Lync") || tempApplication.Contains("ImApp2"))
        //    {
        //        return "InformalMeeting";
        //    }
        //    if (tempApplication.Contains("Eclipse") && desc.Contains("debug") ||
        //        tempApplication.Contains("Visual Studio") && desc.Contains("debug") ||
        //        tempApplication.Contains("VS") && desc.Contains("debug") ||
        //        tempApplication.Contains("IdeApp") && desc.Contains("debug"))
        //    {
        //        return "Dev: Debugging";
        //    }
        //    if (tempApplication.Contains("Eclipse") && desc.Contains("code") ||
        //        tempApplication.Contains("Visual Studio") && desc.Contains("code") ||
        //        tempApplication.Contains("VS") && desc.Contains("code") ||
        //        tempApplication.Contains("IdeApp") && desc.Contains("code"))
        //    {
        //        return "Dev: Code";
        //    }
        //    if (tempApplication.Contains("Eclipse") && desc.Contains("workitems") ||
        //        tempApplication.Contains("Eclipse") && desc.Contains("work items"))
        //    {
        //        return "Dev: WorkItems";
        //    }
        //    if (tempApplication.Contains("Gerrit") || tempApplication.Contains("ReviewApp1"))
        //    {
        //        return "Dev: Reviews";
        //    }
        //    if (tempApplication.Contains("vcs") || tempApplication.Contains("VcsApp1"))
        //    {
        //        return "Dev: Changes";
        //    }
        //    if (tempApplication.Contains("WebStorm") || tempApplication.Contains("IdeApp2"))
        //    {
        //        return "Dev: Code";
        //    }
        //    if (tempApplication.Contains("Sublime Text") || tempApplication.Contains("EditorApp2"))
        //    {
        //        return "Dev: Code";
        //    }
        //    if (tempApplication.Contains("Console") || tempApplication.Contains("ConsoleApp2"))
        //    {
        //        return "Dev: TestingApplication";
        //    }
        //    if (tempApplication.Contains("Tasktop Sync Studio") || tempApplication.Contains("TestApp2"))
        //    {
        //        return "Dev: TestingApplication";
        //    }
        //    if (tempApplication.Contains("HP ALM") || tempApplication.Contains("PlanningApp4"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Outlook") || tempApplication.Contains("EmailApp2"))
        //    {
        //        return "EmailPlanning";
        //    }
        //    if (tempApplication.Contains("Explorer") ||
        //        tempApplication.Contains("Finder") ||
        //        tempApplication.Contains("Snipping Tool"))
        //    {
        //        return "Other";
        //    }


        //    return "?";
        //}

        public static string GetTaskIdentifier(TaskType type, string desc)
        {
            var identifier = "?";

            if (type != TaskType.Task_Other)
            {
                // split task type for "_" (as it should be task7_blabla9
                var underspaceIndex = type.ToString().IndexOf('_');

                if (underspaceIndex > 0)
                {
                    identifier = type.ToString().Substring(0, underspaceIndex);
                }
                // else: can't do automatically
            }
            else
            {
                // split task description for ":"
                var colonIndex = desc.IndexOf(':');

                // first column is taskIdentifier
                if (colonIndex > 0)
                {
                    identifier = desc.Substring(0, colonIndex);
                }
                // else: can't do automatically
            }

            // unify identifier
            identifier = identifier.ToLower().Replace(" ", "");

            return identifier;
        }
    }
}
