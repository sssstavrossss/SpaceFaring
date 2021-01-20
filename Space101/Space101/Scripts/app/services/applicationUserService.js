let ApplicationUserService = function () {
    let changeRole = function (userId, roleId, done, fail) {

        var data = {
            'userId': userId,
            'roleId': roleId
        };

        $.ajax({
            url: '/api/users',
            type: "POST",
            data: data,
            dataType: 'json'
        })
            .done(function (data) { done(data); })
            .fail(fail);

    };

    return {
        changeRole: changeRole
    };
}();