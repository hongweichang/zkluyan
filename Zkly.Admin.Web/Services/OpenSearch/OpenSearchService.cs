﻿namespace Zkly.Admin.Web.Services
{
    using System.IO;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using Zkly.Admin.Web.Constants;
    using Zkly.Admin.Web.Framework;

    public sealed class OpenSearchService : IOpenSearchService
    {
        private readonly UrlHelper urlHelper;

        public OpenSearchService(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        /// <summary>
        /// Gets the Open Search XML for the current site. You can customize the contents of this XML here.
        /// See <see cref="http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart" />
        /// and <see cref="http://www.opensearch.org" /> for more information.
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        public string GetOpenSearchXml()
        {
            // Short name must be less than or equal to 16 characters.
            string shortName = "Search";
            string description = "Search the ASP.NET MVC Boilerplate Site";

            // The link to the search page with the query string set to 'searchTerms' which gets replaced with a user defined query.
            string searchUrl = this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSearch, new { query = "{searchTerms}" });

            // The link to the page with the search form on it. The home page has the search form on it.
            string searchFormUrl = this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex);

            // The link to the favicon.ico file for the site.
            string favicon16Url = this.urlHelper.AbsoluteContent("~/content/icons/favicon.ico");

            // The link to the favicon.png file for the site.
            string favicon32Url = this.urlHelper.AbsoluteContent("~/content/icons/favicon-32x32.png");

            // The link to the favicon.png file for the site.
            string favicon96Url = this.urlHelper.AbsoluteContent("~/content/icons/favicon-96x96.png");

            XNamespace ns = "http://a9.com/-/spec/opensearch/1.1";
            XDocument document = new XDocument(
                new XElement(
                    ns + "OpenSearchDescription",
                    new XElement(ns + "ShortName", shortName),
                    new XElement(ns + "Description", description),
                    new XElement(
                        ns + "Url",
                        new XAttribute("type", "text/html"),
                        new XAttribute("method", "get"),
                        new XAttribute("template", searchUrl)),
                  new XElement(
                            ns + "Image",
                            favicon16Url,
                            new XAttribute("height", "16"),
                            new XAttribute("width", "16"),
                            new XAttribute("type", "image/x-icon")),
                  new XElement(
                            ns + "Image",
                            favicon32Url,
                        new XAttribute("height", "32"),
                        new XAttribute("width", "32"),
                        new XAttribute("type", "image/png")),
                  new XElement(
                            ns + "Image",
                            favicon96Url,
                            new XAttribute("height", "96"),
                            new XAttribute("width", "96"),
                            new XAttribute("type", "image/png")),
                  new XElement(ns + "InputEncoding", "UTF-8"),
                  new XElement(ns + "SearchForm", searchFormUrl)));

            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriterWithEncoding(stringBuilder, Encoding.UTF8))
            {
                document.Save(stringWriter);
            }

            return stringBuilder.ToString();
        }
    }
}