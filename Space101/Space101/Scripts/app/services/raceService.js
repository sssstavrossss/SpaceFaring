let RaceService = () => {
    let deleteRace = function (id, done, fail) {
        $.ajax({
            url: `/api/races/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let hardDeleteRace = function (id, done, fail) {
        $.ajax({
            url: `/api/races/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteRace: deleteRace,
        hardDeleteRace: hardDeleteRace
    };
}