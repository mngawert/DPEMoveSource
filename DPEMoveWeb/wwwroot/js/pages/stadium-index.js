
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

function SearchStadium(token, DATA_REPLACE_OR_APPEND, PAGE) {

    var provinceId = $("#ddlProvince").val();
    var amphurId = $("#ddlAmphur").val();
    var tambonId = $("#ddlTambon").val();
    var txt_STADIUM_NAME = $("#txt_STADIUM_NAME").val();
    var stadiumType = $("#ddlStadiumType").val();

    GetStadium(token, DATA_REPLACE_OR_APPEND, PAGE, txt_STADIUM_NAME, provinceId, amphurId, tambonId, stadiumType);
}

function GetStadium(token, DATA_REPLACE_OR_APPEND, PAGE, STADIUM_NAME, PROV_CODE, AMP_CODE, TAM_CODE, GROUP_ID) {

    var form = new FormData();
    form.append("PAGE", PAGE);
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

        $("#now_page").val(results.now_page);

        var all_pages = Number(results.all_pages);
        var now_page = Number(results.now_page);
        if (all_pages > 0) {
            $("#lbl_now_page").html(results.now_page);
            $("#lbl_all_pages").html(results.all_pages);
        }
        if (now_page < all_pages) {
            $("#btnLoadMore").show();
        }
        else {
            $("#btnLoadMore").hide();
        }

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

        if (DATA_REPLACE_OR_APPEND == "APPEND")
            $("#ul-search-events-result").append(items);
        else
            $("#ul-search-events-result").html(items);

        PrintCommentCount(data);
        PrintVoteAvg(data);
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


$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetStadiumType(token);
        GetProvince(token);

        //PAGE=1
        SearchStadium(token, "REFRESH_DATA", "1");
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


    $("#btnSearchStadium").click(function () {

        var token = localStorage.getItem("token");
        SearchStadium(token, "REFRESH_DATA", "1");
    });

    $("#btnNext").click(function () {
        var nextPage = parseInt($("#now_page").val()) + 1;
        console.log("nextPage", nextPage);
        var token = localStorage.getItem("token");
        SearchStadium(token, "REFRESH_DATA", nextPage);
    });

    $("#btnPrev").click(function () {
        var prevPage = parseInt($("#now_page").val()) - 1;
        console.log("prevPage", prevPage);
        var token = localStorage.getItem("token");
        SearchStadium(token, "REFRESH_DATA", prevPage);
    });

    $("#btnLoadMore").click(function () {
        var nextPage = parseInt($("#now_page").val()) + 1;
        var token = localStorage.getItem("token");
        SearchStadium(token, "APPEND", nextPage);
    });

});


