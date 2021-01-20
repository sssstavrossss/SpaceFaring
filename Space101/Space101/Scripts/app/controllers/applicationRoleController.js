let ApplicationRoleController = function (applicationRoleService) {
    let id;
    let name;
    let tag;
    let target;

    let init = function (container) {
        $(container).on('click', '.js-activity', function (e) {
            target = $(e.target)
            tag = target.closest('div');
            toggleRoleActivity();
        });
    };

    let toggleRoleActivity = function () {
        id = tag.attr('data-id');
        name = target.siblings('.role-name').text();
        applicationRoleService.toggleRoleActivity(id, doneToggle, failToggle);
    };

    let doneToggle = function (activity) {

        if (activity) {
            activity = 'active';
            target.html('&#10004;');
            target.removeClass('inactive');
            target.addClass('active');
        }
        else {
            activity = 'inactive';
            target.html('&#10006;');
            target.removeClass('active');
            target.addClass('inactive');
        }

        bootbox.alert(`Application Role ${name} is now ${activity}!`);

    };

    let failToggle = function () {
        bootbox.alert(`Application Role ${name} failed to change activity!`);
    };

    return {
        init: init
    };

}(ApplicationRoleService);