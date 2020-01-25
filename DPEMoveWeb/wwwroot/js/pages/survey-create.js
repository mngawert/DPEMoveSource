
function GetAnswers() {

    var answerValue_1 = $("[name='AnswerValue_1']").val();
    var answerValue_2 = $("[name='AnswerValue_2']").val();

    var data = JSON.stringify(
        {
            "SurveyAnswer":
                [
                    { "questionId": "1", "answerValue": answerValue_1 },
                    { "questionId": "2", "answerValue": answerValue_2 }
                ]
        });

    return data;
}

function SurveyCreate(inputData) {

    console.log("SurveyCreate");

    var settings = {
        "url": "/WebApi/Survey/Create",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": inputData,
    };

    console.log("settings", settings);

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {

            console.log("SurveyCreate data", data);
            $("#Modal_Results").modal("show");
        }
    });
}

$(document).ready(function () {

    $("#btnSubmit").click(function () {

        var inputData = GetAnswers();

        SurveyCreate(inputData);
    });

});

