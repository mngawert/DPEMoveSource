
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

function GetEducation(token, selectedValue) {
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

function GetPrefix(token, selectedValue) {
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

function GetGmsWorkLevel(token, selectedValue) {

    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/level/getGmsWorkLevel",
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
                item_1 += `<option value="` + value.LEVEL_ID + `">` + value.LEVEL_DETAIL + `</option>`
            });
            $("#ddlLEVEL_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlLEVEL_ID").val(selectedValue);
            }
        }
    });
}

function GetGmsSport(token, selectedValue) {
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
        }
    });
}

function GetGmsTerm(token, TERM_YEAR, selectedValue) {
    var form = new FormData();
    form.append("Token", token);
    form.append("TERM_YEAR", TERM_YEAR);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/term/getGmsTerm",
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
                item_1 += `<option value="${value.TERM_ID}">${value.COURSE_SUBJECT} (${value.TERM_LOCATION})</option>`
            });
            $("#ddlTERM_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlTERM_ID").val(selectedValue);
            }
        }
    });
}

function GetTrainHistoryStatus(token, selectedValue) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryTrain/getHistoryStatusApprove",
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
                item_1 += `<option value="` + value.STATUS_ID + `">` + value.STATUS_NAME + `</option>`
            });
            $("#ddlSTATUS_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlSTATUS_ID").val(selectedValue);
            }
        }
    });
}

function GetGmsTermPosition(token, selectedValue) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/position/getGmsTermPosition",
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
                item_1 += `<option value="` + value.POSITION_ID + `">` + value.POSITION_NAME + `</option>`
            });
            $("#ddlPOSITION_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlPOSITION_ID").val(selectedValue);
            }
        }
    });
}

function GetGmsCareer(token, selectedValue) {
    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/career/getGmsCareer",
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
                item_1 += `<option value="` + value.CAREER_ID + `">` + value.CAREER_NAME + `</option>`
            });
            $("#ddlCAREER_ID").html(item_1);

            if (selectedValue != null) {
                $("#ddlCAREER_ID").val(selectedValue);
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

    if (!$("#frmTab1")[0].checkValidity()) {

        $("#frmTab1")[0].reportValidity()
        return false;
    }

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);
    form.append("FIRST_NAME", $("[name='FIRST_NAME']").val());
    form.append("LAST_NAME", $("[name='LAST_NAME']").val());
    form.append("SEX", $("[name='SEX']").val());
    //form.append("WEIGHT", "63");
    //form.append("TALL", "165");
    form.append("BIRTH_DATE", ConvertDateToEN($("[name='BIRTH_DATE']").val()));
    form.append("HOME_ADDR", $("[name='HOME_ADDR']").val());
    form.append("HOME_ADDR_NAME", $("[name='HOME_ADDR_NAME']").val());
    form.append("HOME_MOO", $("[name='HOME_MOO']").val());
    form.append("HOME_SOI", $("[name='HOME_SOI']").val());
    form.append("HOME_ROAD", $("[name='HOME_ROAD']").val());
    form.append("HOME_TUMBOL_ID", $("[name='HOME_TUMBOL']").val());
    //if ($("[name='HOME_TUMBOL']").val() != "") {
    //    form.append("HOME_TUMBOL", $("[name='HOME_TUMBOL']").text());
    //}
    form.append("HOME_AMPHUR", $("[name='HOME_AMPHUR']").val());
    form.append("HOME_PROV", $("[name='HOME_PROV']").val());
    form.append("HOME_POST", $("[name='HOME_POST']").val());
    form.append("HOME_TEL", $("[name='HOME_TEL']").val());
    //form.append("HOME_FAX", "01211231");
    form.append("E_MAIL", $("[name='E_MAIL']").val());
    form.append("CON_ADDR", $("[name='CON_ADDR']").val());
    form.append("CON_ADDR_NAME", $("[name='CON_ADDR_NAME']").val());
    form.append("CON_MOO", $("[name='CON_MOO']").val());
    form.append("CON_SOI", $("[name='CON_SOI']").val());
    form.append("CON_ROAD", $("[name='CON_ROAD']").val());
    form.append("CON_TUMBOL_ID", $("[name='CON_TUMBOL']").val());
    //if ($("[name='CON_TUMBOL']").val() != "") {
    //    form.append("CON_TUMBOL", $("[name='CON_TUMBOL']").text());
    //}
    form.append("CON_AMPHUR", $("[name='CON_AMPHUR']").val());
    form.append("CON_PROV", $("[name='CON_PROV']").val());
    form.append("CON_POST", $("[name='CON_POST']").val());
    form.append("CON_TEL", $("[name='CON_TEL']").val());
    //form.append("CON_FAX", $("[name='CON_FAX']").val());
    //form.append("CON_EMAIL", "asdff@gmail.com");
    //form.append("HRS_HIS", "ภูมิลำเนาเดิม อยู่ที่ จ.สมุทรสงครามนะ");
    form.append("FIRST_NAME_ENG", $("[name='FIRST_NAME_ENG']").val());
    form.append("LAST_NAME_ENG", $("[name='LAST_NAME_ENG']").val());
    form.append("EDU_ID", $("[name='EDU_ID']").val());
    form.append("RELIGION", $("[name='RELIGION']").val());
    form.append("JOB_SUBJECT", $("[name='JOB_SUBJECT']").val());
    form.append("JOB_POSITION", $("[name='JOB_POSITION']").val());
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
    form.append("REGHOME_TUMBOL_ID", $("[name='REGHOME_TUMBOL']").val());
    //if ($("[name='REGHOME_TUMBOL']").val() != "") {
    //    form.append("REGHOME_TUMBOL", $("[name='REGHOME_TUMBOL']").text());
    //}
    form.append("REGHOME_AMPHUR", $("[name='REGHOME_AMPHUR']").val());
    form.append("REGHOME_PROV", $("[name='REGHOME_PROV']").val());
    form.append("REGHOME_POST", $("[name='REGHOME_POST']").val());
    form.append("EXPERTISE", $("[name='EXPERTISE']").val());
    form.append("CAREER_ID", $("[name='CAREER_ID']").val());

    console.log("posting MEMBER_IMAGE", $("[name='MEMBER_IMAGE']").val());
    console.log("posting MEMBER_IMAGE length", $("[name='MEMBER_IMAGE']").val().length);
    if ($("[name='MEMBER_IMAGE']").val().length > 0) {
        form.append("MEMBER_IMAGE", $("[name='MEMBER_IMAGE']").val());
    }

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
            GetGmsMember(token, memberId);
        }
    });
}


function GetGmsMember(token, MEMBER_ID) {

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

            if (value.HRS_ID == appIdcardNo) {
                localStorage.setItem("Mode", "Edit");
            }

            $("txtMEMBER_ID").val(MEMBER_ID);
            $("#dvMEMBER_IMAGE").html(`<img id='imgPreview' src="${value.MEMBER_IMAGE == null ? "/images/psn010101_02.png" : value.MEMBER_IMAGE}" />`);
            $("#dvMEMBER_IMAGE2").html(`<img src="${value.MEMBER_IMAGE == null ? "/images/psn010101_02.png" : value.MEMBER_IMAGE}" />`);
            $("[name='MEMBER_IMAGE']").val(value.MEMBER_IMAGE);
            $("#lblName").html(`${value.FIRST_NAME} ${value.LAST_NAME}`);
            $("#lblName2").html(`${value.FIRST_NAME} ${value.LAST_NAME}`);
            $("#txtEXPERTISE").val(value.EXPERTISE);
            $("#lblEXPERTISE2").html(value.EXPERTISE);
            $("#txtE_MAIL").val(value.E_MAIL);
            //$("#lblE_MAIL").html(value.E_MAIL);
            $("#lblE_MAIL2").html(value.E_MAIL);
            $("#lblTYPE_SUBJECT").html(value.TYPE_SUBJECT == null ? "-" : value.TYPE_SUBJECT);

            $("#ddlSEX").val(value.SEX);

            $("#txtHRS_ID").val(value.HRS_ID);
            $("#txtFIRST_NAME").val(value.FIRST_NAME);
            $("#txtLAST_NAME").val(value.LAST_NAME);
            $("#txtFIRST_NAME_ENG").val(value.FIRST_NAME_ENG);
            $("#txtLAST_NAME_ENG").val(value.LAST_NAME_ENG);
            $("#txtRELIGION").val(value.RELIGION);
            $("#txtJOB_SUBJECT").val(value.JOB_SUBJECT);
            $("#txtJOB_POSITION").val(value.JOB_POSITION);

            var bdTH = ConvertDateToTH(value.BIRTH_DATE);
            console.log("value.BIRTH_DATE", value.BIRTH_DATE);
            console.log("bdTH", bdTH);
            console.log("bdEN", ConvertDateToEN(bdTH));
            $("#txtBIRTH_DATE").val(bdTH);

            GetTrainHistory(token, value.MEMBER_ID);
            GetEducationHistory(token, value.MEMBER_ID);
            GetWorkHistory(token, value.MEMBER_ID);

            GetInternalToken().done(function (response) {
                var internalToken = response;
                GetReportEvent2(internalToken, value.HRS_ID);
            });

            // Load Dropdownlist and set selected value.
            GetEducation(token, value.EDU_ID);
            GetPrefix(token, value.PREFIX_ID);
            GetGmsType(token, value.TYPE_ID);
            GetGmsWorkLevel(token, null);
            GetGmsSport(token, null);
            GetTrainHistoryStatus(token, null);
            GetGmsTermPosition(token, null);
            GetGmsCareer(token, value.CAREER_ID);

            $("#txtCON_ADDR").val(value.CON_ADDR);
            $("#txtCON_ADDR_NAME").val(value.CON_ADDR_NAME);
            $("#txtCON_MOO").val(value.CON_MOO);
            $("#txtCON_SOI").val(value.CON_SOI);
            $("#txtCON_ROAD").val(value.CON_ROAD);
            $("#txtCON_POST").val(value.CON_POST);
            $("#txtCON_TEL").val(value.CON_TEL);
            
            GetProvince(token, "ddlCON_PROV", value.CON_PROV);
            GetAmphur(token, "ddlCON_AMPHUR", value.CON_PROV, value.CON_AMPHUR);
            GetTambon(token, "ddlCON_TUMBOL", value.CON_PROV, value.CON_AMPHUR, value.CON_TUMBOL_ID);

            $("#txtHOME_ADDR").val(value.HOME_ADDR);
            $("#txtHOME_ADDR_NAME").val(value.HOME_ADDR_NAME);
            $("#txtHOME_MOO").val(value.HOME_MOO);
            $("#txtHOME_SOI").val(value.HOME_SOI);
            $("#txtHOME_ROAD").val(value.HOME_ROAD);
            $("#txtHOME_POST").val(value.HOME_POST);
            $("#txtHOME_TEL").val(value.HOME_TEL);

            GetProvince(token, "ddlHOME_PROV", value.HOME_PROV);
            GetAmphur(token, "ddlHOME_AMPHUR", value.HOME_PROV, value.HOME_AMPHUR);
            GetTambon(token, "ddlHOME_TUMBOL", value.HOME_PROV, value.HOME_AMPHUR, value.HOME_TUMBOL_ID);

            $("#txtREGHOME_ADDR").val(value.REGHOME_ADDR);
            $("#txtREGHOME_ADDR_NAME").val(value.REGHOME_ADDR_NAME);
            $("#txtREGHOME_MOO").val(value.REGHOME_MOO);
            $("#txtREGHOME_SOI").val(value.REGHOME_SOI);
            $("#txtREGHOME_ROAD").val(value.REGHOME_ROAD);
            $("#txtREGHOME_POST").val(value.REGHOME_POST);

            GetProvince(token, "ddlREGHOME_PROV", value.REGHOME_PROV);
            GetAmphur(token, "ddlREGHOME_AMPHUR", value.REGHOME_PROV, value.REGHOME_AMPHUR);
            GetTambon(token, "ddlREGHOME_TUMBOL", value.REGHOME_PROV, value.REGHOME_AMPHUR, value.REGHOME_TUMBOL_ID);
        });

        if (localStorage.getItem("Mode") == "View") {
            ChangeHtmlToModeView();
        }
    });
}

function AddTrainHistory(token, MEMBER_ID) {

    if (!$("#frmAddTrainHistory")[0].checkValidity()) {

        $("#frmAddTrainHistory")[0].reportValidity()
        return false;
    }

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);
    form.append("TERM_ID", $("[name='TERM_ID']").val());
    form.append("HISTORY_STATUS_REGIS", $("[name='HISTORY_STATUS_REGIS']").val());
    form.append("HISTORY_STATUS_APPROVE", $("[name='HISTORY_STATUS_APPROVE']").val());
    form.append("HISTORY_REMARK", $("[name='HISTORY_REMARK']").val());
    form.append("HISTORY_TIME_REGIS", ConvertDateToEN($("[name='HISTORY_TIME_REGIS']").val()));
    form.append("HISTORY_NO", $("[name='HISTORY_NO']").val());
    form.append("POSITION_ID", $("[name='POSITION_ID']").val());

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryTrain/pushHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, status, xhr) {
        console.log("resp", response);
        if (xhr.status == 200) {
            $("#Modal_AddTrainHistory").modal("hide");
            $("#frmAddTrainHistory").trigger("reset");
            GetTrainHistory(token, MEMBER_ID);
        }
    });
}

function DeleteTrainHistory(token, MEMBER_ID, HISTORY_ID) {
    var form = new FormData();
    form.append("Token", token);
    form.append("HISTORY_ID", HISTORY_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryTrain/delHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
        GetTrainHistory(token, MEMBER_ID);
    });
}

function GetTrainHistory(token, MEMBER_ID) {
    console.log("GetTrainHistory ", MEMBER_ID);

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
                    <td class="center ForModeEdit"><a href="javascript:void(0);" onclick="DeleteTrainHistory('${token}', '${MEMBER_ID}', '${value.HISTORY_ID}');" class="button small red">&nbsp;ลบ&nbsp;</a></td>
                </tr>
                `;
            });
            $("#tblGMS_HISTORY >tbody").html(items);
            if (localStorage.getItem("Mode") == "View") {
                ChangeHtmlToModeView();
            }
        }
    });
}

function AddEducationHistory(token, MEMBER_ID) {

    if (!$("#frmAddEducationHistory")[0].checkValidity()) {

        $("#frmAddEducationHistory")[0].reportValidity()
        return false;
    }

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
        $("#Modal_AddEduHistory").modal("hide");
        $("#frmAddEducationHistory").trigger("reset");
        GetEducationHistory(token, MEMBER_ID);
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
        GetEducationHistory(token, MEMBER_ID);
    });
}

function GetEducationHistory(token, MEMBER_ID) {
    console.log("GetEducationHistory");

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
                        <td class="center ForModeEdit"><a href="javascript:void(0);" onclick="DeleteEducationHistory('${token}', '${MEMBER_ID}', '${value.DI_EDU_ID}');" class="button small red">&nbsp;ลบ&nbsp;</a></td>
                    </tr>
                `
            });
            $("#tblEduHistory >tbody").html(items);
            if (localStorage.getItem("Mode") == "View") {
                ChangeHtmlToModeView();
            }
        }
    });
}

function AddWorkHistory(token, MEMBER_ID) {

    if (!$("#frmAddWorkHistory")[0].checkValidity()) {

        $("#frmAddWorkHistory")[0].reportValidity()
        return false;
    }

    var form = new FormData();
    form.append("Token", token);
    form.append("MEMBER_ID", MEMBER_ID);
    //form.append("PROVINCE_ID", "13");
    //form.append("AMPHUR_ID", "03");
    //form.append("TUMBOL_ID", "02");
    form.append("WORK_SUBJECT", $("[name='WORK_SUBJECT']").val());
    form.append("WORK_DETAIL", $("[name='WORK_DETAIL']").val());
    form.append("WORK_TIME_START", ConvertDateToEN($("[name='WORK_TIME_START']").val()));
    form.append("WORK_TIME_END", ConvertDateToEN($("[name='WORK_TIME_END']").val()));
    form.append("WORK_LOCATION", $("[name='WORK_LOCATION']").val());
    form.append("WORK_LEVEL", $("[name='WORK_LEVEL']").val());
    form.append("SPORT_ID", $("[name='SPORT_ID']").val());
    form.append("WORK_SEQ", $("[name='WORK_SEQ']").val());
    form.append("WORK_YEAR", $("[name='WORK_YEAR']").val());

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryWork/pushHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, status, xhr) {
        if (xhr.status == 200) {
            $("#Modal_AddWorkHistory").modal("hide");
            $("#frmAddWorkHistory").trigger("reset");
            GetWorkHistory(token, MEMBER_ID);
        }
    });
}

function DeleteWorkHistory(token, MEMBER_ID, WORK_ID) {

    var form = new FormData();
    form.append("Token", token);
    form.append("WORK_ID", WORK_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/personal/memberHistoryWork/delHistory",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, status, xhr) {
        if (xhr.status == 200) {
            GetWorkHistory(token, MEMBER_ID);
        }
    });
}


function GetWorkHistory(token, MEMBER_ID) {
    console.log("GetWorkHistory");

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
                        <td>${ConvertDateToTH(value.WORK_TIME_START)} - ${ConvertDateToTH(value.WORK_TIME_END)}</td>
                        <td class="center ForModeEdit"><a href="javascript:void(0);" onclick="DeleteWorkHistory('${token}', '${MEMBER_ID}', '${value.WORK_ID}');" class="button small red">&nbsp;ลบ&nbsp;</a></td>
                    </tr>
                `
            });
            $("#tblWorkHistory >tbody").html(items);

            //if (data.length > 0) {
            //    $("#lblCurrentSPORT_SUBJECT").html(data[0].SPORT_SUBJECT);
            //    $("#lblCurrentLEVEL_DETAIL").html(data[0].LEVEL_DETAIL);
            //}
        }
    });
}

function GetInternalToken() {

    var settings = {
        "url": "/api/Account/GetToken",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "email": "readonly@gmail.com", "password": "Bossup2020" }),
    };

    return $.ajax(settings);
}

function GetReportEvent2(token, idCard) {

    var settings = {
        "url": "/api/Report/GetReportEvent2",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "idCard": idCard }),
    };

    $.ajax(settings).done(function (response, status, xhr) {
        if (xhr.status == 200) {
            var data = response;
            var items = "";
            $.each(data, function (index, value) {
                items += `
                    <tr>
                        <td>${index + 1}</td>
                        <td>${value.eventName}</td>
                        <td>${value.participantCount == null ? "" : value.participantCount}</td>
                        <td>${ConvertDateToTH(value.eventStartDate)}</td>
                    </tr>
                `
            });
            $("#tblReportEvent2 >tbody").html(items);
            if (localStorage.getItem("Mode") == "View") {
                ChangeHtmlToModeView();
            }
        }
    });
}

function previewFile() {

    console.log("previewFile");

    const preview = document.querySelector('#imgPreview');
    const file = document.querySelector('#filePreview').files[0];
    const reader = new FileReader();

    console.log("preview", preview);
    console.log("file", file);

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
        var base64result = reader.result.split(',')[1];
        $("[name='MEMBER_IMAGE']").val(base64result);
        console.log("MEMBER_IMAGE of upload", $("[name='MEMBER_IMAGE']").val());
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}


function SendEmail(to, subject, body) {

    var settings = {
        "url": "/WebApi/PSN/SendEmail",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "to": to, "subject": subject, "body": body }),
    };

    $.ajax(settings).done(function (response, status, xhr) {
        if (xhr.status == 200) {

            $("#ModalContact").modal("hide");
        }
    });
}

function ChangeHtmlToModeView() {

    $("input[type=text]").each(function () {
        var text = $(this).val();
        if (text.length == 0) {
            text = "-";
        }
        $(this).replaceWith("<label>" + text + "</label>");
    });

    $("select").each(function () {
        var ddlText = $(this).children(':selected').text();
        if (ddlText == "กรุณาเลือก") {
            ddlText = "-";
        }
        $(this).replaceWith("<label>" + ddlText + "</label>");
    });

    $(".ForModeEdit").hide();
    //$("label").css("margin", "0px");
}

$(document).ready(function () {

    console.log("Mode", Mode);
    localStorage.setItem("Mode", Mode);

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        //var url = window.location.href.replace("#","");
        //var id = url.substring(url.lastIndexOf('/') + 1);
        var id = routeId;
        localStorage.setItem("memberId", id);
        GetGmsMember(token, id);
    });

    $("#btnSave").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            EditGmsMember(token, localStorage.getItem("memberId"));
        });
    });

    $("#btnAddEduHistory").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            AddEducationHistory(token, localStorage.getItem("memberId"));
        });
    });

    $("#btnAddTrainHistory").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            AddTrainHistory(token, localStorage.getItem("memberId"));
        });
    });

    $("#btnAddWorkHistory").click(function () {

        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            AddWorkHistory(token, localStorage.getItem("memberId"));
        });
    });

    $("#txtTERM_YEAR").blur(function () {
        GetToken().done(function (response) {
            var token = JSON.parse(response).data;
            GetGmsTerm(token, $("#txtTERM_YEAR").val(), null);
        });
    });

    $("#chkCopyCON_ADDR_TO_REGHOME").click(function () {

        if ($("#chkCopyCON_ADDR_TO_REGHOME").prop("checked")) {

            $("#ddlREGHOME_PROV").html($("#ddlCON_PROV").html());
            $("#ddlREGHOME_AMPHUR").html($("#ddlCON_AMPHUR").html());
            $("#ddlREGHOME_TUMBOL").html($("#ddlCON_TUMBOL").html());
            $("#ddlREGHOME_PROV").val($("#ddlCON_PROV").val());
            $("#ddlREGHOME_AMPHUR").val($("#ddlCON_AMPHUR").val());
            $("#ddlREGHOME_TUMBOL").val($("#ddlCON_TUMBOL").val());

            $("[name='REGHOME_ADDR']").val($("[name='CON_ADDR']").val());
            $("[name='REGHOME_ADDR_NAME']").val($("[name='CON_ADDR']").val());
            $("[name='REGHOME_MOO']").val($("[name='CON_MOO']").val());
            $("[name='REGHOME_SOI']").val($("[name='CON_SOI']").val());
            $("[name='REGHOME_ROAD']").val($("[name='CON_ROAD']").val());
            $("[name='REGHOME_POST']").val($("[name='CON_POST']").val());
        }
    });

    $("#btnSendEmail").click(function () {
        if (!$("#frmSendEmail")[0].checkValidity()) {

            $("#frmSendEmail")[0].reportValidity()
            return false;
        }

        var to = $("#lblE_MAIL2").text();
        var body = $("#txtSendEmailBody").val();
        if (body.length > 0) {
            SendEmail(to, "ข้อความติดต่อจาก DPEMove", body);
        }
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

    $("#ddlHOME_PROV").change(function () {
        var provinceId = $("#ddlHOME_PROV").val();
        if (provinceId == "") {
            $("#ddlHOME_AMPHUR").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, "ddlHOME_AMPHUR", provinceId, null);
            $("#ddlHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
    });

    $("#ddlHOME_AMPHUR").change(function () {
        var provinceId = $("#ddlHOME_PROV").val();
        var amphurId = $("#ddlHOME_AMPHUR").val();

        if (amphurId == "") {
            $("#ddlHOME_TUMBOL").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetTambon(token, "ddlHOME_TUMBOL", provinceId, amphurId, null);
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


});


