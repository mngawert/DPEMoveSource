


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
                    <td>` + (value.facilitiesAmount == null ? "" : value.facilitiesAmount) + `</td>
                    <td>` + (value.facilitiesUnit == null ? "" : value.facilitiesUnit) + `</td>
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
    //if ($("#txtFacilityAmount_" + mEventFacilitiesTopicId).val() == "")
    //    return false;
    //if ($("#txtFacilityUnit_" + mEventFacilitiesTopicId).val() == "")
    //    return false;

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

    // clear texbox.
    $("#txtFacilityName_" + mEventFacilitiesTopicId).val("");
    $("#txtFacilityAmount_" + mEventFacilitiesTopicId).val("");
    $("#txtFacilityUnit_" + mEventFacilitiesTopicId).val("");

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


function GetEventNearbyFromSession(eventId) {

    console.log('start GetEventNearbyFromSession');
    var options = {};
    var input = {};
    input.eventId = eventId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/GetEventNearbyFromSession";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {
            items +=
                `
                <tr>
                    <td>` + value.nearbyName + `</td>
                    <td>` + (value.distance == null ? "" : value.distance) + `</td>
                    <td>` + (value.distanceUnit == null ? "" : value.distanceUnit) + `</td>
                    <td class="center"><button type="button" onclick="DeleteEventNearbyFromSession(` + value.eventId + `,` + value.eventNearbyId + `)" class="button small red">&nbsp;ลบ&nbsp;</button></td>
                </tr>
                `
        });

        $("#tblEventNearby > tbody").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function AddEventNearbyToSession(eventId) {
    console.log('start AddEventNearbyToSession ');


    if (!$("#frmAddEventNearby")[0].checkValidity()) {

        $("#frmAddEventNearby")[0].reportValidity()
        return false;
    }

    //if ($("#txtEventNearbyName").val() == "") {
    //    $("#txtEventNearbyName").setCustomValidity('Enter User Name Here')
    //    return false;
    //}


    //if ($("#txtEventNearbyAmount").val() == "")
    //    return false;
    //if ($("#txtEventNearbyUnit").val() == "")
    //    return false;

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.nearbyName = $("#txtEventNearbyName").val();
    input.distance = $("#txtEventNearbyAmount").val();
    input.distanceUnit = $("#txtEventNearbyUnit").val();
    input.createdBy = "0";
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    // clear texbox.
    $("#txtEventNearbyName").val("");
    $("#txtEventNearbyAmount").val("");
    $("#txtEventNearbyUnit").val("");

    options.url = "/webapi/Events/AddEventNearbyToSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");

        $("#Modal_AddEventNearby").modal("toggle");

        // re-load data from session.
        GetEventNearbyFromSession(eventId);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function DeleteEventNearbyFromSession(eventId, eventNearbyId) {
    console.log('start DeleteEventNearbyFromSession ');

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.eventNearbyId = eventNearbyId;

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/DeleteEventNearbyFromSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success delete");
        GetEventNearbyFromSession(eventId);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}



function GetProvince() {

    console.log("call GetProvince");

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=province";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.province + `</option>
                `
        });
        $("#ddlProvince").html(items);
        $("#ddlProvince").val(address.provinceCode);

        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetAmphur(provinceId);
        }
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetAmphur(provinceId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=amphur&province=" + provinceId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.amphur + `</option>
                `
        });
        $("#ddlAmphur").html(items);
        $("#ddlAmphur").val(address.amphurCode);
        var provinceId = $("#ddlProvince").val();
        var amphurId = $("#ddlAmphur").val().substr(2, 2);
        if (amphurId == "") {
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetTambon(provinceId, amphurId);
        }
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetTambon(provinceId, amphurId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=tambon&province=" + provinceId + "&amphur=" + amphurId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.tambon + `</option>
                `
        });
        $("#ddlTambon").html(items);
        $("#ddlTambon").val(address.tambonCode);
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

$("#frmAddEventNearby").on("submit", function (e) {

    console.log("frmAddEventNearby on submit");

    e.preventDefault();

    console.log("model.EventId", model.EventId);
    console.log("t_eventId", $("#t_eventId").val());

    AddEventNearbyToSession($("#t_eventId").val());
});

$(document).ready(function () {

    var eventId = routeId;
    console.log("eventId=", eventId);

    GetMEventFacilitiesTopic();
    GetEventNearbyFromSession(eventId);
    GetUploadedFile(eventId);
    GetProvince();

    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetAmphur(provinceId);
        }
    });

    $("#ddlAmphur").change(function () {
        var provinceId = $("#ddlProvince").val();
        var amphurId = $("#ddlAmphur").val().substr(2, 2);
        if (amphurId == "") {
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            GetTambon(provinceId, amphurId);
        }
    });

});

