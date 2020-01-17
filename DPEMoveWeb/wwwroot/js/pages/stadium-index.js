
function GetEvent() {

    var options = {};

    var input = {};
    input.limitStart = "1";
    input.limitSize = "100";
    input.eventName = $("#txtEventName").val();
    input.eventStart = $("#txtEventStart").val();
    input.eventFinish = $("#txtEventFinish").val();
    input.provinceCode = $("#ddlProvince").val();
    input.amphurCode = $("#ddlAmphur").val();
    input.tambonCode = $("#ddlTambon").val();

    console.log("input", input);

    options.data = JSON.stringify(input);

    console.log("options.data", options.data);

    options.url = "/webapi/Events/GetEvent";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {

            items +=
                `
            <li>
                <a href="/Events/Details/` + value.eventId + `">
                    <div class="row event">
                        <div class="col-12 col-sm-5 col-md-4">
                            <div class="event-thumb"><img src="` + value.fileUrl + `" /></div>
                        </div>
                        <div class="col-12 col-sm-7 col-md-8">
                            <div class="rating">
                                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
                            </div>
                            <div class="event-date">` + value.eventStartTH +`</div>
                            <h4>` + value.eventName + `</h4>
                            <div class="event-place">
                                ` + $("<div />").html(value.eventDescription).text().substring(0,100) + `<br />
                                ` + GetProvinceNameById(value.provinceCode) +  `
                            </div>
                            <div class="row read-comment">
                                <div class="col-sm-12 col-md-6">
                                    <div class="read-total">อ่านแล้ว ` + value.readCount + ` คน</div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="comment-total">แสดงความคิดเห็น ` + value.commentCount + ` คน</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </li >`
        });
        $("#ul-search-events-result").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetProvinceNameById(provinceId) {

    if (provinceId == null)
        return "";

    console.log("call GetProvinceNameById", provinceId);
    var provinceName = "n/a";
    $.each(PROVINCE_DATA, function (index, value) {
        if (value.pcode == provinceId) {
            provinceName = value.province;
        }
    });
    return provinceName;
}

function GetProvince() {

    console.log("call GetProvince");

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=province";
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        PROVINCE_DATA = data.DATA;
        console.log('PROVINCE_DATA', PROVINCE_DATA);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.province + `</option>
                `
        });
        $("#ddlProvince").html(items);
        GetEvent();
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetAmphur(provinceId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=amphur&province=" + provinceId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.amphur + `</option>
                `
        });
        $("#ddlAmphur").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}

function GetTambon(provinceId, amphurId) {

    var options = {};

    options.url = "http://103.208.27.224/mots_sport/service/get.php?MOD=tambon&province=" + provinceId + "&amphur=" + amphurId;
    options.contentType = "application/json";
    options.method = "GET";

    options.success = function (_data) {
        data = JSON.parse(_data);
        var items =
            `
            <option value="">แสดงทั้งหมด</option>
            `
        $.each(data.DATA, function (index, value) {
            items +=
                `
                <option value="` + value.pcode + `">` + value.tambon + `</option>
                `
        });
        $("#ddlTambon").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}



function GetToken() {

    var myHeaders = new Headers();
    myHeaders.append("token", "eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ");
    myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("http://data.dpe.go.th/api/stadium/standard/getStandard", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    return false;


    var myHeaders = new Headers();
    myHeaders.append("username", "dpeusers");
    myHeaders.append("password", "users_api@dpe.go.th");

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("http://data.dpe.go.th/api/tokens/keys/tokens", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    return false;



    console.log("TEST location.getProvince");

    var myHeaders = new Headers();
    //myHeaders.append("Token", "eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ");
    myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("http://data.dpe.go.th/api/information/location/getProvince", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    return false;



    console.log("TEST");

    var myHeaders = new Headers();
    myHeaders.append("Token", "eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ");
    myHeaders.append("Content-Type", "multipart/form-data; boundary=--------------------------941675419728121359730982");

    var formdata = new FormData();
    formdata.append("PAGE", "1");
    formdata.append("limit", "10");

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: formdata,
        redirect: 'follow'
    };

    fetch("http://data.dpe.go.th/api/stadium/address/getStadium", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    return false;




    console.log("REST ");

    var settings = {
        "url": "https://localhost:44388/api/RSS/GetProvince",
        "method": "GET",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
    });

    return false;








    console.log("TEST XMLHttpRequest");

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            console.log(this.responseText);
        }
    });

    xhr.open("POST", "http://data.dpe.go.th/api/stadium/standard/getStandard");
    xhr.setRequestHeader("Token", "eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

    xhr.send();

    return false;







    console.log("GetToken");
    $.ajax({
        url: "http://data.dpe.go.th/api/tokens/keys/tokens",
        type: 'GET',
        dataType: 'json',
        headers: {
            'username': 'dpeusers',
            'password': 'users_api@dpe.go.th'
        },
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            console.log("GetToken Success");
            console.log("result", result);
        },
        error: function (error) {
            console.log("GetToken Error");

        }
    });
}


function GetStadium() {

    GetToken();

    return false;


    $("#frmSearchStadium").submit(function (e) {
        e.preventDefault(); //prevent default action 

        //var post_url = $(this).attr("action"); //get form action url
        var post_url = "http://data.dpe.go.th/api/stadium/address/getStadium";
        var request_method = $(this).attr("method"); //get form GET/POST method
        var form_data = $(this).serialize(); //Encode form elements for submission

        $.ajax({
            url: post_url,
            type: request_method,
            headers: {
                'Token': 'eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ'
            },
            data: form_data
        }).done(function (response) { //
            $("#server-results").html(response);
        });
    });


    var options = {};

    var input = {};
    input.limitStart = "1";
    input.limitSize = "100";
    input.eventName = $("#txtEventName").val();
    input.eventStart = $("#txtEventStart").val();
    input.eventFinish = $("#txtEventFinish").val();
    input.provinceCode = $("#ddlProvince").val();
    input.amphurCode = $("#ddlAmphur").val();
    input.tambonCode = $("#ddlTambon").val();

    console.log("input", input);

    options.data = JSON.stringify(input);

    console.log("options.data", options.data);

    options.url = "http://data.dpe.go.th/api/stadium/address/getStadium";
    options.contentType = "application/json";
    options.method = "POST";

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {

            items +=
                `
            <li>
                <a href="/Events/Details/` + value.eventId + `">
                    <div class="row event">
                        <div class="col-12 col-sm-5 col-md-4">
                            <div class="event-thumb"><img src="` + value.fileUrl + `" /></div>
                        </div>
                        <div class="col-12 col-sm-7 col-md-8">
                            <div class="rating">
                                <span class="fa fa-star` + (value.voteAvg > 0 ? (value.voteAvg < 1 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 1 ? (value.voteAvg < 2 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 2 ? (value.voteAvg < 3 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 3 ? (value.voteAvg < 4 ? "-half-o checked" : " checked") : "") + `"></span>
                                <span class="fa fa-star` + (value.voteAvg > 4 ? (value.voteAvg < 5 ? "-half-o checked" : " checked") : "") + `"></span>
                            </div>
                            <div class="event-date">` + value.eventStartTH + `</div>
                            <h4>` + value.eventName + `</h4>
                            <div class="event-place">
                                ` + $("<div />").html(value.eventDescription).text().substring(0, 100) + `<br />
                                ` + GetProvinceNameById(value.provinceCode) + `
                            </div>
                            <div class="row read-comment">
                                <div class="col-sm-12 col-md-6">
                                    <div class="read-total">อ่านแล้ว ` + value.readCount + ` คน</div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="comment-total">แสดงความคิดเห็น ` + value.commentCount + ` คน</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </li >`
        });
        $("#ul-search-events-result").html(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
}


function FetProvince() {

    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };

    fetch("http://103.208.27.224/mots_sport/service/get.php?MOD=province", requestOptions)
        .then(response => response.json())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));
}


$(document).ready(function () {

    FetProvince();

    return false;

    //var PROVINCE_DATA = {};

    //GetProvince();

    //$("#ddlProvince").change(function () {
    //    var provinceId = $("#ddlProvince").val();
    //    if (provinceId == "") {
    //        $("#ddlAmphur").html(`<option value="">แสดงทั้งหมด</option>`);
    //        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    //    }
    //    else {
    //        GetAmphur(provinceId);
    //    }
    //});

    //$("#ddlAmphur").change(function () {
    //    var provinceId = $("#ddlProvince").val();
    //    var amphurId = $("#ddlAmphur").val().substr(2, 2);

    //    if (amphurId == "") {
    //        $("#ddlTambon").html(`<option value="">แสดงทั้งหมด</option>`);
    //    }
    //    else {
    //        GetTambon(provinceId, amphurId);
    //    }
    //});

    //GetStadium();
});


