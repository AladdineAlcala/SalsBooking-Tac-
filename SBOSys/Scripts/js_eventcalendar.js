$(document).ready(function () {


    //Date for the calendar events (dummy data)
    var to_day = new Date();


   

    loadCalendarEvents(calendarEventsUrl.calUrl_AllEvents);


   function loadCalendarEvents(url) {
       
       $('#event_calendar').fullCalendar('destroy');

       $('#event_calendar').fullCalendar({
           contentHeight: 400,
           aspectRatio: 1.8,
           defaultDate: to_day,
           timeFormat: 'h(:mm)a',
           header: {
               left: 'prev,next today',
               center: 'title',
               right: 'month,agendaWeek,agendaDay',
               height: 650
           },
           buttonText: {
               today: 'today',
               month: 'month',
               week: 'week',
               day: 'day'
           },
           defaultView: 'month',
           editable: 'false',
           timezone: 'local',
           eventRender: function (eventObj, $el) {

               $el.popover({
                   title: eventObj.title,
                   content: eventObj.description,
                   trigger: 'hover',
                   placement: 'top',
                   container: 'body'
               });
           },

           events: function (start, end, timezone, callback) {

               $.ajax({
                   url:url,
                   type: 'GET',
                   dataType: 'JSON',

                   success: function (result) {

                       var events = [];

                       var eventdate = "";


                       for (var i = 0; i < result.length; i++) {

                           var txtcolor = '';
                           var color = '';
                           // eventdate = moment.utc(result[i].StartDateTime);

                           var d = moment.utc(result[i].StartDateTime).format('MM/DD/YYYY');
                           eventdate = new Date(d);


                           var event_Type = result[i].eventType;

                           console.log(event_Type);


                           if (to_day.getTime() < eventdate.getTime()) {

                               //console.log(to_day.getTime());

                               //console.log(eventdate.getTime());

                               //console.log('date schedule before');
                               txtcolor = '#FFFFFF';

                           }
                           else if (eventdate.getTime() === to_day.getTime()) {
                               //  console.log('equals');
                               color = '#0000FF';
                               txtcolor = '#FFFFFF';
                           }


                           else {
                               //   console.log('date schedule finish');
                               color = '#FF0000';
                               txtcolor = '#F7F9F9';
                           }

                           if (event_Type == "reservation") {

                               console.log('reservation');

                               color = '#1D8348';
                               txtcolor = '#F7F9F9';
                           }



                           events.push({
                               id: result[i].EventId,
                               title: result[i].EventName,
                               description: result[i].EventDescription,
                               start: moment.utc(result[i].StartDateTime),
                               backgroundColor: color,
                               textColor: txtcolor


                           });

                       }

                       //  console.log(events);
                       callback(events);

                   },
                   error: function (jqXhr, textStatus, errorThrown) {
                       alert("Error when fetching data:", +textStatus + "- " + errorThrown);
                   }


               });
               //end call ajax events
           }
           ,
           dayClick: function (date, jsevent, view) {

               //alert('Clicked on: ' + date.format());

               var datefiltered = moment.utc(date).format('MM/DD/YYYY');

               $.ajax({
                   type: 'Get',
                   url: calendarEventsUrl.calUrl_dayEventUrl,
                   contentType: 'application/html;charset=utf8',
                   data: { eventdate: datefiltered },
                   datatype: 'html',
                   cache: false,
                   success: function (result) {

                       // console.log(result);

                       $('#eventsList').html(result);
                   },
                   error: function (xhr, ajaxOptions, thrownError) {
                       swal("Error on retrieving record!", "Please try again", "error");
                   }


               });


           }//----- end dayclick event
       });

   }

    $("input:radio").on('change',function() {

        var selectedVal = $(this).val();
        var url = "";

        if (selectedVal == "book") {

            console.log('book');

            url = calendarEventsUrl.calUrl_Book_Events;


            loadCalendarEvents(url);

        }
        else if (selectedVal == "reserve") {
            console.log('reserve');
            url = calendarEventsUrl.calUrl_Reservation_Events;
            loadCalendarEvents(url);
           
           
        }
        else {
            console.log('all');

            url = calendarEventsUrl.calUrl_AllEvents;
            loadCalendarEvents(url);

           
        }
      

        //  alert(selectedVal);



    });

});
