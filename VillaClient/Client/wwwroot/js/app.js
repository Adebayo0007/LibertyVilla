// wwwroot/localStorage.js
window.localStorageFunctions = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    }
};
window.localStorageFunctions = {
    getItem: function (key) {
        localStorage.getItem(key);
    }
};

var dat = document.querySelector(".dis");
var dak = document.querySelector(".dik");
var sel = document.querySelector(".sel");
dak.addEventListener("change", () => {
    dat.value = dak.value;
})