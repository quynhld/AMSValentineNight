<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ContractStatusReport.aspx.cs" Inherits="KN.Web.Settlement.Balance.ContractStatusReport" ValidateRequest="false"%>
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



        function fnAccountList(rentCd) {
            // Datum0 : 시작일
            // Datum1 : 종료일
            // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
            // Datum3 : 임대(RentCd : 아파트, 상가)
            // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)


            window.open("/Common/RdPopup/RDPopupEstateTenantInformationReport.aspx?Datum0=" + rentCd, "TenantInformationReport", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
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

        $(document).ready(function () {
           
        }); 
       

    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
         <li><asp:DropDownList ID="ddlInsRentCd" runat="server"></asp:DropDownList></li>   
                                          
    </ul>
    
</fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport" />           
        </Triggers>
        <ContentTemplate>            
            <div style="overflow-y: scroll; height: 120px; width: 840px;">
                
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
</asp:Content>