<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OUTList.aspx.cs" Inherits="KN.Web.Inventory.OUTList" ValidateRequest="false"%>
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
    
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            
            return false;
        }
    }

    function fnDetailView(strUserSeq)
    {
        var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
        // Datum0 : 입주자번호
        // Datum1 : 섹션코드
        // Datum2 : 관리비/주차비/임대료
        // Datum3 : 거주년
        // Datum4 : 거주월

        document.getElementById("<%=hfUserSeq.ClientID%>").value = strUserSeq;

        window.open("/Common/Popup/PopupStoreSettleDetail.aspx?Datum0=" + strUserSeq + "&Datum1=&Datum2=&Datum3=&Datum4=", "DetailStoreVeiw", "status=no, resizable=no, width=1100, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
        return false;
    }

    function fnDetailViewJs(strTxt)
    {
        var strData3 = document.getElementById("<%=ddlYear.ClientID%>");

        if (trim(strData3.value) == "")
        {
            strData3.focus();
            alert(strTxt);
            return false;
        }

        var strData4 = document.getElementById("<%=ddlMonth.ClientID%>");

        if (trim(strData4.value) == "")
        {
            strData4.focus();
            alert(strTxt);
            return false;
        }

        var strData1 = document.getElementById("<%=ddlPayment.ClientID%>");

        if (trim(strData1.value) == "")
        {
            strData1.focus();
            alert(strTxt);
            return false;
        }

        var strData2 = document.getElementById("<%=ddlDocument.ClientID%>");

        if (trim(strData2.value) == "")
        {
            strData2.focus();
            alert(strTxt);
            return false;
        }

        var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
        var strUserNm = document.getElementById("<%=txtSearchNm.ClientID%>").value;
        var strRoomNo = document.getElementById("<%=txtSearchRoom.ClientID%>").value;

        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;

        if (trim(strData2.value) == "0001")
        {
            // Datum0 : 입주자번호
            // Datum1 : 섹션코드
            // Datum2 : 관리비/주차비/임대료
            // Datum3 : 거주년
            // Datum4 : 거주월
            if (trim(strData1.value) == "0001")
            {
                window.open("/Common/RdPopup/RDPopupMngFeeDetail.aspx?Datum0=&Datum1=" + strRentCd + "&Datum2=" + strData1.value + "&Datum3=" + strData3.value + "&Datum4=" + strData4.value, "ManageFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            }
            else if (trim(strData1.value) == "0002")
            {
                window.open("/Common/RdPopup/RDPopupRentalFeeDetail.aspx?Datum0=&Datum1=" + strRentCd + "&Datum2=" + strData1.value + "&Datum3=" + strData3.value + "&Datum4=" + strData4.value, "ManageFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            }
            else if (trim(strData1.value) == "0011")
            {
                window.open("/Common/RdPopup/RDPopupUtilFeeDetail.aspx?Datum0=" + strData3.value + "&Datum1=" + strData4.value + "&Datum2=&Datum3=" + strUserNm + "&Datum4=" + strRoomNo + "&Datum5=" + strRentCd, "UtilFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            }   
            else if (trim(strData1.value) == "0004")
            {
                window.open("/Common/RdPopup/RDPopupUtilFeeDetail.aspx?Datum0=" + strData3.value + "&Datum1=" + strData4.value + "&Datum2=&Datum3=" + strUserNm + "&Datum4=" + strRoomNo + "&Datum5=" + strRentCd, "UtilFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            }  
            else if (trim(strData1.value) == "0005")
            {
                window.open("/Common/RdPopup/RDPopupUtilFeeDetail.aspx?Datum0=" + strData3.value + "&Datum1=" + strData4.value + "&Datum2=&Datum3=" + strUserNm + "&Datum4=" + strRoomNo + "&Datum5=" + strRentCd, "UtilFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            }               
        }
        else if (trim(strData2.value) == "0002")
        {
            window.open("/Common/RdPopup/RDPopupReceiptList.aspx?Datum0=&Datum1=&Datum2=" + strData1.value + "&Datum3=" + strData3.value + "&Datum4=" + strData4.value, "PayedReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        } 

        return false;
    }

    function fnCheckValidate(strText)
    {
        var strSearchNm = document.getElementById("<%=txtSearchNm.ClientID%>");
        var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");

        if (trim(strSearchNm.value) == "" && trim(strSearchRoom.value) == "")
        {
            if (trim(strSearchNm.value) == "")
            {
                strSearchNm.focus();
                alert(strText);
                return false;
            }
            
            if (trim(strSearchRoom.value) == "")
            {
                strSearchRoom.focus();
                alert(strText);
                return false;
            }
        }

        return true;
    }
    //-->
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <fieldset class="sh-field2 MrgB10">
                <legend>출력물</legend>
                <ul class="sf2-ag MrgL10">
                    <li><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></li>
                    <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>
                    <li><asp:DropDownList ID="ddlPayment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged"></asp:DropDownList></li>
                    <li><asp:DropDownList ID="ddlDocument" runat="server"></asp:DropDownList></li>
                    <li>
                        <div class="Btn-Type4-wp">
			                <div class="Btn-Tp4-L">
				                <div class="Btn-Tp4-R">
					                <div class="Btn-Tp4-M">
						                <span><asp:LinkButton ID="lnkbtnPrint" runat="server"></asp:LinkButton></span>
					                </div>
				                </div>
			                </div>
		                </div>
                    </li>
                </ul>
            </fieldset>
            <fieldset class="sh-field2 MrgB10">
                <legend>검색</legend>
                <ul class="sf2-ag MrgL10">
                    <li><asp:Literal ID="ltSearchName" runat="server"></asp:Literal></li>
                    <li><asp:TextBox ID="txtSearchNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
	                <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
	                <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
	                <li>
	                    <div class="C235-st FloatL">
	                        <asp:DropDownList ID="ddlRentNm" runat="server" OnKeyPress="javascript:return fnCheckType();"></asp:DropDownList>
                        </div>
	                </li>
	                <li>
		            <div class="Btn-Type4-wp">
                        <div class="Btn-Tp4-L">
                            <div class="Btn-Tp4-R">
                                <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                            </div>
                        </div>
                    </div>
	                </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPaymentList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <col width="60"/>
                <col width="70"/>
                <col width="110"/>
                <col width="210"/>
                <col width="130"/>
                <col width="130"/>
                <col width="130"/>
                <thead>
                    <tr>
	                    <th class="Fr-line"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltRentNm" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltMngFee" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltRentalFee" runat="server"></asp:Literal></th>
	                    <th class="Ls-line"><asp:Literal ID="ltUtilFee" runat="server"></asp:Literal></th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                </tbody>
            </table>
            <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvPaymentList_ItemCreated" OnItemDataBound="lvPaymentList_ItemDataBound">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="60"/>
                        <col width="70"/>
                        <col width="110"/>
                        <col width="210"/>
                        <col width="130"/>
                        <col width="130"/>
                        <col width="130"/>
                        <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                        <tr>
                            <td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width:100%"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfUserSeq" runat="server"/>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfRentCd" runat="server" />
</asp:Content>