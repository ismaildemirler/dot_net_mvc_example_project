using Domain.WebHelpers.HtmlHelpers.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.WebHelpers.Infrastructre
{
    public class Enums
    {
        // Bu enuma ekleme yaptığınızda AutocompleteDegerFuction enumuna da ekleme yapınız

        /// <summary>
        /// Ototamamları besleyecek servis tipi
        /// </summary>
        public enum AutocompleteFuction
        {
            [Action(ControllerName = "Common", ActionName = "GetAutoCompleteList", Parameters = "nesneTipId=1")]
            Cities

            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=3")]
            //FaaliyetTurGetir,
            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=4")]
            //FaaliyetDurumGetir,
            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=5")]
            //FaaliyetTipGetir,

            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=7")]
            //GelisSekliGetir,
            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=8")]
            //OncelikDurumGetir,
            //[Description("/NesneTip/AjaxNesneDegerGetir?nesneTipId=9")]
            //EvrakDurumGetir,

            //[Description("/Birim/AjaxAra")]
            //BirimGetir,

            //[Description("/Sehir/TumIller")]
            //TumIlleriGetir,
            //[Description("/Sehir/TumIlceler")]
            //TumIlceleriGetir,
            //[Description("/Rapor/AjaxAra")]
            //RaporGetir,
        }
        public enum AutocompleteDegerFuction
        {
            [Description("/AutoComplete/AjaxNesneDegerTekDeger?nesneTipId=1")]
            ViewGetir
        }
        public enum AutocompleteCokDegerFuction
        {
            [Description("/NesneTip/AjaxNesneDegerCokDeger?nesneTipId=1")]
            ViewGetir
        }
        public enum AutoCompleteType
        {
            /// <summary>
            /// liste şeklinde seçenekleri gösterir
            /// </summary>
            List = 0,

            /// <summary>
            /// ağaç yapısında gösterir
            /// </summary>
            Tree = 1,

            /// <summary>
            /// listeleme butonunu kaldırır
            /// </summary>
            None = 2,

            /// <summary>
            /// seçilecek veririn listeleme sayfasını açar,
            /// BU SEÇENKETE mutlaka viewURL belirtilmelidir
            /// </summary>
            CustomView = 3
        }

        public enum ListeIlkYazı
        {
            Seçiniz,
            Hepsi,
            Boş,
            Yok
        }

        public enum ListeIlkDeger
        {
            Sayı,
            Guid,
        }

        public enum ListeSirala
        {
            Normal = 1,
            TextAsc,
            TextDesc,
            ValueAsc,
            ValueDesc
        }

        public enum EnumSort
        {
            None = 0,
            Asc = 1,
            Desc = 2,
            AlfabetikAsc = 3,
            AlfabetikDesc = 4,
        }

        public enum EnumSortType
        {
            asc = 1,
            desc = 2
        }
        public enum EnumFileExtensionTypes
        {
            [EnumMember] pdf,
            [EnumMember] xls,
            [EnumMember] xlsx,
            [EnumMember] doc,
            [EnumMember] docx,
            [EnumMember] zip,
            [EnumMember] rar,
            [EnumMember] png,
            [EnumMember] jpg,
            [EnumMember] jpeg,
            [EnumMember] tiff,
            [EnumMember] tif,
        }              
    }
}
