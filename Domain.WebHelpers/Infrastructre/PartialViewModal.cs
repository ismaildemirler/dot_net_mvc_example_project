using System;
using System.ComponentModel;

namespace Domain.WebHelpers.Infrastructre
{
    [Serializable]
    public class PartialViewModal
    {
        public PartialViewModal()
        {
            Modal = false;
            ShowCloseButton = true;
            Method = "POST";
            EnumModalSize=EnumModalSize.modal_size_70;
        }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PartialViewUrl { get; set; }
        public bool ShowCloseButton { get; set; }
        public string CloseCallBackFunction { get; set; }
        public string ShowCallBackFunction { get; set; }
        public bool Modal { get; set; }
        public string LoadingText { get; set; }
        public object Model { get; set; }
        public string Method { get; set; }
        public EnumModalSize EnumModalSize { get; set; }
        public string ModalSize => EnumModalSize.ToString().Replace("_", "-");
    }

    [Serializable]
    public enum EnumModalSize
    {
        [Description("%40 Width")]
        modal_size_40,
        [Description("%50 Width")]
        modal_size_50,
        [Description("%60 Width")]
        modal_size_60,
        [Description("%70 Width")]
        modal_size_70,
        [Description("%75 Width")]
        modal_size_75,
        [Description("%80 Width")]
        modal_size_80,
        [Description("%90 Width")]
        modal_size_90
    }
}
