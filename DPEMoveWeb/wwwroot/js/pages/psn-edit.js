﻿
function GetProvinceNameById(provinceId) {

    if (provinceId == null)
        return "";

    //console.log("call GetProvinceNameById", provinceId);
    var provinceName = "n/a";
    $.each(PROVINCE_DATA, function (index, value) {
        if (value.PROV_CODE == provinceId) {
            provinceName = value.PROV_NAMT;
            return false;
        }
    });
    return provinceName;
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

function GetStadiumType(Token) {

    var form = new FormData();
    form.append("Token", Token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/stadiumType/getStadiumType",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        var results = JSON.parse(response);
        var data = results.data;
        var items = '';
        $.each(data, function (index, value) {

            items +=
                `
                <option value="` + value.GROUP_ID + `">` + value.GROUP_NAME + `</option>
            `
        });
        $("#ddlStadiumType").append(items);
    });
}

//function SearchPSN(token, DATA_REPLACE_OR_APPEND, PAGE) {

//    var txtName = $("#txtName").val();
//    var provinceId = $("#ddlProvince").val();
//    var amphurId = $("#ddlAmphur").val();
//    var tambonId = $("#ddlTambon").val();

//    GetPSN(token, DATA_REPLACE_OR_APPEND, PAGE, txtName, provinceId, amphurId);
//}

function GetPSN(token, HRS_ID) {

    var form = new FormData();
    form.append("Token", token);
    form.append("HRS_ID", HRS_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/member/getGmsMember",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    //console.log("settings", settings);
    $.ajax(settings).done(function (response) {

        var results = JSON.parse(response);
        //console.log("results", results);

        var data = results.data;
        var items = '';
        $.each(data, function (index, value) {

            $("#dvMEMBER_IMAGE").html(`<img src="${value.MEMBER_IMAGE == null ? "/images/psn010101_02.png" : value.MEMBER_IMAGE }" />`);
            $("#lblName").html(`${value.FIRST_NAME} ${value.LAST_NAME}`);
            $("#lblTYPE_SUBJECT").html(`ความชำนาญ : ${value.TYPE_SUBJECT == null ? "-" : value.TYPE_SUBJECT}`);

        });

        //PrintCommentCount(data);
        //PrintVoteAvg(data);
    });
}

function PrintGMS_HISTORY(data) {

    console.log("PrintGMS_HISTORY data", data)
    var html = "";

    $.each(data, function (index, value) {
        html += `<li>${value.COURSE_SUBJECT}</li>`;

    });

    return html;
}


function PrintCommentCount(data) {

    //console.log("PrintCommentCount");
    $.each(data, function (index, value) {
        GetCommentCount("2", value.STADIUM_ID);
    });
}

function PrintVoteAvg(data) {

    //console.log("PrintVoteAvg");
    $.each(data, function (index, value) {
        GetVoteTotalAvg("2", value.STADIUM_ID);
    });
}

function GetCommentCount(commentOf, eventOrStadiumCode) {

    //console.log("GetCommentCount");
    var settings = {
        "url": "/WebApi/Comments/GetCommentCount",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "commentOf": commentOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        //console.log("GetCommentCount data", data);

        if (jqXHR.status == 200) {
            $("#lblComment_" + eventOrStadiumCode).html(data);
        }
    });
}

function GetVoteTotalAvg(voteOf, eventOrStadiumCode) {

    //console.log("GetVoteTotalAvg");
    var settings = {
        "url": "/WebApi/Votes/GetVoteTotalAvg",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    //console.log("settings", settings)

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        //console.log("GetVoteTotalAvg reponse", data);
        //var value = JSON.parse(response);

        if (jqXHR.status == 200) {
            var value = data;

            var item = 
            `
                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
            `
            $("#dvRating_" + eventOrStadiumCode).html(item);
        }
    });
}


$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        //GetProvince(token);

        var url = window.location.href;
        console.log("url", url);
        var id = url.substring(url.lastIndexOf('/') + 1);

        GetPSN(token, id);


    });


    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, provinceId);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
    });

    $("#ddlAmphur").change(function () {
        var provinceId = $("#ddlProvince").val();
        var amphurId = $("#ddlAmphur").val();

        if (amphurId == "") {
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetTambon(token, provinceId, amphurId);
        }
    });


    //$("#btnSearchPSN").click(function () {

    //    var token = localStorage.getItem("token");
    //    SearchPSN(token, "REFRESH_DATA", "1");
    //});

    //$("#btnNext").click(function () {
    //    var nextPage = parseInt($("#now_page").val()) + 1;
    //    console.log("nextPage", nextPage);
    //    var token = localStorage.getItem("token");
    //    SearchPSN(token, "REFRESH_DATA", nextPage);
    //});

    //$("#btnPrev").click(function () {
    //    var prevPage = parseInt($("#now_page").val()) - 1;
    //    console.log("prevPage", prevPage);
    //    var token = localStorage.getItem("token");
    //    SearchPSN(token, "REFRESH_DATA", prevPage);
    //});

    //$("#btnLoadMore").click(function () {
    //    var nextPage = parseInt($("#now_page").val()) + 1;
    //    var token = localStorage.getItem("token");
    //    SearchPSN(token, "APPEND", nextPage);
    //});

});

