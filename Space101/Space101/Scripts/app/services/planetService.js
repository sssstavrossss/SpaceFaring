let PlanetService = () => {
    let deletePlanet = function (id, done, fail) {
        $.ajax({
            url: `/api/planets/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let hardDeletePlanet = function (id, done, fail) {
        $.ajax({
            url: `/api/planets/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deletePlanet: deletePlanet,
        hardDeletePlanet: hardDeletePlanet
    };
}