function GetSurveyAnswer(surveyId) {

    console.log("GetSurveyAnswer");

    var settings = {
        "url": "/WebApi/Survey/GetSurveyAnswer",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({ "surveyId": surveyId }),
    };

    console.log("settings", settings);

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            console.log("GetSurveyAnswer data", data);
            const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

            var items = '';
            $.each(data, function (index, value) {
                items +=
                    `
                <tr>
                    <td>${value.surveyAnswerId}</td>
                    <td>${value.surveyDescription}</td>
                    <td>${value.createdByEmail}</td>
                    <td>${value.createdDate}</td >
                    <td><a href="/Survey/Details/${value.surveyAnswerId}" class="button darkgreen small">รายละเอียด</a></td>
                </tr>
                `
            });

            $("#tblSurverAnswer > tbody").html(items);

        }
    });
}

$(document).ready(function () {

    GetSurveyAnswer("1");

});

