let UserFrontPageController = function (userFrontPageService) {

    let flightsNo = $('#flights-no');
    let racesNo = $('#races-no');
    let planetsNo = $('#planets-no');
    let aboutUsBtn = $('.about-us');
    let contactUsBtn = $('.contact-us');
    let flightsBtn = $('.flights');
    let badgesBtn = $('.badges');
    let privilegesBtn = $('.privileges');
    let aboutUsDiv = $('#about-us');
    let contactUsDiv = $('#contact-us');
    let flightsDiv = $('#flights');
    let badgesDiv = $('#badges');
    let privilegesDiv = $('#privileges');

    let init = function () {
        initNumbers();
        userFrontPageService.getStatistics(doneGet, failGet);
        flightsBtn.on('click', function () { toggleDivs(flightsDiv) });
        badgesBtn.on('click', function () { toggleDivs(badgesDiv) });
        privilegesBtn.on('click', function () { toggleDivs(privilegesDiv) });
        contactUsBtn.on('click', function () { toggleDivs(contactUsDiv) });
        aboutUsBtn.on('click', function () { toggleDivs(aboutUsDiv) });
    };

    let toggleDivs = function (target) {
        $(aboutUsDiv).hide(0);
        $(contactUsDiv).hide(0);
        $(flightsDiv).hide(0);
        $(badgesDiv).hide(0);
        $(privilegesDiv).hide(0);
        $(target).show(0);
    };

    let initNumbers = function () {
        flightsNo.html('<img style="height: 30px; width: auto;" src="/images/loading4.gif"/>')
        racesNo.html('<img style="height: 30px; width: auto;" src="/images/loading4.gif"/>')
        planetsNo.html('<img style="height: 30px; width: auto;" src="/images/loading4.gif"/>')
    };

    let doneGet = function (data) {
        setTimeout(function () {
            flightsNo.text(data.flights)
            racesNo.text(data.races);
            planetsNo.text(data.planets);
        }, 1500);
    };

    let failGet = function () {
        console.log('something went wrong!');
    };

    let loading = function () {
        $('.flights-main').html('<div style="margin-left: 180px;"><img src="/images/loading4.gif"/></div>');
    };

    return {
        init: init
    }

}(UserFrontPageService);