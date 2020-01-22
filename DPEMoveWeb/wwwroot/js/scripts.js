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
			$(".change-tab").click(function() {
				var c = $(this).attr("class");
				var n = c.lastIndexOf("to-");
				c = c.substring(n+3, c.length);
				$(".tab").removeClass("active").eq( c ).addClass("active");
				$(".tab_item").hide().eq( c ).fadeIn();
			});
		}
		if( $('.MultiCarousel .MultiCarousel-inner .item > div').length ) {
			$('.MultiCarousel .MultiCarousel-inner .item > div').matchHeight();
		}
	});
})(jQuery, this);
