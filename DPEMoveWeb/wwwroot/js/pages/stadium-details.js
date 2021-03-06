﻿
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

function GetStadiumDetails(token, id) {

    var form = new FormData();
    form.append("STADIUM_ID", id);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/address/getStadiumDetail",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        //console.log("response", response);
        var results = JSON.parse(response);
        var data = results.data;
        console.log("data", data);
        var items = '';
        $.each(data, function (index, value) {
            console.log('value', value);
            $("[name='lbl_NAME_LABEL']").html(value.NAME_LABEL)
            $("#lbl_ADDRESS").append(`${value.ADDRESS == null ? "" : value.ADDRESS} ${value.TAM_NAMT == null ? "" : value.TAM_NAMT} ${value.AMP_NAMT == null ? "" : value.AMP_NAMT} ${value.PROV_NAMT == null ? "" : value.PROV_NAMT} ${value.POST_NO == null ? "" : value.POST_NO}`);

            var goolemapUrl = `https://www.google.com/maps/embed/v1/place?key=AIzaSyDBro62OhioE6oXZ97CV8Y4AnrzfVIt4HA&language=th&q=` + value.LATITUDE + `,` + value.LONGITUDE + ``;
            $("#googlemap").attr("src", goolemapUrl);
            $("#btnOpenMap").attr("href", `https://www.google.com/maps/search/?api=1&query=${value.LATITUDE},${value.LONGITUDE}`);

            console.log("goolemapUrl", goolemapUrl);
            console.log("googlemap.src", $("#googlemap").attr("src"));

            $("#lbl_TELEPHONE").append(value.TELEPHONE == null ? " - " : value.TELEPHONE);
            $("#lbl_EMAIL").append(value.EMAIL == null ? " - " : value.EMAIL);
            $("#lbl_DIMENSION").append(value.DIMENSION == null ? " - " : value.DIMENSION);
            $("#lbl_TRANSPORT").append(value.TRANSPORT == null ? " - " : value.TRANSPORT);

            $("#lbl_OPEN_DETAIL").html(value.TIME_ == null ? " - " : value.OPEN_DETAIL);
            $("#lbl_START_TIME").html(value.START_TIME == null ? " - " : value.START_TIME);
            $("#lbl_END_TIME").html(value.END_TIME == null ? " - " : value.END_TIME);
            $("#lbl_TRANSPORT").html(value.TRANSPORT == null ? " - " : value.TRANSPORT);
            $("#dv_POLICY").html(value.POLICY == null ? " - " : value.POLICY);
            //$("#dv_AGREEMENT").html(value.AGREEMENT == null ? " - " : value.AGREEMENT);

            $("#lbl_BUILDING_BY").html(value.BUILDING_BY == null ? " - " : value.BUILDING_BY);
            $("#lbl_SUPPORT").html(value.SUPPORT == null ? " - " : value.SUPPORT);
            $("#lbl_ADDRESS_SUPPORT").html(value.ADDRESS_SUPPORT == null ? " - " : value.ADDRESS_SUPPORT);
            $("#lbl_SPORT_DESCRIPTION").html(value.SPORT_DESCRIPTION);
            $("#lbl_FIELD_EQUIPMENT").html(value.FIELD_EQUIPMENT == null ? " - " : value.FIELD_EQUIPMENT);
            $("#lbl_SPORT_EQUIPMENT").html(value.SPORT_EQUIPMENT == null ? " - " : value.SPORT_EQUIPMENT);
            $("#lbl_ILLUMINATION").html(value.ILLUMINATION == null ? " - " : value.ILLUMINATION);            

            $("#dv_CHARGES").html(value.CHARGES);
            $("[name='dv_ACCEPT_USER']").append(value.ACCEPT_USER);
            $("#dv_REGULATION").append(value.REGULATION);
                        
            var gallery = value.GALLERY;
            PrintGallery(gallery);

            var placeNear = value.PLACE_NEAR;
            PrintPlaceNear(placeNear);

            //var survey = value.SURVEY;
            PrintSurvey(token, id);

            if (value.UNDER_STADIUM_ID != null) {

                var tmp =
                    `
                        <a id="lnk_PARENT_STADIUM_` + value.UNDER_STADIUM_ID +`" href="/Stadium/Details/` + value.UNDER_STADIUM_ID + `">[1]</a>
                    `;
                $("#dv_UNDER_STADIUM_ID").html(tmp);

                PrintParentStadiumName(token, value.UNDER_STADIUM_ID);

            }

            if (value.UNDER_STADIUM.length > 0) {
                PrintUnderStadium(value.UNDER_STADIUM);
                PrintUnderStadiumName(token, value.UNDER_STADIUM);
            }


            var voteOf = "2";
            if (value.UNDER_STADIUM_ID != null) {
                voteOf = "3";

                $(".ForMainStadium").hide();
                $(".ForSubStadium").show();
            }
            else {
                $(".ForMainStadium").show();
                $(".ForSubStadium").hide();
            }

            $("[name='voteOf']").val(voteOf);

            GetVoteType(voteOf, value.STADIUM_ID);
            GetVoteAvg(voteOf, value.STADIUM_ID, appUserId);
            GetVoteTotalAvg(voteOf, value.STADIUM_ID);
            GetVoteTotalAvgDetails(voteOf, value.STADIUM_ID);
        });
    });
}

function PrintGallery(data) {
    console.log("PrintGallery");
    var item_1 = "";
    var item_2 = "";
    $.each(data, function (index, value) {
        //console.log(value);

        item_1 += `<li data-target="#carouselExampleIndicators" data-slide-to="` + index + `" ` + (index == 0 ? `class="active"` : "") + `></li>`;

        //console.log("item_1", item_1);

        item_2 += 
            `
            <div class="carousel-item `+ (index == 0 ? "active" : "") + `">
                <img class="d-block img-fluid" src="` + value +  `" alt="First slide">
            </div>
            `

        //console.log("item_2", item_2);
    });

    $("#indicator_GALLERY").html(item_1);
    $("#inner_GALLERY").html(item_2);
}

function PrintPlaceNear(data) {
    console.log("PrintGallery");
    var item_1 = "";
    $.each(data, function (index, value) {
        //console.log(value);

        var distance = "";
        if (value.DISTANCE != null) {
            distance = Number(value.DISTANCE).toFixed(2);
        }

        item_1 +=
            `
            <tr>
                <td><img src="/images/icon_pin.png" width="18" height="25"> ` + value.NAME_LABEL + `</td>
                <td>`+ distance + ` ` + value.UNIT_NAME + `</td>
            </tr>
            `
    });

    $("#tbl_PLACE_NEAR").html(item_1);
}

function PrintSurvey(token, id) {
    console.log("PrintSurvey");

    var form = new FormData();
    form.append("STADIUM_ID", id);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/survey/getSurvey",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response);

        var results = JSON.parse(response);
        console.log("test", results);
        var data_0 = results.data;
        var item1 = "";
        var item3 = "";
        var sumPoint = 0;
        var item_icon = "";

        $.each(data_0, function (index_0, value_0) {

            if (value_0.SURV_TYPE_ID == "1" || value_0.SURV_TYPE_ID == "2") {

                var data = value_0.SURV_DETL;
                $.each(data, function (index, value) {
                    item_icon +=
                        `
                        <div class="service_sec-box">
                            <img src="` + value.ICON + `" width="74" height="74">
                            <p>` + value.SURV_DETL_NAME + `</p>
                        </div>
                        `;
                    item1 +=
                        `
                        <div class="card_list">
                            <img src="` + value.ICON + `" width="80" height="78" alt="" />
                            <h4>` + value.SURV_DETL_NAME + `</h4>
                            <br />
                            <br />
                            <br />
                        </div>
                        `;
                });
            }
            else if (value_0.SURV_TYPE_ID == "3") {

                var data = value_0.SURV_DETL;
                $.each(data, function (index, value) {
                    item3 +=
                        `
                        <div class="card_list">
                            <img src="` + value.ICON + `" width="80" height="78" alt="" />
                            <h4>` + value.SURV_DETL_NAME + `</h4>
                            <br />
                            <br />
                            <br />
                        </div>
                        `;
                });
            }

        });

        $("#dv_facility_ICON").prepend(item_icon);
        $("#dv_SURVEY_2").html(item1);
        $("#dv_SURV_TYPE_ID_3").html(item3);        
    });

}

function PrintUnderStadium(data) {
    console.log("PrintUnderStadium");
    var item_1 = "";
    $.each(data, function (index, value) {
        console.log(value);
        item_1 +=
        `
            <a id="lnk_UNDER_STADIUM_` + value + `" href="/Stadium/Details/` + value + `">[` + value + `]</a>
        `
    });

    $("#dv_UNDER_STADIUM").html(item_1);
}

function PrintParentStadiumName(token, id) {

    var form = new FormData();
    form.append("STADIUM_ID", id);
    form.append("Token", token);

    var settings = {
        "url": "https://data.dpe.go.th/api/stadium/address/getStadiumDetail",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form
    };

    $.ajax(settings).done(function (response) {


        var results = JSON.parse(response);
        console.log("test", results);
        var data = results.data;
        var item_1 = ``;
        $.each(data, function (index, value) {
            //var item_1 =
            //    `
            //        <div class="row">
            //            <img src="` + value.GALLERY[0] + `" width="74" height="74">
            //        </div>
            //        <div class="row">
            //            <p>` + value.NAME_LABEL + `</p>
            //        </div>
            //    `
            //$("#lnk_PARENT_STADIUM_" + id).html(item_1)

            item_1 += 
                `
                <a href="/Stadium/Details/` + value.STADIUM_ID + `">` + value.NAME_LABEL + `</a>
                `
            $("#dv_PARENT_STADIUM").html(item_1);

            // if parent has child stadium then print child as well.
            if (value.UNDER_STADIUM.length > 0) {
                PrintUnderStadium(value.UNDER_STADIUM);
                PrintUnderStadiumName(token, value.UNDER_STADIUM);
            }
        });
    });
}

function PrintUnderStadiumName(token, data) {

    $.each(data, function (ii, vv) {

        var id = vv;
        var form = new FormData();
        form.append("STADIUM_ID", id);
        form.append("Token", token);

        var settings = {
            "url": "https://data.dpe.go.th/api/stadium/address/getStadiumDetail",
            "method": "POST",
            "timeout": 0,
            "processData": false,
            "mimeType": "multipart/form-data",
            "contentType": false,
            "data": form
        };

        $.ajax(settings).done(function (response) {
            var results = JSON.parse(response);
            var data = results.data;
            $.each(data, function (index, value) {
                var item_1 = 
                    `
                    <div class="row">
                        <div class="col-4">
                            <img src="` + value.GALLERY[0] + `" width="74" height="74">
                        </div>
                        <div class="col-8">
                            ` + value.NAME_LABEL + `
                        </div>
                    </div>
                    `
                $("#lnk_UNDER_STADIUM_" + id).html(item_1)
            });
        });
    });
}

function GetCommentsByStadiumId(stadiumId) {

    console.log('start GetCommentsByStadiumId');
    var options = {};

    options.url = "/webapi/Stadium/GetCommentsByStadiumId/" + stadiumId;
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

function AddComment(stadiumId) {

    console.log('start AddComment');

    if ($("#txtComment").val() == "")
        return false;

    var options = {};

    var input = {};
    input.commentOf = "2";
    input.eventOrStadiumCode = stadiumId;
    input.comment1 = $("#txtComment").val();

    options.data = JSON.stringify(input);
    console.log("input", options.data);

    options.url = "/webapi/Stadium/AddComment";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        console.log("AddComment success");
        GetCommentsByStadiumId(stadiumId);
        //Clear
        $("#txtComment").val("");
        $("#collapseFour4").collapse('show'); // toggle collapse
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetVoteType(voteOf, stadiumId) {

    console.log("GetVoteType");

    var settings = {
        "url": "/WebApi/Votes/GetVoteType",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({ "voteOf": voteOf }),
    };

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        console.log(data);
        console.log("jqXHR.status", jqXHR.status);

        if (jqXHR.status == 200) {

            var items = ``;
            $.each(data, function (index, value) {
                items += `
                    <div class="row">
                        <div class="col-12">
                            <p> ${value.voteType}</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <p class="rating">
                                <input type="radio" name="VoteValue_${value.voteTypeId}" value="5">
                                <img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt="">
                            </p>
                        </div>
                        <div class="col-2">
                            <p class="rating">
                                <input type="radio" name="VoteValue_${value.voteTypeId}" value="4">
                                <img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt="">
                            </p>
                        </div>
                        <div class="col-2">
                            <p class="rating">
                                <input type="radio" name="VoteValue_${value.voteTypeId}" value="3">
                                <img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt="">
                            </p>
                        </div>
                        <div class="col-2">
                            <p class="rating">
                                <input type="radio" name="VoteValue_${value.voteTypeId}" value="2">
                                <img src="/images/ic_star.png" alt=""><img src="/images/ic_star.png" alt="">
                            </p>
                        </div>
                        <div class="col-2">
                            <p class="rating">
                                <input type="radio" name="VoteValue_${value.voteTypeId}" value="1">
                                <img src="/images/ic_star.png" alt="">
                            </p>
                        </div>
                    </div>
                `;

                $("#dvPopupVoteType").html(items);
                GetVote(voteOf, stadiumId, value.voteTypeId, appUserId)
            });
        }
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

    console.log("settings", settings);

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {
            var value = data;
            $(`[name='VoteValue_${voteTypeId}']`).each(function () {
                if (this.value == value.voteValue) {
                    this.checked = true;
                }
            });
        }
    });
}

function GetVoteAvg(voteOf, eventOrStadiumCode, createdBy) {

    console.log("GetVoteAvg");
    var settings = {
        "url": "/WebApi/Votes/GetVoteAvg",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode, "createdBy": createdBy }),
    };

    console.log("settings", settings)

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        console.log(data);

        console.log("GetVoteAvg response", data);
        console.log("jqXHR.status", jqXHR.status);

        if (jqXHR.status == 200) {
            //var value = data;
            //if (value.voteAvg >= 1)
            //    $("#dv_VoteValue").html(`<img src="/images/ic_star.png" alt="">`);
            //if (value.voteAvg >= 2)
            //    $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            //if (value.voteAvg >= 3)
            //    $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            //if (value.voteAvg >= 4)
            //    $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);
            //if (value.voteAvg >= 5)
            //    $("#dv_VoteValue").append(`<img src="/images/ic_star.png" alt="">`);

            var value = data;
            var item =
                `
                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
            `
            $("#dv_MyRating").html(item);
        }
    });
}

function GetVoteTotalAvg(voteOf, eventOrStadiumCode) {

    console.log("GetVoteTotalAvg");
    var settings = {
        "url": "/WebApi/Votes/GetVoteTotalAvg",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    console.log("settings", settings)

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        console.log("GetVoteTotalAvg reponse", data);
        //var value = JSON.parse(response);

        if (jqXHR.status == 200) {
            var value = data;
            $("#lbl_VoteAvg").html(value.voteAvg == null ? "-" : value.voteAvg);
            $("#lbl_VoteText").html(value.voteText == null ? "-" : value.voteText);
            $("#lbl_SURVEY_POINT_AVG").html(value.voteAvg == null ? "-" : value.voteAvg);
            $("#lbl_SURVEY_POINT_TXT").html(value.voteText == null ? "-" : value.voteText);
            $(".rating_box").css("background", value.ratingColor);
        }
    });
}

function GetVoteTotalAvgDetails(voteOf, eventOrStadiumCode) {

    console.log("GetVoteTotalAvgDetails");
    var settings = {
        "url": "/WebApi/Votes/GetVoteTotalAvgDetails",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "voteOf": voteOf, "eventOrStadiumCode": eventOrStadiumCode }),
    };

    console.log("settings", settings)

    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        if (jqXHR.status == 200) {

            var item_1A = "";
            var item_1B = "";

            $.each(data, function (index, value) {
                //console.log(value);

                var POINT = (value.voteAvg / 5) * 100;
                console.log("value.voteAvg", value.voteAvg);
                console.log("POINT", POINT);

                // limit display to 8 items
                if (index < data.length / 2) {
                    item_1A +=
                        `
                <h3>` + value.voteType + `</h3>
                <div class="row">
                    <div class="col-sm-10 col-md-10 col-lg-10">
                        <div class="progress">
                            <div class="progress-bar progress-bar-success" role="progressbar" style="width: ${POINT}%; background-color:${value.ratingColor}" aria-valuenow="` + POINT + `" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                    <div class="col-sm-2col-md-2 col-lg-2">
                        <h5>` + value.voteAvg + `</h5>
                    </div>
                </div>`;
                }
                else if (index < 8) {
                    item_1B +=
                        `
                <h3>` + value.voteType + `</h3>
                <div class="row">
                    <div class="col-sm-10 col-md-10 col-lg-10">
                        <div class="progress">
                            <div class="progress-bar progress-bar-success" role="progressbar" style="width: ${POINT}%; background-color:${value.ratingColor}" aria-valuenow="` + POINT + `" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                    <div class="col-sm-2col-md-2 col-lg-2">
                        <h5>` + value.voteAvg + `</h5>
                    </div>
                </div>`;
                }
            });

            $("#dv_SURVEY_1A").html(item_1A);
            $("#dv_SURVEY_1B").html(item_1B);
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

    $.ajax(settings).done(function (data, textStatus, jqXHR) {

        if (jqXHR.status == 200) {

            GetVote(voteOf, eventOrStadiumCode, voteTypeId, createdBy);
            GetVoteAvg(voteOf, eventOrStadiumCode, createdBy);
            GetVoteTotalAvg(voteOf, eventOrStadiumCode);
            GetVoteTotalAvgDetails(voteOf, eventOrStadiumCode);
        }
    });
}


$(document).ready(function () {

    var stadiumId = routeId;
    console.log("stadiumId=", stadiumId);

    GetToken().done(function (response) {
        var token = JSON.parse(response).data;

        GetStadiumDetails(token, stadiumId);
    });

    GetCommentsByStadiumId(stadiumId);

    $("#lnkGotoFacility").click(function () {
        $("#collapseOne1").collapse('show'); // toggle collapse
    });

    $("#btnVote").click(function () {
        $("input:radio[name^='VoteValue_']:checked").each(function () {

            var voteTypeId = $(this).attr("name").split("_")[1];
            var voteValue = $(this).val();
            var voteOf = $("[name='voteOf']").val();

            console.log("voteTypeId=", voteTypeId);
            console.log("voteValue=", voteValue);
            console.log("voteOf=", voteOf);

            AddOrEditVote(voteOf, stadiumId, voteTypeId, voteValue, appUserId);
        });

        $("#Modal_AddOrEditVote").modal("hide");
    });

});

