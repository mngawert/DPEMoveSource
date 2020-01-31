
function GetToken() {
    var form = new FormData();
    form.append("username", "dpeusers");
    form.append("password", "users_api@dpe.go.th");

    var settings = {
        "url": "https://data.dpe.go.th/api/tokens/keys/tokens",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    return $.ajax(settings);
}

function GetProvince(Token) {

    console.log("call GetProvince");
    var form = new FormData();
    form.append("Token", Token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/location/getProvince",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var results = JSON.parse(response);
            var data = results.data;
            PROVINCE_DATA = data;
            var items =
                `
            <option value="">แสดงทั้งหมด</option>
            `
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.PROV_CODE + `">` + value.PROV_NAMT + `</option>
                `
            });
            $("#ddlProvince").html(items);
        }
    });
}

function GetAmphur(token, PROV_CODE) {


    var form = new FormData();
    form.append("PROV_CODE", PROV_CODE);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/location/getAmpher",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var results = JSON.parse(response);
            var data = results.data;
            var items =
                `
            <option value="">แสดงทั้งหมด</option>
            `
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.AMP_CODE + `">` + value.AMP_NAMT + `</option>
                `
            });
            $("#ddlAmphur").html(items);
        }
    });
}

function GetTambon(token, PROV_CODE, AMP_CODE) {

    var form = new FormData();
    form.append("PROV_CODE", PROV_CODE);
    form.append("AMP_CODE", AMP_CODE);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/location/getTambol",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var results = JSON.parse(response);
            var data = results.data;
            var items =
                `
            <option value="">แสดงทั้งหมด</option>
            `
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.TAM_CODE + `">` + value.TAM_NAMT + `</option>
                `
            });
            $("#ddlTambon").html(items);
        }
    });
}

function GetSportType() {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=sports_type";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        PROVINCE_DATA = data.DATA;
        var items =
            `
            <option value="">- ระบุ -</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.SPORTS_TYPE_ID + `">` + value.SPORTS_TYPE_NAME + `</option>
                `
        });
        $("#ddlSportType_1").html(items);
        $("#ddlSportType_2").html(items);
        $("#ddlSportType_3").html(items);
        $("#ddlSportType_4").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetAnswers() {

    var answerValue_1 = $("[name='AnswerValue_1']").val();
    var answerValue_2 = $("[name='AnswerValue_2']").val();
    var answerValue_3 = $("[name='AnswerValue_3']").val();
    var answerValue_4 = $("[name='AnswerValue_4']").val();
    var answerValue_5 = $("[name='AnswerValue_5']").val();
    var answerValue_6 = $("[name='AnswerValue_6']").val();
    var answerValue_7 = $("[name='AnswerValue_7']").val();
    var answerValue_8 = $("[name='AnswerValue_8']:checked").val();
    var answerValue_9 = $("[name='AnswerValue_9']").val();
    var answerValue_10 = $("[name='AnswerValue_10']").val();
    var answerValue_11 = $("[name='AnswerValue_11']").val();
    var answerValue_12 = $("[name='AnswerValue_12']").val();
    var answerValue_13 = $("[name='AnswerValue_13']").val();
    var answerValue_14 = $("[name='AnswerValue_14']").val();
    var answerValue_15 = $("[name='AnswerValue_15']:checked").val();
    var answerValue_16 = $("[name='AnswerValue_16']").val();
    var answerValue_17 = $("[name='AnswerValue_17']").val();
    var answerValue_18 = $("[name='AnswerValue_18']").val();
    var answerValue_19 = $("[name='AnswerValue_19']").val();
    var answerValue_20 = $("[name='AnswerValue_20']").val();
    var answerValue_21 = $("[name='AnswerValue_21']:checked").val();
    var answerValue_22 = $("[name='AnswerValue_22']:checked").val();
    var answerValue_23 = $("[name='AnswerValue_23']").val();
    var answerValue_24 = $("[name='AnswerValue_24']:checked").val(); var answerValue_24_txt = $("[name='AnswerValue_24_txt']").val();
    var answerValue_25 = $("[name='AnswerValue_25']:checked").val(); var answerValue_25_txt = $("[name='AnswerValue_25_txt']").val();
    var answerValue_26 = $("[name='AnswerValue_26']:checked").val(); var answerValue_26_txt = $("[name='AnswerValue_26_txt']").val();
    var answerValue_27 = $("[name='AnswerValue_27']:checked").val(); var answerValue_27_txt = $("[name='AnswerValue_27_txt']").val();
    var answerValue_28 = $("[name='AnswerValue_28']:checked").val(); var answerValue_28_txt = $("[name='AnswerValue_28_txt']").val();
    var answerValue_29 = $("[name='AnswerValue_29']:checked").val(); var answerValue_29_txt = $("[name='AnswerValue_29_txt']").val();
    var answerValue_30 = $("[name='AnswerValue_30']").val();
    var answerValue_31 = $("[name='AnswerValue_31']").val();
    var answerValue_32 = $("[name='AnswerValue_32']:checked").val();
    var answerValue_33 = $("[name='AnswerValue_33']:checked").val();
    var answerValue_34 = $("[name='AnswerValue_34']").val();
    var answerValue_35 = $("[name='AnswerValue_35']:checked").val();
    var answerValue_36 = $("[name='AnswerValue_36']").val();
    var answerValue_37 = $("[name='AnswerValue_37']").val();
    var answerValue_38 = $("[name='AnswerValue_38']").val();
    var answerValue_39 = $("[name='AnswerValue_39']:checked").val();
    var answerValue_40 = $("[name='AnswerValue_40']").val();
    var answerValue_41 = $("[name='AnswerValue_41']").val();
    var answerValue_42 = $("[name='AnswerValue_42']").val();
    var answerValue_43 = $("[name='AnswerValue_43']:checked").val();
    var answerValue_44 = $("[name='AnswerValue_44']").val();
    var answerValue_45 = $("[name='AnswerValue_45']").val();
    var answerValue_46 = $("[name='AnswerValue_46']").val();
    var answerValue_47 = $("[name='AnswerValue_47']:checked").val();
    var answerValue_48 = $("[name='AnswerValue_48']").val();
    var answerValue_49 = $("[name='AnswerValue_49']").val();
    var answerValue_50 = $("[name='AnswerValue_50']:checked").val();
    var answerValue_51 = $("[name='AnswerValue_51']").val();
    var answerValue_52 = $("[name='AnswerValue_52']").val();
    var answerValue_53 = $("[name='AnswerValue_53']").val();
    var answerValue_54 = $("[name='AnswerValue_54']").val();
    var answerValue_55 = $("[name='AnswerValue_55']").val();
    var answerValue_56 = $("[name='AnswerValue_56']").val();

    var SurveyAnswer =
        [
            { "questionId": "1", "answerValue": answerValue_1 },
            { "questionId": "2", "answerValue": answerValue_2 },
            { "questionId": "3", "answerValue": answerValue_3 },
            { "questionId": "4", "answerValue": answerValue_4 },
            { "questionId": "5", "answerValue": answerValue_5 },
            { "questionId": "6", "answerValue": answerValue_6 },
            { "questionId": "7", "answerValue": answerValue_7 },
            { "questionId": "8", "answerValue": answerValue_8 },
            { "questionId": "9", "answerValue": answerValue_9 },
            { "questionId": "10", "answerValue": answerValue_10 },
            { "questionId": "11", "answerValue": answerValue_11 },
            { "questionId": "12", "answerValue": answerValue_12 },
            { "questionId": "13", "answerValue": answerValue_13 },
            { "questionId": "14", "answerValue": answerValue_14 },
            { "questionId": "15", "answerValue": answerValue_15 },
            { "questionId": "16", "answerValue": answerValue_16 },
            { "questionId": "17", "answerValue": answerValue_17 },
            { "questionId": "18", "answerValue": answerValue_18 },
            { "questionId": "19", "answerValue": answerValue_19 },
            { "questionId": "20", "answerValue": answerValue_20 },
            { "questionId": "21", "answerValue": answerValue_21 },
            { "questionId": "22", "answerValue": answerValue_22 },
            { "questionId": "23", "answerValue": answerValue_23 },
            { "questionId": "24", "answerValue": answerValue_24, "answerText": answerValue_24_txt },
            { "questionId": "25", "answerValue": answerValue_25, "answerText": answerValue_25_txt },
            { "questionId": "26", "answerValue": answerValue_26, "answerText": answerValue_26_txt },
            { "questionId": "27", "answerValue": answerValue_27, "answerText": answerValue_27_txt },
            { "questionId": "28", "answerValue": answerValue_28, "answerText": answerValue_28_txt },
            { "questionId": "29", "answerValue": answerValue_29, "answerText": answerValue_29_txt },
            { "questionId": "30", "answerValue": answerValue_30 },
            { "questionId": "31", "answerValue": answerValue_31 },
            { "questionId": "32", "answerValue": answerValue_32 },
            { "questionId": "33", "answerValue": answerValue_33 },
            { "questionId": "34", "answerValue": answerValue_34 },
            { "questionId": "35", "answerValue": answerValue_35 },
            { "questionId": "36", "answerValue": answerValue_36 },
            { "questionId": "37", "answerValue": answerValue_37 },
            { "questionId": "38", "answerValue": answerValue_38 },
            { "questionId": "39", "answerValue": answerValue_39 },
            { "questionId": "40", "answerValue": answerValue_40 },
            { "questionId": "41", "answerValue": answerValue_41 },
            { "questionId": "42", "answerValue": answerValue_42 },
            { "questionId": "43", "answerValue": answerValue_43 },
            { "questionId": "44", "answerValue": answerValue_44 },
            { "questionId": "45", "answerValue": answerValue_45 },
            { "questionId": "46", "answerValue": answerValue_46 },
            { "questionId": "47", "answerValue": answerValue_47 },
            { "questionId": "48", "answerValue": answerValue_48 },
            { "questionId": "49", "answerValue": answerValue_49 },
            { "questionId": "50", "answerValue": answerValue_50 },
            //{ "questionId": "51", "answerValue": answerValue_51 },
            //{ "questionId": "52", "answerValue": answerValue_52 },
            //{ "questionId": "53", "answerValue": answerValue_53 },
            //{ "questionId": "54", "answerValue": answerValue_54 },
            //{ "questionId": "55", "answerValue": answerValue_55 },
            //{ "questionId": "56", "answerValue": answerValue_56 }
        ]
        ;

    $("[name^='AnswerValue_51']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "51", "answerValue": $(this).val(), "answerText": $(this).val() == "8" ? $("[name='AnswerValue_51_8_txt']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_52']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "52", "answerValue": $(this).val(), "answerText": "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_53']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "53", "answerValue": $(this).val(), "answerText": $(this).val() == "12" ? $("[name='AnswerValue_53_12_txt']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_54']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "54", "answerValue": $(this).val(), "answerText": $(this).val() == "11" ? $("[name='AnswerValue_54_11']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_55']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "55", "answerValue": $(this).val(), "answerText": $(this).val() == "9" ? $("[name='AnswerValue_55_9_txt']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_56']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "56", "answerValue": $(this).val(), "answerText": $(this).val() == "9" ? $("[name='AnswerValue_56_9_txt']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });

    var data = JSON.stringify(
        {
            "SurveyId" : "1",
            "SurveyAnswer": SurveyAnswer
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

function ShowTabIndex(c) {
    $(".tab").removeClass("active").eq(c).addClass("active");
    $(".tab_item").hide().eq(c).fadeIn();
}

function EnableDisableQuestion_15() {

    var answerValue_50 = $("[name='AnswerValue_50']");
    if (answerValue_50.prop("checked") == true) {
        $("#tr_15_1_1, #tr_15_1_2, #tr_15_1_3, #tr_15_1_4").find('input').removeAttr('checked').val("");
        $("#tr_15_1_1, #tr_15_1_2, #tr_15_1_3, #tr_15_1_4").addClass("disabledbutton");
        $("#dv_15_2, #dv_15_3, #dv_15_4").addClass("disabledbutton");
        $("#dv_15_5").removeClass("disabledbutton");
    }
    else {
        $("#tr_15_1_1, #tr_15_1_2, #tr_15_1_3, #tr_15_1_4").removeClass("disabledbutton");
        $("#dv_15_2, #dv_15_3, #dv_15_4").removeClass("disabledbutton");
        $("#dv_15_5").addClass("disabledbutton");
    }
}

$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetProvince(token);

        //Need to change
        //GetSportType();
    });

    EnableDisableQuestion_15();

    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">กรุณาเลือก</option>`);
            $("#ddlTambon").html(`<option value="">กรุณาเลือก</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, provinceId);
            $("#ddlTambon").html(`<option value="">กรุณาเลือก</option>`);
        }
    });

    $("#ddlAmphur").change(function () {
        var provinceId = $("#ddlProvince").val();
        var amphurId = $("#ddlAmphur").val();

        if (amphurId == "") {
            $("#ddlTambon").html(`<option value="">กรุณาเลือก</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetTambon(token, provinceId, amphurId);
        }
    });


    $("#lblAnswerValue_50").click(function () {

        EnableDisableQuestion_15();
    });

    $("#btnSubmit").click(function () {

        if (!$("#frmSurveyCreate_1")[0].checkValidity()) {
            ShowTabIndex("0");
            $("#frmSurveyCreate_1")[0].reportValidity()
            return false;
        }
        if (!$("#frmSurveyCreate_2")[0].checkValidity()) {
            ShowTabIndex("1");
            $("#frmSurveyCreate_2")[0].reportValidity()
            return false;
        }
        if (!$("#frmSurveyCreate_3")[0].checkValidity()) {
            ShowTabIndex("2");
            $("#frmSurveyCreate_3")[0].reportValidity()
            return false;
        }
        if (!$("#frmSurveyCreate_4")[0].checkValidity()) {
            ShowTabIndex("3");
            $("#frmSurveyCreate_4")[0].reportValidity()
            return false;
        }

        var inputData = GetAnswers();

        console.log("inputData", inputData);
        //return false;

        SurveyCreate(inputData);
    });

});

