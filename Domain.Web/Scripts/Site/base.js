
//Utilities
//string contains metodu
if (!('contains' in String.prototype)) {
    String.prototype.contains = function (str, startIndex) {
        return -1 !== String.prototype.indexOf.call(this, str, startIndex);
    };
}
//object serializer
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a,
        function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {""
                o[this.name] = this.value || '';
            }
        });
    return o;
};

function isChecked(controlId) {
    return $('#' + controlId).is(":checked");
}
//Türkçe upper case
$.fn.trUpperCase = function () {
    this.each(function () {
        var inlineText = $(this).text();
        inlineText = inlineText.replace(new RegExp('ğ', 'g'), 'Ğ');
        inlineText = inlineText.replace(new RegExp('ü', 'g'), 'Ü');
        inlineText = inlineText.replace(new RegExp('ş', 'g'), 'Ş');
        inlineText = inlineText.replace(new RegExp('i', 'g'), 'İ');
        inlineText = inlineText.replace(new RegExp('ö', 'g'), 'Ö');
        inlineText = inlineText.replace(new RegExp('ç', 'g'), 'Ç');
        inlineText = inlineText.replace(new RegExp('ı', 'g'), 'I');
        inlineText = inlineText.toUpperCase();
        $(this).text(inlineText);
    });
};
function SetElementValueById(id, value) {
    $("#" + id.replace("#", "")).val(value);
}
function GetElementValueById(id) {
    return $("#" + id.replace("#", "")).val();
}
function QueryStringDegerGetir(href, parametreAdi) {
    var match = RegExp('[?&]' + parametreAdi + '=([^&]*)').exec(href);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}
function SetAjaxAccordionInnerHeightforDropDown() {
    $('.ajaxAccordion').on('shown.bs.dropdown', function (e) {

        var $table = $(this),
            $menu = $(e.target).find('.dropdown-menu'),
            tableOffsetHeight = $table.offset().top + $table.height(),
            menuOffsetHeight = $menu.offset().top + $menu.outerHeight(true);

        if (menuOffsetHeight > tableOffsetHeight)
            $table.css("padding-bottom", menuOffsetHeight - tableOffsetHeight);
    });

    $('.ajaxAccordion').on('hide.bs.dropdown', function () {
        $(this).css("padding-bottom", 0);
    });
}

function ReplaceIllegalCharacters(string) {
    var illegalCharacters = { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': '&quot;', "'": '&#39;', "/": '&#x2F;' };
    return String(string).replace(/[&<>"'\/]/g, function (s) {
        return illegalCharacters[s];
    });
}

function EraseIllegalCharacters(string) {
    return String(string).replace(/[&<>"'\/]/g, '');
}

function HasIllegalCharacters(string) {
    var illegalCharacters = /[&<>"'\/]/g;
    return string.match(illegalCharacters);
}

function split(val) {
    return val.split(/,\s*/);
}

function extractLast(term) {
    return split(term).pop();
}

function formatDate(inputDate) {
    var date = new Date(inputDate);
    if (!isNaN(date.getTime())) {
        // Months use 0 index.
        return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
    }
}

function getDateFilter(inputDate) {
    var d = new Date(inputDate.split(".").reverse().join("-"));
    var dd = d.getDate();
    if (dd < 10) {
        dd = "0" + dd;
    }
    var mm = d.getMonth() + 1;
    if (mm < 10) {
        mm = "0" + mm;
    }
    var yy = d.getFullYear();
    return dd + "/" + mm + "/" + yy;
}

function getDatetimePickerMinMaxDate(inputDate) {
    var d = new Date(inputDate.split(".").reverse().join("-"));
    var dd = d.getDate();
    if (dd < 10) {
        dd = "0" + dd;
    }
    var mm = d.getMonth() + 1;
    if (mm < 10) {
        mm = "0" + mm;
    }
    var yy = d.getFullYear();
    return yy + "-" + mm + "-" + dd;
}

function getDatetimePickerMinMaxDateYearMonth(inputDate) {
    var d = new Date(inputDate.split("/").reverse().join("-"));
    var dd = d.getDate();
    if (dd < 10) {
        dd = "0" + dd;
    }
    var mm = d.getMonth() + 1;
    if (mm < 10) {
        mm = "0" + mm;
    }
    var yy = d.getFullYear();
    return yy + "-" + mm + "-" + dd;
}

function getData(actionURL, actionData, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL)) {
        $.ajax({
            url: actionURL, data: actionData, type: "GET",
            beforeSend: function () {
                blockUI();
            },
            success: function (data) {
                if (data != null && data) {
                    if (data.Result != undefined) {
                        if (data.Result == 1) {
                            if (!wsLib.isNullOrEmpty(data.Message)) { showSuccessNotification(message, ""); }
                            if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                        }
                        else {
                            if (!wsLib.isNullOrEmpty(data.Message)) { ShowMessageSwal({ message: data.Message, type: 2, showCloseButton: true }); }

                            if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                        }
                    } else {
                        if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                    }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                        showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    }
                }
            },
            error: function (jqXHR, textStatus) {
                var msg = jQuery.parseJSON(jqXHR.responseText);
                ShowMessage(msg.Message, 2, true);
            },
            complete: function () {
                unblockUI();
            }
        });
    }
}


function postDataWithConfirm(actionURL, actionData, showMessage, customMessage, successCallback, errorCallback, confirmMessage, confirmPressNoCallback) {

    if (!wsLib.isNullOrEmpty(actionURL)) {

        var message = "Devam etmek istiyor musunuz?";
        if (!wsLib.isNullOrEmpty(confirmMessage)) {
            message = confirmMessage;
        }

        swal({
            title: "Uyarı",
            html: message,
            type: "question",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Evet, devam et",
            cancelButtonText: "Hayır, iptal et",
            width: '500px'
        }).then(function (isConfirm) {
            if (isConfirm.value == true) {
                postData(actionURL, actionData, showMessage, customMessage, successCallback, errorCallback);
            }
            else {
                if (confirmPressNoCallback && typeof (confirmPressNoCallback) === "function") { confirmPressNoCallback(); }
            }
        });
    }
}

function postData(actionURL, actionData, showMessage, customMessage, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL)) {
        var message = "Kayıt işlemi başarı ile gerçekleşti.";
        if (!wsLib.isNullOrEmpty(customMessage)) { message = customMessage; }
        $.ajax({
            url: actionURL, data: actionData, type: "POST",
            beforeSend: function () {
                blockUI();
            },
            success: function (data) {
                if (data != null && data) {
                    if (data.Result != undefined) {
                        if (data.Result == 1) {
                            if (showMessage) { showSuccessNotification(message, ""); }
                            if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                        }
                        else if (data.Result == 0) {
                            if (errorCallback && typeof (errorCallback) === "function") { errorCallback(data); } else {
                                if (wsLib.isNullOrEmpty(data.Message)) {
                                    showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                                }
                                else {
                                    ShowMessageSwal({ message: data.Message, type: 1, showCloseButton: false });
                                }
                            }
                        }
                        else {

                            showErrorNotification(data.Message);
                        }
                    } else {
                        if (showMessage) { showSuccessNotification(message, ""); }
                        if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                    }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                        showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    }
                }
            },
            error: function (jqXHR, textStatus) {

                var msg = jQuery.parseJSON(jqXHR.responseText);

                if (msg.Message != undefined && msg.Message != "") {
                    if (msg.State == 1) {
                        ShowMessage(msg.Message, 3, true);
                    }
                    else {
                        ShowMessage(msg.Message, 1, true);
                    }
                } else {
                    ShowMessage(msg.Message, 2, true);
                }
            },
            complete: function () {
                unblockUI();
            }
        });
    }
}
function postFormData(actionURL, formElementID, showMessage, customMessage, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL) && !wsLib.isNullOrEmpty(formElementID)) {
        var requestData = $("#" + formElementID).serializeArray();
        var message = "Kayıt işlemi başarı ile gerçekleşti.";
        if (wsLib.isNullOrEmpty(customMessage)) { message = customMessage; }
        $.ajax({
            url: actionURL, data: requestData, type: "POST",
            beforeSend: function () {
                blockUI();
            },
            success: function (data) {
                if (data != null && data) {
                    if (showMessage) { showSuccessNotification(message, ""); }
                    if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                } else {
                    showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                }
            },
            error: function (jqXHR, textStatus) {
                var msg = jQuery.parseJSON(jqXHR.responseText);

                if (msg.Message != undefined && msg.Message != "") {
                    if (msg.State == 1) {
                        ShowMessage(msg.Message, 3, true);
                    }
                    else {
                        ShowMessage(msg.Message, 1, true);
                    }
                } else {
                    ShowMessage(msg.Message, 2, true);
                }
            },
            complete: function () {
                unblockUI();
            }
        });
    }
}

function getPartialView(actionURL, actionData, targetElementID, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL) && !wsLib.isNullOrEmpty(targetElementID)) {
        $.ajax({
            url: actionURL,
            data: actionData,
            type: "GET",
            beforeSend: function () {
                blockUI();
            },
            success: function (data) {
                if (data != null && data) {
                    $("#" + targetElementID).html(data);
                    if (successCallback && typeof (successCallback) === "function") {
                        successCallback(data);
                    }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                        showErrorNotification("İçerik yüklenirken bir hata oluştu.", "Hata");
                    }
                }
            },
            error: function (jqXHR, textStatus) {
                var msg = jQuery.parseJSON(jqXHR.responseText);

                if (msg.Message != undefined && msg.Message != "") {
                    if (msg.State == 1) {
                        ShowMessage(msg.Message, 3, true);
                    }
                    else {
                        ShowMessage(msg.Message, 1, true);
                    }
                } else {
                    ShowMessage(msg.Message, 2, true);
                }
            },
            complete: function () {
                ActivateInputProperties();
                unblockUI();
            }
        });
    }
}

function getPartialViewWithModal(actionURL, actionData, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL)) {
        $.ajax({
            url: actionURL, data: actionData, type: "GET",
            success: function (data) {
                if (data != null && data) {
                    $("#modalLayout .modal-body").html(data);
                    $('#modalLayout').modal('show');
                    if (successCallback && typeof (successCallback) === "function") {
                        successCallback();
                    }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                        showErrorNotification("İçerik yüklenirken bir hata oluştu.", "Hata");
                    }
                }
            }
        });
    }
}
function postPartialView(actionURL, formElementID, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL) && !wsLib.isNullOrEmpty(formElementID)) {
        var requestData = $("#" + formElementID).serializeArray();
        $.ajax({
            url: actionURL, data: requestData, type: "POST",
            success: function (data) {
                if (data != null && data) {
                    showSuccessNotification("Kayıt işlemi başarı ile gerçekleşti.", "");
                    if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                } else {
                    showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                }
            }
        });
    }
}
function postPartialViewWithGivenData(actionURL, requestData, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL)) {
        $.ajax({
            url: actionURL, data: requestData, type: "POST",
            success: function (data) {
                if (data != null && data) {
                    if (successCallback && typeof (successCallback) === "function") { successCallback(data); } else { showSuccessNotification("İşlem başarı ile gerçekleşti.", ""); }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else { showErrorNotification("İşlem gerçekleştirilemedi.", "Hata"); }
                }
            }
        });
    }
}
function postAndGetPartialView(actionURL, formElementID, targetElementID, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL) && !wsLib.isNullOrEmpty(sourceElementID)) {
        var requestData = $("#" + formElementID).serializeArray();
        $.ajax({
            url: actionURL, data: requestData, type: "POST",
            success: function (data) {
                if (data != null && data) {
                    $("#" + targetElementID).html(data);
                    showSuccessNotification("Kayıt işlemi başarı ile gerçekleşti.", "");
                    if (successCallback && typeof (successCallback) === "function") { successCallback(); }
                } else {
                    showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                }
            }
        });
    }
}

function postMethodGetPartialView(actionURL, requestData, targetElementID, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL) && !wsLib.isNullOrEmpty(targetElementID)) {
        $.ajax({
            url: actionURL, data: requestData, type: "POST",
            success: function (data) {
                if (data != null && data) {
                    $("#" + targetElementID).html(data);

                    if (successCallback && typeof (successCallback) === "function") { successCallback(); }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                }
            }
        });
    }
}

function postPartialViewWithGivenDataFile(actionURL, requestData, successCallback, errorCallback) {
    if (!wsLib.isNullOrEmpty(actionURL)) {
        $.ajax({
            url: actionURL, data: requestData, type: "POST", contentType: false, processData: false,
            success: function (data) {
                if (data != null && data) {
                    showSuccessNotification("Kayıt işlemi başarı ile gerçekleşti.", "");
                    if (successCallback && typeof (successCallback) === "function") { successCallback(data); }
                } else {
                    showErrorNotification("Kayıt işlemi gerçekleştirilemedi.", "Hata");
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); }
                }
            }
        });
    }
}

function JSONDatetimeToFormattedDate(jsonDate) {
    if (jsonDate == null || jsonDate == "")
        return "";
    var dateString = jsonDate.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "." + month + "." + year;
    return date;
}

function JSONDatetimeToFormattedDateForParse(jsonDate) {
    if (jsonDate == null || jsonDate == "")
        return "";
    var dateString = jsonDate.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = month + "/" + day + "/" + year;
    return date;
}


function JSONDatetimeToNormalDateForPicker(jsonDate) {
    if (jsonDate == null || jsonDate == "")
        return "";
    var dateString = jsonDate.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    if (month < 10)
        month = "0" + month;
    var day = currentTime.getDate();
    if (day < 10)
        day = "0" + day;
    var year = currentTime.getFullYear();

    var date = day + "." + month + "." + year;
    return date;
}

function checkNumberFieldLength(elem, length) {
    if (elem.value.length > length) {
        elem.value = elem.value.slice(0, length);
    }
}

function RandomIdGetir() { return Math.random().toString(36).substring(7); }

function initCustomValidation() {

    function RequireChangeEvent(elm, targetElmID) {
        var tmpVal = getFormVal($(elm));
        $.each($("[data-custom-val-required-source='" + targetElmID + "']"), function (j, subElm) {
            var valelm = $(subElm);
            $(valelm).closest("form").validate();
            if (valelm != undefined) {
                if (tmpVal == String(valelm.data("custom-val-required-condition"))) {
                    $(valelm).rules('add', {
                        required: true,
                        messages: {
                            required: valelm.parent().parent().find("label").text() + " alanı gereklidir."
                        }
                    });

                    $(valelm).attr("data-val", true);
                    $(valelm).attr("data-val-required", valelm.parent().parent().find("label").text() + " alanı gereklidir.");

                    $(valelm).parent().find("label.error").remove();
                }
                else {
                    try {
                        $(valelm).rules('remove', 'required');
                        $(valelm).removeAttr("data-val");
                        $(valelm).removeAttr("data-val-required");
                    } catch (e) {

                    }

                    if (!($(valelm).is(":checkbox"))) {
                        $(valelm).valid();
                        $(valelm).parent().parent().find("span.field-validation-error").remove();
                        $(valelm).parent().find("label.error").remove();
                    }
                }
            }
        });
    }
    $.each($('[data-custom-val-required-source]'), function (i, elm) {
        var targetElmID = $(elm).data("custom-val-required-source");
        if (!wsLib.isNullOrEmpty(targetElmID)) {
            if ($("[name=" + targetElmID + "]").attr("type") == "radio") {
                $("[name=" + targetElmID + "]").change(function () {
                    RequireChangeEvent($(this), targetElmID);
                });
                $("[name=" + targetElmID + "]").trigger('change');
            }
            else {
                $("#" + targetElmID).change(function () {
                    RequireChangeEvent($(this), targetElmID);
                });
                $("#" + targetElmID).trigger('change');
            }
        }
    });

    function RequireExpressionChangeEvent(elm, targetElmID) {
        var tmpVal = getFormVal($(elm));

        $.each($("[data-custom-val-required-source-exp='" + targetElmID + "']"), function (j, subElm) {
            var valelm = $(subElm);
            $(valelm).closest("form").validate();
            if (valelm != undefined) {
                var condition = valelm.data("custom-val-required-expression");
                if (!wsLib.isNullOrEmpty(tmpVal)) {
                    condition = condition.replace(/Source/g, tmpVal);
                    if (eval(condition)) {
                        $(valelm).rules('add', {
                            required: true,
                            messages: {
                                required: valelm.parent().parent().find("label").text() + " alanı gereklidir."
                            }
                        });
                        $(valelm).attr("data-val", true);
                        $(valelm).attr("data-val-required", valelm.parent().parent().find("label").text() + " alanı gereklidir.");

                        $(valelm).parent().find("label.error").remove();
                    }
                    else {
                        try {
                            $(valelm).rules('remove', 'required');
                            $(valelm).removeAttr("data-val");
                            $(valelm).removeAttr("data-val-required");
                        } catch (e) {

                        }

                        if (!($(valelm).is(":checkbox"))) {
                            $(valelm).valid();
                            $(valelm).parent().parent().find("span.field-validation-error").remove();
                            $(valelm).parent().find("label.error").remove();
                        }
                    }
                }
                else {
                    try {
                        $(valelm).rules('remove', 'required');
                        $(valelm).removeAttr("data-val");
                        $(valelm).removeAttr("data-val-required");
                    } catch (e) {

                    }

                    if (!($(valelm).is(":checkbox"))) {
                        $(valelm).valid();
                        $(valelm).parent().parent().find("span.field-validation-error").remove();
                        $(valelm).parent().find("label.error").remove();
                    }
                }
            }
        });
    }
    $.each($('[data-custom-val-required-source-exp]'), function (i, elm) {
        var targetElmID = $(elm).data("custom-val-required-source-exp");
        if (!wsLib.isNullOrEmpty(targetElmID)) {
            if ($("[name=" + targetElmID + "]").attr("type") == "radio") {
                $("[name=" + targetElmID + "]").change(function () {
                    RequireExpressionChangeEvent($(this), targetElmID);
                });
                $("[name=" + targetElmID + "]").trigger('change');
            }
            else {
                $("#" + targetElmID).change(function () {
                    RequireExpressionChangeEvent($(this), targetElmID);
                });
                $("#" + targetElmID).trigger('change');
            }
        }
    });

    function DisplayChangeEvent(elm, targetElmID) {
        var tmpVal = getFormVal($(elm));
        $.each($("[data-display-source='" + targetElmID + "']"), function (j, subElm) {
            var valelm = $(subElm);
            if (valelm != undefined) {
                if (tmpVal == String(valelm.data("display-condition"))) {
                    valelm.removeClass("d-none");
                }
                else {
                    valelm.addClass("d-none");
                    if ($(valelm).find("input[type=radio]").length == 0) {
                        if ($(valelm).find("input[type=checkbox]").length == 0) {
                            valelm.find("input,select").val("");
                            valelm.find("select").trigger("change");
                        }
                        else {
                            valelm.find("input").val(false);
                        }
                    }
                    else {
                        var radio = $(valelm).find("input:checked");
                        $(radio).prop("checked", false);

                        DisplayExpressionChangeEvent(radio, $(radio).attr("name"));
                    }
                }
            }
        });
    }
    $.each($('[data-display-source]'), function (i, elm) {
        var targetElmID = $(elm).data("display-source");
        if (!wsLib.isNullOrEmpty(targetElmID)) {
            if ($("[name=" + targetElmID + "]").attr("type") == "radio") {
                $("[name=" + targetElmID + "]").change(function () {
                    DisplayChangeEvent($(this), targetElmID);
                });
                $("[name=" + targetElmID + "]").trigger('change');
            }
            else {
                $("#" + targetElmID).change(function () {
                    DisplayChangeEvent($(this), targetElmID);
                });
                $("#" + targetElmID).trigger('change');
            }
        }
    });

    function DisplayExpressionChangeEvent(elm, targetElmID) {
        var tmpVal = getFormVal($(elm));
        $.each($("[data-display-source-exp='" + targetElmID + "']"), function (j, subElm) {
            var valelm = $(subElm);
            if (valelm != undefined) {
                var condition = valelm.data("display-expression");
                if (!wsLib.isNullOrEmpty(tmpVal)) {
                    condition = condition.replace(/Source/g, tmpVal);
                    if (eval(condition)) {
                        valelm.removeClass("d-none");
                    }
                    else {
                        valelm.addClass("d-none");

                        if ($(valelm).find("input[type=radio]").length == 0) {
                            if ($(valelm).find("input[type=checkbox]").length == 0) {
                                valelm.find("input,select").val("");
                                valelm.find("select").trigger("change");
                            }
                            else {
                                valelm.find("input").val(false);
                            }
                        } else {
                            var radio = $(valelm).find("input:checked");
                            $(radio).prop("checked", false);

                            DisplayExpressionChangeEvent(radio, $(radio).attr("name"));
                        }
                    }
                }
                else {
                    valelm.addClass("d-none");
                    if ($(valelm).find("input[type=radio]").length == 0) {
                        if ($(valelm).find("input[type=checkbox]").length == 0) {
                            valelm.find("input").val("");
                        }
                        else {
                            valelm.find("input").val(false);
                        }
                    }
                    else {
                        var radio = $(valelm).find("input:checked");
                        $(radio).prop("checked", false);

                        DisplayExpressionChangeEvent(radio, $(radio).attr("name"));
                    }
                }
            }
        });
    }
    $.each($('[data-display-source-exp]'), function (i, elm) {
        var targetElmID = $(elm).data("display-source-exp");
        if (!wsLib.isNullOrEmpty(targetElmID)) {
            if ($("[name=" + targetElmID + "]").attr("type") == "radio") {
                $("[name=" + targetElmID + "]").change(function () {
                    DisplayExpressionChangeEvent($(this), targetElmID);
                });
                $("[name=" + targetElmID + "]").trigger('change');
            }
            else {
                $("#" + targetElmID).change(function () {
                    DisplayExpressionChangeEvent($(this), targetElmID);
                });
                $("#" + targetElmID).trigger('change');
            }
        }
    });
}

function getFormVal(formElement) {
    var result = "";

    if (formElement.is("[type=radio]")) {
        result = $("[name=" + formElement.attr("name") + "]:checked").val();
    }
    else if (formElement.is(":checkbox")) {
        if (formElement.prop("checked")) {
            result = "true";
        }
        else {
            result = "false";
        }
    }
    else {
        result = formElement.val();
    }
    return result;
}

wsLib = function () {
    function isStorageSupport() {
        try {
            return 'localStorage' in window && window['localStorage'] !== null;
        } catch (e) { return false; }
    }
    function setSession(myKey, myValue) { window.sessionStorage.setItem(myKey, myValue); }
    function getSession(myKey) { return window.sessionStorage.getItem(myKey); }
    function clearSession() { window.sessionStorage.clear(); }
    function keySession(myKey) { return window.sessionStorage.key(myKey); }
    function removeSession(myKey) { window.sessionStorage.removeItem(myKey); }
    function lengthSession() { return window.sessionStorage.length; }

    function setLocal(myKey, myValue) { window.localStorage.setItem(myKey, myValue); }
    function getLocal(myKey) { return window.localStorage.getItem(myKey); }
    function clearLocal() { window.localStorage.clear(); }
    function keyLocal(myKey) { return window.localStorage.key(myKey); }
    function removeLocal(myKey) { window.localStorage.removeItem(myKey); }
    function lengthLocal() { return window.localStorage.length; }

    function el(element) { return document.querySelector(element); }
    function trim(str) { return str.replace(/\s/g, ''); }
    function log(myLog) { console.log(myLog); }
    function isNullOrEmpty(value) {
        if (value == "" || value == null || value == undefined)
            return true;
        else
            return false;

    }

    return {
        sp: isStorageSupport,
        setSession: setSession,
        getSession: getSession,
        clearSession: clearSession,
        keySession: keySession,
        removeSession: removeSession,
        lengthSession: lengthSession,
        setLocal: setLocal,
        getLocal: getLocal,
        clearLocal: clearLocal,
        keyLocal: keyLocal,
        removeLocal: removeLocal,
        lengthLocal: lengthLocal,
        el: el,
        trim: trim,
        log: log,
        isNullOrEmpty: isNullOrEmpty
    }
}();
//Utilities

function DatatableHeaderEscapeRegex(sVal) {
    var letters = { "Ý": "İ", "þ": "ş", "ý": "ı" };
    return sVal.replace(/(([Ýþý]))/g, function (letter) { return letters[letter]; });
};

function DatatableHeadersReset() {
    $.each($("th:not(.dtnotexport)"), function (index, element) {
        $(element).text(DatatableHeaderEscapeRegex($(element).text()));
    });
}

//DataTable
function datatableOptions(emptyText) {
    var dataTableLanguage = {
        "sDecimal": ",",
        "sEmptyTable": emptyText,
        "sInfo": "Toplam: <b>_TOTAL_</b> kayıt | _START_ - _END_ arası gösteriliyor",
        "sInfoEmpty": emptyText,
        "sInfoFiltered": "(_MAX_ kayit içerisinden bulunan)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "Sayfada _MENU_ kayıt göster",
        "sLoadingRecords": "Yükleniyor...",
        "sProcessing": "<div class='m-blockui'><span>Veri Yükleniyor...</span><span><div class='m-loader  m-loader--success m-loader--lg'></div></span></div>",
        "sSearch": "",
        "sZeroRecords": "Eşleşen kayıt bulunamadı",
        searchPlaceholder: "Hızlı Ara",
        "oPaginate": {
            "sFirst": "İlk",
            "sLast": "Son",
            "sNext": "Sonraki",
            "sPrevious": "Önceki"
        },
        "oAria": {
            "sSortAscending": ": artan sütun sıralamasını aktifleştir",
            "sSortDescending": ": azalan sütun soralamasını aktifleştir"
        },
        select: {
            rows: {
                _: "Bu sayfada <b>%d</b> kayıt seçildi",
                0: " ",
                1: "Bu sayfada 1 kayıt seçildi"
            }
        }
    };
    return dataTableLanguage;
}
//DataTable


//Mesaj Göster
var msgTitleList = ["Bilgi", "Uyarı", "Hata Oluştu", "İşlem Başarılı", "Uyarı"];
var msgIconList = ["fa fa-info-circle", "fa fa-exclamation-triangle", "fa fa-times-circle", "fa fa-check-circle"];
var msgTitleBgList = ["alert-info", "alert-warning", "alert-danger", "alert-success"];
var msgBtnBgList = ["btn btn-info", "btn btn-warning", "btn btn-danger", "btn btn-success", "btn btn-secondary"];

function MesajButtonQueryOlustur(valueList) {

    var query = "";
    var chr = "";
    for (var i = 0; i < valueList.length; i++) {
        var value = valueList[i];
        if (i === 0)
            chr = "?";
        else
            chr = "&";
        query += chr + value;
    }
    return query;
}

function ShowMessage(msg, tip, kapatGoster) {
    //ShowMessageCustom([
    //    {
    //        "Msg": msg,
    //        "Type": tip,
    //        "KapatGoster": kapatGoster,
    //        "Buttons": [

    //            {
    //                "Id": RandomIdGetir(),
    //                "IFrame": false,
    //                "CssClass": msgBtnBgList[tip],
    //                "Text": "Tamam",
    //                "ActionUrl": null
    //            }
    //        ]
    //    }
    //]);
    var msgObject = {
        message: msg,
        type: tip,
        showCloseButton: kapatGoster
    };

    ShowMessageSwal(msgObject);
}

function ShowMessageSwal(msgObject) {

    var msg = msgObject.message;
    var type = msgObject.type;
    var showCloseButton = msgObject.showCloseButton;

    var msgType = "info";
    switch (type) {
        case 0: msgType = "info"; break;
        case 1: msgType = "warning"; break;
        case 2: msgType = "error"; break;
        case 3: msgType = "success"; break;
        case 4: msgType = "question"; break;
        default:
    }

    swal({
        title: msgTitleList[type],
        text: msg,
        type: msgType,
        confirmButtonText: 'Tamam',
        cancelButtonText: 'İptal',
        allowEscapeKey: showCloseButton,
        allowOutsideClick: showCloseButton,
        confirmButtonClass: msgBtnBgList[type],
        buttonsStyling: false,
        width: '500px',
        html: msg
    });
}

function ShowMessageCustom(msgList) {
    for (var i = 0; i < msgList.length; i++) {
        var msg = msgList[i];
        var kapatGoster = true;
        if (msg.KapatGoster != undefined)
            kapatGoster = msg.ShowCloseButton;

        var msgIndex = msg.Type;
        var modalName = RandomIdGetir();

        var sb = '';
        sb += '<div id="' + modalName + '" class="modal fade mesajModal" role="dialog" aria-hidden="true">';
        sb += '<div class="modal-dialog modal-dialog-centered" role="document">';
        sb += '<div class="modal-content">';
        sb += '  <div class="modal-header ' + msgTitleBgList[msgIndex] + '">';
        sb += '  <h5 class="modal-title"><i class="' + msgIconList[msgIndex] + '"></i> ' + msgTitleList[msgIndex] + '</h5>';
        if (kapatGoster)
            sb += '<button type="button" class="close" data-dismiss="modal" aria-label="Kapat"><span aria-hidden="true">&times;</span></button>';
        sb += '  </div>';
        sb += '  <div class="modal-body" id="modalBody">' + msg.Msg + '</div>';
        sb += '<div class="modal-footer">';
        for (var j = 0; j < msg.Buttons.length; j++) {
            var btn = msg.Buttons[j];
            sb += '<button type=\"button\" data-actionurl="' + btn.ActionUrl + '" data-iframe="' + btn.IFrame + '" class=\"' + btn.CssClass + '\" id="' + btn.Id + '" data-dismiss=\"modal\">' + btn.Text + '</button>';
        }
        sb += '</div>';
        sb += '</div>';
        sb += '</div>';
        sb += '</div>';

        $("#dialogContainer").append(sb);
        var obj = $("#" + modalName + "");

        obj.find('button.close').css({ 'color': obj.find('h4').css('color') });

        obj.modal({ backdrop: 'static' });
        for (var j = 0; j < msg.Buttons.length; j++) {
            var btnAction = msg.Buttons[j];
            if (!(btnAction.ActionUrl == undefined || btnAction.ActionUrl == '')) {
                $('#' + btnAction.Id + '').on('click', function () {
                    if ($(this).data('iframe'))
                        window.parent.location.href = $(this).data("actionurl");
                    else
                        window.location.href = $(this).data("actionurl");
                });
            }
        }
    }
}

function ShowMessageCustomSwal(msgStr) {

    var msg = JSON.parse(msgStr);
    var showCloseButton = true;
    if (msg.ShowCloseButton != undefined)
        showCloseButton = msg.ShowCloseButton;


    var msgType = "info";
    switch (msg.Type) {
        case 0: msgType = "info"; break;
        case 1: msgType = "warning"; break;
        case 2: msgType = "error"; break;
        case 3: msgType = "success"; break;
        case 4: msgType = "question"; break;
        default:
    }

    var buttons = $('<div>');
    var btn;
    var modelKey = RandomIdGetir();
    for (var j = 0; j < msg.Buttons.length; j++) {
        btn = msg.Buttons[j];
        var btnStr = '<button type="button" class="' + btn.CssClass + '" id="' + btn.Id + '"';
        if (!(btn.ActionUrl == undefined || btn.ActionUrl == '')) {
            btnStr += ' data-actionurl="' + btn.ActionUrl + '"';
        }
        if (!(btn.SuccessFn == undefined || btn.SuccessFn == '')) {
            btnStr += ' data-successfn="' + btn.SuccessFn + '"';
        }
        if (!(btn.FailFn == undefined || btn.FailFn == '')) {
            btnStr += ' data-failfn="' + btn.FailFn + '"';
        }
        if (!(btn.Ajax == undefined || btn.Ajax == '')) {
            btnStr += ' data-ajax="' + btn.Ajax + '"';
        }
        if (!(btn.IFrame == undefined || btn.IFrame == '')) {
            btnStr += ' data-iframe="' + btn.IFrame + '"';
        }
        if (!(msg.ModelStr == undefined || msg.ModelStr == '')) {
            btnStr += ' data-modelkey="' + modelKey + '"';
            wsLib.setSession(modelKey, msg.ModelStr);
        }
        if (!(btn.MethodStr == undefined || btn.MethodStr == '')) {
            btnStr += ' data-method="' + btn.MethodStr + '"';
        }
        btnStr += '>' + btn.Text + '</button>';
        buttons.append($(btnStr));
    }

    swal({
        title: msg.Msg,
        type: msgType,
        showConfirmButton: false,
        showCancelButton: false,
        allowEscapeKey: showCloseButton,
        allowOutsideClick: showCloseButton,
        buttonsStyling: false,
        width: '500px',
        html: buttons
    });

    for (var j = 0; j < msg.Buttons.length; j++) {
        var btnAction = msg.Buttons[j];

        $('#' + btnAction.Id + '').on('click', function () {
            if (!($(this).data('actionurl') == undefined || $(this).data('actionurl') == '')) {
                wsLib.setSession('clickedConfirmBtnId', $(this).attr('id'));
                if ($(this).data('ajax')) {

                    var model = JSON.parse(wsLib.getSession($(this).data('modelkey')));
                    var method = $(this).data('method');
                    if (method == undefined || method == '')
                        method = "POST";

                    $.ajax({
                        url: $(this).data('actionurl'),
                        data: model,
                        async: false,
                        type: method,
                        beforeSend: function () {
                            unblockUIAll();
                            blockUI("body", "İşlem Yapılıyor...");
                        },
                        success: function (ajaxdata, textStatus, jqXHR) {
                            if (ajaxdata == 1 || ajaxdata == true) {
                            }
                            else {
                                if (ajaxdata.RedirectUrl != undefined &&
                                    ajaxdata.RedirectUrl != "" &&
                                    ajaxdata.State == 1) {
                                    window.location = ajaxdata.RedirectUrl;
                                } else {

                                    if (ajaxdata.State == 3) {
                                        ShowMessageCustomSwal(ajaxdata.ModelStr);
                                        return;
                                    }
                                    else {
                                        if (ajaxdata.Message != undefined && ajaxdata.Message != "") {
                                            if (ajaxdata.State == 1) {
                                                ShowMessage(ajaxdata.Message, 3, true);
                                            }
                                            else {
                                                ShowMessage(ajaxdata.Message, 2, true);
                                            }
                                        }
                                    }
                                }
                            }

                            var successCallBack = $('#' + wsLib.getSession('clickedConfirmBtnId')).data("successfn");
                            if (successCallBack != undefined && successCallBack != "") {
                                window[successCallBack](ajaxdata, textStatus, jqXHR);
                            }
                        },
                        error: function (jqXHR, textStatus) {
                            var msg = jQuery.parseJSON(jqXHR.responseText);
                            ShowMessage(msg.Message, 1, true);
                            unblockUI();
                            var failCallback = $('#' + wsLib.getSession('clickedConfirmBtnId')).data("failfn");
                            if (failCallback != undefined && failCallback != "") {
                                window[failCallback](jqXHR, textStatus);
                            }
                        },

                        complete: function (ajaxdata, textStatus, jqXHR) {
                            wsLib.clearSession('clickedConfirmBtnId');
                            if (ajaxdata.responseJSON.RedirectUrl != undefined &&
                                ajaxdata.responseJSON.RedirectUrl != "" &&
                                ajaxdata.responseJSON.State == 1) {
                                window.location = ajaxdata.responseJSON.RedirectUrl;
                            }
                            unblockUIAll();
                        }
                    });
                }
                else {
                    if ($(this).data('iframe'))
                        window.parent.location.href = $(this).data('actionurl');
                    else
                        window.location.href = $(this).data('actionurl');
                }
            } else {
                swal.close();
                unblockUIAll();
            }
        });
    }
}

function ShowPartial(title,
    icon,
    url,
    showCloseButton,
    closeCallBackFunction,
    showCallBackFunction,
    modal,
    loadingText,
    model,
    method,
    modalSize) {

    ShowPartialCustom(JSON.stringify(
        {
            "Title": title,
            "Icon": icon,
            "PartialViewUrl": url,
            "ShowCloseButton": showCloseButton,
            "CloseCallBackFunction": closeCallBackFunction,
            "ShowCallBackFunction": showCallBackFunction,
            "Modal": modal,
            "LoadingText": loadingText,
            "Model": model,
            "Method": method,
            "ModalSize": modalSize
        })
    );
}
var partialData = '';
var modalBodyName = '';
var partialHasError = false;
function ShowPartialCustom(partialView) {

    var partial = JSON.parse(partialView);

    var modalName = RandomIdGetir();
    var kapatGoster = true;
    if (partial.ShowCloseButton != undefined)
        kapatGoster = partial.ShowCloseButton;

    var modalSize = "modal-size-70";

    if (partial.ModalSize != undefined)
        modalSize = partial.ModalSize;

    var modal = false;
    if (partial.Modal != undefined)
        modal = partial.Modal;

    var loadingText = "";
    if (partial.LoadingText != undefined)
        loadingText = partial.LoadingText;

    var sb = '';
    sb += '<div id="' + modalName + '" class="modal partialViewModal fade" role="dialog" aria-hidden="true">';
    sb += '<div class="modal-dialog ' + modalSize + ' modal-dialog-centered" role="document">';
    sb += '<div class="modal-content">';
    sb += '  <div class="modal-header ' + msgTitleBgList[0] + '">';
    sb += '  <h5 class="modal-title"><i class="' + partial.Icon + '"></i> ' + partial.Title + '</h5>';
    if (kapatGoster)
        sb += '<button type="button" class="close" data-dismiss="modal" aria-label="Kapat"><span aria-hidden="true">&times;</span></button>';
    sb += '  </div>';
    sb += '  <div class="modal-body" id="' + modalName + '_partialModalBody"></div>';
    sb += '</div>';
    sb += '</div>';
    sb += '</div>';

    $("#dialogContainer").append(sb);
    var obj = $("#" + modalName + "");

    obj.find('button.close').css({ 'color': obj.find('h4').css('color') });

    blockUI("body", loadingText);

    if (partial.Model != undefined) {

        $.ajax({
            async: false,
            url: partial.PartialViewUrl,
            method: "" + partial.Method + "",
            data: partial.Model,
            success: function (data) {
                partialData = data;
                modalBodyName = modalName + '_partialModalBody';
                //$('#' + modalName + '_partialModalBody').html(data);
            },
            error: function (jqXHR) {
                var errMsg = jqXHR.responseJSON.Message == undefined ? "Sistemde Beklenmedik Bir Hata Oluştu" : jqXHR.responseJSON.Message;
                partialHasError = true;
                ShowMessage(errMsg, 1, true);
            },
            complete: function () {
                if (!partialHasError) {
                    obj.modal({
                        backdrop: modal ? 'static' : '',
                        keyboard: kapatGoster
                    });
                    partialHasError = false;
                }
                unblockUI();
            }
        });

    }
    else {
        getPartialView(partial.PartialViewUrl, '', modalName + '_partialModalBody', function () {
            obj.modal({
                backdrop: modal ? 'static' : '',
                keyboard: kapatGoster
            });
            unblockUI();
        }, '');

        //$('#' + modalName + '_partialModalBody').load(partial.PartialViewUrl, function () {
        //    obj.modal({
        //        backdrop: modal ? 'static' : '',
        //        keyboard: kapatGoster
        //    });
        //    unblockUI();
        //});
    }

    $(obj).on('shown.bs.modal', function () {
        if (partial.Model != undefined) {
            $('#' + modalBodyName).html(partialData);
            modalBodyName = '';
            partialData = '';
        }
        if (partial.ShowCallBackFunction != "" && partial.ShowCallBackFunction != undefined && typeof (window[partial.ShowCallBackFunction]) === "function") {
            window[partial.ShowCallBackFunction]();
        }
        ActivateInputProperties();
    });

    $(obj).on('hidden.bs.modal', function (e) {
        if (partial.CloseCallBackFunction != "" && partial.CloseCallBackFunction != undefined && typeof (window[partial.CloseCallBackFunction]) === "function") {
            window[partial.CloseCallBackFunction]();
        }
        $(this).unbind();
        $(this).remove();
    });
    //ActivateDDLs();
}

function HideDialogModal() {
    $('#dialogContainer .modal').modal('hide');
    $('#dialogContainer .modal').unbind();
}

function RemoveDialogModal() {
    HideDialogModal();
    $('#dialogContainer').html('');
    $(".modal-backdrop.fade.show").remove();
}

function HideModal() {
    $('#modalLayout').modal('hide');
    $('#modalLayout').unbind();
}

function RemoveLastDialogModal() {
    $('#dialogContainer').find('.modal').last().modal('hide');
}
//Mesaj Göster

//Block UI
function blockUI(elementId, customMessage) {

    var msg = "Yükleniyor...";
    var element = "body";

    if (customMessage != undefined && customMessage != "")
        msg = customMessage;

    if (elementId != undefined && elementId != "")
        element = elementId;

    mApp.block(element, { overlayColor: "#000000", type: "loader", state: "success", message: msg });
}

function unblockUI(elementId) {
    var element = "body";
    if (elementId != undefined && elementId != "")
        element = elementId;
    mApp.unblock(element);
}
function unblockUIAll() {
    $('.blockUI').remove();
}
//Block UI

//Tabs & Accordion
function ChangeTabIndex(tabId, tabIndex) {
    var tabUl = $('#' + tabId);
    tabUl.find('a[data-toggle="tab"]').each(function (i) {
        if (i == tabIndex) {
            $(this).tab('show');
        }
    });
    ActiveTabHdnYaz();
}

function ActiveTabHdnYaz() {
    var diziStr = '';
    $('.tab-content').each(function (i) {
        var activeDiv = $(this).find('div.active').attr('id');
        diziStr += activeDiv + ',';
    });

    if (diziStr.length > 0) {
        diziStr = diziStr.substring(0, diziStr.length - 1);
    }
    sessionStorage.setItem("tabState", diziStr);
}

function GetAjaxTabPartialData(element) {
    var tabContentId = $(element).attr('href').replace('#', '');
    var tabContent = $('#' + tabContentId);

    if ($(tabContent).html().trim() == "") {

        var url = $(element).data("url");
        var method = $(element).data("method");
        mApp.block($(tabContent), { overlayColor: "#000000", type: "loader", state: "success", message: "Lütfen bekleyiniz..." });

        if (method == undefined || method == '')
            method = "GET";

        $.ajax({
            url: url,
            type: method,
            error: function (jqXHR) {
                var msg = jQuery.parseJSON(jqXHR.responseText);
                if (msg != undefined)
                    ShowMessage(msg.Message, 1, true);
                else
                    $(tabContent).html('<div class="alert alert-danger"><div class="text-center"><b> <i class="fa fa-exclamation-triangle"></i>&nbsp;</b>  İçerik yüklenirken hata oluştu!</div></div>');
            },
            success: function (dataSuccess) {
                if (dataSuccess) {
                    $(tabContent).html(dataSuccess);
                }
            },
            complete: function () { mApp.unblock($(tabContent)); }
        });
    }
}

function ChangeAccordionIndex(accordionId, accordionIndex) {
    var accordion = $('#' + accordionId);
    if (accordion != undefined) {
        $(accordion).find('.m-accordion__item-body').each(function (i) {
            if (i == accordionIndex) {
                if (!$(this).hasClass('show')) {
                    $(this).addClass('show');
                }
            }
            else {
                $(this).removeClass('show');
            }
        });

        $(accordion).find('div[data-toggle="collapse"]').each(function (j) {
            if (j == accordionIndex) {
                if ($(this).hasClass('collapsed')) {
                    $(this).removeClass('collapsed');
                }
            }
            else {
                $(this).addClass('collapsed');
            }
        });
    }

    //ActiveAccordionHdnYaz();
}

function ActiveAccordionHdnYaz() {
    var diziStr = '';
    $('.m-accordion__item-body').each(function (i) {
        if ($(this).hasClass("show"))
            diziStr += $(this).attr('id') + ',';
    });
    if (diziStr.length > 0) {
        diziStr = diziStr.substring(0, diziStr.length - 1);
    }
    sessionStorage.setItem("accordionState", diziStr);
}

function GetAjaxAccordionPartialData(element) {
    var accordionItem = $(element).closest('.m-accordion__item');
    var ajaxAccordion = $(accordionItem).find(".ajaxAccordion");
    if ($(ajaxAccordion).data('load') != true) {
        var accordionBody = $(element);
        mApp.block($(accordionBody), { overlayColor: "#000000", type: "loader", state: "success", message: "Lütfen bekleyiniz..." });
        var url = $(ajaxAccordion).data("url");
        var method = $(ajaxAccordion).data("method");

        if (method == undefined || method == '')
            method = "GET";

        $.ajax({
            url: url,
            type: method,
            error: function (jqXHR) {
                var msg = jQuery.parseJSON(jqXHR.responseText);
                if (msg != undefined)
                    ShowMessage(msg.Message, 1, true);
                else
                    $(accordionBody).find(".ajaxAccordion").html('<div class="alert alert-danger"><div class="text-center"><b> <i class="fa fa-exclamation-triangle"></i>&nbsp;</b>  İçerik yüklenirken hata oluştu!</div></div>');
            },
            success: function (dataSuccess) {
                if (dataSuccess) {
                    $(accordionBody).find(".ajaxAccordion").html(dataSuccess);
                    $(ajaxAccordion).attr('data-load', true);
                }
                SetAjaxAccordionInnerHeightforDropDown();
            },
            complete: function () { mApp.unblock($(accordionBody)); }
        });
    }
}

//Tabs & Accordion

//Session Counter
function CountBack(isPersonnel) {
    SessionTimeOutSuresi--;
    var sure = window.SessionTimeOutSuresi;

    $('.sessionTimer p').html("Eğer İşlem Yapılmazsa, Oturumunuz <b>" + KalanSureStr(sure) + "</b> sonra otomatik olarak kapatılacak.");

    // 300. ve 150. saniyede oturum kapanıyor geri sayımı 
    if (sure == 300 || sure == 150) {
        $('.sessionTimer').show("slow");
    }
    if (sure > 0)
        setTimeout("CountBack(" + isPersonnel + ")", 1000);
    else {
        if (!isPersonnel)
            window.location = "/Login/Exit";
        else
            window.location = "/Login/ExitPersonnel";
    }
}

function KalanSureStr(sec) {
    var str = '';
    var kalanDakika = parseInt(sec / 60);
    var kalanSaniye = parseInt(sec % 60);

    if (kalanDakika > 0)
        str += kalanDakika + ' dakika ';
    str += ' ' + kalanSaniye + ' saniye';

    return str;
}

function getTimeRemaining(endtime) {
    var t = Date.parse(endtime) - Date.parse(new Date());
    var seconds = Math.floor((t / 1000) % 60);
    var minutes = Math.floor((t / 1000 / 60) % 60);
    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
    var days = Math.floor(t / (1000 * 60 * 60 * 24));
    return {
        'total': t,
        'days': days,
        'hours': hours,
        'minutes': minutes,
        'seconds': seconds
    };
}
//Session Counter

//Grid İşlemleri
function GridViewToolTip() {
    $("td.gridViewToolTip").each(function () {
        //var cnt = $(this).next();
        //var popoverTemplate = $('<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>');

        $(this).append("<label class='btn btn-sm btn-success'>" + $(this).html() + "</label>");
        //$(this).addClass("btn blue btn-sm btn-outline btn-circle");
        var placement = "left";
        var baslik = "Bilgi";
        var genislik = 180;

        //if ($(this).data("placement") != undefined)
        //    placement = $(this).data("placement");

        //if ($(this).data("baslik") != undefined)
        //    baslik = $(this).data("baslik");

        //if ($(this).data("genislik") != undefined)
        //    genislik = $(this).data("genislik");

        //if ($(window).width() < 500)
        //    placement = 'top';

        //cnt.hide();

        //$(this).popover({
        //    trigger: 'click hover',
        //    width: genislik,
        //    content: cnt.html(),
        //    template: popoverTemplate,
        //    placement: placement,
        //    title: "<b><i class=\"fa fa-info-circle\"></i> " + baslik + "</b>",
        //    html: true,
        //    container: "body"
        //});
    });
}

function GridIslemler() {
    $('td.grdIslemler ,td.grdIslemlerMin').each(function (i) {
        var isMin = $(this).hasClass('grdIslemlerMin');
        var aButtons = $(this).find('a');
        var sb = '<div class="dropdown">';
        sb += '<button class="btn btn-sm btn-secondary dropdown-toggle' + (aButtons.length > 0 ? '' : ' disabled') + '" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
        sb += '<i class="fa fa-gears "></i>&nbsp;';
        if (!isMin) {
            sb += 'İşlemler';
        }
        sb += '</button>';
        sb += '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start">';
        for (var j = 0; j < aButtons.length; j++) {
            var aButton = aButtons[j];
            sb += aButton.outerHTML;
        }
        sb += '</div>';
        sb += '</div>';
        $(this).html(sb);
    });

    $('td.grdIslemlerYanYana').each(function (i) {

        var sb = '';
        var aButtons = $(this).find('a');
        var buttonWidth = 0;
        sb += '<div class="btn-group  btn-group-sm">';
        for (var j = 0; j < aButtons.length; j++) {
            buttonWidth += $(aButtons[j]).width();
            var aButton = aButtons[j];
            sb += aButton.outerHTML;
        }
        sb += '</div>';
        if (aButtons.length > 0) {
            buttonWidth += 60;
            $(this).css({ 'width': '' + buttonWidth + 'px' });
        }
        $(this).html(sb);
    });

    $('.ExportButton').each(function (index) {
        var tagName = $(this).prop("tagName");
        if (tagName == "A" || tagName == "a")
            $(this).attr("target", "_blank");
    });
}
//Grid İşlemleri

//Cascading
function getDDLData(actionURL, actionData, elementID, defaultValue, selectedValue, callback) {

    if (actionURL != undefined && actionURL != null && actionURL != '') {
        $.ajax({
            url: actionURL, data: actionData, dataType: "json", type: "POST",
            success: function (data) {
                var ddlLst = $("#" + elementID);
                var tmpHTML = "";
                if (defaultValue) { tmpHTML += "<option value=''>Seçiniz</option>"; }
                if (data != null && $.isArray(data) && data.length > 0) {
                    var isGroup = false;

                    if (data[0].Group != undefined && data[0].Group != null && data[0].Group != '') {
                        isGroup = true;
                        sortJson(data, "Group", "Name", "string", true);
                    }

                    if (isGroup) {
                        var tmpGroup = "";
                        $.each(data, function (i, item) {
                            if (tmpGroup != item.Group.Name && item.Group.Name != undefined && item.Group.Name != null && item.Group.Name != '') {
                                if (tmpGroup != "") { tmpHTML += '</optgroup>'; }
                                tmpGroup = item.Group.Name;
                                tmpHTML += '<optgroup label="' + tmpGroup + '">';
                            }
                            tmpHTML += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        });
                    }
                    else {
                        $.each(data, function (i, item) { tmpHTML += "<option value='" + item.Value + "'>" + item.Text + "</option>"; });
                    }

                    ddlLst.html(tmpHTML);

                    if (selectedValue != undefined && selectedValue != null && selectedValue != '') {
                        ddlLst.val(selectedValue);
                    }
                    else {

                        var tmp = ddlLst.data("init-value");
                        if (tmp != null && tmp != undefined) {
                            ddlLst.val(tmp);
                        }
                        else if (defaultValue) {
                            ddlLst.val('');
                        }

                        if (ddlLst.find('option:selected').html() == undefined) {
                            $(ddlLst).val('');
                        }
                    }
                    $(ddlLst).removeAttr("disabled");
                    ddlLst.trigger("change");
                }
                else {
                    ddlLst.html(tmpHTML);
                }

                $('.ddl-select').select2({
                    language: "tr-TR",
                    placeholder: {
                        id: '-1', // the value of the option
                        text: 'Seçiniz'
                    }
                });

                //if (callback !== undefined && callback != '' && typeof (window[callback]) === "function") {
                //    window[callback]();
                //}
                if (callback && typeof (callback) === "function") { callback(data); }
            }
        });
    }
}
//Cascading

function ActivateDDLs() {
    $('.ddl-select').select2({
        language: "tr-TR",
        placeholder: {
            id: '-1',
            text: 'Seçiniz'
        }
    });

    $(".select2-multiple").select2({ closeOnSelect: false });
}

function ActivateInputProperties() {
    /*
float classı verilen text alanları için sadece ondalık girilmesini sağlar.
*/
    $("input[data-datatype='float'], input[data-datatype='decimal']").keypress(function (e) {
        if ((e.which == 44 && this.value.indexOf(',') == '-1' && this.value.length != '0') ||
            (e.which == 45 && this.value.indexOf('-') == '-1' && this.value.length == '0')) {
            return true;
        }
        else if (e.which != 8 & e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("input[data-datatype='sayi'], input[data-datatype='int']").keypress(function (e) {
        if (e.which != 8 & e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("input[data-datatype='tarih'], input[data-datatype='tarih1'], input[data-datatype='tarih2']").inputmask("d/m/y", { "placeholder": "__/__/____" });

    $("input[data-datatype='telefon']").inputmask("mask", { "mask": "(999) 999-9999" });

    $("input[data-mask]").each(function (index, element) {
        $(element).inputmask("mask", { "mask": $(element).data('mask') });
    });

    $("input[data-mask-type='Number']").each(function (index, element) {
        var maskRepeatSize = $(element).data('mask-repeat');
        if (maskRepeatSize != null && maskRepeatSize != 'undefined' && maskRepeatSize > 0) {
            $(element).inputmask({ mask: "9", repeat: $(element).data('mask-repeat'), greedy: true });
        }
        else {
            $(element).inputmask({ mask: "9{*}", greedy: true });
        }
    });

    $("input[data-mask-type='Text']").each(function (index, element) {
        var maskRepeatSize = $(element).data('mask-repeat');
        var maskRepeatType = $(element).data('mask-repeat-type');

        var type = '';
        if (maskRepeatType == 'All') {
            type = '*';
        }
        else if (maskRepeatType == 'Numeric') {
            type = '9';
        }
        else if (maskRepeatType == 'Alphabetical') {
            type = 'a';
        }

        if (type != '') {
            $(element).inputmask(type + '{' + maskRepeatSize + '}');
        }
    });

    $("input[data-mask-type='Phone']").inputmask("mask", { "mask": "(999) 999-9999" });
    $("input[data-mask-type='IBAN']").inputmask("mask", { "mask": "TR999999999999999999999999" });

    $("input[data-mask-type='Decimal']").inputmask("decimal", { radixPoint: "," });
    $("input[data-mask-type='Currency']").inputmask("₺ 999.999.999,99", { numericInput: true });

    $(".datepicker").each(function (index, element) {
        ActivateDateValidation(element);
    });

    function ActivateDateValidation(element) {
        var minDate = $(element).data("date-val-min");
        var maxDate = $(element).data("date-val-max");
        var callbackFn = $(element).data("callback");

        if ($(element).hasClass("dateRangePickerStart")) {
            var endDateElement = $('#' + $(element).data('enddateelement'));

            var endMaxDate = $(endDateElement).data("date-val-max");
            var startMaxDate = $(element).data("date-val-max");

            if (wsLib.isNullOrEmpty(maxDate)) {
                maxDate = endMaxDate;
            }
            else if (maxDate > endMaxDate) {
                maxDate = endMaxDate;
            } else if (endMaxDate > maxDate) {
                maxDate = startMaxDate;
            }
        }

        var format = $(element).data("dateformat");
        var options = {};
        options.locale = "tr";

        if (!wsLib.isNullOrEmpty(format)) {
            options.format = format;
        }
        if (!wsLib.isNullOrEmpty(minDate)) {
            if (format == "MM/YYYY") {
                options.minDate = minDate;
            }
            else {
                options.minDate = getDatetimePickerMinMaxDate(minDate);
            }
        }
        if (!wsLib.isNullOrEmpty(maxDate)) {
            if (format == "MM/YYYY") {
                options.maxDate = maxDate;
            }
            else {
                options.maxDate = getDatetimePickerMinMaxDate(maxDate);
            }
        }

        var elementVal = $(element).val();
        $(element).datetimepicker(options);
        $(element).val(elementVal);

        if (callbackFn != undefined) {
            $(element).on('dp.update', function (e) {
                window[callbackFn]($(this).val());
            });
        }
    }

    $('body').on("dp.change", ".dateRangePickerStart", function (e) {
        var startVal = $(this).val();

        if (!wsLib.isNullOrEmpty(startVal)) {

            var endDateElement = $('#' + $(this).data('enddateelement'));

            var minDate = $(endDateElement).data("date-val-min");
            if (minDate == undefined || new Date(getDatetimePickerMinMaxDate(startVal)) > new Date(getDatetimePickerMinMaxDate(minDate))) {
                minDate = startVal;
            }

            var endMaxDate = $(endDateElement).data("date-val-max");
            var endVal = $(endDateElement).val();

            var format = $(endDateElement).data("dateformat");
            var options = {};
            options.locale = "tr";

            if (!wsLib.isNullOrEmpty(format)) {
                options.format = format;
            }
            if (!wsLib.isNullOrEmpty(minDate)) {
                if (format == "MM/YYYY") {
                    options.minDate = minDate.split("/").reverse().join("-");
                }
                else {
                    options.minDate = getDatetimePickerMinMaxDate(minDate);
                }
            }
            if (!wsLib.isNullOrEmpty(endMaxDate)) {
                if (format == "MM/YYYY") {
                    options.maxDate = endMaxDate.split("/").reverse().join("-");
                }
                else {
                    options.maxDate = getDatetimePickerMinMaxDate(endMaxDate);
                }
            }

            var date = $(endDateElement).data('DateTimePicker');
            if (date != undefined) {
                date.destroy();
            }

            var elementVal = $(endDateElement).val();
            $(endDateElement).datetimepicker(options);
            $(endDateElement).val(elementVal);

            var x = "";
            var y = "";
            if (format == "MM/YYYY") {
                x = new Date(startVal.split("/").reverse().join("-"));
                y = new Date(endVal.split("/").reverse().join("-"));
            }
            else {
                x = new Date(getDatetimePickerMinMaxDate(startVal));
                y = new Date(getDatetimePickerMinMaxDate(endVal));
            }

            if (x > y) {
                $(endDateElement).val('');
            }
            else {
                $(endDateElement).val(endVal);
            }
        }

    });
    $(".dateRangePickerStart").trigger("dp.change");
    $('body').on("click", ".dateRangePickerStart", function (e) {
        $(this).trigger("dp.change");
    });

    $('body').on("dp.change", ".dateRangeFilterStart", function (e) {
        var startVal = $(this).val();

        if (!wsLib.isNullOrEmpty(startVal)) {
            var endDateElement = $('#' + $(this).data('enddateelement'));

            var options = {};
            options.locale = "tr";
            options.format = 'L';
            options.minDate = getDatetimePickerMinMaxDate(startVal);

            var date = $(endDateElement).data('DateTimePicker');
            if (date != undefined) {
                date.destroy();
            }

            $(endDateElement).datetimepicker(options);
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({ container: 'body' });

    $('.ddl-select').select2({
        language: "tr-TR",
        placeholder: {
            id: '-1', // the value of the option
            text: 'Seçiniz'
        }
    });

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    $.validator.addMethod('date', function (value, element) {
        if (this.optional(element)) { return true; }
        var ok = true;
        try {
            var date_regex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
            if (!(date_regex.test(value))) { ok = false; }

            var regex = /^((0[1-9])|(1[0-2]))\/((2009)|(20[1-2][0-9]))$/;
            if (!(regex.test(value))) { ok = false || ok; }
            else { ok = true; }

        } catch (err) { ok = false; }
        return ok;
    });

    initCustomValidation();
}
function ActivateAutoComplete() {
    if ($('.typeahead').length) {

        $('.typeahead').each(function (index) {
            var self = $(this);
            var showDescription = self.data("showdesc");
            var isMultiple = self.data("multiple");
            var targetId = self.data("target");
            var connectedWith = self.data("connectedwith");

            var url = self.data("url-value");
            if (isMultiple === "True") {
                url = self.data("url-values");
            }

            var securityToken = $('[name=__RequestVerificationToken]').val();

            //içi dolu ise ajax ile dolu set et
            var selectedId = $("#" + targetId).val();
            if (selectedId !== "" && selectedId !== "0" && selectedId !== undefined) {

                var acRequest = $.ajax({
                    data: {
                        __RequestVerificationToken: securityToken,
                        selectedId: selectedId
                    },
                    url: url,
                    method: "POST",
                    async: true
                });

                acRequest.done(function (data) {
                    if (isMultiple === "True") {
                        $.each(data,
                            function (i, item) {
                                $("<span></span>")
                                    .addClass("badge badge-info")
                                    .data("vv", item.id)
                                    .text(item.text + " ")
                                    .append($("<i></i>")
                                        .addClass("fa fa-times")
                                        .click(function () {
                                            var v = $("#" + targetId).val();
                                            var vtr = $(this).parent().data("vv");
                                            var vSplitted = v.split(",");
                                            var vNewValue = _.without(vSplitted, vtr);
                                            $("#" + targetId).val(vNewValue.toString());
                                            $(this).parent().remove();
                                        })
                                    )
                                    .appendTo($("#ac_selection_" + targetId));
                                $("#ac_selection_" + targetId).append(" ");
                            });
                    } else {
                        self.val(data.text).trigger("change");
                        self.prop('readonly', true).addClass("text-danger");
                    }

                });

                acRequest.fail(function (msg) {
                    self.val("Yüklenemedi!!");
                    self.prop('disabled', true).addClass("text-danger");
                });

            }


            if (isMultiple === "True") {

                self.on("keydown",
                    function (event) {
                        if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                            event.preventDefault();
                        }
                    });

            }

            if (connectedWith !== undefined) {
                if (selectedId !== "" && selectedId !== "0" && selectedId !== undefined) {
                    $("#_" + connectedWith).data("rootid", selectedId).addClass("connected");

                } else {
                    $("#_" + connectedWith).prop('disabled', true);
                }
            }

            self.autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: self.data("url"),
                        dataType: "json",
                        data: {
                            key: extractLast(request.term),//request.term
                            rootId: self.data("rootid")
                        },
                        success: function (data) {

                            response($.map(data,
                                function (item) {
                                    return {
                                        label: item.description,
                                        value: item.text,
                                        id: item.id
                                    };
                                }));
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            ShowMessage(xhr.responseJSON.Message, "error", true);
                        }
                    });
                },
                minLength: 2,
                //   multiselect: isMultiple === "True"?true:false,

                select: function (event, ui) {


                    if (isMultiple === "True") {

                        var v = [];
                        if ($("#" + targetId).val() !== "")
                            v = split($("#" + targetId).val());

                        if (!_.contains(v, ui.item.id)) {
                            $("<span></span>")
                                .addClass("badge badge-info")
                                .data("vv", ui.item.id)
                                .text(ui.item.value + " ")
                                .append($("<i></i>")
                                    .addClass("fa fa-times")
                                    .click(function () {
                                        var v = split($("#" + targetId).val());
                                        var vtr = $(this).parent().data("vv");
                                        v = _.without(v, vtr);
                                        $("#" + targetId).val(v.toString());
                                        $(this).parent().remove();
                                    })
                                )
                                .appendTo($("#ac_selection_" + targetId));
                            $("#ac_selection_" + targetId).append(" ");
                            v.push(ui.item.id);


                            $("#" + targetId).val(v.toString());
                            self.val("");
                        }
                        self.val("");

                    }
                    else {
                        $("#" + targetId).val(ui.item.id);
                        self.val(ui.item.value);

                        var locked = self.data("locked-after-select");
                        if (locked != "False")
                            self.prop("readonly", true).addClass("text-info");
                    }

                    if (self.data("callback") !== undefined) {
                        window[self.data("callback")](ui.item);
                    }

                    if (targetId !== undefined) {
                        $("#_" + connectedWith).data("rootid", ui.item.id).addClass("connected").prop('disabled', false).focus();

                    }

                    self.blur();
                    return false;


                }

            });

            self.focus(function () {
                $(this).autocomplete("search", "");
            }).autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>").append(item.value).appendTo(ul);
            };

        });

        $(".autocomplete_clear").click(function (e) {
            e.preventDefault();

            var target = $(this).data("target");
            var self = $("#_" + target);
            var connectedId = self.data("connectedwith");
            self.val('').removeAttr("readonly").removeClass("autocomplete_loading text-danger").focus();
            $("#" + target).val("");


            if (self.data("clearcallback") !== undefined) {
                window[self.data("clearcallback")](e);
            }

            if (connectedId !== undefined) {
                //bağlantılı ise bağlı olduğu ototamamları da sıfırla
                $("#ac_clear_" + connectedId + " ").trigger("click");
                $("#_" + connectedId).prop('disabled', true);

            }

            $("#ac_selection_" + target).html("");
        });

        $(".listopener").click(function (e) {
            e.preventDefault();

            var type = $(this).data("type");
            var target = $(this).data("target");
            var self = $("#_" + target);
            var url = self.data("url");
            var viewUrl = self.data("viewurl");
            var mymodal;




            if (type === "Tree") { //tree
                url = url.replace("AjaxAra", "AjaxAraTree");
                mymodal = bootbox.dialog({
                    title: "<i class='fa fa-sitemap'></i> Ağaçtan Seçiniz",
                    message: '<div id="autocompTree"></div>'
                });
                $('#autocompTree')
                    .on('changed.jstree', function (e, data) {
                        var i, j, r = [], v = [];
                        for (i = 0, j = data.selected.length; i < j; i++) {
                            r.push(data.instance.get_node(data.selected[i]).text);

                            v.push(data.instance.get_node(data.selected[i]).id);
                        }
                        self.val(r.join(', ')).trigger("change");
                        $("#" + target).val(v.join(', '));

                        self.prop('readonly', true).addClass("text-danger");

                        if (self.data("callback") !== undefined) {
                            window[self.data("callback")](data.instance.get_node(data.selected[0]));
                        }
                        mymodal.modal("hide");

                    })
                    .jstree({
                        'core': {
                            'themes': {
                                'name': 'proton',
                                'responsive': true
                            },
                            'data': {
                                "url": url,
                                "data": function (node) {
                                    return { "treenodeid": node.id };
                                }
                            }
                        }
                    });
            }
            else if (type === "CustomView") {
                if (viewUrl === "") {
                    alert("viewUrl parametresi ayarlanmamış!");
                } else {


                    var modal_width = '800';
                    var modal_height = '400';

                    if (viewUrl.contains("?")) {
                        viewUrl = viewUrl + "&layout=modal";
                    } else {
                        viewUrl = viewUrl + "?layout=modal";
                    }

                    mymodal = bootbox.dialog({
                        title: $(this).data("original-title"),
                        backdrop: true,
                        closeButton: true,
                        animate: true,
                        className: "my-modal",
                        size: "large",
                        message: '<iframe style="width:100%;height:99%;min-height:' + modal_height + 'px;border:0;"  src="' + viewUrl + '"></iframe>'
                    }).resizable();

                }


            }


            else if (type === "List") // List veya none
            {
                mymodal = bootbox.dialog({
                    title: "<i class='fa fa-list-ul'></i> Listeden Seçiniz",
                    message: '<div id="autocompList"><input type="text" class="form-control" placeholder="aramak için yazmaya başlayınız" id="autocompListKey"/></div>',
                    buttons: {
                        nextbtn: {
                            label: '<i class="fa fa-chevron-right"></i>',
                            className: "btn btn-primary",
                            callback: function () {
                            }
                        },
                        prvsbtn: {
                            label: '<i class="fa fa-chevron-left"></i>',
                            className: "btn btn-primary pull-left",
                            title: "Önceki Sayfa",
                            callback: function () {
                            }
                        }
                    }

                });
                url = url + "&key=" + $("#autocompListKey").val();
                $.getJSON(url, function (data) {
                    var items = [];
                    $.each(data, function (key, val) {
                        items.push('<tr class="pointer" id="tr_' + key + '" data-value="' + val.id + '" data-label="' + val.text + '" data-desc="' + val.description + '"><td>' + val.text + '</td><td>' + val.description + '</td></tr>');
                    });

                    $('<table/>', {
                        'data-start': 0,
                        'class': 'table table-striped table-hover autocompListTable',
                        html: items.join('')
                    }).appendTo($("#autocompList"));
                    $('table.autocompListTable tr')
                        .on('click', function (e, data) {
                            var tr = $(this);
                            self.val(tr.data("label")).trigger("change").prop('readonly', true).addClass("text-danger");
                            $("#" + target).val(tr.data("value"));

                            mymodal.modal("hide");
                        });

                });
            }
            /*    $(mymodal).draggable({
                    handle: ".modal-header"
                }); */
        });
    }
}

//Notifications
function showInfoNotification(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, true, 0); }
function showWarningNotification(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, true, 1); }
function showSuccessNotification(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, true, 2); }
function showErrorNotification(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, true, 3); }
function showInfoMessage(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, false, 0); }
function showWarningMessage(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, false, 1); }
function showSuccessMessage(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, false, 2); }
function showErrorMessage(msgContent, msgHeader) { showToastNotification(msgContent, msgHeader, false, 3); }

function showToastNotification(msgContent, msgHeader, boolNotification, msgType) {
    if (boolNotification)
        toastr.options.positionClass = "toast-top-right";
    else
        toastr.options.positionClass = "toast-top-full-width";

    toastr.options.timeOut = 5000;
    toastr.options.closeButton = true;

    if (typeof msgContent == 'undefined') { msgContent = ''; }

    if (msgType == 0) {
        if (typeof msgHeader == 'undefined') { msgHeader = 'Bilgi'; }
        toastr.info(msgContent, msgHeader);
    }
    else if (msgType == 1) {
        if (typeof msgHeader == 'undefined') { msgHeader = 'Uyarı'; }
        toastr.options.timeOut = 10000;
        toastr.warning(msgContent, msgHeader);
    }
    else if (msgType == 2) {
        if (typeof msgHeader == 'undefined') { msgHeader = ''; }
        toastr.success(msgContent, msgHeader);
    }
    else if (msgType == 3) {
        if (typeof msgHeader == 'undefined') { msgHeader = 'Hata'; }
        toastr.options.timeOut = 0;
        toastr.error(msgContent, msgHeader);
    }
}
//Notifications
//function OnayGoster(el, onayUyari) {
//    try {
//        if (wsLib.isNullOrEmpty(onayUyari)) {
//            if (!($(el).data("uyari") == undefined || $(el).data("uyari") == "")) {
//                onayUyari = $(el).data("uyari");
//            }
//            else if (!($(el).attr("uyari") == undefined || $(el).attr("uyari") == "")) {
//                onayUyari = $(el).attr("uyari");
//            }
//            else {
//                onayUyari = "Bu İşlemi Yapmak İstediğinize Emin misiniz?";
//            }
//        }



//        var frm;
//        if ($(el).attr('type') == "submit") {
//            var relatedFormId = $(el).data("relatedformid");
//            if (relatedFormId != undefined && relatedFormId != "")
//                frm = $('#' + relatedFormId);
//            else
//                frm = $(el).closest('form');
//        }
//        if (frm != undefined) {
//            frm.removeData('validator');
//            frm.removeData('unobtrusiveValidation');
//            $.validator.unobtrusive.parse(frm);

//            if (!$(frm).valid()) {
//                return false;
//            }
//        }

//        var sb = '';
//        sb += '<div id="onayModal" class="modal fade mesajModal" role="dialog" aria-hidden="true">';
//        sb += '<div class="modal-dialog modal-dialog-centered" role="document">';
//        sb += '<div class="modal-content">';
//        sb += '  <div class="modal-header alert-warning">';
//        sb += '  <h5 class="modal-title"><i class="fa fa-exclamation-triangle"></i>&nbsp;Uyarı!</h5>';
//        sb += '<button type="button" class="close" data-dismiss="modal" aria-label="Kapat"><span aria-hidden="true">&times;</span></button>';
//        sb += '  </div>';
//        sb += '  <div class="modal-body" id="modalBody">';
//        sb += '         ' + onayUyari + ' ';
//        sb += '       </div>';
//        sb += '<div class="modal-footer">';
//        sb += '    <button type=\"button\" class=\"btn btn-outline-success btn-sm\" data-dismiss=\"modal\" id=\"onayModalEvet\">Evet</button>';
//        sb += '    <button type=\"button\" class=\"btn btn-outline-danger btn-sm\" data-dismiss=\"modal\" id=\"onayModalHayir\">Hayır</button>';
//        sb += '</div>';
//        sb += '</div>';
//        sb += '</div>';
//        sb += '</div>';

//        $("#dialogContainer").append(sb);

//        var obj = $('#onayModal');

//        obj.modal({ backdrop: 'static' });

//        obj.find('button.close').css({ 'color': obj.find('h4').css('color') });

//        obj.on('shown.bs.modal', function (e) {
//            // do something... Open Event            
//        });
//        obj.on('hidden.bs.modal', function (e) {
//            // do something... Close Event                
//        });

//        $('#onayModalEvet').on('click', function () {

//            $('#onayModal').modal("hide");
//            $('#onayModal').remove();
//            $(".modal-backdrop").remove();

//            var confirmFn = $(el).data('confirmfn');

//            if (frm != undefined) {
//                frm.submit();
//            }
//            else if (confirmFn != undefined && confirmFn != '') {
//                var confirmfnparam = $(el).data('confirmfnparam');
//                window[confirmFn](confirmfnparam);
//            }
//            else {
//                window.location.href = $(el).attr("href");
//            }

//        });

//        $('#onayModalHayir').on('click', function () {
//            $('#onayModal').modal("hide");
//            $('#onayModal').remove();
//            $(".modal-backdrop").remove();
//            return false;
//        });
//    } catch (e) {
//        if (confirm($(el).attr("uyari") == undefined || $(el).attr("uyari") == "" ? "Bu İşlemi Yapmak İstediğinize Emin misiniz?" : $(el).attr("uyari"))) {
//            return true;
//        } else
//            return false;
//    }
//}

function OnayGoster(el) {
    try {

        var onayUyari;

        if (!($(el).data("uyari") == undefined || $(el).data("uyari") == "")) {
            onayUyari = $(el).data("uyari");
        }
        else if (!($(el).attr("uyari") == undefined || $(el).attr("uyari") == "")) {
            onayUyari = $(el).attr("uyari");
        }
        else {
            onayUyari = "Bu İşlemi Yapmak İstediğinize Emin misiniz?";
        }

        var frm;
        if ($(el).attr('type') == "submit") {
            var relatedFormId = $(el).data("relatedformid");
            if (relatedFormId != undefined && relatedFormId != "")
                frm = $('#' + relatedFormId);
            else
                frm = $(el).closest('form');
        }

        if (frm != undefined) {
            frm.removeData('validator');
            frm.removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse(frm);

            if (!$(frm).valid()) {
                return false;
            }
        }

        swal({
            title: "Uyarı",
            html: onayUyari,
            type: "question",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Evet, devam et",
            cancelButtonText: "Hayır, iptal et",
            width: '500px'
        }).then(function (isConfirm) {
            if (isConfirm.value == true) {
                var confirmFn = $(el).data('confirmfn');

                if (frm != undefined) {
                    frm.submit();
                }
                else if (confirmFn != undefined && confirmFn != '') {
                    var confirmfnparam = $(el).data('confirmfnparam');
                    window[confirmFn](confirmfnparam);
                }
                else {
                    window.location.href = $(el).attr("href");
                }
            }
        });
        return false;
    } catch (e) {
        if (confirm($(el).attr("uyari") == undefined || $(el).attr("uyari") == "" ? "Bu Kaydı Silmek İstediğinize Emin misiniz?" : $(el).attr("uyari"))) {
            return true;
        } else
            return false;
    }
}

$(function () {

    var url = window.location;
    var securityToken = $('[name=__RequestVerificationToken]').val();

    $(".select2-multiple").select2({ closeOnSelect: false });

    $(".select2-multiple[data-callback]").select2({ closeOnSelect: false }).on("select2:close",
        function () {
            if ($(this).data('callback') != undefined)
                window[$(this).data('callback')]($(this).val());
        });

    //var element = $('a.nav-link').filter(function () {
    //    return this.href == url || url.href.indexOf(this.href) == 0;
    //});
    //if (element != undefined) {

    //    element.closest('li.nav-item').addClass('active open');
    //    element.parent().parent().parent().addClass('active open');
    //    if (element.is('li')) {
    //        element.addClass('active open');
    //    }
    //}
    //Element CallBacks
    $('body').on('change', '[type=checkbox][data-callback]', function () {
        if ($(this).data('callback') != undefined)
            window[$(this).data('callback')]($(this).is(":checked"));
    });

    $('body').on('change', '.radio-group[data-callback] [type=radio]', function () {

        var group = $(this).closest("[data-callback]");

        if ($(group).data('callback') != undefined) {
            var val = $("[name=" + $(this).attr("name") + "]:checked").val();
            window[$(group).data('callback')](val);
        }
    });

    $('body').on('change', ':not([type=checkbox]):not(.radio-group):not(.typeahead):not(.select2-multiple)', function () {
        if ($(this).data('callback') != undefined)
            window[$(this).data('callback')]($(this).val(), $(this));
    });
    //Element CallBacks

    //Tabs & Accordions
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        ActiveTabHdnYaz();
    });

    //if (sessionStorage.getItem("tabState") != undefined &&
    //    sessionStorage.getItem("tabState").length > 0) {
    //    var dizi = sessionStorage.getItem("tabState").split(',');
    //    for (var i = 0; i < dizi.length; i++) {
    //        $('a[href="#' + dizi[i] + '"]').tab('show');
    //    }
    //}


    if ($('div[data-toggle="collapse"]').length > 0) {

        $('.m-accordion__item-body').on('shown.bs.collapse', function (e) {
            ActiveAccordionHdnYaz();
        });

        $('.m-accordion__item-body').on('hidden.bs.collapse', function (e) {
            ActiveAccordionHdnYaz();
        });

        if (sessionStorage.getItem("accordionState") != undefined &&
            sessionStorage.getItem("accordionState").length > 0) {

            $(".m-accordion__item-body").removeClass("show");
            $('div[data-toggle="collapse"]').addClass("collapsed");

            var diziAccordion = sessionStorage.getItem("accordionState").split(',');
            for (var i = 0; i < diziAccordion.length; i++) {
                if (!(diziAccordion[i] == undefined || diziAccordion[i] == '')) {
                    $('#' + diziAccordion[i]).addClass("show");
                    $('div[href="#' + diziAccordion[i] + '"]').removeClass('collapsed');
                }
            }
        }
    }

    //Tabs & Accordions
    SetAjaxAccordionInnerHeightforDropDown();

    //Cascading
    $("body").on("change", ".ddl[data-related-input-id]", function () {
        var value = $(this).val();

        var relatedInput = $(this).data('related-input-id');
        if (value != undefined && value != null && value != '') {
            var url = $(this).data('related-ajax-url');
            var callBack = $(this).data("related-callback");
            getDDLData(url, { 'id': value }, relatedInput, true, null, callBack);
        }
        else {
            var element = $("#" + relatedInput);
            $(element).val('').trigger("change");
        }
    });
    $(".ddl[data-related-input-id]").trigger("change");
    //Cascading

    if (jQuery().datepicker) {
        $('.datePicker').datepicker({
            orientation: "left",
            autoclose: true,
            format: "dd/mm/yyyy",
            language: 'tr',
            todayHighlight: true,
            useCurrent: false
        });

        $('.datePicker1').datepicker({
            orientation: "left",
            autoclose: true,
            format: "dd/mm/yyyy",
            language: 'tr',
            todayHighlight: true,
            useCurrent: false
        }).on("changeDate", function (selected) {
            var endDateElementStr = $(this).attr('data-enddateelement');
            var endDateElement = $('#' + endDateElementStr);
            $(endDateElement).val('');
            if (selected.date != null) {
                var minDate = new Date(selected.date.valueOf());
                $(endDateElement).datepicker('setStartDate', minDate);
            }
        });

        $('.datePicker2').datepicker({
            orientation: "left",
            autoclose: true,
            format: "dd/mm/yyyy",
            language: 'tr',
            todayHighlight: true,
            useCurrent: false
        });
    }

    $('div.EkBilgi').each(function (i) {
        var html = $(this).html();
        var yeniHtml = '<div class="note note-info"><h3><i class="fa fa-info YanipSon"></i>&nbsp;Uyarı</h3><p> ' + html + '</p></div>';
        $(this).html(yeniHtml);
    });
    //DatePicker

    //*Printer versiyon olşturucu*/
    $(".print").click(function (event) {
        event.preventDefault();
        window.print();
    });

    $(".cbTumunuSec").on("click", (function (e) {
        if ($(".cbTumunuSec").is(':checked')) {
            $(".cbSec:enabled").prop('checked', true);
        }
        else {
            $(".cbSec").prop('checked', false);
        }
    }));
    $(".cbSec input").on("click", (function (e) {

        var cbAdet = 0;
        var isaretli = 0;
        var isaretsiz = 0;
        $(".cbSec").each(function (i) {
            cbAdet++;
            if ($(this).is(':checked')) {
                isaretli++;
            }
            else {
                isaretsiz++;
            }
        });
        if (cbAdet == isaretli) {
            $('.cbTumunuSec').prop('checked', true);
        }
        else {
            $('.cbTumunuSec').prop('checked', false);
        }
    }));



    $(document).on('click', ".modalizer", function (e) {

        e.preventDefault();
        debugger;
        var url = $(this).attr("href");
        var title = $(this).data("pagetitle");
        if (title == undefined || title == '')
            title = 'Ön İzleme';
        var icon = $(this).data("pageicon");
        if (icon == undefined || icon == '')
            icon = 'fa fa-print';
        var height = $(this).data("pageheight");
        if (height == undefined || height == '')
            height = '750';
        //-----------------------------

        var modalName = RandomIdGetir();

        var sb = '';
        sb += '<div id="' + modalName + '" class="modal fade mesajModal " role="dialog" aria-hidden="true">';
        sb += '<div class="modal-dialog modal-size-80 modal-dialog-centered" role="document">';

        sb += '<div class="modal-content">';
        sb += '  <div class="modal-header alert-success">';
        sb += '  <h5 class="modal-title"><i class="' + icon + '"></i> ' + title + '</h5>';
        sb += '<button type="button" class="close" data-dismiss="modal" aria-label="Kapat"><span aria-hidden="true">&times;</span></button>';
        sb += '</div>';
        sb += '<div class="modal-body" id="modalBody" style="padding:0px !important;height:' + height + 'px;">';
        sb += '<iframe id="ifrmModalPopupSayfa" style="width:100%;height:' + (height - 30) + 'px" frameborder="0"/>';
        sb += '</div>';
        sb += '</div>';
        sb += '</div>';
        sb += '</div>';

        $("#dialogContainer").append(sb);
        var modalObj = $("#" + modalName + "");

        modalObj.find('button.close').css({ 'color': modalObj.find('h4').css('color') });

        modalObj.modal({ backdrop: 'static' });

        $(modalObj).on('shown.bs.modal', function () {
            blockUI();
            $('#ifrmModalPopupSayfa').attr('src', url);
            $('#ifrmModalPopupSayfa').on('load', function () {
                unblockUI();
            });
        });

        $(modalObj).on('hidden.bs.modal', function (e) {
            $('#ifrmModalPopupSayfa').attr('src', '');
            $(this).remove();
            $('#ifrmModalPopupSayfa').remove();
        });

        $(modalObj).modal({
            backdrop: 'static'
        });

        modalObj.find('button.close').css({ 'color': modalObj.find('h4').css('color') });
    });

    function EncryptData(value) {
        $.ajax({
            url: '~/Common/EncryptData',
            data: { d: value },
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                ShowMessage(xhr.responseJSON.Message, 2, true);
            }
        });
    }

    function DecryptData(value) {
        $.ajax({
            url: '~/Common/DecryptData',
            data: { d: value },
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                ShowMessage(xhr.responseJSON.Message, 2, true);
            }
        });
    }

    $(document).on('click', ".onay:not([disabled])", function (e) {
        e.preventDefault();
        //var result = true;
        //var beforeConfirm = $(this).data("beforeconfirm");
        //if (!wsLib.isNullOrEmpty(beforeConfirm) && window[beforeConfirm] != "" && window[beforeConfirm] != undefined) {
        //    result = window[beforeConfirm]();
        //    alert(result);
        //}
        //if (result) {
        OnayGoster($(this));
        //}
    });

    $(document).on('click', ".onayValid:not([disabled])", function (e) {
        var ret = true;
        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
            ret = Page_IsValid;
        }
        if (ret) {
            e.preventDefault();
            OnayGoster($(this));
        }
    });

    $("textarea").keyup(function (e) {
        if ($(this).attr("maxlength") == undefined || $(this).attr("maxlength") == -1) {

        }
        else {
            if (this.value.length > $(this).attr("maxlength"))
                this.value = this.value.substring(0, $(this).attr("maxlength"));

            if ($(this).next().attr("id") === $(this).attr("id") + "_sayac")
                $(this).next().remove();

            var position = $(this).position();
            var uyari = ($(this).attr("maxlength") - this.value.length) == 0 ? 'danger' : 'success';

            var x = $(this).offset().left;
            var y = $(this).offset().top + $(this).height() + 500;
            $(this).after('<span class="label label-' + uyari + '" style="white-space:nowrap !important; font-size: 10px; position: absolute;z-index:999999;" id="' + $(this).attr("id") + '_sayac">' + '<span class="fa fa-info-circle" style="float: left; margin-right: .3em;"></span>Kalan Karakter:<strong>' + ($(this).attr("maxlength") - this.value.length) + '</strong></span>');
            $(this).focusin(function () {
                $('#' + $(this).attr("id") + "_sayac").fadeIn(250);
            });

            $(this).focusout(function () {
                $('#' + $(this).attr("id") + "_sayac").fadeOut(250);
            });
        }
    });

    $('td.kisaltma').each(function (i) {
        var limit = 10;
        var icerik = $(this).text().toString().trim();
        if (icerik.length > 10) {
            var index = icerik.indexOf(' ');
            if (index !== 0) {
                $(this).html('<a class="btn btn-default" href="#">' + icerik.substring(0, index) + '...&nbsp;<i class="fa fa-comment-o"></i></a>');
                var a = $(this).find('a');
                $(a).attr('data-toggle', 'popover');
                $(a).attr('data-content', icerik);
                $(a).attr('data-placement', 'top');
                $(a).attr('data-trigger', 'click hover');
            }
        }
    });

    if ($(".YanipSon").length > 0) {
        setInterval(function () {
            $(".YanipSon").fadeOut().fadeIn();
        }, 1500);
    }

    if ($('.typeahead').length) {

        $('.typeahead').each(function (index) {
            var self = $(this);
            var showDescription = self.data("showdesc");
            var isMultiple = self.data("multiple");
            var targetId = self.data("target");
            var connectedWith = self.data("connectedwith");

            var url = self.data("url-value");
            if (isMultiple === "True") {
                url = self.data("url-values");
            }

            //içi dolu ise ajax ile dolu set et
            var selectedId = $("#" + targetId).val();
            if (selectedId !== "" && selectedId !== "0" && selectedId !== undefined) {

                var acRequest = $.ajax({
                    data: {
                        __RequestVerificationToken: securityToken,
                        selectedId: selectedId
                    },
                    url: url,
                    method: "POST",
                    async: true
                });

                acRequest.done(function (data) {
                    if (isMultiple === "True") {
                        $.each(data,
                            function (i, item) {
                                $("<span></span>")
                                    .addClass("badge badge-info")
                                    .data("vv", item.id)
                                    .text(item.text + " ")
                                    .append($("<i></i>")
                                        .addClass("fa fa-times")
                                        .click(function () {
                                            var v = $("#" + targetId).val();
                                            var vtr = $(this).parent().data("vv");
                                            var vSplitted = v.split(",");
                                            var vNewValue = _.without(vSplitted, vtr);
                                            $("#" + targetId).val(vNewValue.toString());
                                            $(this).parent().remove();
                                        })
                                    )
                                    .appendTo($("#ac_selection_" + targetId));
                                $("#ac_selection_" + targetId).append(" ");
                            });
                    } else {
                        self.val(data.text).trigger("change");
                        self.prop('readonly', true).addClass("text-danger");
                    }

                });

                acRequest.fail(function (msg) {
                    self.val("Yüklenemedi!!");
                    self.prop('disabled', true).addClass("text-danger");
                });

            }


            if (isMultiple === "True") {

                self.on("keydown",
                    function (event) {
                        if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                            event.preventDefault();
                        }
                    });

            }

            if (connectedWith !== undefined) {
                if (selectedId !== "" && selectedId !== "0" && selectedId !== undefined) {
                    $("#_" + connectedWith).data("rootid", selectedId).addClass("connected");

                } else {
                    $("#_" + connectedWith).prop('disabled', true);
                }
            }

            self.autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: self.data("url"),
                        dataType: "json",
                        data: {
                            key: extractLast(request.term),//request.term
                            rootId: self.data("rootid")
                        },
                        success: function (data) {

                            response($.map(data,
                                function (item) {
                                    return {
                                        label: item.description,
                                        value: item.text,
                                        id: item.id
                                    };
                                }));
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            ShowMessage(xhr.responseJSON.Message, "error", true);
                        }
                    });
                },
                minLength: 2,
                //   multiselect: isMultiple === "True"?true:false,

                select: function (event, ui) {


                    if (isMultiple === "True") {

                        var v = [];
                        if ($("#" + targetId).val() !== "")
                            v = split($("#" + targetId).val());

                        if (!_.contains(v, ui.item.id)) {
                            $("<span></span>")
                                .addClass("badge badge-info")
                                .data("vv", ui.item.id)
                                .text(ui.item.value + " ")
                                .append($("<i></i>")
                                    .addClass("fa fa-times")
                                    .click(function () {
                                        var v = $("#" + targetId).val();
                                        var vtr = $(this).parent().data("vv");
                                        var vSplitted = v.split(",");
                                        var vNewValue = _.without(vSplitted, vtr);
                                        $("#" + targetId).val(vNewValue.toString());
                                        $(this).parent().remove();
                                    })
                                )
                                .appendTo($("#ac_selection_" + targetId));
                            $("#ac_selection_" + targetId).append(" ");
                            v.push(ui.item.id);


                            $("#" + targetId).val(v.toString());
                            self.val("");
                        }
                        self.val("");

                    }
                    else {
                        $("#" + targetId).val(ui.item.id);
                        self.val(ui.item.value);

                        var locked = self.data("locked-after-select");
                        if (locked != "False")
                            self.prop("readonly", true).addClass("text-info");
                    }

                    if (self.data("callback") !== undefined) {
                        window[self.data("callback")](ui.item);
                    }

                    if (targetId !== undefined) {
                        $("#_" + connectedWith).data("rootid", ui.item.id).addClass("connected").prop('disabled', false).focus();

                    }

                    self.blur();
                    return false;


                }

            });

            self.focus(function () {
                $(this).autocomplete("search", "");
            }).autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>").append(item.value).appendTo(ul);
            };

        });

        $(".autocomplete_clear").click(function (e) {
            e.preventDefault();

            var target = $(this).data("target");
            var self = $("#_" + target);
            var connectedId = self.data("connectedwith");
            self.val('').removeAttr("readonly").removeClass("autocomplete_loading text-danger").focus();
            $("#" + target).val("");


            if (self.data("clearcallback") !== undefined) {
                window[self.data("clearcallback")](e);
            }

            if (connectedId !== undefined) {
                //bağlantılı ise bağlı olduğu ototamamları da sıfırla
                $("#ac_clear_" + connectedId + " ").trigger("click");
                $("#_" + connectedId).prop('disabled', true);

            }

            $("#ac_selection_" + target).html("");
        });

        $(".listopener").click(function (e) {
            e.preventDefault();

            var type = $(this).data("type");
            var target = $(this).data("target");
            var self = $("#_" + target);
            var url = self.data("url");
            var viewUrl = self.data("viewurl");
            var mymodal;




            if (type === "Tree") { //tree
                url = url.replace("AjaxAra", "AjaxAraTree");
                mymodal = bootbox.dialog({
                    title: "<i class='fa fa-sitemap'></i> Ağaçtan Seçiniz",
                    message: '<div id="autocompTree"></div>'
                });
                $('#autocompTree')
                    .on('changed.jstree', function (e, data) {
                        var i, j, r = [], v = [];
                        for (i = 0, j = data.selected.length; i < j; i++) {
                            r.push(data.instance.get_node(data.selected[i]).text);

                            v.push(data.instance.get_node(data.selected[i]).id);
                        }
                        self.val(r.join(', ')).trigger("change");
                        $("#" + target).val(v.join(', '));

                        self.prop('readonly', true).addClass("text-danger");

                        if (self.data("callback") !== undefined) {
                            window[self.data("callback")](data.instance.get_node(data.selected[0]));
                        }
                        mymodal.modal("hide");

                    })
                    .jstree({
                        'core': {
                            'themes': {
                                'name': 'proton',
                                'responsive': true
                            },
                            'data': {
                                "url": url,
                                "data": function (node) {
                                    return { "treenodeid": node.id };
                                }
                            }
                        }
                    });
            }
            else if (type === "CustomView") {
                if (viewUrl === "") {
                    alert("viewUrl parametresi ayarlanmamış!");
                } else {


                    var modal_width = '800';
                    var modal_height = '400';

                    if (viewUrl.contains("?")) {
                        viewUrl = viewUrl + "&layout=modal";
                    } else {
                        viewUrl = viewUrl + "?layout=modal";
                    }

                    mymodal = bootbox.dialog({
                        title: $(this).data("original-title"),
                        backdrop: true,
                        closeButton: true,
                        animate: true,
                        className: "my-modal",
                        size: "large",
                        message: '<iframe style="width:100%;height:99%;min-height:' + modal_height + 'px;border:0;"  src="' + viewUrl + '"></iframe>'
                    }).resizable();

                }


            }


            else if (type === "List") // List veya none
            {
                mymodal = bootbox.dialog({
                    title: "<i class='fa fa-list-ul'></i> Listeden Seçiniz",
                    message: '<div id="autocompList"><input type="text" class="form-control" placeholder="aramak için yazmaya başlayınız" id="autocompListKey"/></div>',
                    buttons: {
                        nextbtn: {
                            label: '<i class="fa fa-chevron-right"></i>',
                            className: "btn btn-primary",
                            callback: function () {
                            }
                        },
                        prvsbtn: {
                            label: '<i class="fa fa-chevron-left"></i>',
                            className: "btn btn-primary pull-left",
                            title: "Önceki Sayfa",
                            callback: function () {
                            }
                        }
                    }

                });
                url = url + "&key=" + $("#autocompListKey").val();
                $.getJSON(url, function (data) {
                    var items = [];
                    $.each(data, function (key, val) {
                        items.push('<tr class="pointer" id="tr_' + key + '" data-value="' + val.id + '" data-label="' + val.text + '" data-desc="' + val.description + '"><td>' + val.text + '</td><td>' + val.description + '</td></tr>');
                    });

                    $('<table/>', {
                        'data-start': 0,
                        'class': 'table table-striped table-hover autocompListTable',
                        html: items.join('')
                    }).appendTo($("#autocompList"));
                    $('table.autocompListTable tr')
                        .on('click', function (e, data) {
                            var tr = $(this);
                            self.val(tr.data("label")).trigger("change").prop('readonly', true).addClass("text-danger");
                            $("#" + target).val(tr.data("value"));

                            mymodal.modal("hide");
                        });

                });
            }
            /*    $(mymodal).draggable({
                    handle: ".modal-header"
                }); */
        });
    }
    $(document).on("click", ".ajaxButton",
        function (event) {

            event.preventDefault();

            var btn = $(this);

            var frm = $('#' + btn.data("relatedcontainer"));
            frm.removeData('validator');
            frm.removeData('unobtrusiveValidation');

            $.validator.unobtrusive.parse(frm);

            if (!$(frm).valid()) {
                $("label.error").remove();
                return false;
            }

            blockUI(frm);

            var url = $(this).data("action");

            var data = frm.serializeObject();
            var request = $.ajax({
                url: url,
                method: 'POST',
                data: data,
                dataType: "json"
            });

            request.done(function (ajaxdata, textStatus, jqXHR) {
                if (ajaxdata.RedirectUrl != undefined && ajaxdata.RedirectUrl != "" && ajaxdata.State == 1) {
                    window.location = ajaxdata.RedirectUrl;
                } else {
                    unblockUI(frm);
                    ShowMessage(ajaxdata.Message, ajaxdata.MessageType, true);
                }
                var successCallBack = btn.attr("ajaxsuccesscallback");
                if (successCallBack != undefined && successCallBack != "") {
                    window[successCallBack](ajaxdata, textStatus, jqXHR);
                }
            });

            request.fail(function (jqXHR, textStatus) {

                var msg = jQuery.parseJSON(jqXHR.responseText);
                ShowMessage(msg.Message, 2, true);
                unblockUI(frm);
                var failCallback = btn.attr("ajaxfailcallback");
                if (failCallback != undefined && failCallback != "") {
                    window[failCallback](jqXHR, textStatus);
                }
            });

            return false;
        });


    $(document).on("submit", "div.ajaxFormWithConfirm form, form.ajaxFormWithConfirm",
        function (event) {

            event.preventDefault();
            var frm = $(this);

            frm.removeData('validator');
            frm.removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse(frm);

            if (!$(frm).valid()) {
                $("label.error").remove();
                return false;
            }

            var confirmbuttontext = "Evet";
            if (frm.attr('confirm-button-text') != undefined && frm.attr('confirm-button-text') != "")
                confirmbuttontext = frm.attr('confirm-button-text');

            var cancelbuttontext = "Hayır";
            if (frm.attr('cancel-button-text') != undefined && frm.attr('cancel-button-text') != "")
                cancelbuttontext = frm.attr('cancel-button-text');




            var swallconfirmresult = swal({
                title: frm.attr('confirm-text-header'),
                html: frm.attr('confirm-text'),
                type: "warning",
                showCancelButton: true,
                closeOnConfirm: false,
                confirmButtonText: confirmbuttontext,
                cancelButtonText: cancelbuttontext,
                width: '650px'
            });

            swallconfirmresult.then(function (isConfirm) {
                if (isConfirm.value == true) {

                    blockUI(frm);

                    var url = frm.attr('action');

                    var formdata = frm.serializeObject();
                    var request = $.ajax({
                        url: url,
                        method: frm.attr('method'),
                        data: formdata
                    });

                    request.done(function (ajaxdata, textStatus, jqXHR) {

                        if (ajaxdata.RedirectUrl != undefined && ajaxdata.RedirectUrl != "") {
                            window.location = ajaxdata.RedirectUrl;
                        } else {
                            if (ajaxdata.Message != undefined && ajaxdata.Message != "")
                                ShowMessage(ajaxdata.Message, ajaxdata.State, true);
                        }

                        var successCallBack = frm.attr("ajaxsuccesscallback");
                        if (successCallBack != undefined && successCallBack != "") {
                            window[successCallBack](ajaxdata, textStatus, jqXHR);
                        }

                    });

                    request.fail(function (jqXHR, textStatus) {
                        var msg = jQuery.parseJSON(jqXHR.responseText);
                        ShowMessage(msg.Message, 2, true);
                        var failCallback = frm.attr("ajaxfailcallback");
                        if (failCallback != undefined && failCallback != "") {
                            window[failCallback](jqXHR, textStatus);
                        }
                    });

                    request.always(function () {
                        unblockUI(frm);
                    });

                }
            });
            return false;
        }
    );

    $(document).on("submit", "div.ajaxForm form, form.ajaxForm",
        function (event) {

            event.preventDefault();
            var frm = $(this);

            frm.removeData('validator');
            frm.removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse(frm);

            if (!$(frm).valid()) {
                return false;
            }

            blockUI(frm);
            var url = frm.attr('action');

            var formdata = frm.serializeObject();
            var request = $.ajax({
                url: url,
                method: frm.attr('method'),
                data: formdata
            });

            request.done(function (ajaxdata, textStatus, jqXHR) {

                if (ajaxdata.RedirectUrl != undefined && ajaxdata.RedirectUrl != "" && ajaxdata.State == 1) {
                    window.location = ajaxdata.RedirectUrl;
                } else {
                    if (ajaxdata.State == 3) {
                        ShowMessageCustomSwal(ajaxdata.ModelStr);
                        return;
                    }
                    else {
                        if (ajaxdata.Message != undefined && ajaxdata.Message != "") {
                            if (ajaxdata.State == 1) {
                                ShowMessage(ajaxdata.Message, 3, true);
                            }

                            else {
                                ShowMessage(ajaxdata.Message, 2, true);
                            }
                        }
                    }
                }
                var successCallBack = frm.attr("ajaxsuccesscallback");
                if (successCallBack != undefined && successCallBack != "") {
                    window[successCallBack](ajaxdata, textStatus, jqXHR);
                }
                unblockUI(frm);
            });

            request.fail(function (jqXHR, textStatus) {
                var msg = jQuery.parseJSON(jqXHR.responseText);
                ShowMessage(msg.Message, 1, true);
                unblockUI(frm);
                var failCallback = frm.attr("ajaxfailcallback");
                if (failCallback != undefined && failCallback != "") {
                    window[failCallback](jqXHR, textStatus);
                }
            });
            return false;

        });

    //fileUpload Helper'ı seçilen dosyanın yalnız isminin görüntülenmesi.
    $('.custom-file-input').on('change', function () {
        let fileName = $(this).val().split('\\').pop();
        $(this).next('.custom-file-label').addClass("selected").html(fileName);
    });

    ActivateInputProperties();

    $("body").on("click", '.btn-ajaxbutton,.btn-ajaxbutton-confirm', function (evt) {

        evt.preventDefault();
        var url = $(this).data('url');
        var btn = $(this);

        var dataParameter = {};
        if (!($(this).data('parameter') == '' || $(this).data('parameter') == undefined)) {
            dataParameter = $(this).data('parameter');
        }

        var method = "GET";
        if ($(this).data('ajaxsendmethod') != undefined && $(this).data('ajaxsendmethod') != "")
            method = $(this).data('ajaxsendmethod');

        if ($(this).hasClass('btn-ajaxbutton')) {
            $.ajax({
                url: url,
                data: dataParameter,
                dataType: "json",
                type: method,
                error: function (data) {

                    if (!wsLib.isNullOrEmpty(data) &&
                        !wsLib.isNullOrEmpty(data.responseJSON) &&
                        !wsLib.isNullOrEmpty(data.responseJSON.Message)) {
                        ShowMessage(data.responseJSON.Message, 1, true);
                    }
                    else {
                        ShowMessage("İşlem Yapılamadı!", 2, true);
                    }
                },
                success: function (ajaxdata) {
                    if (ajaxdata != null && ajaxdata) {
                        var successCallBack = btn.attr("ajaxsuccesscallback");
                        if (successCallBack != undefined && successCallBack != "") {
                            window[successCallBack]();
                        }
                    }
                },

                beforeSend: function () {
                    blockUI("body", "İşlem Yapılıyor...");
                },
                complete: function (ajaxdata, textStatus, jqXHR) {

                    unblockUI('body');

                    if (ajaxdata.responseJSON.RedirectUrl != undefined &&
                        ajaxdata.responseJSON.RedirectUrl != "" &&
                        ajaxdata.responseJSON.State == 1) {
                        window.location = ajaxdata.responseJSON.RedirectUrl;
                    }
                }
            });
        }
        else {

            var question = "Bu İşlemi yapmak istediğinize emin misiniz?";
            if (btn.attr("confirm-msg") != undefined && btn.attr("confirm-msg") != "")
                question = btn.attr("confirm-msg");

            swal({
                title: "Uyarı",
                html: question,
                type: "question",
                showCancelButton: true,
                closeOnConfirm: false,
                confirmButtonText: "Evet, devam et",
                cancelButtonText: "Hayır, iptal et",
                width: '500px'
            }).then(function (isConfirm) {
                if (isConfirm.value == true) {

                    $.ajax({
                        url: url,
                        data: dataParameter,
                        dataType: "json",
                        type: method,
                        error: function (data) {

                            if (!wsLib.isNullOrEmpty(data) &&
                                !wsLib.isNullOrEmpty(data.responseJSON) &&
                                !wsLib.isNullOrEmpty(data.responseJSON.Message)) {
                                ShowMessage(data.responseJSON.Message, 1, true);
                            }
                            else {
                                ShowMessage("İşlem Yapılamadı!", 2, true);
                            }
                        },
                        success: function (ajaxdata) {

                            if (ajaxdata.RedirectUrl != undefined && ajaxdata.RedirectUrl != "" && ajaxdata.State == 1) {
                                window.location = ajaxdata.RedirectUrl;
                            } else {
                                if (ajaxdata.State == 3) {
                                    ShowMessageCustomSwal(ajaxdata.ModelStr);
                                    return;
                                }
                                else {
                                    if (ajaxdata.Message != undefined && ajaxdata.Message != "") {
                                        if (ajaxdata.State == 1) {
                                            ShowMessage(ajaxdata.Message, 3, true);
                                        }

                                        else {
                                            ShowMessage(ajaxdata.Message, 2, true);
                                        }
                                    }
                                }
                            }
                            var successCallBack = btn.attr("ajaxsuccesscallback");
                            if (successCallBack != undefined && successCallBack != "") {
                                window[successCallBack](ajaxdata);
                            }
                        },
                        beforeSend: function () {
                            blockUI("body", "İşlem Yapılıyor...");
                        },
                        complete: function (ajaxdata, textStatus, jqXHR) {
                            unblockUI('body');

                            if (ajaxdata.responseJSON.RedirectUrl != undefined &&
                                ajaxdata.responseJSON.RedirectUrl != "" &&
                                ajaxdata.responseJSON.State == 1) {
                                window.location = ajaxdata.responseJSON.RedirectUrl;
                            }
                        }
                    });
                }
            });
        }
    });

    $('input[jsFileValidation="true"]').bind('change', function (e) {
        var file = this.files[0];
        var fileExtension = file.name.split('.').pop();
        var fileSizeByte = file.size;
        var maxFileSizeMB = !wsLib.isNullOrEmpty($(this).attr("maxFileSizeMB")) ? parseInt($(this).attr("maxFileSizeMB")) : 5;
        var fileAcceptString = !wsLib.isNullOrEmpty($(this).attr("accept")) ? $(this).attr("accept") : "";
        var isFileExtensionControlDone = false;

        if (!wsLib.isNullOrEmpty(fileAcceptString)) {
            $.each(fileAcceptString.split(','), function (i, e) {
                if (fileExtension == e.split('.').pop()) {
                    isFileExtensionControlDone = true;
                }
            });
        }

        if (!isFileExtensionControlDone) {
            if (file.type != "application/pdf" && file.type != "image/tiff" && file.type != "image/jpeg" &&
                file.type != "image/png" && file.type != "image/jpg" && fileExtension != "rar" && fileExtension != "zip") {
                $(this).val(null);
                ShowMessage("Seçili dosya yüklenemez.Yüklenen dosyanın türü .PDF, .ZIP, .RAR, .PNG, .JPEG, .JPG , .TIF, .TIFF, " + fileAcceptString + " olmalıdır!", 1, true);
            }
        }

        if (fileSizeByte > maxFileSizeMB * 1024 * 1024) {
            $(this).val(null);
            ShowMessage("Yüklenen dosyanın boyutu, " + maxFileSizeMB + " MB ile sınırlıdır!", 1, true);
        }
    });

    $('.ajaxFileUploadDownloadOrDelete').each(function () {
        blockUI('body');
        var d = $(this);
        setFileUploadDivHtml(d.attr("id"));
        unblockUI('body');
    });


    $(document).on('change', '.ajaxFileUploadFileInput', function (e) {
        $(this).closest(".ajaxFileUpload").find('.ajaxFileUploadFileName').html('<strong>' + e.target.files[0].name + '</strong>');
    });



    $(document).on('click', '.ajaxFileUploadButton', function () {

        var d = $(this).closest(".ajaxFileUploadDownloadOrDelete");
        var fileUpload = $(this).closest(".ajaxFileUpload").find('.ajaxFileUploadFileInput');

        if (fileUpload != undefined && fileUpload != null) {
            var files = fileUpload.get(0).files;
            if (files.length > 0) {
                var postdata = new FormData();

                //ilgili datalari ekle
                $('#' + d.attr('upload-form-data') + ' :input').each(function () {
                    var input = $(this);
                    postdata.append(input.attr('name'), input.val());
                });

                $('.ajax-file-form-data :input').each(function () {
                    var input = $(this);
                    postdata.append(input.attr('name'), input.val());
                });


                postdata.append("uploadedFile", files[0]);
                var request = $.ajax({
                    url: d.attr('upload-url'),
                    type: "POST",
                    data: postdata,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                        blockUI('body');
                    },
                    complete: function () {
                        unblockUI('body');
                    }
                });
                request.done(function (ajaxdata, textStatus, jqXHR) {
                    d.attr("attachment-id", ajaxdata.encattachmentId);
                    d.attr("file-name", ajaxdata.fileName);
                    setFileUploadDivHtml(d.attr('id'));
                    showSuccessNotification("Dosya yükleme işlemi yapılmıştır.", "Bilgi");

                });
                request.fail(function (jqXHR, textStatus, errorThrown) {
                    var msg = jQuery.parseJSON(jqXHR.responseText);
                    ShowMessage(msg.Message, 2, true);
                });

            }
            else {
                showWarningNotification("İlk önce dosya seçiniz.", "Uyarı");
            }
        }
        else {
            showWarningNotification("Yüklenecek dosya bulunamadı!", "Uyarı");
        }
    });

    $(document).on("click", ".ajaxFileDownloadButton", function () {
        var d = $(this).closest(".ajaxFileUploadDownloadOrDelete");
        window.location = d.attr("download-url") + "?attachmentId=" + d.attr("attachment-id");
    });

    $(document).on("click", ".ajaxFileDeleteButton", function () {

        var d = $(this).closest(".ajaxFileUploadDownloadOrDelete");

        var swallconfirmresult = swal({
            title: "Dosya silinecek.",
            html: "Devam etmek istiyor musunuz?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: true,
            confirmButtonClass: "btn btn-danger",
            cancelButtonText: "Hayır, iptal et",
            confirmButtonText: "Evet, dosyayı sil.",

        });

        swallconfirmresult.then(function (isConfirm) {
            if (isConfirm.value == true) {

                var postdata = {};
                $('.ajax-file-form-data :input').each(function () {
                    var input = $(this);
                    postdata[input.attr('name')] = input.val();
                });

                postdata["attachmentid"] = d.attr("attachment-id");

                var request = $.ajax({
                    url: d.attr('delete-url'),
                    type: "POST",
                    data: postdata,
                    beforeSend: function () {
                        blockUI('body');
                    },
                    complete: function () {
                        unblockUI('body');
                    }
                });
                request.done(function (ajaxdata, textStatus, jqXHR) {
                    d.attr("attachment-id", "");
                    d.attr("file-name", "");
                    setFileUploadDivHtml(d.attr('id'));
                    showSuccessNotification("Dosya silme işlemi yapılmıştır.", "Bilgi");
                });
                request.fail(function (jqXHR, textStatus, errorThrown) {
                    var msg = jQuery.parseJSON(jqXHR.responseText);
                    ShowMessage(msg.Message, 2, true);
                });


            }
        });

    });

    $('.dtfiltersearch').on("click", function (e) {
        var inputs = $(this).closest('.card-body').find(':input');
        var searchfilterguid = $(this).closest('.card-body').data('searchfilterguid');
        var searchData = {};
        $(inputs).each(function () {
            debugger;
            var val = $(this).val();
            var name = $(this).attr("name");
            if ($(this).data("searchfiltername") != undefined &&
                $(this).data("searchfiltername") != null &&
                $(this).data("searchfiltername") != '')
                name = $(this).data("searchfiltername");
            searchData[name] = val;
        });
        wsLib.setSession(searchfilterguid, JSON.stringify(searchData));
        var table = $('table[data-searchfilterguid=' + searchfilterguid + ']');
        var gridFunctionName = $(table).data('function-name');
        window[gridFunctionName](searchData);
    });

    $('.dtfilterclear').on("click", function (e) {
        var searchfilterguid = $(this).closest('.card-body').data('searchfilterguid');
        var inputs = $(this).closest('.card-body').find(':input');
        $(inputs).each(function () {
            $(inputs).val('').trigger('change');
        });
        wsLib.removeSession(searchfilterguid);
    });


    if ($('div[data-searchfilterguid]').length > 0) {
        $('div[data-searchfilterguid]').each(function (i) {
            var searchfilterguid = $(this).data('searchfilterguid');
            if (searchfilterguid != '' &&
                searchfilterguid != undefined &&
                searchfilterguid != null) {
                var searchData = wsLib.getSession(searchfilterguid);
                if (searchData != '' &&
                    searchData != undefined &&
                    searchData != null &&
                    searchData.length > 0) {
                    var searchDataJson = JSON.parse(searchData);
                    $.each(searchDataJson, function (key, value) {
                        var el = $('#' + key);
                        if (el.length == 0) {
                            el = $('[name=' + key + ']');
                        }
                        if (el.length == 0) {
                            el = $('[data-searchfiltername=' + key + ']');
                        }
                        $(el).val(value).trigger('change');
                    });

                    var table = $('table[data-searchfilterguid=' + searchfilterguid + ']');
                    var gridFunctionName = $(table).data('function-name');
                    window[gridFunctionName](searchDataJson);
                }
            }
        });
    }
});
// $('...').scrollToMe();
jQuery.fn.extend({
    scrollToMe: function () {
        var x = jQuery(this).offset().top - 100;
        jQuery('html,body').animate({ scrollTop: x }, 500);
    }
});

function CheckTableHasRows(tableNameOrId) {
    var tr = $("#" + tableNameOrId + " > tbody > tr:not(.child.detail)");
    var count = 0;
    if (!$(tr).html().contains("kayıt") && !$(tr).html().contains("Kayıt")) {
        count = $(tr).length;
    }
    return count == 0 ? false : true;
}

function setFileUploadDivHtml(divid) {
    var d = $('#' + divid);
    if (d.attr('attachment-id') != "")
        d.html(getFileDownloadOrDeleteHtml(d.attr('file-name')));
    else
        d.html(getFileUploadHtml());

}

function getFileUploadHtml() {
    var sb = '<div class="ajaxFileUpload">';
    sb += '<p class="ajaxFileUploadFileName m--font-danger"></p>';
    sb += '<div class="row">';
    sb += '<div class="col-lg-2 col-sm-12">';
    sb += '<label class="btn btn-outline-dark">';
    sb += '<input type="file" style="display:none" class="ajaxFileUploadFileInput" accept="application/pdf, .zip, .rar, image/tiff, image/tif, image/jpeg, image/jpg" />';
    sb += '<span class="la la-file-text-o"></span>';
    sb += 'Dosya Seç';
    sb += '</label>';
    sb += '</div>';
    sb += '<div class="col-lg-10 col-sm-12">';
    sb += '<button type="button" class="ajaxFileUploadButton btn  btn-outline-primary">';
    sb += '<span>';
    sb += '<i class="la la-upload"></i>';
    sb += '<span>Yükle</span>';
    sb += '</span>';
    sb += '</button>';
    sb += '<span>&nbsp;&nbsp;Dosya seçip yükle butonuna basınız.</span>';
    sb += ' </div>';
    sb += '</div>';
    sb += '</div>'
    return sb;
}

function getFileDownloadOrDeleteHtml(filename) {

    var sb = '<div class="ajaxFileDownloadOrDelete">';
    sb += '<p class="ajaxFileUploadFileName">Yüklenen Dosya : &nbsp;&nbsp;<strong><span class="m--font-danger">' + filename + '</span></strong></p>';
    sb += '<div class="row">';
    sb += '<div class="col-lg-2 col-sm-12">';
    sb += '<button type="button" class="ajaxFileDownloadButton ExportButton btn  btn-outline-primary">';
    sb += '<span>';
    sb += '<i class="la la-download"></i>';
    sb += '<span>İndir</span>';
    sb += '</span>';
    sb += '</button>';
    sb += '</div>';
    sb += '<div class="col-lg-10 col-sm-12">';
    sb += '<button type="button" class="ajaxFileDeleteButton btn  btn-outline-danger">';
    sb += '<span>';
    sb += '<i class="la la-trash"></i>';
    sb += '<span>Sil</span>';
    sb += '</span>';
    sb += '</button>';
    sb += ' </div>';
    sb += '</div >';
    sb += '</div >';
    return sb;
}


