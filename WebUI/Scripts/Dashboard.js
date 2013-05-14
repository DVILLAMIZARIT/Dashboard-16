$(function () {
    /* Responsive */
    var breakpoints = [320, 480, 768, 900, 1024, 1280];
    $.fn.setBreakpoints && $(window).setBreakpoints({
        distinct: true,
        breakpoints: breakpoints
    });
    function handleBreakpoint(e) {
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
    }
    for (var i = 0; i < breakpoints.length; i++) {
        $(window).bind('enterBreakpoint' + breakpoints[i] + ' exitBreakpoint' + breakpoints[i], handleBreakpoint);
    }

    /* sidebar menu */
    var $content = $('.page-content'),
        $sidebar = $('.page-sidebar');
    function refreshSidebarAndContentHeight() {
        if (!$content.data('height')) {
            $content.data('height', $content.height());
        }
        if ($sidebar.height() > $content.height()) {
            $content.css('min-height', $sidebar.height() + 20);
        } else {
            $content.css('min-height', $content.data('height'));
        }
    };
    function handleSidebarLinkClick(e) {
        var $this = $(this);
        if (!$this.next().hasClass('sub-menu')) return;
        var $parent = $this.parent().parent();
        $parent.children('li.open')
            .children('a')
                .children('.arrow').removeClass('open').end()
            .end().children('.sub-menu').slideUp(200).end()
            .removeClass('open');
        var $sub = $this.next();
        if ($sub.is(':visible')) {
            $this.find('.arrow').removeClass('open');
            $this.parent().removeClass('open');
            $sub.slideUp(200, function () {
                refreshSidebarAndContentHeight();
            });
        } else {
            $this.find('.arrow').addClass('open');
            $this.parent().addClass('open');
            $sub.slideDown(200, function () {
                refreshSidebarAndContentHeight();
            });
        }
    }
    $(document).on('click', '.page-sidebar li > a', handleSidebarLinkClick);
    $.cookie && (function () {
        if ($.cookie('sidebar-closed') == 1) {
            $container.addClass('sidebar-closed');
        }
    })();

    /* sidebar toggle */
    var $container = $('.page-container');
    $.cookie && (function () {
        if ($.cookie('sidebar-closed') == 1) {
            $container.addClass('sidebar-closed');
        }
        $('.page-sidebar .sidebar-toggle').click(function (e) {
            $('.sidebar-search').removeClass('open');
            if ($container.hasClass('sidebar-closed')) {
                $container.removeClass('sidebar-closed');
                $.cookie && $.cookie('sidebar-closed', null);
            } else {
                $container.addClass('sidebar-closed');
                $.cookie && $.cookie('sidebar-closed', 1);
            }
            e.preventDefault();
        });
    });
    $('.sidebar-search .remove').click(function (e) {
        $('.sidebar-search').removeClass('open');
        e.preventDefault();
    });
    
    /* Go-to-top Shortcut */
    $('.footer .go-top').on('click', function (e) {
        $(window).animate({ scrollTop: 0 }, 'slow');
    });

    // Load tabs on URL change (use #!tab1/nested-tab/nested-nested-tab format)
    if (window.location.hash.length > 1 && window.location.hash.match(/^#!.*/)) {
        var tabs = window.location.hash.substr(2).split('/');
        for (var t = 0; t < tabs.length; t++) {
            var $activeTab = $('a[href="#' + tabs[t] + '"]');
            if ($activeTab) {
                $activeTab.tab('show');
            } else {
                break; // stop on first missing tab
            }
        }
    }
});
