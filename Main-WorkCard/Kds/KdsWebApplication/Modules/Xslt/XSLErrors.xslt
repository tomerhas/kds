<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">  
  <xsl:output method="xml" indent="yes"/>
  <xsl:variable name="RowKey">0</xsl:variable>
  <xsl:template match="/">
    <table border="0" width="100%" >     
      <xsl:for-each select="NewDataSet/Table[SHOW_ERROR=1]">       
          <tr class="WorkCardErrorLine">
            <td width="340px">
              <xsl:value-of select="TEUR_SHGIA"/>
            </td>                          
            <td align="left" width="100px">
              <xsl:if test="ISHUR_RASHEMET=1 and USER_PROFILE=0">
                 <Input type="button" ID="btnErrCancel"  value="בטל שגיאה" class="btnWorkCancelError" Width="100px" Height="34px" onClick="CancelError(this);"  />
              </xsl:if>
              <xsl:if test="ISHUR_RASHEMET=0 or USER_PROFILE=1">
                <Input type="button" ID="btnErrCancel"  value="בטל שגיאה" class="btnWorkCancelErrorDisabled" Width="100px" Height="34px" disabled="disabled"/>
              </xsl:if> 
            </td>                                                    
            <td align="left">
              <xsl:if test="KOD_ISHUR>0 and USER_PROFILE=0">
                  <Input type="button" ID="btnErrApproval"  value="העברה לאישור" class="btnWorkCardUpdate" Height="34px" onClick="ApproveError(this);" />
              </xsl:if>
              <xsl:if test="KOD_ISHUR=0 or USER_PROFILE=1">
                  <Input type="button" ID="btnErrApproval" value="העברה לאישור" class="btnWorkCardUpdateDisabled" Height="34px" disabled="disabled" />
              </xsl:if> 
            </td>
            <td style="display:none">
              <label id="lblKey">
                <xsl:value-of select="ERR_KEY"/>
              </label>
            </td>           
          </tr>           
      </xsl:for-each>      
    </table>    
  </xsl:template>
</xsl:stylesheet>
