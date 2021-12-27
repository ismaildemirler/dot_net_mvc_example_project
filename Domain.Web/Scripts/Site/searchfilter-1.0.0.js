(function ($) {
    $.fn.searchfilter = function (options) {
        var defaults = { defaultfilter: "" };
        var settings = $.extend(defaults, options);
        this.data("settings", settings);

        return this.each(function (index) {
            selectionPanel = $(this).find(".list-group-selecteditems");
            selectionControl = $(this).find(".list-group-searchfilters");
            filterKey = (window.location.pathname).replace("/", "");

            if (settings.defaultfilter != "") {
                selectionControl.val(settings.defaultfilter);
            }

            $(selectionPanel).on("click", "a", function () {
                var tmpListID = $(this).data("list");
                var tmpValue = $(this).data("value");
                var elm = $('#' + tmpListID + ' a[data-value="' + tmpValue + '"]'); 
                $(elm).removeClass("selected");
                var i = $(elm).find("i");
                if ($(i).hasClass("fa-check-square-o")) {
                    $(i).removeClass("fa-check-square-o").addClass("fa-square-o");
                }
                else if ($(i).hasClass("fa-circle")) {
                    $(i).removeClass("fa-circle").addClass("fa-circle-o");
                }
                $(this).remove();
                setSelectionPanelVisibility();
            });

            $(".filter-group > .card-header a").click(function () {
                var tmpIcon = $(this).find("i");
                if ($(tmpIcon).hasClass("fa-caret-down")) { $(tmpIcon).removeClass("fa-caret-down").addClass("fa-caret-up"); }
                else { $(tmpIcon).removeClass("fa-caret-up").addClass("fa-caret-down"); }
            });

            $(this).on("click", ".filter-group > .list-group a.list-group-item", function () {
                var tmpParent = $(this).closest(".selectable");
                var tmpParentID = tmpParent.attr("id");
                var tmpType = tmpParent.data("type");
                var tmpValue = $(this).data("value");
                var targetID = tmpParent.data("targetid");
                var targetURL = tmpParent.data("targeturl");
                var tmpListGroupHeader = $(this).closest(".list-group-parent-container");

                if ($(this).hasClass("selected")) {
                    $(this).removeClass("selected");
                    var i = $(this).find("i");
                    if ($(i).hasClass("fa-check-square-o")) {
                        $(i).removeClass("fa-check-square-o").addClass("fa-square-o");
                    }
                    else if ($(i).hasClass("fa-circle")) {
                        $(i).removeClass("fa-circle").addClass("fa-circle-o");
                    }
                    $(selectionPanel).find('a[data-value="' + tmpValue + '"]').trigger("click");
                }
                else {
                    var tmpTitle = tmpParent.data("title");
                    var tmpParentTitle = "";
                    if (tmpListGroupHeader.length > 0) {
                        tmpParentTitle = tmpListGroupHeader.prev().text() + " - ";
                    }
                    if (tmpTitle != "") { tmpTitle += " : " + tmpParentTitle + $(this).text(); }
                    else { tmpTitle = tmpParentTitle + $(this).text(); }

                    if ($(tmpParent).hasClass("radio")) {
                        $(selectionPanel).find("a[data-list=" + tmpParentID + "]").trigger("click");
                    }

                    $(this).addClass("selected");
                    var i = $(this).find("i");
                    if ($(i).hasClass("fa-square-o")) {
                        $(i).removeClass("fa-square-o").addClass("fa-check-square-o");
                    }
                    else if ($(i).hasClass("fa-circle-o")) {
                        $(i).removeClass("fa-circle-o").addClass("fa-circle");
                    }
                    $(selectionPanel).append('<a class="list-group-item" data-list="' + tmpParentID + '" data-value="' + tmpValue + '" data-value2=""  data-type="' + tmpType + '" href="#"><i class="fa fa-times-circle text-danger"></i>' + tmpTitle + '</a>');
                }
                if (!(wsLib.isNullOrEmpty(targetID) && wsLib.isNullOrEmpty(targetURL))) {
                    $("#" + $("#" + tmpParentID).data('targetid')).parent().find(".card-header").removeAttr('title');
                    var lstSelected = [];
                    var tmp = tmpParent.find(".list-group-item.selected");
                    $.each(tmp, function (index, item) { lstSelected.push($(item).data("value")); });
                    if (lstSelected.length > 0) {
                        $.ajax({
                            url: targetURL,
                            type: "POST",
                            data: { lstSelected: lstSelected },
                            success: function (data) {
                                if (data && data.length > 0) {
                                    var tmpHTML = "";
                                    var parentValue = "";
                                    $.each(data, function (index, item) {
                                        if (item.ParentValue != parentValue) {
                                            if (parentValue != "") { tmpHTML += '</div>'; }
                                            parentValue = item.ParentValue;
                                            tmpHTML += '<a href="#P' + parentValue + '" data-toggle="collapse" class="list-group-parent"><div class="list-group-item-header">' + tmpParent.find('.list-group-item.selected[data-value="' + item.ParentValue + '"]').text() + '<i class="pull-right fa fa-caret-down"></i></div></a>';
                                            tmpHTML += '<div id="P' + parentValue + '" class="list-group-parent-container collapse">';
                                        }
                                        tmpHTML += '<a href="#" data-value="' + item.Value + '" class="list-group-item">';
                                        tmpHTML += '<i class="fa ' + (item.Selected ? 'fa-check-square-o' : 'fa-square-o') + ' fa-fw"></i>' + item.Text + '</a>';
                                    });
                                    if (parentValue != "") { tmpHTML += '</div>'; }
                                    $("#" + targetID.replace(".", "\\.")).html(tmpHTML);
                                }
                            },
                            error: function (reponse) {
                                alert("error : " + reponse);
                            }
                        });
                    }
                }
                setSelectionPanelVisibility();
            });

            $(".filter-group .btn-range").click(function () {
                var parent = $(this).parent().parent().parent().parent();
                var tmpParentID = $(parent).attr("id");
                var tmpType = $(parent).data("type");
                var tmpValue = $(parent).find(".valuemin").val();
                var tmpValue2 = $(parent).find(".valuemax").val();
                var tmpTitle = $(parent).data("title");
                if (tmpTitle != "") { tmpTitle += " : " + tmpValue + ' - ' + tmpValue2; }
                else { tmpTitle = tmpValue + ' - ' + tmpValue2; }
                $(selectionPanel).append('<a class="list-group-item" data-list="' + tmpParentID + '" data-value="' + tmpValue + '" data-value2="' + tmpValue2 + '" data-type="' + tmpType + '" href="#"><i class="fa fa-times-circle text-danger"></i>' + tmpTitle + '</a>');
                setSelectionPanelVisibility();
            });

            $(".filter-group .btn-input").click(function () {
                var tmpParentID = $(this).parent().parent().parent().attr("id");
                var tmpType = $(this).parent().parent().parent().data("type");
                var tmpValue = $(this).parent().prev().val();
                var tmpTitle = $(this).parent().parent().parent().data("title");
                if (tmpTitle != "") { tmpTitle += " : " + tmpValue; }
                else { tmpTitle = tmpValue; }
                var canAdd = true;
                $.each($(selectionPanel).find("a"), function (i, elm) {
                    if ($(elm).data("list") == tmpParentID && $(elm).data("value") == tmpValue) {
                        canAdd = false;
                    }
                });
                if (canAdd) {
                    $(selectionPanel).append('<a class="list-group-item" data-list="' + tmpParentID + '" data-value="' + tmpValue + '" data-value2="" data-type="' + tmpType + '" href="#"><i class="fa fa-times-circle text-danger"></i>' + tmpTitle + '</a>');
                }
                $(this).parent().prev().val("");
                setSelectionPanelVisibility();
            });


            setSelectionData();

            $(this).on("click", ".btn-filter-cancel", function () {
                $(selectionPanel).find("a").remove();
                $(".filter-group > .list-group > a.selected").each(function (index, element) {
                    $(element).removeClass("selected");
                    $(element).find("i").removeClass("fa-check-square-o").addClass("fa-square-o");
                });
                $(".filter-group > .list-group > input").each(function (index, element) {
                    $(element).val("");
                });
                setSelectionPanelVisibility();
            });

            $(this).on("click", ".btn-filter-submit", function () {
                oFilters = [];
                selectionPanel.find("a").each(function (index, element) {
                    oFilters.push({ ID: $(element).data("list"), Text: $(element).text(), Value: $(element).data("value"), Value2: $(element).data("value2"), FilterType: $(element).data("type") })
                });

                if (!wsLib.isNullOrEmpty(filterKey)) {
                    wsLib.clearSession();
                    wsLib.setSession(filterKey, JSON.stringify(oFilters))
                }

                selectionControl.val(JSON.stringify(oFilters));
                Listele();
            });

            function setSelectionPanelVisibility() {
                if ($(selectionPanel).find("a").length > 0) { $(selectionPanel).parent().removeClass("hidden"); }
                else { $(selectionPanel).parent().addClass("hidden"); }

            }

            function setSelectionData() {
                setSelectionPanelVisibility();
            }

        });
    };
}(jQuery));

$(function () {
    $(".panel-filter").searchfilter();
});

function setSelectionData() {

    var selectionPanel = $(".list-group-selecteditems");
    var selectionControl = $(".list-group-searchfilters");
    var filterKey = (window.location.pathname).replace("/", "");

    $(selectionPanel).html('');

    if (!wsLib.isNullOrEmpty(filterKey) && wsLib.getSession(filterKey)) {
        selectionControl.val(wsLib.getSession(filterKey));
    }

    if (!wsLib.isNullOrEmpty(selectionControl.val())) {
        var oFilters = JSON.parse(selectionControl.val())
        oFilters.forEach(function (value) {
            $(selectionPanel).append('<a class="list-group-item" data-list="' + value.ID + '" data-value="' + value.Value + '" data-value2="' + value.Value2 + '" data-type="' + value.FilterType + '" href="#"><i class="fa fa-times-circle text-danger"></i>' + value.Text + '</a>');
            $('#' + value.ID + ' a[data-value="' + value.Value + '"]').addClass("selected").find("i").removeClass("fa-square-o").addClass("fa-check-square-o");
        });
    }
}

