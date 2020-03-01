
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

function GetProvince(Token, selectedValue, SearchStadiumCallback) {

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
            GetAmphur(Token, $("#ddlProvince").val(), urlParam.get("Amphur"), SearchStadiumCallback);
        }
    });
}

function GetAmphur(token, PROV_CODE, selectedValue, SearchStadiumCallback) {


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
            GetTambon(token, $("#ddlProvince").val(), $("#ddlAmphur").val(), urlParam.get("Tambon"), SearchStadiumCallback);

        }
    });
}

function GetTambon(token, PROV_CODE, AMP_CODE, selectedValue, SearchStadiumCallback) {

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

            if (SearchStadiumCallback != null) {
                SearchStadiumCallback(token);
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

function GetStadiumType(token, selectedValue, SearchStadiumCallback) {

    var form = new FormData();
    form.append("Token", token);

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

        if (selectedValue != null) {
            $("#ddlStadiumType").val(selectedValue);
        }

        var urlParam = new URLSearchParams(window.location.search);
        GetProvince(token, urlParam.get("Province"), SearchStadiumCallback);
    });
}

var SearchStadiumCallback = function (token) {

    var urlParam = new URLSearchParams(window.location.search);
    var pageNumber = urlParam.get("PageNumber") == null ? "1" : urlParam.get("PageNumber");

    var provinceId = $("#ddlProvince").val();
    var amphurId = $("#ddlAmphur").val();
    var tambonId = $("#ddlTambon").val();
    var txt_STADIUM_NAME = $("#txt_STADIUM_NAME").val();
    var stadiumType = $("#ddlStadiumType").val();

    GetStadium(token, pageNumber, txt_STADIUM_NAME, provinceId, amphurId, tambonId, stadiumType);
}

function GetStadium(token, pageNumber, STADIUM_NAME, PROV_CODE, AMP_CODE, TAM_CODE, GROUP_ID) {

    var form = new FormData();
    form.append("PAGE", pageNumber);
    form.append("limit", "10");
    form.append("PROV_CODE", PROV_CODE);
    form.append("AMP_CODE", AMP_CODE);
    form.append("TAM_CODE", TAM_CODE);
    form.append("GROUP_ID", GROUP_ID);
    form.append("STADIUM_NAME", STADIUM_NAME);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/address/getStadium",
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

        //$("#now_page").val(results.now_page);

        //var all_pages = Number(results.all_pages);
        //var now_page = Number(results.now_page);
        //if (all_pages > 0) {
        //    $("#lbl_now_page").html(results.now_page);
        //    $("#lbl_all_pages").html(results.all_pages);
        //}
        //if (now_page < all_pages) {
        //    $("#btnLoadMore").show();
        //}
        //else {
        //    $("#btnLoadMore").hide();
        //}

        var data = results.data;
        var items = '';
        $.each(data, function (index, value) {

            items +=
                `
        <li>
            <a href="/Stadium/Details/` + value.STADIUM_ID + `">
                <div class="row event">
                    <div class="col-12 col-sm-5 col-md-4">
                        <div class="event-thumb"><img src="` + value.COVER_IMG + `" /></div>
                    </div>
                    <div class="col-12 col-sm-7 col-md-8">
                        <div id="dvRating_` + value.STADIUM_ID + `" class="rating">
                            <span class="fa fa-star"></span>
                            <span class="fa fa-star"></span>
                            <span class="fa fa-star"></span>
                            <span class="fa fa-star"></span>
                            <span class="fa fa-star"></span>
                        </div>
                        <div class="event-date"></div>
                        <h4>` + value.NAME_LABEL + `</h4>
                        <div class="event-place">
                            สถานที่ : ` + value.ADDRESS + `<br />
                            ` + value.PROV_NAMT + `
                        </div>
                        <div class="row read-comment">
                            <div class="col-sm-12 col-md-6">
                                <div class="read-total"></div>
                            </div>
                            <div class="col-sm-12 col-md-6">
                                <div class="comment-total">แสดงความคิดเห็น ` + `<span id="lblComment_` + value.STADIUM_ID + `">-</span>` + ` คน</div>
                            </div>
                        </div>
                    </div>
                </div>
            </a>
        </li>
        `
        });
        $("#ul-search-events-result").html(items);

        PrintCommentCount(data);
        PrintVoteAvg(data);

        GenerateTotalItems(Number(results.all_rows), "lblTotalItems");
        GeneratePaginationHtml(pageNumber, results.all_rows == 0 ? 0 : results.all_pages, "ulPagination");
    });
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

function GenerateTotalItems(totalItems, forId) {
    if (totalItems > 0) {
        $("#" + forId).html("ผลการค้นหา " + totalItems.toLocaleString() + " รายการ");
    }
    else {
        $("#" + forId).html("ค้นหาข้อมูลไม่พบ");
    }
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


$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        var urlParam = new URLSearchParams(window.location.search);
        if (urlParam.get("STADIUM_NAME") != null) {
            $("#txt_STADIUM_NAME").val(urlParam.get("STADIUM_NAME"));
        }

        GetStadiumType(token, urlParam.get("StadiumType"), SearchStadiumCallback);
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


