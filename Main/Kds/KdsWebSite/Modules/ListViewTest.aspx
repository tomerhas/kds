<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListViewTest.aspx.cs" Inherits="Modules_ListViewTest" %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucSidurim.ascx" TagName="ucSidurim"  %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucAccordionTest.ascx" TagName="ucAccordion"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../StyleSheet.css" type="text/css" rel="stylesheet" /> 
    <style type="text/css">
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_0 { background-color:white;visibility:hidden;display:none;position:absolute;left:0px;top:0px; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_1 { text-decoration:none; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_2 {  }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_3 { border-style:none; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_4 {  }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_5 { border-style:none; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_6 {  }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_7 {  }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_8 { border-style:none; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_9 {  }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_10 { border-style:none; }
	.ctl00_ContentPlaceHolder1_LeftArticleControl1_SectionMenu1_ctl00_11 {  }

</style>
</head>
<body>
    <form id="form1" runat="server" dir="rtl">
         <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true">        
         </asp:ScriptManager>
        <div>                
            <uc:ucSidurim runat="server" ID="SD" />             
        </div>
  
  
    <ajaxToolkit:MaskedEditExtender runat="server" ID="maskTextBox"
        TargetControlID="txtTest" 
        Mask="99:99"
        MessageValidatorTip="true" 
        OnFocusCssClass="MaskedEditFocus" 
        OnInvalidCssClass="MaskedEditError"
        MaskType="Time" 
        InputDirection="RightToLeft" 
        AcceptNegative="Left" 
        DisplayMoney="Left"
        ErrorTooltipEnabled="True"/>


    <asp:Panel ID="panelTitleIntroduction" runat="server" Height="25px" Width="405px">
    <img id="imgBackgroundIntroduction" src="App_Themes/CollapsiblePanel/images/BackgroundIntroduction.png" />
    </asp:Panel>

    <asp:Panel ID="panelContentIntroduction" runat="server" Height="160px" Width="415px">
    .....
    <span style="font: 18px 'Lucida Grande', LucidaGrande, Verdana, sans-serif; color: #2d4c78;">The CollapsiblePanel is one of AJAX control in ASP.NET AJAX Control Toolkit. This tutorial combines CollapisblePanel and ASP.NET AJAX RC 1.0 to make amazing web application. It shows you how powerful in AJAX technology and what's advantage to use it. Hope you will like it</span>
    .....
    </asp:Panel>


   
    <asp:Panel ID="panelTitleEmployeeList" runat="server" Height="25px" Width="405px">
    <img id="imgBackgroundEmployeeList" src="App_Themes/CollapsiblePanel/images/BackgroundEmployeeList.png" />
    </asp:Panel>
    <asp:Panel ID="panelContentEmployeeList" runat="server" Height="310px" Width="415px">
    .....                    
  <%-- <asp:DataGrid ID="dgNew" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" oneditcommand="dgNew_EditCommand" Width=100% >
    .....
    </asp:DataGrid>--%>
    </asp:Panel>
    
    
    
<asp:Panel ID="panelTitleEmployeeDetail" runat="server" Height="25px" Width="405px">

<img id="imgBackgroundEmployeeDetail" src="App_Themes/CollapsiblePanel/images/BackgroundEmployeeDetail.png" />

</asp:Panel>

<asp:Panel ID="panelContentEmployeeDetail" runat="server" Height="300px" Width="415px">

.....

<table id="tblInformation" align="center" border="1" bordercolor="graytext" cellpadding="2" cellspacing="0" height="160" style="width: 300px; height: 170px" width="710" runat=server>

.....

</table>

</asp:Panel>



<ajaxToolkit:CollapsiblePanelExtender ID="cpeIntroduction" runat="server" TargetControlID="panelContentIntroduction" ExpandControlID="panelTitleIntroduction" CollapseControlID="panelTitleIntroduction" Collapsed="false"

ImageControlID="imgBackgroundIntroduction" ExpandedImage="App_Themes/CollapsiblePanel/images/HightLightIntroduction.png"

CollapsedImage="App_Themes/CollapsiblePanel/images/BackgroundIntroduction.png" SuppressPostBack="True" ScrollContents=true ></ajaxToolkit:CollapsiblePanelExtender>

<ajaxToolkit:CollapsiblePanelExtender ID="cpeEmployeeList" runat="server" TargetControlID="panelContentEmployeeList"

ExpandControlID="panelTitleEmployeeList" CollapseControlID="panelTitleEmployeeList" Collapsed="true"

ImageControlID="imgBackgroundEmployeeList" ExpandedImage="App_Themes/CollapsiblePanel/images/HightLightEmployeeList.png"

CollapsedImage="App_Themes/CollapsiblePanel/images/BackgroundEmployeeList.png" SuppressPostBack="True" ></ajaxToolkit:CollapsiblePanelExtender>

<ajaxToolkit:CollapsiblePanelExtender ID="cpeEmployeeDetail" runat="server" TargetControlID="panelContentEmployeeDetail"

ExpandControlID="panelTitleEmployeeDetail" CollapseControlID="panelTitleEmployeeDetail" Collapsed="true"

ImageControlID="imgBackgroundEmployeeDetail" ExpandedImage="App_Themes/CollapsiblePanel/images/HightLightEmployeeDetail.png"

CollapsedImage="App_Themes/CollapsiblePanel/images/BackgroundEmployeeDetail.png" SuppressPostBack="True"></ajaxToolkit:CollapsiblePanelExtender>

 
 <SPAN style="FONT-SIZE: 10pt; COLOR: blue; FONT-FAMILY: Verdana; mso-fareast-font-family: MingLiU; mso-bidi-font-family: 'Courier New'; mso-no-proof: yes">="panelTitleIntroduction"</SPAN><SPAN style="FONT-SIZE: 10pt; COLOR: #2b443f; FONT-FAMILY: Verdana; mso-fareast-font-family: MingLiU; mso-bidi-font-family: 'Courier New'; mso-no-proof: yes"> </SPAN><SPAN style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Verdana; mso-fareast-font-family: MingLiU; mso-bidi-font-family: 'Courier New'; mso-no-proof: yes">runat</SPAN><SPAN style="FONT-SIZE: 10pt; COLOR: blue; FONT-FAMILY: Verdana; mso-fareast-font-family: MingLiU; mso-bidi-font-family: 'Courier New'; mso-no-proof: yes">="server"</SPAN><SPAN style="FONT-SIZE: 10pt; COLOR: #2b443f; FONT-FAMILY: Verdana; mso-fareast-font-family: MingLiU; mso-bidi-font-family: 'Courier New'; mso-no-proof: yes"> </SPAN>




    



   <%-- <asp:ListView runat="server" ID="ListView1" DataSourceID="SqlDataSourceSidurim" >        
      <LayoutTemplate>
        <table runat="server" id="table1" >
          <tr runat="server" id="itemPlaceholder" ></tr>
        </table>
      </LayoutTemplate>
      <ItemTemplate>
        <table runat="server" id="tblSidurim"  border="1">
            <tr style="height:50px">    
                <td style="width:30px">
                    מספר סידור
                </td>
                <td style="width:40px">
                    שעת התחלה
                </td>
                <td style="width:30px">
                    שעת סיום
                </td>
                <td style="width:50px">
                    סיבת אי החתמה כניסה
                </td>
                <td style="width:50px">
                    סיבת אי החתמה יציאה
                </td>
                <td>
                    שעת התחלה לתשלום
                </td>
                <td>
                    שעת גמר לתשלום
                </td>
                <td>
                    חריגה
                </td>
                <td>
                   פיצול / הפסקה
                </td>
                <td>
                   המרה
                </td>
                <td>
                   השלמה
                </td>
                <td>
                   מחוץ למכסה
                </td>
                <td>
                   לא לתשלום
                </td>
                <td>
                   בטל
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("mispar_sidur") %>' />              
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server"  Text='<%#Eval("shat_hatchala") %>' /> 
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("shat_gmar") %>' /> 
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlInReson" ></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOutReson" ></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("shat_hatchala_letashlum") %>' />
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("shat_gmar_letashlum") %>' />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlException" ></asp:DropDownList>
                </td>
                <td>
                 
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                  
                </td>
                <td>
                   
                </td> 
            </tr>
        </table>
    </ItemTemplate>
</asp:ListView>--%>

<%--<asp:SqlDataSource
  id="SqlDataSource1"
  runat="server"
  DataSourceMode="DataSet"  
  ConnectionString="Data Source=kdstst.world;User ID=KDSADMIN;Password=maayan"  
  SelectCommand="SELECT o.mispar_ishi, o.mispar_sidur, to_char(o.shat_gmar,'HH:MM') shat_gmar, to_char(o.shat_hatchala,'HH:MM') shat_hatchala, o.visa, 
           o.chariga,o.pitzul_hafsaka,o.taarich,o.shayah_leyom_kodem,o.mispar_shiurey_nehiga,   
           o.out_michsa ,o.hamarat_shabat, o.mikum_shaon_knisa,o.mikum_shaon_yetzia,
           o.hashlama,o.lo_letashlum,o.mikum_shaon_knisa,
           o.kod_siba_ledivuch_yadani_in, o.kod_siba_ledivuch_yadani_out, o.kod_siba_lo_letashlum,
           to_char(o.shat_hatchala_letashlum,'HH:MM') shat_hatchala_letashlum, to_char(o.shat_gmar_letashlum,'HH:MM') shat_gmar_letashlum,o.mezake_nesiot,
           o.tosefet_grira, o.achuz_knas_lepremyat_visa,o.achuz_viza_besikun,
           o.mispar_musach_o_machsan,o.sug_hazmanat_visa,
           o.heara, o.taarich_idkun_acharon, o.meadken_acharon,
           
           po.kisuy_tor,po.makat_nesia,po.shat_yetzia,po.oto_no,po.mispar_sidur peilut_mispar_sidur,
           po.mispar_siduri_oto,po.mispar_visa,po.mispar_knisa,            
           sug_yom.erev_shishi_chag,sug_yom.shbaton, to_char(o.taarich,'d') iDay,ym.sug_yom, --mao.kod_matzav,
           v_sidurm.zakay_lezman_halbasha halbash_kod,
           v_sidurm.shat_hatchala_muteret, 
           v_sidurm.shat_gmar_muteret, 
           v_sidurm.zakay_michutz_lamichsa,           
           --v_sidurm.shat_hatchala_muteret hour_kod2, 
           v_sidurm.sidur_asur_ledaveach_peilut no_peilot_kod, 
           v_sidurm.asur_ledaveach_mispar_rechev no_oto_no,
           v_sidurm.sidur_namlak_visa sidur_visa_kod,
           v_sidurm.sector_avoda,
           v_sidurm.zakay_lehashlama_avur_sidur hashlama_kod,          
           v_sidurm.zakay_lechariga, v_sidurm.zakay_lehamara ,
           v_sidurm.zakay_leaman_nesia,
           v_sidurm.hova_ledaveach_peilut peilut_required_kod,
           v_sidurm.asur_ledivuach_lechevrot_bat sidur_not_valid_kod,
           --v_sidurm.sidur_netzer sidur_netzer_kod,
           v_sidurm.hukiyut_beshabaton sidur_not_in_shabton_kod,
           v_sidurm.sidur_misug_headrut headrut_type_kod,
           v_sidurm.sug_avoda,
           v_sidurm.shaon_nochachut,
           v_sidurm.mispar_sidur mispar_sidur_myuhad,
           v_sidurm.sidur_kaytanot_veruey_kayiz sidur_in_summer,
           v_sidurm.lo_letashlum_automati,
           v_sidurm.kizuz_al_pi_hatchala_gmar,
           v_element.peula_o_yedia_bilvad element_for_yedia,
           v_element.hova_mispar_rechev bus_number_must, 
           v_element.divuach_besidur_visa divuch_in_sidur_visa,
           v_element.sug_avoda element_hamtana,
           v_element.sector_zvira_zman_haelement element_zvira_zman, 
           v_element.erech_element element_in_minutes ,
           v_element.mispar_sidur_matalot_tnua,
           v_element.bitul_biglal_ichur_lasidur                                    
    FROM tb_sidurim_ovdim o,tb_peilut_ovdim po,
         tb_yamim_meyuchadim ym,ctb_sugey_yamim_meyuchadim sug_yom,
         pivot_sidurim_meyuchadim v_sidurm,
         pivot_meafyeney_elementim v_element         
    WHERE o.mispar_ishi = po.mispar_ishi(+)
          AND o.taarich = po.taarich(+)
          AND o.mispar_sidur= po.mispar_sidur(+)
          AND o.shat_hatchala = po.shat_hatchala_sidur(+)
          AND o.mispar_ishi=63626
          AND o.taarich=to_date('28/04/2009','dd/mm/yyyy')          
          AND o.taarich=ym.taarich(+)
          AND ym.sug_yom=sug_yom.sug_yom(+)         
          AND v_sidurm.mispar_sidur(+)=o.mispar_sidur          
          AND to_date('28/04/2009','dd/mm/yyyy')  between v_sidurm.me_tarich(+) and v_sidurm.ad_tarich(+)
          AND to_number(substr(po.makat_nesia,2,2)) = v_element.kod_element(+)
          AND to_date('28/04/2009','dd/mm/yyyy')  between v_element.me_tarich(+) and v_element.ad_tarich(+)          
    ORDER BY o.shat_hatchala  ,po.shat_yetzia" ProviderName="Oracle.DataAccess.Client">
</asp:SqlDataSource>--%>


         <p>

 <asp:TextBox runat=server ID="txtTest"></asp:TextBox>
         </p>
        

    </form>
</body>
</html>
