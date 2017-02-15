<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngReadMonthForOverTime.aspx.cs" Inherits="KN.Web.Management.Remote.MngReadMonthForOverTime" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    <!--//
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
        }
    }     
    function fnMovePage(intPageNo) 
    {
        if (intPageNo == null) 
        {
            intPageNo = 1;
        }
        
        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
    }

    function fnCheckValidate(strAlert)
    {        
        var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
        var strYear = document.getElementById("<%=txtSearchDt.ClientID%>");
            
        if (trim(strSearchRoom.value) == "" && trim(strYear.value) == "")
        {       
            alert(strAlert);
            return false;
        }
        
        return true;
    }

    function fnDetailView(strRoomNo, strChargeSeq)
    {
        $('#<%=txthfRoomNo.ClientID %>').val(strRoomNo);
        $('#<%=txthfChargeSeq.ClientID %>').val(strChargeSeq);
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;        
        return false;
    }
    
    
    function fnModifyData(strAlertText)
    {
        var strYear = document.getElementById("<%=txtSearchDt.ClientID%>");
        
        
        if (trim(strYear.value) == "")
        {
            strYear.focus();
            alert(strAlertText);
            
            return false;
        }       
        
        return true;
    }
    $(document).ready(function () {
        callCalendar();
    }); 
        
    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").monthpicker();
    }     
    //-->
    </script>
    <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="Literal2" runat="server" Text="Company Name"></asp:Literal></li>
            <li><asp:TextBox ID="txtCompNm" runat="server" Width="180px" MaxLength="1000" CssClass="sh-input"></asp:TextBox></li>              
            <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>  
            <li><asp:Literal ID="Literal1" runat="server" Text="Date Time"></asp:Literal></li>          
            <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            <li>
                <asp:DropDownList ID="ddlPrintYN" runat="server">
                    <asp:ListItem Value="N">Not Print</asp:ListItem>
                    <asp:ListItem Value="Y">Printed</asp:ListItem>
                </asp:DropDownList>
            </li> 
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <table class="TbCel-Type6-A MrgT10 ">
            <col width="5%" />
            <col width="8%" />
            <col width="40%" />
            <col width="8%" />
            <col width="10%" />
            <col width="10%" />
            <col width="10%" />
            <col width="10%" />
            <col width="10%" />
        <tr>
             <th>No.</th>
             <th>Room</th>            
            <th>Tenant Name</th>
            <th>Period</th>
            <th>H-Over</th>
            <th>Square</th>
            <th>U-Price</th>
            <th>Ex-Rate</th>
            <th>Discount</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPrintYN" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click" />
        </Triggers>
        <ContentTemplate>
           <div style="height: 425px;overflow-y: scroll;overflow-x: hidden;width: 840px">
            <asp:ListView ID="lvDayChargeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemDataBound="lvDayChargeList_ItemDataBound" OnItemCreated="lvDayChargeList_ItemCreated">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="5%" />
                        <col width="8%" />                        
                        <col width="37%" />
                        <col width="8%" />
                        <col width="8%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="5%" />
                        <col width="10%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("RoomNo")%>","<%#Eval("OUSeq")%>");'>
                         <td align="center" class="P0"><asp:Literal ID="ltNo" runat="server"></asp:Literal></td>
                         <td align="center" class="P0"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></td>                       
                        <td align="center" class="P0">
                            <asp:Literal ID="ltUserNm" runat="server"></asp:Literal>
                            <asp:HiddenField ID="txthfChargeSeq" runat="server" />
                            <asp:HiddenField ID="txthfRoomNo" runat="server" />
                        </td>
                        <td align="center" class="P0"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltHoursOver" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltSquare" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltUnitPrice" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltExchangRate" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltDiscount" runat="server"></asp:Literal></td>
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
            </div>
            <div class="Btwps FloatR">
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
                                <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="imgbtnMakeExcel_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>