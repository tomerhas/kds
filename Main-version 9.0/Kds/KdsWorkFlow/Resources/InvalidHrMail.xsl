<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes" encoding="windows-1255" omit-xml-declaration="yes" version="4.0"/>
    <xsl:template match="/mail">
      <html>
        <meta content="MSHTML 6.00.2900.3603" name="GENERATOR"></meta>
        <body dir="rtl">
          <div style="text-align:right;font-family:Arial;font-size:10;">
            <table>
              <tr>
                <td>
                  <img src="cid:head"></img>
                </td>
              </tr>
            </table>
            <table>
              <tr>
                <td style="font-weight:bold;" colspan="2">
                  לא נמצא גורם מאשר ראשי
                </td>
              </tr>
              <tr>
                <td>מספר אישי:</td>
                <td>
                  <xsl:value-of select="@employeeNumber"/>
                </td>
              </tr>
              <tr>
                <td>תאריך:</td>
                <td>
                  <xsl:value-of select="@workDate"/>
                </td>
              </tr>
              <tr>
                <td>קוד אישור:</td>
                <td>
                  <xsl:value-of select="@code"/>
                </td>
              </tr>
              <tr>
                <td>קוד תפקיד:</td>
                <td>
                  <xsl:value-of select="@jobCode"/>
                </td>
              </tr>
            </table>
          </div>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
