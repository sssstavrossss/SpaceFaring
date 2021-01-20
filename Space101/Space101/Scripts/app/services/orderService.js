let OrderService = () => {
    let getOrderTickets = function (id, done, fail) {
        $.ajax({
            url: `/api/orders/getTickets/${id}`,
            method: "GET"
        })
            .done(done)
            .fail(fail);
    };

    let deleteOrder = function (id, done, fail) {
        $.ajax({
            url: `/api/orders/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        getOrderTickets: getOrderTickets,
        deleteOrder: deleteOrder
    };
};