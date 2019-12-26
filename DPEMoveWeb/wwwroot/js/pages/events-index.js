

$(document).ready(function () {

    var options = {};

    var input = {};
    input.limitStart = "1";
    input.limitSiz = "10";

    options.data = JSON.stringify(input);

    options.url = "webapi/Events/GetEvent";
    options.contentType = "application/json";
    options.method = "POST";

    //options.beforeSend = function (request) {
    //    request.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("token"));
    //};

    options.success = function (data) {
        var items = '';
        $.each(data, function (index, value) {
            items += 
            `
            <li>
                <a href="Events/Details/` + value.eventId +`">
                    <div class="row event">
                        <div class="col-12 col-sm-5 col-md-4">
                            <div class="event-thumb"><img src="` + value.fileUrl +`" /></div>
                        </div>
                        <div class="col-12 col-sm-7 col-md-8">
                            <div class="rating">
                                <span class="fa fa-star checked"></span>
                                <span class="fa fa-star checked"></span>
                                <span class="fa fa-star-half-o checked"></span>
                                <span class="fa fa-star"></span>
                                <span class="fa fa-star"></span>
                            </div>
                            <div class="event-date">17 พ.ย. 62</div>
                            <h4>` + value.eventName + `</h4>
                            <div class="event-place">
                                ` + value.addressDescription + `<br />
                                จ.นนทบุรี
                            </div>
                            <div class="row read-comment">
                                <div class="col-sm-12 col-md-6">
                                    <div class="read-total">อ่านแล้ว ` + value.readCount +` คน</div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="comment-total">แสดงความคิดเห็น ` + value.commentCount +` คน</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </li >`
        });
        $("#ul-search-events-result").append(items);
    };
    options.error = function (a, b, c) {
        console.log("Error while calling the Web API!(" + b + " - " + c + ")");
    };
    $.ajax(options);
});

