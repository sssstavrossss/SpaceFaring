let RaceClassificationsController = function (raceClassificationsService) {
    let classificationsDiv = $('#classification-div');
    let name = $('#name');
    let span;
    let id;
    let nameToDelete;
    let validation = false;

    let createTag = function (name, id) {
        return `<div class="classification-tag" data-id="${id}">
                                 <span class="tag-name">${name}</span>
                                 <span title="Delete" class="delete">&#10006;</span>
                            </div>`;
    };

    let tagList = [];

    let init = function () {
        
        populateDiv();

        $(classificationsDiv).on('click', '.delete', function (b) {
            span = $(b.target);
            id = Number(span.closest('div').attr('data-id'));
            nameToDelete = span.siblings('span').text();
            deleteClassification();
        });

        $('#add-classification').on('click', function (e) {
            e.preventDefault();
            createClassification();
            formValidate();
            console.log(raceClassification.name.length);
            console.log(validation);
            if (validation) {
                submitForm();
            };
            
        })
        
    };

    let populateDiv = function () {
        raceClassificationsService.getRaceClassifications(failGet, doneGet);
    };

    let raceClassification = {};

    let createClassification = function () {
        raceClassification.name = name.val();
        raceClassification.raceClassificationID = 0;
    };

    let failGet = function () {
        bootbox.alert("Something went wrong! ");
    };

    let doneGet = function (data) {
        classificationsDiv.empty();
        $.each(data, function (key, entry) {
            tagList.push(createTag(entry.name, entry.raceClassificationID))
        });
        classificationsDiv.append(tagList);
    }

    let donePost = function (id) {
        $('#name').val('');
        let newTag = createTag(raceClassification.name, id);
        classificationsDiv.append(newTag);
    };

    let failPost = function () {
        bootbox.alert("Race classification failed to get posted! ");
    };

    let doneDelete = function () {
        bootbox.alert(`Race classification ${nameToDelete} was deleted!`);
        span.closest('div').remove();
    };

    let failDetele = function () {
        bootbox.alert(`Race classification ${nameToDelete} failed to be deleted!`);
    };

    let deleteClassification = function() {
        bootbox.confirm(`Are you sure to delete ${nameToDelete}?`, function (result) {
            if (result) {
                raceClassificationsService.deleteRaceClassification(id, doneDelete, failDetele);
            };
        });
    };

    let submitForm = function () {
        console.log(validation);
        if (validation) {
            raceClassificationsService.createRaceClassification(failPost, donePost, raceClassification);
        };
    };

    let formValidate = function () {
        $('#error').text('');
        let noLength = 'Name is required<br/>';
        let lowLength = 'Name most be more than 2 characters<br/>';
        let highLength = 'Name most be more than 2 characters<br/>';
        let length = Number(raceClassification.name.length);

        validation = raceClassification.name.length <= 50 && raceClassification.name.length >= 2;

        if (!validation) {
            if (length == 0) {
                $('#error').append(noLength);
            } else if (length >= 50) {
                $('#error').append(highLength);
            } else {
                $('#error').append(lowLength);
            }
        }
    };

    return {
        init: init
    }

}(RaceClassificationsService);