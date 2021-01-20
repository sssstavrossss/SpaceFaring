let ClimateService = () => {
    let deleteClimate = function (id, done, fail) {
        $.ajax({
            url: `/api/climates/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteClimate: deleteClimate
    };
};