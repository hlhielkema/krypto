$(function () {
    $('.open-mobile-menu').click(function () {        
        $('.mobile-menu').addClass('opened');
    });
    $('.close-mobile-menu').click(function (e) {
        e.preventDefault();
        $('.mobile-menu').removeClass('opened');
    });
});