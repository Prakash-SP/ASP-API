e.preventDefault()

function preventDefault() {
   window.history.forward();
}

setTimeout("preventDefault()", 0);
window.onunload = function () { null };

