<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngReadMonthForOverTimeAir.aspx.cs" Inherits="KN.Web.Management.Remote.MngReadMonthForOverTimeAir" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
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

    function fnCheckValidate(strAlert)
    {        
        var strSearchFloor = document.getElementById("<%=txtSearchFloor.ClientID%>");
        var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
        var strYear = document.getElementById("<%=ddlYear.ClientID%>");
        var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");
            
        if (trim(strSearchFloor.value) == "" && trim(strSearchRoom.value) == "" && trim(strYear.value) == "" && trim(strMonth.value) == "")
        {       
            alert(strAlert);
            return false;
        }
        
        return true;
    }

    function fnDetailView(strRoomNo, strChargeSeq)
    {
       // alert(strRoomNo + "-" + strChargeSeq);
        $('#<%=txthfRoomNo.ClientID %>').val(strRoomNo);
        $('#<%=txthfChargeSeq.ClientID %>').val(strChargeSeq);
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;

//        window.open("/Common/RDPopup/RDPopupEnergyDetail.aspx?Datum1=&Datum2=" + strData2 + "&Datum3=" + strData3 + "&Datum4=" + strData4, "EnergyDetail", "status=no, resizable=no, width=730, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        
        return false;
    }
    
    function fnMakeExcelFile()
    {
        document.getElementById("<%=imgbtnMakeExcel.ClientID%>").click();
    }
    
    function fnModifyData(strAlertText)
    {
        var strYear = document.getElementById("<%=ddlYear.ClientID%>");
        var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");
        
        if (trim(strYear.value) == "")
        {
            strYear.focus();
            alert(strAlertText);
            
            return false;
        }
        
        if (trim(strMonth.value) == "")
        {
            strMonth.focus();
            alert(strAlertText);
            
            return false;
        }
        
        return true;
    }
    //-->
    </script>
    <div class="Tab-wp MrgB10">
        <ul class="TabM">
            <li><asp:LinkButton ID="lnkbtnDay" runat="server" OnClick="lnkbtnDay_Click"></asp:LinkButton></li>
            <li class="Over CursorNon"><asp:Literal ID="ltMonth" runat="server"></asp:Literal></li>
        </ul>
    </div>
    <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
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
            <li>
                <div class="Btn-Type1-wp Mrg0">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span><asp:LinkButton ID="lnkbtnMakeInvoice" runat="server" OnClick="lnkbtnMakeInvoice_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <table class="TbCel-Type6-A MrgT10 ">
                <col width="30%" />
                <col width="10%" />
                <col width="10%" />
                <col width="10%" />
                <col width="10%" />
                <col width="5%" />
                <col width="5%" />
                <col width="5%" />
                <col width="5%" />
                <col width="10%" />
        <tr>
            <th>Tenant Name</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Fist Index</th>
            <th>End Index</th>
            <th>Normal</th>
            <th>Height</th>
            <th>Low</th>
            <th>Other</th>
            <th>Discount</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvDayChargeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemDataBound="lvDayChargeList_ItemDataBound" OnItemCreated="lvDayChargeList_ItemCreated">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="30%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="5%" />
                        <col width="5%" />
                        <col width="5%" />
                        <col width="5%" />
                        <col width="10%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("RoomNo")%>","<%#Eval("ChargeSeq")%>");'>
                        <td align="center" class="P0">
                            <asp:Literal ID="ltUserNm" runat="server"></asp:Literal>
                            <asp:HiddenField ID="txthfChargeSeq" runat="server" />
                            <asp:HiddenField ID="txthfRoomNo" runat="server" />
                        </td>
                        <td align="center" class="P0"><asp:Literal ID="ltSUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltEUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltFistIndex" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltEndIndex" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltNormalUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltHeightUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltLowUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltOtherUsing" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltDisCount" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="10" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div><span id="spanPageNavi" runat="server" style="width: 100%"></span></div>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click" />
            <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click" />
            <asp:HiddenField ID="hfCurrentPage" runat="server" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="txthfRoomNo" runat="server" />
            <asp:HiddenField ID="txthfChargeSeq" runat="server" />
            <asp:HiddenField ID="hfVatRation" runat="server" />
            <asp:TextBox ID="txtHfYYYYMM" runat="server" Visible="false"></asp:TextBox>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL" style="display: none">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span><asp:LinkButton ID="lnkbtnModify" runat="server" OnClick="lnkbtnModify_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span><asp:LinkButton ID="lnkCreateUtil" runat="server" 
                                    OnClick="lnkCreateUtil_Click" Text="Create New"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClientClick="javascript:return fnMakeExcelFile();"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ImageButton ID="imgbtnMakeExcel" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnMakeExcel_Click" />
</asp:Content>