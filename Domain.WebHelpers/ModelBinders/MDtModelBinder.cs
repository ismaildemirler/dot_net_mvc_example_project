using Domain.WebHelpers.Models.Shared;
using System;
using System.Web.Mvc;

namespace Domain.WebHelpers.ModelBinders
{
    public class MDtModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            // Retrieve request data

            var page = Convert.ToInt32(request["pagination[page]"]);
            var pages = Convert.ToInt32(request["pagination[pages]"]);
            var perpage = Convert.ToInt32(request["pagination[perpage]"]);
            var total = Convert.ToInt32(request["pagination[total]"]);
            var query = request["query"];

            //// Search
            //var search = new DtSearch
            //{
            //    Value = request["search[value]"],
            //    Regex = Convert.ToBoolean(request["search[regex]"])
            //};

            //// Order
            //var o = 0;
            //var order = new List<DtOrder>();
            //while (request["order[" + o + "][column]"] != null)
            //{
            //    order.Add(new DtOrder()
            //    {
            //        Column = Convert.ToInt32(request["order[" + o + "][column]"]),
            //        Dir = request["order[" + o + "][dir]"]
            //    });
            //    o++;
            //}

            return new MDtParameterModel
            {
                field = request["sort[field]"],
                sort = request["sort[sort]"],
                page = page,
                pages = pages,
                perpage = perpage,
                total = total
            };
        }
    }
}
