using System;
using System.IO;

namespace NetProOA.TemplateE.Web
{
    public class VueRender
    {
        public static string Html { get; set; }

        public static void InitHtml()
        {
            Html = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "wwwroot", "index.html"));
        }

        public static string GetRawHtml()
        {
            return Html;
        }
    }
}
