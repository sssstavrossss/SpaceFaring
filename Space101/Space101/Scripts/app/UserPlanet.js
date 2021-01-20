$(document).ready(function () {

    let slideIndex = 1;

    $('.main-buttons a').on('click', function (e) {
        let href = $(e.target).parent().attr('data-href');
        $(`#${href}`).siblings().hide(100);
        $(`#${href}`).slideToggle(300);
    });

    $('.prev').on('click', function () { plusSlides(-1) } );
    $('.next').on('click', function () { plusSlides(1) } );
    $('.dot').on('click', function (e) { currentSlide(e) } );
    
    showSlides(slideIndex);

    // Next/previous controls
    function plusSlides(n) {
        showSlides(slideIndex += n);
    }

    // Thumbnail image controls
    function currentSlide(e) {
        showSlides(slideIndex = $(e.target).attr('data-image'));
    }

    function showSlides(n) {
        console.log(3);
        var i;
        var slides = $(".mySlides");
        var dots = $(".dot");
        if (n > slides.length) { slideIndex = 1 }
        if (n < 1) { slideIndex = slides.length }
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        for (i = 0; i < dots.length; i++) {
            dots[i].className = dots[i].className.replace(" active", "");
        }
        slides[slideIndex - 1].style.display = "block";
        dots[slideIndex - 1].className += " active";
    }

});