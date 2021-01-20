let UserFavoriteController = function (userFavoriteService) {

    let apiFavObj = {};
    let fav = $('.favorite');

    let init = function () {
        fav.on('click', function () { createApiObject($(this)); sendCall($(this)); });
    };

    let createApiObject = function (target) {
        apiFavObj.FlightID = target.attr('data-flight-id');
        apiFavObj.UserFavoriteID = target.attr('data-favorite-id')
        apiFavObj.ApplicationUserID = 0;
    };

    let sendCall = function () {
        userFavoriteService.updateFavorite(apiFavObj, done, fail);
    };

    let done = function (target) {
        //let txt = target.closest('favor').html();
        //$(target).$('span').text('Favorite');
        //console.log(target.attr('data-flight-id'));
    };

    let fail = function () {
        console.log('failed to update');
    };

    return {
        init: init
    }

}(UserFavoriteService);