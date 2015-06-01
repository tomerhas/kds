<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes" encoding="windows-1255" omit-xml-declaration="yes" version="4.0"/>
  <!--<xsl:param name="FolderPath" />-->
  <xsl:template match="/mail">
    <html>
      <meta content="MSHTML 6.00.2900.3603" name="GENERATOR"></meta>
      <body dir="rtl">
          <div id="divFolder" runat="server" style="width:100%;border-style:solid;border-width:1px;">
            <table>
              <tr>
                <td>
                    הדו"ח הופק ונמצא בתקיה 
                  <a>
                  <xsl:attribute name="href">
                    <xsl:value-of select ="@FolderPath"/>
                  </xsl:attribute>
                    <xsl:value-of select ="@NameFolder"/>
                  </a>
                </td>
              </tr>
            </table>
          </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
