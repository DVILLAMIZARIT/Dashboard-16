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