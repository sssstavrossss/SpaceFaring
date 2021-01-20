let UsersFlightsController = function (usersFlightsService, userFlightsSignalR) {

    let totalFlights = []; // for filtering
    let totalDestinationPlanets = []; // for filtering
    let totalDeparturePlanets = []; // for filtering
    let daysDiff = []; // for filtering
    let prices = []; // for filtering // descending order
    let minPrice; // for filters
    let maxPrice; // for filters
    let sortSelect = $('#sort-select'); // sort select selector
    let departureInput = $('#departure-input'); // departure input selector
    let destinationInput = $('#destination-input'); // destination input selector
    let dataFrom = $('#from'); // date from selector
    let dateTo = $('#to'); // date to selector
    let vip = $('#vip'); // vip checkbox selector
    let noVip = $('#vip-no'); // no vip checkbox selector
    let sliderRange = $('#slider-range'); // slider selector
    let searchBtn = $('#search-btn'); // search btn selector
    let apiObject = {}; // the object to send to api controller
    let flightNo = $('#flight-no'); // flight number selector
    let flightRow = $('.flight-row'); // flight row for signalR
    let favorite = $('.favorite-display img'); // favorite display img
    let userFavorites = []; // user favorites list
    let userLogged = false;
    let apiFavObj = {};

    let failTemplate = function () {
        return '<h3 style="margin-left: 35px;">Something went wrong!</h2><br/><h2>Try again later....</h3>'
    };

    let noDataTemplate = function () {
        return '<h3 style="margin-left: 35px;">There are no flights right now!</h2>'
    };

    let noSearchDataTemplate = function () {
        return '<h3 style="margin-left: 35px;">There are no flights with these details right now!</h3>'
    };

    let getVipText = function (data) {
        if (data !== true)
            return `NO <br/>`
        else
            return ``;
    };

    let getVipClass = function (data) {
        if (data)
            return `class="is-vip `
        else
            return `class="not-vip `;
    };

    let getSuperVipClass = function (data) {
        if (data)
            return `super-is-vip `
        else
            return `super-not-vip `;
    }; 

    let statusClass = function (data) {
        if (data == 1) {
            return ` bg-green`;
        } else if (data == 2) {
            return ` bg-orange`;
        } else {
            return ` bg-red`;
        }
    };

    let getHref = function (data) {
        if (data.flightStatus.flightStatusId === 1 || data.flightStatus.flightStatusId === 2)
            return `<a href="/Flight/ChooseSeats/${data.flightId}">`;
        else 
            return `<a href="javascript:void(0)">`;
    };

    let favorites = function (data) {
        if (userLogged && userFavorites.length > 0) {
            if (data.isFavorited)
                return `<div class="favorite-display" data-flight-id="${data.flightId}" data-favorite-id="${data.favoriteId}">
                        <img src="/images/like-red.png"/></div>`
            else
                return `<div class="favorite-display" data-flight-id="${data.flightId}" data-favorite-id="${data.favoriteId}">
                        <img src="/images/like-white.png"/></div>`;
        } else if (userLogged && userFavorites.length == 0) {
            return `<div class="favorite-display" data-flight-id="${data.flightId}" data-favorite-id="${data.favoriteId}">
                        <img src="/images/like-white.png"/></div>`;
        } else if (!userLogged) {
            return ``;
        }
    };

    let flightTemplate = function (data) {

        return `
                <div class="row flight-row" id="flight[${data.flightId}]" data-flight-id="${data.flightId}">
                    <div class="status-display ${statusClass(data.flightStatus.flightStatusId)}">${data.flightStatus.statusName}</div>
                    ${favorites(data)}
                    <div class="col-xs-4">
                        <div class="flight-div">
                            <div class="flight-details">
                                <div class="flight-details-departure">
                                    <div><img src="/images/internet.png" alt="planet logo" style="height: 30px; width: auto;"/></div>
                                    <div>Departure: </div>
                                    <div>${data.flightPath.departure.name}</div>
                                </div>
                                <div class="flight-details-destination">
                                    <div><img src="/images/internet.png" alt="planet logo" style="height: 30px; width: auto;"/></div>
                                    <div>Destination: </div>
                                    <div>${data.flightPath.destination.name}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="flight-div">
                            <div class="flight-details">
                                <div class="flight-details-date">
                                    <div><img src="/images/timetable.png" alt="date logo" style="height: 30px; width: auto;"/></div>
                                    <div>${data.dateString}</div>
                                </div>
                                <div class="flight-details-price">
                                    <div><img src="/images/dollar.png" alt="dollar logo" style="height: 30px; width: auto;"/></div>
                                    <div>Base Price: </div>
                                    <div>${data.basePrice}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="starship-div">
                            <div class="starship-details">
                                <div class="starship-details-intro">
                                    <div><img src="/images/spaceship.png" alt="spaceship logo" style="height: 25px; width: auto;"/></div>
                                </div>
                                <div class="starship-details-name">
                                    <div>${data.starship.model}</div>
                                </div>
                                <div class="starship-details-passengers">
                                    <div>Passengers: </div>
                                    <div>${data.starship.passengerCapacity}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-1 ${getSuperVipClass(data.isVIP)} vip-super-div-inflight">
                        <div ${getVipClass(data.isVIP)} vip-div-inflight">${getVipText(data.isVIP)} VIP</div>
                    </div>
                    <div class="col-xs-1 book-flight-super-div">
                        <div class="book-flight-div">${getHref(data)}<button class="book-flight-btn">BOOK NOW</button></a></div>
                    </div>
                </div>
               `;
    };

    let init = function () {

        loading();
        setTimeout(function () {
            usersFlightsService.getUserFavorites(doneFav, failFav);
            getData();
            userFlightsSignalR.hubInit(UsersFlightsController);
        }, 500)

        $(sortSelect).on('change', function (e) { sortFlights($(e.target).val()); }); //sorting event
        $(searchBtn).on('click', function () {
            loading();
            createApiObject();
            setTimeout(function () {
                sendCall();
            }, 500);
        }); // search functionality
        //document.getElementsByClassName('favorite-display').addEventListener("click", function (e) { console.log('hi') });

    };

    let doneFav = function (data) {
        userFavorites = [];
        userLogged = false;
        if (data.length >= 0) {
            userFavorites = data;
            userLogged = true;
        }
    };

    let failFav = function () {
        console.log('failed to get favorites');
    };

    let updateFlight = function (flight) {
        var refinedList = [];
        $(totalFlights).each(function (key, value) {
            if (value.flightId == flight.FlightId) {
                value.flightId = flight.FlightId;
                value.basePrice = flight.BasePrice;
                value.dateString = flight.DateString;
                value.dateDiff = flight.DateDiff;
                value.flightPath.departure.name = flight.FlightPath.Departure.Name;
                value.flightPath.departure.title = flight.FlightPath.Departure.Title;
                value.flightPath.destination.planetID = flight.FlightPath.Destination.PlanetID;
                value.flightPath.destination.name = flight.FlightPath.Destination.Name;
                value.flightPath.destination.title = flight.FlightPath.Destination.Title;
                value.flightPath.destination.planetID = flight.FlightPath.Destination.PlanetID;
                value.flightStatus.flightStatusId = flight.FlightStatus.FlightStatusId;
                value.flightStatus.statusName = flight.FlightStatus.StatusName;
                value.flightStatuses = flight.FlightStatuses;
                value.isVIP = flight.IsVIP;
                value.starship.cargoCapacity = flight.Starship.CargoCapacity;
                value.starship.length = flight.Starship.Length;
                value.starship.manufacturer = flight.Starship.Manufacturer;
                value.starship.model = flight.Starship.Model;
                value.starship.passengerCapacity = flight.Starship.PassengerCapacity;
                value.isFavorited = value.isFavorited;
                value.favoriteId = value.favoriteId;
            };
            refinedList.push(value);
        });
        totalFlights = refinedList;
        doneGetSearchData(totalFlights);
    };

    let updateFlightStatus = function (flightId, statusId) {
        var refinedList = [];
        $(totalFlights).each(function (key, value) {
            if (value.flightId == flightId) {
                let tempList = value.flightStatuses;
                let newStatus = {};
                $(tempList).each(function (key, status) {
                    if (status.flightStatusId == statusId) { newStatus = status; }
                });
                value.flightStatus.flightStatusId = newStatus.flightStatusId;
                value.flightStatus.statusName = newStatus.statusName;
            };
            refinedList.push(value);
        });
        totalFlights = refinedList;
        doneGetSearchData(totalFlights);
    };

    let appendFlight = function (data) {
        totalFlights.push(data);
        doneGetSearchData(totalFlights);
    };

    let createFlight = function (flight) {
        let newFlight = {};
        newFlight.flightId = flight.FlightId;
        newFlight.basePrice = flight.BasePrice;
        newFlight.dateString = flight.DateString;
        newFlight.dateDiff = flight.DateDiff;
        newFlight.flightPath = {};
        newFlight.flightPath.departure = {};
        newFlight.flightPath.destination = {};
        newFlight.flightPath.departure.name = flight.FlightPath.Departure.Name;
        newFlight.flightPath.departure.title = flight.FlightPath.Departure.Title;
        newFlight.flightPath.destination.planetID = flight.FlightPath.Destination.PlanetID;
        newFlight.flightPath.destination.name = flight.FlightPath.Destination.Name;
        newFlight.flightPath.destination.title = flight.FlightPath.Destination.Title;
        newFlight.flightPath.destination.planetID = flight.FlightPath.Destination.PlanetID;
        newFlight.flightStatus = {};
        newFlight.flightStatus.flightStatusId = flight.FlightStatus.FlightStatusId;
        newFlight.flightStatus.statusName = flight.FlightStatus.StatusName;
        newFlight.isVIP = flight.IsVIP;
        newFlight.starship = {};
        newFlight.starship.cargoCapacity = flight.Starship.CargoCapacity;
        newFlight.starship.length = flight.Starship.Length;
        newFlight.starship.manufacturer = flight.Starship.Manufacturer;
        newFlight.starship.model = flight.Starship.Model;
        newFlight.starship.passengerCapacity = flight.Starship.PassengerCapacity;
        newFlight.flightStatuses = [];
        newFlight.flightStatuses = flight.FlightStatuses;
        newFlight.favoriteId = 0;
        newFlight.isFavorited = false;
        totalFlights.push(newFlight);
        doneGetSearchData(totalFlights);
    };

    let deleteFlight = function (id) {
        let refinedList = [];
        $(totalFlights).each(function (key, value) {
            if (value.flightId != id)
                refinedList.push(value);
        });
        totalFlights = refinedList;
        doneGetSearchData(totalFlights);
    };

    let statusChange = function (flightId, statusId) {
        
        let idArray = [];
        $(totalFlights).each(function (key, value) {
            idArray.push(value.flightId);
        });
        idArray = idArray.sort();

        if (idArray.includes(flightId)) {
            if (statusId !== 1 && statusId !== 2) {
                updateFlightStatus(flightId, statusId);
                setTimeout(function () {
                    deleteFlight(flightId);
                }, 12000);
            }
            else 
                updateFlightStatus(flightId, statusId);
        } else if (!idArray.includes(flightId)) {
            if (statusId !== 1 && statusId !== 2) 
                return;
            else 
                usersFlightsService.getChangedFlight(flightId, appendFlight);
        }

    };

    let createApiObject = function () {
        apiObject.minPrice = $(sliderRange).slider("values", 0); //initiate min price for object to send
        apiObject.maxPrice = $(sliderRange).slider("values", 1); //max price
        apiObject.vipBool = $(vip).is(':checked'); // true / false for vip checkbox
        apiObject.dateFrom = $(dataFrom).datepicker({ dateFormat: 'dd,MM,yyyy' }).val(); // get departure date from
        apiObject.dateTo = $(dateTo).datepicker({ dateFormat: 'dd,MM,yyyy' }).val(); // get departure date to
        apiObject.departurePlanet = $(departureInput).val(); // get departure planet
        apiObject.destinationPlanet = $(destinationInput).val(); // get destination planet 
        apiObject.NoVipBool = $(noVip).is(':checked');
        console.log(apiObject);
    };

    let sendCall = function () {
        usersFlightsService.getFilteredFlights(apiObject, doneGetSearchData, failGetData);
    };

    let sortFlights = function (sort) {
        if (totalFlights.length == 0)
            return;

        //i am here to sort the list i have according to sort select
        let refinedList = [];

        if (sort === 'DASC') {         //date ascending
            let tempList = [];
            tempList = daysDiff;
            tempList.reverse();
            refinedList = sortHelper(tempList, totalFlights, 'dateDiff');
        } else if (sort === 'DDESC') { //date descending
            let tempList = [];
            tempList = daysDiff;
            tempList.reverse();
            refinedList = sortHelper(tempList, totalFlights, 'dateDiff');
        } else if (sort === 'PLDESC') { //departure planet descending
            let tempList = [];
            tempList = totalDeparturePlanets;
            tempList.reverse();
            refinedList = sortHelper(tempList, totalFlights, 'flightPath', 'departure', 'name');
        } else if (sort === 'PLASC') { //departure planet asceding
            let tempList = [];
            tempList = totalDeparturePlanets;
            tempList.reverse()
            refinedList = sortHelper(tempList, totalFlights, 'flightPath', 'departure', 'name');
        } else if (sort === 'PRDESC') {//price descending
            prices.sort(function (a, b) { return b - a });
            refinedList = sortHelper(prices, totalFlights, 'basePrice');
        } else if (sort === 'PRASC') { //price ascending
            prices.sort(function (a, b) { return a - b });
            refinedList = sortHelper(prices, totalFlights, 'basePrice');
        };

        paging(refinedList);

    };

    let sortHelper = function (initial, secondary, target, starget, ttarget) {
        let temp = [];
        $(initial).each(function (key, init) {
            $(secondary).each(function (key, sec) {
                if (starget) {
                    if (sec[target][starget][ttarget] === init)
                        temp.push(sec);
                }
                else 
                    if (sec[target] === init)
                        temp.push(sec);
            });
        });
        return temp;
    };

    let getData = function () {
        usersFlightsService.getFlights(doneGetData, failGetData);
    };

    let appendFavorites = function () {
        $(totalFlights).each(function (key, flight) {
            flight.isFavorited = false;
            flight.favoriteId = 0;
            $(userFavorites).each(function (key, fav) {
                if (flight.flightId == fav.flightID) {
                    flight.isFavorited = true;
                    flight.favoriteId = fav.userFavoriteID;
                }
            });
        });
    };

    let doneGetData = function (data) {
        if (data.length > 0) {
            console.log(data);
            assignArrays(data);
            appendFavorites();
            initializeUI();
            $(flightNo).text(totalFlights.length);
        }
        else {
            noDataTemplate
            $('.flights-main').text('');
            $('.flights-main').append(noDataTemplate());
            totalFlights = [];
        }
    };

    let doneGetSearchData = function (data) {
        console.log(data);
        if (data.length > 0) {
            assignArrays(data);
            sortFlights($(sortSelect).val());
            $(flightNo).text(totalFlights.length);
        }
        else {
            $('.flights-main').text('');
            $('.flights-main').append(noSearchDataTemplate());
            totalFlights = [];
            $(flightNo).text('No');
        }
    };

    let failGetData = function () {
        $('.flights-main').append(failTemplate());
    };

    let assignArrays = function (data) {

        totalFlights = [];
        totalDestinationPlanets = [];
        totalDeparturePlanets = [];
        daysDiff = [];
        prices = [];
        minPrice = 0;
        maxPrice = 0;

        totalFlights = data;

        $(data).each(function (key, value) {
            totalDestinationPlanets.push(value.flightPath.destination.name);
        });
        totalDestinationPlanets = totalDestinationPlanets.filter(function (itm, i, a) {
            return i == totalDestinationPlanets.indexOf(itm);
        });
        totalDestinationPlanets = totalDestinationPlanets.sort();

        $(data).each(function (key, value) {
            totalDeparturePlanets.push(value.flightPath.departure.name);
        });
        totalDeparturePlanets = totalDeparturePlanets.filter(function (itm, i, a) {
            return i == totalDeparturePlanets.indexOf(itm);
        });
        totalDeparturePlanets = totalDeparturePlanets.sort();

        $(data).each(function (key, value) {
            daysDiff.push(value.dateDiff);
        });
        daysDiff = daysDiff.filter(function (itm, i, a) {
            return i == daysDiff.indexOf(itm);
        });
        daysDiff.sort(function (a, b) { return a - b }); // ascending

        $(data).each(function (key, value) {
            prices.push(value.basePrice);
        });
        prices = prices.filter(function (itm, i, a) {
            return i == prices.indexOf(itm);
        });
        prices.sort(function (a, b) { return b - a }); // descending (a -b asc)
        minPrice = prices[prices.length - 1];
        maxPrice = prices[0];

    };

    let initializeUI = function () {

        regAutocomplete(destinationInput, totalDestinationPlanets);
        regAutocomplete(departureInput, totalDeparturePlanets);
        jqui();
        sortFlights('DASC');
    };

    let regAutocomplete = function (field, data) {
        $(field).autocomplete({
            source: data
        });
    };

    let paging = function (data) {
        $('.main').pagination({
            dataSource: data,
            pageSize: 8,
            showGoInput: true,
            showGoButton: true,
            callback: function (data, pagination) {
                let html = [];
                $(data).each(function (key, value) {
                    html.push(flightTemplate(value));
                });
                $('.main').find('.flights-main').html(html);
                $('.favorite-display').on('click', function () { changeFavorite($(this)) });
            }
        });
    };

    let changeFavorite = function (target) {
        apiFavObj.FlightID = $(target).attr('data-flight-id');
        apiFavObj.UserFavoriteID = $(target).attr('data-favorite-id');
        apiFavObj.ApplicationUserID = 0;
        usersFlightsService.updateUserFavorites(doneFavUpdate, apiFavObj);
        changeImage(target);
    };

    let doneFavUpdate = function (target) {
        usersFlightsService.getUserFavorites(doneFav, failFav);
        appendFavorites();
    };

    let changeImage = function (target) {
        if ($(target).html().includes('white'))
            $(target).html('<img src="/images/like-red.png"/></div>');
        else {
            $(target).html('<img src="/images/like-white.png"/></div>');
        }
    };

    let jqui = function () {
        $(function () {
            $("#slider-range").slider({
                range: true,
                min: Math.round(minPrice - 0.51),
                max: Math.ceil(maxPrice + 0.51),
                values: [Math.round(minPrice - 0.51), Math.ceil(maxPrice + 0.51)],
                slide: function (event, ui) {
                    $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
                }
            });
            $("#amount").val("$" + $("#slider-range").slider("values", 0) +
                " - $" + $("#slider-range").slider("values", 1));
        });

        $(function () {
            var dateFormat = "mm/dd/yy",
                from = $("#from")
                    .datepicker({
                        changeMonth: true,
                        numberOfMonths: 1,
                        minDate: 0
                    })
                    .on("change", function () {
                        to.datepicker("option", "minDate", getDate(this));
                    }),
                to = $("#to").datepicker({
                    changeMonth: true,
                    numberOfMonths: 1,
                    minDate: +1
                })
                    .on("change", function () {
                        from.datepicker("option", "maxDate", getDate(this));
                    });

            function getDate(element) {
                var date;
                try {
                    date = $.datepicker.parseDate(dateFormat, element.value);
                } catch (error) {
                    date = null;
                }

                return date;
            }
        });
    };

    let loading = function () {
        $('.flights-main').html('<div style="margin-left: 30%; margin-top: 80px;"><img src="/images/loading4.gif"/></div>');
    };

    return {
        init: init,
        updateFlight: updateFlight,
        statusChange: statusChange,
        createFlight: createFlight,
        deleteFlight: deleteFlight
    };

}(UsersFlightsService, UserFlightsSignalR);

