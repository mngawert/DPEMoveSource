

function GetInternalToken(email, password) {

    var settings = {
        "url": "/api/Account/GetToken",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "email": email, "password": password }),
    };

    return $.ajax(settings);
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

function GetReportEvent1(token) {

    var settings = {
        "url": "https://dpemove.dpe.go.th/api/Report/GetReportEvent1",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        "data": JSON.stringify({ "eventDateFrom": "2020-01-01", "eventDateTo": "2099-12-31" }),
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        console.log(response);

        if (jqXHR.status == 200) {
            var data = response;
            var items = "";
            var totalEvents = 0;
            $.each(data, function (index, value) {
                items +=
                `
                    <tr>
                        <th scope="row">${value.provNamt}</th>
                        <td>${value.noOfEvents}</td>
                    </tr>
                `
                totalEvents += parseInt(value.noOfEvents);
            });
            $("#lblReportEvent1Count").html(`<strong>รวม ${totalEvents} กิจกรรม</strong>`);
            $("#tblReportEvent1 >tbody").html(items);
        }
    });
}

function GetReportStadium1(token) {

    var form = new FormData();
    form.append("Token", token);
    form.append("limit", "100");

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/report/getNumStadiumProv",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        if (jqXHR.status == 200) {
            var data = JSON.parse(response).data;
            data.sort(function (a, b) { return b.TOTAL - a.TOTAL });

            data = data.slice(0, 15);
            var items = "";
            $.each(data, function (index, value) {
                items +=
                    `
                    <tr>
                        <th scope="row">${value.PROV_NAMT}</th>
                        <td>${value.TOTAL}</td>
                    </tr>
                `
            });
            $("#tblReportStadium1 >tbody").html(items);
        }
    });
}


$(document).ready(function () {

    var email = "readonly@gmail.com";
    var password = "Bossup2020";

    GetInternalToken(email, password).done(function (response) {
        var token = response;
        localStorage.setItem("token", token);

        GetReportEvent1(token);
    });

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        GetReportStadium1(token);
    });
});

