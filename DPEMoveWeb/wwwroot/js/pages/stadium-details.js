
function GetStadiumDetails(id) {

    //var form = new FormData();
    //form.append("STADIUM_ID", "15");

    //var settings = {
    //    "url": "http://data.dpe.go.th/api/stadium/address/getStadiumDetail",
    //    "method": "POST",
    //    "timeout": 0,
    //    "processData": false,
    //    "mimeType": "multipart/form-data",
    //    "contentType": false,
    //    "data": form
    //};

    //$.ajax(settings).done(function (response) {
    //    console.log(response);
    //});

    //return false;



    //var myHeaders = new Headers();
    //myHeaders.append("Content-Type", "multipart/form-data; boundary=--------------------------866572304719600514599436");

    //var formdata = new FormData();
    //formdata.append("STADIUM_ID", "51370");

    //var requestOptions = {
    //    method: 'POST',
    //    headers: myHeaders,
    //    body: formdata,
    //    redirect: 'follow'
    //};

    //fetch("http://data.dpe.go.th/api/stadium/address/getStadiumDetail", requestOptions)
    //    .then(response => response.json())
    //    .then(result => console.log(result))
    //    .catch(error => console.log('error', error));

    //return false;


    var form = new FormData();
    form.append("STADIUM_ID", id);

    var settings = {
        "url": "http://data.dpe.go.th/api/stadium/address/getStadiumDetail",
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
            $("#lbl_NAME_LABEL").html(value.NAME_LABEL)
            $("#lbl_ADDRESS").append(value.ADDRESS + " " + value.TAM_NAMT + " " + value.AMP_NAMT + " " + value.PROV_NAMT);

            $("#lbl_TELEPHONE").append(value.TELEPHONE == null ? " - " : value.TELEPHONE);
            $("#lbl_EMAIL").append(value.EMAIL == null ? " - " : value.EMAIL);

            $("#lbl_TIME_").html(value.TIME_ == null ? " - " : value.TIME_);
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
                        <a href="/Stadium/Details/` + value.UNDER_STADIUM_ID + `">[1]</a>
                    `;
                $("#dv_UNDER_STADIUM_ID").html(tmp);
            }

            if (value.UNDER_STADIUM.length > 0) {
                PrintUnderStadium(value.UNDER_STADIUM);
            }

        });
    });
}

function PrintGallery(data) {
    console.log("PrintGallery");
    var item_1 = "";
    var item_2 = "";
    $.each(data, function (index, value) {
        console.log(value);

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
        console.log(value);

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
    var item_1 = "";
    $.each(data, function (index, value) {
        console.log(value);

        item_1 +=
            `
            <div class="card_list">
                <img src="` + value.ICON + `" width="80" height="78" alt="" />
                <h4>` + value.SURV_NAME + `</h4>
                <p>
                    ` + value.SURV_DETL_NAME + `
                </p>
            </div>
            `
    });

    $("#dv_SURVEY").html(item_1);
}

function PrintUnderStadium(data) {
    console.log("PrintUnderStadium");
    var item_1 = "";
    $.each(data, function (index, value) {
        console.log(value);

        item_1 +=
            `
                <a href="/Stadium/Details/` + value + `">[` + (index+1) + `]</a>
            `
    });

    $("#dv_UNDER_STADIUM").html(item_1);
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


$(document).ready(function () {

    var stadiumId = routeId;
    console.log("stadiumId=", stadiumId);

    GetVoteAvg("2", stadiumId);
    GetStadiumDetails(stadiumId);
    GetCommentsByStadiumId(stadiumId);

    $("#lnkGotoFacility").click(function () {
        $("#collapseOne1").collapse('show'); // toggle collapse
    });

});

