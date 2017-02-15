<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngPaymentList.aspx.cs" Inherits="KN.Web.Management.Manage.MngPaymentList" ValidateRequest="false" EnableEventValidation="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnMovePage(intPageNo) 
        {
            if (intPageNo == null) 
            {
                intPageNo = 1;
            }
            
            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }
        
        function fnExcelPrint(strTxt)
        {
            var strRentalYear = document.getElementById("<%=ddlYear.ClientID%>");
            var strRentalMonth = document.getElementById("<%=ddlMonth.ClientID%>");
            
            if (trim(strRentalYear.value) == "")
            {
                strRentalYear.focus();
                alert(strTxt);
                return false;
            }
            
            if (trim(strRentalMonth.value) == "")
            {
                strRentalMonth.focus();
                alert(strTxt);
                return false;
            }
            
            return true;
        }

    
        function fnDetailView(strRentalYear, strRentalMM, strUserSeq)
        {
            document.getElementById("<%=hfRentalYear.ClientID%>").value = strRentalYear;
            document.getElementById("<%=hfRentalMM.ClientID%>").value = strRentalMM;
            document.getElementById("<%=hfUserSeq.ClientID%>").value = strUserSeq;

            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
        }
        
        function fnMngFeeEntirePrint()
        {
            var strData1 = document.getElementById("<%=hfRentCd.ClientID%>").value;
            var strData2 = document.getElementById("<%=hfFeeTy.ClientID%>").value;
            var strData3 = document.getElementById("<%=txtNm.ClientID%>").value;
            var strData4 = document.getElementById("<%=txtSearchFloor.ClientID%>").value;
            var strData5 = document.getElementById("<%=txtSearchRoom.ClientID%>").value;
            var strData6 = document.getElementById("<%=ddlPayYn.ClientID%>").value;
            var strData7 = document.getElementById("<%=ddlLateYn.ClientID%>").value;
            var strData8 = document.getElementById("<%=ddlYear.ClientID%>").value;
            var strData9 = document.getElementById("<%=ddlMonth.ClientID%>").value;
            
            window.open("/Common/RdPopup/RDPopupMngPaymentList.aspx?Datum0=" + strData1 + "&Datum1=" + strData2 + "&Datum2=" + strData3 + "&Datum3=" + strData4 + "&Datum4=" + strData5 + "&Datum5=" + strData6 + "&Datum6=" + strData7 + "&Datum7=" + strData8 + "&Datum8=" + strData9, "fnMngFeeEntirePrint", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }
        
        function fnMngFeeChartPrint(strTxt)
        {
            var strData1 = document.getElementById("<%=hfRentCd.ClientID%>").value;
            var strData2 = document.getElementById("<%=hfFeeTy.ClientID%>").value;
            var strData3 = document.getElementById("<%=ddlYear.ClientID%>");
            var strData4 = document.getElementById("<%=ddlMonth.ClientID%>");
            
            if (trim(strData3.value) == "")
            {
                strData3.focus();
                alert(strTxt);
                return false;
            }
            
            if (trim(strData4.value) == "")
            {
                strData4.focus();
                alert(strTxt);
                return false;
            }
            
            window.open("/Common/RdPopup/RDPopupMngPaymentChart.aspx?Datum0=" + strData1 + "&Datum1=" + strData2 + "&Datum2=" + strData3 + "&Datum3=" + strData4, "fnMngFeeEntirePrint", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }
        
        function fnCheckValidate(strAlert)
        {
            var strNm = document.getElementById("<%=txtNm.ClientID%>");
            var strSearchFloor = document.getElementById("<%=txtSearchFloor.ClientID%>");
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strYear = document.getElementById("<%=ddlYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");

            if (trim(strNm.value) == "" && trim(strSearchFloor.value) == "" && trim(strSearchRoom.value) == "" && trim(strYear.value) == "" && trim(strMonth.value) == "")
            {       
                alert(strAlert);
                return false;
            }
            
            return true;
        }
        
        function fnPrintOutBill(strTxt)
        {
            var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
            var strFeeTy = document.getElementById("<%=hfFeeTy.ClientID%>").value;            
            var strYear = document.getElementById("<%=ddlYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");
            
            if (trim(strYear.value) == "")
            {
                strYear.focus();
                alert(strTxt);
                return false;
            }
            
            if (trim(strMonth.value) == "")
            {
                strMonth.focus();
                alert(strTxt);
                return false;
            }

            // Datum0 : 입주자번호
            // Datum1 : 섹션코드
            // Datum2 : 관리비/주차비/임대료
            // Datum3 : 거주년
            // Datum4 : 거주월
            window.open("/Common/RdPopup/RDPopupRentalMngFeeDetail.aspx?Datum0=&Datum1=" + strRentCd + "&Datum2=" + strFeeTy + "&Datum3=" + strYear.value + "&Datum4=" + strMonth.value, "ManageFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }
    //-->
    </script>
    <asp:UpdatePanel ID="upSearch" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged"/>
        </Triggers>
        <ContentTemplate>
	    <fieldset class="sh-field5 MrgB10">
            <legend>검색</legend>
            <ul class="sf5-ag MrgL30 ">
                <li><asp:Literal ID="ltName" runat="server"></asp:Literal></li>
                <li><asp:TextBox ID="txtNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>
                <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
	            <li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	            <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
	            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	            <li>
                    <div class="Btn-Type1-wp Mrg0">
                        <div class="Btn-Tp-L">
                            <div class="Btn-Tp-R">
                                <div class="Btn-Tp-M">
                                    <span><asp:LinkButton ID="lnkbtnBillPrint" runat="server" 
                                        onclick="lnkbtnBillPrint_Click"></asp:LinkButton></span>
                                </div>
                            </div>
                        </div>
                    </div>
	            </li>
            </ul>
            <ul class="sf5-ag MrgL30 bgimgN">
	            <li>
	                <asp:Literal ID="ltPayNoPaid" runat="server"></asp:Literal>
	                <asp:DropDownList ID="ddlPayYn" runat="server"></asp:DropDownList>
	            </li>
	            <li>
	                <asp:Literal ID="ltLate" runat="server"></asp:Literal>
	                <asp:DropDownList ID="ddlLateYn" runat="server"></asp:DropDownList>
	            </li>
	            <li>
	                <div class="C235-st FloatL">
	                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
	                    <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
                    </div>
	            </li>
	            <li>
		            <div class="Btn-Type4-wp">
			            <div class="Btn-Tp4-L">
				            <div class="Btn-Tp4-R">
					            <div class="Btn-Tp4-M">
						            <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
					            </div>
				            </div>
			            </div>
		            </div>		        
	            </li>
            </ul>
        </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        </Triggers>
        <ContentTemplate>
        <table class="TypeA">
            <col width="85px"/>
            <col width="195px"/>
            <col width="84px"/>
            <col width="84px"/>
            <col width="75px"/>
            <col width="75px"/>
            <col width="74px"/>
            <col width="84px"/>
            <thead>
                <tr>			            
	                <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltNameTitle" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltReceiteTitle" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltLateYn" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltTotalPay" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltPayment" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltRemainder" runat="server"></asp:Literal></th>
	                <th class="Ls-line"><asp:Literal ID="ltPayDay" runat="server"></asp:Literal></th>
                </tr>
            </thead>
            <tbody>
            </tbody>                
        </table>
        <asp:ListView ID="lvMngPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMngPaymentList_ItemCreated"
            OnLayoutCreated="lvMngPaymentList_LayoutCreated" OnItemDataBound="lvMngPaymentList_ItemDataBound">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="85px"/>
                    <col width="195px"/>
                    <col width="84px"/>
                    <col width="84px"/>
                    <col width="75px"/>
                    <col width="75px"/>
                    <col width="74px"/>
                    <col width="84px"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("RentalYear")%>","<%#Eval("RentalMM")%>","<%#Eval("UserSeq") %>");'>                
			        <td class="TbTxtCenter"><asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltNameList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltPayNoPaidList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltLateYnList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltTotalPayList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltPaymentList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltRemainderList" runat="server"></asp:Literal></td>
			        <td class="TbTxtCenter"><asp:Literal ID="ltPayDayList" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="8" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <div><span id="spanPageNavi" runat="server" style="width:100%"></span></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btwps FloatR2">
	    <div class="Btn-Type3-wp FloatL">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnChartPrint" runat="server"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp FloatL">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnPrint" runat="server" OnClientClick="javascript:return fnMngFeeEntirePrint();"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp FloatL">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfRentalYear" runat="server"/>
    <asp:HiddenField ID="hfRentalMM" runat="server"/>
    <asp:HiddenField ID="hfUserSeq" runat="server"/>    
    <asp:HiddenField ID="hfRentCd" runat="server"/>
    <asp:HiddenField ID="hfFeeTy" runat="server"/>    
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>