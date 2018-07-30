$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    $('.summernote').summernote();
});
$(window).scroll(function () {      /*for scrollToTop button*/
    if ($(this).scrollTop() > 100) {
        $('.goToTop').fadeIn();
    } else {
        $('.goToTop').fadeOut();
    }
});
$('.goToTop').click(function () {
    $("html, body").animate({ scrollTop: 0 }, 100);
    return false;
});