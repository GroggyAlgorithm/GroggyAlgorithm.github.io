// JavaScript source code
"use strict";



document.addEventListener("DOMContentLoaded", () => {
	const observer = new IntersectionObserver(entries => {
		//console.log(entries[0].intersectionRatio); Used this to see the location on scroll, so when less than 0.9 location, toggle bar--bg class
		document.querySelector(".navbar").classList.toggle("navbar--close", entries[0].intersectionRatio < 0.9);
	}, {
			threshold: 0.9 //As soon as there's 90%  visible, execute 
	})

	observer.observe(document.querySelector(".header")); //pass in the header class to be observed and this code will be done to it
});
