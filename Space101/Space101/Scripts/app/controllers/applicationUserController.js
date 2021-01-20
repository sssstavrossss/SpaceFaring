let ApplicationUserController = function (applicationUserService) {
    
    let roleId;
    let userId;
    let select;
    let text;

    let init = function (container) {
        
        $(container).on("change", ".role-list", function (e) {
            select = $(e.target);
            changeRole();
        });
    };

    let changeRole = function () {
        roleId = select.children('option:selected').val();
        userId = select.attr('data-user-id');
        applicationUserService.changeRole(userId, roleId, done, fail)
    };

    let done = function (name) {
        select.parent('td').siblings('.js-user-role').text(name);
        //select.p .siblings('.js-user-role').text(name);
        console.log('done');
    };

    let fail = function () {
        console.log('fail');
    };

    return {
        init: init
    }

}(ApplicationUserService);