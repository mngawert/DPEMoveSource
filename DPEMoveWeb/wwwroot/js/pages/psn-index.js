
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

    //console.log("call GetProvince");
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

function getPrefix(token, selectedValue) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/prefix/getPrefix",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, status, xhr) {
        if (xhr.status == 200) {
            var data = JSON.parse(response).data

            var item_1 = `<option value="">กรุณาเลือก</option>`
            var item_2 = `<option value="">กรุณาเลือก</option>`
            $.each(data, function (index, value) {
                item_1 += `<option value="` + value.PREFIX_ID + `">` + value.PREFIX_TH + `</option>`
                item_2 += `<option value="` + value.PREFIX_ID + `">` + value.PREFIX_EN + `</option>`
            });
            $("#ddlPREFIX_TH").html(item_1);
            $("#ddlPREFIX_EN").html(item_2);

            if (selectedValue != null) {
                $("#ddlPREFIX_TH").val(selectedValue);
                $("#ddlPREFIX_EN").val(selectedValue);
            }

        }
    });
}

function CreateGmsMember(token) {

    var form = new FormData();
    form.append("Token", token);
    form.append("FIRST_NAME", $("[name='FIRST_NAME']").val());
    form.append("LAST_NAME", $("[name='LAST_NAME']").val());
    form.append("SEX", $("[name='SEX']").val());
    form.append("BIRTH_DATE", "1957-01-01");
    form.append("FIRST_NAME_ENG", $("[name='FIRST_NAME_ENG']").val());
    form.append("LAST_NAME_ENG", $("[name='LAST_NAME_ENG']").val());
    form.append("PREFIX_ID", $("[name='PREFIX_ID']").val());
    form.append("MEMBER_USERNAME", $("[name='HRS_ID']").val());
    form.append("MEMBER_PASSWORD", $("[name='HRS_ID']").val());
    form.append("CLASS_ID", "1");
    form.append("HRS_ID", $("[name='HRS_ID']").val());

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/member/pushGmsMember",
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
            console.log("results", results);
            var data = results.data;

            if (data.length > 0) {
                window.location.href = window.location.href + "/Edit/" + data[0].MEMBER_ID;
            }
        }
    });

}


function SearchPSN(token, DATA_REPLACE_OR_APPEND, PAGE) {

    var txtName = $("#txtName").val();
    var provinceId = $("#ddlProvince").val();
    var amphurId = $("#ddlAmphur").val();
    var tambonId = $("#ddlTambon").val();

    GetPSN(token, DATA_REPLACE_OR_APPEND, PAGE, txtName, provinceId, amphurId);
}

function GetPSN(token, DATA_REPLACE_OR_APPEND, PAGE, NAME, PROV_CODE, AMP_CODE) {

    var form = new FormData();
    form.append("PAGE", PAGE);
    form.append("Token", token);
    form.append("NAME", NAME);
    form.append("PROV_CODE", PROV_CODE);
    form.append("AMP_CODE", AMP_CODE);


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
            <div class="row">
                <div class="col-12 col-sm-3">
                    <div class="thumb-img"><img src="${value.MEMBER_IMAGE == null ? "images/psn010101_02.png" : value.MEMBER_IMAGE }" /></div>
                </div>
                <div class="col-12 col-sm-9">
                    <h4>${value.FIRST_NAME} ${value.LAST_NAME}</h4>
                    <div class="training-history">ประวัติการฝึกอบรม</div>
                    <div class="clearfix">
                        <div class="col-traininghistory" id="dvHistory_${value.MEMBER_ID}">
                            <ol>
                            </ol>
                        </div>
                        <div class="col-th-btn">
                            <a href="/PSN/Edit/${value.MEMBER_ID}" class="button darkgreen medium">แก้ไข</a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        `
        });

        if (DATA_REPLACE_OR_APPEND == "APPEND")
            $("#ul-search-person-result").append(items);
        else
            $("#ul-search-person-result").html(items);

        PrintHistory(token, data);
    });
}

function PrintHistory(token, data) {

    //console.log("PrintHistory data", data)

    $.each(data, function (index, value) {
        GetHistory(token, value.MEMBER_ID);
    });
}

function GetHistory(token, MEMBER_ID) {
    //console.log("GetHistory ", MEMBER_ID);

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryTrain/getHistory",
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
            var items = ``
            $.each(data, function (index, value) {
                items +=
                `
                <li> ${ value.COURSE_SUBJECT }</li >
                `
            });
            $("#dvHistory_" + MEMBER_ID + " > ol").html(items);
        }
    });
}

$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        //console.log("localStorage.token", localStorage.getItem("token"));

        GetProvince(token);

        getPrefix(token, null);

        //PAGE=1
        SearchPSN(token, "REFRESH_DATA", "1");
    });

    $("#btnCreate").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            CreateGmsMember(token);
        });
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


    $("#btnSearchPSN").click(function () {

        var token = localStorage.getItem("token");
        SearchPSN(token, "REFRESH_DATA", "1");
    });

    $("#btnNext").click(function () {
        var nextPage = parseInt($("#now_page").val()) + 1;
        console.log("nextPage", nextPage);
        var token = localStorage.getItem("token");
        SearchPSN(token, "REFRESH_DATA", nextPage);
    });

    $("#btnPrev").click(function () {
        var prevPage = parseInt($("#now_page").val()) - 1;
        console.log("prevPage", prevPage);
        var token = localStorage.getItem("token");
        SearchPSN(token, "REFRESH_DATA", prevPage);
    });

    $("#btnLoadMore").click(function () {
        var nextPage = parseInt($("#now_page").val()) + 1;
        var token = localStorage.getItem("token");
        SearchPSN(token, "APPEND", nextPage);
    });

});


