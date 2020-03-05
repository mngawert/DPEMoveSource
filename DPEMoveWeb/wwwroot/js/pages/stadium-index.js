
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

    var SEARCH_BUILDING_BY = $("[name='SEARCH_BUILDING_BY']").val();
    var SEARCH_SUPPORT_FLAG = $("[name='SEARCH_SUPPORT_FLAG']").val();
    var SEARCH_SUPPORT = $("[name='SEARCH_SUPPORT']").val();

    GetStadium(token, pageNumber, txt_STADIUM_NAME, provinceId, amphurId, tambonId, stadiumType, SEARCH_BUILDING_BY, SEARCH_SUPPORT_FLAG, SEARCH_SUPPORT);
}

function GetStadium(token, pageNumber, STADIUM_NAME, PROV_CODE, AMP_CODE, TAM_CODE, GROUP_ID, SEARCH_BUILDING_BY, SEARCH_SUPPORT_FLAG, SEARCH_SUPPORT) {

    var form = new FormData();
    form.append("PAGE", pageNumber);
    form.append("limit", "10");
    form.append("IS_SUB", "0");
    form.append("PROV_CODE", PROV_CODE);
    form.append("AMP_CODE", AMP_CODE);
    form.append("TAM_CODE", TAM_CODE);
    form.append("GROUP_ID", GROUP_ID);
    form.append("STADIUM_NAME", STADIUM_NAME);
    form.append("BUILDING_BY", SEARCH_BUILDING_BY);
    form.append("SUPPORT_FLAG", SEARCH_SUPPORT_FLAG);
    form.append("SUPPORT", SEARCH_SUPPORT);
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
                    <div class="row event">
                        <div class="col-12 col-md-4">
                            <div class="event-thumb"><a href="/Stadium/Details/${value.STADIUM_ID}"><img src="${value.COVER_IMG}" /></a></div>
                        </div>
                        <div class="col-12 col-md-8">
                            <div class="row">
                                <div class="col-12">                            
                                    <div id="dvRating_${value.STADIUM_ID}" class="rating">
                                        <span class="fa fa-star"></span>
                                        <span class="fa fa-star"></span>
                                        <span class="fa fa-star"></span>
                                        <span class="fa fa-star"></span>
                                        <span class="fa fa-star"></span>
                                    </div>
                                    <h4><a href="/Stadium/Details/${value.STADIUM_ID}"> ${value.NAME_LABEL} </a></h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8"> 
                                    <div class="std-info">
                                        <img src="/images/icon_detail_01.png" width="27" height="38"> ${value.ADDRESS == null ? "" : value.ADDRESS} ${value.TAM_NAMT == null ? "" : value.TAM_NAMT} ${value.AMP_NAMT == null ? "" : value.AMP_NAMT} ${value.PROV_NAMT == null ? "" : value.PROV_NAMT} ${value.POST_NO == null ? "" : value.POST_NO}
                                        <br />
                                        <img src="/images/icon_phone.png" width="27" height="38"> ${value.TELEPHONE == null ? " - " : value.TELEPHONE}
                                        <br />
                                        <img src="/images/icon_area.png" width="27" height="38"> ${value.DIMENSION == null ? " - " : value.DIMENSION}
                                        <br />
                                        <img src="/images/icon_time.png" width="27" height="38"> ${value.TIME_ == null ? " - " : value.TIME_}
                                        <br />
                                        <img src="/images/icon_address.png" width="27" height="38"> <a href='https://www.google.com/maps/search/?api=1&query=${value.LATITUDE},${value.LONGITUDE}' target="_blank">ดูแผนที่</a>
                                    </div>
                                </div>
                                <div class="col-4"> 
                                    <div>
                                        ${ PrintSubStadium(value.UNDER_STADIUM_NEW)}
                                        <div> ${ value.UPDATE_DATE == null ? "" : "วันที่สำรวจล่าสุด " + ConvertDateToTH(value.UPDATE_DATE) } </div >
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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

function PrintSubStadium(data) {
    if (data.length > 0) {
        var items = "";
        items += "<div style='padding-bottom: 10px'>"
        items += "<div>ประเภทสนามกีฬา</div>";
        $.each(data, function (index, value) {
            items += `
                    <div>-<a href="/Stadium/Details/${value.STADIUM_ID}"> ${value.NAME_LABEL} </a></div>
            `;
        });
        items += "</div>"
        return items;
    }
    return "";
}

function ConvertDateToTH(input) {
    //'1986-02-12' to '12/02/2529'
    if (input != null) {
        return input.substr(8, 2) + "/" + input.substr(5, 2) + "/" + (parseInt(input.substr(0, 4)) + 543);
    }
    else {
        return null;
    }
}

function ConvertDateToEN(input) {
    //'12/02/2529' to '1986-02-12'
    if (input != null) {
        return (parseInt(input.substr(6, 4)) - 543) + "-" + input.substr(3, 2) + "-" + input.substr(0, 2);
    }
    else {
        return null;
    }
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
        $("#" + forId).html("ไม่พบข้อมูล");
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
        if (urlParam.get("SEARCH_BUILDING_BY") != null) {
            $("[name='SEARCH_BUILDING_BY']").val(urlParam.get("SEARCH_BUILDING_BY"));
        }
        if (urlParam.get("SEARCH_SUPPORT_FLAG") != null) {
            $("[name='SEARCH_SUPPORT_FLAG']").val(urlParam.get("SEARCH_SUPPORT_FLAG"));
        }
        if (urlParam.get("SEARCH_SUPPORT") != null) {
            $("[name='SEARCH_SUPPORT']").val(urlParam.get("SEARCH_SUPPORT"));
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


