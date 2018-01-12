$(document).ready(function () {    
    $(".opened").click(function () {        
        $(".popup-window").css("position", "absolute").fadeIn();
        $(".popup-window").css("top", ($(window).height() - $(".popup-window").height()) / 2 + $(window).scrollTop() + "px");
        $(".popup-window").css("left", ($(window).width() - $(".popup-window").width()) / 2 + "px");
        $(".backpopup").fadeIn();
    });
    $(".backpopup,.closed").click(function () {
        $(".popup-window").fadeOut();
        $(".backpopup").fadeOut();
    });
});