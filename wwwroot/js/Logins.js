// Login 

var working = false;
$('.login').on('submit', function (e) {
    e.preventDefault();
    if (working) return;
    working = true;
    var $this = $(this),
        $state = $this.find('button > .state');
    $this.addClass('loading');
    $state.html('Authenticating');
    setTimeout(function () {
        $this.addClass('ok');
        $state.html('Welcome back!');
        setTimeout(function () {
            $state.html('Log in');
            $this.removeClass('ok loading');
            working = false;
        }, 4000);
    }, 3000);
});

// Table


$(document).ready(function () {

    var table = $('#table');

    // Table bordered
    $('#table-bordered').change(function () {
        var value = $(this).val();
        table.removeClass('table-bordered').addClass(value);
    });

    // Table striped
    $('#table-striped').change(function () {
        var value = $(this).val();
        table.removeClass('table-striped').addClass(value);
    });

    // Table hover
    $('#table-hover').change(function () {
        var value = $(this).val();
        table.removeClass('table-hover').addClass(value);
    });

    // Table color
    $('#table-color').change(function () {
        var value = $(this).val();
        table.removeClass(/^table-mc-/).addClass(value);
    });

    $("input[Id=Input_FullName]").keyup(function (event) {
        if (!(event.keyCode >= 37 && event.keyCode <= 40)) {
            var inputVal = $(this).val();
            $(this).val(inputVal.replace(/[^a-z0-9]/gi, ''));
        }
    });

});

// jQuery’s hasClass and removeClass on steroids
// by Nikita Vasilyev
// https://github.com/NV/jquery-regexp-classes
(function (removeClass) {

    jQuery.fn.removeClass = function (value) {
        if (value && typeof value.test === "function") {
            for (var i = 0, l = this.length; i < l; i++) {
                var elem = this[i];
                if (elem.nodeType === 1 && elem.className) {
                    var classNames = elem.className.split(/\s+/);

                    for (var n = classNames.length; n--;) {
                        if (value.test(classNames[n])) {
                            classNames.splice(n, 1);
                        }
                    }
                    elem.className = jQuery.trim(classNames.join(" "));
                }
            }
        } else {
            removeClass.call(this, value);
        }
        return this;
    }

})(jQuery.fn.removeClass);

//jQuery(function ($) {

//    $("input[Id=Input_FullName]").keyup(function (event) {
//        if (!(event.keyCode >= 37 && event.keyCode <= 40)) {
//            var inputVal = $(this).val();
//            $(this).val(inputVal.replace(/[^a-z0-9]/gi, ''));
//        }
//    });

//})(jQuery);