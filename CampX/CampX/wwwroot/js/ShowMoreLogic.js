$(document).ready(function () {
    $(".showMore").click(function () {
        console.log(this)
        var $ShowMoreDiv = $(this).prev(".ShowMoreDiv");
        $ShowMoreDiv.toggleClass("expanded");
        console.log($(this).prev(".ShowMoreDiv"))
        if ($ShowMoreDiv.hasClass("expanded")) {
            $(this).text("Arată mai puțin");
            $("#ShowMoreDiv").addClass("expanded");
        } else {
            $(this).text("Arată mai mult");
            $("#ShowMoreDiv").removeClass("expanded");
        }
    });
});