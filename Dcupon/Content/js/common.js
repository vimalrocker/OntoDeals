
jQuery(document).ready(function () {
    "use strict";
    jQuery('.toggle').click(function () {
        if (jQuery('.submenu').is(":hidden")) {
            jQuery('.submenu').slideDown("fast");
        } else {
            jQuery('.submenu').slideUp("fast");
        }
        return false;
    });


    $("input[data-autocomplete]").focus(function () {

        $(this).each(function () {

            $(this).autocomplete({
                source: function (request, response) {
                    $.ajax({

                        dataType: "json",
                        data: {
                            q: request.term
                        },
                        url: "/Home/SearchList?term=" + request.term,
                        success: function (data) {

                            response(data);



                        }
                    });
                }
            });

        });
    });




    /*Phone Menu*/
    jQuery(".topnav").accordion({
        accordion: false,
        speed: 300,
        closedSign: '+',
        openedSign: '-'
    });

    $("#nav > li").hover(function () {
        var el = $(this).find(".level0-wrapper");
        el.hide();
        el.css("left", "0");
        el.stop(true, true).delay(150).fadeIn(300, "easeOutCubic");
    }, function () {
        $(this).find(".level0-wrapper").stop(true, true).delay(300).fadeOut(300, "easeInCubic");
    });
    var scrolled = false;

    jQuery("#nav li.level0.drop-menu").mouseover(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul.level1').fadeIn(100);
        }
        return false;
    }).mouseleave(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul.level1').fadeOut(100);
        }
        return false;
    });
    jQuery("#nav li.level0.drop-menu li").mouseover(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul').css({ top: 0, left: "165px" });
            var offset = jQuery(this).offset();
            if (offset && (jQuery(window).width() < offset.left + 325)) {
                jQuery(this).children('ul').removeClass("right-sub");
                jQuery(this).children('ul').addClass("left-sub");
                jQuery(this).children('ul').css({ top: 0, left: "-167px" });
            } else {
                jQuery(this).children('ul').removeClass("left-sub");
                jQuery(this).children('ul').addClass("right-sub");
            }
            jQuery(this).children('ul').fadeIn(100);
        }
    }).mouseleave(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul').fadeOut(100);
        }
    });

    jQuery("#best-seller-slider .slider-items").owlCarousel({
        items: 4, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });

    jQuery("#featured-slider .slider-items").owlCarousel({
        items: 5, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#bag-seller-slider .slider-items").owlCarousel({
        items: 3, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#shoes-slider .slider-items").owlCarousel({
        items: 8, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#recommend-slider .slider-items").owlCarousel({
        items: 6, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#brand-logo-slider .slider-items").owlCarousel({
        autoplay: true,
        items: 6, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#category-desc-slider .slider-items").owlCarousel({
        autoplay: true,
        items: 1, //10 items above 1000px browser width
        itemsDesktop: [1024, 1], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 1], // 3 items betweem 900px and 601px
        itemsTablet: [600, 1], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#more-views-slider .slider-items").owlCarousel({
        autoplay: true,
        items: 3, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    $("#related-products-slider .slider-items").owlCarousel({
        items: 4, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    $("#upsell-products-slider .slider-items").owlCarousel({
        items: 4, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false
    });
    jQuery("#more-views-slider .slider-items").owlCarousel({
        autoplay: true,
        items: 3, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1024px and 901px
        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0;
        itemsMobile: [320, 1],
        navigation: true,
        navigationText: ["<a class=\"flex-prev\"></a>", "<a class=\"flex-next\"></a>"],
        slideSpeed: 500,
        pagination: false

    });
    jQuery("ul.accordion li.parent, ul.accordion li.parents, ul#magicat li.open").each(function () {
        jQuery(this).append('<em class="open-close">&nbsp;</em>');
    });

    jQuery('ul.accordion, ul#magicat').accordionNew();

    jQuery("ul.accordion li.active, ul#magicat li.active").each(function () {
        jQuery(this).children().next("div").css('display', 'block');
    });
});

var isTouchDevice = ('ontouchstart' in window) || (navigator.msMaxTouchPoints > 0);
jQuery(window).on("load", function () {

    if (isTouchDevice) {
        jQuery('#nav a.level-top').click(function (e) {
            $t = jQuery(this);
            $parent = $t.parent();
            if ($parent.hasClass('parent')) {
                if (!$t.hasClass('menu-ready')) {
                    jQuery('#nav a.level-top').removeClass('menu-ready');
                    $t.addClass('menu-ready');
                    return false;
                }
                else {
                    $t.removeClass('menu-ready');
                }
            }
        });
    }
    //on load
    jQuery().UItoTop();


}); //end: on load

//]]>

//$(window).scroll(function() {
//if ($(this).scrollTop() > 1){  
//$('nav').addClass("sticky");
//}
//else{
//$('nav').removeClass("sticky");
//}
//});

/*--------| UItoTop jQuery Plugin 1.1-------------------*/
(function ($) {
    jQuery.fn.UItoTop = function (options) {

        var defaults = {
            text: '',
            min: 200,
            inDelay: 600,
            outDelay: 400,
            containerID: 'toTop',
            containerHoverID: 'toTopHover',
            scrollSpeed: 1200,
            easingType: 'linear'
        };

        var settings = $.extend(defaults, options);
        var containerIDhash = '#' + settings.containerID;
        var containerHoverIDHash = '#' + settings.containerHoverID;

        jQuery('body').append('<a href="#" id="' + settings.containerID + '">' + settings.text + '</a>');
        jQuery(containerIDhash).hide().click(function () {
            jQuery('html, body').animate({ scrollTop: 0 }, settings.scrollSpeed, settings.easingType);
            jQuery('#' + settings.containerHoverID, this).stop().animate({ 'opacity': 0 }, settings.inDelay, settings.easingType);
            return false;
        })
        .prepend('<span id="' + settings.containerHoverID + '"></span>')
        .hover(function () {
            jQuery(containerHoverIDHash, this).stop().animate({
                'opacity': 1
            }, 600, 'linear');
        }, function () {
            jQuery(containerHoverIDHash, this).stop().animate({
                'opacity': 0
            }, 700, 'linear');
        });

        jQuery(window).scroll(function () {
            var sd = $(window).scrollTop();
            if (typeof document.body.style.maxHeight === "undefined") {
                jQuery(containerIDhash).css({
                    'position': 'absolute',
                    'top': $(window).scrollTop() + $(window).height() - 50
                });
            }
            if (sd > settings.min)
                jQuery(containerIDhash).fadeIn(settings.inDelay);
            else
                jQuery(containerIDhash).fadeOut(settings.Outdelay);
        });

    };
})(jQuery);


/*--------| End UItoTop -------------------*/

function deleteCartInCheckoutPage() {
    $(".checkout-cart-index a.btn-remove2,.checkout-cart-index a.btn-remove").click(function (event) {
        event.preventDefault();
        if (!confirm(confirm_content)) {
            return false;
        }
    });
    return false;
}
function slideEffectAjax() {
    $('.top-cart-contain').mouseenter(function () {
        $(this).find(".top-cart-content").stop(true, true).slideDown();
    });

    $('.top-cart-contain').mouseleave(function () {
        $(this).find(".top-cart-content").stop(true, true).slideUp();
    });
}
function deleteCartInSidebar() {
    if (is_checkout_page > 0) return false;
    $('#cart-sidebar a.btn-remove, #mini_cart_block a.btn-remove').each(function () { });
}

$(document).ready(function () {
    slideEffectAjax();
});


/*-------- End Cart js -------------------*/


jQuery.extend(jQuery.easing,
	{
	    easeInCubic: function (x, t, b, c, d) {
	        return c * (t /= d) * t * t + b;
	    },
	    easeOutCubic: function (x, t, b, c, d) {
	        return c * ((t = t / d - 1) * t * t + 1) + b;
	    },
	});

(function (jQuery) {
    jQuery.fn.extend({
        accordion: function () {
            return this.each(function () {

                function activate(el, effect) {
                    jQuery(el).siblings(panelSelector)[(effect || activationEffect)](((effect == "show") ? activationEffectSpeed : false), function () {
                        jQuery(el).parents().show();
                    });
                }
            });
        }
    });
})(jQuery);

jQuery(function ($) {
    $('.accordion').accordion();
    $('.accordion').each(function (index) {
        var activeItems = $(this).find('li.active');
        activeItems.each(function (i) {
            $(this).children('ul').css('display', 'block');
            if (i == activeItems.length - 1) {
                $(this).addClass("current");
            }
        });
    });

});



/*-------- End Nav js -------------------*/
/*============= Responsive Nav =============*/
(function ($) {
    $.fn.extend({
        accordion: function (options) {
            var defaults = {
                accordion: 'true',
                speed: 300,
                closedSign: '[+]',
                openedSign: '[-]'
            };
            var opts = $.extend(defaults, options);
            var $this = $(this);
            $this.find("li").each(function () {
                if ($(this).find("ul").size() != 0) {
                    $(this).find("a:first").after("<em>" + opts.closedSign + "</em>");
                    if ($(this).find("a:first").attr('href') == "#") {
                        $(this).find("a:first").click(function () { return false; });
                    }
                }
            });
            $this.find("li em").click(function () {
                if ($(this).parent().find("ul").size() != 0) {
                    if (opts.accordion) {
                        //Do nothing when the list is open
                        if (!$(this).parent().find("ul").is(':visible')) {
                            parents = $(this).parent().parents("ul");
                            visible = $this.find("ul:visible");
                            visible.each(function (visibleIndex) {
                                var close = true;
                                parents.each(function (parentIndex) {
                                    if (parents[parentIndex] == visible[visibleIndex]) {
                                        close = false;
                                        return false;
                                    }
                                });
                                if (close) {
                                    if ($(this).parent().find("ul") != visible[visibleIndex]) {
                                        $(visible[visibleIndex]).slideUp(opts.speed, function () {
                                            $(this).parent("li").find("em:first").html(opts.closedSign);
                                        });
                                    }
                                }
                            });
                        }
                    }
                    if ($(this).parent().find("ul:first").is(":visible")) {
                        $(this).parent().find("ul:first").slideUp(opts.speed, function () {
                            $(this).parent("li").find("em:first").delay(opts.speed).html(opts.closedSign);
                        });
                    } else {
                        $(this).parent().find("ul:first").slideDown(opts.speed, function () {
                            $(this).parent("li").find("em:first").delay(opts.speed).html(opts.openedSign);
                        });
                    }
                }
            });
        }
    });
})(jQuery);

/*============= End Responsive Nav =============*/

(function (jQuery) {
    jQuery.fn.extend({
        accordionNew: function () {
            return this.each(function () {
                var jQueryul = jQuery(this),
                elementDataKey = 'accordiated',
                activeClassName = 'active',
                activationEffect = 'slideToggle',
                panelSelector = 'ul, div',
                activationEffectSpeed = 'fast',
                itemSelector = 'li';
                if (jQueryul.data(elementDataKey))
                    return false;
                jQuery.each(jQueryul.find('ul, li>div'), function () {
                    jQuery(this).data(elementDataKey, true);
                    jQuery(this).hide();
                });
                jQuery.each(jQueryul.find('em.open-close'), function () {
                    jQuery(this).click(function (e) {
                        activate(this, activationEffect);
                        return void (0);
                    });
                    jQuery(this).bind('activate-node', function () {
                        jQueryul.find(panelSelector).not(jQuery(this).parents()).not(jQuery(this).siblings()).slideUp(activationEffectSpeed);
                        activate(this, 'slideDown');
                    });
                });
                var active = (location.hash) ? jQueryul.find('a[href=' + location.hash + ']')[0] : jQueryul.find('li.current a')[0];
                if (active) {
                    activate(active, false);
                }
                function activate(el, effect) {
                    jQuery(el).parent(itemSelector).siblings().removeClass(activeClassName).children(panelSelector).slideUp(activationEffectSpeed);
                    jQuery(el).siblings(panelSelector)[(effect || activationEffect)](((effect == "show") ? activationEffectSpeed : false), function () {
                        if (jQuery(el).siblings(panelSelector).is(':visible')) {
                            jQuery(el).parents(itemSelector).not(jQueryul.parents()).addClass(activeClassName);
                        } else {
                            jQuery(el).parent(itemSelector).removeClass(activeClassName);
                        }
                        if (effect == 'show') {
                            jQuery(el).parents(itemSelector).not(jQueryul.parents()).addClass(activeClassName);
                        }
                        jQuery(el).parents().show();
                    });
                }
            });
        }
    });
})(jQuery);


/*============= End Left Nav =============*/

jQuery(document).ready(function () {
    $('.actions a[data-cupon-type="1"]').each(function () {
        $(this).text('View Code');

    });

    $('.actions a.coupon-popover').click(function (e) {
        var couponType = $(this).attr('data-cupon-type');
        var couponcode = $(this).attr('data-cupon-code');
        var coupondescription = $(this).attr('data-coupon-description');
        var coupontitle = $(this).parents('.col-item').find('.item-title a').text();
        var couponwebsite = $(this).attr('data-coupon-website');
        var url = document.location;
        var setutl = 'http://ontodeals.com/' + 'Coupons?src=' + couponwebsite;
        var vendorurl = $(this).attr('href');

        if (couponType == '1') {
            e.preventDefault();



            $('#dealPopup .modal-title').text('here is your coupon code');
            $('.coupon-code').text(couponcode);
            $('.coupon-code').attr('id', 'copy-button');
            $('.coupon-code').append('<span id="copy-t">copy</span>');
            $('#copy-t').attr('data-clipboard-text', couponcode);
            $('.coupon-title').text(coupontitle);
            $('.coupon-description').text(coupondescription);
            $('.redirect-vendor a').text(couponwebsite).attr('href', vendorurl).attr('target', '_blank');;
            $('.offerby').html('View all <a href="' + setutl + '">' + couponwebsite + '</a> Offers and Deals');


        }
        else {


            $('#dealPopup .modal-title').text('Deal Activated');
            $('.coupon-code').text('No need of coupon code deal activated automatically');
            $('.coupon-title').text(coupontitle);
            $('.coupon-description').text(coupondescription);
            $('.redirect-vendor a').text(couponwebsite).attr('href', vendorurl).attr('target', '_blank');
            $('.offerby').html('View all <a href="' + setutl + '">' + couponwebsite + '</a> Offers and Deals');

        }

        $('#dealPopup').modal();
    });

    // $('.userError').fadeOut(0);

    if ($('#Error').size() != 0) {

        $('#myModal').modal();
        $('.userError').fadeIn();
    }

    else {
        $('.userError').fadeOut(0);
    }

    $('#myModal').on('hidden.bs.modal', function (e) {
        $('.userError').fadeOut(0);
    });

    $('#show-img').removeAttr('style');
    $('#show-img').removeAttr('width');
    $('#show-img').removeAttr('height');



    $('.userlogin').click(function () {
        if ($('#myModal #Email').val() != '' && $('#myModal #Password').val() != '') {

            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Customer/Login?email=" + $('#Email').val() + '&password=' + $('#Password').val(),
                beforeSend: function () {
                    $('.userlogin').text('Please wait...');
                    $('.userlogin').attr('disabled', 'disabled');
                },
                success: function (data) {
                    var obj = jQuery.parseJSON(data);


                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.userlogin').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.userlogin').text('Login')
                    }

                    else {
                        $('.userError').fadeOut();

                        document.location.href = "http://" + window.location.host + "/Customer/DashBoard";
                    }
                }
            });

        }

        else {
            $('#myModal #Email,#myModal #Password').focus()

        }
    });
    //$('.cust_login').on("click", "a", function (e) {
    $('.cust_login').click(function () {
        //alert("click");
        if ($('#userReg #Name').val() != '' && $('#userReg #Email').val() != '' && $('#userReg #Password').val() != '' && $('#userReg #Password').val() != '') {

            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Customer/Register?name=" + $('#userReg #Name').val() + '&email=' + $('#userReg #Email').val() + '&password=' + $('#userReg #Password').val() + '&source=' + 'OntoDeals',
                beforeSend: function () {
                    $('.cust_login').text('Please wait...');
                    $('.cust_login').attr('disabled', 'disabled');
                },
                success: function (data) {
                    var obj = jQuery.parseJSON(data);



                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.cust_login').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.cust_login').text('Register')
                    }

                    else {
                        $('.userError').fadeOut();

                        document.location.href = "http://" + window.location.host + "/Customer/DashBoard";
                    }


                }
            })

        }

        else {
            $('#userReg #Name,#userReg #Email,#userReg #Password').focus()

        }
       // e.preventDefault();
    });

    
    $('.forgot-pass-submit').click(function () {

        if ($('#forgotemail').val() != '') {

            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Customer/ForgotPassword?email=" + $('#forgotemail').val(),
                beforeSend: function () {
                    $('.forgot-pass-submit').text('Please wait...');
                    $('.forgot-pass-submit').attr('disabled', 'disabled');
                },
                success: function (data) {
                    var obj = jQuery.parseJSON(data);



                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.forgot-pass-submit').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.forgot-pass-submit').text('Submit')
                    }

                    else {
                        $('.userError').text(obj.Message);
                        $('#forgotpass .userError').removeClass('alert-danger').addClass('alert-success').fadeIn();
                        $('.forgot-pass-submit').text('Mail Sent..');
                        $('.forgot-pass-submit').attr('disabled', 'disabled');
                    }


                }
            })
        }

        else {
            $('#forgotemail').focus().trigger('change');
        }

    })


    $('.pass-reset').click(function () {

        if ($('#renew-pass').val() != '') {

            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Customer/UserPasswordReset?newpassword=" + $('#renew-pass').val() + "&token=" + $('#token').val() + "&email=" + $('#email').val(),
                beforeSend: function () {
                    $('.pass-reset').text('Please wait...');
                    $('.pass-reset').attr('disabled', 'disabled');
                },
                success: function (data) {
                    var obj = jQuery.parseJSON(data);



                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.pass-reset').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.pass-reset').text('Reset Password')
                    }

                    else {
                        $('.userError').text(obj.Message);
                        $('.userError').removeClass('alert-danger').addClass('alert-success').fadeIn();

                    }


                }
            })
        }

        else {
            $('#new-pass').focus().trigger('change');
        }

    })

    //$('.panel-body').html('<img src="/Content/images/adss.jpg" id="show-img" >')

    $('#myTab').tab('show');

    $('.close').click(function () {

        $('.userError').fadeOut();

    });




    $('.main').click(function (e) {



        var size = $(this).parent().find('em').size();

        if (size != 0 && $(window).outerWidth() < 750) {

            $(this).next('em').click();


        }


    })



});