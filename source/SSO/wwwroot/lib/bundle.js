console.log('IM LsOADED BOIsS');
var htmlProto = HTMLElement.prototype;
htmlProto.alert = function (options) {
    /// DEFAULTS
    var elemRef = this;
    var transition = 0.2;
    var duration = null;
    if (typeof options != 'undefined') {
        duration = options.duration;
    }
    if (duration != null) {
        setTimeout(function () {
            elemRef.style.transition = transition + "s";
            elemRef.style.opacity = '0';
            setTimeout(function () {
                elemRef.remove();
            }, transition * 1000);
        }, duration);
        console.log(this);
    }
};
//# sourceMappingURL=alert.js.map