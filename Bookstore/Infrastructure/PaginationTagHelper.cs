using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Bookstore.Models.ViewModels;

//this page creates the pagination tag helper

namespace Bookstore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")] // for the div tag we are going to create a new attribute called page-blah
    public class PaginationTagHelper : TagHelper
    {
        // Dynamically create the page links for us

        private IUrlHelperFactory uhf; // helps us build tag helper links

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound] // 
        public ViewContext vc { get; set; } // view context file has information about the view that we are woking from

        // different than view context
        public PageInfo PageBlah { get; set; } // page-blah in HTML = PageBlah in C# (so we named our tag helper page-blah for html so we must receive it as PageBlah in C#
        public string PageAction { get; set; } // this is page-action in HTML

        // CSS TEST
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        // END OF CSS TEST

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageBlah.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                // CSS TEST
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageBlah.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }

                // END OF CSS TEST

                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }

    }
}
