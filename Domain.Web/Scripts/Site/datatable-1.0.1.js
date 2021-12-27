$(function () {
    InitGridButtons();

    //$("body").on("click", ".parentTable > tbody > tr > td[tabindex=0]", function () {
    //    debugger;
    //    var tr = $(this).parent();
    //    var datatable = $(tr).closest(".bsDataTable");
    //    var partialUrl = $(datatable).data("detailtable-partial-url");

    //    var detailColumn = $(datatable).data("detail-column");
    //    var data = $(datatable).DataTable().row(tr).data();
    //    var postData = $.parseJSON(data[detailColumn]);

    //    if (!($(tr).hasClass("parent"))) {
    //        if ($(tr).next().hasClass("detail")) {
    //            $(tr).next().remove();
    //        }
    //        else {
    //            getData(partialUrl, null, function (data) {
    //                $(tr).after('<tr class="child detail"><td class="td-detail" colspan="100%"> ' + data + '</td></tr>');
    //                $(tr).addClass("parent");
    //                var functionName = $(tr).next().find(".bsDataTable").data('function-name');
    //                window[functionName](postData);
    //            });
    //        }
    //    }
    //    else {
    //        if ($(tr).find("td:hidden").length > 0 && $(tr).next().hasClass("child")) {
    //            getData(partialUrl, null, function (data) {
    //                $(tr).next().after('<tr class="child detail"><td class="td-detail" colspan="100%"> ' + data + '</td></tr>');
    //                var functionName = $(tr).next().next().find(".bsDataTable").data('function-name');
    //                window[functionName](postData);
    //            });
    //        }
    //        else {
    //            var trDetail = $(tr).next();
    //            if (trDetail.hasClass("detail")) {
    //                trDetail.remove();
    //                $(tr).removeClass("parent");
    //            }
    //        }
    //    }
    //});

    $("body").on("click", ".parentTable > tbody > tr > td[tabindex=0]", function () {
        var tr = $(this).closest('tr');
        var datatable = $(tr).closest(".bsDataTable");
        var partialUrl = datatable.data("detailtable-partial-url");
        var row = $(datatable).DataTable().row(tr);
        var rowData = row.data();
        var detailTableColumnId = datatable.attr("data-detail-column");

        var jsonData = JSON.parse('{"' + detailTableColumnId + '":"' + rowData[detailTableColumnId] + '"}');

        if (!($(tr).hasClass("parent"))) {
            if ($(tr).next().hasClass("detail")) {
                $(tr).next().remove();
            }
            else {
                getData(partialUrl, jsonData, function (data) {
                    $(tr).after('<tr class="child detail"><td class="td-detail" colspan="100%"> ' + data + '</td></tr>');
                    $(tr).addClass("parent");
                });
            }
        }
        else {
            if ($(tr).find("td:hidden").length > 0 && $(tr).next().hasClass("child")) {
                getData(partialUrl, jsonData, function (data) {
                    $(tr).next().after('<tr class="child detail"><td class="td-detail" colspan="100%"> ' + data + '</td></tr>');
                });
            }
            else {
                var trDetail = $(tr).next();
                if (trDetail.hasClass("detail")) {
                    trDetail.remove();
                    $(tr).removeClass("parent");
                }
            }
        }
    });

    $("body").on("change", ".grdChkAll", function () {
        var parentGrid = $(this).data("parent");
        var chkBoxList = $('#' + parentGrid).find('.grdChk');
        var state = $(this).is(":checked");
        $(chkBoxList).each(function (index) {
            $(this).prop('checked', state);
            if (state)
                $(this).closest('tr').addClass('selected');
            else
                $(this).closest('tr').removeClass('selected');
        });
    });



    $("body").on("change", ".grdChk", function () {

        var state = $(this).is(':checked');
        if (state)
            $(this).closest('tr').addClass('selected');
        else
            $(this).closest('tr').removeClass('selected');

        var cbCount = 0;
        var cbSigned = 0;
        var cbUnSigned = 0;
        var parentGrid = $(this).data("parent");
        var chkBoxList = $('#' + parentGrid).find('.grdChk');
        $(chkBoxList).each(function (index) {
            cbCount++;
            if ($(this).is(':checked')) {
                cbSigned++;
            } else {
                cbUnSigned++;
            }
        });
        if (cbCount == cbSigned) {
            $('#' + parentGrid).find('.grdChkAll').prop('checked', true);
        } else {
            $('#' + parentGrid).find('.grdChkAll').prop('checked', false);
        }
    });

    $("body").on("change", ".grdRb", function () {
        var name = $(this).attr('name');
        var allRb = $("input[name=" + name + "]");
        $(allRb).each(function () {
            $(this).closest('tr').removeClass('selected');
        });
        $(this).closest('tr').addClass('selected');
    });
});

jQuery.fn.dataTable.render.ellipsis = function (cutoff, wordbreak, escapeHtml) {
    var esc = function (t) {
        return t
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    };

    return function (d, type, row) {
        // Order, search, export and type get the original data
        
        if (type !== 'display') {
            return d;
        }

        if (typeof d !== 'number' && typeof d !== 'string') {
            return d;
        }

        d = d.toString(); // cast numbers

        if (d.length <= cutoff) {
            return d;
        }

        var shortened = d.substr(0, cutoff - 1);

        // Find the last white space character in the string
        if (wordbreak) {
            shortened = shortened.replace(/\s([^\s]*)$/, '');
        }

        // Protect against uncontrolled HTML input
        if (escapeHtml) {
            shortened = esc(shortened);
        }

        return '<span class="ellipsis" title="' + esc(d) + '">' + shortened + '&#8230;</span>';
    };
};

function GetSelectedRowsData(datatableId) {
    var trList = $('#' + datatableId).find("tbody").find('tr');
    var array = new Array();
    $(trList).each(function (index) {
        if ($(this).hasClass('selected')) {
            var row = $('#' + datatableId).DataTable().row($(this));
            var rowData = row.data();
            array.push(rowData);
        }
    });
    return array;
}

function GetRowsData(datatableId) {
    return $('#' + datatableId).DataTable().rows().data();
}

function InitGridButtons() {

    $("body").on("click", ".btn-partial", function () {
        var size = $(this).data("partial-size");
        if (wsLib.isNullOrEmpty(size)) {
            size = "modal-size-70";
        }
        debugger;
        var method = $(this).data("partial-method");
        if (wsLib.isNullOrEmpty(method)) {
            method = "GET";
        }

        var model = $(this).data("model");
        if (wsLib.isNullOrEmpty(model)) {
            model = undefined;
        }

        ShowPartial($(this).text(), "fa fa-save", $(this).data('action'), true, "", function () {
            ActivateDDLs();
            ActivateAutoComplete();
            ActivateInputProperties();
        }, true, "", model, method, size);
    });

    $("body").on("click", ".btnInfo", function () {
        ShowPartial("Bekleyen İşleme Ait Bilgiler", "fa fa-list", $(this).data("url"), true, null, null, null, null, null, "GET");
    });

    $("body").on("click", ".grid-btn-paymentapprove", function () {
        ShowPartial("Ödeme Olurları", "fa fa-list", $(this).data('url'), true, "", "", true, "", undefined, "GET");
    });

    $("body").on("click", ".grid-btn-insert", function () {
        var _url = $(this).data("action");
        window.location = _url;
    });

    $("body").on("click", '.grid-btn-delete', function (evt) {
        var dtRow = $(this).parents('tr');
        var _url = $(this).data("url");
        var table = $(this).closest("table").first();
        var swalText = $(this).data("swal-text");
        var successCallBackFunction = $(this).data("success-callback");

        if (wsLib.isNullOrEmpty(swalText)) {
            swalText = "Seçili kayıt silinecek!";
        }

        swal({
            title: "Devam etmek istiyor musunuz ?",
            text: swalText,
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır, iptal!",
            reverseButtons: !0
        }).then(function (e) {
            if (e.value) {
                $.ajax({
                    url: _url, data: { __RequestVerificationToken: $('[name=__RequestVerificationToken]').val() }, dataType: "json", type: "POST",
                    error: function (data) {
                        if (!wsLib.isNullOrEmpty(data) && !wsLib.isNullOrEmpty(data.responseJSON) && !wsLib.isNullOrEmpty(data.responseJSON.Message)) {
                            ShowMessage(data.responseJSON.Message, 2, true);
                        }
                        else {
                            ShowMessage("İşlem yapılamadı!", 2, true);
                        }
                    },
                    success: function (data) {
                        if (data != null && data) {
                            if (data.Result != undefined) {
                                if (data.Result == 1) {
                                    showSuccessNotification(data.Message);
                                    var dataTable = $(table).DataTable();
                                    dataTable.row(dtRow[0].rowIndex - 1).remove().draw();
                                    if (successCallBackFunction !== undefined && successCallBackFunction != '' && typeof (window[successCallBackFunction]) === "function") {
                                        window[successCallBackFunction]();
                                    }
                                }
                                else {
                                    showErrorNotification(data.Message);
                                }
                            } else {
                                showSuccessNotification("İşlem başarılı.", "Bilgi");
                                var dataTable = $(table).DataTable();
                                dataTable.row(dtRow[0].rowIndex - 1).remove().draw();
                                if (successCallBackFunction !== undefined && successCallBackFunction != '' && typeof (window[successCallBackFunction]) === "function") {
                                    window[successCallBackFunction]();
                                }
                            }
                        } else {
                            if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                                showErrorNotification("İşlem yapılamadı!", "Hata");
                            }
                        }
                    },
                    beforeSend: function () {
                        blockUI(table, "Lütfen bekleyiniz...");
                    },
                    complete: function () {
                        unblockUI(table);
                    }
                });
            }
        });
    });


    $("body").on("click", '.grid-btn-post', function (evt) {
        var dtRow = $(this).parents('tr');
        var _url = $(this).data("url");
        var table = $(this).closest("table").first();
        var swalText = $(this).data("swal-text"); 
        var swalSuccessMessage = $(this).data("swal-success-message"); 
        var swalErrorMessage = $(this).data("swal-error-message"); 
        var successCallBackFunction = $(this).data("success-callback");
        if (wsLib.isNullOrEmpty(swalSuccessMessage)) {
            swalSuccessMessage = "İşlem başarılı!";
        } 
        if (wsLib.isNullOrEmpty(swalErrorMessage)) {
            swalErrorMessage = "Sistemde beklenmedik bir hata oluştu.";
        }
        if (wsLib.isNullOrEmpty(swalText)) {
            customAjaxPost(_url, successCallBackFunction, swalSuccessMessage, swalErrorMessage, table, dtRow);
        } 
        if (!wsLib.isNullOrEmpty(swalText)) {
            swal({
                title: "Devam etmek istiyor musunuz ?",
                text: swalText,
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Evet",
                cancelButtonText: "Hayır, iptal!",
                reverseButtons: !0
            }).then(function (e) {
                if (e.value) {
                    customAjaxPost(_url, successCallBackFunction, swalSuccessMessage, swalErrorMessage, table,dtRow);
                }
            });
        }
        
    });

    function customAjaxPost(_url, successCallBackFunction,swalSuccessMessage, swalErrorMessage,table,dtRow) {
        $.ajax({
            url: _url, data: { __RequestVerificationToken: $('[name=__RequestVerificationToken]').val() }, dataType: "json", type: "POST",
            error: function (data) {
                if (!wsLib.isNullOrEmpty(data) && !wsLib.isNullOrEmpty(data.responseJSON) && !wsLib.isNullOrEmpty(data.responseJSON.Message)) {
                    ShowMessage(data.responseJSON.Message, 2, true);
                }
                else {
                    ShowMessage(swalErrorMessage, 2, true);
                }
            },
            success: function (data) {
                if (data != null && data) {
                    if (data.Result != undefined) {
                        if (data.Result == 1) {
                            showSuccessNotification(data.Message);
                            var dataTable = $(table).DataTable();
                            dataTable.row(dtRow[0].rowIndex - 1).remove().draw();
                            if (successCallBackFunction !== undefined && successCallBackFunction != '' && typeof (window[successCallBackFunction]) === "function") {
                                window[successCallBackFunction]();
                            }
                        }
                        else {
                            showErrorNotification(data.Message);
                        }
                    } else {
                        showSuccessNotification(swalSuccessMessage, "Bilgi");
                        var dataTable = $(table).DataTable();
                        dataTable.row(dtRow[0].rowIndex - 1).remove().draw();
                        if (successCallBackFunction !== undefined && successCallBackFunction != '' && typeof (window[successCallBackFunction]) === "function") {
                            window[successCallBackFunction]();
                        }
                    }
                } else {
                    if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                        showErrorNotification(swalErrorMessage, "Hata");
                    }
                }
            },
            beforeSend: function () {
                blockUI(table, "İşlem yapılıyor...");
            },
            complete: function () {
                unblockUI(table);
            }
        });
    }

    $("body").on("click", '.tbl-btn-delete', function (evt) {
        var dtRow = $(this).parents('tr');
        var table = $(this).closest('table');
        var _url = $(this).data("url");
        var swalText = $(this).data("swal-text");

        if (wsLib.isNullOrEmpty(swalText)) {
            swalText = "Seçili kayıt silinecek!";
        }

        swal({
            title: "Devam etmek istiyor musunuz ?",
            text: swalText,
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır, iptal!",
            reverseButtons: !0
        }).then(function (e) {
            if (e.value) {
                $.ajax({
                    url: _url, data: { __RequestVerificationToken: $('[name=__RequestVerificationToken]').val() }, dataType: "json", type: "POST",
                    error: function (data) {
                        if (!wsLib.isNullOrEmpty(data) && !wsLib.isNullOrEmpty(data.responseJSON) && !wsLib.isNullOrEmpty(data.responseJSON.Message)) {
                            ShowMessage(data.responseJSON.Message, 2, true);
                        }
                        else {
                            ShowMessage("Kayıt Silinemedi", 2, true);
                        }
                    },
                    success: function (data) {

                        if (data != null && data) {
                            if (data.Result != undefined) {
                                if (data.Result == 1) {
                                    showSuccessNotification(data.Message);
                                    dtRow.remove();
                                    if (table.find("tbody tr").length == 0) {
                                        table.find("tbody").html("<tr> <td colspan='" + $(table).find("thead tr:first th").length + "' class='text-center'> Kayıt bulunmamaktadır! </td></tr>");
                                    }
                                }
                                else {
                                    showErrorNotification(data.Message);
                                }
                            } else {
                                showSuccessNotification("Kayıt başarı ile silindi!", "Bilgi");
                                dtRow.remove();
                                if (table.find("tbody tr").length == 0) {
                                    table.find("tbody").html("<tr> <td colspan='" + $(table).find("thead tr:first th").length + "' class='text-center'> Kayıt bulunmamaktadır! </td></tr>");
                                }
                            }
                        } else {
                            if (errorCallback && typeof (errorCallback) === "function") { errorCallback(); } else {
                                showErrorNotification("Kayıt silinemedi!", "Hata");
                            }
                        }
                    },
                    beforeSend: function () {
                        blockUI("body", "Kayıt Siliniyor...");
                    },
                    complete: function () {
                        unblockUI('body');
                    }
                });
            }
        });
    });

    $("body").on("click", ".grid-btn-export", function () {
        var filter = $("#SearchFilters").val();
        var _url = $(this).data("action");
        var $iframe, iframe_doc, iframe_html;
        if (($iframe = $('#download_iframe')).length === 0) {
            $iframe = $("<iframe id='download_iframe' style='display: none' src='about:blank'></iframe>").appendTo("body");
        }
        iframe_doc = $iframe[0].contentWindow || $iframe[0].contentDocument;
        if (iframe_doc.document) {
            iframe_doc = iframe_doc.document;
        }
        iframe_html = "<html><head></head><body><form method='POST' action='" + _url + "'>"
        iframe_html += "<input type='hidden' name='searchFilters' value='" + filter + "'>";
        iframe_html += "</form></body></html>";
        iframe_doc.open();
        iframe_doc.write(iframe_html);
        $(iframe_doc).find('form').submit();
    });
}

var oldExportAction = function (self, e, dt, button, config) {
    if (button[0].className.indexOf('buttons-excel') >= 0) {
        if ($.fn.dataTable.ext.buttons.excelHtml5.available(dt, config)) {
            $.fn.dataTable.ext.buttons.excelHtml5.action.call(self, e, dt, button, config);
        }
        else {
            $.fn.dataTable.ext.buttons.excelFlash.action.call(self, e, dt, button, config);
        }
    }
    else if (button[0].className.indexOf('buttons-pdf') >= 0) {
        if ($.fn.dataTable.ext.buttons.pdfHtml5.available(dt, config)) {
            $.fn.dataTable.ext.buttons.pdfHtml5.action.call(self, e, dt, button, config);
        } else {
            $.fn.dataTable.ext.buttons.pdfFlash.action.call(self, e, dt, button, config);
        }
    }
    else if (button[0].className.indexOf('buttons-print') >= 0) {
        $.fn.dataTable.ext.buttons.print.action(e, dt, button, config);
    }
};

var newExportAction = function (e, dt, button, config) {
    var self = this;
    var oldStart = dt.settings()[0]._iDisplayStart;

    dt.one('preXhr', function (e, s, data) {
        // Just this once, load all data from the server...
        data.start = 0;
        data.length = 2147483647;

        dt.one('preDraw', function (e, settings) {
            // Call the original action function 
            oldExportAction(self, e, dt, button, config);

            dt.one('preXhr', function (e, s, data) {
                // DataTables thinks the first item displayed is index 0, but we're not drawing that.
                // Set the property to what it was before exporting.
                settings._iDisplayStart = oldStart;
                data.start = oldStart;
            });

            // Reload the grid with the original page. Otherwise, API functions like table.cell(this) don't work properly.
            setTimeout(dt.ajax.reload, 0);

            // Prevent rendering of the full data to the DOM
            return false;
        });
    });

    // Requery the server with the new one-time export settings
    dt.ajax.reload();
};

