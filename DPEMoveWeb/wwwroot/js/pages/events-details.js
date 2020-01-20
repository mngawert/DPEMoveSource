
function GetMEventFacilitiesTopic() {

    console.log('start GetMEventFacilitiesTopic');
    var options = {};

    options.url = "/webapi/Events/GetMEventFacilitiesTopic";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (data) {
        $.each(data, function (index, value) {

            var obj = {};
            obj.eventId = routeId;
            obj.mEventFacilitiesTopicId = value.eventFacilitiesTopicId;
            options.data = JSON.stringify(obj);
            console.log("input", options.data);

            GetEventFacilitiesFromSession(obj)
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetEventFacilitiesFromSession(obj) {

    console.log('start GetEventFacilitiesFromSession');
    var options = {};
    var input = {};
    input.eventId = obj.eventId;
    input.mEventFacilitiesTopicId = obj.mEventFacilitiesTopicId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/GetEventFacilitiesFromSession";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {
            items +=
                `
                <tr>
                    <td>` + value.eventFacilitiesName + `</td>
                    <td>` + value.facilitiesAmount + `</td>
                    <td>` + value.facilitiesUnit + `</td>
                    <td class="center"><button type="button" onclick="DeleteEventFacilitiesFromSession(` + value.eventId + `,` + value.mEventFacilitiesTopicId + `,` + value.eventFacilitiesId + `)" class="button small red">&nbsp;ลบ&nbsp;</button></td>
                </tr>
                `
        });

        $("#tblTopic_" + obj.mEventFacilitiesTopicId +" > tbody").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function AddEventFacilitiesToSession(eventId, mEventFacilitiesTopicId) {
    console.log('start AddEventFacilitiesToSession ');

    if ($("#txtFacilityName_" + mEventFacilitiesTopicId).val() == "")
        return false;
    if ($("#txtFacilityAmount_" + mEventFacilitiesTopicId).val() == "")
        return false;
    if ($("#txtFacilityUnit_" + mEventFacilitiesTopicId).val() == "")
        return false;

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.mEventFacilitiesTopicId = mEventFacilitiesTopicId;
    input.eventFacilitiesName = $("#txtFacilityName_" + mEventFacilitiesTopicId).val();
    input.facilitiesAmount = $("#txtFacilityAmount_" + mEventFacilitiesTopicId).val();
    input.facilitiesUnit = $("#txtFacilityUnit_" + mEventFacilitiesTopicId).val();
    input.createdBy = "0";
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/AddEventFacilitiesToSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        // re-load data from session.
        var obj = {};
        obj.eventId = eventId;
        obj.mEventFacilitiesTopicId = mEventFacilitiesTopicId;
        GetEventFacilitiesFromSession(obj);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function DeleteEventFacilitiesFromSession(eventId, mEventFacilitiesTopicId, eventFacilitiesId) {
    console.log('start DeleteEventFacilitiesFromSession ');

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.mEventFacilitiesTopicId = mEventFacilitiesTopicId;
    input.eventFacilitiesId = eventFacilitiesId;

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/DeleteEventFacilitiesFromSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        var obj = {};
        obj.eventId = eventId;
        obj.mEventFacilitiesTopicId = mEventFacilitiesTopicId;
        GetEventFacilitiesFromSession(obj);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetProvinceName(proviceId) {

    console.log("call GetProvince");
    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=province";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        //console.log(data);
        $.each(data.DATA, function (index, value) {
            //console.log("check", value);
            if (value.pcode == proviceId) {
                //console.log("found!");
                $("#lblProvinceName").html(value.province);
            }
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetAmphurName(provinceId, amphurId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=amphur&province=" + provinceId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        $.each(data.DATA, function (index, value) {
            if (value.pcode == amphurId) {
                $("#lblAmphurName").html(value.amphur);
            }
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetTambonName(provinceId, amphurId, tambonId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=tambon&province=" + provinceId + "&amphur=" + amphurId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        //console.log(data);
        $.each(data.DATA, function (index, value) {
            if (value.pcode == tambonId) {
                $("#lblTambonName").html(value.tambon);
            }
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function GetUploadedFile(eventId) {

    var options = {};

    var input = {};
    input.eventId = eventId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/GetUploadedFile";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("GetUploadedFile success");

        var items = "";
        $.each(data, function (index, value) {
            items +=
                `
                <li>
                    <div class="row event">
                        <div class="col-md-4">
                            <div class="event-thumb"><img src="` +value.fileUrl + `" /></div>
                        </div>
                        <div class="col-md-2">
                            <button type="button" onclick="DeleteUploadedFile(` + eventId + `, ` + value.uploadedFileId + `)" class="button small red">&nbsp;ลบ&nbsp;</button>
                        </div>
                    </div>
                </li>
                `
        });
        $("#ul-UploadedFile").html(items);

    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function UploadFile(fileId, eventId) {

    var files = $("#" + fileId).get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("file", files[i]);
    }

    $.ajax({
        type: "POST",
        url: "/Events/UploadFiles",
        dataType: "json",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (result, status, xhr) {
            var fileName = result;
            AddUploadedFileToDatabase(fileName, eventId);
        },
        error: function (xhr, status, error) {
            console.log("uploadfile error")
        }
    });
}

function AddUploadedFileToDatabase(fileName, eventId) {

    var options = {};

    var input = {};
    input.fileName = fileName;
    input.eventId = eventId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/AddUploadedFileToDatabase";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("AddUploadedFileToDatabase success");
        GetUploadedFile(eventId);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function DeleteUploadedFile(eventId, uploadedFileId) {
    var options = {};

    var input = {};
    input.eventId = eventId;
    input.uploadedFileId = uploadedFileId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/DeleteUploadedFile";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("DeleteUploadedFile success");
        GetUploadedFile(eventId);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetCommentsByEventId(eventId) {

    console.log('start GetCommentsByEventId');
    var options = {};

    options.url = "/webapi/Events/GetCommentsByEventId/"+eventId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (data) {
        console.log("data", data);
        var items = '';
        $.each(data, function (index, value) {
            items +=
            `
            <li>
                <div class="comment-head">
                    <div class="comment-by">` + value.userCode + `</div>
                    <div class="comment-when">` + value.createdDateTH + `</div>
                </div>
                <div class="comment-text">
                    ` + value.comment1 + `
                </div>
            </li>
            `
        });

        $("#ulComment").html(items);
        $("#lbl_CommentCount").html(data.length);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function AddComment(eventId) {

    console.log('start AddComment');

    if ($("#txtComment").val() == "")
        return false;

    var options = {};

    var input = {};
    input.commentOf = "1";
    input.eventOrStadiumCode = model.eventCode;
    input.comment1 = $("#txtComment").val();

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/AddComment";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        console.log("AddComment success");
        GetCommentsByEventId(eventId);
        //Clear
        $("#txtComment").val("");
        $("#collapseFour4").collapse('show'); // toggle collapse
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetVoteAvg(voteOf, eventOrStadiumCode) {
    var settings = {
        "url": "/WebApi/Votes/GetVoteAvg",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    console.log("settings", settings)

    $.ajax(settings).done(function (response) {
        console.log(response);
        //var value = JSON.parse(response);
        var value = response;
        $("#lbl_VoteAvg").html(value.voteAvg == null ? "-" : value.voteAvg);
        $("#lbl_VoteText").html(value.voteText == null ? "-" : value.voteText);
    });
}

function GetVote(voteOf, eventOrStadiumCode, voteTypeId, createdBy) {

    console.log("GetVote");

    var settings = {
        "url": "/WebApi/Votes/GetVote",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode, "voteTypeId": voteTypeId, "createdBy": createdBy }),
    };

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        console.log(data);

        console.log("GetVote response", data);
        console.log("jqXHR.status", jqXHR.status); //handle your 204 or other status codes here

        if (jqXHR.status == 200) {
            var value = data;
            if (value.voteValue >= 1)
                $("#dv_VoteValue").html(`<img src="/images/ic_star.png" alt="">`);
            if (value.voteValue >= 2)
                $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            if (value.voteValue >= 3)
                $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            if (value.voteValue >= 4)
                $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            if (value.voteValue >= 5)
                $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);

            //$("input:radio[name='VoteValue']:checked").val();
            //$('input[value="Admin3"]').prop("checked", true);
        }
    });
}

function AddOrEditVote(voteOf, eventOrStadiumCode, voteTypeId, voteValue, createdBy) {

    console.log("AddOrEditVote");

    var settings = {
        "url": "/WebApi/Votes/AddOrEditVote",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode, "voteTypeId": voteTypeId, "voteValue": voteValue, "createdBy": createdBy }),
    };

    console.log("settings", settings);

    $.ajax(settings).done(function (response) {
        console.log("done");
        console.log(response);

        GetVote("1", eventOrStadiumCode, voteTypeId, createdBy);
        GetVoteAvg("1", eventOrStadiumCode);
    });
}

$(document).ready(function () {

    var eventId = routeId;
    console.log("eventId=", eventId);
    console.log("eventOrStadiumCode", eventOrStadiumCode);
    console.log('appUserId=', appUserId);
    GetProvinceName(model.provinceCode);
    GetAmphurName(model.provinceCode, model.amphurCode.substr(2, 2));
    GetTambonName(model.provinceCode, model.amphurCode.substr(2, 2), model.tambonCode);
    GetVote("1", eventOrStadiumCode, 1001, appUserId)
    GetVoteAvg("1", eventOrStadiumCode);
    GetCommentsByEventId(eventId);

    $("#lnkGotoFacility").click(function(){
        $("#collapseOne1").collapse('show'); // toggle collapse
    });

    $("#btnVote").click(function () {
        var voteValue = $("input:radio[name='VoteValue']:checked").val();

        AddOrEditVote("1", eventOrStadiumCode, 1001, voteValue, appUserId);
        $("#Modal_AddOrEditVote").modal("hide");
    });


    //GetMEventFacilitiesTopic();
    //GetUploadedFile(eventId);
    //GetProvince();

    //$("#ddlProvince").change(function () {
    //    var provinceId = $("#ddlProvince").val();
    //    if (provinceId == "") {
    //        $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
    //        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    //    }
    //    else {
    //        GetAmphur(provinceId);
    //    }
    //});

    //$("#ddlAmphur").change(function () {
    //    var provinceId = $("#ddlProvince").val();
    //    var amphurId = $("#ddlAmphur").val().substr(2, 2);
    //    if (amphurId == "") {
    //        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    //    }
    //    else {
    //        GetTambon(provinceId, amphurId);
    //    }
    //});

});

