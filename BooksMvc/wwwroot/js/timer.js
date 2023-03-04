
showToolTip: function(button) {
    button.tooltip({ title: "Осталось" }).tooltip("show");
    setTimeout(function () {
        button.tooltip("destroy");
    },
        500);
}