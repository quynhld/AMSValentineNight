var target;
var contPop_Top;
var contPop_Left;
var contCal_Day;

var oPopup = window.createPopup(); // IE 용 팝업
var Pop_div; 												// Mozilla 용 DIV
var contTxtNm; 										        // Input box name
var contHfNm; 											    // Input box name2	
var dash; 													// 날짜에 '-' 붙이기 여부

function ContCalendar_Click(e) 
{
    contCal_Day = e.title;
    if (contCal_Day.length > 6) 
    {
        if (ContCheck_navigator()) 
        { //true 면 IE
            document.getElementById(contTxtNm).value = contCal_Day;
            if (dash) 
            {
                document.getElementById(contTxtNm).value = contCal_Day.substr(0, 4) + '-' + contCal_Day.substr(4, 2) + '-' + contCal_Day.substr(6);
                if (contHfNm != null && contHfNm != '') 
                {
                    document.getElementById(contHfNm).value = contCal_Day.substr(0, 4) + '-' + contCal_Day.substr(4, 2) + '-' + contCal_Day.substr(6);
                }
            }
            else 
            {
                document.getElementById(contTxtNm).value = contCal_Day.substr(0, 4) + contCal_Day.substr(4, 2) + contCal_Day.substr(6);
                if (contHfNm != null && contHfNm != '') 
                {
                    document.getElementById(contHfNm).value = contCal_Day.substr(0, 4) + contCal_Day.substr(4, 2) + contCal_Day.substr(6);
                }
            }
            oPopup.hide();

        } 
        else 
        { //아니면 모질라
            if (dash) 
            {
                contTxtNm.value = contCal_Day.substr(0, 4) + '-' + contCal_Day.substr(4, 2) + '-' + contCal_Day.substr(6);
                if (contHfNm != null && contHfNm != '') 
                {
                    contHfNm.value = contCal_Day.substr(0, 4) + '-' + contCal_Day.substr(4, 2) + '-' + contCal_Day.substr(6);
                }
            } else {
                contTxtNm.value = contCal_Day.substr(0, 4) + contCal_Day.substr(4, 2) + contCal_Day.substr(6);
                if (contHfNm != null && contHfNm != '') {
                    contHfNm.value = contCal_Day.substr(0, 4) + contCal_Day.substr(4, 2) + contCal_Day.substr(6);
                }
            }
            Pop_div = document.getElementById('contcalendar_div');
            Pop_div.style.display = 'none';
        }
    }
}

function ContCheck_navigator() {
    var appName = navigator.appName; 										//**	브라우저명
    var appVersion = parseFloat(navigator.appVersion.split("MSIE")[1]); 		//**	브라우저 버전
    var bitUseEditor																//**	에디터 사용 유무

    if (appName != "Microsoft Internet Explorer" || appVersion < 5.5) {
        return false; //**	익스플로어가 아니고 버전이 5.5보다 작을때는 "사용 안함"
    } else {
        return true; 												//**	에디터 사용함
    }
}

function ContCalendar(objCont, name, name2, dash_yn) {
    dash = dash_yn;
    if (ContCheck_navigator()) { //true 면 IE
        ContCalendar_IE(objCont, name, name2);
    } else { //아니면 모질라
        ContCalendar_Mozilla(objCont, name, name2);
    }
}

function ContCalendar_IE(objCont, name, name2) {														// jucke
    var now = objCont.value.split("-");

    contTxtNm = name;
    contHfNm = name2;
    target = objCont;

    contPop_Top = event.clientY - 5;
    contPop_Left = event.clientX - 150;

    if (now.length == 3) {
        ContShow_cal_IE(now[0], now[1], now[2]);
    } else {
        now = new Date();
        ContShow_cal_IE(now.getFullYear(), now.getMonth() + 1, now.getDate());
    }
}

function ContCalendar_Mozilla(objCont, name, name2) {														// jucke
    //var now = objCont.value.split("-");
    try {
        contTxtNm = document.getElementById(name);
        contHfNm = document.getElementById(name2);
        
        target = objCont;
        
//        contPop_Top = document.body.clientTop + GetObjectCalTop(objCont) - document.body.scrollTop;
        //        contPop_Left = document.body.clientLeft + GetObjectCalLeft(objCont) - document.body.scrollLeft;
        contPop_Top = event.clientY - 5;
        contPop_Left = event.clientX - 150;

        /*
        if (now.length == 3) {
        Show_cal(now[0],now[1],now[2]);
        } else {
        now = new Date();
        Show_cal(now.getFullYear(), now.getMonth()+1, now.getDate());
        }
        */
        var now = new Date();

        ContShow_cal_Mozilla(now.getFullYear(), now.getMonth() + 1, now.getDate());
    } catch (e) {
        alert(e);
    }
}

function ContCalendar_M(objCont, name) {
    var now = objCont.value.split("-");
    target = objCont;
    contTxtNm = name;

    contPop_Top = document.body.clientTop + GetObjectCalTop(objCont) - document.body.scrollTop;
    contPop_Left = document.body.clientLeft + GetObjectCalLeft(objCont) - document.body.scrollLeft;

    if (now.length == 2) {
        ContShow_cal_M(now[0], now[1]);
    } else {
        now = new Date();
        ContShow_cal_M(now.getFullYear(), now.getMonth() + 1);
    }
}

function doOver(el) {
    contCal_Day = el.title;

    if (contCal_Day.length > 7) {
        el.style.borderColor = "#FF0000";
    }
}

function doOut(el) {
    contCal_Day = el.title;

    if (contCal_Day.length > 7) {
        el.style.borderColor = "#FFFFFF";
    }
}

function day2(d) {	// 2자리 숫자료 변경
    var str = new String();

    if (parseInt(d) < 10) {
        str = "0" + parseInt(d);
    } else {
        str = "" + parseInt(d);
    }
    return str;
}

function ContShow_cal_IE(sYear, sMonth, sDay) {
    var Months_day = new Array(0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31)
    var Month_Val = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var intThisYear = new Number(), intThisMonth = new Number(), intThisDay = new Number();

    datToday = new Date(); 												// 현재 날자 설정

    intThisYear = parseInt(sYear, 10);
    intThisMonth = parseInt(sMonth, 10);
    intThisDay = parseInt(sDay, 10);

    if (intThisYear == 0) intThisYear = datToday.getFullYear(); 			// 값이 없을 경우
    if (intThisMonth == 0) intThisMonth = parseInt(datToday.getMonth(), 10) + 1; // 월 값은 실제값 보다 -1 한 값이 돼돌려 진다.
    if (intThisDay == 0) intThisDay = datToday.getDate();

    switch (intThisMonth) {
        case 1:
            intPrevYear = intThisYear - 1;
            intPrevMonth = 12;
            intNextYear = intThisYear;
            intNextMonth = 2;
            break;
        case 12:
            intPrevYear = intThisYear;
            intPrevMonth = 11;
            intNextYear = intThisYear + 1;
            intNextMonth = 1;
            break;
        default:
            intPrevYear = intThisYear;
            intPrevMonth = parseInt(intThisMonth, 10) - 1;
            intNextYear = intThisYear;
            intNextMonth = parseInt(intThisMonth, 10) + 1;
            break;
    }
    intPPyear = intThisYear - 1
    intNNyear = intThisYear + 1

    NowThisYear = datToday.getFullYear();                           // 현재 년
    NowThisMonth = datToday.getMonth() + 1;                         // 현재 월
    NowThisDay = datToday.getDate();                                // 현재 일

    datFirstDay = new Date(intThisYear, intThisMonth - 1, 1); 		// 현재 달의 1일로 날자 객체 생성(월은 0부터 11까지의 정수(1월부터 12월))
    intFirstWeekday = datFirstDay.getDay();                         // 현재 달 1일의 요일을 구함 (0:일요일, 1:월요일)
    //intSecondWeekday = intFirstWeekday;
    intThirdWeekday = intFirstWeekday;

    datThisDay = new Date(intThisYear, intThisMonth, intThisDay);   // 넘어온 값의 날자 생성
    //intThisWeekday = datThisDay.getDay();							// 넘어온 날자의 주 요일

    intPrintDay = 1; 												// 달의 시작 일자
    secondPrintDay = 1;
    thirdPrintDay = 1;

    Stop_Flag = 0

    if ((intThisYear % 4) == 0) {									// 4년마다 1번이면 (사로나누어 떨어지면)
        if ((intThisYear % 100) == 0) {
            if ((intThisYear % 400) == 0) {
                Months_day[2] = 29;
            }
        } else {
            Months_day[2] = 29;
        }
    }
    intLastDay = Months_day[intThisMonth]; 					        // 마지막 일자 구함

    Cal_HTML = "<html><body>";
    Cal_HTML += "<form name='calendar'>";
    Cal_HTML += "<table id=ContCal_Table border=0 bgcolor='#f4f4f4' cellpadding=1 cellspacing=1 width=100% onmouseover='parent.doOver(window.event.srcElement)' onmouseout='parent.doOut(window.event.srcElement)' style='font-size : 12;font-family:굴림;'>";
    Cal_HTML += "<tr height='35' align=center bgcolor='#f4f4f4'>";
    Cal_HTML += "<td colspan=7 align=center>";
    Cal_HTML += "	<select name='selYear' STYLE='font-size:11;' OnChange='parent.fnContChangeYearD(calendar.selYear.value, calendar.selMonth.value, " + intThisDay + ")';>";
    for (var optYear = (intThisYear - 100); optYear < (intThisYear + 60); optYear++) {
        Cal_HTML += "		<option value='" + optYear + "' ";
        if (optYear == intThisYear) Cal_HTML += " selected>\n";
        else Cal_HTML += ">\n";
        Cal_HTML += optYear + "</option>\n";
    }
    Cal_HTML += "	</select>";
    Cal_HTML += "&nbsp;&nbsp;&nbsp;<a style='cursor:hand;' OnClick='parent.ContShow_cal_IE(" + intPrevYear + "," + intPrevMonth + "," + intThisDay + ");'>◀</a> ";
    Cal_HTML += "<select name='selMonth' STYLE='font-size:11;' OnChange='parent.fnContChangeYearD(calendar.selYear.value, calendar.selMonth.value, " + intThisDay + ")';>";
    for (var i = 1; i < 13; i++) {
        Cal_HTML += "		<option value='" + Month_Val[i - 1] + "' ";
        if (intThisMonth == parseInt(Month_Val[i - 1], 10)) Cal_HTML += " selected>\n";
        else Cal_HTML += ">\n";
        Cal_HTML += Month_Val[i - 1] + "</option>\n";
    }
    Cal_HTML += "	</select>&nbsp;";
    Cal_HTML += "<a style='cursor:hand;' OnClick='parent.ContShow_cal_IE(" + intNextYear + "," + intNextMonth + "," + intThisDay + ");'>▶</a>";
    Cal_HTML += "</td></tr>";
    Cal_HTML += "<tr align=center bgcolor='#87B3D6' style='color:#2065DA;' height='25'>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>S</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>M</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>T</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>W</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>T</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>F</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>S</font></td>";
    Cal_HTML += "</tr>";

    for (intLoopWeek = 1; intLoopWeek < 7; intLoopWeek++) {	// 주단위 루프 시작, 최대 6주
        Cal_HTML += "<tr height='24' align=right bgcolor='white'>"
        for (intLoopDay = 1; intLoopDay <= 7; intLoopDay++) {	// 요일단위 루프 시작, 일요일 부터
            if (intThirdWeekday > 0) {											// 첫주 시작일이 1보다 크면
                Cal_HTML += "<td>";
                intThirdWeekday--;
            } else {
                if (thirdPrintDay > intLastDay) {								// 입력 날짝 월말보다 크다면
                    Cal_HTML += "<td>";
                } else {																// 입력날짜가 현재월에 해당 되면
                    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + day2(intThisMonth).toString() + day2(thirdPrintDay).toString() + " style=\"cursor:Hand;border:1px solid white;";
                    if (intThisYear == NowThisYear && intThisMonth == NowThisMonth && thirdPrintDay == intThisDay) {
                        Cal_HTML += "background-color:#C6F2ED;";
                    }

                    switch (intLoopDay) {
                        case 1: 														// 일요일이면 빨간 색으로
                            Cal_HTML += "color:red;"
                            break;
                        //case 7:   
                        //	Cal_HTML += "color:blue;"   
                        //	break;   
                        default:
                            Cal_HTML += "color:black;"
                            break;
                    }
                    Cal_HTML += "\">" + thirdPrintDay;
                }
                thirdPrintDay++;

                if (thirdPrintDay > intLastDay) {								// 만약 날짜 값이 월말 값보다 크면 루프문 탈출
                    Stop_Flag = 1;
                }
            }
            Cal_HTML += "</td>";
        }
        Cal_HTML += "</tr>";
        if (Stop_Flag == 1) break;
    }
    Cal_HTML += "</table></form></body></html>";

    var oPopBody = oPopup.document.body;
    oPopBody.style.backgroundColor = "lightyellow";
    oPopBody.style.border = "solid black 1px";
    oPopBody.innerHTML = Cal_HTML;

    var calHeight = oPopBody.document.all.ContCal_Table.offsetHeight;
    //행이 6개 행인지, 5개인지 구분
    if (intLoopWeek == 6) calHeight = 214;
    else calHeight = 189;

    oPopup.show(contPop_Left, (contPop_Top + target.offsetHeight), 170, calHeight, document.body);
}


function ContShow_cal_Mozilla(sYear, sMonth, sDay) {
    var Months_day = new Array(0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31)
    var Month_Val = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var intThisYear = new Number(), intThisMonth = new Number(), intThisDay = new Number();

    datToday = new Date(); 												// 현재 날자 설정

    intThisYear = parseInt(sYear, 10);
    intThisMonth = parseInt(sMonth, 10);
    intThisDay = parseInt(sDay, 10);

    if (intThisYear == 0) intThisYear = datToday.getFullYear(); 			// 값이 없을 경우
    if (intThisMonth == 0) intThisMonth = parseInt(datToday.getMonth(), 10) + 1; // 월 값은 실제값 보다 -1 한 값이 돼돌려 진다.
    if (intThisDay == 0) intThisDay = datToday.getDate();

    switch (intThisMonth) {
        case 1:
            intPrevYear = intThisYear - 1;
            intPrevMonth = 12;
            intNextYear = intThisYear;
            intNextMonth = 2;
            break;
        case 12:
            intPrevYear = intThisYear;
            intPrevMonth = 11;
            intNextYear = intThisYear + 1;
            intNextMonth = 1;
            break;
        default:
            intPrevYear = intThisYear;
            intPrevMonth = parseInt(intThisMonth, 10) - 1;
            intNextYear = intThisYear;
            intNextMonth = parseInt(intThisMonth, 10) + 1;
            break;
    }
    intPPyear = intThisYear - 1
    intNNyear = intThisYear + 1

    NowThisYear = datToday.getFullYear(); 								// 현재 년
    NowThisMonth = datToday.getMonth() + 1; 								// 현재 월
    NowThisDay = datToday.getDate(); 										// 현재 일

    datFirstDay = new Date(intThisYear, intThisMonth - 1, 1); 		// 현재 달의 1일로 날자 객체 생성(월은 0부터 11까지의 정수(1월부터 12월))
    intFirstWeekday = datFirstDay.getDay(); 								// 현재 달 1일의 요일을 구함 (0:일요일, 1:월요일)
    //intSecondWeekday = intFirstWeekday;
    intThirdWeekday = intFirstWeekday;

    datThisDay = new Date(intThisYear, intThisMonth, intThisDay); // 넘어온 값의 날자 생성
    //intThisWeekday = datThisDay.getDay();										// 넘어온 날자의 주 요일

    intPrintDay = 1; 															// 달의 시작 일자
    secondPrintDay = 1;
    thirdPrintDay = 1;

    Stop_Flag = 0

    if ((intThisYear % 4) == 0) {												// 4년마다 1번이면 (사로나누어 떨어지면)
        if ((intThisYear % 100) == 0) {
            if ((intThisYear % 400) == 0) {
                Months_day[2] = 29;
            }
        } else {
            Months_day[2] = 29;
        }
    }
    intLastDay = Months_day[intThisMonth]; 					// 마지막 일자 구함

    //Cal_HTML = "<html><body>";
    Cal_HTML = "";
    Cal_HTML += "<form name='calendar'>";
    Cal_HTML += "<table id=ContCal_Table border=0 bgcolor='#f4f4f4' cellpadding=1 cellspacing=1 width=100% style='font-size : 12;font-family:굴림;'>";
    Cal_HTML += "<tr height='35' align=center bgcolor='#f4f4f4'>";
    Cal_HTML += "<td colspan=7 align=center>";
    Cal_HTML += "	<select name='selYear' STYLE='font-size:11;' OnChange='fnContChangeYearD(calendar.selYear.value, calendar.selMonth.value, " + intThisDay + ")';>";
    for (var optYear = (intThisYear - 100); optYear < (intThisYear + 60); optYear++) {
        Cal_HTML += "		<option value='" + optYear + "' ";
        if (optYear == intThisYear) Cal_HTML += " selected>\n";
        else Cal_HTML += ">\n";
        Cal_HTML += optYear + "</option>\n";
    }
    Cal_HTML += "	</select>";
    Cal_HTML += "&nbsp;&nbsp;&nbsp;<a style='cursor:pointer;' OnClick='ContShow_cal_Mozilla(" + intPrevYear + "," + intPrevMonth + "," + intThisDay + ");'>◀</a> ";
    Cal_HTML += "<select name='selMonth' STYLE='font-size:11;' OnChange='fnContChangeYearD(calendar.selYear.value, calendar.selMonth.value, " + intThisDay + ")';>";
    for (var i = 1; i < 13; i++) {
        Cal_HTML += "		<option value='" + Month_Val[i - 1] + "' ";
        if (intThisMonth == parseInt(Month_Val[i - 1], 10)) Cal_HTML += " selected>\n";
        else Cal_HTML += ">\n";
        Cal_HTML += Month_Val[i - 1] + "</option>\n";
    }
    Cal_HTML += "	</select>&nbsp;";
    Cal_HTML += "<a style='cursor:pointer;' OnClick='ContShow_cal_Mozilla(" + intNextYear + "," + intNextMonth + "," + intThisDay + ");'>▶</a>";
    Cal_HTML += "<span style=\"cursor:pointer;color:red;position:relative;left:10px;bottom:5px;\" onClick=\"document.getElementById('contcalendar_div').style.display = 'none';\">X</span>";
    Cal_HTML += "</td></tr>";
    Cal_HTML += "<tr align=center bgcolor='#87B3D6' style='color:#2065DA;' height='25'>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>S</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>M</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>T</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>W</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>T</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>F</font></td>";
    Cal_HTML += "	<td style='padding-top:3px;' width='24'><font color=black>S</font></td>";
    Cal_HTML += "</tr>";

    for (intLoopWeek = 1; intLoopWeek < 7; intLoopWeek++) {	// 주단위 루프 시작, 최대 6주
        Cal_HTML += "<tr height='24' align=right bgcolor='white'>"
        for (intLoopDay = 1; intLoopDay <= 7; intLoopDay++) {	// 요일단위 루프 시작, 일요일 부터
            if (intThirdWeekday > 0) {											// 첫주 시작일이 1보다 크면
                Cal_HTML += "<td>";
                intThirdWeekday--;
            } else {
                if (thirdPrintDay > intLastDay) {								// 입력 날짝 월말보다 크다면
                    Cal_HTML += "<td>";
                } else {																// 입력날짜가 현재월에 해당 되면
                    Cal_HTML += "<td onClick='ContCalendar_Click(this);' title=" + intThisYear + day2(intThisMonth).toString() + day2(thirdPrintDay).toString() + " OnMouseOver=\"this.style.background = '#FFFF99';\" onmouseout=\"this.style.background = '#FFFFFF';\" style=\"cursor:pointer;";
                    if (intThisYear == NowThisYear && intThisMonth == NowThisMonth && thirdPrintDay == intThisDay) {
                        Cal_HTML += "background-color:#C6F2ED;";
                    }

                    switch (intLoopDay) {
                        case 1: 														// 일요일이면 빨간 색으로
                            Cal_HTML += "color:red;"
                            break;
                        //case 7:   
                        //	Cal_HTML += "color:blue;"   
                        //	break;   
                        default:
                            Cal_HTML += "color:black;"
                            break;
                    }
                    Cal_HTML += "\">" + thirdPrintDay;
                }
                thirdPrintDay++;

                if (thirdPrintDay > intLastDay) {								// 만약 날짜 값이 월말 값보다 크면 루프문 탈출
                    Stop_Flag = 1;
                }
            }
            Cal_HTML += "</td>";
        }
        Cal_HTML += "</tr>";
        if (Stop_Flag == 1) break;
    }
    Cal_HTML += "</table></form>";

    var calHeight;
    //행이 6개 행인지, 5개인지 구분
    if (intLoopWeek == 6) calHeight = 214;
    else calHeight = 189;

    Pop_div = document.getElementById('contcalendar_div');

    Pop_div.style.backgroundColor = "lightyellow";
    Pop_div.style.border = "solid black 1px";

    Pop_div.style.position = 'absolute';
    Pop_div.style.width = '170px';

    Pop_div.style.height = calHeight + 'px';
    //alert("Pop_div.style.height222=" + Pop_div.style.height);

    Pop_div.style.top = (contPop_Top + 13) + 'px';

    Pop_div.style.left = contPop_Left + 'px';

    Pop_div.innerHTML = Cal_HTML;

    Pop_div.style.backgroundColor = "lightyellow";
    Pop_div.style.border = "solid black 1px";

    if (Pop_div.style.display == 'none' || Pop_div.style.display == '') Pop_div.style.display = 'block';

}


function ContShow_cal_M(sYear, sMonth) {
    var intThisYear = new Number(), intThisMonth = new Number()
    datToday = new Date(); 												// 현재 날자 설정

    intThisYear = parseInt(sYear, 10);
    intThisMonth = parseInt(sMonth, 10);

    if (intThisYear == 0) intThisYear = datToday.getFullYear(); 			// 값이 없을 경우
    if (intThisMonth == 0) intThisMonth = parseInt(datToday.getMonth(), 10) + 1; // 월 값은 실제값 보다 -1 한 값이 돼돌려 진다.

    switch (intThisMonth) {
        case 1:
            intPrevYear = intThisYear - 1;
            intNextYear = intThisYear;
            break;
        case 12:
            intPrevYear = intThisYear;
            intNextYear = intThisYear + 1;
            break;
        default:
            intPrevYear = intThisYear;
            intNextYear = intThisYear;
            break;
    }
    intPPyear = intThisYear - 1
    intNNyear = intThisYear + 1

    Cal_HTML = "<html><head>\n";
    Cal_HTML += "</head><body>\n";
    Cal_HTML += "<table id=ContCal_Table border=0 bgcolor='#f4f4f4' cellpadding=1 cellspacing=1 width=100% onmouseover='parent.doOver(window.event.srcElement)' onmouseout='parent.doOut(window.event.srcElement)' style='font-size : 12;font-family:굴림;'>\n";
    Cal_HTML += "<tr height='30' align=center bgcolor='#f4f4f4'>\n";
    Cal_HTML += "<td colspan='4' align='center'>\n";
    Cal_HTML += "<a style='cursor:pointer;' OnClick='parent.ContShow_cal_M(" + intPPyear + "," + intThisMonth + ");'>◀</a>&nbsp;";
    Cal_HTML += "<select name='selYear' STYLE='font-size:11;' OnChange='parent.fnContChangeYearM(this.value, " + intThisMonth + ")';>";
    for (var optYear = (intThisYear - 100); optYear < (intThisYear + 60); optYear++) {
        Cal_HTML += "		<option value='" + optYear + "' ";
        if (optYear == intThisYear) Cal_HTML += " selected>\n";
        else Cal_HTML += ">\n";
        Cal_HTML += optYear + "</option>\n";
    }
    Cal_HTML += "	</select>\n";
    Cal_HTML += "<a style='cursor:pointer;' OnClick='parent.ContShow_cal_M(" + intNNyear + "," + intThisMonth + ");'>▶</a>";
    Cal_HTML += "</td></tr>\n";
    Cal_HTML += "<tr><td colspan=4 height='1' bgcolor='#000000'></td></tr>";
    Cal_HTML += "<tr height='20' align=center bgcolor=white>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-01" + " style=\"cursor:pointer;\">Jan</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-02" + " style=\"cursor:pointer;\">Feb</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-03" + " style=\"cursor:pointer;\">Mar</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-04" + " style=\"cursor:pointer;\">Apr</td>";
    Cal_HTML += "</tr>\n";
    Cal_HTML += "<tr height='20' align=center bgcolor=white>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-05" + " style=\"cursor:pointer;\">May</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-06" + " style=\"cursor:pointer;\">Jun</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-07" + " style=\"cursor:pointer;\">Jul</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-08" + " style=\"cursor:pointer;\">Aug</td>";
    Cal_HTML += "</tr>\n";
    Cal_HTML += "<tr height='20' align=center bgcolor=white>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-09" + " style=\"cursor:pointer;\">Sep</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-10" + " style=\"cursor:pointer;\">Oct</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-11" + " style=\"cursor:pointer;\">Nov</td>";
    Cal_HTML += "<td onClick=parent.ContCalendar_Click(this); title=" + intThisYear + "-12" + " style=\"cursor:pointer;\">Dec</td>";
    Cal_HTML += "</tr>\n";
    Cal_HTML += "</table>\n</body></html>";

    var oPopBody = oPopup.document.body;
    oPopBody.style.backgroundColor = "lightyellow";
    oPopBody.style.border = "solid black 1px";
    oPopBody.innerHTML = Cal_HTML;

    oPopup.show(contPop_Left, (contPop_Top + target.offsetHeight), 160, 99, document.body);
}


//----------------------------------
//	일Calendar 년도리스트에서 년도 선택
//----------------------------------
function fnContChangeYearD(sYear, sMonth, sDay) {
    if (ContCheck_navigator()) { //true 면 IE
        ContShow_cal_IE(sYear, sMonth, sDay);
    } else { //아니면 모질라
        ContShow_cal_Mozilla(sYear, sMonth, sDay);
    }


}


//----------------------------------
//	월Calendar 년도리스트에서 년도 선택
//----------------------------------
function fnContChangeYearM(sYear, sMonth) {
    ContShow_cal_M(sYear, sMonth);
}


/**
HTML 개체용 유틸리티 함수
**/
function GetObjectCalTop(objCont) {
    if (objCont.offsetParent == document.body) {
        return objCont.offsetTop;
    }
    else {
        if (objCont.offsetParent != null) {
            return objCont.offsetTop + GetObjectCalTop(objCont.offsetParent);
        }
        else {
            //return objCont.offsetTop;
            return objCont.offsetTop;
        }
    }
}

function GetObjectCalLeft(objCont) {
    if (objCont.offsetParent == document.body) {
        return objCont.offsetLeft;
    }
    else {
        if (objCont.offsetParent != null) {
            return objCont.offsetLeft + GetObjectCalLeft(objCont.offsetParent);
        }
        else {
            return objCont.offsetLeft;
        }
    }
}