
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

function GetProvince(Token, selectedValue, SearchPSNCallback) {

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

            if (selectedValue != null) {
                $("#ddlProvince").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetAmphur(Token, $("#ddlProvince").val(), urlParam.get("Amphur"), SearchPSNCallback);
        }
    });
}

function GetAmphur(token, PROV_CODE, selectedValue, SearchPSNCallback) {

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
            GetTambon(token, $("#ddlProvince").val(), $("#ddlAmphur").val(), urlParam.get("Tambon"), SearchPSNCallback);
        }
    });
}

function GetTambon(token, PROV_CODE, AMP_CODE, selectedValue, SearchPSNCallback) {

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

            if (SearchPSNCallback != null) {
                SearchPSNCallback(token);
            }
        }
    });
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

function GetGmsSport(token, selectedValue, SearchPSNCallback) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/sport/getGmsSport",
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
            $.each(data, function (index, value) {
                item_1 += `<option value="` + value.SPORT_ID + `">` + value.SPORT_SUBJECT + `</option>`
            });
            $("#ddlSPORT_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlSPORT_ID").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetGmsType(token, urlParam.get("TYPE_ID_SEARCH"), SearchPSNCallback);
        }
    });
}

function GetGmsType(token, selectedValue, SearchPSNCallback) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/type/getGmsType",
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
            $.each(data, function (index, value) {
                item_1 += `<option value="` + value.TYPE_ID + `">` + value.TYPE_SUBJECT + `</option>`
            });
            $("#ddlTYPE_ID").html(item_1);
            $("#ddlTYPE_ID_SEARCH").html(item_1);

            if (selectedValue != null) {
                $("#ddlTYPE_ID").val(selectedValue);
                $("#ddlTYPE_ID_SEARCH").val(selectedValue);
            }

            var urlParam = new URLSearchParams(window.location.search);
            GetProvince(token, urlParam.get("Province"), SearchPSNCallback);
        }
    });
}

function CreateGmsMember(token) {

    if (!$("#frmCreateGmsMember")[0].checkValidity()) {

        $("#frmCreateGmsMember")[0].reportValidity()
        return false;
    }

    var form = new FormData();
    form.append("Token", token);
    form.append("FIRST_NAME", $("[name='FIRST_NAME']").val());
    form.append("LAST_NAME", $("[name='LAST_NAME']").val());
    form.append("SEX", $("[name='SEX']").val());
    form.append("BIRTH_DATE", ConvertDateToEN($("[name='BIRTH_DATE']").val()));
    form.append("FIRST_NAME_ENG", $("[name='FIRST_NAME_ENG']").val());
    form.append("LAST_NAME_ENG", $("[name='LAST_NAME_ENG']").val());
    form.append("PREFIX_ID", $("[name='PREFIX_ID']").val());
    form.append("MEMBER_USERNAME", $("[name='HRS_ID']").val());
    form.append("MEMBER_PASSWORD", $("[name='HRS_ID']").val());
    form.append("CLASS_ID", "1");
    form.append("HRS_ID", $("[name='HRS_ID']").val());
    form.append("TYPE_ID", $("[name='TYPE_ID']").val());

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

            if (!results.status) {
                alert(results.message);
            }

            var data = results.data;

            if (data.length > 0) {
                window.location.href = window.location.href.replace("#","") + "/Details/" + data[0].MEMBER_ID;
            }
        }
    });

}

var SearchPSNCallback = function (token) {

    var urlParam = new URLSearchParams(window.location.search);
    var pageNumber = urlParam.get("PageNumber") == null ? "1" : urlParam.get("PageNumber");

    var txtName = $("#txtName").val();
    var provinceId = $("#ddlProvince").val();
    var amphurId = $("#ddlAmphur").val();
    var tambonId = $("#ddlTambon").val();

    GetPSN(token, pageNumber, txtName, provinceId, amphurId);
}

function GetPSN(token, pageNumber, NAME, PROV_CODE, AMP_CODE) {

    var form = new FormData();
    form.append("PAGE", pageNumber);
    form.append("Token", token);
    form.append("NAME", NAME);
    form.append("PROV_CODE", PROV_CODE);
    form.append("AMP_CODE", AMP_CODE);
    form.append("SPORT_ID", $("[name='SPORT_ID']").val());
    form.append("TYPE_ID", $("[name='TYPE_ID_SEARCH']").val());
    form.append("HRS_ID", $("[name='SEARCH_HRS_ID']").val());

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
                            <a href="/PSN/Details/${value.MEMBER_ID}" class="button darkgreen medium">รายละเอียด</a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        `
        });

        $("#ul-search-person-result").html(items);

        PrintHistory(token, data);

        GenerateTotalItems(Number(results.all_rows), "lblTotalItems");
        GeneratePaginationHtml(pageNumber, results.all_rows > 0 ? results.all_pages : 0, "ulPagination");
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

function GetGMSMemberByProfileIDCard(token, HRS_ID) {

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

    $.ajax(settings).done(function (response, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var results = JSON.parse(response);
            var data = results.data;
            $.each(data, function (index, value) {

                console.log("data", data);
                appUserHRS_ID = value.HRS_ID;
                appUserMEMBER_ID = value.MEMBER_ID;

                $("#btnGoToIPESHD").show();
                $("#btnOpenPopupForCreatePSN").text("แก้ไขข้อมูล");
            });
        }
    });
}


function DisplayPopupForCreatePSN() {

    if (appIdcardNo.length > 0) {

        if (appUserMEMBER_ID.length > 0) {
            window.location.href = window.location.href.replace("#", "") + "/Details/" + appUserMEMBER_ID;
        }
        else {
            $("#Modal_CreatePSN").modal("show");
        }
    }
    else {
        $("#Modal_NoProfileIDCard").modal("show");
        console.log("error there is no idcard");
    }
}

function GoToIPESHD() {

    if (appIdcardNo.length > 0) {
        const param1 = window.btoa(appIdcardNo);
        console.log("param1", param1);
        const param2 = window.btoa(appUserMEMBER_ID + "&" + appUserHRS_ID);
        console.log("param2", param2);

        var url = `https://ipeshd.dpe.go.th?n=${param1}&o=${param2}`;
        console.log("url: ", url);
        window.open(url, '_blank');
    }
    else {
        $("#Modal_NoProfileIDCard").modal("show");
        console.log("error there is no idcard");
    }
}


var appUserHRS_ID = "";
var appUserMEMBER_ID = "";

$(document).ready(function () {

    var mode = getUrlVars()["mode"];
    console.log("mode", mode);
    console.log("appUserId", appUserId);

    if (mode == "CreatePSN" && appUserId != -1) {
        DisplayPopupForCreatePSN();
    }

    if (appUserId != -1) {
        $("#btnOpenPopupForCreatePSN").show();
        //$("#btnGoToIPESHD").show();
    }

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);

        getPrefix(token, null);

        if (appIdcardNo.length > 0) {
            GetGMSMemberByProfileIDCard(token, appIdcardNo);
        }

        var urlParam = new URLSearchParams(window.location.search);
        if (urlParam.get("SEARCH_NAME") != null) {
            $("[name='SEARCH_NAME']").val(urlParam.get("SEARCH_NAME"));
        }
        if (urlParam.get("SEARCH_HRS_ID") != null) {
            $("[name='SEARCH_HRS_ID']").val(urlParam.get("SEARCH_HRS_ID"));
        }
        
        GetGmsSport(token, urlParam.get("SPORT_ID"), SearchPSNCallback);
    });

    $("#btnCreate").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            CreateGmsMember(token);
        });
    });

    $("#btnOpenPopupForCreatePSN").click(function () {
        DisplayPopupForCreatePSN();
    });

    $("#btnGoToIPESHD").click(function () {
        GoToIPESHD();
    });
    
    $("#chkAgree").click(function () {
        if (this.checked) {
            $('#btnCreate').removeAttr('disabled');
        }
        else {
            $('#btnCreate').attr('disabled', 'disabled');
        }
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


