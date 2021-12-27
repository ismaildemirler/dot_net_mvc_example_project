using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Domain.WebHelpers.Infrastructre
{
    public enum MessageType
    {
        info = 0,
        warning = 1,
        error = 2,
        success = 3,
        question = 4
    }

    [Serializable]
    public class MsgButton
    {
        public string Id { get; set; }
        public bool IFrame { get; set; }
        public bool Ajax { get; set; }
        public string CssClass { get; set; }
        public string Text { get; set; }
        public string ActionUrl { get; set; }
        public string SuccessFn { get; set; }
        public string FailFn { get; set; }
        public FormMethod Method { get; set; }
        public string MethodStr => Method.ToString().ToUpper();
        public MsgButton()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            IFrame = false;
            Ajax = false;
            CssClass = "btn btn-default";
            SuccessFn = "";
            FailFn = "";
            Method = FormMethod.Post;
        }
    }

    [Serializable]
    public class Message
    {
        public string Msg { get; set; }
        public string Url { get; set; }
        public MessageType Type { get; set; }
        public bool ShowCloseButton { get; set; }
        public object Model { get; set; }
        public string ModelStr => Model == null ? "{}" : JsonConvert.SerializeObject(Model);

        public Message()
        {
            ShowCloseButton = true;
            Type = MessageType.info;
        }
        public List<MsgButton> Buttons { get; set; }
    }


}