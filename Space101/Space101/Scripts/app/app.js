//Requires bootbox
let CustomBootBoxAlerts = function () {

    let confirmDelete = function (modelName, confirmResult) {
        bootbox.confirm({
            message: `Are you sure you want to delete the ${modelName}?`,
            title: `Delete ${modelName}`,
            size: "small",
            buttons: {
                confirm: {
                    label: "Yes",
                    className: "btn-lg btn-primary"
                },
                cancel: {
                    label: "No",
                    className: "btn-lg"
                }
            },
            callback: function (result) {
                confirmResult(result)
            }
        });
    };

    let confirmDelete2 = function (modelName, confirmResult) {
        bootbox.confirm({
            message: `Are you sure you want to delete the ${modelName}?`,
            title: `Delete ${modelName}`,
            size: "big",
            buttons: {
                confirm: {
                    label: "Yes2",
                    className: "btn-lg btn-primary"
                },
                cancel: {
                    label: "No2",
                    className: "btn-lg"
                }
            },
            callback: function (result) {
                confirmResult(result)
            }
        });
    };

    let confirmChangeStatus = function (modelName, confirmResult) {
        bootbox.confirm({
            message: `Are you sure you want to change the ${modelName} Status?`,
            title: `Change ${modelName} status`,
            size: "big",
            buttons: {
                confirm: {
                    label: "Yes",
                    className: "btn-lg btn-primary"
                },
                cancel: {
                    label: "No",
                    className: "btn-lg"
                }
            },
            callback: function (result) {
                confirmResult(result)
            }
        });
    };

    let confirmManageSeat = function (modelName, confirmResult, actionName) {
        bootbox.confirm({
            message: `Are you sure you want to ${actionName} the ${modelName}?`,
            title: ` ${actionName[0].toUpperCase()}${actionName.substring(1, actionName.length)} ${modelName}`,
            size: "big",
            buttons: {
                confirm: {
                    label: "Yes",
                    className: "btn-lg btn-primary"
                },
                cancel: {
                    label: "No",
                    className: "btn-lg"
                }
            },
            callback: function (result) {
                confirmResult(result)
            }
        });
    };

    return {
        confirmDelete: confirmDelete,
        confirmDelete2: confirmDelete2,
        confirmChangeStatus: confirmChangeStatus,
        confirmManageSeat: confirmManageSeat
    }

}();
//Requires toastr
let CustomToasts = function () {

    let successDelete = function (modelName) {
        let options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr['success'](`The ${modelName} was succefully deleted`, `Everything is cool !!`, options);
    };

    let failDelete = function (modelName) {
        let options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr['error'](`The ${modelName} was not deleted`, `Something failed !!`, options);
    };

    let failChangeStatus = function (modelName) {
        let options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr['error'](`The ${modelName} Status was not changed`, `Something failed !!`, options);
    };

    let successManageModel = function (modelName, action) {
        let options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr['success'](`The ${modelName} was succefully ${action}`, `Everything is cool !!`, options);
    };

    let failManageModel = function (modelName,action, message) {
        let options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        if (message) {
            toastr['error'](`The ${modelName} was not ${action}`, message, options);
        }
        else {
            toastr['error'](`The ${modelName} was not ${action}`, `Something failed !!`, options);
        }
        
    };

    return {
        successDelete: successDelete,
        failDelete: failDelete,
        failChangeStatus: failChangeStatus,
        successManageModel: successManageModel,
        failManageModel: failManageModel
    }

}();