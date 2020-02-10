

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
            var sumTotal = 0;
            $.each(data, function (index, value) {
                items +=
                    `
                    <tr>
                        <th scope="row">${value.PROV_NAMT}</th>
                        <td>${parseInt(value.TOTAL).toLocaleString()}</td>
                    </tr>
                `;
                sumTotal += parseInt(value.TOTAL);
                reportData.push({ "PROV_NAMT": value.PROV_NAMT, "TOTAL": value.TOTAL});
            });
            $("#tblReportStadium1 >tbody").html(items);
            $("#tblReportStadium2 >tbody").html(items);
            $("#lblReportStadiumCount").html(sumTotal.toLocaleString());

            //var dataTableData = google.visualization.arrayToDataTable(reportData);
            //console.log("dataTableData", dataTableData);
            //DrawGoogleMap();
        }
    });
}

function GetStadiumData() {
        $.ajax({
            method: "POST",
            url: "/Home/GetStadiumData",
            dataType: 'json',
            error: function (jqXHR, exception) {
                alert("error");
            }
        })
        .done(function (obj) {
            //alert(obj.row);

            if (obj.row > 0) {
                //alert(obj.data[0]["NAME_LABEL"]);
            }
        });
}

function DrawGoogleMap() {

    google.charts.load('current', {
        'packages': ['geochart'],
        // Note: you will need to get a mapsApiKey for your project.
        // See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
        'mapsApiKey': 'AIzaSyBW_ZloATWKeCRioFkeAQIaqTJErmD-IQA'
    });
    google.charts.setOnLoadCallback(drawRegionsMap);
}

function drawRegionsMap() {

    var data = google.visualization.arrayToDataTable([
        ['จังหวัด', 'จำนวนสนามกีฬา'],
        ['เชียงใหม่', 298],
        ['Chiang Rai', 393],
        ['Phetchaburi', 348],
        ['Phetchabun', 404],
        ['Loei', 719],
        ['Phrae', 169],
        ['Mae Hong Son', 234],
        ['Krabi', 429],
        ['Bangkok', 173],
        ['Kanchanaburi', 417],
        ['Kalasin', 579],
        ['Kamphaeng Phet', 352],
        ['Khon Kaen', 891],
        ['Chanthaburi', 112],
        ['Chachoengsao', 272],
        ['Chonburi', 391],
        ['Chai Nat', 472],
        ['Chaiyaphum', 1067],
        ['Chumphon', 362],
        ['Trang', 292],
        ['ตราด', 156],
        ['Tak', 211],
        ['Nakhon Nayok', 89],
        ['Nakhon Pathom', 863],
        ['Nakhon Phanom', 622],
        ['Nakhon Ratchasima', 2427],
        ['Nakhon Si Thammarat', 735],
        ['Nakhon Sawan', 879],
        ['Nonthaburi', 167],
        ['Narathiwat', 143],
        ['Nan', 154],
        ['Bueng Kan', 124],
        ['Buriram', 852],
        ['Pathum Thani', 144],
        ['Prachuap Khiri Khan', 172],
        ['Prachinburi', 431],
        ['Pattani', 139],
        ['Phra Nakhon Si Ayutthaya', 426],
        ['Phayao', 229],
        ['Phang Nga', 77],
        ['Phatthalung', 418],
        ['Phichit', 252],
        ['Phitsanulok', 759],
        ['Phuket', 53],
        ['Maha Sarakham', 389],
        ['Mukdahan', 156],
        ['Yasothon', 591],
        ['Yala', 109],
        ['Roi Et', 615],
        ['Ranong', 68],
        ['Rayong', 405],
        ['Ratchaburi', 211],
        ['Lopburi', 382],
        ['Lampang', 487],
        ['Lamphun', 154],
        ['Sisaket', 1535],
        ['Sakon Nakhon', 1668],
        ['Songkhla', 317],
        ['Satun', 109],
        ['Samut Prakan', 126],
        ['Samut Songkhram', 54],
        ['Samut Sakhon', 208],
        ['Sa Kaeo', 99],
        ['Saraburi', 347],
        ['Sing Buri', 149],
        ['Sukhothai (Sukhothai Thani)', 504],
        ['Suphan Buri', 311],
        ['Surat Thani', 347],
        ['Surin', 1533],
        ['Nong Khai', 185],
        ['Nong Bua Lam Phu', 439],
        ['Ang Thong', 141],
        ['Amnat Charoen', 569],
        ['Udon Thani', 1024],
        ['Uttaradit', 227],
        ['Uthai Thani', 231],
        ['Ubon Ratchathani', 1886]
    ]);

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

    GetStadiumData();
    DrawGoogleMap();
});

