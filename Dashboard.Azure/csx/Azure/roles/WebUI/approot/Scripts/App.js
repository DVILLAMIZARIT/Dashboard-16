$(function () {
    /* Compatibility tests */
    if (/MSIE (\d+(?:\.\d+)?);/.test(navigator.userAgent)) { //test for MSIE x.x;
        var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
        if (ieversion == 8) {
            $('.visible-ie8').show();
        } else if (ieversion == 10) {
            $('html').addClass('ie10');
        }
    }
    var isTouchDevice = function () {
        try {
            document.createEvent("TouchEvent");
            return true;
        } catch (e) {
            return false;
        }
    };

    /* Responsive */
    var breakpoints = [320, 480, 768, 900, 1024, 1280];
    $.fn.setBreakpoints && $(window).setBreakpoints({
        distinct: true,
        breakpoints: breakpoints
    });
    for (var i = 0; i < breakpoints.length; i++) {
        $(window).bind('enterBreakpoint' + breakpoints[i] + ' exitBreakpoint' + breakpoints[i], function (e) {
            var windowWidth = $(window).width();
            // Sidebar toggle (remove for <900 [phone/mobile])
            if (windowWidth < 900) {
                $.cookie && $.cookie('sidebar-closed', null);
                $('.page-container').removeClass('sidebar-closed');
            }
            // Element repositioning
            $('.responsive').each(function () {
                var forTablet = $(this).data('tablet'),
                    forDesktop = $(this).data('desktop');
                if (forTablet && forDesktop) {
                    $(this).removeClass(windowWidth <= 1280 ? forDesktop : forTablet)
                        .addClass(windowWidth <= 1280 ? forTablet : forDesktop);
                }
            });
            // Sidebar and content
            var $content = $('.page-content'),
                $sidebar = $('.page-sidebar');
            if (!$content.data('height')) {
                $content.data('height', $content.height());
            }
            $content.css('min-height', $sidebar.height() > $content.height() ? $sidebar.height() - 20 : $content.data('height'));
        });
    }
    

    /* Shortcut */
    $('.footer .go-top').on('click', function (e) {
        $(window).animate({ scrollTop: 0 }, 'slow');
    });

    /* Additional UI Enhancements */
    $.fn.uniform && $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)").uniform();
    $.fn.placeholder && $('input, textarea').placeholder();
    $.fn.toolip && (function () {
        // http://stackoverflow.com/questions/9958825/how-do-i-bind-twitter-bootstrap-tooltips-to-dynamically-created-elements
        //$('body').tooltip({ selector: '.tooltips' + (isTouchDevice() ? ':not(.no-tooltip-on-touch-device)' : '') });
        // But still buggy and ignores data-* attribute overrides -- https://github.com/twitter/bootstrap/issues/4990
        $('.tooltips' + (isTouchDevice() ? ':not(.no-tooltip-on-touch-device)' : '')).tooltip();
    })();
    $.fn.popover && (function () {
        // http://stackoverflow.com/questions/9958825/how-do-i-bind-twitter-bootstrap-tooltips-to-dynamically-created-elements
        //$('body').popover({ selector: '.popovers' });
        // But still buggy and ignores data-* attribute overrides -- https://github.com/twitter/bootstrap/issues/4990
        $('.popovers').popover();
    })();
});