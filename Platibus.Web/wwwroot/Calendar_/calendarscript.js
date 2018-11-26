window.onload = function current_calendar() {

    var da = new Date();
    var month_names = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    var month = da.getMonth();
    var year = da.getFullYear();
    var first_date = month_names[month] + " " + 1 + " " + year;
    var tmp = new Date(first_date).toDateString();
    var first_day = tmp.substring(0, 3); //writes out mon, tue, ... 
    var day_names = ['Sun', 'Mon','Tue','Wed','Thu','Fri','Sat'];
    var day_nr = day_names.indexOf(first_day);
    var days = new Date(year, month+1, 0).getDate();
    var calendar = get_calendar(day_nr, days);
    document.getElementById('calendar-month2').innerHTML = month_names[month];
    document.getElementById('calendar-year2').innerHTML = year;
    document.getElementById('calendar-dates').appendChild(calendar);
    
}



//window.onload = current_calendar();
function prev_calendar() {
    
    var month = document.getElementById('calendar-month2').innerText;
    var year = document.getElementById('calendar-year2').innerText;
    var month_names = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    document.getElementById('calendar-dates').innerHTML= "";
    document.getElementById('calendar-month2').innerHTML= "";
    document.getElementById('calendar-year2').innerHTML= "";
    month = month_names.indexOf(month);
    month = month-1;
    if (month < 0) {
        month = 11
        year = parseInt(year);
        year = year-1
    }
    var first_date = month_names[month] + " " + 1 + " "+ year;
    var tmp = new Date(first_date).toDateString();
    var first_day = tmp.substring(0, 3); //writes out mon, tue, ... 
    var day_names = ['Sun', 'Mon','Tue','Wed','Thu','Fri','Sat'];
    var day_nr = day_names.indexOf(first_day);
    var days = new Date(year, month+1, 0).getDate();
    var calendar = get_calendar(day_nr, days);
    document.getElementById('calendar-month2').innerHTML = month_names[month];
    document.getElementById('calendar-year2').innerHTML = year;
    document.getElementById('calendar-dates').appendChild(calendar);
    
    
}

function next_calendar() {
    var month = document.getElementById('calendar-month2').innerText;
    var year = document.getElementById('calendar-year2').innerText;
    var month_names = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    document.getElementById('calendar-dates').innerHTML= "";
    document.getElementById('calendar-month2').innerHTML= "";
    document.getElementById('calendar-year2').innerHTML= "";
    month = month_names.indexOf(month);
    month = month+1;
    if (month > 11) {
        month = 0
        year = parseInt(year);
        year = year+1
    }
    var first_date = month_names[month] + " " + 1 + " "+ year;
    var tmp = new Date(first_date).toDateString();
    var first_day = tmp.substring(0, 3); //writes out mon, tue, ... 
    var day_names = ['Sun','Mon','Tue','Wed','Thu','Fri','Sat'];
    var day_nr = day_names.indexOf(first_day);
    var days = new Date(year, month+1, 0).getDate();
    var calendar = get_calendar(day_nr, days);
    document.getElementById('calendar-month2').innerHTML = month_names[month];
    document.getElementById('calendar-year2').innerHTML = year;
    document.getElementById('calendar-dates').appendChild(calendar);
    
}
function get_calendar(day_nr, days){

    var table = document.createElement('table');
    var tr = document.createElement('tr');

    //row for the letters of the days of a week
    for (var c=0; c <= 6; c++) {
        var td = document.createElement('td');
        td.innerHTML = "SMTWTFS"[c];
        tr.appendChild(td)
    }
    
    table.appendChild(tr);

    //create 2 row
    tr = document.createElement('tr');
    var c;
    for (c=0; c<=6; c++) {
        if(c == day_nr) {
            break;
        }
        var td = document.createElement('td');
        td.innerHTML = "";
        tr.appendChild(td);
    }

    var count = 1;
    for(; c <=6; c++){
        var td = document.createElement('td');
        td.innerHTML = count;
        count++;
        tr.appendChild(td)
    }
    //create rest of the rows
    table.appendChild(tr);
    for(var r=3; r<= 6; r++){
        tr = document.createElement('tr')
        for (var c=0; c<= 6; c++) {
            if(count > days) {
                table.appendChild(tr);
                return table;
            }
            var td = document.createElement('td');
            td.innerHTML = count;
            count++;
            tr.appendChild(td);
        }
        table.appendChild(tr);
    }
}



