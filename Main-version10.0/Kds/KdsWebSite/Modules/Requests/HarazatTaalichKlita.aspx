<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="HarazatTaalichKlita.aspx.cs" Inherits="Modules_Requests_HarazatTaalichKlita" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 34px;
            width: 583px;
        }
        .style2
        {
            width: 583px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
<fieldset class="FilterFieldSet" style="width:750px"> 
     <asp:Label runat="server" ID="lblPageHeader"   width="750px"   CssClass = "GridHeader">����� ����� ������</asp:Label>
    <asp:Label runat="server" ID="lblErrorPage" Font-Bold="true"    width="750px"   CssClass="ErrorMessage"></asp:Label> 
    <asp:UpdatePanel ID="upRdoId" runat="server" RenderMode="Inline" UpdateMode="Always">
    <ContentTemplate> 
     <table>
        <tr>
            <td class="style2" >
                <asp:Button ID="btnKlitatShaonim" runat="server"  Text="����� ������"   
                    CssClass="ImgButtonApprovalRegular"  Width="154px" 
                    onclick="btnKlitatShaonim_Click"    />
               
            </td>
            
        </tr>
        <tr>
            <td class="style2">
             <asp:Button ID="btnRianunMatzavOvdim" runat="server"  Text="����� ��� ������"  
                    CssClass="ImgButtonApprovalRegular" Width="154px" 
                    onclick="btnRianunMatzavOvdim_Click"    />
             
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnYeziratYomAvoda" runat="server"   Text="����� ��� �����" 
                    CssClass="ImgButtonApprovalRegular"  Width="154px" 
                    onclick="btnYeziratYomAvoda_Click"    />
                    �����:
                 <KdsCalendar:KdsCalendar runat="server" ID="clnYomAvoda"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>       &nbsp;&nbsp;     
                <asp:Label runat="server" ID="lblYomAvoda" CssClass="ErrorMessage"  Visible="false" Text="���� ������ �����!"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnRianunTavlaotTnua" runat="server"   
                    Text="����� ������ �����"     CssClass="ImgButtonApprovalRegular" 
                     Width="154px" onclick="btnRianunTavlaotTnua_Click"    />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnRianunTavlaotHR" runat="server"   
                    Text="����� ������ HR ��� ������� �����"     
                    CssClass="ImgButtonApprovalRegular"  Width="230px" 
                    onclick="btnRianunTavlaotHR_Click"    />
                    ���� ������:
                <asp:DropDownList ID="ddlTavlaot" runat="server" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btnRianunMeafyeneiBizua" runat="server"   
                    Text="����� ������ ������� ����� ��� HR"    CssClass="ImgButtonApprovalRegular" 
                     Width="230px" onclick="btnRianunMeafyeneiBizua_Click"    />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnHasvaatHR" runat="server"   Text="������ ����� HR"    
                    CssClass="ImgButtonApprovalRegular"  Width="154px" 
                    onclick="btnHasvaatHR_Click"  />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnHashlamatNetunimVeShguimHR" runat="server"   
                    Text="����� ������ ������� ���� ������� �- HR"   
                    CssClass="ImgButtonApprovalRegular"     Width="230px" 
                    onclick="btnHashlamatNetunimVeShguimHR_Click"     />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnKlitatNetuneySadran" runat="server"   
                    Text="����� ����� ���� (���� �����,������ ��������)"   
                    CssClass="ImgButtonApprovalRegular"   Width="268px" 
                    onclick="btnKlitatNetuneySadran_Click"    />
                    �����:
                  <KdsCalendar:KdsCalendar runat="server" ID="clnSadran"  TabIndex="4" AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>   &nbsp;&nbsp;     
                <asp:Label runat="server" ID="lblSadran" CssClass="ErrorMessage"  Visible="false" Text="���� ������ �����!"  ></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnHahlamatNetunimVeshguim" runat="server"   
                    Text="����� ������ �������"     CssClass="ImgButtonApprovalRegular"    
                    Width="154px" onclick="btnHahlamatNetunimVeshguim_Click"   />
                    �����:
                <KdsCalendar:KdsCalendar runat="server" ID="clnHaslama"  TabIndex="4" AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>    &nbsp;&nbsp;     
                <asp:Label runat="server" ID="lblHashlama" CssClass="ErrorMessage"  Visible="false" Text="���� ������ �����!"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnIshurim" runat="server"   Text="�������"    
                    CssClass="ImgButtonApprovalRegular"   Width="154px" 
                    onclick="btnIshurim_Click"    />
                    �����:
                    <KdsCalendar:KdsCalendar runat="server" ID="clnIshurim"  TabIndex="4" AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>  &nbsp;&nbsp;     
                    <asp:Label runat="server" ID="lblIshurim" CssClass="ErrorMessage" Visible="false" Text="���� ������ �����!"  ></asp:Label>
            </td>
        </tr>
     </table>
      
      </ContentTemplate>
      </asp:UpdatePanel>
 </fieldset>
</asp:Content>

