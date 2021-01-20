let PlanetsUsersController = function (planetsUsersService) {

    let totalPlanets = [];
    let totalTerrains = [];
    let totalClimates = [];

    let optionTemplate = function (data) {
        return `<option value="${data}">${data}</option>`;
    };

    let template = function (planet) {

        let planetActivity = function (data) {
            if (data)
                return `<span class="planet-available">Active</span>`;
            else
                return `<span class="planet-unvailable">Inactive</span>`;
        };

        let planetAvatar = function (planet) {
            if (planet.avatar != null && planet.avatar.length > 3)
                return `style="
                        background-image: url(data:image/jpg;base64,${planet.avatar});
                        background-size: cover;
                        animation: planetRotate calc(${planetRotation(planet)}*.4s) linear infinite;">`;
            else
                return `><img src="/images/null-image.jpg" style="height: 100%; width: auto;"/>`;
        };

        let planetRotation = function (planet) {
            if (planet.details != null)
                if (planet.details.rotationPeriod != null && planet.details.rotationPeriod > 0)
                    return planet.details.rotationPeriod;
                else
                    return 50;
            else 
                return 50;
        };

        return `
                <div class="col-xs-3">
                <div class="row">
                    <div class="col-12">
                        <article class="card card--planet">
                            <div class="planet-availability">
                                <span>${planet.name}</span>
                                ${planetActivity(planet.isActive)}
                            </div>
                            <a href='/UserPlanet/Planet/${planet.planetID}' data-title="Go to Planet Details">
                                <div class="card__planet">
                                    <div class="planet__atmosphere">
                                        <div class="planet__surface" ${planetAvatar(planet)}
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </article>
                    </div>
                </div>
            </div>
                `;

    };

    let init = function () {

        loading();
        setTimeout(function () {
            populateData();
        }, 500);

        $('#search-input').on('input', function () { filterDataController() });
        $('#select-climate').on('input', function () { filterDataController() });
        $('#select-terrain').on('input', function () { filterDataController() });
        $('#select-sort').on('input', function () { filterDataController() });

    };

    let filterDataController = function () {
        let searchField = $('#search-input').val();
        let climateSelect = $('#select-climate').val();
        let terrainSelect = $('#select-terrain').val();
        let sortSelect = $('#select-sort').val();
        let newPlanetList = filterData(searchField, climateSelect, terrainSelect);
        let newNameList = nameArray(newPlanetList);
        newNameList = sortNameArray(newNameList, sortSelect);
        regAutocomplete(newNameList);
        newPlanetList = sortPlanetList(newPlanetList, newNameList);
        paging(newPlanetList);
        $("[id^='ui-id-']").on('click', function (e) {
            let txt = $(e.target).text();
            $('#search-input').attr('value', txt);
            filterDataController();
        });
    };

    let sortNameArray = function (newNameList, sortSelect) {
        if (sortSelect == "ASC")
            return newNameList = newNameList.sort();
        else 
            return newNameList = newNameList.sort().reverse();
    };

    let nameArray = function (newPlanetList) {
        let nameList = [];
        $(newPlanetList).each(function (key, value) {
            nameList.push(value.name);
        });
        return nameList;
    };

    let sortPlanetList = function (newPlanetList, newNameList) {
        let sortedPlanetList = [];
        $(newNameList).each(function (key, name) {
            $(newPlanetList).each(function (key, planet) {
                if (planet.name == name)
                    sortedPlanetList.push(planet);
            });
        });
        return sortedPlanetList;
    };

    let filterData = function (searchField, climateSelect, terrainSelect) {
        let newPlanetList = []
        if (climateSelect == '' && terrainSelect == '') {
            $(totalPlanets).each(function (key, value) {
                if (String(value.name).toLowerCase().includes(searchField.toLowerCase())) {
                    newPlanetList.push(value)
                }
            });
        } else if (climateSelect == '') {
            $(totalPlanets).each(function (key, planet) {
                if (String(planet.name).toLowerCase().includes(searchField.toLowerCase())) {
                    $(planet.surfaceMorphologies).each(function (key, value) {
                        if (String(value.terrain.name).includes(terrainSelect))
                            newPlanetList.push(planet)
                    })
                }
            });
        } else if (terrainSelect == '') {
            $(totalPlanets).each(function (key, planet) {
                if (String(planet.name).toLowerCase().includes(searchField.toLowerCase())) {
                    $(planet.climateZones).each(function (key, value) {
                        if (String(value.climate.name).includes(climateSelect))
                            newPlanetList.push(planet)
                    })
                }
            });
        } else {
            $(totalPlanets).each(function (key, planet) {
                if (String(planet.name).toLowerCase().includes(searchField.toLowerCase())) {
                    $(planet.climateZones).each(function (key, value) {
                        if (String(value.climate.name).includes(climateSelect)) {
                            $(planet.surfaceMorphologies).each(function (key, value) {
                                if (String(value.terrain.name).includes(terrainSelect))
                                    newPlanetList.push(planet)
                            })
                        }
                    })
                }
            });
        }
        return newPlanetList;
    }

    let regAutocomplete = function (data) {
        $("#search-input").autocomplete({
            source: data
        });
    };

    let populateData = function () {
        planetsUsersService.getAllPlanets(doneData, failData, assignArrays);
    };

    let failData = function () {
        console.log('failed');
    };

    let assignArrays = function (data) {
        totalPlanets = data;
        refinedPlanets = totalPlanets;
        $(data).each(function (key, value) {
            $(value.surfaceMorphologies).each(function (key, value) {
                totalTerrains.push(value.terrain.name);
            })
        });
        $(data).each(function (key, value) {
            $(value.climateZones).each(function (key, value) {
                totalClimates.push(value.climate.name);
            })
        });
        totalTerrains = totalTerrains.filter(function (itm, i, a) {
            return i == totalTerrains.indexOf(itm);
        });
        totalClimates = totalClimates.filter(function (itm, i, a) {
            return i == totalClimates.indexOf(itm);
        });
        createSelects();
    };

    let createSelects = function () {
        let temp = [];
        $(totalTerrains).each(function (key, value) {
            temp.push(optionTemplate(value));
        });
        $('#select-terrain').append(temp);
        temp = [];
        $(totalClimates).each(function (key, value) {
            temp.push(optionTemplate(value));
        });
        $('#select-climate').append(temp);
    };

    let doneData = function (data) {
        filterDataController();
    };

    let paging = function (data) {
        $('#planet-container').pagination({
            dataSource: data,
            pageSize: 8,
            showGoInput: true,
            showGoButton: true,
            callback: function (data, pagination) {
                let html = [];
                $(data).each(function (key, value) {
                    html.push(template(value));
                });
                $('#planet-container').find('#planet-row').html(html);
            }
        });
    }

    let loading = function () {
        $('#planet-row').html('<div style="margin: 100px 38%"><img src="/images/loading4.gif"/></div>');
    };

    return {
        init: init
    }

}(PlanetsUsersService);

