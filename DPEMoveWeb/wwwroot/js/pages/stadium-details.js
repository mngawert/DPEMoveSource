
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
            $("#lbl_ADDRESS").append(value.ADDRESS + " " + value.TAM_NAMT + " " + value.AMP_NAMT + " " + value.PROV_NAMT);

            $("#lbl_TELEPHONE").append(value.TELEPHONE == null ? " - " : value.TELEPHONE);
            $("#lbl_EMAIL").append(value.EMAIL == null ? " - " : value.EMAIL);
            $("#lbl_DIMENSION").append(value.DIMENSION == null ? " - " : value.DIMENSION);
            $("#lbl_TRANSPORT").append(value.TRANSPORT == null ? " - " : value.TRANSPORT);

            $("#lbl_OPEN_DETAIL").html(value.TIME_ == null ? " - " : value.OPEN_DETAIL);
            $("#lbl_START_TIME").html(value.START_TIME == null ? " - " : value.START_TIME);
            $("#lbl_END_TIME").html(value.END_TIME == null ? " - " : value.END_TIME);
            $("#lbl_TRANSPORT").html(value.TRANSPORT == null ? " - " : value.TRANSPORT);
            $("#dv_POLICY").html(value.POLICY == null ? " - " : value.POLICY);
            $("#dv_AGREEMENT").html(value.AGREEMENT == null ? " - " : value.AGREEMENT);
                        
            var gallery = value.GALLERY;
            PrintGallery(gallery);

            var placeNear = value.PLACE_NEAR;
            PrintPlaceNear(placeNear);

            var survey = value.SURVEY;
            PrintSurvey(survey);

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

function PrintSurvey(data) {
    console.log("PrintSurvey");
    var item_1A = "";
    var item_1B = "";
    var item_2 = "";
    var sumPoint = 0;
    var item_icon = "";

    $.each(data, function (index, value) {
        //console.log(value);

        sumPoint += Number(value.POINT);

        // limit display to 8 items
        if (index < data.length/2) {
            item_1A +=
                `
                <h3>` + value.SURV_NAME + `</h3>
                <div class="row">
                    <div class="col-sm-10 col-md-10 col-lg-10">
                        <div class="progress">
                            <div class="progress-bar progress-bar-success" role="progressbar" style="width: `+ value.POINT + `%" aria-valuenow="` + value.POINT + `" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                    <div class="col-sm-2col-md-2 col-lg-2">
                        <h5>` + value.POINT + `</h5>
                    </div>
                </div>
            `;
        }
        else if (index < 8) {
            item_1B +=
                `
                <h3>` + value.SURV_NAME + `</h3>
                <div class="row">
                    <div class="col-sm-10 col-md-10 col-lg-10">
                        <div class="progress">
                            <div class="progress-bar progress-bar-success" role="progressbar" style="width: ` + value.POINT + `%" aria-valuenow="` + value.POINT + `" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                    <div class="col-sm-2col-md-2 col-lg-2">
                        <h5>` + value.POINT + `</h5>
                    </div>
                </div>
            `;
        }

        item_2 +=
            `
            <div class="card_list">
                <img src="` + value.ICON + `" width="80" height="78" alt="" />
                <h4>` + value.SURV_NAME + `</h4>
                <p>` + value.SURV_DETL_NAME + `</p> <br />
            </div>
            `;

        item_icon +=
            `
            <div class="service_sec-box">
                <img src="` + value.ICON + `" width="74" height="74">
                <p>` + value.SURV_DETL_NAME + `</p>
            </div>
            `

    });

    if (data.length > 0) {
        var avg = sumPoint / data.length / 10;
        $("#lbl_SURVEY_POINT_AVG").html(avg % 1 == 0 ? avg: avg.toFixed(1));
    }

    $("#dv_SURVEY_1A").html(item_1A);
    $("#dv_SURVEY_1B").html(item_1B);
    $("#dv_SURVEY_2").html(item_2);
    $("#dv_facility_ICON").prepend(item_icon);
}

function PrintUnderStadium(data) {
    console.log("PrintUnderStadium");
    var item_1 = "";
    $.each(data, function (index, value) {
        console.log(value);
        item_1 +=
        `
            <a id="lnk_UNDER_STADIUM_` + value + `" href="/Stadium/Details/` + value + `">[` + value + `]</a><br />
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
        var data = results.data;
        $.each(data, function (index, value) {
            var item_1 =
                `
                    <div class="row">
                        <img src="` + value.GALLERY[0] + `" width="74" height="74">
                    </div>
                    <div class="row">
                        <p>` + value.NAME_LABEL + `</p>
                    </div>
                `
            $("#lnk_PARENT_STADIUM_" + id).html(item_1)
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
                        <img src="` + value.GALLERY[0] + `" width="74" height="74">
                    </div>
                    <div class="row">
                        <p>` + value.NAME_LABEL + `</p>
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

            $.each(data, function (index, value) {
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
        console.log(data);

        console.log("GetVote response", data);
        console.log("jqXHR.status", jqXHR.status); //handle your 204 or other status codes here

        if (jqXHR.status == 200) {
            var value = data;

            //if (value.voteValue >= 1)
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


    GetVoteType("2", stadiumId);
    GetVoteAvg("2", stadiumId, appUserId);
    GetVoteTotalAvg("2", stadiumId);


    GetCommentsByStadiumId(stadiumId);

    $("#lnkGotoFacility").click(function () {
        $("#collapseOne1").collapse('show'); // toggle collapse
    });

    $("#btnVote").click(function () {
        $("input:radio[name^='VoteValue_']:checked").each(function () {

            var voteTypeId = $(this).attr("name").split("_")[1];
            var voteValue = $(this).val();
            console.log("voteTypeId=", voteTypeId);
            console.log("voteValue=", voteValue);
            AddOrEditVote("2", stadiumId, voteTypeId, voteValue, appUserId);
        });

        $("#Modal_AddOrEditVote").modal("hide");
    });

});

