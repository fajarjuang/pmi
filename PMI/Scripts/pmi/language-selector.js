$(document).ready(function () {
    $("#lang-change").change(function () {
        var chosenLang = $(this).val();
        $.cookie("_culture", chosenLang, { expires: 365, path: '/' });
        window.location.reload();
    });

    var cult = $.cookie("_culture");

    if (cult == undefined) {
        // I HATE IE! :(
        if (navigator.language != undefined) {
            cult = navigator.language.substring(0, 2);
        } else {
            cult = navigator.userLanguage.substring(0, 2);
        }
    }

    $("#lang-change").val(cult);
});