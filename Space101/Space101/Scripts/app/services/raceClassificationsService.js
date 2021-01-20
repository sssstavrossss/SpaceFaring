let RaceClassificationsService = function () {

    let getRaceClassifications = function (failGet, doneGet) {
        $.ajax({
            url: `/api/racesclassifications`,
            method: "GET"
        })
            .done(function (data) {
                doneGet(data);
            })
            .fail(failGet);
    };

    let createRaceClassification = function (failPost, donePost, raceClassification) {
        $.ajax({
            url: `/api/racesclassifications`,
            method: "POST",
            data: raceClassification,
        })
            .done(function (data) {
                donePost(data);
            })
            .fail(failPost);

        return false;
    };

    let deleteRaceClassification = function (id, doneDelete, failDelete) {
        $.ajax({
            url: `/api/racesclassifications/${id}`,
            method: "DELETE"
        })
            .done(doneDelete)
            .fail(failDelete);
    };

    let hardDeleteRaceClassification = function (id, doneDelete, failDelete) {
        $.ajax({
            url: `/api/racesclassifications/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(doneDelete)
            .fail(failDelete);
    };

    return {
        getRaceClassifications: getRaceClassifications,
        createRaceClassification: createRaceClassification,
        deleteRaceClassification: deleteRaceClassification,
        hardDeleteRaceClassification: hardDeleteRaceClassification
    };

}();