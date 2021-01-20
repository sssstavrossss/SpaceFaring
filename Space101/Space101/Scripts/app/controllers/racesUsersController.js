let RacesUsersController = function (racesUsersService) {

    let totalRaces = [];
    let totalClassifications = [];
    let totalRacesNames = [];

    let optionTemplate = function (data) {
        return `<option value="${data}">${data}</option>`;
    }

    let template = function (data) {

        let avatarTamplate = function (data) {
            if (data.avatar != null && data.avatar.length > 3)
                return `style="
                        background-image: url(data:image/png;base64,${data.avatar});
                        background-size: cover;">`;
            else
                return `><img src="/images/null-image.jpg" style="height: 100%; width: auto;"/>`;
        };

        return `
                <div class="col-xs-2">
                    <article class="card card--race">
                            <div class="race-availability">
                                <span>${data.name}</span>
                            </div>
                              <a href="/UserRace/Race/${data.raceID}">
                            <div class="card__race">
                                <div class="race__atmosphere">
                                    <div class="race__surface" ${avatarTamplate(data)}
                                    </div>
                                </div>
                            </div>
                        </a>
                        </article>
                 </div>
                `;
    };

    let init = function () {

        loading();
        setTimeout(function () {
            populateData();
        }, 500);

        $('#search-input').on('input', function () { filterDataController() });
        $('#select-classification').on('input', function () { filterDataController() });
        $('#select-sort').on('input', function () { filterDataController() });

    };

    let filterDataController = function () {
        let searchField = $('#search-input').val();
        let classificationSelect = $('#select-classification').val();
        let sortSelect = $('#select-sort').val();
        let newRaceList = filterData(searchField, classificationSelect);
        let newNameList = nameArray(newRaceList);
        newNameList = sortNameArray(newNameList, sortSelect);
        regAutocomplete(newNameList);
        newRaceList = sortRaceList(newRaceList, newNameList);
        paging(newRaceList);
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

    let nameArray = function (newRaceList) {
        let nameList = [];
        $(newRaceList).each(function (key, value) {
            nameList.push(value.name);
        });
        return nameList;
    };

    let sortRaceList = function (newRaceList, newNameList) {
        let sortedRaceList = [];
        $(newNameList).each(function (key, name) {
            $(newRaceList).each(function (key, race) {
                if (race.name == name)
                    sortedRaceList.push(race);
            });
        });
        return sortedRaceList;
    };

    let filterData = function (searchField, classificationSelect) {
        let newRaceList = []
        if (classificationSelect == '') {
            $(totalRaces).each(function (key, value) {
                if (String(value.name).toLowerCase().includes(searchField.toLowerCase())) {
                    newRaceList.push(value)
                }
            });
        } else {
            $(totalRaces).each(function (key, race) {
                if (String(race.name).toLowerCase().includes(searchField.toLowerCase())) {
                    if (String(race.raceClassification.name).includes(classificationSelect))
                         newRaceList.push(race)
                }
            });
        }
        return newRaceList;
    }

    let regAutocomplete = function (data) {
        $("#search-input").autocomplete({
            source: data
        });
    };

    let populateData = function () {
        racesUsersService.getAllRaces(doneData, failData, assignArrays);
    };

    let failData = function () {
        console.log('failed');
    };

    let assignArrays = function (data) {
        totalRaces = data;
        $(data).each(function (key, value) {
            totalClassifications.push(value.raceClassification.name);
        });
        totalClassifications = totalClassifications.filter(function (itm, i, a) {
            return i == totalClassifications.indexOf(itm);
        });
        $(data).each(function (key, value) {
            totalRacesNames.push(value.name);
        });
        totalRacesNames = totalRacesNames.filter(function (itm, i, a) {
            return i == totalRacesNames.indexOf(itm);
        });
        totalClassifications = totalClassifications.sort();
        totalRacesNames = totalRacesNames.sort();
        createSelects();
        regAutocomplete(totalRacesNames);
    };

    let createSelects = function () {
        let temp = [];
        $(totalClassifications).each(function (key, value) {
            temp.push(optionTemplate(value));
        });
        $('#select-classification').append(temp);
    };

    let doneData = function (data) {
        filterDataController();
    };

    let paging = function (data) {
        $('#races-container').pagination({
            dataSource: data,
            pageSize: 18,
            showGoInput: true,
            showGoButton: true,
            callback: function (data, pagination) {
                let html = [];
                $(data).each(function (key, value) {
                    html.push(template(value));
                });
                $('#races-container').find('#race-row').html(html);
            }
        });
    }

    let loading = function () {
        $('#race-row').html('<div style="margin: 100px 38%"><img src="/images/loading4.gif"/></div>');
    };

    return {
        init: init
    }

}(RacesUsersService);

