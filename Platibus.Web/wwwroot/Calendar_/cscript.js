//this function finds the current date 
function daycalendar() {
    
    var day=['Sunday', 'Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
    var month=['January','February','March','April','May','June','July','August','September','October','November','December' ];
    var da = new Date();
    settext('calendar-day', day[da.getDay()]);
    settext('calendar-date', da.getDate());
    settext('calendar-month-year', month[da.getMonth()] + ' ' + (da.getFullYear()));
    
}

//this function will set the text value of <p> tags
function settext(id, val) {
    
    if (val < 10) {
        val = "0" + val //add leading 0 if val < 10
    }
    document.getElementById(id).innerHTML = val;
}

//this function builds the full calendar
window.onload = function fullcalendar() {
    
    var da = new Date();
    var month_names=['January','February','March','April','May','June','July','August','September','October','November','December' ];
    var month = da.getMonth();
    var year = da.getFullYear();
    var first_date = month_names[month] + " " + 1 + " " + year; 
    var tmp = new Date(first_date).toDateString();
    var first_day = tmp.substring(0, 3); //writes out mon, tue, ... 
    var day_names = ['Sun', 'Mon','Tue','Wed','Thu','Fri','Sat'];
    var day_nr = day_names.indexOf(first_day);
    var days = new Date(year, month+1, 0).getDate();
    var calendar = getcalendar(day_nr, days);
    document.getElementById("calendar-month-year2").innerHTML = month_names[month] + " " + year;
    document.getElementById("calendar-dates").appendChild(calendar);
}



function getcalendar(day_nr, days){
    
    var table = document.createElement('table');
    var tr = document.createElement('tr');
    
    //row for the letters of the days of a week
    for (var c=0; c < 6; c++) {
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
        var td = td.createElement('td');
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

window.onload = daycalendar;

