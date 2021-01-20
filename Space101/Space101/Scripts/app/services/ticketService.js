let TicketService = () => {

    let deleteTicket = function (id, done, fail) {
        $.ajax({
            url: `/api/tickets/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteTicket: deleteTicket
    };
};