using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.Button;
using Domain.WebHelpers.HtmlHelpers.Grid.Column;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.Enum;
using static Domain.WebHelpers.Infrastructre.Enums;

namespace Domain.WebHelpers.HtmlHelpers.Grid
{
    public class BSGrid<T> : IBSGrid<T>, IHtmlString where T : class
    {
        #region properties
        private string _id;
        private string _title;
        private string _cssIcon;
        private bool _isHeaderVisible;
        private HtmlHelper _helper;
        private string _ajaxUrl;
        private string _functionName;
        private bool _isFunctionCallInstantly;
        private int _pageLength;
        private string _callBackFunctionName;
        private string _emptyText;
        private object _postData;
        private bool _isParentTable;
        private bool _isDetailTable;
        private string _detailTablePartialUrl;
        private bool _isPagingEnabled;
        private bool _showExportButton;
        private bool _showPageLength;

        private bool _exportAllData;
        private bool _addRowNumber;

        private int _sortIndex;
        private EnumSortType _sortType;
        private string _height;
        private GridSelectionType _selectionType;

        private List<int> _pageLengthList = new List<int>();
        private List<GridButton> _toolbarButtons = new List<GridButton>();
        private List<GridColumn<T>> _columns = new List<GridColumn<T>>();
        private string _searchFilterGuid;
        #endregion

        #region constructor
        public BSGrid(HtmlHelper helper, string id = "")
        {

            _id = id;
            _helper = helper;
            _isHeaderVisible = true;
            _functionName = "Listele";
            _isFunctionCallInstantly = true;
            _pageLength = 10;
            _emptyText = "";
            _postData = null;
            _isParentTable = false;
            _isDetailTable = false;
            _isPagingEnabled = true;
            _selectionType = GridSelectionType.None;
            _pageLengthList = new List<int> { 10, 25, 50 };
#if Kobi
            _showExportButton = false;
            _showPageLength=false;
#else
            _showExportButton = true;
            _showPageLength = true;
#endif
            _exportAllData = false;
            _addRowNumber = false;

        }

        public BSGrid(HtmlHelper<T> helper, string id = "")
        {
            _id = id;
            _helper = helper;
            _isHeaderVisible = true;
            _functionName = "Listele";
            _isFunctionCallInstantly = true;
            _pageLength = 10;
            _emptyText = "Kayıt bulunmamaktadır";
            _postData = null;
            _isParentTable = false;
            _isDetailTable = false;
            _isPagingEnabled = true;
            _selectionType = GridSelectionType.None;
            _pageLengthList = new List<int> { 10, 25, 50 };
#if Kobi
            _showExportButton = false;
            _showPageLength=false;
#else
            _showExportButton = true;
            _showPageLength = true;
#endif
            _exportAllData = false;
            _addRowNumber = false;
        }
        #endregion

        #region properties fluent
        public IBSGrid<T> SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IBSGrid<T> SetColumns(Action<GridColumnBuilder<T>> columnBuilder)
        {
            var builder = new GridColumnBuilder<T>();
            columnBuilder(builder);
            foreach (var column in builder)
            {
                _columns.Add(column);
            }
            return this;
        }
        public IBSGrid<T> SetToolbarButtons(Action<GridButtonBuilder> btnBuilder)
        {
            var builder = new GridButtonBuilder();
            btnBuilder(builder);
            foreach (var button in builder)
            {
                _toolbarButtons.Add(button);
            }
            return this;
        }
        public IBSGrid<T> SetCssIcon(string cssIcon = "")
        {
            _cssIcon = cssIcon;
            return this;
        }
        public IBSGrid<T> SetHeaderVisible(bool isHeaderVisible)
        {
            _isHeaderVisible = isHeaderVisible;
            return this;
        }
        public IBSGrid<T> SetAjaxUrl(string ajaxUrl, object postData = null)
        {
            _ajaxUrl = ajaxUrl;
            _postData = postData;
            return this;
        }

        public IBSGrid<T> SetDefaultOrder(int sortIndex, EnumSortType sortType = EnumSortType.desc)
        {
            _sortIndex = sortIndex;
            _sortType = sortType;
            return this;
        }
        public IBSGrid<T> SetFunctionName(string functionName = "Listele")
        {
            _functionName = functionName;
            return this;
        }
        public IBSGrid<T> SetFunctionCallInstantly(bool isFunctionCallInstantly = true)
        {
            _isFunctionCallInstantly = isFunctionCallInstantly;
            return this;
        }
        public IBSGrid<T> SetPageLength(int pageLength = 10)
        {
            _pageLength = pageLength;
            if (!_pageLengthList.Contains(_pageLength))
                _pageLengthList.Add(_pageLength);
            return this;
        }
        public IBSGrid<T> SetPaging(bool isPagingEnabled)
        {
            _isPagingEnabled = isPagingEnabled;
            return this;
        }
        public IBSGrid<T> SetCallBackFunctionName(string callBackFunctionName)
        {
            _callBackFunctionName = callBackFunctionName;
            return this;
        }
        public IBSGrid<T> SetEmptyText(string emptyText)
        {
            _emptyText = emptyText;
            return this;
        }
        public IBSGrid<T> SetParentTable()
        {
            _isParentTable = true;
            return this;
        }
        public IBSGrid<T> SetDetailTable()
        {
            _isDetailTable = true;
            return this;
        }
        public IBSGrid<T> SetDetailTablePartialUrl(string detailTablePartialUrl)
        {
            _detailTablePartialUrl = detailTablePartialUrl;
            return this;
        }

        public IBSGrid<T> SetSelectionType(GridSelectionType selectionType)
        {
            _selectionType = selectionType;
            return this;
        }

        public IBSGrid<T> SetExportButtonShow(bool showExportButton, bool exportAllData = false)
        {
            _exportAllData = exportAllData;
            _showExportButton = showExportButton;
            return this;
        }

        public IBSGrid<T> SetPageLengthShow(bool showPageLength)
        {
            _showPageLength = showPageLength;
            return this;
        }

        public IBSGrid<T> SetShowRowNumber(bool showRowNumber)
        {
            _addRowNumber = showRowNumber;
            return this;
        }

        #endregion

        #region return html to view
        public override string ToString()
        {
            return ToHtmlString();
        }
        public string ToHtmlString()
        {
            var sb = new StringBuilder();
            var guid = Guid.NewGuid().ToString().Replace("-", "");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

            if (_id.IsBlank())
            {
                _id = Guid.NewGuid().ToString();
            }
            else if (_isDetailTable)
            {
                _id = _id + guid;
            }

            if (string.IsNullOrEmpty(_title))
                _title = "Liste";

            if (string.IsNullOrEmpty(_cssIcon))
                _cssIcon = "fa-gear";

            sb.Append("<div class='m-portlet m-portlet--brand m-portlet--head-solid-bg w-100'>");

            if (_isHeaderVisible)
            {
                sb.AppendFormat(@"
			<div class='m-portlet__head'>
				<div class='m-portlet__head-caption'>
					<div class='m-portlet__head-title'>
						<span class='m-portlet__head-icon'>
							<i class='fa {0}'></i>
						</span>
						<h3 class='m-portlet__head-text'>
							{1}
						</h3>
					</div>			
				</div>
                ", _cssIcon, _title);


                if (_toolbarButtons.Count > 0)
                {
                    sb.Append("<div class='m-portlet__head-tools'> <ul class='m-portlet__nav'>");
                    foreach (var item in _toolbarButtons)
                    {
                        sb.Append(item.ToString());
                    }
                    sb.Append("</ul> </div>");
                }

                sb.Append("</div>");
            }

            sb.Append("<div class='m-portlet__body grid_portlet__body'>");

            var detailColumn = _columns.FirstOrDefault(w => w.IsDetail);

            if (_isParentTable && detailColumn == null)
            {
                throw new Exception("Parent table olan tablolarda en az bir detail özelliği set edilmiş kolon bulunmalıdır.");
            }

            if (_isDetailTable)
                _functionName += guid;

            //sb.AppendFormat("<table id='{0}' class='table table-bordered table-hover no-margin bsDataTable w-100 {1}'>", _id, _isParentTable ? "parentTable" : "");
            sb.AppendFormat("<table id='{0}' class='table table-bordered table-hover no-margin bsDataTable w-100 {1}' {2} {3} data-function-name='{4}' {5}>", _id, _isParentTable ? "parentTable" : "", _isParentTable ? "data-detailtable-partial-url='" + _detailTablePartialUrl + "'" : "", _isParentTable ? "data-detail-column=" + Util.GetPropertyInfo<T>(detailColumn.expression).Name : "", _functionName, !string.IsNullOrEmpty(_searchFilterGuid) ? "data-searchfilterguid='" + _searchFilterGuid + "'" : "");

            sb.Append("<thead><tr>");

            var columnBuilder = new StringBuilder();



            if (detailColumn != null)
            {
                sb.AppendFormat("<th class='detail dtnotexport'></th>");

                columnBuilder.Append("{");
                columnBuilder.AppendFormat("defaultContent: '', \"orderable\" : false, \"visible\" : true");
                columnBuilder.Append("},");
            }

            if (_selectionType != GridSelectionType.None)
            {
                if (_selectionType == GridSelectionType.Multi)
                {
                    sb.Append("<th class=\"dtnotexport\">").Append($"<label for=\"{_id}_grdCheckAll\" class=\"m-checkbox m-checkbox--solid m-checkbox--success\"><input data-parent=\"{_id}\" id=\"{_id}_grdCheckAll\"  class=\"grdChkAll\" type=\"checkbox\"><span></span></label>").Append("</th>");
                    columnBuilder.Append("{orderable:false, data: function(row, type, set) {" +
                                         "var chkId=RandomIdGetir(); " +
                                         "return '" + "<label for=\"'+chkId+'\" class=\"m-checkbox m-checkbox--solid m-checkbox--success\"><input data-parent=\"" + _id + "\" id=\"'+chkId+'\" class=\"grdChk\" type=\"checkbox\"><span></span></label>';}},");
                }
                else if (_selectionType == GridSelectionType.Single)
                {
                    sb.Append("<th class=\"dtnotexport\"></th>");
                    columnBuilder.Append("{orderable:false, data: function(row, type, set) {" +
                                         "var rbId=RandomIdGetir(); " +
                                         "return '" + "<label for=\"'+rbId+'\" class=\"m-radio m-radio--solid m-radio--success\"><input name=\"" + _id + "_rb\" id=\"'+rbId+'\" class=\"grdRb\" type=\"radio\"><span></span></label>';}},");
                }
            }

            if (_addRowNumber)
            {
                sb.AppendFormat("<th class='dtRowNumber'></th>");
                columnBuilder.Append("{");
                columnBuilder.AppendFormat("defaultContent: '', \"orderable\" : false, \"visible\" : true,width:10,");
                columnBuilder.Append(@"render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; }");
                columnBuilder.Append("},");
            }

            var sortIndex = 0;
            for (var i = 0; i < _columns.Count; i++)
            {
                var column = _columns[i];
                var propInfo = Util.GetPropertyInfo<T>(column.expression);

                if (column.IsSortable && sortIndex == 0)
                {
                    sortIndex = i;
                }

                var columnClass = "";

                if (!column.IsExportable)
                    columnClass += " dtnotexport ";

                if (column.IsDetail)
                    columnClass += " d-none dtnotexport";

                var columnTitle = column.CustomTitle.IsBlank() ? Util.GetDisplayName<T>(propInfo) : column.CustomTitle;
                sb.AppendFormat("<th {1} >{0}</th>", columnTitle, !string.IsNullOrEmpty(columnClass) ? $"class=\"{columnClass}\"" : "");

                //if (column.CustomTitle.IsBlank())
                //{
                //    var displayName = Util.GetDisplayName<T>(propInfo);
                //    sb.AppendFormat("<th {1}>{0}</th>", displayName, !column.IsExportable ? "class=\"dtnotexport\"" : "");
                //}
                //else
                //{
                //    sb.AppendFormat("<th {1}>{0}</th>", columnTitle, !column.IsExportable ? "class=\"dtnotexport\"" : "");
                //}

                columnBuilder.Append("{");

                if (column.CustomDataIndex.IsBlank())
                {
                    columnBuilder.AppendFormat(" data : '{0}' ", propInfo.Name);
                }
                else
                {
                    columnBuilder.AppendFormat(" data : '{0}' ", column.CustomDataIndex);
                }

                if (column.IsFixed)
                {

                }

                if (!column.DateFormat.IsBlank())
                {
                    columnBuilder.AppendFormat(@", render: function(d)");
                    columnBuilder.AppendLine("{");
                    columnBuilder.AppendFormat(@"if(d != null) return moment(d).format('{0}'); else return '';", column.DateFormat);
                    columnBuilder.AppendLine("}");
                }

                if (column.ColumnLength > 0)
                    columnBuilder.AppendFormat($", render: $.fn.dataTable.render.ellipsis({column.ColumnLength}, true)");

                columnBuilder.AppendFormat(", \"orderable\" : {0}", column.IsSortable ? "true" : "false");
                columnBuilder.AppendFormat(", \"visible\" : {0}", column.IsVisible && !column.IsDetail ? "true" : "false");
                if (!column.Width.IsBlank())
                {

                }

                if (!column.ClassName.IsBlank())
                    columnBuilder.AppendFormat(", className : '{0}'", column.ClassName == "grdIslemler" ? " grdIslemler dtnotexport " : column.ClassName);

                columnBuilder.Append("},");
            }

            var exportButton = new StringBuilder();
            if (_showExportButton)
            {
                exportButton.Append("{");
                exportButton.Append("extend: 'excel',");
                exportButton.AppendFormat("filename: '{0}',", string.IsNullOrEmpty(_title) ? "Domain" : _title);
                exportButton.Append("text:'<i class=\"fa fa-file-excel-o\"></i>',");
                exportButton.Append("titleAttr: 'Excel',");
                exportButton.Append("exportOptions: {columns: ':not(.dtnotexport)', orthogonal: 'export'},");
                exportButton.Append("className: 'btn btn-sm btn-success',");
                exportButton.Append(@"init: function( api, node, config) {
                    $(node).removeClass('dt-button')}");
                if (_exportAllData)
                    exportButton.Append(",action: newExportAction");
                exportButton.Append("}");
                exportButton.Append(",{");
                exportButton.Append("extend: 'print',");
                exportButton.Append("text:'<i class=\"fa fa-print\"></i>',");
                exportButton.Append("titleAttr: 'Yazdır',");
                exportButton.AppendFormat("messageTop: '{0}',", string.IsNullOrEmpty(_title) ? "Domain" : _title);
                exportButton.Append("messageBottom: null,");
                exportButton.Append("exportOptions: {columns: ':not(.dtnotexport)', orthogonal: 'export'},");
                exportButton.Append("className: 'btn btn-sm btn-info',");
                exportButton.Append(@"init: function( api, node, config) {
                    $(node).removeClass('dt-button')}");
                if (_exportAllData)
                    exportButton.Append(",action: newExportAction");
                exportButton.Append("}");
            }

            sb.Append("</tr></thead>");
            sb.Append("<tbody></tbody>");

            sb.AppendLine("</table></div></div>");

            if (_sortIndex == 0)
                _sortIndex = sortIndex;

            var data = JsonConvert.SerializeObject(_postData);

            sb.AppendFormat("<script>function {0}(postData)", _functionName);
            sb.Append("{");

            if (_postData != null)
            {
                sb.AppendFormat("var data = {0};", data);
            }
            else
            {
                sb.Append("var data = { };");
            }

            sb.Append("if (postData != undefined && postData != null)");
            sb.Append("{");
            sb.Append("$.each(postData, function(key, value) {");
            sb.Append("data[key] = value;");
            sb.Append("});");
            sb.Append("}");
            sb.Append("setSelectionData();");
            sb.Append("data.searchFilters = $('#SearchFilters').val();");            

            sb.AppendFormat("$('#{0}').dataTable(", _id);
            sb.Append("{ order: [[");
            sb.AppendFormat("{0}, \"{1}\"]],", _sortIndex, _sortType.ToString());

            var pagelengthList = string.Join(",", _pageLengthList.OrderBy(o => o).ToList());

            sb.AppendFormat("paging:{0}," + (!string.IsNullOrEmpty(_height) ? "scrollY:'" + _height + "'," : "") + "  pageLength:{1},searching: false, stateSave:true, pagingType: 'input',keys:true, deferRender: true, fixedHeader: true, language: datatableOptions('{2}'), processing: true, serverSide: true, responsive: true, dom:'<\"top\"" + (_showPageLength ? "l" : "") + "B>rt<\"bottom\"fip><\"clear\">', " + (_showPageLength ? "lengthMenu: [[" + pagelengthList + "], [" + pagelengthList + "]]," : "") + " buttons: [{3}],", _isPagingEnabled ? "true" : "false", _pageLength, _emptyText, exportButton);
            sb.AppendFormat("columns: [{0}], destroy: true,", columnBuilder.ToString().TrimEnd(','));
            sb.Append("ajax: {");
            sb.AppendFormat("url: '{0}',", _ajaxUrl);
            sb.Append(@"type: 'POST',
                                 data: data,
                                 error: function(xhr) {
                                    if(xhr.responseJSON != undefined)
                                    {                                        
                                        ShowMessage(xhr.responseJSON.Message, 1, true);
                                    }
                                 },
                                 complete: function() {
                                     GridIslemler();
                                     DatatableHeadersReset();    
                                ");
            if (!_callBackFunctionName.IsBlank())
            {
                sb.AppendFormat(@"if (typeof (window['{0}']) === 'function')", _callBackFunctionName);
                sb.Append("{");
                sb.AppendFormat("window['{0}']();", _callBackFunctionName);
                sb.Append("}");
            }

            sb.Append(@"         }
                              }");
            sb.Append("}); }");

            if (_isFunctionCallInstantly && !_isDetailTable)
            {
                sb.Append(@"$(function() {");
                sb.AppendFormat("{0}();", _functionName);
                sb.Append("});");
            }
            sb.Append("</script>");

            return sb.ToString();
        }

        public IBSGrid<T> SetSearchFilterGuid(string searhFilterGuid)
        {
            _searchFilterGuid = searhFilterGuid;
            return this;
        }

        #endregion
    }

    public class BSGrid : IBSGrid, IHtmlString
    {
        #region properties
        private string _id;
        private string _title;
        private string _cssIcon;
        private bool _isHeaderVisible;
        private HtmlHelper _helper;
        private string _ajaxUrl;
        private string _functionName;
        private bool _isFunctionCallInstantly;
        private int _pageLength;

        private string _callBackFunctionName;
        private string _emptyText;
        private object _postData;
        private bool _isParentTable;
        private bool _isDetailTable;
        private string _detailTablePartialUrl;
        private bool _isPagingEnabled;
        private string _height;
        private GridSelectionType _selectionType;
        private bool _showExportButton;
        private bool _exportAllData;
        private bool _showPageLength;
        private bool _addRowNumber;

        private int _sortIndex;
        private EnumSortType _sortType;

        private List<int> _pageLengthList = new List<int>();
        private List<GridButton> _toolbarButtons = new List<GridButton>();
        private List<GridColumn> _columns = new List<GridColumn>();
        private string _searchFilterGuid;
        #endregion

        #region constructor
        public BSGrid(HtmlHelper helper, string id = "")
        {
            _id = id;
            _helper = helper;
            _isHeaderVisible = true;
            _functionName = "Listele";
            _isFunctionCallInstantly = true;
            _pageLength = 10;
            _emptyText = "";
            _postData = null;
            _isParentTable = false;
            _isDetailTable = false;
            _isPagingEnabled = true;
            _selectionType = GridSelectionType.None;
            _pageLengthList = new List<int> { 10, 25, 50 };
#if Kobi
            _showExportButton = false;
            _showPageLength=false;
#else
            _showExportButton = true;
            _showPageLength = true;
#endif
            _exportAllData = false;
            _addRowNumber = false;
            _searchFilterGuid = "";
        }
        #endregion

        #region properties fluent
        public IBSGrid SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IBSGrid SetColumns(Action<GridColumnBuilder> columnBuilder)
        {
            var builder = new GridColumnBuilder();
            columnBuilder(builder);
            foreach (var column in builder)
            {
                _columns.Add(column);
            }
            return this;
        }
        public IBSGrid SetToolbarButtons(Action<GridButtonBuilder> btnBuilder)
        {
            var builder = new GridButtonBuilder();
            btnBuilder(builder);
            foreach (var button in builder)
            {
                _toolbarButtons.Add(button);
            }
            return this;
        }
        public IBSGrid SetCssIcon(string cssIcon = "")
        {
            _cssIcon = cssIcon;
            return this;
        }
        public IBSGrid SetHeaderVisible(bool isHeaderVisible)
        {
            _isHeaderVisible = isHeaderVisible;
            return this;
        }
        public IBSGrid SetAjaxUrl(string ajaxUrl, object postData = null)
        {
            _ajaxUrl = ajaxUrl;
            _postData = postData;
            return this;
        }
        public IBSGrid SetDefaultOrder(int sortIndex, EnumSortType sortType = EnumSortType.desc)
        {
            _sortIndex = sortIndex;
            _sortType = sortType;
            return this;
        }
        public IBSGrid SetFunctionName(string functionName = "Listele")
        {
            _functionName = functionName;
            return this;
        }
        public IBSGrid SetFunctionCallInstantly(bool isFunctionCallInstantly = true)
        {
            _isFunctionCallInstantly = isFunctionCallInstantly;
            return this;
        }
        public IBSGrid SetPageLength(int pageLength = 10)
        {
            _pageLength = pageLength;
            if (!_pageLengthList.Contains(_pageLength))
                _pageLengthList.Add(_pageLength);
            return this;
        }
        public IBSGrid SetPaging(bool isPagingEnabled)
        {
            _isPagingEnabled = isPagingEnabled;
            return this;
        }
        public IBSGrid SetCallBackFunctionName(string callBackFunctionName)
        {
            _callBackFunctionName = callBackFunctionName;
            return this;
        }
        public IBSGrid SetEmptyText(string emptyText)
        {
            _emptyText = emptyText;
            return this;
        }
        public IBSGrid SetParentTable()
        {
            _isParentTable = true;
            return this;
        }
        public IBSGrid SetDetailTable()
        {
            _isDetailTable = true;
            return this;
        }
        public IBSGrid SetDetailTablePartialUrl(string detailTablePartialUrl)
        {
            _detailTablePartialUrl = detailTablePartialUrl;
            return this;
        }
        /// <summary>
        /// 200px   50vh
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public IBSGrid SetHeight(string height)
        {
            _height = height;
            return this;
        }

        public IBSGrid SetSelectionType(GridSelectionType selectionType)
        {
            _selectionType = selectionType;
            return this;
        }

        public IBSGrid SetExportButtonShow(bool showExportButton, bool exportAllData = false)
        {
            _showExportButton = showExportButton;
            _exportAllData = exportAllData;
            return this;
        }

        public IBSGrid SetPageLengthShow(bool showPageLength)
        {
            _showPageLength = showPageLength;
            return this;
        }

        public IBSGrid SetShowRowNumber(bool addRowNumber)
        {
            _addRowNumber = addRowNumber;
            return this;
        }

        #endregion

        #region return html to view
        public override string ToString()
        {
            return ToHtmlString();
        }
        public string ToHtmlString()
        {
            var sb = new StringBuilder();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

            var guid = Guid.NewGuid().ToString().Replace("-", "");
            if (_id.IsBlank())
            {
                _id = Guid.NewGuid().ToString();
            }
            else if (_isParentTable || _isDetailTable)
            {
                _id = _id + guid;
            }

            if (string.IsNullOrEmpty(_title))
                _title = "Liste";

            if (string.IsNullOrEmpty(_cssIcon))
                _cssIcon = "fa-gear";

            sb.Append("<div class='m-portlet m-portlet--brand m-portlet--head-solid-bg w-100'>");

            if (_isHeaderVisible)
            {
                sb.AppendFormat(@"
			<div class='m-portlet__head'>
				<div class='m-portlet__head-caption'>
					<div class='m-portlet__head-title'>
						<span class='m-portlet__head-icon'>
							<i class='fa {0}'></i>
						</span>
						<h3 class='m-portlet__head-text'>
							{1}
						</h3>
					</div>			
				</div>
                ", _cssIcon, _title);


                if (_toolbarButtons.Count > 0)
                {
                    sb.Append("<div class='m-portlet__head-tools'> <ul class='m-portlet__nav'>");
                    foreach (var item in _toolbarButtons)
                    {
                        sb.Append(item.ToString());
                    }
                    sb.Append("</ul> </div>");
                }

                sb.Append("</div>");
            }

            sb.Append("<div class='m-portlet__body grid_portlet__body'>");

            var detailColumn = _columns.FirstOrDefault(w => w.IsDetail);
            if (_isParentTable && detailColumn == null)
            {
                throw new Exception("Parent table olan tablolarda en az bir detail özelliği set edilmiş kolon bulunmalıdır.");
            }

            if (_isDetailTable)
                _functionName += guid;

            sb.AppendFormat("<table id='{0}' class='table table-bordered table-hover no-margin bsDataTable w-100 {1}' {2} {3} data-function-name='{4}' {5}>", _id, _isParentTable ? "parentTable" : "", _isParentTable ? "data-detailtable-partial-url='" + _detailTablePartialUrl + "'" : "", _isParentTable ? "data-detail-column=" + detailColumn.Name : "", _functionName, !string.IsNullOrEmpty(_searchFilterGuid) ? "data-searchfilterguid='" + _searchFilterGuid + "'" : "");
            sb.Append("<thead><tr>");

            var columnBuilder = new StringBuilder();


            if (detailColumn != null)
            {
                sb.AppendFormat("<th class='detail dtnotexport'></th>");

                columnBuilder.Append("{");
                columnBuilder.AppendFormat("defaultContent: '', \"orderable\" : false, \"visible\" : true");
                columnBuilder.Append("},");
            }

            if (_selectionType != GridSelectionType.None)
            {
                if (_selectionType == GridSelectionType.Multi)
                {
                    sb.Append("<th class=\"dtnotexport\">").Append($"<label for=\"{_id}_grdCheckAll\" class=\"m-checkbox m-checkbox--solid m-checkbox--success\"><input data-parent=\"{_id}\" id=\"{_id}_grdCheckAll\"  class=\"grdChkAll\" type=\"checkbox\"><span></span></label>").Append("</th>");
                    columnBuilder.Append("{orderable:false, data: function(row, type, set) {" +
                                         "var chkId=RandomIdGetir(); " +
                                         "return '" + "<label for=\"'+chkId+'\" class=\"m-checkbox m-checkbox--solid m-checkbox--success\"><input data-parent=\"" + _id + "\" id=\"'+chkId+'\" class=\"grdChk\" type=\"checkbox\"><span></span></label>';}},");
                }
                else if (_selectionType == GridSelectionType.Single)
                {
                    sb.Append("<th class=\"dtnotexport\"></th>");
                    columnBuilder.Append("{orderable:false, data: function(row, type, set) {" +
                                         "var rbId=RandomIdGetir(); " +
                                         "return '" + "<label for=\"'+rbId+'\" class=\"m-radio m-radio--solid m-radio--success\"><input name=\"" + _id + "_rb\" id=\"'+rbId+'\" class=\"grdRb\" type=\"radio\"><span></span></label>';}},");
                }
            }


            if (_addRowNumber)
            {
                sb.AppendFormat("<th class='dtRowNumber'></th>");
                columnBuilder.Append("{");
                columnBuilder.AppendFormat("defaultContent: '', \"orderable\" : false, \"visible\" : true,width:10,");
                columnBuilder.Append(@"render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; }");
                columnBuilder.Append("},");
            }

            var sortIndex = 0;
            for (var i = 0; i < _columns.Count; i++)
            {
                var column = _columns[i];
                if (column.IsSortable && sortIndex == 0)
                {
                    sortIndex = i;
                }

                var columnClass = "";

                if (!column.IsExportable)
                    columnClass += " dtnotexport ";

                if (column.IsDetail)
                    columnClass += " d-none dtnotexport ";

                sb.AppendFormat("<th {1} >{0}</th>", column.Title.IsBlank() ? column.Name : column.Title, !string.IsNullOrEmpty(columnClass) ? $"class=\"{columnClass}\"" : "");
                columnBuilder.Append("{");

                if (column.CustomDataIndex.IsBlank())
                {
                    columnBuilder.AppendFormat(" data : '{0}' ", column.Name);
                }
                else
                {
                    columnBuilder.AppendFormat(" data : '{0}' ", column.CustomDataIndex);
                }

                if (column.IsFixed)
                {

                }

                if (!column.DateFormat.IsBlank())
                {
                    columnBuilder.AppendFormat(@", render: function(d)");
                    columnBuilder.AppendLine("{");
                    columnBuilder.AppendFormat(@"if(d != null) return moment(d).format('{0}'); else return '';", column.DateFormat);
                    columnBuilder.AppendLine("}");
                }

                if (column.ColumnLength > 0)
                    columnBuilder.AppendFormat($", render: $.fn.dataTable.render.ellipsis({column.ColumnLength}, true)");


                columnBuilder.AppendFormat(", \"orderable\" : {0}", column.IsSortable ? "true" : "false");
                columnBuilder.AppendFormat(", \"visible\" : {0}", column.IsVisible && !column.IsDetail ? "true" : "false");
                if (!column.Width.IsBlank())
                {

                }

                if (!column.ClassName.IsBlank())
                    columnBuilder.AppendFormat(", className : '{0}'", column.ClassName == "grdIslemler" ? " grdIslemler dtnotexport " : column.ClassName);
                columnBuilder.Append("},");
            }

            sb.Append("</tr></thead>");
            sb.Append("<tbody></tbody>");

            sb.AppendLine("</table></div></div>");

            if (_sortIndex == 0)
                _sortIndex = sortIndex;

            var data = JsonConvert.SerializeObject(_postData);

            sb.AppendFormat("<script>function {0}(postData)", _functionName);
            sb.Append("{");

            if (_postData != null)
            {
                sb.AppendFormat("var data = {0};", data);
            }
            else
            {
                sb.Append("var data = { };");
            }

            sb.Append("if (postData != undefined && postData != null)");
            sb.Append("{");
            sb.Append("$.each(postData, function(key, value) {");
            sb.Append("data[key] = value;");
            sb.Append("});");
            sb.Append("}");
            sb.Append("setSelectionData();");
            sb.Append("data.searchFilters = $('#SearchFilters').val();");           

            var exportButton = new StringBuilder();
            if (_showExportButton)
            {
                exportButton.Append("{");
                exportButton.Append("extend: 'excel',");
                exportButton.AppendFormat("filename: '{0}',", string.IsNullOrEmpty(_title) ? "Domain" : _title);
                exportButton.Append("text:'<i class=\"fa fa-file-excel-o\"></i>',");
                exportButton.Append("titleAttr: 'Excel',");
                exportButton.Append("exportOptions: {columns: ':not(.dtnotexport)', orthogonal: 'export'},");
                exportButton.Append("className: 'btn btn-sm btn-success',");
                exportButton.Append(@"init: function( api, node, config) {
                    $(node).removeClass('dt-button')}");
                if (_exportAllData)
                    exportButton.Append(",action: newExportAction");
                exportButton.Append("}");
                exportButton.Append(",{");
                exportButton.Append("extend: 'print',");
                exportButton.Append("text:'<i class=\"fa fa-print\"></i>',");
                exportButton.Append("titleAttr: 'Yazdır',");
                exportButton.AppendFormat("messageTop: '{0}',", string.IsNullOrEmpty(_title) ? "Domain" : _title);
                exportButton.Append("messageBottom: null,");
                exportButton.Append("exportOptions: {columns: ':not(.dtnotexport)', orthogonal: 'export'},");
                exportButton.Append("className: 'btn btn-sm btn-info',");
                exportButton.Append(@"init: function( api, node, config) {
                    $(node).removeClass('dt-button')}");
                if (_exportAllData)
                    exportButton.Append(",action: newExportAction");
                exportButton.Append("}");
            }

            sb.AppendFormat("$('#{0}').dataTable(", _id);
            sb.Append("{ order: [[");
            sb.AppendFormat("{0}, \"{1}\"]],", _sortIndex, _sortType);
            var pagelengthList = string.Join(",", _pageLengthList.OrderBy(o => o).ToList());
            sb.AppendFormat("paging:{0}, " + (!string.IsNullOrEmpty(_height) ? "scrollY:'" + _height + "'," : "") + " pageLength:{1}," + (_showPageLength ? "lengthMenu: [[" + pagelengthList + "], [" + pagelengthList + "]]," : "") + " searching: false, pagingType: 'input',keys:true, deferRender: true, fixedHeader: true, language: datatableOptions('{2}'),stateSave:true, processing: true, serverSide: true, responsive: true, dom:'<\"top\"" + (_showPageLength ? "l" : "") + "B>rt<\"bottom\"fip><\"clear\">',buttons:[{3}] ,", _isPagingEnabled ? "true" : "false", _pageLength, _emptyText, exportButton);
            sb.AppendFormat("columns: [{0}], destroy: true,", columnBuilder.ToString().TrimEnd(','));
            sb.Append("ajax: {");
            sb.AppendFormat("url: '{0}',", _ajaxUrl);

            sb.Append(@"type: 'POST',
                                 data: data,
                                 error: function(xhr) {
                                    if(xhr.responseJSON != undefined)
                                    {                                        
                                        ShowMessage(xhr.responseJSON.Message, 1, true);
                                    }
                                 },
                                 complete: function() {
                                     GridIslemler();
                                     DatatableHeadersReset();    
                                 ");
            if (!_callBackFunctionName.IsBlank())
            {
                sb.AppendFormat(@"if (typeof (window['{0}']) === 'function')", _callBackFunctionName);
                sb.Append("{");
                sb.AppendFormat("window['{0}']();", _callBackFunctionName);
                sb.Append("}");
            }

            sb.Append(@"         }
                              }");
            sb.Append("}); }");

            if (_isFunctionCallInstantly && !_isDetailTable)
            {
                sb.Append(@"$(function() {");
                sb.AppendFormat("{0}();", _functionName);
                sb.Append("});");
            }
            sb.Append("</script>");

            return sb.ToString();
        }

        public IBSGrid SetSearchFilterGuid(string searhFilterGuid)
        {
            _searchFilterGuid = searhFilterGuid;
            return this;
        }

        #endregion
    }
}
