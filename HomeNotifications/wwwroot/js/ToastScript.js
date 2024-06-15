function toastFadeout() {
    document.getElementById('toast-notification').classList.remove('toast-visible');
}

var input = document.getElementById('toast-class');

if (input && input.value) {
    var toastClass = input.value;
    var toastContent = document.getElementById('toast-content');

    var toastDiv = document.createElement("div");

    toastDiv.id = 'toast-notification';
    toastDiv.className = 'home-toast';

    document.body.appendChild(toastDiv);

    toastDiv.textContent = toastContent.value;
    toastDiv.classList.add(`${toastClass}`);
    toastDiv.classList.add('toast-visible');

    this.setTimeout(() => { toastFadeout() }, 5000);
}
