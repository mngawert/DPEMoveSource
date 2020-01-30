
function GetToken(email, password) {

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

function GetReportEvent1(token) {
    var settings = {
        "url": "https://dpemove.dpe.go.th/api/Report/GetReportEvent1",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
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


$(document).ready(function () {


    var email = "readonly@gmail.com";
    var password = "Bossup2020";

    GetToken(email, password).done(function (response) {
        var token = response;
        localStorage.setItem("token", token);
        console.log("localStorage.token", localStorage.getItem("token"));

        GetReportEvent1(token);

    });
});

