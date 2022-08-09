// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    //HERO SLIDER
    $('#hero-slider').owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        items: 1,
        dots: false,
        smartSpeed: 1000,
        navText: ['PREV', 'NEXT'],
        responsive: {
            0: {
                nav: false
            },
            768: {
                nav: true
            }
        }
    })
})