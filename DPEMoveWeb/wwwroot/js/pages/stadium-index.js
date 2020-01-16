
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
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetProvinceNameById(provinceId) {

    if (provinceId == null)
        return "";

    console.log("call GetProvinceNameById", provinceId);
    var provinceName = "n/a";
    $.each(PROVINCE_DATA, function (index, value) {
        if (value.pcode == provinceId) {
            provinceName = value.province;
        }
    });
    return provinceName;
}

function GetProvince() {

    console.log("call GetProvince");

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=province";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        PROVINCE_DATA = data.DATA;
        console.log('PROVINCE_DATA', PROVINCE_DATA);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.province + `</option>
                `
        });
        $("#ddlProvince").html(items);
        GetEvent();
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetAmphur(provinceId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=amphur&province=" + provinceId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.amphur + `</option>
                `
        });
        $("#ddlAmphur").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetTambon(provinceId, amphurId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=tambon&province=" + provinceId + "&amphur=" + amphurId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.tambon + `</option>
                `
        });
        $("#ddlTambon").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}



function GetStadium() {

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

    console.log("input", input);

    options.data = JSON.stringify(input);

    console.log("options.data", options.data);

    options.url = "http://data.dpe.go.th/api/stadium/address/getStadium";
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
                            <div class="event-date">` + value.eventStartTH + `</div>
                            <h4>` + value.eventName + `</h4>
                            <div class="event-place">
                                ` + $("<div />").html(value.eventDescription).text().substring(0, 100) + `<br />
                                ` + GetProvinceNameById(value.provinceCode) + `
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
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}



$(document).ready(function () {



    var PROVINCE_DATA = {};

    GetProvince();

    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetAmphur(provinceId);
        }
    });

    $("#ddlAmphur").change(function () {
        var provinceId = $("#ddlProvince").val();
        var amphurId = $("#ddlAmphur").val().substr(2, 2);

        if (amphurId == "") {
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetTambon(provinceId, amphurId);
        }
    });

    GetStadium();
});


