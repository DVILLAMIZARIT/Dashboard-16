var App = function () {
    "use strict"; // http://msdn.microsoft.com/en-us/library/br230269.aspx
    var app = this,
        isRTL = !1,
        isIE8 = !1, isIE9 = !1, isIE10 = !1,
        sidebarWidth = 225,
        sidebarCollapsedWidth = 35,
        layoutColorCodes = {
            blue: '#4B8DF8',
            red: '#E02222',
            green: '#35AA47',
            purple: '#852B99',
            grey: '#555',
            lightGrey: '#FAFAFA',
            yellow: '#FFB848'
        };

    //
    // browser check
    // various browser checks for laying out the theme properly
    //
    var browser = {
        ieCheck: function () {
            var ua = navigator.userAgent.match(/MSIE ([\d.]+)/);
            if (ua) {
                isIE8 = ua[1] == "8.0";
                isIE9 = ua[1] == "9.0";
                isIE10 = ua[1] == "10";
                if (isIE10) {
                    $('html').addClass('ie10');
                }
            }
        },
        isTouchDevice: function () {
            try {
                document.createEvent('TouchEvent');
                return true;
            } catch (e) {
                return false;
            }
        }
    };

    //
    // layout
    // handles layout elements
    //
    var layout = {
        //TODO: move layout elements here
    };

    //
    // responsive
    // handles responsive layout for the theme
    //
    var responsive = {
        handlers: {
            _items: [],
            add: function (fn) {
                this._items.push(fn);
            },
            clear: function (fn) {
                this._items = [];
            },
            run: function () { // runResponsiveHandlers()
                for (var i = 0; i < this._items.length; i++) {
                    this._items[i].call();
                }
            }
        },

        calculateFixedSidebarViewportHeight: function () { // _calculateFixedSidebarViewportHeight()
            var sidebarHeight = $(window).height() - $('.header').height() + 1;
            if ($('body').hasClass('page-footer-fixed')){
                sidebarHeight -= $('.footer').height();
            }
            return sidebarHeight;
        },
        desktopTabletContents: function () { // handleDesktopTabletContents
            var winWidth = $(window).width(),
                isBoxed = $('body').hasClass('page-boxed');
            if (winWidth <= 1280 || isBoxed) {
                $('.responsive').each(function () {
                    var $this = $(this),
                        tablet = $this.data('tablet'),
                        desktop = $this.data('desktop');
                    if (tablet) {
                        $this.removeClass(desktop).addClass(tablet);
                    }
                });
            }
            if (winWidth > 1280 && !isBoxed) {
                $('.responsive').each(function () {
                    var $this = $(this),
                        tablet = $this.data('tablet'),
                        desktop = $this.data('desktop');
                    if (tablet) {
                        $this.removeClass(tablet).addClass(desktop);
                    }
                });
            }
        },
        fixedSidebar: function () { // handleFixedSidebar
            var $menu = $('.page-sidebar-menu');
            if ($menu.parent('.slimScrollDiv').size() == 1) {
                $menu.slimsScroll({ destroy: true });
                $menu.css('min-height','');
            }
            if ($('.page-sidebar-fixed').size() == 0) {
                responsive.sidebarAndContentHeight();
                return;
            }
            if ($(window).width() >= 980) {
                var sidebarHeight = responsive.calculateFixedSidebarViewportHeight();
            }
        },
        fixedSidebarHoverable: function () { // handleFixedSidebarHoverable()
            var $body = $('body');
            if (!$body.hasClass('page-sidebar-fixed')) {
                return;
            }
            var $pageSidebar = $('.page-sidebar');
            $pageSidebar.off('mouseenter').on('mouseenter', function () {
                var $this = $(this);
                if ((!$body.hasClass('page-sidebar-closed') || !$body.hasClass('page-sidebar-fixed')) || $this.hasClass('page-sidebar-hovering')) {
                    return;
                }
                $body.removeClass('page-sidebar-closed').addClass('page-sidebar-hover-on');
                $this.addClass('page-sidebar-hovering').animate({
                    width: sidebarWidth
                }, 400, '', function () {
                    $this.removeClass('page-sidebar-hovering');
                });
            }).off('mouseleave').on('mouseleave', function () {
                var $this = $(this);
                if ((!$body.hasClass('page-sidebar-hover-on') || !$body.hasClass('page-sidebar-fixed')) || $this.hasClass('page-sidebar-hovering')) {
                    return;
                }
                $this.addClass('page-sidebar-hovering').animate({
                    width: sidebarCollapsedWidth
                }, 400, '', function () {
                    $body.addClass('page-sidebar-closed').removeClass('page-sidebar-hover-on');
                    $this.removeClass('page-sidebar-hovering');
                });
            });
        },
        sidebarAndContentHeight: function () { // handleSidebarAndContentHeight
            var $content = $('.page-content'),
                $sidebar = $('.page-sidebar'),
                $body = $('body');
            if ($body.hasClass('.page-footer-fixed') && !$body.hasClass('page-sidebar-fixed')) {
                var availHeight = $(window).height() - $('.footer').height();
                if ($content.height() < availHeight) {
                    $content.css('min-height', availHeight + 'px !important');
                }
            } else {
                var height;
                if ($body.hasClass('page-sidebar-fixed')) {
                    height = responsive.calculateFixedSidebarViewportHeight();
                } else {
                    height = $sidebar.height() + 20;
                }
                if (height >= $content.height()) {
                    $content.css('min-height', height + 'px !important');
                }
            }
        },
        sidebarState: function () { // handleSidebarState()
            if ($(window).width() < 980) {
                $('body').removeClass('page-sidebar-closed');
            }
        },

        init: function () { // handeResponsiveOnResize()
            var resizeTimeout = null;
            if (isIE8) {
                var currHeight;
                $(window).resize(function () {
                    if (currHeight == document.documentElement.clientHeight) {
                        return;
                    }
                    if (resizeTimeout) {
                        clearTimeout(resizeTimeout);
                    }
                    resizeTimeout = setTimeout(function () {
                        responsive.update();
                    }, 50);
                    currHeight = document.documentElement.clientHeight;
                });
            } else {
                $(window).resize(function () {
                    if (resizeTimeout) {
                        clearTimeout(resizeTimeout);
                    }
                    resizeTimeout = setTimeout(function () {
                        responsive.update();
                    }, 50);
                });
            }
        },
        update: function (isInit) { // handeResponsive() & handleResponsiveOnInit()
            if (!isInit) {
                ui.plugins.tooltips(); // handleTooltips();
            }
            responsive.sidebarState(); // handleSidebarState();
            responsive.desktopTabletContents(); // handleDesktopTabletContents();
            responsive.sidebarAndContentHeight(); // handleSidebarAndContentHeight();
            if (!isInit) {
                ui.plugins.chosenSelect(); // handleChoosenSelect();
                // handleFixedSidebar();
                responsive.handlers.run(); // runResponsiveHandlers();
            }
        }
    };

    //
    // storage
    // store information using localStorage when possible, otherwise defaulting to cookies
    //
    var storage = {
        getItem: function (k) {
            if (typeof window.localStorage != 'undefined' && window.localStorage !== null) {
                return localStorage.getItem(k);
            } else if ($.cookie) {
                return $.cookie(k);
            }
            return false;
        },
        removeItem: function (k) {
            if (typeof window.localStorage != 'undefined' && window.localStorage !== null) {
                return localStorage.removeItem(k);
            } else if ($.cookie) {
                return $.cookie(k, null);
            }
            return false;
        },
        setItem: function (k, v) {
            if (typeof window.localStorage != 'undefined' && window.localStorage !== null) {
                return localStorage.setItem(k, v);
            } else if ($.cookie) {
                return $.cookie(k, v, { path: '/', expires: 30 /*days*/ });
            }
            return false;
        }
    };

    //
    // ui
    // handle all the user interface elements
    //
    var ui = {
        goToTop: function () { // handleGoTop
            $('.footer').on('click', '.go-top', function (e) {
                ui.scrollTo();
                e.preventDefault();
            });
        },
        plugins: {
            chosenSelect: function () { // handleChoosenSelect()
                $.chosen && $('.chosen').each(function () {
                    $(this).chosen({
                        allow_single_deselect: $(this).data('withDeselect') == 1
                    });
                });
            },
            fancybox: function () {
                $.fancybox && $('.fancybox-button').fancybox({
                    groupAttr: 'data-rel',
                    prevEffect: 'none',
                    nextEffect: 'none',
                    closeBtn: !0,
                    helpers: {
                        title: {
                            type: 'inside'
                        }
                    }
                });
            },
            popovers: function () { // handlePopovers()
                $.popover && $('.popovers').popover();
            },
            scrollers: function () { // handleScrollers()
                $.slimScroll && $('.scroller').each(function () {
                    var $this = $(this);
                    $this.slimScroll({
                        size: '7px',
                        color: '#A1B2BD',
                        position: isRTL ? 'left' : 'right',
                        height: $this.data('height'),
                        alwaysVisible: $this.data('alwaysVisible') == 1,
                        railVisible: $this.data('railVisible') == 1
                    });
                });
            },
            tooltips: function () { // handleTooltips()
                if (!$.tooltip) return;

                var sel = '.tooltips';
                if (browser.isTouchDevice()) {
                    sel += ':not(.no-tooltip-on-touch-device)';
                }
                $(sel).tooltip();
            },
            uniform: function (els) {
                if (!$.uniform) return;

                var $els = els ? $(els) : $('input[type="checkbox"]:not(.toggle), input[type="radio"]:not(.toggle,.star)');
                $els.each(function (i, e) {
                    var $this = $(this);
                    if ($this.parents('.checker').size() == 0) {
                        $this.show().uniform();
                    }
                });
            }
        },
        scrollTo: function (el,offset) { // 
            $('html,body').animate({
                scrollTop: el ? $(el).offset().top : 0
            }, 'slow');
        },
        sidebar: function () { // handleSidebarMenu 
            $('.page-sidebar').on('click', 'li > a', function (e) {
                var $this = $(this),
                    $next = $this.next(),
                    $target = $(e.target);

                if (!$next.hasClass('sub-menu')) {
                    $('.btn-navbar:not(.collapsed)').click();
                    return;
                }
                var $parent = $this.parent().parent();
                $parent.children('li.open')
                    .children('a > .arrow').removeClass('open').end()
                    .children('.submenu').slideUp(200).end()
                    .removeClass('open');

                if ($next.is(':visible')) {
                    $('.arrow', this).removeClass('open');
                    $this.parent().removeClass('open');
                    $next.slideUp(200, responsive.sidebarAndContentHeight);
                } else {
                    $('.arrow', this).addClass('open');
                    $this.parent().addClass('open');
                    $next.slideDown(200, responsive.sidebarAndContentHeight);
                }
            });
        }
    };

    return {
        Init: function () {
            browser.ieCheck();
            responsive.init();
            responsive.update(true);
            responsive.fixedSidebar();
            ui.plugins.chosenSelect();
            ui.plugins.scrollers();
            ui.plugins.uniform();
            ui.sidebar();
        },
        Responsive: {
            Run: responsive.handlers.run,
            Subscribe: responsive.handlers.add
        },
        Storage: storage,
        UI: {
            NormalizeHeight: ui.normalizeHeight,
            ScrollTo: ui.scrollTo,
            ScrollTop: function () { this.ScrollTo(); }
        },

        isIE8: function () { return isIE8; },
        isIE9: function () { return isIE9; },
        isIE10: function () { return isIE10; },
        isTouchDevice: function () { return browser.isTouchDevice(); }
    };
}();

$(App.Init);