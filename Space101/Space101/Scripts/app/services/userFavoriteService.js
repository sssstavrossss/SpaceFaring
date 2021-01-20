let UserFavoriteService = function () {

    let updateFavorite = function (data, done, fail) {
        $.ajax({
            url: `/api/UserFavorites`,
            method: "POST",
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8'
        })
            .done(done)
            .fail(fail);
    };

    return {
        updateFavorite: updateFavorite
    }

}();