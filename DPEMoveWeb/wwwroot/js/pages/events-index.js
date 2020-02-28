
function GetSection(token, selectedValue, GetEvenCallback) {

    console.log("GetSection");

    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/activity/section/getSection",
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
            var items = `<option value="">กรุณาเลือก</option>`;
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.SECTION_CAT_ID + `">` + value.SECTION_CAT_NAME + `</option>
                `
            });
            $("#ddlSection").html(items);

            if (selectedValue != null) {
                $("#ddlSection").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetProvince(token, urlParam.get("Province"), GetEvenCallback);
        }
    });
}

function GenerateTotalItems(totalItems, forId) {
    $("#" + forId).html(totalItems);
}

function GeneratePaginationHtml(pageNumber, totalPages, forId) {

    var url = window.location.pathname;
    var urlParam = new URLSearchParams(window.location.search);
    urlParam.delete("PageNumber");

    for (var pair of urlParam.entries()) {
        url += `&${pair[0]}=${pair[1]}`;
    }


    var pageNumber = parseInt(pageNumber == null ? 1 : pageNumber);

    var start = 1;
    var end = totalPages;
    if ((pageNumber + 5) < end) {
        end = pageNumber + 5;
    }
    start = end - 9;
    if (start < 1) {
        start = 1;
        end = Math.min(10, totalPages);
    }

    console.log("pageNumber:" + pageNumber + " totalPages:" + totalPages);
    console.log("start:" + start + " end:" + end);

    var items_1 = "";
    for (var i = start; i <= end; i++) {
        var urlWithPageNumber = (url + "&PageNumber=" + i).replace("&", "?");
        items_1 += `<li class="page-item ${i == pageNumber ? "active" : ""}"><a class="page-link" href="${urlWithPageNumber}">${i}</a></li>`;
    }

    $("#" + forId).append(items_1);
}


var GetEvenCallback = function GetEvent() {

    console.log("GetEvent");
    var urlParam = new URLSearchParams(window.location.search);
    var pageNumber = urlParam.get("PageNumber");

    var options = {};

    var input = {};
    input.limitStart = pageNumber;
    input.limitSize = "10";
    input.eventName = $("#txtEventName").val();
    input.eventStart = $("#txtEventStart").val();
    input.eventFinish = $("#txtEventFinish").val();
    input.provinceCode = $("#ddlProvince").val();
    input.amphurCode = $("#ddlAmphur").val();
    input.tambonCode = $("#ddlTambon").val();
    input.onlyMyEvent = $("#chkOnlyMyEvent").prop("checked") == true ? $("#chkOnlyMyEvent").val() : "";
    input.sectionCatId = $("#ddlSection").val();

    console.log("input", input);

    options.data = JSON.stringify(input);

    console.log("options.data", options.data);

    options.url = "/webapi/Events/GetEvent";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (response) {
        var data = response.data;

        var items = '';
        $.each(data, function (index, value) {

            var thumb = $("<img />");
            thumb.attr("src", value.fileUrl);

            //console.log("test thumb", thumb.html());
            //console.log("value.fileUrl", value.fileUrl);

            items +=
                `
            <li>
                <a href="/Events/Details/` + value.eventId + `">
                    <div class="row event">
                        <div class="col-12 col-sm-5 col-md-4">
                            <div id="dvThumb_${value.eventId}" class="event-thumb"></div>
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
        PrintThumb(data);

        GenerateTotalItems(response.totalItems, "lblTotalItems");
        GeneratePaginationHtml(pageNumber, response.totalPages, "ulPagination");
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

function PrintThumb(data) {
    $.each(data, function (index, value) {
        if (value.fileUrl != null) {
            $("#dvThumb_" + value.eventId).append(`<img src=${encodeURI(value.fileUrl)} />`);
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

function GetProvince(Token, selectedValue, GetEvenCallback) {

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

            if (selectedValue != null) {
                $("#ddlProvince").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetAmphur(Token, $("#ddlProvince").val(), urlParam.get("Amphur"), GetEvenCallback);
        }
    });
}

function GetAmphur(token, PROV_CODE, selectedValue, GetEvenCallback) {

    console.log("GetAmphur");

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

            if (selectedValue != null) {
                $("#ddlAmphur").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetTambon(token, $("#ddlProvince").val(), $("#ddlAmphur").val(), urlParam.get("Tambon"), GetEvenCallback);
        }
    });
}

function GetTambon(token, PROV_CODE, AMP_CODE, selectedValue, GetEvenCallback) {

    console.log("GetTambon");

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

            if (selectedValue != null) {
                $("#ddlTambon").val(selectedValue);
            }

            if (GetEvenCallback != null) {
                GetEvenCallback();
            }
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

        var urlParam = new URLSearchParams(window.location.search);

        if (urlParam.get("EventName") != null) {
            $("#txtEventName").val(urlParam.get("EventName"));
        }
        if (urlParam.get("EventStart") != null) {
            $("#txtEventStart").val(urlParam.get("EventStart"));
        }
        if (urlParam.get("EventFinish") != null) {
            $("#txtEventFinish").val(urlParam.get("EventFinish"));
        }
        if (urlParam.get("OnlyMyEvent") != null) {
            $("#chkOnlyMyEvent").val(urlParam.get("OnlyMyEvent"));
            $("#chkOnlyMyEvent").prop("checked", true);
        }

        GetSection(token, urlParam.get("SectionCatId"), GetEvenCallback);
    });

    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, provinceId, null);
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
            GetTambon(token, provinceId, amphurId, null);
        }
    });
});


