
$(document).ready(function () {

    $(".nav-btn-container").on('click', function () {
        $("#sidenav").show(240);
        $(document).mouseup(function (e) {
            var container = $("#sidenav");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                container.hide(460);
            }
        });
    });

    $("#registerLinkLoggedIn").on('click', function () {
        $("#user-sidenav").toggle(240);
        $(document).mouseup(function (e) {
            var container = $("#user-sidenav");
            var container2 = $('#registerLinkLoggedIn')
            if (!container.is(e.target) && !container2.is(e.target) && container.has(e.target).length === 0) {
                container.hide(350);
            }
        });
    });
    
    $(".closebtn").on('click', function () { $("#sidenav").hide(460); $(".drop-menu").hide(380); });

    $(".user-closebtn").on('click', function () { $("#user-sidenav").hide(460); });

    $('.drop-btn').on('click', function (e) {
        let target = $(this);
        e.stopPropagation();
        target.parent().parent().siblings().find('.drop-menu').hide(380);
        target.siblings().toggle(350);
        $('body').on('click', function () {
            target.siblings().hide(350);
        });
    });

});