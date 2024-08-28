$(document).on("click", "#notification-form-button", function() {
    $.ajax({
        type: "GET",
        url: "/Index?handler=FormPartial",
        success: function(data) {
            $("#notification-form").html(data);
        }
    });
});