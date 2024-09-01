$(document).on("click", "#overlay-background", function (event) {
    if (event.target.id == "overlay-background") {
        overlayOff();
    }
});

function overlayOff() {
    document.getElementById("overlay-background").style.display = "none";
}

function overlayOn() {
    document.getElementById("overlay-background").style.display = "block";
}