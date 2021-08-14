// JavaScript source code

"use strict";



var slideBox = document.getElementById("slideBox"); 

var btnLeft = document.getElementById("btnRight");

var Right = document.getElementById("btnLeft");





function jump() {
	//If the player does not have the class on it..
	if (slideBox.classList != "animate-slide") {
		slideBox.classList.add("animate-slide"); //add the slide class from the css
	}
	setTimeout(function () { //Then, we declate set timeout function and that removes the animation 
		//After....
		slideBox.classList.remove("animate-slide");
	}, 800) //runs every 500ms
}



