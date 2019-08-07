console.log('IM LsOADED BOIsS');

var htmlProto: any = HTMLElement.prototype;

interface AlertOptions {
    // duration in seconds
    duration: number;
}
htmlProto.alert = function(options: AlertOptions) {
    /// DEFAULTS
    var elemRef: HTMLDivElement = this;
    var transition = 0.2;
    var duration = null;
    if (typeof options != 'undefined') {
        duration = options.duration;
    }

    if (duration != null) {

        setTimeout(() => {
            elemRef.style.transition = `${transition}s`;
            elemRef.style.opacity = '0';
            setTimeout(() => {
                elemRef.remove();
            }, transition * 1000);
        }, duration);
        console.log(this);
    }
    
}