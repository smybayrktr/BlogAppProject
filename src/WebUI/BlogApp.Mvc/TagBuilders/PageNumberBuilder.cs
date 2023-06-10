using System;
using BlogApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogApp.Mvc.TagBuilders
{
    [HtmlTargetElement("div",Attributes ="page-model")]
	public class PageNumberBuilder : TagHelper
    {
        public string PageAction { get; set; } //Linke tıklayınce gideceği sayfa?

        public PagingInfo PageModel { get; set; }

        /*
		 * <div aria-label="...">
         *   <ul class="pagination pagination-sm">
         *       <li class="page-item"><a class="page-link" href="#">3</a></li>
         *   </ul>
         * </div>
		 */

        IUrlHelperFactory urlHelperFactory; //PageActionu oluşturacak olan string

        [ViewContext]
        [HtmlAttributeNotBound] //Html de kullanmaması için.
        public ViewContext ViewContext { get; set; } //Viewde bulunan ulaşılabilecek değerler

        public PageNumberBuilder(IUrlHelperFactory factory)
        {
            urlHelperFactory = factory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination pagination-sm");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("page-item");
                if (i == PageModel.CurrentPage)
                {
                    li.AddCssClass("active");
                }
                TagBuilder a = new TagBuilder("a");
                a.AddCssClass("page-link");
                a.Attributes["href"] = urlHelper.Action(PageAction, new { pageNo = i,id=PageModel.CategoryId });
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);

            }
            div.InnerHtml.AppendHtml(ul);

            output.Content.AppendHtml(div);
        }


    }
}

