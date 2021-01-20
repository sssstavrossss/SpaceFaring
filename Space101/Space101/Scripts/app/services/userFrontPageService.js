let UserFrontPageService = function () {

    let getStatistics = function (doneGet, failGet) {
        $.ajax({
            method: 'GET',
            url: '/api/DatabaseStatistics/GetUserFrontPage'
        })
            .done(function (data) {
                doneGet(data);
            })
            .fail(failGet);
    };

    return {
        getStatistics: getStatistics
    }

}();