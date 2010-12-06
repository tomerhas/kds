<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes" encoding="windows-1255" omit-xml-declaration="yes" version="4.0"/>
  <xsl:param name="adminAddress" />
  <xsl:param name="subject" />
  <xsl:param name="url" />
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
                <td style="font-weight:bold;">
                  שלום
                </td>
                <td style="font-weight:bold;">
                  <xsl:value-of select="concat(@firstname,' ',@lastname)"/>
                </td>
              </tr>
              <tr>
                <td style="font-weight:bold;text-decoration:underline;">
                  הנידון:
                </td>
                <td>
                  <xsl:value-of select="$subject"/>
                </td>


              </tr>
            </table>
            <div style="width:100%;border-style:solid;border-width:1px;">
              <table>
                <tr>
                  <td colspan="3">
                    <xsl:choose>
                      <xsl:when test="number(@main)=1">
                        קיימים במערכת מקרים הממתינים לאישורך:
                      </xsl:when>
                      <xsl:otherwise>
                        קיימים במערכת מקרים אשר הועברו לטיפולך:
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
              
                <xsl:for-each select="entry">
                  <tr>
                    <td>
                      <xsl:value-of select="@approvals"/>
                    </td>
                    <td>מקרים בחודש</td>
                    <td>
                      <a>
                        <xsl:attribute name="href">
                          <xsl:value-of select ="concat($url,'?month=',@month)"/>
                        </xsl:attribute>
                        <xsl:value-of select="@month"/>
                      </a>
                    </td>
                  </tr>
                </xsl:for-each>
              </table>
            </div>
            <br></br>
            <div>
              <table>
                <tr>
                  <td style="font-weight:bold;">
                    לכל שאלה או בעיה במערכת ניתן לפנות למנהל המערכת:
                  </td>
                </tr>
                <tr>
                  <td>
                    <a>
                      <xsl:attribute name="href">
                        <xsl:value-of select ="concat('mailto:',$adminAddress)"/>
                      </xsl:attribute>
                      <xsl:value-of select="$adminAddress"/>
                    </a>
                  </td>
                </tr>
              </table>
             </div>
          </div>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
