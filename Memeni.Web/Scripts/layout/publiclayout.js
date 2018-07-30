(function ($) {
    'use strict';
    $(document).on('ready', function () {
        // Header
        $.HSCore.components.HSHeader.init($('#js-header'));
        $.HSCore.helpers.HSHamburgers.init('.hamburger');

        // Initialization of HSMegaMenu plugin
        $('.js-mega-menu').HSMegaMenu({
            event: 'hover',
            pageContainer: $('.container'),
            breakpoint: 991
        });
        $.HSCore.components.HSTabs.init('[data-tabs-mobile-type]');

        $.HSCore.helpers.HSHeightCalc.init();
        $.HSCore.components.HSOnScrollAnimation.init('[data-animation]');
        $.HSCore.components.HSVideoAudio.init('.js-video-audio');
        $.HSCore.helpers.HSBgVideo.init('.js-bg-video');
        $.HSCore.components.HSCarousel.init('.js-carousel');
        $.HSCore.components.HSPopup.init('.js-fancybox');
        $.HSCore.components.HSPopup.init('.js-fancybox-media', {
            helpers: {
                media: {},
                overlay: {
                    css: {
                        'background': 'rgba(0, 0, 0, .8)'
                    }
                }
            }
        });
    });
})(jQuery);