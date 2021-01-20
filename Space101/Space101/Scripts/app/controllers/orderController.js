let OrderController = ((orderService) => {
    let button;
    let table;
    let modelName = 'Order';

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable();
    }
    let initializeGetOrderTickets = function (container, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-get-order-tickets', function () {
            button = $(this);
            assignAlerts(doneAlertType, failAlertType);
            getOrderTickets();
        });

        let getOrderTickets = function () {
            let id = button.attr("data-order-id");
            orderService().getOrderTickets(id, doneGet, failGet);
        }

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let doneGet = function (tickets) {
            let tr = button.closest("tr");
            let row = table.row(tr);

            if (row.child.isShown()) {
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                row.child(ticketsTemplate(tickets).detailsTable).show();
                tr.addClass('shown');
            }

            if (doneAlert)
                doneAlert(modelName);
        };

        let failGet = function () {

            let tr = button.closest("tr");
            let row = table.row(tr);

            let emptyTr = $(`<tr>
                    <td colspan="11">There are no tickets in the order</td>
                    </tr>`);

            if (row.child.isShown()) {
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                row.child(emptyTr).show();
                tr.addClass('shown');
            }

            if (failAlert)
                failAlert(modelName);
        };

        let ticketsTemplate = function (tickets) {

            let detailsTable = $("<table>").addClass("table table-condensed table-responsive table-striped text-center table-bordered");
            let detailsTHead = $(`<thead>
                    <tr>
                        <th>Ticket Id</td>
                        <th>Flight Path</td>
                        <th>Date</td>
                        <th>Time</td>
                        <th>Flight Id</td>
                        <th>Seat Id</td>
                        <th>Class</td>
                        <th>Firstname</td>
                        <th>Lastname</td>
                        <th>Race</td>
                        <th>Homeplanet</td>
                        <th>Price</td>
                    <tr />
                </thead>`)
            let detailsTBody = $("<tbody>");
            let detailsTr = function (ticket) {

                return `<tr>
                        <td>${ticket.ticketId}</td>
                        <td>${ticket.path}</td>
                        <td>${ticket.date}</td>
                        <td>${ticket.time}</td>
                        <td>${ticket.flightId}</td>
                        <td>${ticket.seatId}</td>
                        <td>${ticket.travelClass}</td>
                        <td>${ticket.passengerName}</td>
                        <td>${ticket.passengerLastName}</td>
                        <td>${ticket.race}</td>
                        <td>${ticket.planet}</td>
                        <td>${ticket.price}</td>
                    </tr>`
            }

            for (var ticket of tickets) {
                detailsTBody.append(detailsTr(ticket));
            }
            detailsTable.append(detailsTHead).append(detailsTBody);

            return {
                detailsTable: detailsTable
            }
        }
    }
    let initializeDeleteOrder = function (container, confirmType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-delete', function () {
            button = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteOrder(confirmType);
        });
        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteOrder = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = button.attr("data-delete-order-id");
                orderService().deleteOrder(id, doneDelete, failDelete);
            }
        }

        let doneDelete = function () {
            table
                .row(button.parents('tr'))
                .remove()
                .draw();

            if (doneAlert) {
                doneAlert(modelName, "deleted");
            }
        };

        let failDelete = function (jqxhr) {
            let message = JSON.parse(jqxhr.responseText).message;
            if (failAlert)
                failAlert(modelName, "deleted", message);
        };
    };

    return {
        initializeDataTable: initializeDataTable,
        initializeGetOrderTickets: initializeGetOrderTickets,
        initializeDeleteOrder: initializeDeleteOrder
    }

})(OrderService);