using System;
using System.Text;
using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Container
{
    public class BSFilter : IBSFilter, IDisposable
    {
        private HtmlHelper _htmlHelper;
        private string _panelTitle;
        private string _id;

        public BSFilter(HtmlHelper htmlHelper, string id, string title)
        {
            this._htmlHelper = htmlHelper;
            this._panelTitle = title;
            this._id = id;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class='card panel-filter'>");
            sb.Append("<div class='card-header'>");
            sb.AppendFormat("<h6 class='card-title'><a href='#' data-toggle='collapse' data-target='#{0}'><i class='fa {1}'></i> {2}<i class='fa fa-chevron-down pull-right'></i></a></h6>", 
                _id.Replace(".", "\\."), "fa-filter", _panelTitle);
            sb.AppendFormat("</div><div id='{0}' class='card-body' >", _id);
            sb.Append("<div class='card filter-group sm-bmargin hidden'>");
            sb.Append("<div class='card-header'><h6 class='card-title'>Seçimleriniz</h6></div>");
            sb.Append("<div class='list-group selectable list-group-selecteditems'></div></div>");
            sb.Append("<input id='SearchFilters' name='SearchFilters' class='list-group-searchfilters' value='' type='hidden'/>");

            _htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }

        public void Dispose()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='filter-group buttons'>");
            sb.Append("<a class='btn btn-sm btn-danger btn-filter-cancel'><i class='fa fa-trash'></i>Temizle</a>");
            sb.Append("<a class='btn btn-sm btn-success btn-filter-submit'><i class='fa fa-filter'></i>Listele</a>");
            sb.Append("</div>");
            sb.Append("</div></div>");

            _htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }
    }
}
