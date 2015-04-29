var hidemenu;

/* set Cookie to hide menu */
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

/* get Cookie to check hide menu */
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

jQuery(document).ready(function() {

    'use strict';

    /* Contact us Form Submit */
    $('#contactsubmit').bind('click', function() {
        $.ajax({
            url: 'contact.php',
            type: 'POST',
            data: $('#contactform').serialize(),
            success: function(data) {
                if (data == 'Sent Success') {
                    $('#formmsg').addClass('mag-alert-scc').show().find('span.error').html('Thank you for contact us');
                } else {
                    $('#formmsg').addClass('mag-alert-dngr').show().find('span.error').html(data);
                }
            }
        });
        return false;
    });
    /* Toggle scroll menu */
    $('#hidemenu').bind('click', function() {
        var nav = $('.main-menu');
        nav.removeClass("f-nav");
        $(this).hide();
        hidemenu = true;
        setCookie('hidemenu', 'hide', 10);
    });

    $('#showmenu').bind('click', function() {
        var nav = $('.main-menu');
        nav.addClass("f-nav");
        $(this).show();
        hidemenu = false;
        setCookie('hidemenu', 'hide', -10);
    });

    /* Toolbar Menu */
    $('#main-menu-items').smartmenus();

    /* Home Big Boxs Hover caption JS */
    $('.boxgrid.caption').hover(function() {
        $(".cover", this).stop().animate({
            top: '70px'
        }, {
            queue: false,
            duration: 183
        });
    }, function() {
        $(".cover", this).stop().animate({
            top: '142px'
        }, {
            queue: false,
            duration: 153
        });
    });

    $('.boxgrid2.caption').hover(function() {
        $(".cover", this).stop().animate({
            top: '270px'
        }, {
            queue: false,
            duration: 183
        });
    }, function() {
        $(".cover", this).stop().animate({
            top: '366px'
        }, {
            queue: false,
            duration: 153
        });
    });

    $('.boxgrid3.caption').hover(function() {
        $(".cover", this).stop().animate({
            top: '145px'
        }, {
            queue: false,
            duration: 183
        });
    }, function() {
        $(".cover", this).stop().animate({
            top: '202px'
        }, {
            queue: false,
            duration: 153
        });
    });

    /* News ticker JQ */
    $('.newsticker').newsTicker({
        row_height: 40,
        max_rows: 1,
        speed: 500,
        pauseOnHover: 1,
        prevButton: $('#tkr-prev'),
        nextButton: $('#tkr-nxt'),
        stopButton: $('#tkr-stop')
    });

    /* Flickr Feed */
    $('#basicuse').jflickrfeed({
        limit: 9,
        qstrings: {
            id: '80919450@N00'
        },
        itemTemplate: '<a href="{{image_b}}"><img src="{{image_m}}" alt="{{title}}" /></a>'
    });

    /* Right Side Calender */
    var cal = CALENDAR();

    cal.init();


    /* Tooltips */
    $('.mag-info a').tooltip();

    /* Selectors */
    $('select.cust-slctr').customSelect();

});

/* ===== Sliders ===== */

$(window).load(function() {
    $('.flexslider.hm-slider').flexslider({
        animation: 'fade',
        controlNav: false,
        prevText: "",
        nextText: ""

    });

    $('.flexslider.sm-sldr').flexslider({
        animation: 'slide',
        controlNav: false,
        slideshowSpeed: 2000,
        animationSpeed: 2500,
        prevText: "",
        nextText: ""
    });

    $('.flexslider.news-sldr').flexslider({
        animation: 'slide',
        controlNav: false,
        pauseText: '',
        itemWidth: 183,
        itemMargin: 0,
        slideshowSpeed: 4000,
        animationSpeed: 2500,
        prevText: "",
        nextText: ""

    });

    $('.flexslider.img-sm-gal').flexslider({
        animation: 'slide',
        controlNav: true,
        directionNav: false,
        pauseText: '',
        itemWidth: 79,
        itemMargin: 15,
        slideshowSpeed: 6000,
        animationSpeed: 2500,
        slideshow: false,
        prevText: "",
        nextText: ""

    });


    $('.flexslider.vid-thmb').flexslider({
        animation: 'fade',
        controlNav: false,
        itemWidth: 166,
        itemMargin: 10,
        slideshowSpeed: 4000,
        animationSpeed: 2500,
        prevText: "",
        nextText: ""

    });


});


/* This is for the Fixed Menu on scroll */
var nav = $('.main-menu');

$(window).scroll(function() {
    var hidecookie = getCookie('hidemenu');
    if ($(this).scrollTop() > 160 && hidemenu != true && hidecookie != 'hide') {
        nav.addClass("f-nav");
        $('#hidemenu').show();
    } else {
        nav.removeClass("f-nav");
        $('#hidemenu').hide();
    }
});