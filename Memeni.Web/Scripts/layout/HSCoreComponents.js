$(document).on('ready', function () {
    // Toolttips
   $('[data-toggle="tooltip"]').tooltip();

    // initialization of counters
    var counters = $.HSCore.components.HSCounter.init('[class*="js-counter"]');

    // Rating
    $.HSCore.components.HSRating.init($('.js-rating'), {
        spacing: 2
    });

    // Initialization of HSScrollBar component
    $.HSCore.components.HSScrollBar.init($('.js-scrollbar'));

    // initializtion of horizontal progress bars
    var horizontalProgressBars = $.HSCore.components.HSProgressBar.init('.js-hr-progress-bar', {
        direction: 'horizontal',
        indicatorSelector: '.js-hr-progress-bar-indicator'
    });

    // Datepicker
    $.HSCore.components.HSDatepicker.init('#datepickerInline');
});