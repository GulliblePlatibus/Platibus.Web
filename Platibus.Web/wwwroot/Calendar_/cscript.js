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


window.onload = daycalendar();

