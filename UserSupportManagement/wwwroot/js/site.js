// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function myFunction() {
    // Get the checkbox
    var checkBox = document.getElementById("myCheck");
    // Get the output text
    var inputbox1 = document.getElementById("inputbox1");
    var inputbox2 = document.getElementById("inputbox2");
    var inputbox3 = document.getElementById("inputbox3");

    // If the checkbox is checked, display the output inputbox
    if (checkBox.checked == true) {
        inputbox1.style.display = "block";
        inputbox2.style.display = "block";
        inputbox3.style.display = "block";
    } else {
        inputbox1.style.display = "none";
        inputbox2.style.display = "none";
        inputbox3.style.display = "none";
    }
} 
