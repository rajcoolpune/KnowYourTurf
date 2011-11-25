if (typeof kyt == 'undefined') {
    kyt = function() {
    }
}

if (typeof kyt.calendar == 'undefined') {
    kyt.calendar = function() {
    }
}

(function($) {
    $.fn.asCalendar = function(calendarDefinition, userOptions) {
        var calendarDefaultOptions = {
            header: {
				left: 'prev,next today',
				center: calendarDefinition.Title,
				right: 'month,agendaWeek,agendaDay'
			},
            editable:true,
            theme:true,
            firstDay:1,
            allDaySlot:false,
            allDayDefault:false,
            slotMinutes:15,
            events: calendarDefinition.Url,
            dayClick: function(date, allDay, jsEvent, view){ $.publish('/calendar_'+calendarDefinition.id+'/dayClick', [date, allDay, jsEvent, view]);},
            eventClick: function(calEvent, jsEvent, view){ $.publish('/calendar_'+calendarDefinition.id+'/eventClick', [calEvent, jsEvent, view]);},
            eventDrop: function(event, dayDelta,minuteDelta,allDay,revertFunc){ $.publish('/calendar_'+calendarDefinition.id+'/eventDrop', [event, dayDelta,minuteDelta,allDay,revertFunc]);},
            eventResize: function(event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){ $.publish('/calendar_'+calendarDefinition.id+'/eventResize', [event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ]);}
        };

        var calendarOptions = $.extend(calendarDefaultOptions, userOptions || {});
        var calendar = this;
        calendar.fullCalendar(calendarOptions);
        return calendar;
    }
})(jQuery);