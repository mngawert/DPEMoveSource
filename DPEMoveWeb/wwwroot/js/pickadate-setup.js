function initDatePickerTST() {
    $('.datepicker').each(function (index, value) {
        if (!$(this).is('[readonly]')) {
            $(this).pickadate({
                monthsFull: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฏาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                weekdaysFull: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัส', 'ศุกร์', 'เสาร์'],
                weekdaysShort: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
                selectYears: true,
                selectMonths: true,
                format: 'dd/mm/yyyy',
                onSet: function (data) {
                    if (this.get('select', 'dd/mm/') != "") {
                        var selected_date = this.get('select', 'dd/mm/') + (parseInt(this.get('select', 'yyyy')) + 543);

                        this.$node.val(selected_date);
                    }
                },
                onOpen: function (data) {
                    $('.picker__header select:first-child option').each(function () {
                        var yearBE = parseInt($(this).val()) + 543;
                        $(this).html(yearBE);
                    });
                }
            });
            var date = $(this).val();

            if (date != "") {
                var dateText = date.substring(0, 6) + "" + (parseInt(date.substring(6)));

                var picker = $(this).pickadate('picker');
                picker.set('select', dateText, { format: 'dd/mm/yyyy' });
            }
        } else {
            var date = $(this).val();

            if (date != "") {
                var dateText = date.substring(0, 6) + "" + (parseInt(date.substring(6)) + 543);
                $(this).val(dateText);
            }
        }
    });
}

initDatePickerTST();

$('.datepicker1').pickadate({
    format: 'dd/mm/yyyy',
    selectMonths: true,
    selectYears: 100,
    onOpen: function () {
        $('.picker__header select:first-child option').each(function () {

            console.log("test date val", $(this).val());
            console.log("test date html", $(this).html());
            var yearBE = parseInt($(this).val()) + 543;
            $(this).html(yearBE);
        });
    },
    onSet: function (data) {
        if (this.get('select', 'dd/mm/') != "") {
            var selected_date = this.get('select', 'dd/mm/') + (parseInt(this.get('select', 'yyyy')) + 543);

            this.$node.val(selected_date);
        }
    }
});

$('.datepicker2').pickadate({
    monthsFull: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฏาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
    monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    weekdaysFull: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัส', 'ศุกร์', 'เสาร์'],
    weekdaysShort: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
    format: 'dd/mm/yyyy',
    selectMonths: true,
    selectYears: 100,
    onSet: function (data) {

        $('.picker__header select:first-child option').each(function () {
            var yearBE = parseInt($(this).val()) + 543;
            $(this).html(yearBE);
        });
    }
});

$('.datepicker3').pickadate({
    monthsFull: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
    monthsShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
    weekdaysFull: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัส', 'ศุกร์', 'เสาร์'],
    weekdaysShort: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
    format: 'dd/mm/yyyy',
    selectMonths: true,
    selectYears: true,
    klass: {
        selectMonth: 'picker__select--month browser-default',
        selectYear: 'picker__select--year browser-default'
    },
    onOpen: function () {
        //$('.picker__header select:first-child option').each(function(){
        //    var yearBE = parseInt($(this).val())+543;
        //    $(this).html(yearBE);
        //});
    },
    onSet: function (data) {
        //$('.picker__header select:first-child option').each(function(){
        //// var yearBE = parseInt($(this).val())+543;
        //    var yearBE = parseInt($(this).val())+543;
        //    $(this).html(yearBE);
        //});

        //if (this.get('select', 'dd/mm/') != "") {
        //    var selected_date = this.get('select', 'dd/mm/') + (parseInt(this.get('select', 'yyyy')) + 543);

        //    this.$node.val(selected_date);
        //}
    },
    onClose: function () {
    }
});