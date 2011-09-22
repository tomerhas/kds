<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">  
  <xsl:output method="xml" indent="yes"/>
  <xsl:variable name="RowKey">0</xsl:variable>
  <xsl:template match="/">
    <table border="1">
      <tr>
        <th colspan="5" bgcolor="#FFFFDE">תיאור שגיאה</th>
      </tr>
     
      <xsl:for-each select="NewDataSet/Table[SHOW_ERROR=1]">       
          <tr>
            <td class="ErrItemRow">
              <xsl:value-of select="TEUR_SHGIA"/>
            </td>                          
            <td>
              <xsl:if test="ISHUR_RASHEMET=1 and USER_PROFILE=0">
                 <Input type="button" ID="btnErrCancel"  value="בטל שגיאה" class="ErrBtnItemRowEnable" Width="100px" Height="25px" onClick="CancelError(this);"  />
              </xsl:if>
              <xsl:if test="ISHUR_RASHEMET=0 or USER_PROFILE=1">
                <Input type="button" ID="btnErrCancel"  value="בטל שגיאה" class="ErrBtnItemRow" Width="100px" Height="25px" disabled="disabled"/>
              </xsl:if> 
            </td>                                                    
            <td>
              <xsl:if test="KOD_ISHUR>0 and USER_PROFILE=0">
                  <Input type="button" ID="btnErrApproval"  value="העברה לאישור" class="ErrBtnItemRowEnable" onClick="ApproveError(this);" />
              </xsl:if>
              <xsl:if test="KOD_ISHUR=0 or USER_PROFILE=1">
                  <Input type="button" ID="btnErrApproval" value="העברה לאישור" class="ErrBtnItemRow" disabled="disabled" />
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
