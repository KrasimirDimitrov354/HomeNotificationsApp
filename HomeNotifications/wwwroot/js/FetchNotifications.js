async function getToken() {
        $.ajax({
            type: "GET",
            url: "/Index?handler=Token",
            success: function (token) {
                localStorage.setItem("user-token", token);
            }
        });
}

async function getNotifications() {
    const url = "https://localhost:7219/api/Notification/GetLatestNotifications";
    let token = localStorage.getItem("user-token");

    await fetch(url, {
        headers: {Authorization: `Bearer ${token}`}
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error));
}

getToken();
getNotifications();