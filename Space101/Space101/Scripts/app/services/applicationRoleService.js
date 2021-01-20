let ApplicationRoleService = function () {
    let toggleRoleActivity = function (id, doneToggle, failToggle) {
        $.ajax({
            url: `/api/applicationroles/${id}`,
            method: "POST"
        })
            .done(function (data) {
                doneToggle(data);
            })
            .fail(failToggle);
    };

    return {
        toggleRoleActivity: toggleRoleActivity
    }
}();