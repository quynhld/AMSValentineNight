<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CompView.aspx.cs" Inherits="KN.Web.Stock.Company.CompView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <table class="TbCel-Type2-E">
        <col width="100px"/>
        <col width="300px"/>
        <col width="100px"/>
        <col width="300px"/>
	    <tbody>
		    <tr>
			    <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
			    <td colspan="3"><asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltRepresentiveNm" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsRepresentiveNm" runat="server"></asp:Literal></td>
			    <th><asp:Literal ID="ltChargerNm" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsChargerNm" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltIndus" runat="server"></asp:Literal></th>
			    <td colspan="3">
		            <asp:Literal ID="itInsIndus" runat="server"></asp:Literal>			    
			    </td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltTel" runat="server"></asp:Literal></th>
			    <td>
				    <asp:Literal ID="ltInsCompTelFrontNo" runat="server"></asp:Literal>
				    <span>-</span>
				    <asp:Literal ID="ltInsCompTelMidNo" runat="server"></asp:Literal>
				    <span>-</span>
				    <asp:Literal ID="ltInsCompTelRearNo" runat="server"></asp:Literal>
			    </td>
			    <th><asp:Literal ID="ltFax" runat="server"></asp:Literal></th>
			    <td>
				    <asp:Literal ID="ltInsCompFaxFrontNo" runat="server"></asp:Literal>
				    <span>-</span>
				    <asp:Literal ID="ltInsCompFaxMidNo" runat="server"></asp:Literal>
				    <span>-</span>
				    <asp:Literal ID="ltInsCompFaxRearNo" runat="server"></asp:Literal>
			    </td>
		    </tr>
		    <tr>
			    <th rowspan="2"><asp:Literal ID="ltAddr" runat="server"></asp:Literal></th>
			    <td colspan="3">
			        <asp:Literal ID="ltInsAddr" runat="server"></asp:Literal>
			    </td>
		    </tr>
		    <tr>
		        <td colspan="3">
		            <asp:Literal ID="ltInsDetAddr" runat="server"></asp:Literal>
		        </td>
		    </tr>
    	    <tr>
			    <th><asp:Literal ID="ltCompTy" runat="server"></asp:Literal></th>
			    <td colspan="3"><asp:Literal ID="ltInsCompTy" runat="server"></asp:Literal></td>
		    </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon1" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:Literal ID="ltInsfileAddon1" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfFilePath1" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon2" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:Literal ID="ltInsfileAddon2" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfFilePath2" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon3" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:Literal ID="ltInsfileAddon3" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfFilePath3" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
			    <th><asp:Literal ID="ltIntro" runat="server"></asp:Literal></th>
			    <td colspan="3">
				    <asp:Literal ID="ltInsIntro" runat="server"></asp:Literal>
				</td>
			</tr>
        </tbody>
    </table>
    <div class="Btwps FloatR2"">
	    <div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
        <div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
	</div>
    <asp:TextBox ID="txtHfCompNo" runat="server" Visible="false"></asp:TextBox>
</asp:Content>