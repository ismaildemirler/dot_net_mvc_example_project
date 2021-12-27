$(function () {
    $('#Yukleniyor').click(function () {
        var dv = $(this).find('div');
        if ($(dv).hasClass("waitAdded")) return;
        $(dv).addClass("waitAdded");
        $(dv).html("İşleminiz Devam Ediyor. Lütfen Bekleyiniz.");
        $(dv).animate({
            "font-size": 16,
            "width": 385,
            "padding-right": 5,
            "border": "1px solid #ccc"
        }, 500).css({
            "background-color": "white",
            "text-align": "right",
            "-webkit-box-shadow": "0 0 10px 2px rgba (176,167,176,1)",
            "-moz-box-shadow": " 0 0 10px 2px rgba(176,167,176,1)",
            "box-shadow": "0 0 10px 2px rgba(176,167,176,1)"
        });
    });

    $("#Yukleniyor").show();

    $(window).bind('beforeunload', function () {
        $("#Yukleniyor").show();//.fadeIn();//.show("slide", { direction: "up" }, 500);
    });

    $(window).ready(function () {
        $("#Yukleniyor").fadeOut("slow");
    });

    $(document).on('click', ".ExportButton:not([disabled]),.ExportButtonNotNewPage:not([disabled])", function (e) {
        if ($(this).attr("target") == undefined) {
            var timeOut = $(this).data("exportloadingtimeout");
            if (timeOut == undefined || timeOut == "")
                timeOut = 5000;
            setTimeout(function () { $("#Yukleniyor").fadeOut("slow"); }, timeOut);
        }
    });
});

