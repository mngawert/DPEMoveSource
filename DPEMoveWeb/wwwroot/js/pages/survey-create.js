
function GetToken() {

    var settings = {
        "url": "/api/Account/GetDPEToken",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
    };

    return $.ajax(settings);
}

function GetProvince(token) {

    console.log("call GetProvince");
    var form = new FormData();
    form.append("Token", token);

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

            // reload save answer
            var savedProvinceId = localStorage.getItem("saveAns_10");
            if (savedProvinceId != null) {
                $("#ddlProvince").val(savedProvinceId);
                GetAmphur(token, savedProvinceId);
            }
        }
    });
}

function SaveSection_1() {

    console.log("SaveSection_1");

    localStorage.setItem("saveAns_1", $("[name='AnswerValue_1']").val());
    localStorage.setItem("saveAns_2", $("[name='AnswerValue_2']").val());
    localStorage.setItem("saveAns_3", $("[name='AnswerValue_3']").val());
    localStorage.setItem("saveAns_4", $("[name='AnswerValue_4']").val());
    localStorage.setItem("saveAns_5", $("[name='AnswerValue_5']").val());
    localStorage.setItem("saveAns_6", $("[name='AnswerValue_6']").val());
    localStorage.setItem("saveAns_7", $("[name='AnswerValue_7']").val());
    localStorage.setItem("saveAns_8", $("[name='AnswerValue_8']:checked").val());
    localStorage.setItem("saveAns_9", $("[name='AnswerValue_9']").val());
    localStorage.setItem("saveAns_10", $("[name='AnswerValue_10']").val());
    localStorage.setItem("saveAns_11", $("[name='AnswerValue_11']").val());
    localStorage.setItem("saveAns_12", $("[name='AnswerValue_12']").val());
    localStorage.setItem("saveAns_13", $("[name='AnswerValue_13']").val());
    localStorage.setItem("saveAns_14", $("[name='AnswerValue_14']").val());
    localStorage.setItem("saveAns_15", $("[name='AnswerValue_15']:checked").val());
    localStorage.setItem("saveAns_16", $("[name='AnswerValue_16']").val());
    localStorage.setItem("saveAns_17", $("[name='AnswerValue_17']").val());
    localStorage.setItem("saveAns_18", $("[name='AnswerValue_18']").val());
}

function ReloadSection_1() {

    console.log("ReloadSection_1");
    console.log("ddl_10", localStorage.getItem("saveAns_10"));
    console.log("ddl_11", localStorage.getItem("saveAns_11"));
    console.log("ddl_12", localStorage.getItem("saveAns_12"));

    if (localStorage.getItem("saveAns_1") != null) $("[name='AnswerValue_1']").val(localStorage.getItem("saveAns_1"));
    if (localStorage.getItem("saveAns_2") != null) $("[name='AnswerValue_2']").val(localStorage.getItem("saveAns_2"));
    if (localStorage.getItem("saveAns_3") != null) $("[name='AnswerValue_3']").val(localStorage.getItem("saveAns_3"));
    if (localStorage.getItem("saveAns_4") != null) $("[name='AnswerValue_4']").val(localStorage.getItem("saveAns_4"));
    if (localStorage.getItem("saveAns_5") != null) $("[name='AnswerValue_5']").val(localStorage.getItem("saveAns_5"));
    if (localStorage.getItem("saveAns_6") != null) $("[name='AnswerValue_6']").val(localStorage.getItem("saveAns_6"));
    if (localStorage.getItem("saveAns_7") != null) $("[name='AnswerValue_7']").val(localStorage.getItem("saveAns_7"));
    //if (localStorage.getItem("saveAns_8") != null) $("[name='AnswerValue_8']").val(localStorage.getItem("saveAns_8"));
    if (localStorage.getItem("saveAns_9") != null) $("[name='AnswerValue_9']").val(localStorage.getItem("saveAns_9"));
    //if (localStorage.getItem("saveAns_10") != null) $("[name='AnswerValue_10']").val(localStorage.getItem("saveAns_10"));
    //if (localStorage.getItem("saveAns_11") != null) $("[name='AnswerValue_11']").val(localStorage.getItem("saveAns_11"));
    //if (localStorage.getItem("saveAns_12") != null) $("[name='AnswerValue_12']").val(localStorage.getItem("saveAns_12"));
    if (localStorage.getItem("saveAns_13") != null) $("[name='AnswerValue_13']").val(localStorage.getItem("saveAns_13"));
    if (localStorage.getItem("saveAns_14") != null) $("[name='AnswerValue_14']").val(localStorage.getItem("saveAns_14"));
    //if (localStorage.getItem("saveAns_15") != null) $("[name='AnswerValue_15']").val(localStorage.getItem("saveAns_15"));
    if (localStorage.getItem("saveAns_16") != null) $("[name='AnswerValue_16']").val(localStorage.getItem("saveAns_16"));
    if (localStorage.getItem("saveAns_17") != null) $("[name='AnswerValue_17']").val(localStorage.getItem("saveAns_17"));
    if (localStorage.getItem("saveAns_18") != null) $("[name='AnswerValue_18']").val(localStorage.getItem("saveAns_18"));

    $("[name='AnswerValue_8']").each(function () {
        if ($(this).val() == localStorage.getItem("saveAns_8")) {
            $(this).prop("checked", true);
        }
    });
    $("[name='AnswerValue_15']").each(function () {
        if ($(this).val() == localStorage.getItem("saveAns_15")) {
            $(this).prop("checked", true);
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

            var savedAmphurId = localStorage.getItem("saveAns_11");
            if (savedAmphurId != null) {
                $("#ddlAmphur").val(savedAmphurId);
                GetTambon(token, PROV_CODE, savedAmphurId);
            }
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

            var savedTambonId = localStorage.getItem("saveAns_12");
            if (savedTambonId != null) {
                $("#ddlTambon").val(savedTambonId);
            }
        }
    });
}

function GetActivityType(token, SECTION_CAT_ID, FOR_ID) {
    var form = new FormData();
    form.append("Token", token);
    form.append("SECTION_CAT_ID", SECTION_CAT_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/activity/type/getActivityType",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            response = response.replace(/\ufeff/g, ''); //Remove BOM character
            var results = JSON.parse(response);
            var data = results.data;
            PROVINCE_DATA = data;
            var items =
                `
            <option value="">- ระบุ -</option>
            `
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.ACT_TYPE_NAME + `">` + value.ACT_TYPE_NAME + `</option>
                `
            });
            $(`[id^='${FOR_ID}']`).html(items);
        }
    });
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
    var answerValue_57 = $("[name='AnswerValue_57']").val();
    var answerValue_58 = $("[name='AnswerValue_58']").val();
    var answerValue_59 = $("[name='AnswerValue_59']").val();
    var answerValue_60 = $("[name='AnswerValue_60']").val();
    var answerValue_61 = $("[name='AnswerValue_61']").val();
    var answerValue_62 = $("[name='AnswerValue_62']").val();
    var answerValue_63 = $("[name='AnswerValue_63']").val();
    var answerValue_64 = $("[name='AnswerValue_64']").val();
    var answerValue_65 = $("[name='AnswerValue_65']").val();
    var answerValue_66 = $("[name='AnswerValue_66']").val();
    var answerValue_67 = $("[name='AnswerValue_67']").val();
    var answerValue_68 = $("[name='AnswerValue_68']").val();
    var answerValue_69 = $("[name='AnswerValue_69']").val();
    var answerValue_70 = $("[name='AnswerValue_70']").val();
    var answerValue_71 = $("[name='AnswerValue_71']").val();

    var answerValue_72 = $("[name='AnswerValue_72']").val();
    var answerValue_73 = $("[name='AnswerValue_73']").val();

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

            { "questionId": "55", "answerValue": answerValue_55 },
            { "questionId": "56", "answerValue": answerValue_56 },
            { "questionId": "57", "answerValue": answerValue_57 },
            { "questionId": "58", "answerValue": answerValue_58 },
            { "questionId": "59", "answerValue": answerValue_59 },
            { "questionId": "60", "answerValue": answerValue_60 },
            { "questionId": "61", "answerValue": answerValue_61 },
            { "questionId": "62", "answerValue": answerValue_62 },
            { "questionId": "63", "answerValue": answerValue_63 },
            { "questionId": "64", "answerValue": answerValue_64 },
            { "questionId": "65", "answerValue": answerValue_65 },
            { "questionId": "66", "answerValue": answerValue_66 },
            { "questionId": "67", "answerValue": answerValue_67 },
            { "questionId": "68", "answerValue": answerValue_68},
            { "questionId": "69", "answerValue": answerValue_69 },
            { "questionId": "70", "answerValue": answerValue_70 },
            { "questionId": "71", "answerValue": answerValue_71 },

            //{ "questionId": "72", "answerValue": answerValue_72 },
            //{ "questionId": "73", "answerValue": answerValue_73 }
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
    $("[name^='AnswerValue_72']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "72", "answerValue": $(this).val(), "answerText": $(this).val() == "9" ? $("[name='AnswerValue_72_9_txt']").val() : "" };

            SurveyAnswer.push(obj);
        }
    });
    $("[name^='AnswerValue_73']").each(function () {
        if ($(this).prop("checked") == true) {

            var obj = { "questionId": "73", "answerValue": $(this).val(), "answerText": $(this).val() == "9" ? $("[name='AnswerValue_73_9_txt']").val() : "" };

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
            ShowPopupSurveyCreateSuccess();
        }
    });
}

function ShowPopupSurveyCreateSuccess() {

    var settings = {
        "url": "/WebApi/Master/GetConfig",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "name": "SurveyCreateSuccessMessage" }),
    };

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            $.each(data, function (index, value) {
                $("#dvSurveyCreateSuccessMessage").html(value.value);
            });
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

    // for testing
    //$("#Modal_Results").modal("show");

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetProvince(token);
        ReloadSection_1();

        //Need to change
        GetActivityType(token, 1, "ddlSportType");
        GetActivityType(token, 2, "ddlRecreation");
    });

    EnableDisableQuestion_15();

    $("#ddlProvince").change(function () {
        localStorage.removeItem("saveAns_10")
        localStorage.removeItem("saveAns_11")
        localStorage.removeItem("saveAns_12")

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
        localStorage.removeItem("saveAns_10")
        localStorage.removeItem("saveAns_11")
        localStorage.removeItem("saveAns_12")

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


    $("#chkAnswerValue_50").click(function () {

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

        SaveSection_1();

        SurveyCreate(inputData);
    });

});

