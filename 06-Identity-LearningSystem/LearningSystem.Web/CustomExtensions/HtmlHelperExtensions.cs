namespace LearningSystem.Web.CustomExtensions
{
    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString SuccessButton(this HtmlHelper helper, string innerText, string path)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.AddCssClass("btn btn-success");
            builder.MergeAttribute("href", path);
            builder.InnerHtml = innerText;
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string imageUrl, string alt = "", string width = "auto", string height = "auto")
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("width", String.IsNullOrEmpty(width) ? "auto" : $"{width}px");
            builder.MergeAttribute("height", String.IsNullOrEmpty(height) ? "auto" : $"{height}px");
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString YouTubeEmbedder(this HtmlHelper helper, string videoId, int width = 560, int height = 315)
        {
            TagBuilder builder = new TagBuilder("iframe");
            builder.MergeAttribute("src", @"https://www.youtube.com/embed/" + videoId);
            builder.MergeAttribute("frameborder", "0");
            builder.MergeAttribute("allowfullscreen", null);
            builder.MergeAttribute("width", $"{width}px");
            builder.MergeAttribute("height", $"{height}px");
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString TableHelper<T>(this HtmlHelper helper, IEnumerable<T> collection, params string[] styles)
        {
            TagBuilder builder = new TagBuilder("table");
            foreach (var style in styles)
            {
                builder.AddCssClass(style);
            }

            TagBuilder tableHeaders = new TagBuilder("tr");
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                TagBuilder propHeader = new TagBuilder("th");
                propHeader.SetInnerText(prop.Name);
                tableHeaders.InnerHtml += propHeader;
            }

            builder.InnerHtml += tableHeaders;

            foreach (var item in collection)
            {
                TagBuilder tableData = new TagBuilder("tr");
                foreach (var prop in props)
                {
                    TagBuilder userProp = new TagBuilder("td");
                    userProp.SetInnerText(GetPropValue(item, prop.Name));
                    tableData.InnerHtml += userProp;
                }
                builder.InnerHtml += tableData;
            }

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static string GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
        }
    }
}