function showNotificationForm() {
    overlayOn();

    $(document).on("click", "#notification-form-button", function () {
        $.ajax({
            type: "GET",
            url: "/Index?handler=FormPartial",
            success: function (data) {
                $("#notification-form").html(data);
            }
        });
    });
}

function postNotificationForm() {
    $(document).on("click", "#notification-submit", function () {
        $.post("/Index?handler=FormPartial", $("form").serialize(), function () {
            overlayOff();
        });
    });
}