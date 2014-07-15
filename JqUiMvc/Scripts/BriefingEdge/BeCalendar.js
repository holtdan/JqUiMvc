
function Calendar(container, JSONargs) {
    this.div = container;

    /*
        set the default values for the calendar
        options
    */
    this.sevendays = false;
    this.pastselectable = true;
    this.showclosebutton = false;
    this.autoselecttoday = true;
    this.showhalfdays = false;
    this.usedraggableselection = false;
    this.calid = "BeCalendar";
    this.startdate = new Date.today();

    /*
        Pull options from the JSON argument
    */
    if (arguments.length > 1) {

        if (JSONargs.hasOwnProperty('calendarid')) {
            this.calid = JSONargs.calendarid;
        }
        if (JSONargs.hasOwnProperty('sevendays')) {
            this.sevendays = JSONargs.sevendays;
        }
        if (JSONargs.hasOwnProperty('pastselectable')) {
            this.pastselectable = JSONargs.pastselectable;
        }
        if (JSONargs.hasOwnProperty('showclosebutton')) {
            this.showclosebutton = JSONargs.showclosebutton;
        }
        if (JSONargs.hasOwnProperty('autoselecttoday')) {
            this.autoselecttoday = JSONargs.autoselecttoday;
        }
        if (JSONargs.hasOwnProperty('showhalfdays')) {
            this.showhalfdays = JSONargs.showhalfdays;
        }
        if (JSONargs.hasOwnProperty('usedraggableselection')) {
            this.usedraggableselection = JSONargs.usedraggableselection;
        }
        if (JSONargs.hasOwnProperty('startdate')) {
            this.startdate = Date.parse(JSONargs.startdate);
        }
    }


    this.afterLoad = function () { };

    //Create the BeCalendar inside the specified div
    this.div.append('<div class="BECalendar clearfix" id="' + this.calid + '"></div>');


    this.cdate = this.startdate.clone();           //used as the "current date" when setting up the calendar
    //alert('this.cdate = ' + this.cdate);
    this.firstdate = this.startdate.clone();       //first date of the selected range
    this.enddate = this.startdate.clone();        //last date of the selected range    
    this.firstOfMonth = this.startdate.clone();    //first day of the currently displayed month
    this.lastOfMonth = this.startdate.clone();     //last day of the currently displayed month

    this.mouseisdown = false;                //boolean for keeping track of dragging to select multiple dates

    this.cal = $("#" + this.calid);            //the actual jquery object for the current calendar
    var me = this;                           //this is a reference to the calendar object for use
    //inside of jquery action listeners where "this" is different 


    //action listener to highlight the clicked date
    this.cal.on("click", ".selectabledate", function () {
        me.cal.find(".selecteddate").removeClass("selecteddate");
        $(this).addClass("selecteddate");
    });

    //action listener to highlight the clicked am/pm portion of a date
    this.cal.on("click", ".selectableampm", function () {
        me.cal.find(".selectedampm").removeClass("selectedampm");
        $(this).addClass("selectedampm");
    });


    //set up the draggable action listeners if this calendar uses them
    if (this.usedraggableselection) {


        var me = this;

        this.cal.on("mousedown", ".selectabledate", function () {
            me.cal.find(".selecteddate").removeClass("selecteddate");
            $(this).addClass("selecteddate");
            me.firstdate = Date.parse($(this).attr("data-date")); //set the first date of the range
            me.mouseisdown = true;
        });
        this.cal.on("mouseup", ".selectabledate", function () {
            // $(this).addClass("selecteddate");
            me.enddate = Date.parse($(this).attr("data-date"));  //set the last date of the range
            me.mouseisdown = false;

            //find each selectable date between the first and last date, and make sure they
            //are highlighted
            me.cal.find(".selectabledate").each(function () {
                currdate = Date.parse($(this).attr("data-date"));
                if (currdate.compareTo(me.firstdate) >= 0 && currdate.compareTo(me.enddate) <= 0) {
                    $(this).addClass("selecteddate");
                }
            });

        });

        //this allows you to see the range of dates that you are currently highlighting
        //before releasing the mouse button
        this.cal.on("mouseover", ".selectabledate", function () {
            me.enddate = Date.parse($(this).attr("data-date"));
            if (me.mouseisdown) {
                me.cal.find(".selecteddate").removeClass("selecteddate");
                me.cal.find(".selectabledate").each(function () {
                    currdate = Date.parse($(this).attr("data-date"));
                    //find each selectable date between the first and current hovered-over date, 
                    //and make sure they are highlighted
                    if (currdate.compareTo(me.firstdate) >= 0 && currdate.compareTo(me.enddate) <= 0) {
                        $(this).addClass("selecteddate");
                    }
                });

            }

        });





    }
}


/*
    retrieve the first day of the currently displayed month 
*/
Calendar.prototype.firstDayOfMonth = function () {
    return this.firstOfMonth.getFullYear() + "-" + (this.firstOfMonth.getMonth() + 1) + "-" + this.firstOfMonth.getDate();


}
/*
    retrieve the last day of the currently displayed month 
*/
Calendar.prototype.lastDayOfMonth = function () {
    this.lastOfMonth = this.firstOfMonth.clone();
    this.lastOfMonth.next().month();
    this.lastOfMonth.addDays(-1);
    return this.lastOfMonth.getFullYear() + "-" + (this.lastOfMonth.getMonth() + 1) + "-" + this.lastOfMonth.getDate();
}
Calendar.prototype.getCurrMonth = function () {
    return this.firstOfMonth.getMonth() + 1
}
Calendar.prototype.getCurrYear = function () {
    return this.firstOfMonth.getFullYear();
}
/*
    This must be called to render the calendar 
*/
Calendar.prototype.show = function () {
    this.setupCalendar();
}

/*
    set up an actionlistener to execute a user defined function "func"
    to be run when a date or date-part is clicked
*/
Calendar.prototype.dateClick = function (func) {
    this.cal.on("click", ".selectabledate", func);
    this.cal.on("click", ".selectableampm", func);
}

/*
    This function is run after the calendar has been created,
    A user defined function can be supplied to style dates, 
    add extra content to dates, etc.
*/
Calendar.prototype.dateStyle = function (func) {
    this.afterLoad = func;
    // func();
}

/*
    Create the unchanging elements of the calendar, such as
    the month name div, the days of the week, and the left/right arrows
*/
Calendar.prototype.setupCalendarTable = function () {
    this.cal.append('<div class="clearfix CalendarHeader"></div>');
    this.cal.append('<div class="clearfix CalendarBody"></div>');
    this.cal.find(".CalendarHeader").append('<div class="clearfix CalendarMon"></div>');

    $(".CalendarMon").append(
        '<table><tr>' +
        '<td width="20%"><div class="CalendarBtn clickable Xdblprevarrow"><span class="glyphicon glyphicon-backward"></span></div></td>' +
        '<td width="10%" align="right"><div class="CalendarBtn clickable Xprevarrow"><span class="glyphicon glyphicon-chevron-left"></span></div></td>' +
        '<td width="40%"><div class="monname"></div></td>' +
        '<td width="10%"><div class="CalendarBtn clickable Xprevarrow"><span class="glyphicon glyphicon-chevron-right"></span></div></td>' +
        '<td width="20%"><div class="CalendarBtn clickable Xdblnxtarrow pull-right"><span class="glyphicon glyphicon-forward"></span></div></td>' +
        '</tr></table>');
    //$(".CalendarMon").append(
    //    '<div class="container container-fluid">' +
    //    '<div class="row">' +
    //    '<div class="col-md-2 col-lg-2 CalendarBtn clickable Xdblprevarrow"><span class="glyphicon glyphicon-backward"></span></div>' +
    //    '<div class="col-md-2 col-lg-2 CalendarBtn clickable Xprevarrow"><span class="glyphicon glyphicon-chevron-left"></span></div>' +
    //    '<div class="col-md-4 col-lg-4 CalendarBtn monname"></div>' +
    //    '<div class="col-md-2 col-lg-2 CalendarBtn clickable Xprevarrow"><span class="glyphicon glyphicon-chevron-right"></span></div>' +
    //    '<div class="col-md-2 col-lg-2 CalendarBtn clickable Xdblnxtarrow"><span class="glyphicon glyphicon-forward"></span></div>' +
    //    '</div></div>');
    this.cal.find(".CalendarHeader").append('<div class="DaysOfWeek"></div>');

    //$(".DaysOfWeek").append(
    //    '<table id="dowsTable"><tr>' +
    //    '<th><div class="CalendarDoW"><span>Sun</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Mon</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Tue</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Wed</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Thu</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Fri</span></div></th>' +
    //    '<th><div class="CalendarDoW"><span>Sat</span></div></th>' +
    //    '</tr></table>');
}
Calendar.prototype.setupCalendar = function () {
    this.setupCalendarTable();
    //this.cal.append('<div class="clearfix CalendarHeader"></div>');
    //this.cal.append('<div class="clearfix CalendarBody"></div>');
    //this.cal.find(".CalendarHeader").append('<div class="clearfix CalendarMon"></div>');
    //this.cal.find(".CalendarMon").append('<div class="clickable dblprevarrow"></div><div class="clickable prevarrow"></div><div class="monname"></div><div class="clickable nextarrow"></div><div class="clickable dblnextarrow"></div>');
    //this.cal.find(".CalendarHeader").append('<div class="DaysOfWeek"></div>');
    //this.cal.find(".DaysOfWeek").append('<div class="CalendarDoW"><span>Mon</span></div>');
    //this.cal.find(".DaysOfWeek").append('<div class="CalendarDoW"><span>Tue</span></div>');
    //this.cal.find(".DaysOfWeek").append('<div class="CalendarDoW"><span>Wed</span></div>');
    //this.cal.find(".DaysOfWeek").append('<div class="CalendarDoW"><span>Thu</span></div>');
    //this.cal.find(".DaysOfWeek").append('<div class="CalendarDoW"><span>Fri</span></div>');
    //if(this.sevendays)
    //{
    //    this.cal.find(".DaysOfWeek").prepend("<div class='CalendarDoW'><span>Sun</span></div>");
    //    this.cal.find(".DaysOfWeek").append("<div class='CalendarDoW'><span>Sat</span></div>");
    //    this.cal.find(".CalendarDoW").css("width",Math.round(this.cal.find(".CalendarBody").width()/7));

    //}
    if (this.showclosebutton) {
        var me = this;
        this.cal.append('<div class="CalendarClose"><span>Close</span></div>');
        this.cal.find(".CalendarClose").click(function () {
            //this assumes the Calendar is within a parent div that was popped up.
            me.cal.parent().popup();
        })
    }
    //fill the dates of the calendar
    this.fillCalendar();

    //set this to "me" because there is a scoping problem with using
    //actionlisteners inside a class
    var me = this;

    this.cal.find(".nextarrow").click(function () {
        me.cdate.next().month();
        me.fillCalendar();
    });

    this.cal.find(".prevarrow").click(function () {

        me.cdate.addMonths(-1);
        me.fillCalendar();
    });
    this.cal.find(".dblnextarrow").click(function () {
        me.cdate.next().year();
        me.fillCalendar();
    });
    this.cal.find(".dblprevarrow").click(function () {
        me.cdate.addYears(-1);
        me.fillCalendar();
    });
}
/*  fillCalendar places all dates in correct places, and
    it also executes a specified function after loading the dates.
    This function will most likely be used to style specific dates using the
    "modifydate" function     
*/
Calendar.prototype.fillCalendar = function () {
    this.cdate.moveToFirstDayOfMonth();
    initialdate = this.cdate.clone();

    this.firstOfMonth = this.cdate.clone();

    var dayPct = this.cal.find(".CalendarMon").width() / 5;
    if (this.sevendays)
        dayPct = this.cal.find(".CalendarMon").width() / 7;
    var widParm = ' width="' + dayPct + '%" ';

    firstDay = this.cdate.getDay();
    curmonth = this.cdate.getMonth();
    monthstarted = false;
    this.cal.find(".monname").html(this.cdate.toString('MMMM') + " " + this.cdate.getFullYear());
    this.cal.find(".DaysOfWeek").html("");

    var myHtml = "";
    myHtml += '<table id="dowsTable"><tr>';

    if (this.sevendays)
        myHtml += '<th><div class="CalendarDoW"'+widParm+'><span>Sun</span></div></th>';

    myHtml +=
        '<th><div class="CalendarDoW"' + widParm + '><span>Mon</span></div></th>' +
        '<th><div class="CalendarDoW"' + widParm + '><span>Tue</span></div></th>' +
        '<th><div class="CalendarDoW"' + widParm + '><span>Wed</span></div></th>' +
        '<th><div class="CalendarDoW"' + widParm + '><span>Thu</span></div></th>' +
        '<th><div class="CalendarDoW"' + widParm + '><span>Fri</span></div></th>';

    if (this.sevendays)
        myHtml += '<th><div class="CalendarDoW"' + widParm + '><span>Sat</span></div></th>' +
      '</tr>';
    //$(".DaysOfWeek").append(
    //   '<table id="dowsTable"><tr>' +
    //   '<th><div class="CalendarDoW"><span>Sun</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Mon</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Tue</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Wed</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Thu</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Fri</span></div></th>' +
    //   '<th><div class="CalendarDoW"><span>Sat</span></div></th>' +
    //   '</tr></table>');
    for (i = 0; i < 6; i++)//for each row in the calendar
    {
        //this.cal.find(".dowsTable").append("<tr class ='calrow calrow" + i + "'></tr>");
        myHtml += "<tr class ='calrow calrow" + i + "'>";
        for (j = 0; j < 7; j++)//for each day of the week
        {
            if (j == firstDay)//mark where the first day of the month starts
            {
                monthstarted = true;
            }

            if ((j != 0 && j != 6) || this.sevendays)//if not Sunday or Saturday
            {

                if (monthstarted && (curmonth == this.cdate.getMonth()))//only show dates for the current month
                {

                    if (this.showhalfdays) {
                        //display the date
                        //this.cal.find(".calrow"+i).append("<td><div class='"+i+"_"+j+" calcell' data-DOW='"+j+"'><div class='AMPMdate'>"+this.cdate.getDate()+"</div><div class='calAM clickable selectableampm' data-half='AM'>AM</div><div class='calPM clickable selectableampm' data-half='PM'>PM</div></div></td>");
                        myHtml += "<td><div class='becDay " + i + "_" + j + " calcell' data-DOW='" + j + "'><div class='AMPMdate'>" + this.cdate.getDate() + "</div><div class='calAM clickable selectableampm' data-half='AM'>AM</div><div class='calPM clickable selectableampm' data-half='PM'>PM</div></div></td>";

                        //add an attribute to hold the value of the date to the cell
                        //this.cal.find("."+i+"_"+j).attr("data-date",this.cdate.getFullYear()+"-"+(this.cdate.getMonth()+1)+"-"+this.cdate.getDate());
                        this.cdate = this.cdate.next().day();

                    }
                    else {
                        //display the date
                        //this.cal.find(".calrow"+i).append("<td><div class='"+i+"_"+j+" calcell clickable selectabledate' data-DOW='"+j+"'>"+this.cdate.getDate()+"</div></td>");
                        var dataDate = this.cdate.getFullYear() + "-" + (this.cdate.getMonth() + 1) + "-" + this.cdate.getDate();
                        myHtml += "<td><div class='becDay " + i + "_" + j + " calcell clickable selectabledate' data-DOW='" + j + "' data-date='" + dataDate + "'>" + this.cdate.getDate() + "</div></td>";

                        //add an attribute to hold the value of the date to the cell
                        //this.cal.find("."+i+"_"+j).attr("data-date",this.cdate.getFullYear()+"-"+(this.cdate.getMonth()+1)+"-"+this.cdate.getDate());
                        this.cdate = this.cdate.next().day();
                    }


                }
                else {
                    //display an empty cell
                    myHtml += "<td><div class='becDay calcell'>&nbsp;</div></td>";
                }
            }
            else //weekend
            {
                if (monthstarted)//just increment the date, don't display anything
                {
                    this.cdate = this.cdate.next().day();
                }
            }
        }
        myHtml += "</tr>";
    }
    this.cal.find(".DaysOfWeek").html(myHtml);
    var innerH = this.cal.find(".CalendarHeader").html();
    //center day content
    //do some funky math to fit the cells in and account for the borders
    //parentheight = this.cal.find(".CalendarBody").parent().height();
    //dateheight = parentheight - this.cal.find(".CalendarHeader").height();
    //this.cal.find(".calrow").css("height",Math.round((dateheight/6))+"px");
    //this.cal.find(".calcell").css("line-height",Math.round(this.cal.find(".calrow").height())+"px");

    //if(this.sevendays)
    //{
    //     this.cal.find(".calcell").css("width",Math.round(this.cal.find(".CalendarBody").width()/7));

    //}
    //select the current day
    var me = this;

    if (this.autoselecttoday == true) {
        this.cal.find(".selectabledate").each(function () {
            currdate = Date.parse($(this).attr("data-date"));
            if (currdate.compareTo(me.firstdate) >= 0 && currdate.compareTo(me.enddate) <= 0) {
                $(this).addClass("selecteddate");
            }
        });
    }



    //now that the dates have all been created, run the user supplied
    //styling function
    this.afterLoad();

    //if past is unavailable, disable it
    if (this.pastselectable == false) {
        Dtoday = new Date.today();
        this.cal.find(".selectabledate").each(function () {
            currdate = Date.parse($(this).attr("data-date"));
            if (currdate.compareTo(Dtoday) <= 0) {
                $(this).removeClass("selectabledate");

            }
        })
        //disable half-day calendar days
        if (this.cal.find(".selectableampm").length > 0) {
            this.cal.find(".calcell[data-date]").each(function () {
                currdate = Date.parse($(this).attr("data-date"));
                if (currdate.compareTo(Dtoday) <= 0) {

                    $(this).children(".selectableampm").removeClass("selectableampm");
                }

            })

        }

    }


    this.cdate = initialdate.clone();//set date back to beginning of month

}
/*
    modifyDateHalf, modifyDate, and modifyDate range all work very similarly

    provide a date string "2014-5-1" and a function to apply to that date.
    This will mostly be used for styling, or disabling a date for selection

    The function is executed in reference to the jquery object that describes 
    the calcell... this means that in the supplied function, you refer to 
    $(this) as the calcell you are going to modify.


*/
Calendar.prototype.modifyDateHalf = function (datestr, half, functoexec) {

    //half is either "AM" or "PM"
    this.cal.find(".calcell[data-date]").each(function () {


        celldate = Date.parse($(this).attr("data-date"));

        compdate = Date.parse(datestr);
        compdate.set({ hour: 0, minute: 0 });
        if (celldate.equals(compdate)) {

            $(this).children('.cal' + half).each(functoexec);

        }
    })
}

Calendar.prototype.modifyDate = function (datestr, functoexec) {

    if (this.cal.find(".selectableampm").length > 0) {
        this.cal.find(".calcell").each(function () {

            if ($(this).children(".selectableampm").length > 0) {
                celldate = Date.parse($(this).attr("data-date"));

                compdate = Date.parse(datestr);
                compdate.set({ hour: 0, minute: 0 });

                if (celldate.equals(compdate)) {
                    $(this).each(functoexec);
                }

            }
        })

    }
    else {
        this.cal.find(".selectabledate").each(function () {

            celldate = Date.parse($(this).attr("data-date"));

            compdate = Date.parse(datestr);
            compdate.set({ hour: 0, minute: 0 });


            if (celldate.equals(compdate)) {

                $(this).each(functoexec);
            }

        })
    }
}
Calendar.prototype.modifyDateRange = function (startdatestr, enddatestr, functoexec) {

    this.cal.find(".selectabledate").each(function () {

        celldate = Date.parse($(this).attr("data-date"));

        startcompdate = Date.parse(startdatestr);
        endcompdate = Date.parse(enddatestr);

        startcompdate.set({ hour: 0, minute: 0 });
        endcompdate.set({ hour: 0, minute: 0 });


        changeflag = false;
        if (celldate.compareTo(startcompdate) >= 0 && celldate.compareTo(endcompdate) <= 0) {
            changeflag = true;
        }

        if (changeflag) {
            $(this).each(functoexec);
        }

    })
    if (this.cal.find(".selectableampm").length > 0) {
        this.cal.find(".calcell[data-date]").each(function () {

            celldate = Date.parse($(this).attr("data-date"));

            startcompdate = Date.parse(startdatestr);
            endcompdate = Date.parse(enddatestr);
            changeflag = false;
            if (celldate.compareTo(startcompdate) >= 0 && celldate.compareTo(endcompdate) <= 0) {
                changeflag = true;
            }

            if (changeflag) {
                $(this).each(functoexec);
            }

        })
    }

}
Calendar.prototype.disableDate = function (datestr) {
    func = function () {
        $(this).removeClass('selectabledate');
    }
    this.modifyDate(datestr, func);

}
//
// Takes in a date format eg. dd-mm-yyyy and the iso8601 formatted date, and formats
// it according to the passed in format
//
function formatDate(dateformat, iso8601date) {
    dateparts = iso8601date.split("-");
    year = dateparts[0];
    month = dateparts[1];
    day = dateparts[2];


    retstring = dateformat.toUpperCase();

    retstring = retstring.replace("YYYY", year);
    retstring = retstring.replace("MM", month);
    retstring = retstring.replace("DD", day);

    return retstring;



}
