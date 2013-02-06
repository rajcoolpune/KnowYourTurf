﻿using System;
using CC.Core.CoreViewModelAndDTOs;

namespace KnowYourTurf.Core
{
    public class CalendarViewModel : ViewModel
    {
        public CalendarDefinition CalendarDefinition { get; set; }
        public string DeleteUrl { get; set; }

    }

    public class GetEventsViewModel:ViewModel
    {
        public double start { get; set; }
        public double end { get; set; }
    }

    public class CalendarDefinition
    {
        public string Url { get; set; }
        public string Title { get; set; }

        public string AddUpdateUrl { get; set; }
        public string AddUpdateTemplateUrl { get; set; }
        public string AddUpdateRoute { get; set; }

        public string EventChangedUrl { get; set; }

        public string DisplayUrl { get; set; }
        public string DisplayTemplateUrl { get; set; }
        public string DisplayRoute { get; set; }

        public string DeleteUrl { get; set; }
        public string PopupTitle { get; set; }
    }

    public class CalendarEvent
    {
        public int EntityId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string className { get; set; }
        public string color { get; set; }
    }
}