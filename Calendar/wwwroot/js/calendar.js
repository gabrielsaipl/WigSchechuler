let calendar = document.querySelector('.calendar');
let calendar_body = document.querySelector('.calendar-body');

function daysInMonth(iMonth, iYear) {
    return 32 - new Date(iYear, iMonth, 32).getDate();
}

function createCalendar(month, year) {
    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    let monthName = months[month];
    document.getElementById("calendar-month").innerHTML = monthName;
    document.getElementById("previous").innerHTML = "<";
    document.getElementById("next").innerHTML = ">";

    const calendar_days = document.createElement('div');
    calendar_days.className = 'calendar-days';
    //calendar_days.innerHTML = '';
    let date = 1;
    for (j = 0; j < 6; j++) {
        const week = document.createElement('div');
        week.className = 'week';
    
        for (let i = 0; i < 7; i++) {
            const day = document.createElement('div');
            day.className = 'day';

            if (j === 0 && i < new Date(year, month, 1).getDay()) {
                day.innerHTML = '';
            } else if (date > daysInMonth(month,year)) {
                break;
            } else {
                day.innerText = date.toString();
                date++;
            }
            week.appendChild(day);
        }
        calendar_days.appendChild(week);
    }
    calendar_body.appendChild(calendar_days);
}

document.addEventListener('DOMContentLoaded', function () {
    const now = new Date();
    const year = now.getFullYear();
    const month = now.getMonth();
    createCalendar(month, year);
});