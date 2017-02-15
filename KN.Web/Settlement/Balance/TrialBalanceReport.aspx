<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="TrialBalanceReport.aspx.cs" Inherits="KN.Web.Settlement.Balance.TrialBalanceReport" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        function fnSelectCheckValidate(strText) {

            return true;
        }

        function fnCheckRoomNo(strAlertText, strTanVal, strLongVal, strShortVal) {
            return true;
        }



        function fnAccountList(rentCd, feeTy, roomNo, tenantNm) {


            window.open("/Common/RdPopup/RDPopupTrialBalanceReport.aspx?Datum0=" + rentCd + "&Datum1=" + feeTy + "&Datum2=" + roomNo + "&Datum3=" + tenantNm, "TrialBalanceReport", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
            return false;
        }
      

        function fnIssuingCheck(strTxt)
        {
            if (confirm(strTxt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        function fnChangePopup(strCompNmId, strRoomNoId, strUserSeqId, strCompNmS, strRentCd) {
            strCompNmS = $('#<%=txtCompanyNm.ClientID %>').val();
            strRentCd = document.getElementById("<%=ddlRentCd.ClientID%>").value;
            window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId + "&UserSeqId=" + strUserSeqId + "&CompNmS=" + strCompNmS + "&RentCdS=" + strRentCd, 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
            return false;
        }

        function callBack(compNm, rentCd, roomNo, userSeq) {
            if (rentCd == '0007' || rentCd == '0008') {
                rentCd = '9900';
            }
            if (rentCd == '0003') {
                rentCd = '0002';
            }
            document.getElementById("<%=ddlRentCd.ClientID%>").value = rentCd;
        }

        function loadInit() {
            $('#<%=txtCompanyNm.ClientID %>').keydown(function (e) {
                var code = e.keyCode || e.which;
                if (code == '9') {
                    $('#<%=imgbtnSearchCompNm.ClientID %>').click();

                    return false;
                }
                return true;
            });

        }

        $(document).ready(function () {
            loadInit();
        }); 
       

    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
            <li><b><asp:Literal ID="ltInsRentTy"  Text = "Rent Type :" runat="server"></asp:Literal></b></li>
            <li><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></li>
            <li><b><asp:Literal ID="ltInsFeeTy"  Text = "Fee Type :" runat="server"></asp:Literal></b></li>
            <li><asp:DropDownList ID="ddlItemCd" runat="server"></asp:DropDownList></li>
            
         
        </ul>
    <ul class="sf5-ag MrgL30 bgimgN">
            <li><b><asp:Literal ID="ltSearchRoom" runat="server" Text="Room :" ></asp:Literal></b></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Name :"></asp:Literal></b></li>
            <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="250" Width="220px" runat="server"></asp:TextBox></li>
            <asp:ImageButton ID="lnkLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="lnkLoadData_Click"/> 
        <li><asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/></li>   
            <li>
                <div class="Btn-Type4-wp">
                     <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click">Print</asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>                  

	             
            </ul>
    
</fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport" />  
             <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />         
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="40px" />
                    <col width="60px" />
                    <col width="70px" />
                    <col width="80px" />
                    <col width="280px" />
                    <col width="120px" />
                    <col width="130px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack = "true"
                                oncheckedchanged="chkAll_CheckedChanged1" Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Tenant Name"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                        </th>
                        <th class="Ls-line">
                            <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 120px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" 
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onitemcommand="lvPrintoutList_ItemCommand">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="40px" />                           
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
                            <col width="240px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter" runat="server" id="tdChk">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                                <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRefPrintNo" runat="server" Visible="false"></asp:TextBox>

                                <asp:TextBox ID="txtHfPrintSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                
                                <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>

                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="55" MaxLength="7" AutoPostBack="true" OnTextChanged="txtInvoiceNo_TextChanged" Visible = "false"></asp:TextBox>
                                <asp:TextBox ID="txtOldInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsTaxCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsNm" runat="server" Visible="false"></asp:TextBox>

                            </td>
                          
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAmtViNo" runat="server" Width="110" CssClass="TbTxtRight"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtInsRegDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
                                <asp:Literal ID="ltCalendarImg" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfInsRegDt" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="30px" />                            
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
                            <col width="280px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr>
                                <td colspan="8" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
             <div class="Btwps FloatR" >
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click">Excel Report</asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfUserSeq" runat="server" />
    <asp:HiddenField ID="hfPayDt" runat="server" />
     <asp:HiddenField ID="hfPayDtE" runat="server" />
    <asp:HiddenField ID="hfBillCd" runat="server"/>
    <asp:HiddenField ID="hfBillCdDt" runat="server"/>
    <asp:HiddenField ID="hfPeriod" runat="server"/>
    <asp:HiddenField ID="hfPeriodE" runat="server"/>
    <asp:HiddenField ID="hfRoomNo" runat="server"/>
    <asp:HiddenField ID="hfTenantNm" runat="server"/>
    <asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>
</asp:Content>