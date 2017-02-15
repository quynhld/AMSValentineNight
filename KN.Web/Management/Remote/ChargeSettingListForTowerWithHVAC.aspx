<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ChargeSettingListForTowerWithHVAC.aspx.cs"
    Inherits="KN.Web.Management.Remote.ChargeSettingListForTowerWithHVAC" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
        }
    }        
        <!--//
        function fnFirstCheckValidate(strText)
        {
            var strGenCharge = document.getElementById("<%=txtFirstGenCharge.ClientID%>");
            var strStartDt = document.getElementById("<%=txtFirstStartDt.ClientID%>");

            if (trim(strGenCharge.value) == "")
            {
                alert(strText);
                strGenCharge.focus();
                return false;
            }

            if (trim(strStartDt.value) == "")
            {
                alert(strText);
                strStartDt.focus();
                return false;
            }

            return true;
        }

         function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd)
        {
            var strCompNmS = $('#<%=txtTitle.ClientID %>').val();
            window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
        
            return false;
        }

        function fnSecondCheckValidate(strText)
        {
           
            var strGenCharge = document.getElementById("<%=txtSecondGenCharge.ClientID%>");
            var strStartDt = document.getElementById("<%=txtSecondStartDt.ClientID%>");
            var strRoomNo = document.getElementById("<%=txtRoomNo.ClientID%>");
            var strUserSeq = document.getElementById("<%=HfReturnUserSeqId.ClientID%>");
             var strNm = document.getElementById("<%=txtTitle.ClientID%>");


            if (trim(strUserSeq.value) == "")
            {
                alert(strText);
                strNm.focus();
                return false;
            }
            
            if (trim(strRoomNo.value) == "")
            {
                alert(strText);
                strNm.focus();
                return false;
            }
            
            if (trim(strGenCharge.value) == "")
            {
                strGenCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strStartDt.value) == "")
            {
                alert(strText);
                strStartDt.focus();
                return false;
            }

            return true;
        }

        $(document).ready(function () {
            callCalendar();
        });

        function callCalendar() {
            $(".cCalendar").datepicker();
            //Tab function
            $('#<%=txtTitle.ClientID %>').keydown(function(e) {          
               var code = e.keyCode || e.which;
               if (code == '9') {             
                   $('#<%=imgbtnSearchCompNm.ClientID %>').click();
               
               return false;
               }
                return true;
            });                    
        }

        function callBack(compNm,rentCd,roomNo,userSeq) {
        $('#<%=txthfUserSeq.ClientID %>').val(userSeq);
        return false;
    }    
    //-->
    </script>
    <div class="FloatR2">
        (<asp:Literal ID="ltFirstBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal
            ID="ltRealBaseRate" runat="server"></asp:Literal><asp:HiddenField ID="hfRealBaseRate"
                runat="server" />
        )
    </div>
    <asp:Literal ID="ltCommon" runat="server"></asp:Literal>
    <table class="TbCel-Type6-A">
        <col width="10%" />
        <col width="19%" />
        <col width="19%" />
        <col width="19%" />
        <col width="19%" />
        <col width="8%" />
        <tr>
            <th>
                <asp:Literal ID="ltFirstSeq" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltFirstGenCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltFirstPeakCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltFirstNightCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltFirstStartDt" runat="server"></asp:Literal>
            </th>
            <th>
                &nbsp;
            </th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upRemoteForTower" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnInput" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div style="overflow-y: scroll; overflow-x: hidden; height: 100px; width: 840px;">
                <asp:ListView ID="lvChargeInfoList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemDataBound="lvChargeInfoList_ItemDataBound" OnItemCreated="lvChargeInfoList_ItemCreated"
                    OnItemDeleting="lvChargeInfoList_ItemDeleting" OnItemUpdating="lvChargeInfoList_ItemUpdating">
                    <LayoutTemplate>
                        <table class="TbCel-Type4-A">
                            <col width="10%" />
                            <col width="19%" />
                            <col width="19%" />
                            <col width="19%" />
                            <col width="10%" />
                            <col width="8%" />
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" class="P0">
                                <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtGenCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtPeakCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtNightCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                                <asp:Literal ID="ltStartDt" runat="server" Visible="false"></asp:Literal>
                                <asp:HiddenField ID="hfStartDt" runat="server" />
                                <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtHfChargeSeq" runat="server" Visible="false"></asp:TextBox>
                                <span>
                                    <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                                <span>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA">
                            <tbody>
                                <tr>
                                    <td colspan="6" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <table class="TbCel-Type6-A">
                <colgroup>
                    <col width="10%" />
                    <col width="19%" />
                    <col width="19%" />
                    <col width="19%" />
                    <col width="10%" />
                    <col width="8%" />
                    <tbody>
                        <tr>
                            <td align="center" class="P0">
                                &nbsp;
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtFirstGenCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtFirstPeakCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtFirstNightCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstStartDt" runat="server" CssClass="bgType2 cCalendar" Width="70"></asp:TextBox>
                                <img alt="Calendar" onclick="CallCalendar('#<%=txtFirstStartDt.ClientID%>')" src="/Common/Images/Common/calendar.gif"
                                    style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfFirstStartDt" runat="server" />
                                <asp:TextBox ID="txtHfFirstOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <span>
                                    <asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"
                                        OnClick="imgbtnRegist_Click" /></span>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server" />
    <br />
    <asp:Literal ID="ltIndividual" runat="server"></asp:Literal>
    <table class="TbCel-Type6-A">
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="12%" />
        <col width="8%" />
        <tr>
            <th>
                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondSeq" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondGenCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondPeakCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondNightCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltOther" runat="server" Text="Other"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondACCharge" runat="server" Text="E-Over Time"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltSecondStartDt" runat="server"></asp:Literal>
            </th>
            <th>
                &nbsp;
            </th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upRoomList" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnInput" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div style="overflow-y: scroll; overflow-x: hidden; height: 300px; width: 840px;">
                <asp:ListView ID="lvChargelistForRoom" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemDataBound="lvChargelistForRoom_ItemDataBound" OnItemCreated="lvChargelistForRoom_ItemCreated"
                    OnItemDeleting="lvChargelistForRoom_ItemDeleting" OnItemUpdating="lvChargelistForRoom_ItemUpdating">
                    <LayoutTemplate>
                        <table class="TbCel-Type4-A">
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="8%" />
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr title="<%#Eval("TenantNm")%>">
                            <td align="center" class="P0">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtGenCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtPeakCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtNightCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtOther" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtACCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="90"></asp:TextBox>
                                <asp:Literal ID="ltStartDt" runat="server" Visible="false"></asp:Literal>
                                <asp:HiddenField ID="hfStartDt" runat="server" />
                                <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtHfChargeSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <span>
                                    <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                                <span>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA">
                            <tbody>
                                <tr>
                                    <td colspan="6" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <table class="TbCel-Type4-A">
                <colgroup>
                    <col width="15%" />
                    <col width="5%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="15%" />
                    <col width="5%" />
                    <tbody>
                        <tr>
                            <td lign="center" class="P0">
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="100" CssClass="bgType2 "
                                    Text=""></asp:TextBox>
                                <asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif"
                                    ImageAlign="AbsMiddle" Width="17px" Height="15px" />
                            </td>
                            <td lign="center" class="P0">
                                <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="20" Width="48" CssClass="bgType2 "
                                    Text=""></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtSecondGenCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtSecondPeakCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtSecondNightCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtOtherInput" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtSecondACCharge" runat="server" CssClass="bgType2" MaxLength="18"
                                    Width="50"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtSecondStartDt" runat="server" CssClass="bgType2 cCalendar" Width="70"></asp:TextBox>
                                <img alt="Calendar" onclick="CallCalendar('#<%=txtSecondStartDt.ClientID%>')" src="/Common/Images/Common/calendar.gif"
                                    style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfSecondStartDt" runat="server" />
                                <asp:TextBox ID="txtHfSecondOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <span>
                                    <asp:ImageButton ID="imgbtnInput" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"
                                        OnClick="imgbtnInput_Click" /></span>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txthfUSeq" runat="server" Visible="false" Text=""></asp:TextBox>
    <asp:HiddenField ID="HfReturnUserSeqId" runat="server" />
    <asp:TextBox ID="txthfUserSeq" runat="server" Visible="false" Text=""></asp:TextBox>
    <script language="javascript" type="text/javascript">
    <!--        //
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "") {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    //-->
    </script>
</asp:Content>
