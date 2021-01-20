$(document).ready(function () {
    $('.main-buttons a').on('click', function (e) {
        let href = $(e.target).parent().attr('data-href');
        $(`#${href}`).siblings().hide(100);
        $(`#${href}`).slideToggle(300);
    });
});