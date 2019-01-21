namespace ObservationStudyTool.Models
{
    /**
     *  Task 0: administrative work (e.g. answer company survey, reporting time at work, etc.)
     * 	Task 1: private stuff (work unrelated web browsing, smartphone use, eat/drink, toilet break)
	 * 	Task 2: Planned meeting, scrum meeting, etc.
     * 	Task 3: unplanned/informal meeting (e.g. a colleague coming by to ask a question. Also most Skype calls)
     * 	Task 4: awareness & team stuff (short chats or email checks)
	 * 	Task 5: planning (calendar, work item tracker in IDE or browser)
	 * 	Task 6: observation session / study related
     * 	Task 7: development task (can be a code review, exploratory/research task, bugfix or new feature)
     *  Task_Other: please state in description (and map later)
     */
    public enum TaskType
    {
        Task0_Administrative,
        Task1_Private,
        Task2_PlannedMeetings,
        Task3_InformalAdHocMeetings,
        Task4_Awareness_EmailsAndChats,
        Task5_Planning_CalendarWorkItems,
        Task6_StudyRelated,
        Task7_DevelopmentCoding,
        Task_Other
    }
}
