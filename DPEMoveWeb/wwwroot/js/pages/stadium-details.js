
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

        });

        console.log(response);
    });
}


$(document).ready(function () {

    var stadiumId = routeId;
    console.log("stadiumId=", stadiumId);

    GetStadiumDetails(stadiumId);


});

