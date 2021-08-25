// JavaScript source code

"use strict";


var slideIndex = 1;
ShowSlides(slideIndex);


// Next/previous controls
function plusSlides(n) {
    


    ShowSlides(slideIndex += n);
    
}

// Thumbnail image controls
function currentSlide(n) {
    
    ShowSlides(slideIndex = n);
}



function ShowSlides(n) {
    var i;
    var slides = document.getElementsByClassName("slides");
    var slideLength = slides.length;
    var dots = document.getElementsByClassName("slide-dots");

    if (n > slideLength) { slideIndex = 1; }

    if (n < 1) { slideIndex = slideLength; }

    for (i = 0; i < slideLength; i++) {
        slides[i].style.display = "none";
    }

    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}













