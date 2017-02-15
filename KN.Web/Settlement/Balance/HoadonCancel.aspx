<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonCancel.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonCancel" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
                      
        }
    }       
    <!--//
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            return false;
        }
    }

    function fnSelectCheckValidate(strText)
    {

        return true;
    }

    function fnRentChange()
    {


        return false;
    }

    function fnCardNoChange()
    {

        return false;
    }

    function fnCalendarChange()
    {
        return false;
    }

    function fnOpenCalendar(obj)
    {

        return false;
    }

    

    function SaveSuccess() {
        alert('Save Successful !');         
    }   
   

    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }
    
    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

    function formatStringToDate(strdate) {
        var year        = strdate.substring(0,4);
        var month       = strdate.substring(4,6);
        var day         = strdate.substring(6,8);
        var newdate = year + "-" + month + "-" + day;
        return newdate
    }



     function fnDetailView(InvoiceNo)
    {
       
        document.getElementById("<%=hfInvoiceNo.ClientID%>").value = InvoiceNo;

        
            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
            return false;
    }

    

      function fnOccupantList(strSeq) {           

        }

     $(document).ready(function () {
            
        });

         

//-->
   
    </script>

<fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10 ">
            <li><asp:Literal ID="ltInvoiceNo" runat="server" Text="Invoice No"></asp:Literal></li>
            <li><asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20" Width="100"></asp:TextBox></li>            
            
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />          
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
            
        </Triggers>
        <ContentTemplate>
            <p style="text-align:right"><b><asp:Literal ID="ltMaxNo" runat="server"></asp:Literal></b>&nbsp;:&nbsp;<asp:Literal ID="ltInsMaxNo" runat="server"></asp:Literal></p>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>                    
                    <col width="70px" />
                    <col width="60px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="200px" />
                    <col width="120px" />                   
                    <tr>                        
                        <th>
                           Date
                        </th>
                        <th>
                            Serial No
                        </th>
                        <th>
                            Invoice No
                        </th>
                        <th>
                            Room No
                        </th>
                        <th>
                            Description
                        </th>
                        <th>
                            Grand amount
                        </th>
                        
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 100px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">                                                    
                            <col width="70px" />
                            <col width="60px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="200px" />
                            <col width="120px" />                            
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                         <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:return fnDetailView(this,'<%#Eval("InvoiceNo")%>')">
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="lnsDate" runat="server"></asp:Literal> 
                                                              
                                <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRefPrintNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                
                                <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>

                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="55" MaxLength="7" AutoPostBack="true" Visible = "false"></asp:TextBox>
                                <asp:TextBox ID="txtOldInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsTaxCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsNm" runat="server" Visible="false"></asp:TextBox>
                            
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="lnsSerialNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="lnsInvoiceNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="InsRoomNo" runat="server"></asp:Literal></td>
                           
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="240"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsTotal" runat="server" Width="110" CssClass="TbTxtRight"></asp:TextBox></td>                           
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">                                                   
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>           
          <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type2" style="margin-bottom: 10px;">
                 
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRevInvoiceNo" runat="server" Text="Revoke Invoice No"></asp:Literal>
                    </th>
                    <td colspan = "5">
                       <asp:TextBox ID="txtRevInvoiceNo" runat="server" CssClass="bgType2" MaxLength="18" 
                            Width="100" ReadOnly="True"></asp:TextBox>
                    </td>
                    
                </tr>                

                
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltAmount" runat="server" Text="Net amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtIRevNetAmt" runat="server" CssClass="bgType2" MaxLength="18" 
                            Width="100" ReadOnly="True"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltExchangeRate" runat="server" Text="VAT amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtIRevVatAmt" runat="server" CssClass="bgType2" MaxLength="18" 
                            Width="100" ReadOnly="True"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRealAmt" runat="server" Text="Grand amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtIRevGrandAmt" runat="server" CssClass="bgType2" MaxLength="18"
                            Width="100" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <th align="center">
                        <asp:Literal ID="ltNewInvoice" runat="server" Text="Replace Invoice No"></asp:Literal>
                    </th>
                    <td colspan = "5">
                        <asp:Literal ID="ltInputInvoice" runat="server" ></asp:Literal>
                    </td>
                    
                </tr>  
                <tr>
                    <th align="center">
                        <asp:Literal ID="Literal2" runat="server" Text="New Net amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtInputNetAmt" runat="server" CssClass="bgType2" 
                            MaxLength="18" Width="100"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="Literal3" runat="server" Text="New VAT amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtInputVatAmt" runat="server" CssClass="bgType2" MaxLength="18" 
                            Width="100"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="Literal4" runat="server" Text="New Grand Amount"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtInputGrandAmt" runat="server" CssClass="bgType2" MaxLength="18"
                            Width="100"></asp:TextBox>
                    </td>
                </tr>     
            </table>
           
            <div class="Btwps FloatR2">

            
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnRegist" runat="server"  Text="Replace" 
                                    onclick="lnkbtnRegist_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>                

            </div>
            <asp:HiddenField ID="hfUserSeq" runat="server" />
            <asp:HiddenField ID="hfSeq" runat="server" Value="" />
            <asp:HiddenField ID="hfFeeTy" runat="server" Value="" />
            <asp:HiddenField ID="hfRef_Seq" runat="server" Value="" />
            <asp:HiddenField ID="hfRoomNo" runat="server" Value="" />
            <asp:HiddenField ID="hfSeqDt" runat="server" Value="0" />
            <asp:HiddenField ID="hfBillCdDt" runat="server" Value="" />
            <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfChargeSeq" runat="server" Visible="false" Text="0"></asp:TextBox>
            </b>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfPayDt" runat="server" />
    <asp:HiddenField ID="hfInvoiceNo" runat="server" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
     <asp:HiddenField ID="hfsendParam" runat="server"/>
     <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
</asp:Content>