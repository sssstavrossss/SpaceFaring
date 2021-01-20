//Needs Globalize to work
let LocalCultureController = ((localCultureService) => {
    let cultureName;

    let initLocalCulture = function () {
        localCultureService().getServerCulture(doneCulture, failCulture);
    };

    let doneCulture = function (data) {
        cultureName = data;
        Globalize.culture(cultureName);
        getCultureName();
    }

    let getCultureName = function () {
        let result;
        for (var culture in Globalize()['cultures']) {
            if (culture !== 'en' && culture !== 'default') {
                result = culture;
            }
        }
        if (!result)
            result = 'en';
        return result;
    };

    let failCulture = function () {
        alert("Something Failed");
    }

    return {
        initLocalCulture: initLocalCulture,
        cultureName: getCultureName()
    }

})(LocalCultureService);