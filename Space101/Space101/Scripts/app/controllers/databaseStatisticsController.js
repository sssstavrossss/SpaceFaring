let DatabaseStatisticsController = ((databaseStatisticsService) => {

    let init = function (redrawButtonId) {
        DrawAdminStatistics();

        $(redrawButtonId).on('click', function () {
            DrawAdminStatistics();
            UpdateDatabaseNumbers();
        });
    };

    let DrawAdminStatistics = function () {
        DrawPlanetsPerClimate("#planetsPerClimate");
        DrawPlanetsPerTerrain("#planetsPerTerrain");
        DrawFlightsPerStatus("#flightPerStatus");
        DrawFlightsDetails(new flightDetailsContainer("#flightOcupancyRate", "#flightRevenuePerDistance", "#flightExpectedRevenue", "#flightTicketRevenue"));
        DrawFlightsPerDate("#flightsPerDate");
    }

    function ChartOptions(ctx, type, data, label, labels, bgColors, borderColors, chartFill) {
        this.ctx = ctx;
        this.type = type;
        this.data = data;
        this.label = label;
        this.labels = labels;
        this.bgColors = bgColors;
        this.borderColors = borderColors;
        this.chartFill = chartFill;
    };
    function DrawChart(chartOptions) {
        new Chart(chartOptions.ctx, {
            type: chartOptions.type,
            data: {
                labels: chartOptions.labels,
                datasets: [{
                    label: chartOptions.label,
                    data: chartOptions.data,
                    backgroundColor: chartOptions.bgColors,
                    borderColor: chartOptions.borderColors,
                    borderWidth: 1,
                    fill: chartOptions.chartFill
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    };

    let DrawPlanetsPerClimate = function (chartContainer) {
        var ctx = $(`${chartContainer}`);
        let labels = [];
        let data = [];
        let bgColors = [];
        let brdColors = [];

        let doneGet = function (response) {
            for (var i of response) {
                labels.push(i.climateName);
                data.push(i.numberOfPlanets);
                bgColors.push(i.displayColor + "66");
                brdColors.push(i.displayColor);
            }
            let options = new ChartOptions(ctx, 'horizontalBar', data, 'Planets / Climate', labels, bgColors, brdColors);
            DrawChart(options);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getPlanetsPerClimate(doneGet, failGet);
    };
    let DrawPlanetsPerTerrain = function (chartContainer) {
        var ctx = $(`${chartContainer}`);
        let labels = [];
        let data = [];
        let bgColors = [];
        let brdColors = [];

        let doneGet = function (response) {
            for (var i of response) {
                labels.push(i.terrainName);
                data.push(i.numberOfPlanets);
                bgColors.push(i.displayColor + "66");
                brdColors.push(i.displayColor);
            }
            let options = new ChartOptions(ctx, 'horizontalBar', data, 'Planets / Terrain', labels, bgColors, brdColors);
            DrawChart(options);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getPlanetsPerTerrain(doneGet, failGet);
    };
    let DrawFlightsPerStatus = function (chartContainer) {
        var ctx = $(`${chartContainer}`);
        let labels = [];
        let data = [];
        let bgColors = [
            'rgba(54, 162, 235, 0.6)',
            'rgba(255, 159, 64, 0.6)',
            'rgba(255, 99, 132, 0.6)',
            'rgba(255, 206, 86, 0.6)',
            'rgba(75, 192, 192, 0.6)',
            'rgba(153, 102, 255, 0.6)'

        ];
        let brdColors = [
            'rgba(54, 162, 235, 1)',
            'rgba(255, 159, 64, 1)',
            'rgba(255, 99, 132, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)'

        ];

        let doneGet = function (response) {
            for (var i of response) {
                labels.push(i.statusName);
                data.push(i.flightsNumber);
            }
            let options = new ChartOptions(ctx, 'doughnut', data, 'Flights / Status', labels, bgColors, brdColors);
            DrawChart(options);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getFlightsPerStatus(doneGet, failGet);
    };

    let flightDetailsContainer = function (chartOcupancyRateContainer, chartRevenuePerDistanceContainer, chartExpectedRevenueContainer, chartTicketRevenueContainer) {
        this.chartOcupancyRateContainer = chartOcupancyRateContainer;
        this.chartRevenuePerDistanceContainer = chartRevenuePerDistanceContainer;
        this.chartExpectedRevenueContainer = chartExpectedRevenueContainer;
        this.chartTicketRevenueContainer = chartTicketRevenueContainer;
    }
    let DrawFlightsDetails = function (flightDetailsContainer) {
        var ocupancyRatectx = $(`${flightDetailsContainer.chartOcupancyRateContainer}`);
        var revenuePerDistancectx = $(`${flightDetailsContainer.chartRevenuePerDistanceContainer}`);
        var expectedRevenuectx = $(`${flightDetailsContainer.chartExpectedRevenueContainer}`);
        var ticketRevenuectx = $(`${flightDetailsContainer.chartTicketRevenueContainer}`);

        let type = "bar";
        let labels = [];
        let ocupancyRateData = [];
        let revenuePerDistanceData = [];
        let expectedRevenueData = [];
        let ticketRevenueData = [];
        let bgColors = 'rgba(54, 162, 235, 0.6)';
        let brdColors = 'rgba(54, 162, 235, 1)';

        let doneGet = function (response) {
            for (var i of response) {
                labels.push(i.flightId);
                ocupancyRateData.push(i.ocupancyRate);
                revenuePerDistanceData.push(i.revenuePerDistance);
                expectedRevenueData.push(i.expectedRevenue);
                ticketRevenueData.push(i.ticketRevenue);
            }
            let ocupancyRateOptions = new ChartOptions(ocupancyRatectx, type, ocupancyRateData, 'Ocupancy Rate', labels, bgColors, brdColors);
            let revenuePerDistanceOptions = new ChartOptions(revenuePerDistancectx, type, revenuePerDistanceData, 'Revenue Per Distance', labels, bgColors, brdColors);
            let expectedRevenueOptions = new ChartOptions(expectedRevenuectx, type, expectedRevenueData, 'Maximum Expected Revenue', labels, bgColors, brdColors);
            let ticketRevenueOptions = new ChartOptions(ticketRevenuectx, type, ticketRevenueData, 'Ticket Revenue', labels, bgColors, brdColors);
            DrawChart(ocupancyRateOptions);
            DrawChart(revenuePerDistanceOptions);
            DrawChart(expectedRevenueOptions);
            DrawChart(ticketRevenueOptions);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getFlightsDetails(doneGet, failGet);
    };

    let DrawFlightsPerDate = function (chartContainer) {
        var ctx = $(`${chartContainer}`);

        let labels = [];
        let data = [];
        let bgColors = [
            'rgba(54, 162, 235, 0.6)',
            'rgba(255, 159, 64, 0.6)',
            'rgba(255, 99, 132, 0.6)',
            'rgba(255, 206, 86, 0.6)',
            'rgba(75, 192, 192, 0.6)',
            'rgba(153, 102, 255, 0.6)'

        ];
        let brdColors = [
            'rgba(54, 162, 235, 1)',
            'rgba(255, 159, 64, 1)',
            'rgba(255, 99, 132, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)'

        ];
        let fillChart = false;

        let doneGet = function (response) {
            let dates = [];
            let filteredDates = [];

            for (var i of response) {
                dates.push(i.date);
                data.push(i.flightsNumber);
            }

            filteredDates = dates.sort(function (a, b) {
                let array1 = a.split("/");
                let array2 = b.split("/");
                return new Date(parseInt(array1[2]), parseInt(array1[1]) + 1, parseInt(array1[0]), 0, 0, 0, 0) - new Date(parseInt(array2[2]), parseInt(array2[1]) + 1, parseInt(array2[0]), 0, 0, 0, 0);
            });

            for (var i of dates) {
                labels.push(i);
            }
            let options = new ChartOptions(ctx, 'line', data, 'Number Of Flights', labels, bgColors, brdColors, fillChart);
            DrawChart(options);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getFlightsPerDate(doneGet, failGet);
    };

    let UpdateDatabaseNumbers = function () {

        let container = $("#database-numbers")

        let doneGet = function (response) {
            container.find($("#planets-number")).text(response.planets);
            container.find($("#flightPaths-number")).text(response.flightpaths);
            container.find($("#flighs-number")).text(response.flights);
            container.find($("#starships-number")).text(response.starships);
            container.find($("#climates-number")).text(response.climates);
            container.find($("#terrains-number")).text(response.terrains);
            container.find($("#races-number")).text(response.races);
            container.find($("#raceClassifications-number")).text(response.raceClassification);
            container.find($("#users-number")).text(response.users);
            container.find($("#orders-number")).text(response.orders);
            container.find($("#tickets-number")).text(response.tickets);
            container.find($("#revenues-number")).text(response.revenue);
            $("#dataset-timestamp").text(response.databaseTimeStamp);
        };

        let failGet = function () {
            console.log("something failed");
        };

        databaseStatisticsService().getDataBaseNumbers(doneGet, failGet);
    }

    return {
        init: init
    }

})(DatabaseStatisticsService);