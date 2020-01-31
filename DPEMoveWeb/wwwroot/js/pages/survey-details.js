function GetSurveyAnswerDetails(surveyAnswerId) {

    console.log("GetSurveyAnswerDetails");

    var settings = {
        "url": "/WebApi/Survey/GetSurveyAnswerDetails",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({ "surveyAnswerId": surveyAnswerId }),
    };

    console.log("settings", settings);

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            console.log("GetSurveyAnswer data", data);

            var items = '';
            $.each(data, function (index, value) {
                items +=
                    `
                <tr>
                    <td>${value.questionId}</td>
                    <td>${value.questionText}</td>
                    <td>${value.answerValue}</td>
                    <td>${value.answerText}</td>
                </tr>
                `
            });

            $("#tblSurverAnswerDetails > tbody").html(items);

        }
    });
}

$(document).ready(function () {

    var url = window.location.href;
    console.log("url", url);
    var id = url.substring(url.lastIndexOf('/') + 1);

    GetSurveyAnswerDetails(id);

});

