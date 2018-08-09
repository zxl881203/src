/* Copyright (c) 2009 Brandon Aaron (http://brandonaaron.net)
 * Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
 * Thanks to: http://adomas.org/javascript-mouse-wheel/ for some pointers.
 * Thanks to: Mathias Bank(http://www.mathias-bank.de) for a scope bug fix.
 *
 * Version: 3.0.2
 *
 * Requires: 1.2.2+
 */
(function(c) {
    var a = ["touchend"];
    c.event.special.touchend = {
        setup: function() {
            if (this.addEventListener) {
                for (var d = a.length; d;) {
                    this.addEventListener(a[--d], b, false)
                }
            } else {
                this.ontouchend = b
            }
        },
        teardown: function() {
            if (this.removeEventListener) {
                for (var d = a.length; d;) {
                    this.removeEventListener(a[--d], b, false)
                }
            } else {
                this.ontouchend = null
            }
        }
    };
    c.fn.extend({
        touchend: function(d) {
            return d ? this.bind("touchend", d) : this.trigger("touchend")
        },
        untouchend: function(d) {
            return this.unbind("touchend", d)
        }
    });
    function b(f){
        var d = [].slice.call(arguments, 1),
            g = 0,
            e = true;
        f = c.event.fix(f || window.event);
        f.type = "touchend";
        if (f.wheelDelta) {
            g = f.wheelDelta / 120
        }
        if (f.detail) {
            g = -f.detail / 3
        }
        d.unshift(f, g);
        return c.event.handle.apply(this, d);
    }
})(jQuery);