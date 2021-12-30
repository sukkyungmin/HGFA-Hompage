// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// full page license

$(document).ready(function () {
    $('#fullpage').fullpage({

        licenseKey: 'OPEN-SOURCE-GPLV3-LICENSE',
        autoScrolling: true,
        scrollHorizontally: true,
        navigation: true,
        navigationPosition: 'right',
    });
});


// 개발중인 아이템 팝업

var $button = $('.button'),
    $modalContainer = $('#modal-container'),
    $body = $('body'),
    $content = $('.content'),
    btnId;

$button.on('click', function () {
    btnId = $(this).attr('id');

    $modalContainer
        .removeAttr('class')
        .addClass(btnId);
    $content
        .removeAttr('class')
        .addClass('content');

    $body.addClass('modal-active');

    if (btnId == 'two' || btnId == 'three' || btnId == 'four') {
        $content.addClass(btnId);
    }

});

$modalContainer.on('click', function () {
    $(this).addClass('out');
    $body.removeClass('modal-active');
    if ($(this).hasClass(btnId)) {

        $content.addClass('out');

    }
});

// 카카오 지도의 z축 인덱스 변경

$(function () {
    var addressindex = $('.addressindex');
    var z_index_0 = $('.z_index_0');
    var z_index_1 = $('.z_index_1');
    z_index_0.click(function () {
        addressindex.css('z-index', '-1');
    });
    z_index_1.click(function () {
        addressindex.css('z-index', '0');
    });
});


// Mune Style2(햄버거 메뉴) 적용할때 

$('.icon-menu').on('click', function () {
    $('.menu').toggleClass('expanded');
    $('span').toggleClass('hidden');
    $('.icon-menu , .icon').toggleClass('close');
});

$('nav li').hover(
    function () {
        $('ul', this).stop().slideDown(200);
    },
    function () {
        $('ul', this).stop().slideUp(200);
    }
);


// AOS Animation 

AOS.init({
    easing: 'ease-out-back',
    duration: 800,
    delay: 300,
    once: true
});

// Scroll 시 이벤트 (Top으로 이동하기)


$(document).ready(function () {

    $('.top').fadeOut();

    $(window).scroll(function () {

        if ($(this).scrollTop() > 100) {
            $('.top').fadeIn();
            $('.hides').fadeOut('slow');
        } else {
            $('.top').fadeOut();
            $('.hides').fadeIn('slow');
        }
    });

    $('.top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 400);
        return false;
    });
});


$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    //console.log(scroll);
    if (scroll >= 50) {
        //console.log('a');
        $(".top").addClass("change1");
    } else {
        //console.log('a');
        $(".top").removeClass("change1");
    }
});



// 타 회사업체 로고 무한 롤링 

window.onload = function () {


    //$(document).ready(function () {

    var bannerLeft = 0;
    var first = 1;
    var last;
    var imgCnt = 0;
    var $img = $(".banner_wraper img");
    var $first;
    var $last;

    $img.each(function () {   // 5px 간격으로 배너 처음 위치 시킴
        $(this).css("left", bannerLeft);
        bannerLeft += $(this).width() + 130;
        $(this).attr("id", "banner" + (++imgCnt));  // img에 id 속성 추가
    });

    if (imgCnt > 9) {                //배너 9개 이상이면 이동시킴

        last = imgCnt;

        setInterval(function () {
            $img.each(function () {
                $(this).css("left", $(this).position().left - 1); // 1px씩 왼쪽으로 이동
            });
            $first = $("#banner" + first);
            $last = $("#banner" + last);
            if ($first.position().left < -200) {    // 제일 앞에 배너 제일 뒤로 옮김
                $first.css("left", $last.position().left + $last.width() + 130);
                first++;
                last++;
                if (last > imgCnt) { last = 1; }
                if (first > imgCnt) { first = 1; }
            }
        }, 16.67);   //여기 값을 조정하면 속도를 조정할 수 있다.(위에 1px 이동하는 부분도 조정하면 깔끔하게 변경가능하다        
    }
};


//Minag Animation (snip1401)

$(".hover").mouseleave(
    function () {
        $(this).removeClass("hover");
    }
);


$(".hover2").mouseleave(
    function () {
        $(this).removeClass("hover2");
    }
);



// None_01 화면 함수

$(document).ready(function () {

    //debugger;

    var curPage = 1;
    var numOfPages = $(".skw-page").length;
    var animTime = 1000;
    var scrolling = false;
    var pgPrefix = ".skw-page-";

    function pagination() {
        scrolling = true;

        $(pgPrefix + curPage).removeClass("inactive").addClass("active");
        $(pgPrefix + (curPage - 1)).addClass("inactive");
        $(pgPrefix + (curPage + 1)).removeClass("active");

        setTimeout(function () {
            scrolling = false;
        }, animTime);
    };

    function navigateUp() {
        if (curPage === 1) return;
        curPage--;
        pagination();
    };

    function navigateDown() {
        if (curPage === numOfPages) return;
        curPage++;
        pagination();
    };

    $(document).on("mousewheel DOMMouseScroll", function (e) {

        setTimeout(function () {
            if (scrolling) return;
            if (e.originalEvent.wheelDelta > 0 || e.originalEvent.detail < 0) {
                navigateUp();
            } else {
                navigateDown();
            }
        }, 300);

    });

    $(document).on("keydown", function (e) {
        if (scrolling) return;
        if (e.which === 38) {
            navigateUp();
        } else if (e.which === 40) {
            navigateDown();
        }
    });

});



//게시판

//$(document).ready(function () {
//    $.ajax({
//        type: "get",
//        url: "bbs_all",
//        success: function (data) {
//            //데이터 만큼 폴문을 돌림
//            for (var str in data) {
//                //변수 tr에 속성을 data-id로 data[str]['b_no']를 넣어주고 id가 list인 태그에 추가
//                var tr = $("<tr></tr>").attr("data-id", data[str]['b_no']).appendTo("#list");
//                //tr에 td테이블 추가 및 클래스 추가 + 텍스트 수정
//                $("<td></td>").text(data[str]['b_no']).addClass("view_btn").appendTo(tr);
//                $("<td></td>").text(data[str]['b_title']).addClass("view_btn").appendTo(tr);
//                $("<td></td>").text(data[str]['b_ownernick']).addClass("view_btn").appendTo(tr);
//                $("<td></td>").text(FormatToUnixtime(data[str]['b_regdate'])).addClass("view_btn").appendTo(tr);
//            }
//        },
//        error: function (error) {
//            alert("오류 발생" + error);
//        }
//    });

//    $(document).on("click", ".view_btn", function () {
//        //해당하는 태그 속성중 DATA-ID를 가져와서 B_NO에 담음
//        var b_no = $(this).parent().attr("data-id");

//        $.ajax({
//            type: "get",
//            url: "get_bbs",
//            data: {
//                b_no: b_no
//            },
//            success: function (data) {
//                //해당하는 ID에 텍스트문 변경
//                $("#b_title").text(data['b_title']);
//                $("#b_review").text(data['b_ownernick'] + " - " + FormatToUnixtime(data['b_regdate']));
//                $("#b_content").text(data['b_content']);
//                //모달을 실행
//                $('#view_modal').modal('show');
//            },
//            error: function (error) {
//                alert("오류 발생" + error);
//            }
//        });
//    });

//    function FormatToUnixtime(unixtime) {
//        var u = new Date(unixtime);
//        return u.getUTCFullYear() +
//            '-' + ('0' + u.getUTCMonth()).slice(-2) +
//            '-' + ('0' + u.getUTCDate()).slice(-2) +
//            ' ' + ('0' + u.getUTCHours()).slice(-2) +
//            ':' + ('0' + u.getUTCMinutes()).slice(-2) +
//            ':' + ('0' + u.getUTCSeconds()).slice(-2)
//    };
//});



// News

(function ($) {
    var settings;
    $.fn.ziehharmonika = function (actionOrSettings, parameter) {
        if (typeof actionOrSettings === 'object' || actionOrSettings === undefined) {
            // Default settings:
            settings = $.extend({
                // To use a headline tag other than h3, adjust or overwrite ziehharmonika.css as well
                headline: 'h3',
                // Give headlines a certain prefix, e.g. "♫ My headline"
                prefix: false,
                // Only 1 accordion can be open at any given time
                highlander: true,
                // Allow or disallow last open accordion to be closed
                collapsible: false,
                // Arrow down under headline
                arrow: true,
                // Opened/closed icon on the right hand side of the headline (either false or JSON containing symbols or image paths)
                collapseIcons: {
                    opened: '&ndash;',
                    closed: '+'
                },
                // Collapse icon left or right
                collapseIconsAlign: 'right',
                // Scroll to opened accordion element
                scroll: true
            }, actionOrSettings);
        }
        // actions
        if (actionOrSettings == "open") {
            if (settings.highlander) {
                $(this).ziehharmonika('forceCloseAll');
            }
            var ogThis = $(this);
            $(this).addClass('active').next('div').slideDown(400, function () {
                if (settings.collapseIcons) {
                    $('.collapseIcon', ogThis).html(settings.collapseIcons.opened);
                }
                // parameter: scroll to opened element
                if (parameter !== false) {
                    smoothScrollTo($(this).prev(settings.collapseIcons));
                }
            });
            return this;
        } else if (actionOrSettings == "close" || actionOrSettings == "forceClose") {
            // forceClose ignores collapsible setting
            if (actionOrSettings == "close" && !settings.collapsible && $(settings.headline + '[class="active"]').length == 1) {
                return this;
            }
            var ogThis = $(this);
            $(this).removeClass('active').next('div').slideUp(400, function () {
                if (settings.collapseIcons) {
                    $('.collapseIcon', ogThis).html(settings.collapseIcons.closed);
                }
            });
            return this;
        } else if (actionOrSettings == "closeAll") {
            $(settings.headline).ziehharmonika('close');
        } else if (actionOrSettings == "forceCloseAll") {
            // forceCloseAll ignores collapsible setting
            $(settings.headline).ziehharmonika('forceClose');
        }

        if (settings.prefix) {
            $(settings.headline, this).attr('data-prefix', settings.prefix);
        }
        if (settings.arrow) {
            $(settings.headline, this).append('<div class="arrowDown"></div>');
        }
        if (settings.collapseIcons) {
            $(settings.headline, this).each(function (index, el) {
                if ($(this).hasClass('active')) {
                    $(this).append('<div class="collapseIcon">' + settings.collapseIcons.opened + '</div>');
                } else {
                    $(this).append('<div class="collapseIcon">' + settings.collapseIcons.closed + '</div>');
                }
            });
        }
        if (settings.collapseIconsAlign == 'left') {
            $('.collapseIcon, ' + settings.headline).addClass('alignLeft');
        }

        $(settings.headline, this).click(function () {
            if ($(this).hasClass('active')) {
                $(this).ziehharmonika('close');
            } else {
                $(this).ziehharmonika('open', settings.scroll);
            }
        });
    };

    function smoothScrollTo(element, callback) {
        var time = 400;
        $('html, body').animate({
            scrollTop: $(element).offset().top
        }, time, callback);
    }
}(jQuery));

$('.ziehharmonika').ziehharmonika({
    highlander: false,
    collapsible: true,
    collapseIcons: {
        opened: '-',
        closed: '+'
    }
});

//$('.ziehharmonika h3:eq(0)').ziehharmonika('open', false);


//scroll btm bar Animation

$(window).on("scroll", function () {
    var scroll = $(window).scrollTop();

    var docH = $(document).height() - $(window).height();
    var bar_percent = scroll / docH * 100;
    $(".scroll-btm-bar").css("width", bar_percent + "%");
});



// Time Line Animation

var ScrollOut = (function () {
    'use strict';

    function clamp(v, min, max) {
        return min > v ? min : max < v ? max : v;
    }
    function sign(x) {
        return (+(x > 0) - +(x < 0));
    }
    function round(n) {
        return Math.round(n * 10000) / 10000;
    }

    var cache = {};
    function replacer(match) {
        return '-' + match[0].toLowerCase();
    }
    function hyphenate(value) {
        return cache[value] || (cache[value] = value.replace(/([A-Z])/g, replacer));
    }

    /** find elements */
    function $(e, parent) {
        return !e || e.length === 0
            ? // null or empty string returns empty array
            []
            : e.nodeName
                ? // a single element is wrapped in an array
                [e]
                : // selector and NodeList are converted to Element[]
                [].slice.call(e[0].nodeName ? e : (parent || document.documentElement).querySelectorAll(e));
    }
    function setAttrs(el, attrs) {
        // tslint:disable-next-line:forin
        for (var key in attrs) {
            if (key.indexOf('_')) {
                el.setAttribute('data-' + hyphenate(key), attrs[key]);
            }
        }
    }
    function setProps(cssProps) {
        return function (el, props) {
            for (var key in props) {
                if (key.indexOf('_') && (cssProps === true || cssProps[key])) {
                    el.style.setProperty('--' + hyphenate(key), round(props[key]));
                }
            }
        };
    }

    var clearTask;
    var subscribers = [];
    function loop() {
        // process subscribers
        var s = subscribers.slice();
        s.forEach(function (s2) { return s2(); });
        // schedule next loop if the queue needs it
        clearTask = subscribers.length ? requestAnimationFrame(loop) : 0;
    }
    function subscribe(fn) {
        subscribers.push(fn);
        if (!clearTask) {
            loop();
        }
        return function () {
            subscribers = subscribers.filter(function (s) { return s !== fn; });
            if (!subscribers.length && clearTask) {
                clearTask = 0;
                cancelAnimationFrame(clearTask);
            }
        };
    }

    function noop() { }

    /**
     * Creates a new instance of ScrollOut that marks elements in the viewport with
     * an "in" class and marks elements outside of the viewport with an "out"
     */
    // tslint:disable-next-line:no-default-export
    function main(opts) {
        // Apply default options.
        opts = opts || {};
        // Debounce onChange/onHidden/onShown.
        var onChange = opts.onChange || noop;
        var onHidden = opts.onHidden || noop;
        var onShown = opts.onShown || noop;
        var onScroll = opts.onScroll || noop;
        var props = opts.cssProps ? setProps(opts.cssProps) : noop;
        var se = opts.scrollingElement;
        var container = se ? $(se)[0] : window;
        var doc = se ? $(se)[0] : document.documentElement;
        var rootChanged = false;
        var scrollingElementContext = {};
        var elementContextList = [];
        var clientOffsetX, clientOffsety;
        var sub = subscribe(render);
        function index() {
            elementContextList = $(opts.targets || '[data-scroll]', $(opts.scope || doc)[0]).map(function (el) { return ({ element: el }); });
        }
        function update() {
            // Calculate position, direction and ratio.
            var clientWidth = doc.clientWidth;
            var clientHeight = doc.clientHeight;
            var scrollDirX = sign(-clientOffsetX + (clientOffsetX = doc.scrollLeft || window.pageXOffset));
            var scrollDirY = sign(-clientOffsety + (clientOffsety = doc.scrollTop || window.pageYOffset));
            var scrollPercentX = doc.scrollLeft / (doc.scrollWidth - clientWidth || 1);
            var scrollPercentY = doc.scrollTop / (doc.scrollHeight - clientHeight || 1);
            // Detect if the root context has changed.
            rootChanged =
                rootChanged ||
                scrollingElementContext.scrollDirX !== scrollDirX ||
                scrollingElementContext.scrollDirY !== scrollDirY ||
                scrollingElementContext.scrollPercentX !== scrollPercentX ||
                scrollingElementContext.scrollPercentY !== scrollPercentY;
            scrollingElementContext.scrollDirX = scrollDirX;
            scrollingElementContext.scrollDirY = scrollDirY;
            scrollingElementContext.scrollPercentX = scrollPercentX;
            scrollingElementContext.scrollPercentY = scrollPercentY;
            var childChanged = false;
            for (var index_1 = 0; index_1 < elementContextList.length; index_1++) {
                var ctx = elementContextList[index_1];
                var element = ctx.element;
                // find the distance from the element to the scrolling container
                var target = element;
                var offsetX = 0;
                var offsetY = 0;
                do {
                    offsetX += target.offsetLeft;
                    offsetY += target.offsetTop;
                    target = target.offsetParent;
                } while (target && target !== container);
                // Get element dimensions.
                var elementHeight = element.clientHeight || element.offsetHeight || 0;
                var elementWidth = element.clientWidth || element.offsetWidth || 0;
                // Find visible ratios for each element.
                var visibleX = (clamp(offsetX + elementWidth, clientOffsetX, clientOffsetX + clientWidth) -
                    clamp(offsetX, clientOffsetX, clientOffsetX + clientWidth)) /
                    elementWidth;
                var visibleY = (clamp(offsetY + elementHeight, clientOffsety, clientOffsety + clientHeight) -
                    clamp(offsetY, clientOffsety, clientOffsety + clientHeight)) /
                    elementHeight;
                var intersectX = visibleX === 1 ? 0 : sign(offsetX - clientOffsetX);
                var intersectY = visibleY === 1 ? 0 : sign(offsetY - clientOffsety);
                var viewportX = clamp((clientOffsetX - (elementWidth / 2 + offsetX - clientWidth / 2)) / (clientWidth / 2), -1, 1);
                var viewportY = clamp((clientOffsety - (elementHeight / 2 + offsetY - clientHeight / 2)) / (clientHeight / 2), -1, 1);
                var visible = +(opts.offset
                    ? opts.offset <= clientOffsety
                    : (opts.threshold || 0) < visibleX * visibleY);
                var changedVisible = ctx.visible !== visible;
                var changed = ctx._changed ||
                    changedVisible ||
                    ctx.visibleX !== visibleX ||
                    ctx.visibleY !== visibleY ||
                    ctx.index !== index_1 ||
                    ctx.elementHeight !== elementHeight ||
                    ctx.elementWidth !== elementWidth ||
                    ctx.offsetX !== offsetX ||
                    ctx.offsetY !== offsetY ||
                    ctx.intersectX !== ctx.intersectX ||
                    ctx.intersectY !== ctx.intersectY ||
                    ctx.viewportX !== viewportX ||
                    ctx.viewportY !== viewportY;
                if (changed) {
                    childChanged = true;
                    ctx._changed = true;
                    ctx._visibleChanged = changedVisible;
                    ctx.visible = visible;
                    ctx.elementHeight = elementHeight;
                    ctx.elementWidth = elementWidth;
                    ctx.index = index_1;
                    ctx.offsetX = offsetX;
                    ctx.offsetY = offsetY;
                    ctx.visibleX = visibleX;
                    ctx.visibleY = visibleY;
                    ctx.intersectX = intersectX;
                    ctx.intersectY = intersectY;
                    ctx.viewportX = viewportX;
                    ctx.viewportY = viewportY;
                    ctx.visible = visible;
                }
            }
            if (!sub && (rootChanged || childChanged)) {
                sub = subscribe(render);
            }
        }
        function render() {
            maybeUnsubscribe();
            // Update root attributes if they have changed.
            if (rootChanged) {
                rootChanged = false;
                setAttrs(doc, {
                    scrollDirX: scrollingElementContext.scrollDirX,
                    scrollDirY: scrollingElementContext.scrollDirY
                });
                props(doc, scrollingElementContext);
                onScroll(doc, scrollingElementContext, elementContextList);
            }
            var len = elementContextList.length;
            for (var x = len - 1; x > -1; x--) {
                var ctx = elementContextList[x];
                var el = ctx.element;
                var visible = ctx.visible;
                if (ctx._changed) {
                    ctx._changed = false;
                    props(el, ctx);
                }
                if (ctx._visibleChanged) {
                    setAttrs(el, { scroll: visible ? 'in' : 'out' });
                    onChange(el, ctx, doc);
                    (visible ? onShown : onHidden)(el, ctx, doc);
                }
                // if this is shown multiple times, keep it in the list
                if (visible && opts.once) {
                    elementContextList.splice(x, 1);
                }
            }
        }
        function maybeUnsubscribe() {
            if (sub) {
                sub();
                sub = undefined;
            }
        }
        // Run initialize index.
        index();
        update();
        // Hook up document listeners to automatically detect changes.
        window.addEventListener('resize', update);
        container.addEventListener('scroll', update);
        return {
            index: index,
            update: update,
            teardown: function () {
                maybeUnsubscribe();
                window.removeEventListener('resize', update);
                container.removeEventListener('scroll', update);
            }
        };
    }

    return main;

}());

ScrollOut({
    cssProps: {
        visibleY: true,
        viewportY: true
    },
});

$(document).ready(function () {

    $(window).resize(function () {
        if ($(this).width() <= 1200) {
            $('.right').removeClass('right').addClass('righttoleft')
        } else {
            $('.righttoleft').removeClass('righttoleft').addClass('right')
        }
    });

});


//Scroll Top Progress

(function ($) {

    $(document).ready(function () {
        "use strict";

        //Scroll back to top

        var progressPath = document.querySelector('.progress-wrap path');
        var pathLength = progressPath.getTotalLength();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
        progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
        progressPath.style.strokeDashoffset = pathLength;
        progressPath.getBoundingClientRect();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
        var updateProgress = function () {
            var scroll = $(window).scrollTop();
            var height = $(document).height() - $(window).height();
            var progress = pathLength - (scroll * pathLength / height);
            progressPath.style.strokeDashoffset = progress;
        }
        updateProgress();
        $(window).scroll(updateProgress);
        var offset = 50;
        var duration = 550;
        jQuery(window).on('scroll', function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('.progress-wrap').addClass('active-progress');
            } else {
                jQuery('.progress-wrap').removeClass('active-progress');
            }
        });
        jQuery('.progress-wrap').on('click', function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        })


    });

})(jQuery);



// History Year selection


(function () {

    //////////////////////
    // Utils
    //////////////////////
    function throttle(fn, delay, scope) {
        // Default delay
        delay = delay || 250;
        var last, defer;
        return function () {
            var context = scope || this,
                now = +new Date(),
                args = arguments;
            if (last && now < last + delay) {
                clearTimeout(defer);
                defer = setTimeout(function () {
                    last = now;
                    fn.apply(context, args);
                }, delay);
            } else {
                last = now;
                fn.apply(context, args);
            }
        }
    }

    function extend(destination, source) {
        for (var k in source) {
            if (source.hasOwnProperty(k)) {
                destination[k] = source[k];
            }
        }
        return destination;
    }

    //////////////////////
    // END Utils
    //////////////////////

    //////////////////////
    // Scroll Module
    //////////////////////

    var ScrollManager = (function () {

        var defaults = {

            steps: null,
            navigationContainer: null,
            links: null,
            scrollToTopBtn: null,

            navigationElementClass: '.Quick-navigation',
            currentStepClass: 'current',
            smoothScrollEnabled: true,
            stepsCheckEnabled: true,

            // Callbacks
            onScroll: null,

            onStepChange: function (step) {
                var self = this;
                var relativeLink = [].filter.call(options.links, function (link) {
                    link.classList.remove(self.currentStepClass);
                    return link.hash === '#' + step.id;
                });
                relativeLink[0].classList.add(self.currentStepClass);
            },

            // Provide a default scroll animation
            smoothScrollAnimation: function (target) {
                $('html, body').animate({
                    scrollTop: target
                }, 'slow');
            }
        },

            options = {};

        // Privates
        var _animation = null,
            currentStep = null,
            throttledGetScrollPosition = null;

        return {

            scrollPosition: 0,

            init: function (opts) {

                options = extend(defaults, opts);

                if (options.steps === null) {
                    console.warn('Smooth scrolling require some sections or steps to scroll to :)');
                    return false;
                }

                // Allow to customize the animation engine ( jQuery / GSAP / velocity / ... )
                _animation = function (target) {
                    target = typeof target === 'number' ? target : $(target).offset().top;
                    return options.smoothScrollAnimation(target);
                };

                // Activate smooth scrolling
                if (options.smoothScrollEnabled) this.smoothScroll();

                // Scroll to top handling
                if (options.scrollToTopBtn) {
                    options.scrollToTopBtn.addEventListener('click', function () {
                        options.smoothScrollAnimation(0);
                    });
                }

                // Throttle for performances gain
                throttledGetScrollPosition = throttle(this.getScrollPosition).bind(this);
                window.addEventListener('scroll', throttledGetScrollPosition);
                window.addEventListener('resize', throttledGetScrollPosition);

                this.getScrollPosition();
            },

            getScrollPosition: function () {
                this.scrollPosition = window.pageYOffset || window.scrollY;
                if (options.stepsCheckEnabled) this.checkActiveStep();
                if (typeof options.onScroll === 'function') options.onScroll(this.scrollPosition);
            },

            scrollPercentage: function () {
                var body = document.body,
                    html = document.documentElement,
                    documentHeight = Math.max(body.scrollHeight, body.offsetHeight,
                        html.clientHeight, html.scrollHeight, html.offsetHeight);

                var percentage = Math.round(this.scrollPosition / (documentHeight - window.innerHeight) * 100);
                if (percentage < 0) percentage = 0;
                if (percentage > 100) percentage = 100;
                return percentage;
            },

            doSmoothScroll: function (e) {
                if (e.target.nodeName === 'A') {
                    e.preventDefault();
                    if (location.pathname.replace(/^\//, '') === e.target.pathname.replace(/^\//, '') && location.hostname === e.target.hostname) {
                        var targetStep = document.querySelector(e.target.hash);
                        targetStep ? _animation(targetStep) : console.warn('Hi! You should give an animation callback function to the Scroller module! :)');
                    }
                }
            },

            smoothScroll: function () {
                if (options.navigationContainer !== null) options.navigationContainer.addEventListener('click', this.doSmoothScroll);
            },

            checkActiveStep: function () {
                var scrollPosition = this.scrollPosition;

                [].forEach.call(options.steps, function (step) {
                    var bBox = step.getBoundingClientRect(),
                        position = step.offsetTop,
                        height = position + bBox.height;

                    if (scrollPosition >= position && scrollPosition < height && currentStep !== step) {
                        currentStep = step;
                        step.classList.add(self.currentStepClass);
                        if (typeof options.onStepChange === 'function') options.onStepChange(step);
                    }
                    step.classList.remove(options.currentStepClass);
                });
            },

            destroy: function () {
                window.removeEventListener('scroll', throttledGetScrollPosition);
                window.removeEventListener('resize', throttledGetScrollPosition);
                options.navigationContainer.removeEventListener('click', this.doSmoothScroll);
            }
        }
    })();
    //////////////////////
    // END scroll Module
    //////////////////////


    //////////////////////
    // APP init
    //////////////////////

    var scrollToTopBtn = document.querySelector('.Scroll-to-top'),
        steps = document.querySelectorAll('.js-scroll-step'),
        navigationContainer = document.querySelector('.Quick-navigation'),
        links = navigationContainer.querySelectorAll('a'),
        progressIndicator = document.querySelector('.Scroll-progress-indicator');

    ScrollManager.init({
        steps: steps,
        scrollToTopBtn: scrollToTopBtn,
        navigationContainer: navigationContainer,
        links: links,

        // Customize onScroll behavior
        onScroll: function () {
            var percentage = ScrollManager.scrollPercentage();
            percentage >= 90 ? scrollToTopBtn.classList.add('visible') : scrollToTopBtn.classList.remove('visible');

            if (percentage >= 10) {
                progressIndicator.innerHTML = percentage + "%";
                progressIndicator.classList.add('visible');
            } else {
                progressIndicator.classList.remove('visible');
            }
        },

        // Behavior when a step changes
        // default : highlight links 

        // onStepChange: function (step) {},

        // Customize the animation with jQuery, GSAP or velocity 
        // default : jQuery animate()
        // Eg with GSAP scrollTo plugin

        //smoothScrollAnimation: function (target) {
        //		TweenLite.to(window, 2, {scrollTo:{y:target}, ease:Power2.easeOut});
        //}

    });

    //////////////////////
    // END APP init
    //////////////////////

})();




/*
Product Page
////////////////////////////////////////////////////////////////
*/

// image
