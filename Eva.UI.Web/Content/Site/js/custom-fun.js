$(document).ready(function () {
    "use strict";

    

    /* -------------------------------------------------------------------------*
     * GO TO TOP
     * -------------------------------------------------------------------------*/
    $.scrollUp();

    /* -------------------------------------------------------------------------*
     * MODAL BOXES
     * -------------------------------------------------------------------------*/
    $('.open-popup-link').magnificPopup({
        removalDelay: 500, //delay removal by X to allow out-animation
        callbacks: {
            beforeOpen: function () {
                this.st.mainClass = this.st.el.attr('data-effect');
            }
        },
        midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
    });
    $('#image-popups').magnificPopup({
        delegate: 'a',
        type: 'image',
        removalDelay: 500, //delay removal by X to allow out-animation
        callbacks: {
            beforeOpen: function () {
                // just a hack that adds mfp-anim class to markup 
                this.st.image.markup = this.st.image.markup.replace(
                  'mfp-figure', 'mfp-figure mfp-with-anim');
                this.st.mainClass = this.st.el.attr('data-effect');
            }
        },
        closeOnContentClick: true,
        midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
    });
    $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: true,
        fixedContentPos: false
    });

    /* -------------------------------------------------------------------------*
     * MASONRY
     * -------------------------------------------------------------------------*/
    var $container = $('.grid').masonry({
        itemSelector: '.masonry-item',
    });

    /* -------------------------------------------------------------------------*
     * MASONRY BLOG LINK NUDGING
     * -------------------------------------------------------------------------*/
    $('.masonry-item a.more').hover(function () { //mouse in
        $(this).animate({
            paddingLeft: '30px'
        }, 400);
    }, function () { //mouse out
        $(this).animate({
            paddingLeft: 15
        }, 400);
    });

    /* -------------------------------------------------------------------------*
     * SCROLL BAR
     * -------------------------------------------------------------------------*/
    /*var seq = 0;
    $("html").niceScroll({
        styler: "fb",
        cursorcolor: "#ff6600"
    });
    $(window).load(function () {
        setTimeout(function () {
            $("#gmbox div").animate({
                'top': 60
            }, 1500, "easeOutElastic");
        }, 1500);
    });*/

  
    /* -------------------------------------------------------------------------*
     * CALENDAR
     * -------------------------------------------------------------------------*/
    $('.single').pickmeup({
        flat: true
    });

    /* -------------------------------------------------------------------------*
     * SETTING DATE AND TIME
     * -------------------------------------------------------------------------*/
    var datetime = null,
      date = null;
    var update = function () {
        moment.locale('pt_br');
        date = moment(new Date());
        datetime.html(date.format('dddd, D MMMM  YYYY, HH:mm:ss'));
    };
    datetime = $('#time-date');
    update();
    setInterval(update, 1000);

   
    /* -------------------------------------------------------------------------*
     * SEARCH BAR
     * -------------------------------------------------------------------------*/
    // Hide search wrap by default;
    $(".search-container").hide();
    $(".toggle-search").on("click", function (e) {
        // Prevent default link behavior
        e.preventDefault();
        // Stop propagation
        e.stopPropagation();
        // Toggle search-wrap
        $(".search-container").slideToggle(500, function () {
            // Focus on the search bar
            // When animation is complete
            $("#search-bar").focus();
        });
    });
    // Close the search bar if user clicks anywhere
    $(document).click(function (e) {
        var searchWrap = $(".search-container");
        if (!searchWrap.is(e.target) && searchWrap.has(e.target).length === 0) {
            searchWrap.slideUp(500);
        }
    });

    /* -------------------------------------------------------------------------*
     * ADDING SLIDE UP AND ANIMATION TO DROPDOWN
     * -------------------------------------------------------------------------*/
    enquire.register("screen and (min-width:767px)", {
        match: function () {
            $(".dropdown").hover(function () {
                $('.dropdown-menu', this).stop().fadeIn("slow");
            }, function () {
                $('.dropdown-menu', this).stop().fadeOut("slow");
            });
        }
    });

    /* -------------------------------------------------------------------------*
     * DROPDOWN LINK NUDGING
     * -------------------------------------------------------------------------*/
    $('.dropdown-menu a').hover(function () { //mouse in
        $(this).animate({
            paddingLeft: '30px'
        }, 400);
    }, function () { //mouse out
        $(this).animate({
            paddingLeft: 20
        }, 400);
    });

    /* -------------------------------------------------------------------------*
     * HOT NEWS
     * -------------------------------------------------------------------------*/
    $('#js-news').ticker();
    

    /* -------------------------------------------------------------------------*
     * OWL CAROUSEL
     * -------------------------------------------------------------------------*/
    var time = 4; // time in seconds
    var $progressBar,
      $bar,
      $elem,
      isPause,
      tick,
      percentTime;
    var sync1 = $("#sync1");
    var sync2 = $("#sync2");
    sync1.owlCarousel({
        singleItem: true,
        slideSpeed: 1000,
        navigation: true,
        pagination: false,
        transitionStyle: "fadeUp",
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
        afterInit: progressBar,
        afterMove: moved,
        startDragging: pauseOnDragging
    });
    sync2.owlCarousel({
        items: 4,
        itemsDesktop: [1199,4],
        itemsDesktopSmall: [979, 3],
        itemsTablet: [768, 3],
        itemsMobile: [479, 3],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function (el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });

    function syncPosition(el) {
        var current = this.currentItem;
        $("#sync2").find(".owl-item").removeClass("synced").eq(current).addClass("synced");
        if ($("#sync2").data("owlCarousel") !== undefined) {
            center(current);
        }
    }
    $("#sync2").on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }
        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            }
            else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        }
        else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1]);
        }
        else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1);
        }
    }
    //Init progressBar where elem is $("#owl-demo")
    function progressBar(elem) {
        $elem = elem;
        //build progress bar elements
        buildProgressBar();
        //start counting
        start();
    }
    //create div#progressBar and div#bar then prepend to $("#owl-demo")
    function buildProgressBar() {
        $progressBar = $("<div>", {
            id: "progressBar"
        });
        $bar = $("<div>", {
            id: "bar"
        });
        $progressBar.append($bar).prependTo($elem);
    }

    function start() {
        //reset timer
        percentTime = 0;
        isPause = false;
        //run interval every 0.01 second
        tick = setInterval(interval, 10);
    };

    function interval() {
        if (isPause === false) {
            percentTime += 1 / time;
            $bar.css({width: percentTime + "%"});
            //if percentTime is equal or greater than 100
            if (percentTime >= 100) {
                //slide to next item
                $elem.trigger('owl.next');
            }
        }
    }
    //pause while dragging
    function pauseOnDragging() {
        isPause = true;
    }
    //moved callback
    function moved() {
        //clear interval
        clearTimeout(tick);
        //start again
        start();
    }
    //pause on mouseover
    $elem.on('mouseover', function() {isPause = true;});
    $elem.on('mouseout', function() { isPause = false; });
    $("#vid-thumbs").owlCarousel({
        navigation: false,
        pagination: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true
    });
    $("#owl-lifestyle").owlCarousel({
        autoPlay: false, //Set AutoPlay to 3 seconds
        navigation: true,
        pagination: false,
        items: 3,
        itemsDesktop: [1199,3],
        itemsDesktopSmall: [979, 2],
        itemsTablet: [768, 2],
        itemsMobile: [479, 1]
    });
    $("#owl-blog").owlCarousel({
        navigation: true,
        pagination: false,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true
    });

});

/* -------------------------------------------------------------------------*
* WEATHER
* -------------------------------------------------------------------------*/
$.simpleWeather({
    location: 'Porto Velho, RO',
    woeid: '',
    unit: 'c',
    success: function (weather) {
        html = '<i class="icon-' + weather.code + '"></i> ' + weather.city +
          ', ' + weather.region + ' ' + weather.temp + '&deg;' +
          weather.units.temp + ' ';
        $("#weather").html(html);
    },
    error: function (error) {
        $("#weather").html('<p>' + error + '</p>');
    }
});