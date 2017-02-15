/*****************************************************************
 * 함수		명	:	trim
 * 함수	  설명	:	문자 앞뒤의 공백제거하는 함수
 * 작   성  자  :	양영석
 * 작   성  일  :	2010년 07월 13일
 * Input    값  :	String
 * Output   값  :	String
 *****************************************************************/
function trim(str)
{
	var count = str.length;
	var len = count;
	var st = 0;

	while ((st < len) && (str.charAt(st) <= ' '))
	{
       	st++;
    }

	while ((st < len) && (str.charAt(len - 1) <= ' '))
	{
    	len--;
	}

	return ((st > 0) || (len < count)) ? str.substring(st, len) : str ;
}

/*****************************************************************
 * 함수	    명	:	peridCheck
 * 함수   설명	:	주민등록 번호가 올바른지 확인하는 함수
 * 작   성  자  :	양영석
 * 작   성  일  :	2010년 07월 13일
 * Input    값  :	String, String
 * Output   값  :	주민등록 번호의 형태일 경우 false,  아닐경우 true
 *****************************************************************/
function peridCheck(strPerid1, strPerid2)
{
	var idnumber = strPerid1+strPerid2;

	a = new Array(13);

	for(var i=0; i<13;i++)
	{
		a[i] = parseInt(idnumber.charAt(i));
	}

	var j = a[0]*2 + a[1]*3 + a[2]*4 + a[3]*5 + a[4]*6 + a[5]*7 + a[6]*8 + a[7]*9 + a[8]*2 + a[9]*3 + a[10]*4 + a[11]*5;
	var j = j % 11;
	var k = 11 - j;

	if(k > 9)
	{
		k = k % 10
	}

	if(k != a[12])
	{
		return true; //올바르지 않은 번호
	}
	else
	{
		return false; //올바른 번호
	}
}

/*****************************************************************
 * 함수		명	: IsNumeric
 * 함수	  설명	: 숫자만 입력가능한지 체크하는 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 숫자일 경우 alert
 * 용       법  : onkeypress="javascript:IsNumeric(this, '숫자만 입력하십시오.');"
 *****************************************************************/
function IsNumeric(obj, strText)
{
    // 문자입력 금지 함수 설정
    if ((event.keyCode < 48 || event.keyCode > 57)&& event.keyCode != 13) 
	{
	    event.keyCode = 0;
	    alert(strText);
	    
	    obj.value = "";
	    obj.focus();
	    
	    event.returnValue = false; 
	}
}

/*****************************************************************
 * 함수		명	: IsNumericOrDot
 * 함수	  설명	: 숫자와 .(Dot)만 입력가능한지 체크하는 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 숫자일 경우 alert
 * 용       법  : onkeypress="javascript:IsNumericOrDot(this, '소수점 형식의 숫자만 입력하십시오.');"
 *****************************************************************/
function IsNumericOrDot(obj, strText) 
{
    if (event.keyCode != 46 && ((event.keyCode < 48) || (event.keyCode > 57)) && event.keyCode != 13) 
	{
	    event.keyCode = 0;
	    alert(strText);

	    obj.value = "";
	    obj.focus();

	    event.returnValue = false; 
    }
}


/*****************************************************************
* 함수		명	: IsNumericOrDot
* 함수	  설명	: 숫자와 .(Dot)만 입력가능한지 체크하는 함수
* 작   성  자  : 양영석
* 작   성  일  : 2010년 07월 13일
* Input    값  :
* Output   값  : 숫자일 경우 alert
* 용       법  : onkeypress="javascript:IsSpace(this, '소수점 형식의 숫자만 입력하십시오.');"
*****************************************************************/
function IsSpace(obj, strText) {
    if (event.keyCode == 32) {       
        //alert(strText);

        //obj.value = "";
        obj.focus();

        event.returnValue = false;
    } else {
        return true;
    }
}

/*****************************************************************
 * 함수		명	: fnEnterBlock
 * 함수	  설명	: 엔터입력시 AutoPostBack을 막기위한 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 
 * 용       법  : <body onkeypress="fnEnterBlock();">
 *****************************************************************/
function fnEnterBlock() 
{ 
    if (event.keyCode == 13) 
        { 
            self.focus(); 
            return false; 
        } 
}

/*****************************************************************
 * 함수  명 : addComma
 * 함수   설명 : 3자리수 + 콤마 (금액단위 표현)
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  : 숫자값
 * Output   값  : 
 * 용       법  : addComma(숫자값);
 *****************************************************************/
 function addComma(str)
{
 var input_str = str.toString();
 if (input_str == '') return false;
 input_str = parseInt(input_str.replace(/[^0-9]/g, '')).toString();
 if (isNaN(input_str)) { return false; }
 var sliceChar = ',';
 var step = 3;
 var step_increment = -1;
 var tmp  = '';
 var retval = '';
 var str_len = input_str.length;
 for (var i=str_len; i>=0; i--)
 {
  tmp = input_str.charAt(i);
  if (tmp == sliceChar) continue;
  if (step_increment%step == 0 && step_increment != 0) retval = tmp + sliceChar + retval;
  else retval = tmp + retval;
  step_increment++;
 }
 return retval;
}
/*****************************************************************
 * 함수		명	: ConfirmOK
 * 함수	  설명	: 저장하기 전에 확인하는 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 
 * 용       법  : onClientClick="javascript: return ConfirmOK();"
 *****************************************************************/
function ConfirmOK()
{
    if(confirm("저장 하시겠습니까?"))
        return true;
    else
        return false;
}
/*****************************************************************
 * 함수		명	: DeleteOK
 * 함수	  설명	: 삭제하기 전에 확인하는 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 
 * 용       법  : onClientClick="javascript: return DeleteOK();"
 *****************************************************************/
function DeleteOK()
{
    if(confirm("정말로 삭제하시겠습니까?"))
        return true;
    else
        return false;
}

/*****************************************************************
 * 함수		명	: fnBizNoCheck
 * 함수	  설명	: 사업자등록번호가 올바른지 체크하는 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 
 * 용       법  : onClientClick="javascript: return BizNoCheck();"
 *****************************************************************/
function fnBizNoCheck(strBizRegNo)
{
    // strBizRegNo는 숫자만 10자리로 해서 문자열로 넘긴다.
    var checkID = new Array(1, 3, 7, 1, 3, 7, 1, 3, 5, 1);
    var tmpBizID;
    var intTmpI;
    var intSum=0;
    var strSndNo;
    var intResult;

    strBizRegNo = strBizRegNo.replace(/-/gi,'');

    for (intTmpI=0; intTmpI<=7; intTmpI++)
    {
    	intSum = intSum + checkID[intTmpI] * strBizRegNo.charAt(intTmpI);
    }

	strSndNo = "0" + (checkID[8] * strBizRegNo.charAt(8));
    strSndNo = strSndNo.substring(strSndNo.length - 2, strSndNo.length);
    intSum = intSum + Math.floor(strSndNo.charAt(0)) + Math.floor(strSndNo.charAt(1));
    intResult = (10 - (intSum % 10)) % 10 ;

    if (Math.floor(strBizRegNo.charAt(9)) == intResult)
    {
    	return true;
    }

    return false;
}

/*****************************************************************
 * 함수		명	: CancelOK
 * 함수	  설명	: 취소 확인 여부 확인 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  :
 * Output   값  : 
 * 용       법  : onClientClick="return CancelOK();"
 *****************************************************************/
function CancelOK()
{
    if(confirm("작업하신 내용을 취소하시겠습니까?"))
        return true;
    else
        return false;
}
/*****************************************************************
 * 함수		명	: CancelOK
 * 함수	  설명	: 법인번호 체크 함수
 * 작   성  자  : 양영석
 * 작   성  일  : 2010년 07월 13일
 * Input    값  : object명
 * Output   값  : 
 * 용       법  : onClientClick="return CancelOK();"
 *****************************************************************/
function CheckBubin(obj)
{
    var err = 0;

    var objchar = eval("document.all."+ obj + "1");
    var objchar2 = eval("document.all."+ obj + "2");

    if(objchar.value.length != 6)
    {
        alert("법인등록번호를 정확히 입력하여 주세요.");
        objchar.value = "";
        objchar.focus();
        return false;
    }
    if(objchar2.value.length != 7)
    {
        alert("법인등록번호를 정확히 입력하여 주세요.");
        objchar2.value = "";
        objchar2.focus();
        return false;
    }

    for(CB_i=0;CB_i<objchar2.value.length;CB_i++)
    {
        var bubinnum=objchar2.value.charAt(CB_i);

        if (bubinnum < '0' || bubinnum > '9')
        {
            alert("법인등록번호는 숫자만 가능합니다.");
            objchar2.value = objchar2.value.substring(0, CB_i);;
            objchar2.focus();
            return false;
        }
    }

    if(objchar2.value)
    {
         if(objchar2.value.length == 7)
         {
            var fullbubin = objchar.value + objchar2.value;
            var hap = 0;
            var j = 0;

            for (CB_ii=0; CB_ii<12;CB_ii++)
            {
                if(j < 1 || j > 2){j=1;}
                hap = hap + (parseInt(fullbubin.charAt(CB_ii)) * j);
                j++;
            }

            if ((10 - (hap%10))%10 != parseInt(fullbubin.charAt(12)))
            {
                err=1;
            }

            if (err == 1)
            {
                alert("올바른 법인등록번호가 아닙니다.");
                objchar.value = "";
                objchar2.value = "";
                objchar.focus();
                return false;
            }
        }
    }
    return true;
}

/*****************************************************************
 * 함수     명 : delPoint
 * 함수   설명 : 소숫점자리 절삭 (금액단위 표현)
 * 작   성  자 : 양영석
 * 작   성  일 : 2010년 07월 13일
 * Input    값 : 숫자값
 * Output   값 : 
 * 용       법 : delPoint(숫자값);
 *****************************************************************/
 function delPoint(int_value)
{
   var re_value = int_value + "";
   return (parseInt(re_value.substr(0, re_value.length - 3)) * 1);

}

/*****************************************************************
* 함수     명 : fnCheckData
* 함수   설명 : 값유무에 따른 Boolean값 리턴
* 작   성  자 : 양영석
* 작   성  일 : 2010년 07월 30일
* Input    값 : boolean
* Output   값 : 
* 용       법 : fnCheckData(object, 경고문);
*****************************************************************/
function fnCheckData(objItems, strText)
{
    if (trim(objItems.value) == "")
    {
        alert(strText);
        return false;
    }
    else
    {
        return true;
    }
}

/*****************************************************************
* 함수     명 : fnCheckDataWithFocus
* 함수   설명 : 값유무에 따른 Boolean값 리턴 (포커스 이동)
* 작   성  자 : 양영석
* 작   성  일 : 2010년 07월 30일
* Input    값 : boolean
* Output   값 : 
* 용       법 : fnCheckDataWithFocus(object, 경고문);
*****************************************************************/
function fnCheckDataWithFocus(objItems, strText)
{
    if (trim(objItems.value) == "")
    {
        alert(strText);
        objItems.focus();
        return false;
    }
    else
    {
        return true;
    }
}

/*****************************************************************
* 함수     명 : fnConfirm
* 함수   설명 : Confirm 처리
* 작   성  자 : 양영석
* 작   성  일 : 2010년 08월 11일
* Input    값 : boolean
* Output   값 : 
* 용       법 : fnConfirm(질의문);
*****************************************************************/
function fnConfirm(strText)
{
    if (confirm(strText))
    {
        return true;
    }
    else
    {
        return false;
    }
}

/*****************************************************************
* 함수     명 : fnCheckFileTypeExcel
* 함수   설명 : Excel 타입중 Excel 97-2003 타입만 리딩함.
* 작   성  자 : 양영석
* 작   성  일 : 2010년 09월 3일
* Input    값 : void
* Output   값 : 
* 용       법 : fnCheckFileTypeExcel(해당 컨트롤, 에러문구);
*****************************************************************/
function fnCheckFileTypeExcel(file, strText)
{
    if (file && file.value.length > 0)
    {
        if (!event.srcElement.value.match(/(.xls|.xlS|.xLs|.Xls.xLS|.XLs|.XlS|.XLS|[0-7]{8})/))
        {
            alert(strText);
            file.select();
            document.selection.clear();
        }
    }
}
/*****************************************************************
* 함수     명 : newWindow
* 함수   설명 : 출력물 팝업.
* 작   성  자 : 심환영
* 작   성  일 : 2010년 10월 15일
* Input    값 : void
* Output   값 : 
* 용       법 : newWindow
*****************************************************************/
function newWindow(Curl, Cname, cWidth, cHeight) {
var int_windowLeft = (screen.width - cWidth) / 2;
var int_windowTop = (screen.height - cHeight) / 2;
var str_windowProperties = 'height=' + cHeight + ',width=' + cWidth + ',top=' + int_windowTop + ',left=' + int_windowLeft + ',scrollbars=yes, resizable=no';
    var obj_window = window.open(Curl, Cname, str_windowProperties);
	if (parseInt(navigator.appVersion) >= 4) {
	  obj_window.window.focus();
	}
}
/*******************************
    New Calender by:BaoTV
*******************************/
function CallCalendar(parameters) {
    $(parameters).focus();
}

/*******************************
New Loading by:BaoTV
*******************************/
function ShowLoading(parameters) {
    if (parameters!="") {
        $(".knTw").mask(parameters);
    } else {
        $(".knTw").mask("Waiting...");
    }

}
/*******************************
New Close Loading by:BaoTV
*******************************/
function CloseLoading() {
    $(".knTw").unmask();   
}



