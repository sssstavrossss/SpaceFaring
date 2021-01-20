let TicketController = ((ticketService) => {
    let button;
    let table;
    let modelName = 'Ticket';

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };
    let initializeDatabaseManagerDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };
    let initializeDeleteTicket = function (container, confirmType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-delete', function () {
            button = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteTicket(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        };

        let deleteTicket = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = button.attr("data-ticket-id");
                ticketService().deleteTicket(id, doneDelete, failDelete);
            }
        }

        let doneDelete = function () {
            table
                .row(button.parents('tr'))
                .remove()
                .draw();

            if (doneAlert) {
                doneAlert(modelName, "deleted");
            };
        }

        let failDelete = function (jqxhr) {
            let message = JSON.parse(jqxhr.responseText).message;
            if (failAlert)
                failAlert(modelName, "deleted", message);
        };
    }

    return {
        initializeDeleteTicket: initializeDeleteTicket,
        initializeDataTable: initializeDataTable,
        initializeDatabaseManagerDataTable: initializeDatabaseManagerDataTable
    }

})(TicketService);