<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:media="http://search.yahoo.com/mrss/">
  <xsl:output method="html" encoding="UTF-8" indent="yes" omit-xml-declaration="yes"/>
  <xsl:param name="whichOne" select="1"/>
  <xsl:template match="/">
    <tbody>
      <tr>
        <td>
          <p>
            <strong>
              <xsl:element name="a">
                <xsl:attribute name="id">DataList1_ctl00_title</xsl:attribute>
                <xsl:attribute name="href">
                  <xsl:value-of select="rss/channel/item[$whichOne]/link[1]"/>
                </xsl:attribute>
                <xsl:value-of select="rss/channel/item[$whichOne]/title[1]"/>
              </xsl:element>
            </strong>
            <br/>
            <span id="DataList1_ctl00_date" style="display:none">
              <xsl:value-of select="substring-before(rss/channel/item[$whichOne]/pubDate[1], '+0000')"/>
            </span>
            <span id="DataList1_ctl00_creator">
              <xsl:value-of select="rss/channel/item[$whichOne]/dc:creator[1]"/>
            </span>
          </p>
          <p>
            <xsl:element name="img">
              <xsl:attribute name="id">DataList1_ctl00_media</xsl:attribute>
              <xsl:attribute name="class">right</xsl:attribute>
              <xsl:attribute name="src">
                <xsl:value-of
                  select="rss/channel/item[$whichOne]/media:content[./media:title/text() != ./preceding-sibling::dc:creator/text()]/@url"
                />
              </xsl:attribute>
            </xsl:element>
            <span id="DataList1_ctl00_description">
              <xsl:value-of select="substring-before(rss/channel/item[$whichOne]/description[1], '&lt;img ')" disable-output-escaping="yes"/>
              <xsl:element name="a">
                <xsl:attribute name="id">DataList1_ctl00_more</xsl:attribute>
                <xsl:attribute name="href">
                  <xsl:value-of select="rss/channel/item[$whichOne]/link[1]"/>
                </xsl:attribute>
                more
              </xsl:element>
            </span>
          </p>
          <p>
            <strong>
              <a href="http://sowingculture.wordpress.com">From the Sowing Culture
                blog</a>
            </strong>
          </p>
        </td>
      </tr>
    </tbody>
  </xsl:template>
</xsl:stylesheet>
