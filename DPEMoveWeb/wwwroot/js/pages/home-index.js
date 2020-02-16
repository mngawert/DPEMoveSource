
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
        if (jqXHR.status == 200) {
            var data = response;
            var items = "";
            var totalEvents = 0;
            $.each(data, function (index, value) {
                items +=
                `
                    <tr>
                        <th scope="row">${value.provNamt}</th>
                        <td>${parseInt(value.noOfEvents).toLocaleString()}</td>
                    </tr>
                `
                totalEvents += parseInt(value.noOfEvents);
            });
            $("#tblReportEvent1 >tbody").html(items);
            $("#lblReportEvent1Count").html(totalEvents.toLocaleString());
        }
    });
}

function GetReportStadium1(token) {

    console.log("GetReportStadium1");

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

    return $.ajax(settings);

}

function PrintTableForReportStadium1(token) {

    GetReportStadium1(token).done(function (response, textStatus, jqXHR) {
        if (jqXHR.status == 200) {
            var data = JSON.parse(response).data;

            data.sort(function (a, b) { return b.TOTAL - a.TOTAL });
            var data_15 = data.slice(0, 15);
            var items = "";
            var sumTotal = 0;
            $.each(data_15, function (index, value) {
                items +=
                    `
                    <tr>
                        <th scope="row">${value.PROV_NAMT}</th>
                        <td>${parseInt(value.TOTAL).toLocaleString()}</td>
                    </tr>
                `;
                sumTotal += parseInt(value.TOTAL);
                reportData.push({ "PROV_NAMT": value.PROV_NAMT, "TOTAL": value.TOTAL });
            });
            $("#tblReportStadium1 >tbody").html(items);
            $("#tblReportStadium2 >tbody").html(items);
            $("#lblReportStadiumCount").html(sumTotal.toLocaleString());

            DrawGoogleMap(data);
        }
    });
}


function GetStadiumData() {
        $.ajax({
            method: "POST",
            url: "/Home/GetStadiumData",
            dataType: 'json',
            data: $("#pageForm-test").serialize(),
            error: function (jqXHR, exception) {
                //alert("error");
            }
        })
        .done(function (obj) {
            //alert(obj.row);

            if (obj.row > 0) {
                //alert(obj.data[0]["NAME_LABEL"]);
            }
        });
}

function GetReportSurvey151A(token) {
    var settings = {
        "url": "https://dpemove.dpe.go.th/api/Report/GetReportSurvey151A",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "createdDateFrom": "2020-01-01", "createdDateTo": "2099-01-01" }),
    };

    return $.ajax(settings);
}

function DrawChartForReportSurvey151A(token) {

    google.charts.load('current', { 'packages': ['bar'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportSurvey151A(token).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'กีฬา');
                data.addColumn('number', 'ระยะเวลา');

                $.each(response, function (index, value) {
                    data.addRow([value.sportName, value.sumAttr]);
                });

                console.log("GetReportSurvey151A data", data);

                var options = {
                    width: 750,
                    height: 300,
                    //legend: { position: 'none' },
                    chart: {
                        title: '',
                        subtitle: '',
                    }
                };

                var chart = new google.charts.Bar(document.getElementById('columnchart_material'));
                chart.draw(data, google.charts.Bar.convertOptions(options));
            }
        });
    }
}

function DrawGoogleMap(inputData) {

    google.charts.load('current', {
        'packages': ['geochart'],
        // Note: you will need to get a mapsApiKey for your project.
        // See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
        'mapsApiKey': 'AIzaSyDBro62OhioE6oXZ97CV8Y4AnrzfVIt4HA'
    });
    google.charts.setOnLoadCallback(drawRegionsMap);

    function drawRegionsMap() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'จังหวัด');
        data.addColumn('number', 'จำนวนสนามกีฬา');

        $.each(inputData, function (index, value) {
            data.addRow([value.PROV_NAMT, parseInt(value.TOTAL)]);
        });

        var options = {
            width: 800,
            height: 550,
            region: 'TH',
            resolution: "provinces",
            keepAspectRatio: false,
            colorAxis: { colors: ['green'] },
            legend: 'center'
        };

        var chart = new google.visualization.GeoChart(document.getElementById('regions_div'));
        google.visualization.events.addOneTimeListener(chart, 'ready', fixToolTipPosition);
        chart.draw(data, options);
    }
}

function fixToolTipPosition() {
    var container = $('#regions_div div:last-child ')[0];

    function setPosition(e) {
        var tooltip = $('.google-visualization-tooltip');
        var left = mouse.x - tooltip.width() / 2;
        var top = mouse.y - tooltip.height() - 15;
        tooltip.css('left', left + 'px');
        tooltip.css('top', top + 'px');
    }

    if (typeof MutationObserver === 'function') {
        var observer = new MutationObserver(function (m) {
            alert();
            setPosition(m);
        });
        observer.observe(container, {
            childList: true
        });
    } else if (document.addEventListener) {
        container.addEventListener('DOMNodeInserted', setPosition);
    } else {
        container.attachEvent('onDOMNodeInserted', setPosition);
    }
}



$(document).ready(function () {

    GetInternalToken().done(function (response) {
        var token = response;
        localStorage.setItem("token", token);

        GetReportEvent1(token);
        DrawChartForReportSurvey151A(token);
    });

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;
        PrintTableForReportStadium1(token);
    });

    //GetStadiumData();

});

