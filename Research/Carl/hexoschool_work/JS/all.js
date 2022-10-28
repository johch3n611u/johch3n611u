$(document).ready(function() {
    course_slidedown();
    subscription();
    var swiper = new Swiper('.swiper-container', {
        effect: 'cube',
        grabCursor: true,
        cubeEffect: {
            shadow: true,
            slideShadows: true,
            shadowOffset: 20,
            shadowScale: 0.94,
        },
        pagination: {
            el: '.swiper-pagination',
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        autoplay: {
            delay: 3000,
            disableOnInteraction: false,
        },
    });
    // lightbox2
    lightbox.option({
        'resizeDuration': 200,
        'wrapAround': true,
        'positionFromTop': 150,
    })
    $('.top a').click(function(e) {
        e.preventDefault();
        $('html,body').animate({ scrollTop: 0 }, 700);
    });
});

function course_slidedown() {
    $('.course-slide').click(function(event) {
        event.preventDefault();
        $('.course-slide').toggleClass('active');
        $('.course-open li').slideToggle();
    });
}

function subscription() {
    $(".question,.banner-button").click(function(event) {
        event.preventDefault();
        $('.course-slide').removeClass('active');
        $('.course-open li').fadeOut();
        $('.subscription').fadeIn().css('display', 'flex');
    });
}


function cancel(event) {
    $('.subscription').fadeOut();
}

function success(event) {
    cancel();
    $('.success').fadeIn(1500).fadeOut(1500);
}