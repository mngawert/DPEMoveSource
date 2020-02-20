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
				setTimeout(function(){}, 2000);
				$('body').animate({
                    scrollTop: $("#start-input").offset().top - 30
                }, 2000);

			});
		}
		if( $('.MultiCarousel .MultiCarousel-inner .item > div').length ) {
			$('.MultiCarousel .MultiCarousel-inner .item > div').matchHeight();
		}
		if( $('#btn-advsearch').length ) {
			$('#btn-advsearch').click(function() {
				if( $('#section-advsearch').hasClass('active') )
					$('#section-advsearch').removeClass('active');
				else
					$("#section-advsearch").addClass("active");
			});
		}
	});
})(jQuery, this);
