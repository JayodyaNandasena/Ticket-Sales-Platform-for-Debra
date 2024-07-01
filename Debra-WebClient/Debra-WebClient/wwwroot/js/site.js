// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showTicketDetails() {
    console.log("Hi");
    try {
        document.querySelector(".ticket-details").style.display = "block";
    } catch {
        console.log("Not Found");
    }
    
    
}


