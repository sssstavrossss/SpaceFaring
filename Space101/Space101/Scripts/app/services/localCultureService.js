let LocalCultureService = () => {
    let getServerCulture = function (done, fail) {
        $.ajax({
            url: `/api/localCultures`,
            method: "Get"
        })
            .done(done)
            .fail(fail);
    };

    return {
        getServerCulture: getServerCulture
    };
}