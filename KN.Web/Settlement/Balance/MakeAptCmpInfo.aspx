<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="MakeAptCmpInfo.aspx.cs" Inherits="KN.Web.Settlement.Balance.MakeAptCmpInfo" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender();                         
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

     function fnReConfirm(strText) {
            if (confirm(strText)) {
                document.getElementById("<%=imgbtnDelMonthInfo.ClientID%>").click();
            }
            else {
                document.getElementById("<%=imgbtnCancel.ClientID%>").click();
            }
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

     function LoadCalender() {
            datePicker();
        }

    function fnRoomChange() {
            document.getElementById("<%=imgbtnRoomChange.ClientID%>").click();
        }

   

    function datePicker() {
                      
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



     function fnDetailView(trow,RoomNo,ParkingCarNo,CarCd,CmpNm,CmpTaxCd,UserAddr, UserDetAddr, CarTyNm, ParkingCardNo, ParkingTagNo)
    {
        document.getElementById("<%=txtRegRoomNo.ClientID%>").setAttribute('disabled', true);

        document.getElementById("<%=txtRegRoomNo.ClientID%>").value = RoomNo;
        document.getElementById("<%=ddlCarNo.ClientID%>").value = ParkingCarNo;
        document.getElementById("<%=txtTaxNo.ClientID%>").value = CmpTaxCd;        
        document.getElementById("<%=txtCmpName.ClientID%>").value = CmpNm;
        document.getElementById("<%=txtAddress.ClientID%>").value = UserAddr;
        document.getElementById("<%=txtDetAddress.ClientID%>").value = UserDetAddr;
        document.getElementById("<%=txtCarTy.ClientID%>").value = CarTyNm;  
        document.getElementById("<%=txtParkingCardNo.ClientID%>").value = ParkingCardNo;  
        document.getElementById("<%=txtTagNo.ClientID%>").value = ParkingTagNo;    

        
        resetTable();
        $(trow).addClass('rowSelected'); 
          
         <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;

    }

     function resetTable() {
        $("div#<%=upSettlement.ClientID %> tbody").find("tr").each(function() {
            //get all rows in table
            $(this).removeClass('rowSelected');
        });  
    }

      function fnOccupantList(strSeq) {    
           $.initWindowMsg();
   
            window.open("/Common/RdPopup/RDPopupSpecialDebitList.aspx?Datum0=" + strSeq + "&Datum1=" + strReqDt, "PrintSpecialDebit", "status=no, resizable=yes, width=900, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            return false;
        }

     $(document).ready(function () {
             LoadCalender();             
            
        });

         $(function () {
            $.windowMsg("childMsg1", function (message) {
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = message;                
                     
                <%=Page.GetPostBackEventReference(imgbtnPrint)%>;
                return false;
            });
        });  

//-->
   
    </script>
    <style type="text/css">
        .rowSelected
        {
            background-color: #E4EEF5;
        }
        .rowHover
        {
            background-color: #E4EEF5;
        }
    </style>
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li><b>
                <asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="350px"
                    runat="server"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltItem" runat="server" Text="Car Type"></asp:Literal>
            </li>
            <li>                        
                        <asp:DropDownList ID="ddlCarTy" runat="server">
                        </asp:DropDownList>
            </li>
           
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li><b>
                <asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox></li>
           <li>
                <asp:Literal ID="ltSearchCarNo" runat="server" Text ="Car No."></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCarNo" CssClass="grBg bgType2" runat="server" Width="80px" MaxLength="20" 
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltSearchCardNo" runat="server" Text = "Parking Card No."></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCardNo" CssClass="grBg bgType2" runat="server" Width="80px" MaxLength="20" 
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
            
            <div class="Btn-Type4-wp">
                <div class="Btn-Tp4-L">
                    <div class="Btn-Tp4-R">
                        <div class="Btn-Tp4-M">
                            <span>
                                <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click">Search</asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />                       
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />             
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />   
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" />             
            <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="lnkbtnBack" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRoomChange" EventName="Click" />   
            <asp:AsyncPostBackTrigger ControlID="txtRegRoomNo" EventName="TextChanged" /> 
            <asp:AsyncPostBackTrigger ControlID="ddlCarNo" EventName="SelectedIndexChanged" /> 
            <asp:AsyncPostBackTrigger ControlID="imgbtnDelMonthInfo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCancel" EventName="Click" />                       
                               
        </Triggers>
        <ContentTemplate>
            <div style="width: 840px;">
                <table class="TbCel-Type6-A" cellpadding="0">
                    <colgroup>
                        <col width="40px" />
                        <col width="100px" />
                        <col width="160px" />
                        <col width="160px" />
                        <col width="280px" />
                        <col width="140px" />
                        
                        
                        <tr>
                            <th class="Fr-line">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack = "true"
                                oncheckedchanged="chkAll_CheckedChanged1" Style="text-align: center" />                                
                            </th>
                            <th>
                                Room No
                            </th>
                            <th>
                                Car No
                            </th>
                            <th>
                                CarType
                            </th>
                            <th>
                                Company Name
                            </th>
                            <th>
                                Tax Code
                            </th>
                        </tr>
                    </colgroup>
                </table>
                <div style="overflow-y: scroll; height: 200px; width: 840px;">
                    <asp:ListView ID="lvPrintoutList" runat="server" 
                        ItemPlaceholderID="iphItemPlaceHolderID" 
                        onitemcreated="lvPrintoutList_ItemCreated" 
                        onitemdatabound="lvPrintoutList_ItemDataBound" 
                        onlayoutcreated="lvPrintoutList_LayoutCreated" 
                        onselectedindexchanged="lvPrintoutList_SelectedIndexChanged">
                        <LayoutTemplate>
                            <table cellpadding="0" class="TypeA-shorter">
                                <col width="40px" />
                                <col width="100px" />                                
                                <col width="160px" />
                                <col width="160px" />
                                <col width="320px" />  
                                <col width="100px" />                                                   
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="cursor:pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)" onclick="javascript:return fnDetailView(this,'<%#Eval("RoomNo")%>','<%#Eval("ParkingCarNo")%>','<%#Eval("CarCd")%>','<%#Eval("CmpNm")%>','<%#Eval("CmpTaxCd")%>','<%#Eval("UserAddr")%>','<%#Eval("UserDetAddr")%>','<%#Eval("CarTyNm")%>','<%#Eval("ParkingCardNo")%>','<%#Eval("ParkingTagNo")%>')">
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:CheckBox ID="chkboxList" runat="server" >
                                    </asp:CheckBox>
                                </td>                                
                                <td class="TbTxtCenter">
                                    <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                                </td>
                                <td class="TbTxtCenter">
                                    <asp:Literal ID="ltCarNo" runat="server"></asp:Literal>
                                </td>
                                <td class="TbTxtCenter">
                                    <asp:Literal ID="ltCarType" runat="server"></asp:Literal>
                                </td>
                                <td class="TbTxtCenter">
                                    <asp:Literal ID="ltCmpNm" runat="server"></asp:Literal>
                                </td>
                                <td class="TbTxtCenter">
                                    <asp:Literal ID="ltTaxCode" runat="server"></asp:Literal>
                                </td>
                               
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table cellpadding="0" class="TypeA-shorter" width="840">
                                <col width="40px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="350px" /> 
                                <col width="100px" />                                
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>                           
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />   
             <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" /> 
             <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" /> 
             <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" /> 
             <asp:AsyncPostBackTrigger ControlID="lnkbtnBack" EventName="Click" />             
             <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="imgbtnRoomChange" EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="txtRegRoomNo" EventName="TextChanged" />
              <asp:AsyncPostBackTrigger ControlID="ddlCarNo" EventName="SelectedIndexChanged" />
             <asp:AsyncPostBackTrigger ControlID="imgbtnDelMonthInfo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCancel" EventName="Click" />            
                         
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1B iw840" style="margin-bottom: 20px;">                
                
                <tr>
                    <th>
                        <asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal>
                    </th>
                    <td>
                         <asp:TextBox ID="txtRegRoomNo" runat="server" CssClass="bgType2" MaxLength="20" Width="100" 
                             ontextchanged="txtRegRoomNo_TextChanged" AutoPostBack = "true"></asp:TextBox>
                    </td>
                     <th>
                        <asp:Literal ID="ltRegParkingCarNo" runat="server" Text = "Parking Car No"></asp:Literal>
                    </th>
                    <td >
                        <asp:DropDownList ID="ddlCarNo" runat="server" AutoPostBack="true" Width="100" 
                            onselectedindexchanged="ddlCarNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td> 

                    <th>
                        <asp:Literal ID="Literal4" runat="server" Text ="Tax Code"></asp:Literal>
                    </th>
                    <td >
                        <asp:TextBox ID="txtTaxNo" runat="server"></asp:TextBox>                       
                    </td>                    
                   
                </tr>  
                 <tr>
                    <th >
                        <asp:Literal ID="ltRegCarTy" runat="server" Text ="Car Type"></asp:Literal>
                    </th>
                    <td>                        
                        <asp:TextBox ID="txtCarTy" runat="server" Enabled="False"></asp:TextBox>  
                        <asp:TextBox ID="txtHfCarTy" runat="server" Visible="false"></asp:TextBox>                      
                    </td>
                    <th>
                        <asp:Literal ID="ltRegParkingCardNo" runat="server" Text = "Card No"></asp:Literal>
                    </th>
                    <td >                        
                         <asp:TextBox ID="txtParkingCardNo" runat="server" Enabled="False"></asp:TextBox>                      
                    </td>                    

                     <th >
                        <asp:Literal ID="Literal3" runat="server" Text ="Tag No"></asp:Literal>
                    </th>
                    <td>                        
                         <asp:TextBox ID="txtTagNo" runat="server" Enabled="False"></asp:TextBox>                     
                    </td>     
                </tr>
                <tr>
                    <th >
                        <asp:Literal ID="ltCmpName" runat="server" Text="Company Name"></asp:Literal>
                    </th>
                    <td colspan = "7" >
                       <asp:TextBox ID="txtCmpName" CssClass="bgType2" runat="server" Width="500px"></asp:TextBox>
                    </td>
                   
                </tr> 
                 <tr>
                    <th rowspan ="2">
                        <asp:Literal ID="ltDesVi" runat="server" Text="Address"></asp:Literal>
                    </th>
                    <td colspan = "7" >
                        <asp:TextBox ID="txtAddress" CssClass="bgType2" runat="server" Width="550px"></asp:TextBox>
                    </td>                      
                    
                  
                </tr>
                  <tr>                    
                    <td colspan = "7">
                        <asp:TextBox ID="txtDetAddress" CssClass="bgType2" runat="server" Width="550px"></asp:TextBox>
                    </td>  
                </tr>  
            </table>
           
            <div class="Btwps FloatR2">

                <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
                                <div class="Btn-Tp3-M">
                                    <span>
                                        <asp:LinkButton ID="lnkbtnBack" runat="server" Text="New" 
                                        OnClick="lnkbtnBack_Click"></asp:LinkButton></span>
                                </div>
                            </div>
                        </div>
                    </div>
                         
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Save"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>                

                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" Text="Update" 
                                    onclick="lnkbtnUpdate_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Delete" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
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
            <asp:HiddenField ID="hfRentCd" runat="server" Value="" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfChargeSeq" runat="server" Visible="false" Text="0"></asp:TextBox>
            </b>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfReqDt" runat="server" Value=""/>    
    <asp:HiddenField ID="hfRefSeq" runat="server" Value=""/>
    <asp:HiddenField ID="hfPrintBundleNo" runat="server" Value=""/>
    <asp:HiddenField ID="hfsendParam" runat="server"/>
    <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailView_Click" />
    <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
    OnClick="imgbtnPrint_Click" />
    <asp:ImageButton ID="imgbtnDetailPayment" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailPayment_Click" />
    <asp:ImageButton ID="imgbtnRoomChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
            OnClick="imgbtnRoomChange_Click" />
    <asp:ImageButton ID="imgbtnDelMonthInfo" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDelMonthInfo_Click" />
     <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                    OnClick="imgbtnCancel_Click" />
</asp:Content>
