/**
 * Go To wrapper.
 *
 * @author Htmlstream
 * @version 1.0
 *
 */
;(function ($) {
  'use strict';
  $.HSCore.components.HSGoTo = {
    /**
     *
     *
     * @var Object _baseConfig
     */
    _baseConfig: {},

    /**
     *
     *
     * @var jQuery pageCollection
     */
    pageCollection: $(),

    /**
     * Initialization of Go To wrapper.
     *
     * @param String selector (optional)
     * @param Object config (optional)
     *
     * @return jQuery pageCollection - collection of initialized items.
     */

    init: function (selector, config) {
      this.collection = selector && $(selector).length ? $(selector) : $();
      if (!$(selector).length) return;

      this.config = config && $.isPlainObject(config) ?
        $.extend({}, this._baseConfig, config) : this._baseConfig;

      this.config.itemSelector = selector;

      this.initGoTo();

      return this.pageCollection;
    },

    initGoTo: function () {
      //Variables
      var $self = this,
        collection = $self.pageCollection;

      //Actions
      this.collection.each(function (i, el) {
        //Variables
        var $this = $(el),
          $target = $this.data('target'),
          type = $this.data('type'),
          showEffect = $this.data('show-effect'),
          position = JSON.parse(el.getAttribute('data-position')),
          compensation = $($this.data('compensation')).outerHeight();

        $this.addClass('animated').css({
          'position': type,
          'opacity': 0
        });

        if (type == 'fixed' || type == 'absolute') {
          $this.css(position);
        }

        $this.on('click', function (e) {
          e.preventDefault();

          $('html, body').stop().animate({
            'scrollTop': $target ? $($target).offset().top - compensation : 0
          }, 800);
        });

        if (!$this.hasClass('u-animation-was-fired')) {
          if ($this.offset().top <= $(window).height()) {
            $this.addClass('u-animation-was-fired ' + showEffect).css({
              'opacity': ''
            });
          }
        }

        $(window).on('scroll', function () {
          var thisOffsetTop = $this.offset().top;

          if (!$this.hasClass('u-animation-was-fired')) {
            if ($(window).scrollTop() >= thisOffsetTop - $(window).height()) {
              $this.addClass('u-animation-was-fired ' + showEffect).css({
                'opacity': ''
              });
            }
          }
        });

        //Actions
        collection = collection.add($this);
      });
    }
  };
})(jQuery);
