


function showSlides(index, className) {
    var i;
    var slides = document.getElementsByClassName(className);
    var slideLength = slides.length;
    var slideIndex = index;

    if (slideIndex > slideLength) { 
        slideIndex = 0; 
    }

    if (slideIndex < 0) { 
        slideIndex = slideLength; 
    }

    for (i = 0; i < slideLength; i++) {
        if(slides[i].classList.contains('show') == true) {
            slides[slideIndex].classList.remove('show');    
        }
        if(slides[i].classList.contains('hide') == false) {
            slides[i].classList.add('hide');
        }
    }

    if(slides[i].classList.contains('show') == false) {
        slides[slideIndex].classList.add('show');    
    }
    if(slides[i].classList.contains('hide') == true) {
        slides[i].classList.remove('hide');
    }

    return slideIndex;
}






// Helper function to get an element's exact position
function getPosition(el) {
    var xPos = 0;
    var yPos = 0;

    while (el) {
    if (el.tagName == "BODY") {
        // deal with browser quirks with body/window/document and page scroll
        var xScroll = el.scrollLeft || document.documentElement.scrollLeft;
        var yScroll = el.scrollTop || document.documentElement.scrollTop;

        xPos += (el.offsetLeft - xScroll + el.clientLeft);
        yPos += (el.offsetTop - yScroll + el.clientTop);
    } else {
        // for all other non-BODY elements
        xPos += (el.offsetLeft - el.scrollLeft + el.clientLeft);
        yPos += (el.offsetTop - el.scrollTop + el.clientTop);
    }

    el = el.offsetParent;
    }
    return {
    x: xPos,
    y: yPos
    };
};



function autoSlideShow(index, className) {
    var slides = document.getElementsByClassName(className);
    var slideLength = slides.length;
    var i = 0;
    var autoSlideShowIndex = index;

    for(i = 0; i < slideLength; i++) {
        if(slides[i].classList.contains('fade-in-out-3s') == true) {
            slides[i].classList.remove('fade-in-out-3s');
        }
        if(slides[i].classList.contains('hide') == false) {
            slides[i].classList.add('hide');
        }
        
    }

    if(autoSlideShowIndex >= slideLength) {
        autoSlideShowIndex = 0;
    }

    if(slides[autoSlideShowIndex].classList.contains('fade-in-out-3s') == false) {
        slides[autoSlideShowIndex].classList.add('fade-in-out-3s');
        if(slides[autoSlideShowIndex].classList.contains('hide') == true) {
            slides[autoSlideShowIndex].classList.remove('hide');
        }
    }

    autoSlideShowIndex++;

    return autoSlideShowIndex;
};



var showSection = function (strQuerySelectorStart, strQuerySelectorEnd, strSelectedQuery) {

    var startPoint = document.body.querySelector(strQuerySelectorStart);
    var endPoint = document.body.querySelector(strQuerySelectorEnd);
    var selectedQuery = document.body.querySelector(strSelectedQuery);
    var sectionShown;


    if (!startPoint || !endPoint) {
        return;
    }

    var positionStart = getPosition(startPoint);
    var endPosition = getPosition(endPoint);

    if(window.scrollY < endPosition.y) {
        if(selectedQuery.classList.contains('hide-fade') == false) {
            selectedQuery.classList.add('hide-fade');
            // alert("Removed show-fade");
            
        }
        if(selectedQuery.classList.contains('show-fade-4s') == true) {
            selectedQuery.classList.remove('show-fade-4s');
        }
        
        sectionShown = false;
    }
    else if (window.scrollY > positionStart.y) {

        if(selectedQuery.classList.contains('show-fade-3s') == false) {
            selectedQuery.classList.add('show-fade-3s');
            // alert("Removed hide-fade");
        }
        if(selectedQuery.classList.contains('hide-fade') == true) {
            selectedQuery.classList.remove('hide-fade');
        }
        sectionShown = true;
        

    } else {
        
        
        if(selectedQuery.classList.contains('hide-fade') == false) {
            selectedQuery.classList.add('hide-fade');
            // alert("Removed show-fade");
            
        }
        if(selectedQuery.classList.contains('show-fade-3s') == true) {
            selectedQuery.classList.remove('show-fade-3s');
        }
        
        sectionShown = false;

    }


    return sectionShown;

};


window.addEventListener('DOMContentLoaded', event => {
    
    
    

    

    function showThings() {

        showSection('#showXp','#showXp','#work');
        showSection('#projectsShowPoint','#projectsShowPoint','#projects');
    }
    document.addEventListener('scroll', showThings);
    showThings();

});

var autoSlideShowIndex1 = 0;
var autoSlideShowIndex2 = 0;
function blinkArrows() {

    autoSlideShowIndex1 = autoSlideShow(autoSlideShowIndex1,"arrow_next_xp");
    autoSlideShowIndex2 = autoSlideShow(autoSlideShowIndex2,"arrow_next_projects");
}

var projectIndex = 0;

function projectSlides() {
    projectIndex = autoSlideShow(projectIndex,"project_slides");
}

var titleIndex1 = 0;
var titleIndex2 = 0;

function moveThroughTitles() {
    titleIndex1 = autoSlideShow(titleIndex1,"slide_title_head");
    titleIndex2 = autoSlideShow(titleIndex2,"slide_title_head");
}


moveThroughTitles();
window.setInterval(moveThroughTitles,3000);




projectSlides();
window.setInterval(projectSlides,3500);