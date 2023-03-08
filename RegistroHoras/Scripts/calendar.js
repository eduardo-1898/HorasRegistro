const calendar = document.querySelector(".calendar"),
    date = document.querySelector(".date"),
    daysContainer = document.querySelector(".days"),
    prev = document.querySelector(".prev"),
    next = document.querySelector(".next"),
    todayBtn = document.querySelector(".today-btn"),
    gotoBtn = document.querySelector(".goto-btn"),
    dateInput = document.querySelector(".date-input"),
    eventDay = document.querySelector(".event-day"),
    eventDate = document.querySelector(".event-date"),
    eventsContainer = document.querySelector(".events")

let today = new Date();
let activeDay;
let month = today.getMonth();
let year = today.getFullYear();

const months = [
    "Enero",
    "Febrero",
    "Marzo",
    "Abril",
    "Mayo",
    "Junio",
    "Julio",
    "Agosto",
    "Septiembre",
    "Octubre",
    "Noviembre",
    "Diciembre",
];



function getEvents() {
    $.ajax({
        url: "CalendarService.asmx/getData",
        type: 'POST',
        data: "{ mes: '" + (month + 1) + "', ano: " + year + "}",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var getData = JSON.parse(data.d);
            const firstDay = new Date(year, month, 1);
            const lastDay = new Date(year, month + 1, 0);
            const prevLastDay = new Date(year, month, 0);
            const prevDays = prevLastDay.getDate();
            const lastDate = lastDay.getDate();
            const day = firstDay.getDay();
            const nextDays = 7 - lastDay.getDay() - 1;

            date.innerHTML = months[month] + " " + year;

            let days = "";
            let datetest = new Date();

            for (let x = day; x > 0; x--) {
                days += `<div class="day prev-date">${prevDays - x + 1}</div>`;
            }

            for (let i = 1; i <= lastDate; i++) {

                let event = false;

                if (getData == null) {
                    event = true;
                }
                else if ((getData.length === 0) && i < datetest.getDate()) {
                    event = true;
                }
                else if (i > datetest.getDate() && month == datetest.getMonth() && year == datetest.getFullYear()) {
                    event = false;
                }
                else if (getData.length === 0 && month != datetest.getMonth() && year != datetest.getFullYear()) {
                    event = true;
                }
                else if (new Date(year, month, i).getDay() == 0 || new Date(year, month, i).getDay() == 6) {
                    event = false;
                }
                else if (getData.length > 0) {
                    var response = true;
                    getData.forEach((eventObj) => {
                        let newDateParse = new Date(eventObj.fecha);
                        if (newDateParse.getDate() == i &&
                            newDateParse.getMonth() == month &&
                            newDateParse.getFullYear() == year &&
                            eventObj.horas >= 8
                        ) {
                            event = false;
                            response = false;
                        }
                        else if (!response) {
                            event = event;
                        }
                        else {
                            event = true;
                        }
                    });
                }

                if (
                    i === new Date().getDate() &&
                    year === new Date().getFullYear() &&
                    month === new Date().getMonth()
                ) {
                    activeDay = i
                    if (event) {
                        days += `<div class="day today active event">${i}</div>`;
                    } else {
                        days += `<div class="day today active">${i}</div>`;
                    }
                } else {
                    if (event) {
                        days += `<div class="day event">${i}</div>`;
                    } else {
                        days += `<div class="day ">${i}</div>`;
                    }
                }
            }

            for (let j = 1; j <= nextDays; j++) {
                days += `<div class="day next-date">${j}</div>`;
            }
            daysContainer.innerHTML = days;
            addListner();

            putEvents(new Date().getDate());
        }
    });

}

function prevMonth() {
    month--;
    if (month < 0) {
        month = 11;
        year--;
    }
    getEvents();
}

function nextMonth() {
    month++;
    if (month > 11) {
        month = 0;
        year++;
    }
    getEvents();
}

prev.addEventListener("click", prevMonth);
next.addEventListener("click", nextMonth);

 getEvents();

function addListner() {
    const days = document.querySelectorAll(".day");
    days.forEach((day) => {
        day.addEventListener("click", (e) => {
            activeDay = Number(e.target.innerHTML);
            putEvents(Number(e.target.innerHTML));

            days.forEach((day) => {
                day.classList.remove("active");
            });

            if (e.target.classList.contains("prev-date")) {
                prevMonth();

                setTimeout(() => {

                    const days = document.querySelectorAll(".day");
                    days.forEach((day) => {
                        if (
                            !day.classList.contains("prev-date") &&
                            day.innerHTML === e.target.innerHTML
                        ) {
                            day.classList.add("active");
                        }
                    });
                }, 100);
            } else if (e.target.classList.contains("next-date")) {
                nextMonth();
                setTimeout(() => {
                    const days = document.querySelectorAll(".day");
                    days.forEach((day) => {
                        if (
                            !day.classList.contains("next-date") &&
                            day.innerHTML === e.target.innerHTML
                        ) {
                            day.classList.add("active");
                        }
                    });
                }, 100);
            } else {
                e.target.classList.add("active");
            }
        });
    });
}


function getActiveDay(date) {
    eventDate.innerHTML = date + " " + months[month] + " " + year;
}

function getActiveDaySegment(date, hours) {
    if (((hours / 8) * 100) > 100) {
        eventDate.innerHTML = date + " " + months[month] + " " + year + " - 100%";
    }
    else {
        eventDate.innerHTML = date + " " + months[month] + " " + year + " - " + ((hours / 8) * 100) + "%";
    }

}

function SendRequest() {
    var date = new Date(year, month, activeDay)
    const dateFormatter = Intl.DateTimeFormat('sv-SE');
    window.location = 'Principal.aspx?Fecha=' + dateFormatter.format(date);
}

function putEvents(dia) {
    let events = "";
    $.ajax({
        url: "CalendarService.asmx/getActivities",
        type: 'POST',
        data: "{ mes: '" + (month + 1) + "', ano: " + year + ", dia: " + dia + "}",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var getData = JSON.parse(data.d);
            var hours = 0;
            if (getData.length > 0) {                
                getData.forEach((item) => {
                    hours = + Number(hours) + Number(item.Horas);
                    events += `<div class="event">
                                    <div class="title">
                                        <i class="fas fa-circle"></i>
                                        <h3 class="event-title">${item.Proyecto}</h3>
                                    </div>
                                    <div class="event-time">
                                        <span class="event-time">${item.FechaInicio} - ${item.FechaFinal} - Cliente: ${item.Cliente}</span><br>
                                    </div>
                                </div>`;    

                    eventsContainer.innerHTML = events;
                });
                getActiveDaySegment(dia, hours);
            }
            else {
                events = `<div class="no-event">
                        <h3>Sin eventos</h3>
                     </div>`;
                eventsContainer.innerHTML = events;
                getActiveDaySegment(dia, hours);
            }
        }
    });
}
