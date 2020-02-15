
function GetEvent() {

    var options = {};

    var input = {};
    input.limitStart = "1";
    input.limitSize = "100";
    input.eventName = $("#txtEventName").val();
    input.eventStart = $("#txtEventStart").val();
    input.eventFinish = $("#txtEventFinish").val();
    input.provinceCode = $("#ddlProvince").val();
    input.amphurCode = $("#ddlAmphur").val();
    input.tambonCode = $("#ddlTambon").val();
    input.onlyMyEvent = $("#chkOnlyMyEvent").prop("checked") == true ? $("#chkOnlyMyEvent").val() : "";

    console.log("input", input);

    options.data = JSON.stringify(input);

    console.log("options.data", options.data);

    options.url = "/webapi/Events/GetEvent";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {

            items +=
                `
            <li>
                <a href="/Events/Details/` + value.eventId + `">
                    <div class="row event">
                        <div class="col-12 col-sm-5 col-md-4">
                            <div class="event-thumb"><img src="` + value.fileUrl + `" /></div>
                        </div>
                        <div class="col-12 col-sm-7 col-md-8">
                            <div class="rating">
                                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
                            </div>
                            <div class="event-date">` + value.eventStartTH +`</div>
                            <h4>` + value.eventName + `</h4>
                            <div class="event-place">
                                ` + $("<div />").html(value.eventDescription).text().substring(0,100) + `<br />
                                ` + GetProvinceNameById(value.provinceCode) +  `
                            </div>
                            
                            <div class="row">
                                <div class="col-12">
                                    <br />
                                    <h5> ${value.isFree == "1" ? "ไม่มีค่าใช้จ่าย (ฟรี)" : ""} </h5>
                                </div>
                            </div>
                            <div class="row" id="dvEventFee_${value.eventId}">
                                <div class="col-6">
                                    <h5>ค่าสมัคร</h5>
                                    <p id="pEventFee_5001_${value.eventId}"></p>
                                </div>
                                <div class="col-6">
                                    <h5>ค่าบริการ</h5>
                                    <p id="pEventFee_5002_${value.eventId}"></p>
                                </div>
                            </div>
                            <div class="row read-comment">
                                <div class="col-sm-12 col-md-6">
                                    <div class="read-total">อ่านแล้ว ` + value.readCount + ` คน</div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="comment-total">แสดงความคิดเห็น ` + value.commentCount + ` คน</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </li >`
        });
        $("#ul-search-events-result").html(items);
        PrintEventFee(data);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function PrintEventFee(data) {
    $.each(data, function (index, value) {
        if (value.isFree == "1") {
            $("#dvEventFee_" + value.eventId).hide();
        }
        else {
            GetEventFee(value.eventId);
        }
    });
}

function GetEventFee(eventId) {

    var settings = {
        "url": "/WebApi/Events/GetEventFee",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "eventId": eventId }),
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var data = response;
            var item_5001 = "";
            var item_5002 = "";
            $.each(data, function (index, value) {
                if (value.feeId == 5001) {
                    item_5001 += `<p class="m-0">${value.eventFeeName} ${value.eventFeeAmount} ${value.eventFeeUnit}</p>`
                }
                if (value.feeId == 5002) {
                    item_5002 += `<p class="m-0">${value.eventFeeName} ${value.eventFeeAmount} ${value.eventFeeUnit}</p>`
                }
            });
            $("#pEventFee_5001_" + eventId).html(item_5001 == "" ? "-" : item_5001);
            $("#pEventFee_5002_" + eventId).html(item_5002 == "" ? "-" : item_5002);
        }
    });
}


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

            GetEvent();
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

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

$(document).ready(function () {

    var mode = getUrlVars()["mode"];
    console.log("mode", mode);
    console.log("appUserId", appUserId);
    if (mode == "CreateEvent" && appUserId != -1) {
        $("#Modal_CreateEvent").modal("show");
    }

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetProvince(token);
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
});


