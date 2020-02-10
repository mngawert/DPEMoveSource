
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

    if (!$("#frmAddEventFacilities_" + mEventFacilitiesTopicId)[0].checkValidity()) {

        $("#frmAddEventFacilities_" + mEventFacilitiesTopicId)[0].reportValidity()
        return false;
    }
    

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
        $("#ModalTopic_" + mEventFacilitiesTopicId).modal("toggle");

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





function GetMFee() {

    console.log('start GetMFee');
    var options = {};

    options.url = "/webapi/Events/GetMFee";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (data) {
        $.each(data, function (index, value) {

            var obj = {};
            obj.eventId = routeId;
            obj.feeId = value.feeId;
            options.data = JSON.stringify(obj);
            console.log("input", options.data);

            GetEventFeeFromSession(obj)
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetEventFeeFromSession(obj) {

    console.log('start GetEventFeeFromSession');
    var options = {};
    var input = {};
    input.eventId = obj.eventId;
    input.feeId = obj.feeId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/GetEventFeeFromSession";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {
            items +=
                `
                <tr>
                    <td>` + value.eventFeeName + `</td>
                    <td>` + (value.eventFeeAmount == null ? "" : value.eventFeeAmount) + `</td>
                    <td>` + (value.eventFeeUnit == null ? "" : value.eventFeeUnit) + `</td>
                    <td class="center"><button type="button" onclick="DeleteEventFeeFromSession(` + value.eventId + `,` + value.feeId + `,` + value.eventFeeId + `)" class="button small red">&nbsp;ลบ&nbsp;</button></td>
                </tr>
                `
        });

        $("#tblFee_" + obj.feeId + " > tbody").html(items);

        var rowCount = $('#tblFee_5001 >tbody >tr').length + $('#tblFee_5002 >tbody >tr').length;
        if (rowCount > 0) {
            $('input[name="IsFree"]').prop("checked", false);
        }
        else {
            $('input[name="IsFree"]').prop("checked", true);
        }

    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function AddEventFeeToSession(eventId, feeId) {
    console.log('start AddEventFeeToSession ');

    if (!$("#frmAddEventFee_" + feeId)[0].checkValidity()) {

        $("#frmAddEventFee_" + feeId)[0].reportValidity()
        return false;
    }

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.feeId = feeId;
    input.eventFeeName = $("#txtEventFeeName_" + feeId).val();
    input.eventFeeAmount = $("#txtEventFeeAmount_" + feeId).val();
    input.eventFeeUnit = $("#txtEventFeeUnit_" + feeId).val();
    input.createdBy = "0";
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    // clear texbox.
    $("#txtEventFeeName_" + feeId).val("");
    $("#txtEventFeeAmount_" + feeId).val("");
    $("#txtEventFeeUnit_" + feeId).val("");

    options.url = "/webapi/Events/AddEventFeeToSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        $("#ModalFee_" + feeId).modal("toggle");
        // re-load data from session.
        var obj = {};
        obj.eventId = eventId;
        obj.feeId = feeId;
        GetEventFeeFromSession(obj);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function DeleteEventFeeFromSession(eventId, feeId, eventFeeId) {
    console.log('start DeleteEventFeeFromSession');

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.feeId = feeId;
    input.eventFeeId = eventFeeId;

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/DeleteEventFeeFromSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        var obj = {};
        obj.eventId = eventId;
        obj.feeId = feeId;
        GetEventFeeFromSession(obj);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}



function GetMParticipant() {

    console.log('start GetMParticipant');
    var options = {};

    options.url = "/webapi/Events/GetMParticipant";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (data) {
        $.each(data, function (index, value) {

            var obj = {};
            obj.eventId = routeId;
            obj.participantId = value.participantId;
            options.data = JSON.stringify(obj);
            console.log("input", options.data);

            GetEventParticipantFromSession(obj)
        });
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetEventParticipantFromSession(obj) {

    console.log('start GetEventParticipantFromSession');
    var options = {};
    var input = {};
    input.eventId = obj.eventId;
    input.participantId = obj.participantId;
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/GetEventParticipantFromSession";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {
            items +=
                `
                <tr>
                    <td>` + value.eventParticipantName + `</td>
                    <td>` + (value.eventParticipantAmount == null ? "" : value.eventParticipantAmount) + `</td>
                    <td>` + (value.eventParticipantUnit == null ? "" : value.eventParticipantUnit) + `</td>
                    <td class="center"><button type="button" onclick="DeleteEventParticipantFromSession(` + value.eventId + `,` + value.participantId + `,` + value.eventParticipantId + `)" class="button small red">&nbsp;ลบ&nbsp;</button></td>
                </tr>
                `
        });

        $("#tblParticipant_" + obj.participantId + " > tbody").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function AddEventParticipantToSession(eventId, participantId) {
    console.log('start AddEventParticipantToSession ');

    if (!$("#frmAddEventParticipant_" + participantId)[0].checkValidity()) {

        $("#frmAddEventParticipant_" + participantId)[0].reportValidity()
        return false;
    }

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.participantId = participantId;
    input.eventParticipantName = $("#txtEventParticipantName_" + participantId).val();
    input.eventParticipantAmount = $("#txtEventParticipantAmount_" + participantId).val();
    input.eventParticipantUnit = $("#txtEventParticipantUnit_" + participantId).val();
    input.createdBy = "0";
    options.data = JSON.stringify(input);
    console.log("input", options.data);

    // clear texbox.
    $("#txtEventParticipantName_" + participantId).val("");
    $("#txtEventParticipantAmount_" + participantId).val("");
    $("#txtEventParticipantUnit_" + participantId).val("");

    options.url = "/webapi/Events/AddEventParticipantToSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        $("#ModalParticipant_" + participantId).modal("toggle");
        // re-load data from session.
        var obj = {};
        obj.eventId = eventId;
        obj.participantId = participantId;
        GetEventParticipantFromSession(obj);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function DeleteEventParticipantFromSession(eventId, participantId, eventParticipantId) {
    console.log('start DeleteEventParticipantFromSession');

    var options = {};

    var input = {};
    input.eventId = eventId;
    input.participantId = participantId;
    input.eventParticipantId = eventParticipantId;

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Events/DeleteEventParticipantFromSession";
    options.contentType = "application/json";
    options.method = "POST";
    options.success = function (data) {
        console.log("success add");
        var obj = {};
        obj.eventId = eventId;
        obj.participantId = participantId;
        GetEventParticipantFromSession(obj);
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

function GetAddressFromDatabase(token) {

    console.log("GetAddressFromDatabase");
    console.log("address.provinceCode", address.provinceCode);
    console.log("address.amphurCode", address.amphurCode);
    console.log("address.tambonCode", address.tambonCode);

    // load all province and selected value
    GetProvince(token, address.provinceCode);

    // load Amphur
    if (address.provinceCode == null) {
        $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    }
    else {
        GetAmphur(token, address.provinceCode, address.amphurCode);
    }

    // load Tambon
    if (address.amphurCode == null) {
        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    }
    else {
        GetTambon(token, address.provinceCode, address.amphurCode, address.tambonCode);
    }
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

function GetProvince(token, selectedProvince) {

    console.log("call GetProvince");
    var form = new FormData();
    form.append("Token", token);

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

            if (selectedProvince != null) {
                $("#ddlProvince").val(selectedProvince);
            }
        }
    });
}

function GetAmphur(token, PROV_CODE, selectedAmphur) {


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

            if (selectedAmphur != null) {
                $("#ddlAmphur").val(selectedAmphur);
            }
        }
    });
}

function GetTambon(token, PROV_CODE, AMP_CODE, selectedTambon) {

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

            if (selectedTambon != null) {
                $("#ddlTambon").val(selectedTambon);
            }
        }
    });
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

function GetSection(token, selectedSection) {

    var form = new FormData();
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/activity/section/getSection",
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
            var items = `<option value="">กรุณาเลือก</option>`;
            $.each(data, function (index, value) {
                items +=
                    `
                <option value="` + value.SECTION_CAT_ID + `">` + value.SECTION_CAT_NAME + `</option>
                `
            });
            $("#ddlSection").html(items);

            if (selectedSection != null) {
                $("#ddlSection").val(selectedSection);
                GetActivityType(token, selectedSection, model.actTypeId);

                if (selectedSection != "0") {
                    $("[name='SectionCatEtc']").val("");
                    $("[name='SectionCatEtc']").hide();
                }
                else {
                    $("[name='SectionCatEtc']").show();
                }
            }
        }
    });
}

function GetActivityType(token, SECTION_CAT_ID, selectedActivityType) {
    var form = new FormData();
    form.append("Token", token);
    form.append("SECTION_CAT_ID", SECTION_CAT_ID);

    var settings = {
        "url": "https://data.dpe.go.th/api/activity/type/getActivityType",
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
            var items = ``;

            $.each(data, function (index, value) {
                items +=
                `
                    <div class="col-3">
                        <label><input type="radio" name="ActTypeId" value="${value.ACT_TYPE_ID}" class="input-field" /> ${value.ACT_TYPE_NAME}</label>
                    </div>                
                `
            });
            $("#dvActivityType > .row").html(items);

            if (selectedActivityType != null) {
                $("[name='ActTypeId']").each(function () {
                    if ($(this).val() == selectedActivityType) {
                        $(this).prop("checked", true);
                    }
                });
            }
        }
    });
}

$(document).ready(function () {

    var eventId = routeId;
    console.log("eventId=", eventId);
    console.log("model.sectionCatId", model.sectionCatId);
    console.log("model.sectionCatEtc", model.sectionCatEtc);
    console.log("model.actTypeId", model.actTypeId);
    console.log("model.actTypeEtc", model.actTypeEtc);

    GetMEventFacilitiesTopic();
    GetMFee();
    GetMParticipant();
    GetEventNearbyFromSession(eventId);
    GetUploadedFile(eventId);

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetAddressFromDatabase(token);
        GetSection(token, model.sectionCatId);
    });

    $("#ddlProvince").change(function () {
        var provinceId = $("#ddlProvince").val();
        if (provinceId == "") {
            $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
            $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
        }
        else {
            var token = localStorage.getItem("token");
            GetAmphur(token, provinceId, null);
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
            GetTambon(token, provinceId, amphurId, null);
        }
    });

    $("#ddlSection").change(function () {
        var selectedSection = $("#ddlSection").val();
        if (selectedSection != "0") {
            $("[name='SectionCatEtc']").val("");
            $("[name='SectionCatEtc']").hide();
        }
        else {
            $("[name='SectionCatEtc']").show();
        }

        var token = localStorage.getItem("token");
        GetActivityType(token, selectedSection, null);
    });

});

