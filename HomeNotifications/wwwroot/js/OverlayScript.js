$(document).on("click", "#overlay-background", function (event) {
    if (event.target.id == "overlay-background") {
        off();
    }
});

function off() {
    document.getElementById("overlay-background").style.display = "none";
}

function on() {
    document.getElementById("overlay-background").style.display = "block";
}