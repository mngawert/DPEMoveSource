
function GetProvince(token, forId, selectedValue) {

    console.log("call GetProvince");
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/location/getProvince",
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
                <option value="` + value.PROV_ID + `">` + value.PROV_NAME + `</option>
                `
            });
            $("#" + forId).html(items);
            if (selectedValue != null) {
                $("#" + forId).val(selectedValue);
            }
        }
    });
}

function GetAmphur(token, forId, PROV_ID, selectedValue) {


    var form = new FormData();
    form.append("Token", token);
    form.append("PROV_ID", PROV_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/location/getAmpher",
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
                <option value="` + value.AMPHUR_ID + `">` + value.AMPHUR_NAME + `</option>
                `
            });
            $("#" + forId).html(items);
            if (selectedValue != null) {
                $("#" + forId).val(selectedValue);
            }
        }
    });
}

function GetTambon(token, forId, PROV_ID, AMPHUR_ID, selectedValue) {

    var form = new FormData();
    form.append("Token", token);
    form.append("PROV_ID", PROV_ID);
    form.append("AMPHUR_ID", AMPHUR_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/location/getTambol",
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
                <option value="` + value.TUMBOL_ID + `">` + value.TUMBOL_NAME + `</option>
                `
            });
            $("#" + forId).html(items);
            if (selectedValue != null) {
                $("#" + forId).val(selectedValue);
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

function GetGmsType(token, selectedValue) {
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

            if (selectedValue != null) {
                $("#ddlTYPE_ID").val(selectedValue);
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


function EditGmsMember(token, MEMBER_ID) {

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);
    form.append("FIRST_NAME", $("[name='FIRST_NAME']").val());
    form.append("LAST_NAME", $("[name='LAST_NAME']").val());
    form.append("SEX", $("[name='SEX']").val());
    //form.append("WEIGHT", "63");
    //form.append("TALL", "165");
    form.append("BIRTH_DATE", ConvertDateToEN($("[name='BIRTH_DATE']").val()));
    //form.append("HOME_ADDR", "231");
    //form.append("HOME_ADDR_NAME", "หมู่บ้านพฤกษา");
    //form.append("HOME_MOO", "1");
    //form.append("HOME_SOI", "ซ.สันติสุข 2");
    //form.append("HOME_ROAD", "ถ.ประชาอุทิศ");
    //form.append("HOME_TUMBOL", "ในเมือง");
    //form.append("HOME_PROV", "10");
    //form.append("HOME_AMPHUR", "50");
    //form.append("HOME_POST", "10310");
    //form.append("HOME_TEL", "0222222222");
    //form.append("HOME_FAX", "01211231");
    //form.append("E_MAIL", "abc@gmail.com");
    form.append("CON_ADDR", $("[name='CON_ADDR']").val());
    form.append("CON_ADDR_NAME", $("[name='CON_ADDR_NAME']").val());
    form.append("CON_MOO", $("[name='CON_MOO']").val());
    form.append("CON_SOI", $("[name='CON_SOI']").val());
    form.append("CON_ROAD", $("[name='CON_ROAD']").val());
    //form.append("CON_TUMBOL", "วังทองหลาง");
    //form.append("CON_AMPHUR", "50");
    //form.append("CON_PROV", "10");
    form.append("CON_POST", $("[name='CON_POST']").val());
    form.append("CON_TEL", $("[name='CON_TEL']").val());
    //form.append("CON_FAX", $("[name='CON_FAX']").val());
    //form.append("CON_EMAIL", "asdff@gmail.com");
    //form.append("HRS_HIS", "ภูมิลำเนาเดิม อยู่ที่ จ.สมุทรสงครามนะ");
    form.append("FIRST_NAME_ENG", $("[name='FIRST_NAME_ENG']").val());
    form.append("LAST_NAME_ENG", $("[name='LAST_NAME_ENG']").val());
    form.append("EDU_ID", $("[name='EDU_ID']").val());
    form.append("RELIGION", $("[name='RELIGION']").val());
    //form.append("JOB_SUBJECT", "นักกีฬานะ");
    //form.append("JOB_POSITION", "นักฟุตบอลทีมชาตินะ");
    //form.append("WORK_PLACE", "โรงเรียนอนุบาลศรีธาตุนะ");
    form.append("PREFIX_ID", $("[name='PREFIX_ID']").val());
    form.append("MEMBER_USERNAME", $("[name='HRS_ID']").val());
    form.append("MEMBER_PASSWORD", $("[name='HRS_ID']").val());
    //form.append("CLASS_ID", "1");
    form.append("TYPE_ID", $("[name='TYPE_ID']").val());
    form.append("REGHOME_ADDR", $("[name='REGHOME_ADDR']").val());
    form.append("REGHOME_ADDR_NAME", $("[name='REGHOME_ADDR_NAME']").val());
    form.append("REGHOME_MOO", $("[name='REGHOME_MOO']").val());
    form.append("REGHOME_SOI", $("[name='REGHOME_SOI']").val());
    form.append("REGHOME_ROAD", $("[name='REGHOME_ROAD']").val());
    //form.append("REGHOME_TUMBOL", "ต.ในเมือง");
    //form.append("REGHOME_AMPHUR", "50");
    //form.append("REGHOME_PROV", "10");
    form.append("REGHOME_POST", $("[name='REGHOME_POST']").val());
    //form.append("EXPERTISE", "ความรู้ด้านกีฬาฟุตบอล");

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/member/editGmsMember",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        if (jqXHR.status == 200) {
            $("#Modal_Results").modal("show");
            var memberId = localStorage.getItem("memberId");
            console.log("memberId", memberId);
            getGmsMember(token, memberId);
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

            $("txtMEMBER_ID").val(MEMBER_ID);
            $("#dvMEMBER_IMAGE").html(`<img src="${value.MEMBER_IMAGE == null ? "/images/psn010101_02.png" : value.MEMBER_IMAGE}" />`);
            $("#dvMEMBER_IMAGE2").html(`<img src="${value.MEMBER_IMAGE == null ? "/images/psn010101_02.png" : value.MEMBER_IMAGE}" />`);
            $("#lblName").html(`${value.FIRST_NAME} ${value.LAST_NAME}`);
            $("#lblName2").html(`${value.FIRST_NAME} ${value.LAST_NAME}`);
            $("#lblEXPERTISE").html(value.EXPERTISE);
            $("#lblEXPERTISE2").html(value.EXPERTISE);
            $("#lblE_MAIL").html(value.E_MAIL);
            $("#lblE_MAIL2").html(value.E_MAIL);
            $("#lblTYPE_SUBJECT").html(`ความชำนาญ : ${value.TYPE_SUBJECT == null ? "-" : value.TYPE_SUBJECT}`);

            $("#ddlSEX").val(value.SEX);

            $("#txtHRS_ID").val(value.HRS_ID);
            $("#txtFIRST_NAME").val(value.FIRST_NAME);
            $("#txtLAST_NAME").val(value.LAST_NAME);
            $("#txtFIRST_NAME_ENG").val(value.FIRST_NAME_ENG);
            $("#txtLAST_NAME_ENG").val(value.LAST_NAME_ENG);
            $("#txtRELIGION").val(value.RELIGION);

            var bdTH = ConvertDateToTH(value.BIRTH_DATE);
            console.log("value.BIRTH_DATE", value.BIRTH_DATE);
            console.log("bdTH", bdTH);
            console.log("bdEN", ConvertDateToEN(bdTH));
            $("#txtBIRTH_DATE").val(bdTH);

            GetHistory(token, value.MEMBER_ID);
            getEducationHistory(token, value.MEMBER_ID);
            getWorkHistory(token, value.MEMBER_ID);

            // Load Dropdownlist and set selected value.
            getEducation(token, value.EDU_ID);
            getPrefix(token, value.PREFIX_ID);
            GetGmsType(token, value.TYPE_ID);


            $("#txtCON_ADDR").val(value.CON_ADDR);
            $("#txtCON_ADDR_NAME").val(value.CON_ADDR_NAME);
            $("#txtCON_MOO").val(value.CON_MOO);
            $("#txtCON_SOI").val(value.CON_SOI);
            $("#txtCON_ROAD").val(value.CON_ROAD);
            $("#txtCON_POST").val(value.CON_POST);
            $("#txtCON_TEL").val(value.CON_TEL);
            

            GetProvince(token, "ddlCON_PROV", value.CON_PROV);
            GetAmphur(token, "ddlCON_AMPHUR", value.CON_PROV, value.CON_AMPHUR);
            GetTambon(token, "ddlCON_TUMBOL", value.CON_PROV, value.CON_AMPHUR, null /*value.CON_TUMBOL*/);

            $("#txtREGHOME_ADDR").val(value.REGHOME_ADDR);
            $("#txtREGHOME_ADDR_NAME").val(value.REGHOME_ADDR_NAME);
            $("#txtREGHOME_MOO").val(value.REGHOME_MOO);
            $("#txtREGHOME_SOI").val(value.REGHOME_SOI);
            $("#txtREGHOME_ROAD").val(value.REGHOME_ROAD);
            $("#txtREGHOME_POST").val(value.REGHOME_POST);

            GetProvince(token, "ddlREGHOME_PROV", value.REGHOME_PROV);
            GetAmphur(token, "ddlREGHOME_AMPHUR", value.REGHOME_PROV, value.REGHOME_AMPHUR);
            GetTambon(token, "ddlREGHOME_TUMBOL", value.REGHOME_PROV, value.REGHOME_AMPHUR, null /*value.REGHOME_TUMBOL*/);
        });

    });
}

function GetHistory(token, MEMBER_ID) {
    console.log("GetHistory ", MEMBER_ID);

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
                items += `
                <tr>
                    <td>${index + 1}</td>
                    <td>${value.TERM_YEAR}</td>
                    <td>${value.COURSE_SUBJECT}</td>
                    <td>${value.SPORT_SUBJECT}</td>
                    <td>${value.LEVEL_DETAIL}</td>
                    <td class="center"><a href="#" class="button small red">&nbsp;ลบ&nbsp;</a> <a href="#" class="button small darkgreen">แก้ไข</a></td>
                </tr>
                `;
            });
            $("#tblGMS_HISTORY >tbody").html(items);
        }
    });
}

function AddEducationHistory(token, MEMBER_ID) {

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);
    form.append("EDU_YR", $("[name='EDU_YR']").val());
    form.append("EDU_LEVEL", $("[name='EDU_LEVEL']").val());
    form.append("EDU_DEPT", $("[name='EDU_DEPT']").val());
    form.append("EDU_INSTITUTE", $("[name='EDU_INSTITUTE']").val());

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryEdu/pushHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
        $("#Modal_AddEdu").modal("hide");
        getEducationHistory(token, MEMBER_ID);
    });
}

function DeleteEducationHistory(token, MEMBER_ID, DI_EDU_ID) {
    var form = new FormData();
    form.append("Token", token);
    form.append("DI_EDU_ID", DI_EDU_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryEdu/delHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
        getEducationHistory(token, MEMBER_ID);
    });
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
                        <td>${index + 1}</td>
                        <td>${value.EDU_YR}</td>
                        <td>${value.EDU_LEVEL}</td>
                        <td>${value.EDU_DEPT}</td>
                        <td>${value.EDU_INSTITUTE}</td>
                        <td class="center"><a href="javascript:void(0);" onclick="DeleteEducationHistory('${token}', '${MEMBER_ID}', '${value.DI_EDU_ID}');" class="button small red">&nbsp;ลบ&nbsp;</a> <a href="#" class="button small darkgreen">แก้ไข</a></td>
                    </tr>
                `
            });
            $("#tblEduHistory >tbody").html(items);
        }
    });
}

function getWorkHistory(token, MEMBER_ID) {
    console.log("getWorkHistory");

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
                        <td>${index+1}</td>
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

            if (data.length > 0) {
                $("#lblCurrentSPORT_SUBJECT").html(data[0].SPORT_SUBJECT);
                $("#lblCurrentLEVEL_DETAIL").html(data[0].LEVEL_DETAIL);
            }
        }
    });
}



$(document).ready(function () {

    //GetToken().done(function (response) {
    //    var token = JSON.parse(response).data;
    //    localStorage.setItem("token", token);
    //    console.log("localStorage.token", localStorage.getItem("token"));

    //    getEducation(token);
    //    getPrefix(token);
    //});

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        var url = window.location.href;
        var id = url.substring(url.lastIndexOf('/') + 1);

        localStorage.setItem("memberId", id);
        getGmsMember(token, id);
    });

    $("#btnSave").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            EditGmsMember(token, localStorage.getItem("memberId"));
        });
    });

    $("#btnAddEdu").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            AddEducationHistory(token, localStorage.getItem("memberId"));
        });
    });

    
    $("#ddlCON_PROV").change(function () {
        var provinceId = $("#ddlCON_PROV").val();
        if (provinceId == "") {
            $("#ddlCON_AMPHUR").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlCON_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, "ddlCON_AMPHUR", provinceId, null);
            $("#ddlCON_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
    });

    $("#ddlCON_AMPHUR").change(function () {
        var provinceId = $("#ddlCON_PROV").val();
        var amphurId = $("#ddlCON_AMPHUR").val();

        if (amphurId == "") {
            $("#ddlCON_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetTambon(token, "ddlCON_TUMBOL", provinceId, amphurId, null);
        }
    });

    $("#ddlREGHOME_PROV").change(function () {
        var provinceId = $("#ddlREGHOME_PROV").val();
        if (provinceId == "") {
            $("#ddlREGHOME_AMPHUR").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlREGHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, "ddlREGHOME_AMPHUR", provinceId, null);
            $("#ddlREGHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
    });

    $("#ddlREGHOME_AMPHUR").change(function () {
        var provinceId = $("#ddlREGHOME_PROV").val();
        var amphurId = $("#ddlREGHOME_AMPHUR").val();

        if (amphurId == "") {
            $("#ddlREGHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetTambon(token, "ddlREGHOME_TUMBOL", provinceId, amphurId, null);
        }
    });




    ddlREGHOME_PROV
});


