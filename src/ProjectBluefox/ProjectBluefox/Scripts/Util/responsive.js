$(function () {
    $('.open-mobile-menu').click(function () {        
        $('body').addClass('mobile-menu-opened');
    });
    $('.close-mobile-menu').click(function (e) {
        e.preventDefault();
        $('body').removeClass('mobile-menu-opened');
    });
});