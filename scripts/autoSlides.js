// JavaScript source code
"use strict";
var autoSlideIndex = 0;
AutoSlides(autoSlideIndex);



function AutoSlides(n) {
    var i;
    var slides = document.getElementsByClassName("auto-slides");
    var slideLength = slides.length;


    for (i = 0; i < slideLength; i++) {
        slides[i].style.display = "none";
    }



    autoSlideIndex++;


    if (autoSlideIndex > slideLength) { autoSlideIndex = 1; }

    slides[autoSlideIndex - 1].style.display = "block";

    setTimeout(AutoSlides, 3000); // time out for passed # of seconds
}