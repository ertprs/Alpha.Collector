
$(".table tr").live("click", function () {
    $(".table tr").each(function (i, val) {
        $(val).css("backgroundColor", "");
    });

    $(this).css("backgroundColor", "#CCE8CF");

});