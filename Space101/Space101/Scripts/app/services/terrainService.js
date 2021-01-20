let TerrainService = () => {
    let deleteTerrain = function (id, done, fail) {
        $.ajax({
            url: `/api/terrains/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteTerrain: deleteTerrain
    };
}