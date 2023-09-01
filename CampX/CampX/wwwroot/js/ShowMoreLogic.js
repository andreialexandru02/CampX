$(document).ready(function () {
    $(".showMore").click(function () {
        console.log(this)
        var $ShowMoreDiv = $(this).prev(".ShowMoreDiv");
        $ShowMoreDiv.toggleClass("expanded");
        console.log($(this).prev(".ShowMoreDiv"))
        if ($ShowMoreDiv.hasClass("expanded")) {
            $(this).text("Show Less");
            $("#ShowMoreDiv").addClass("expanded");
        } else {
            $(this).text("Show More");
            $("#ShowMoreDiv").removeClass("expanded");
        }
    });
});