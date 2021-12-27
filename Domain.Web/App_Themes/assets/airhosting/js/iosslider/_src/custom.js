
			$(document).ready(function() {
				
				$('.iosSlider').iosSlider({
					desktopClickDrag: true,
					snapToChildren: true,
					navSlideSelector: '.sliderContainer .slideSelectors .item',
					onSlideComplete: slideComplete,
					onSliderLoaded: sliderLoaded,
					onSlideChange: slideChange,
					autoSlide: true,
					scrollbar: true,
					scrollbarContainer: '.sliderContainer .scrollbarContainer',
					scrollbarMargin: '0',
					scrollbarBorderRadius: '0'
				});
				
			});
			
			function slideChange(args) {
						
				$('.sliderContainer .slideSelectors .item').removeClass('selected');
				$('.sliderContainer .slideSelectors .item:eq(' + (args.currentSlideNumber - 1) + ')').addClass('selected');
			
			}
			
			function slideComplete(args) {
				
				if(!args.slideChanged) return false;
				
				$(args.sliderObject).find('.text1, .text2, .text3, .text4, .text5, .text6, .text7').attr('style', '');
				
				$(args.currentSlideObject).find('.text1').animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text2').delay(200).animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text3').delay(400).animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text4').delay(900).animate({
					left: '0px',
					opacity: '1'
				}, 900, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text5').delay(600).animate({
					left: '0px',
					opacity: '1'
				}, 600, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text6').delay(1300).animate({
					left: '340px',
					opacity: '1'
				}, 1300, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text7').delay(1600).animate({
					left: '368px',
					opacity: '1'
				}, 1600, 'easeOutQuint');
				
			}
			
			function sliderLoaded(args) {
					
				$(args.sliderObject).find('.text1, .text2, .text3, .text4, .text5, .text6, .text7').attr('style', '');
				
				$(args.currentSlideObject).find('.text1').animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text2').delay(200).animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text3').delay(400).animate({
					left: '10px',
					opacity: '1'
				}, 400, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text4').delay(900).animate({
					left: '0px',
					opacity: '1'
				}, 900, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text5').delay(600).animate({
					left: '0px',
					opacity: '1'
				}, 600, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text6').delay(1300).animate({
					left: '340px',
					opacity: '1'
				}, 1300, 'easeOutQuint');
				
				$(args.currentSlideObject).find('.text7').delay(1600).animate({
					left: '368px',
					opacity: '1'
				}, 1600, 'easeOutQuint');
				
				slideChange(args);
				
			}