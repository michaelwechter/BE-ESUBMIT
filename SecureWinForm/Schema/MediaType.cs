using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace SECURE.Common.Controller.Media.Schema
{
    /// <summary>
    /// Contains the SECURE supported media types (XML and JSON)
    /// </summary>
    /// <remarks>
    /// MIME Type Detection in Windows Internet Explorer
    /// http://msdn.microsoft.com/en-us/library/ms775147.aspx
    /// </remarks>
    public static class SupportedMediaTypes
    {
        /// <summary>
        /// Plain Text Media Type
        /// </summary>
        public static MediaTypeHeaderValue PlainText = new MediaTypeHeaderValue("text/plain");
        /// <summary>
        /// XML Media Type
        /// </summary>
        public static MediaTypeHeaderValue XML = new MediaTypeHeaderValue("application/xml");
    }
}
