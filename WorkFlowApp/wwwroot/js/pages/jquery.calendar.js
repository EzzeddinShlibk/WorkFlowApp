document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['interaction', 'dayGrid', 'timeGrid'],
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        defaultDate: new Date(), 
        navLinks: true,
        selectable: true,
        selectMirror: true,
        select: function (arg) {
            var title = prompt('Event Title:');
            if (title) {
                // Save the event on the server
                saveEvent({
                    title: title,
                    start: arg.start,
                    end: arg.end,
                });
            }
            calendar.unselect();
        },
        editable: true,
        eventLimit: true,
        events: '/Calendar/GetEvents', // Adjust the URL based on your server endpoint
        eventClick: function (arg) {
            if (confirm('Delete event?')) {
                deleteEvent(arg.event.id); // Pass the event id for deletion
            }
        }
    });

    function saveEvent(eventData) {
        // Send event data to the server using AJAX
        $.ajax({
            type: 'POST',
            url: '/Calendar/SaveEvent', // Adjust the URL based on your server endpoint
            contentType: 'application/json',
            data: JSON.stringify(eventData),
            success: function (response) {
                // Handle the response if needed
                console.log(response);
                // Reload events after saving
                calendar.refetchEvents();
            },
            error: function (error) {
                // Handle the error if needed
                console.error(error);
            }
        });
    }

    function deleteEvent(eventId) {
        // Send the event id to the server for deletion using AJAX
        $.ajax({
            type: 'POST',
            url: '/Calendar/DeleteEvent', // Adjust the URL based on your server endpoint
            data: { eventId: eventId },
            success: function (response) {
                // Handle the response if needed
                console.log(response);
                // Reload events after deletion
                calendar.refetchEvents();
            },
            error: function (error) {
                // Handle the error if needed
                console.error(error);
            }
        });
    }

    calendar.render();
});
