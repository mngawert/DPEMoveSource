
function GetInternalToken() {

    var settings = {
        "url": "/api/Account/GetReadOnlyToken",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
    };

    return $.ajax(settings);
}

function GetToken() {
    var settings = {
        "url": "/api/Account/GetDPEToken",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
    };

    return $.ajax(settings);
}

function GetReportEvent1(token) {

    var settings = {
        "url": "/api/Report/GetReportEvent1",
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
                        <th scope="row">${value.provNamt == null ? "-" : value.provNamt}</th>
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

function GetReportEvent3(token) {

    var d = new Date();
    d = new Date(d.getTime() - 3000000);
    var date_format_str = d.getFullYear().toString() + "-" + ((d.getMonth() + 1).toString().length == 2 ? (d.getMonth() + 1).toString() : "0" + (d.getMonth() + 1).toString()) + "-" + (d.getDate().toString().length == 2 ? d.getDate().toString() : "0" + d.getDate().toString());
    console.log(date_format_str);

    var settings = {
        "url": "/api/Report/GetReportEvent3",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "eventDateFrom": date_format_str, "eventDateTo": "2099-12-31" }),
    };

    console.log("sysdate", new Date());

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        if (jqXHR.status == 200) {

            var data = response.slice(0, 6);

            const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
            const options_DAY = { day: 'numeric' };
            const options_MONTH_SHORT = { month: 'short' };
            const options_MONTH_LONG = { month: 'long' };
            var items = `
                    <div class="month_sec">${d.toLocaleDateString('th-TH', options_MONTH_LONG)}</div>
            `;
            $.each(data, function (index, value) {

                var mydate = new Date(value.eventStartDate);
                //console.log(mydate.toLocaleDateString('th-TH', options));

                items +=
                    `
                    <div class="day_sec">
                        <div class="row">
                            <div class="day_left">
                                <h3>${ mydate.toLocaleDateString('th-TH', options_DAY) }</h3>
                                <span>${ mydate.toLocaleDateString('th-TH', options_MONTH_SHORT) }</span >
                            </div>
                            <div class="detail_right">
                                <a href="/Events/Details/${value.eventId}">${value.eventName}...</a>
                                <div id="dvRating_${value.eventCode}" class="rating">
                                    <span class="fa fa-star"></span>
                                    <span class="fa fa-star"></span>
                                    <span class="fa fa-star"></span>
                                    <span class="fa fa-star"></span>
                                    <span class="fa fa-star"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
            });
            $("#dvReportEvent3").append(items);
            $("#dvReportEvent3").append(`<div class="readmore"><a href="/Events">read more &#xbb;</a></div>`);

            PrintVoteAvg(data);
        }
    });
}

function PrintVoteAvg(data) {

    //console.log("PrintVoteAvg");
    $.each(data, function (index, value) {
        GetVoteTotalAvg("1", value.eventCode);
    });
}

function GetVoteTotalAvg(voteOf, eventOrStadiumCode) {

    //console.log("GetVoteTotalAvg");
    var settings = {
        "url": "/WebApi/Votes/GetVoteTotalAvg",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    //console.log("settings", settings)

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        //console.log("GetVoteTotalAvg reponse", data);
        //var value = JSON.parse(response);

        if (jqXHR.status == 200) {
            var value = data;

            var item =
                `
                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
            `
            $("#dvRating_" + eventOrStadiumCode).html(item);
        }
    });
}


function GetReportEvent4(token) {

    var settings = {
        "url": "/api/Report/GetReportEvent4",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "eventDateFrom": "2020-01-01", "eventDateTo": "2099-12-31" }),
    };

    return $.ajax(settings);
}

function DrawChartForReportEvent4(token) {

    //google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportEvent4(token).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'กิจกรรม');
                data.addColumn('number', 'จำนวน');

                $.each(response, function (index, value) {

                    var sectionName = GetSectionNameById(value.sectionCatId);
                    data.addRow([sectionName, value.noOfEvents]);
                });

                var options = {
                    width: 750,
                    height: 400,
                    title: 'กิจกรรมกีฬาและนันทนาการในประเทศไทย'
                };

                var chart = new google.visualization.PieChart(document.getElementById('dvReportEvent4'));
                chart.draw(data, options);
            }
        });
    }
}

function GetReportEvent5(token, provinceCode) {

    console.log("calling GetReportEvent5: ", provinceCode);
    var settings = {
        "url": "/api/Report/GetReportEvent5",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "eventDateFrom": "2020-01-01", "eventDateTo": "2099-12-31", "provinceCode": provinceCode }),
    };

    return $.ajax(settings);
}

function DrawChartForReportEvent5(token, provinceCode) {

    //google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportEvent5(token, provinceCode).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'กิจกรรม');
                data.addColumn('number', 'จำนวน');

                $.each(response, function (index, value) {

                    var sectionName = GetSectionNameById(value.sectionCatId);
                    data.addRow([sectionName, value.noOfEvents]);
                });

                var options = {
                    width: 750,
                    height: 400,
                    title: 'ภาพรวมกิจกรรมกีฬาและนันทนาการ'
                };

                var chart = new google.visualization.PieChart(document.getElementById('dvReportEvent5'));
                chart.draw(data, options);
            }
        });
    }
}


function GetReportEvent6(token, provinceCode) {

    var settings = {
        "url": "/api/Report/GetReportEvent6",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "eventDateFrom": "2020-01-01", "eventDateTo": "2099-12-31", "provinceCode": provinceCode }),
    };

    return $.ajax(settings);
}

function DrawChartForReportEvent6(token, provinceCode) {

    //google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportEvent6(token, provinceCode).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'กิจกรรม');
                data.addColumn('number', 'จำนวน');

                $.each(response, function (index, value) {

                    data.addRow([value.eventLevelName.replace("โปรดระบุ", ""), value.noOfEvents]);
                });

                var options = {
                    width: 750,
                    height: 400,
                    title: 'สถิติกิจกรรมกีฬาและนันทนาการแบ่งตามระดับกิจกรรม'
                };

                var chart = new google.visualization.PieChart(document.getElementById('dvReportEvent6'));
                chart.draw(data, options);
            }
        });
    }
}


function GetReportEvent7(token, provinceCode) {

    var settings = {
        "url": "/api/Report/GetReportEvent7",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "eventDateFrom": "2020-01-01", "eventDateTo": "2099-12-31", "provinceCode": provinceCode }),
    };

    return $.ajax(settings);
}

function DrawChartForReportEvent7(token, provinceCode) {

    //google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportEvent7(token, provinceCode).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'กิจกรรม');
                data.addColumn('number', 'จำนวน');

                $.each(response, function (index, value) {

                    data.addRow([value.eventObjectiveName.replace("โปรดระบุ", ""), value.noOfEvents]);
                });

                var options = {
                    width: 750,
                    height: 400,
                    title: 'สถิติกิจกรรมกีฬาและนันทนาการแบ่งตามกลุ่มเป้าหมายกิจกรรม'
                };

                var chart = new google.visualization.PieChart(document.getElementById('dvReportEvent7'));
                chart.draw(data, options);
            }
        });
    }
}


function GetReportEvent8(token, provinceCode) {
    var settings = {
        "url": "/api/Report/GetReportEvent8",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        "data": JSON.stringify({ "createdDateFrom": "2020-01-01", "createdDateTo": "2099-01-01", "provinceCode": provinceCode }),
    };

    return $.ajax(settings);
}

function DrawChartForReportEvent8(token, provinceCode) {

    //google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        GetReportEvent8(token, provinceCode).done(function (response, textStatus, jqXHR) {
            if (jqXHR.status == 200) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', 'ชนิดกีฬา');
                data.addColumn('number', 'จำนวนกิจกรรม');

                var data_10 = response.slice(0, 10);
                $.each(data_10, function (index, value) {

                    var activityName = GetActivityNameById(value.actTypeId);
                    activityName = activityName.replace("โปรดระบุ", "")
                    console.log("activityName", activityName);

                    data.addRow([activityName, value.noOfEvents]);
                });

                var options = {
                    width: 750,
                    height: 400,
                    chartArea: { width: '50%' },
                    //legend: { position: 'none' },
                    //bar: { groupWidth: "95%" },
                    colors: [ '#7CCC4E' ],
                    title: 'สถิติกิจกรรมกีฬาและนันทนาการแบ่งประเภทกีฬา',
                    vAxis: {
                        title: '',
                        textstyle: {
                            fontsize: 14,
                            bold: true,
                            color: '#848484'
                        },
                        titletextstyle: {
                            fontsize: 14,
                            bold: true,
                            color: '#848484'
                        }
                    }
                };

                var chart = new google.visualization.BarChart(document.getElementById("dvReportEvent8"));
                chart.draw(data, options);
            }
        });
    }
}

function GetReportSurvey151A(token) {
    var settings = {
        "url": "/api/Report/GetReportSurvey151A",
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

    //google.charts.load('current', { 'packages': ['corechart'] });
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
                    height: 400,
                    //legend: { position: 'none' },
                    //bar: { groupWidth: "70%" },
                    title: 'พฤติกรรมการออกกำลังกาย',
                    chartArea: { width: '50%' },
                    //vAxis: {
                    //    title: '',
                    //    textStyle: {
                    //        fontSize: 14,
                    //        bold: true,
                    //        color: '#848484'
                    //    },
                    //    titleTextStyle: {
                    //        fontSize: 14,
                    //        bold: true,
                    //        color: '#848484'
                    //    }
                    //}
                };

                //var chart = new google.charts.Bar(document.getElementById('columnchart_material'));
                //chart.draw(data, google.charts.Bar.convertOptions(options));

                var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
                chart.draw(data, options);
            }
        });
    }
}

function DrawGoogleMap(inputData) {

    //google.charts.load('current', {'packages': ['geochart'], 'mapsApiKey': 'AIzaSyDBro62OhioE6oXZ97CV8Y4AnrzfVIt4HA'});
    google.charts.setOnLoadCallback(drawRegionsMap);

    function drawRegionsMap() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'จังหวัด');
        data.addColumn('number', 'สนามกีฬา');

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

function GetNews() {
    var settings = {
        "url": "/api/RSS/GetNews",
        "method": "GET",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        if (jqXHR.status == 200) {

            var xxml = $.parseXML(response),
                $xxml = $(xxml),
                $test = $xxml.find('item');

            var index = 0;
            //var $xml = $(response);
            $xxml.find("item").each(function () {
                var $this = $(this),
                    item = {
                        title: $this.find("title").text(),
                        link: $this.find("link").text(),
                        description: $this.find("description").text(),
                        pubDate: $this.find("pubDate").text(),
                        author: $this.find("author").text(),
                        header: $this.find("header").text,
                    }

                $("#dvNews").append(`
                                <div class="carousel-item ${ index == 0 ? "active" : "" }">
                                    <img class="d-block img-fluid" src="${ $(item.description).attr("src") }" alt="First slide">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h4>${item.title}</h4>
                                        <a target="_blank" href="${item.link}">read more</a>
                                    </div>
                                </div>
                `);

                index = index + 1;
                if (index >= 3) {
                    return false;
                }
            });

            //$.each(data, function (index, value) {

            //    console.log(value.title);
            //});
        }
    });
}

function GetStadium(token) {

    var form = new FormData();
    form.append("PAGE", "1");
    form.append("PROV_CODE", "10");
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/address/getStadium",
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
            var items = "";
            $.each(data, function (index, value) {
                items +=
                    `
                    <div class="item">
                        <div class="pad15">
                            <a href="/Stadium/Details/${value.STADIUM_ID}"><img src="${value.COVER_IMG}" width="500" height="375" alt="" /></a>
                            <p class="lead">${value.NAME_LABEL}</p>
                        </div>
                    </div>
                `;
            });
            //console.log("items", items);
            $("#dvStaidum").html(items);

            console.log("2)ResCarouselSize");
            ResCarouselSize();
        }
    });
}

function GetProvince(token) {

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
            $("[name='Province']").html(items);
        }
    });
}

function GetSection(token) {

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
            response = response.replace(/\ufeff/g, ''); //Remove BOM character
            var results = JSON.parse(response);
            SECTION_DATA = results.data;

            console.log("SECTION_DATA", SECTION_DATA);

            DrawChartForReportEvent4(localStorage.getItem("token"));
            DrawChartForReportEvent5(localStorage.getItem("token"), $("#ddlProvinceForReportEvent5").val());
            DrawChartForReportEvent6(localStorage.getItem("token"), $("#ddlProvinceForReportEvent6").val());
            DrawChartForReportEvent7(localStorage.getItem("token"), $("#ddlProvinceForReportEvent7").val());
            DrawChartForReportSurvey151A(localStorage.getItem("token"));
        }
    });
}

function GetSectionNameById(id) {

    if (id == null)
        return "";

    var sectionName = "n/a";
    $.each(SECTION_DATA, function (index, value) {
        if (value.SECTION_CAT_ID == id) {
            sectionName = value.SECTION_CAT_NAME;
            return false;
        }
    });
    return sectionName;
}

function GetActivityType(token, SECTION_CAT_ID) {
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
            response = response.replace(/\ufeff/g, ''); //Remove BOM character
            var results = JSON.parse(response);
            ACTIVITY_DATA = results.data;

            DrawChartForReportEvent8(localStorage.getItem("token"), $("#ddlProvinceForReportEvent8").val());
        }
    });
}

function GetActivityNameById(id) {

    if (id == null)
        return "";

    var activityName = "n/a";
    $.each(ACTIVITY_DATA, function (index, value) {
        if (value.ACT_TYPE_ID == id) {
            activityName = value.ACT_TYPE_NAME;
            return false;
        }
    });
    return activityName;
}

$(document).ready(function () {

    var SECTION_DATA = {}
    var ACTIVITY_DATA = {}

    GetInternalToken().done(function (resp1) {
        var token1 = resp1;
        localStorage.setItem("token", token1);

        GetReportEvent1(token1);
        //DrawChartForReportSurvey151A(token1);
        //DrawChartForReportEvent4(token1);
        GetReportEvent3(token1);
        GetStadium(token1);

        GetToken().done(function (resp2) {
            var token2 = JSON.parse(resp2).data;
            PrintTableForReportStadium1(token2);
            GetProvince(token2);
            GetSection(token2);
            GetActivityType(token2, 1);
        });
    });

    GetNews();
    //GetStadiumData();

    $("#ddlProvinceForReportEvent5").change(function () {
        DrawChartForReportEvent5(localStorage.getItem("token"), $(this).val());
    });

    $("#ddlProvinceForReportEvent6").change(function () {
        DrawChartForReportEvent6(localStorage.getItem("token"), $(this).val());
    });

    $("#ddlProvinceForReportEvent7").change(function () {
        DrawChartForReportEvent7(localStorage.getItem("token"), $(this).val());
    });

    $("#ddlProvinceForReportEvent8").change(function () {
        DrawChartForReportEvent8(localStorage.getItem("token"), $(this).val());
    });

});

