#pragma checksum "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9431974baeb9c40b900090ac8e77e11c9df89ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Identity_Home_Index), @"mvc.1.0.view", @"/Views/Identity/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\_ViewImports.cshtml"
using Spotlight;

#line default
#line hidden
#line 2 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\_ViewImports.cshtml"
using Spotlight.Models;

#line default
#line hidden
#line 3 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\_ViewImports.cshtml"
using Spotlight.Models.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9431974baeb9c40b900090ac8e77e11c9df89ea", @"/Views/Identity/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30bb6917a6f2a179dd58f18f100cc926bc3a4300", @"/Views/_ViewImports.cshtml")]
    public class Views_Identity_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<string, object>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#line 2 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            WriteLiteral("\r\n");
            WriteLiteral("<div class=\"bg-primary m-1 p-1 text-white\"><h4>User Details</h4></div>\r\n<table class=\"table table-sm table-bordered m-1 p-1\">\r\n");
#line 9 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml"
     foreach (var kvp in Model) {

#line default
#line hidden
            WriteLiteral("        <tr><th>");
#line 10 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml"
           Write(kvp.Key);

#line default
#line hidden
            WriteLiteral("</th><td>");
#line 10 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml"
                            Write(kvp.Value);

#line default
#line hidden
            WriteLiteral("</td></tr>\r\n");
#line 11 "C:\Users\Nikola\Desktop\New folder\1.Spotlight-master28end-editdelete\Spotlight\Views\Identity\Home\Index.cshtml"
    }

#line default
#line hidden
            WriteLiteral("</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<string, object>> Html { get; private set; }
    }
}
#pragma warning restore 1591
