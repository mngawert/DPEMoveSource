(function ($, root, undefined) {
	$(function () {
		'use strict';
		if( $(".tab").length ) {
			$(".tab").click(function() {
				$(".tab").removeClass("active").eq($(this).index()).addClass("active");
				$(".tab_item").hide().eq($(this).index()).fadeIn()
			}).eq(0).addClass("active");
			$(".tab_item").css('display', 'none');
			$(".tab_item:first-child").css('display', 'block');
		}
	});
})(jQuery, this);
