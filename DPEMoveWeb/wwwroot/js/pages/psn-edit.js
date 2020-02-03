
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

function getEducation(token, selectedValue) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/education/getEducation",
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
            var items =
                `
            <option value="">กรุณาเลือก</option>
            `
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.EDU_ID + `">` + value.EDU_NAME + `</option>
                `
            });
            $("#ddlEDU_ID").html(items);
            $("#ddlEDU_ID").val(selectedValue);
        }
    });
}

function getPrefix(token) {
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
        }
    });
}

function getGmsMember(token, MEMBER_ID) {

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);

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
            $("#txtHRS_ID").val(value.HRS_ID);
            $("#txtFIRST_NAME").val(value.FIRST_NAME);
            $("#txtLAST_NAME").val(value.LAST_NAME);
            $("#txtFIRST_NAME_EN").val(value.FIRST_NAME_EN);
            $("#txtLAST_NAME_EN").val(value.LAST_NAME_EN);

            PrintGMS_HISTORY(value.GMS_HISTORY);
            getEducationHistory(token, value.MEMBER_ID);
            getWorkHistory(token, value.MEMBER_ID);

            // Load Dropdownlist and set selected value.
            getEducation(token);
            getPrefix(token);
        });

    });
}

function PrintGMS_HISTORY(data) {

    console.log("PrintGMS_HISTORY data", data)
    var items = "";

    $.each(data, function (index, value) {
        items += `
                <tr>
                    <td>${index+1}</td>
                    <td></td>
                    <td>${value.COURSE_SUBJECT}</td>
                    <td>${value.SPORT_SUBJECT}</td>
                    <td>${value.LEVEL_DETAIL}</td>
                    <td class="center"><a href="#" class="button small red">&nbsp;ลบ&nbsp;</a> <a href="#" class="button small darkgreen">แก้ไข</a></td>
                </tr>
        `;
    });

    $("#tblGMS_HISTORY >tbody").html(items);
}

function getEducationHistory(token, MEMBER_ID) {
    console.log("getEducationHistory");

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryEdu/getHistory",
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
            var items = "";
            $.each(data, function (index, value) {
                items += `
                    <tr>
                        <td>1</td>
                        <td>${value.EDU_YR}</td>
                        <td>${value.EDU_LEVEL}</td>
                        <td>${value.EDU_DEPT}</td>
                        <td>${value.EDU_INSTITUTE}</td>
                        <td class="center"><a href="#" class="button small red">&nbsp;ลบ&nbsp;</a> <a href="#" class="button small darkgreen">แก้ไข</a></td>
                    </tr>
                `
            });
            $("#tblEduHistory >tbody").html(items);
        }
    });
}

function getWorkHistory(token, MEMBER_ID) {
    console.log("getEducationHistory");

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryWork/getHistory",
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
            var items = "";
            $.each(data, function (index, value) {
                items += `
                    <tr>
                        <td>1</td>
                        <td>${value.WORK_SUBJECT}</td>
                        <td>${value.SPORT_SUBJECT}</td>
                        <td>${value.LEVEL_DETAIL}</td>
                        <td>${value.WORK_LOCATION}</td>
                        <td>${value.WORK_TIME_START.replace("00:00:00", "")} - ${value.WORK_TIME_END.replace("00:00:00", "")}</td>
                        <td class="center"><a href="#" class="button small red">&nbsp;ลบ&nbsp;</a> <a href="#" class="button small darkgreen">แก้ไข</a></td>
                    </tr>
                `
            });
            $("#tblWorkHistory >tbody").html(items);
        }
    });
}



$(document).ready(function () {

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        getEducation(token);
        getPrefix(token);
    });

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        var url = window.location.href;
        var id = url.substring(url.lastIndexOf('/') + 1);

        getGmsMember(token, id);


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


