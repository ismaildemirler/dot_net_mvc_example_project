using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem;
using Domain.WebHelpers.HtmlHelpers.Accordion.Enum;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.Infrastructure.Utilities.Common;

namespace Domain.WebHelpers.HtmlHelpers.Accordion
{
    public class BSAccordion : IBSAccordion, IHtmlString
    {
        #region private properties

        private readonly string _id;
        private List<IBSAccordionItem> _items;
        private object _htmlAttributes;
        private EnumAccordionStyles _style;

        #endregion

        #region ctor

        public BSAccordion(string id)
        {
            _id = id;
            if (string.IsNullOrEmpty(_id))
                _id = Utilities.RandomNumber(0, 100000).ToString();
            _style = EnumAccordionStyles.Default;
            _items = new List<IBSAccordionItem>();
        }

        #endregion

        #region fluent methods
        public IBSAccordion SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }

        public IBSAccordion SetItems(List<IBSAccordionItem> items)
        {
            _items = items;
            return this;
        }

        public IBSAccordion SetStyle(EnumAccordionStyles style)
        {
            _style = style;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            var accordionContainer = new TagBuilder("div");
            accordionContainer.MergeAttribute("id", $"m_accordion_{_id}");
            accordionContainer.MergeAttribute("role", "tablist");
            accordionContainer.AddCssClass("m-accordion");

            if (_htmlAttributes != null)
            {
                var attributes = Util.MergeHtmlAttributes(_htmlAttributes, null);
                accordionContainer.MergeAttributes(attributes);
            }

            switch (_style)
            {
                case EnumAccordionStyles.Default:
                    accordionContainer.AddCssClass("m-accordion--default");
                    break;
                case EnumAccordionStyles.Bordered:
                    accordionContainer.AddCssClass("m-accordion--bordered");
                    break;
                case EnumAccordionStyles.BorderlessSolidBackground:
                    accordionContainer.AddCssClass("m-accordion--default m-accordion--solid");
                    break;
                case EnumAccordionStyles.BorderedSolidBackground:
                    accordionContainer.AddCssClass("m-accordion--bordered m-accordion--solid");
                    break;
                default:
                    accordionContainer.AddCssClass("m-accordion--default");
                    break;
            }

            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                item.SetParentId($"m-accordion_{_id}", $"m-accordion_{_id}_{i}");
                accordionContainer.InnerHtml += item.ToHtmlString();
            }
            var sbJs = new StringBuilder();

            sbJs.Append(@"<script>
                        $(function () {
                            $('#m_accordion_" + _id + @" .m-accordion__item-body').each(function(i) {
                                if ($(this).hasClass('show'))
                                    GetAjaxAccordionPartialData($(this));
                            });

                            $('#m_accordion_" + _id + @" .m-accordion__item-body').on('shown.bs.collapse',function() {
                                GetAjaxAccordionPartialData($(this));
                            });
                        });
                        </script>");

            accordionContainer.InnerHtml += sbJs.ToString();
            return accordionContainer.ToString();
        }
    }
}
