using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Input.Button.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.FileUpload
{
    public class BSFileUploader : IBSFileUploader, IHtmlString
    {
        private bool _isMultiple;
        private String ActionUrl;

        public BSFileUploader(string urlAction)
        {
            this.ActionUrl = urlAction;
        }

        public IBSFileUploader SetMultiple(bool isMultiple)
        {
            this._isMultiple = isMultiple;
            return this;
        }

        public string ToHtmlString()
        {
            #region Form
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("method", "POST");
            form.MergeAttribute("enctype", "multipart/form-data");
            form.MergeAttribute("data-url", ActionUrl);
            form.MergeAttribute("id", "fileupload");

            #region buttonbar
            TagBuilder divInputGroup = new TagBuilder("div");
            divInputGroup.AddCssClass("row fileupload-buttonbar");

            #region inputs
            TagBuilder divInputAppend = new TagBuilder("div");
            divInputAppend.AddCssClass("col-lg-7");

            //input file
            TagBuilder spanAppend = new TagBuilder("span");
            spanAppend.AddCssClass("btn btn-success fileinput-button");

            TagBuilder iAppend = new TagBuilder("i");
            iAppend.AddCssClass("glyphicon glyphicon-plus");

            TagBuilder spanFileUploadTextAppend = new TagBuilder("span")
            {
                InnerHtml = "Dosya Ekle..."
            };

            TagBuilder inputFileAppend = new TagBuilder("input");
            inputFileAppend.MergeAttribute("type", "file");
            inputFileAppend.MergeAttribute("name", "files[]");
            if (_isMultiple)
            {
                inputFileAppend.MergeAttribute("multiple", "multiple");
            }
            spanAppend.InnerHtml += iAppend.ToString();
            spanAppend.InnerHtml += spanFileUploadTextAppend.ToString();
            spanAppend.InnerHtml += inputFileAppend.ToString();

            //Tümünü Yükle Butonu
            TagBuilder buttonUploadAllAppend = new TagBuilder("button");
            buttonUploadAllAppend.AddCssClass("btn btn-primary start");
            buttonUploadAllAppend.MergeAttribute("type", "submit");

            TagBuilder iUploadAllAppend = new TagBuilder("i");
            iUploadAllAppend.AddCssClass("glyphicon glyphicon-upload");

            TagBuilder spanUploadAllTextAppend = new TagBuilder("span")
            {
                InnerHtml = "Tümünü Yükle"
            };
            buttonUploadAllAppend.InnerHtml += iUploadAllAppend.ToString();
            buttonUploadAllAppend.InnerHtml += spanUploadAllTextAppend.ToString();

            //Tümünü İptal Et Butonu
            TagBuilder buttonCancelAllAppend = new TagBuilder("button");
            buttonCancelAllAppend.AddCssClass("btn btn-warning cancel");
            buttonCancelAllAppend.MergeAttribute("type", "reset");

            TagBuilder iCancelAllAppend = new TagBuilder("i");
            iCancelAllAppend.AddCssClass("glyphicon glyphicon-ban-circle");

            TagBuilder spanCancelAllTextAppend = new TagBuilder("span")
            {
                InnerHtml = "Tümünü İptal Et"
            };
            buttonCancelAllAppend.InnerHtml += iCancelAllAppend.ToString();
            buttonCancelAllAppend.InnerHtml += spanCancelAllTextAppend.ToString();

            //Tümünü Sil Butonu
            TagBuilder buttonDeleteAllAppend = new TagBuilder("button");
            buttonDeleteAllAppend.AddCssClass("btn btn-danger delete");
            buttonDeleteAllAppend.MergeAttribute("type", "button");

            TagBuilder iDeleteAllAppend = new TagBuilder("i");
            iDeleteAllAppend.AddCssClass("glyphicon glyphicon-trash");

            TagBuilder spanDeleteAllTextAppend = new TagBuilder("span")
            {
                InnerHtml = "Tümünü Sil"
            };
            buttonDeleteAllAppend.InnerHtml += iDeleteAllAppend.ToString();
            buttonDeleteAllAppend.InnerHtml += spanDeleteAllTextAppend.ToString();

            //checkbox
            TagBuilder inputToggleAppend = new TagBuilder("input");
            inputToggleAppend.AddCssClass("toggle");
            inputToggleAppend.MergeAttribute("type", "checkbox");

            //span process
            TagBuilder spanProcessAppend = new TagBuilder("span");
            spanProcessAppend.AddCssClass("fileupload-process");

            divInputAppend.InnerHtml += spanAppend.ToString();
            divInputAppend.InnerHtml += buttonUploadAllAppend.ToString();
            divInputAppend.InnerHtml += buttonCancelAllAppend.ToString();
            divInputAppend.InnerHtml += buttonDeleteAllAppend.ToString();
            divInputAppend.InnerHtml += inputToggleAppend.ToString();
            divInputAppend.InnerHtml += spanProcessAppend.ToString();
            #endregion

            #region progress bar
            TagBuilder divGlobalProgressAppend = new TagBuilder("div");
            divGlobalProgressAppend.AddCssClass("col-lg-5 fileupload-progress fade");

            TagBuilder divProgressBarAppend = new TagBuilder("div");
            divProgressBarAppend.AddCssClass("progress progress-striped active");
            divProgressBarAppend.MergeAttribute("role", "progressbar");
            divProgressBarAppend.MergeAttribute("aria-valuemin", "0");
            divProgressBarAppend.MergeAttribute("aria-valuemax", "100");

            TagBuilder divBarSuccessAppend = new TagBuilder("div");
            divBarSuccessAppend.AddCssClass("progress-bar progress-bar-success");
            divProgressBarAppend.InnerHtml += divBarSuccessAppend.ToString();

            TagBuilder divExtendedProgressAppend = new TagBuilder("div");
            divExtendedProgressAppend.AddCssClass("progress-extended");
            divExtendedProgressAppend.InnerHtml = "&nbsp;";

            divGlobalProgressAppend.InnerHtml += divProgressBarAppend.ToString();
            divGlobalProgressAppend.InnerHtml += divExtendedProgressAppend.ToString();
            #endregion

            divInputGroup.InnerHtml += divInputAppend.ToString();
            divInputGroup.InnerHtml += divGlobalProgressAppend.ToString();
            #endregion

            #region Progress and Presentation
            TagBuilder divProgressStateAppend = new TagBuilder("div");
            divProgressStateAppend.AddCssClass("col-lg-5 fileupload-progress fade");

            TagBuilder divProgressStripedAppend = new TagBuilder("div");
            divProgressStripedAppend.AddCssClass("progress progress-striped active");
            divProgressStripedAppend.MergeAttribute("role", "progressbar");
            divProgressStripedAppend.MergeAttribute("aria-valuemin", "0");
            divProgressStripedAppend.MergeAttribute("aria-valuemax", "100");

            TagBuilder divProgressBarSuccessAppend = new TagBuilder("div");
            divProgressBarSuccessAppend.AddCssClass("progress-bar progress-bar-success");
            divProgressStripedAppend.InnerHtml += divProgressBarSuccessAppend.ToString();

            TagBuilder divExtendedAppend = new TagBuilder("div");
            divExtendedAppend.AddCssClass("progress-extended");
            divExtendedAppend.InnerHtml = "&nbsp;";

            divProgressStateAppend.InnerHtml += divProgressStripedAppend.ToString();
            divProgressStateAppend.InnerHtml += divExtendedAppend.ToString();

            //table presentation
            TagBuilder tablePresentationAppend = new TagBuilder("table");
            tablePresentationAppend.AddCssClass("table table-striped");
            tablePresentationAppend.MergeAttribute("role", "presentation");

            TagBuilder tbodyPresentationAppend = new TagBuilder("tbody");
            tbodyPresentationAppend.AddCssClass("files");
            tablePresentationAppend.InnerHtml += tbodyPresentationAppend.ToString();
            #endregion

            #region Blueimp Gallery
            TagBuilder divBlueimpGalleryAppend = new TagBuilder("div");
            divBlueimpGalleryAppend.AddCssClass("blueimp-gallery blueimp-gallery-controls");
            divBlueimpGalleryAppend.MergeAttribute("data-filter", ":even");

            TagBuilder divSlidesAppend = new TagBuilder("div");
            divSlidesAppend.AddCssClass("slides");

            TagBuilder hTitleAppend = new TagBuilder("h3");
            hTitleAppend.AddCssClass("title");

            TagBuilder aPrevAppend = new TagBuilder("a");
            aPrevAppend.AddCssClass("prev");

            TagBuilder aNextAppend = new TagBuilder("a");
            aNextAppend.AddCssClass("next");

            TagBuilder aCloseAppend = new TagBuilder("a");
            aCloseAppend.AddCssClass("close");

            TagBuilder aPlayPauseAppend = new TagBuilder("a");
            aPlayPauseAppend.AddCssClass("play-pause");

            TagBuilder olIndicatorAppend = new TagBuilder("ol");
            olIndicatorAppend.AddCssClass("indicator");

            divBlueimpGalleryAppend.InnerHtml += divSlidesAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += hTitleAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += aPrevAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += aNextAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += aCloseAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += aPlayPauseAppend.ToString();
            divBlueimpGalleryAppend.InnerHtml += olIndicatorAppend.ToString();
            #endregion

            form.InnerHtml += divInputGroup.ToString();
            form.InnerHtml += divProgressStateAppend.ToString();
            form.InnerHtml += tablePresentationAppend.ToString();
            form.InnerHtml += divBlueimpGalleryAppend.ToString();

            form.InnerHtml += @"<script id='template-upload' type='text/x-tmpl'>
            {% for (var i = 0, file; file = o.files[i]; i++) { %}
                <tr class='template-upload fade'>
                    <td>
                    <span class='preview'></span>
                    </td>
                    <td>
                    <p class='name'>{%=file.name%}</p>
                    <strong class='error text-danger'></strong>
                    </td>
                    <td>
                    <p class='size'>Yükleniyor...</p>
                    <div class='progress progress-striped active' role='progressbar' aria-valuemin='0' aria-valuemax='100' aria-valuenow='0'><div class='progress-bar progress-bar-success' style='width:0%;'></div></div>
                    </td>
                    <td>
                {% if (!i && !o.options.autoUpload) { %}
                    <button class='btn btn-primary start' disabled>
                        <i class='glyphicon glyphicon-upload'></i>
                        <span>Yükle</span>
                        </button>
                    {% } %}
                {% if (!i) { %}
                    <button class='btn btn-warning cancel'>
                        <i class='glyphicon glyphicon-ban-circle'></i>
                        <span>İptal</span>
                        </button>
                    {% } %}
                </td>
                    </tr>
                {% } %}
            </script>";

            form.InnerHtml += @"<script id='template-download' type='text/x-tmpl'>
            {% for (var i = 0, file; file = o.files[i]; i++) { %}
                <tr class='template-download fade'>
                    <td>
                    <span class='preview'>
                {% if (file.thumbnailUrl) { %}
                    <a href='{%=file.url%}' title='{%=file.name%}' download='{%=file.name%}' data-gallery><img src='{%=file.thumbnailUrl%}' ></ a >
                    {% } %}
                </span>
                    </td>
                    <td>
                    <p class='name'>
                {% if (file.url) { %}
                    <a href='{%=file.url%}' title='{%=file.name%}' download='{%=file.name%}' {%=file.thumbnailUrl?'data-gallery':''%}>{%=file.name%}</a>
                    {% } else { %}
                    <span>{%=file.name%}</span>
                    {% } %}
                </p>
                {% if (file.error) { %}
                    <div><span class='label label-danger'>Error</span> {%=file.error%}</div>
                    {% } %}
                </td>
                    <td>
                    <span class='size'>{%=o.formatFileSize(file.size)%}</span>
                    </td>
                    <td>
                {% if (file.deleteUrl) { %}
                    <button class='btn btn-danger delete' data-type='{%=file.deleteType%}' data-url='{%=file.deleteUrl%}' {% if (file.deletewithcredentials) { %} data-xhr-fields='{'withCredentials':true}' {% } %}>
                    <i class='glyphicon glyphicon-trash'></i>
                        <span>Sil</span>
                        </button>
                        <input type = 'checkbox' name='delete' value='1' class='toggle'>
                    {% } else { %}
                    <button class='btn btn-warning cancel'>
                        <i class='glyphicon glyphicon-ban-circle'></i>
                        <span>İptal</span>
                        </button>
                    {% } %}
                </td>
                    </tr>
                {% } %}
            </script>";
            #endregion

            return form.ToString();
        }
    }
}
