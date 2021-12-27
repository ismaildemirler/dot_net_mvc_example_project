﻿/*
 Highstock JS v6.1.1 (2018-06-27)

 (c) 2009-2016 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (W, L) { "object" === typeof module && module.exports ? module.exports = W.document ? L(W) : L : W.Highcharts = L(W) })("undefined" !== typeof window ? window : this, function (W) {
    var L = function () {
        var a = "undefined" === typeof W ? window : W, C = a.document, D = a.navigator && a.navigator.userAgent || "", E = C && C.createElementNS && !!C.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, q = /(edge|msie|trident)/i.test(D) && !a.opera, n = -1 !== D.indexOf("Firefox"), f = -1 !== D.indexOf("Chrome"), r = n && 4 > parseInt(D.split("Firefox/")[1],
            10); return a.Highcharts ? a.Highcharts.error(16, !0) : {
                product: "Highstock", version: "6.1.1", deg2rad: 2 * Math.PI / 360, doc: C, hasBidiBug: r, hasTouch: C && void 0 !== C.documentElement.ontouchstart, isMS: q, isWebKit: -1 !== D.indexOf("AppleWebKit"), isFirefox: n, isChrome: f, isSafari: !f && -1 !== D.indexOf("Safari"), isTouchDevice: /(Mobile|Android|Windows Phone)/.test(D), SVG_NS: "http://www.w3.org/2000/svg", chartCount: 0, seriesTypes: {}, symbolSizes: {}, svg: E, win: a, marginNames: ["plotTop", "marginRight", "marginBottom", "plotLeft"], noop: function () { },
                charts: []
            }
    }(); (function (a) {
    a.timers = []; var C = a.charts, D = a.doc, E = a.win; a.error = function (q, n) { q = a.isNumber(q) ? "Highcharts error #" + q + ": www.highcharts.com/errors/" + q : q; if (n) throw Error(q); E.console && console.log(q) }; a.Fx = function (a, n, f) { this.options = n; this.elem = a; this.prop = f }; a.Fx.prototype = {
        dSetter: function () {
            var a = this.paths[0], n = this.paths[1], f = [], r = this.now, A = a.length, w; if (1 === r) f = this.toD; else if (A === n.length && 1 > r) for (; A--;)w = parseFloat(a[A]), f[A] = isNaN(w) ? n[A] : r * parseFloat(n[A] - w) + w; else f = n; this.elem.attr("d",
                f, null, !0)
        }, update: function () { var a = this.elem, n = this.prop, f = this.now, r = this.options.step; if (this[n + "Setter"]) this[n + "Setter"](); else a.attr ? a.element && a.attr(n, f, null, !0) : a.style[n] = f + this.unit; r && r.call(a, f, this) }, run: function (q, n, f) {
            var r = this, A = r.options, w = function (a) { return w.stopped ? !1 : r.step(a) }, y = E.requestAnimationFrame || function (a) { setTimeout(a, 13) }, p = function () { for (var c = 0; c < a.timers.length; c++)a.timers[c]() || a.timers.splice(c--, 1); a.timers.length && y(p) }; q !== n || this.elem["forceAnimate:" +
                this.prop] ? (this.startTime = +new Date, this.start = q, this.end = n, this.unit = f, this.now = this.start, this.pos = 0, w.elem = this.elem, w.prop = this.prop, w() && 1 === a.timers.push(w) && y(p)) : (delete A.curAnim[this.prop], A.complete && 0 === a.keys(A.curAnim).length && A.complete.call(this.elem))
        }, step: function (q) {
            var n = +new Date, f, r = this.options, A = this.elem, w = r.complete, y = r.duration, p = r.curAnim; A.attr && !A.element ? q = !1 : q || n >= y + this.startTime ? (this.now = this.end, this.pos = 1, this.update(), f = p[this.prop] = !0, a.objectEach(p, function (a) {
            !0 !==
                a && (f = !1)
            }), f && w && w.call(A), q = !1) : (this.pos = r.easing((n - this.startTime) / y), this.now = this.start + (this.end - this.start) * this.pos, this.update(), q = !0); return q
        }, initPath: function (q, n, f) {
            function r(a) { var b, c; for (e = a.length; e--;)b = "M" === a[e] || "L" === a[e], c = /[a-zA-Z]/.test(a[e + 3]), b && c && a.splice(e + 1, 0, a[e + 1], a[e + 2], a[e + 1], a[e + 2]) } function A(a, b) {
                for (; a.length < k;) { a[0] = b[k - a.length]; var c = a.slice(0, m);[].splice.apply(a, [0, 0].concat(c)); h && (c = a.slice(a.length - m), [].splice.apply(a, [a.length, 0].concat(c)), e--) } a[0] =
                    "M"
            } function w(a, c) { for (var e = (k - a.length) / m; 0 < e && e--;)b = a.slice().splice(a.length / u - m, m * u), b[0] = c[k - m - e * m], d && (b[m - 6] = b[m - 2], b[m - 5] = b[m - 1]), [].splice.apply(a, [a.length / u, 0].concat(b)), h && e-- } n = n || ""; var y, p = q.startX, c = q.endX, d = -1 < n.indexOf("C"), m = d ? 7 : 3, k, b, e; n = n.split(" "); f = f.slice(); var h = q.isArea, u = h ? 2 : 1, t; d && (r(n), r(f)); if (p && c) { for (e = 0; e < p.length; e++)if (p[e] === c[0]) { y = e; break } else if (p[0] === c[c.length - p.length + e]) { y = e; t = !0; break } void 0 === y && (n = []) } n.length && a.isNumber(y) && (k = f.length + y * u * m,
                t ? (A(n, f), w(f, n)) : (A(f, n), w(n, f))); return [n, f]
        }
    }; a.Fx.prototype.fillSetter = a.Fx.prototype.strokeSetter = function () { this.elem.attr(this.prop, a.color(this.start).tweenTo(a.color(this.end), this.pos), null, !0) }; a.merge = function () {
        var q, n = arguments, f, r = {}, A = function (f, y) { "object" !== typeof f && (f = {}); a.objectEach(y, function (p, c) { !a.isObject(p, !0) || a.isClass(p) || a.isDOMElement(p) ? f[c] = y[c] : f[c] = A(f[c] || {}, p) }); return f }; !0 === n[0] && (r = n[1], n = Array.prototype.slice.call(n, 2)); f = n.length; for (q = 0; q < f; q++)r = A(r,
            n[q]); return r
    }; a.pInt = function (a, n) { return parseInt(a, n || 10) }; a.isString = function (a) { return "string" === typeof a }; a.isArray = function (a) { a = Object.prototype.toString.call(a); return "[object Array]" === a || "[object Array Iterator]" === a }; a.isObject = function (q, n) { return !!q && "object" === typeof q && (!n || !a.isArray(q)) }; a.isDOMElement = function (q) { return a.isObject(q) && "number" === typeof q.nodeType }; a.isClass = function (q) {
        var n = q && q.constructor; return !(!a.isObject(q, !0) || a.isDOMElement(q) || !n || !n.name || "Object" ===
            n.name)
    }; a.isNumber = function (a) { return "number" === typeof a && !isNaN(a) && Infinity > a && -Infinity < a }; a.erase = function (a, n) { for (var f = a.length; f--;)if (a[f] === n) { a.splice(f, 1); break } }; a.defined = function (a) { return void 0 !== a && null !== a }; a.attr = function (q, n, f) { var r; a.isString(n) ? a.defined(f) ? q.setAttribute(n, f) : q && q.getAttribute && ((r = q.getAttribute(n)) || "class" !== n || (r = q.getAttribute(n + "Name"))) : a.defined(n) && a.isObject(n) && a.objectEach(n, function (a, f) { q.setAttribute(f, a) }); return r }; a.splat = function (q) {
        return a.isArray(q) ?
            q : [q]
    }; a.syncTimeout = function (a, n, f) { if (n) return setTimeout(a, n, f); a.call(0, f) }; a.clearTimeout = function (q) { a.defined(q) && clearTimeout(q) }; a.extend = function (a, n) { var f; a || (a = {}); for (f in n) a[f] = n[f]; return a }; a.pick = function () { var a = arguments, n, f, r = a.length; for (n = 0; n < r; n++)if (f = a[n], void 0 !== f && null !== f) return f }; a.css = function (q, n) { a.isMS && !a.svg && n && void 0 !== n.opacity && (n.filter = "alpha(opacity\x3d" + 100 * n.opacity + ")"); a.extend(q.style, n) }; a.createElement = function (q, n, f, r, A) {
        q = D.createElement(q); var w =
            a.css; n && a.extend(q, n); A && w(q, { padding: 0, border: "none", margin: 0 }); f && w(q, f); r && r.appendChild(q); return q
    }; a.extendClass = function (q, n) { var f = function () { }; f.prototype = new q; a.extend(f.prototype, n); return f }; a.pad = function (a, n, f) { return Array((n || 2) + 1 - String(a).replace("-", "").length).join(f || 0) + a }; a.relativeLength = function (a, n, f) { return /%$/.test(a) ? n * parseFloat(a) / 100 + (f || 0) : parseFloat(a) }; a.wrap = function (a, n, f) {
        var r = a[n]; a[n] = function () {
            var a = Array.prototype.slice.call(arguments), w = arguments, y = this;
            y.proceed = function () { r.apply(y, arguments.length ? arguments : w) }; a.unshift(r); a = f.apply(this, a); y.proceed = null; return a
        }
    }; a.formatSingle = function (q, n, f) { var r = /\.([0-9])/, A = a.defaultOptions.lang; /f$/.test(q) ? (f = (f = q.match(r)) ? f[1] : -1, null !== n && (n = a.numberFormat(n, f, A.decimalPoint, -1 < q.indexOf(",") ? A.thousandsSep : ""))) : n = (f || a.time).dateFormat(q, n); return n }; a.format = function (q, n, f) {
        for (var r = "{", A = !1, w, y, p, c, d = [], m; q;) {
            r = q.indexOf(r); if (-1 === r) break; w = q.slice(0, r); if (A) {
                w = w.split(":"); y = w.shift().split(".");
                c = y.length; m = n; for (p = 0; p < c; p++)m && (m = m[y[p]]); w.length && (m = a.formatSingle(w.join(":"), m, f)); d.push(m)
            } else d.push(w); q = q.slice(r + 1); r = (A = !A) ? "}" : "{"
        } d.push(q); return d.join("")
    }; a.getMagnitude = function (a) { return Math.pow(10, Math.floor(Math.log(a) / Math.LN10)) }; a.normalizeTickInterval = function (q, n, f, r, A) {
        var w, y = q; f = a.pick(f, 1); w = q / f; n || (n = A ? [1, 1.2, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10] : [1, 2, 2.5, 5, 10], !1 === r && (1 === f ? n = a.grep(n, function (a) { return 0 === a % 1 }) : .1 >= f && (n = [1 / f]))); for (r = 0; r < n.length && !(y = n[r], A && y * f >= q ||
            !A && w <= (n[r] + (n[r + 1] || n[r])) / 2); r++); return y = a.correctFloat(y * f, -Math.round(Math.log(.001) / Math.LN10))
    }; a.stableSort = function (a, n) { var f = a.length, r, A; for (A = 0; A < f; A++)a[A].safeI = A; a.sort(function (a, f) { r = n(a, f); return 0 === r ? a.safeI - f.safeI : r }); for (A = 0; A < f; A++)delete a[A].safeI }; a.arrayMin = function (a) { for (var n = a.length, f = a[0]; n--;)a[n] < f && (f = a[n]); return f }; a.arrayMax = function (a) { for (var n = a.length, f = a[0]; n--;)a[n] > f && (f = a[n]); return f }; a.destroyObjectProperties = function (q, n) {
        a.objectEach(q, function (a,
            r) { a && a !== n && a.destroy && a.destroy(); delete q[r] })
    }; a.discardElement = function (q) { var n = a.garbageBin; n || (n = a.createElement("div")); q && n.appendChild(q); n.innerHTML = "" }; a.correctFloat = function (a, n) { return parseFloat(a.toPrecision(n || 14)) }; a.setAnimation = function (q, n) { n.renderer.globalAnimation = a.pick(q, n.options.chart.animation, !0) }; a.animObject = function (q) { return a.isObject(q) ? a.merge(q) : { duration: q ? 500 : 0 } }; a.timeUnits = {
        millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5, month: 24192E5,
        year: 314496E5
    }; a.numberFormat = function (q, n, f, r) {
        q = +q || 0; n = +n; var A = a.defaultOptions.lang, w = (q.toString().split(".")[1] || "").split("e")[0].length, y, p, c = q.toString().split("e"); -1 === n ? n = Math.min(w, 20) : a.isNumber(n) ? n && c[1] && 0 > c[1] && (y = n + +c[1], 0 <= y ? (c[0] = (+c[0]).toExponential(y).split("e")[0], n = y) : (c[0] = c[0].split(".")[0] || 0, q = 20 > n ? (c[0] * Math.pow(10, c[1])).toFixed(n) : 0, c[1] = 0)) : n = 2; p = (Math.abs(c[1] ? c[0] : q) + Math.pow(10, -Math.max(n, w) - 1)).toFixed(n); w = String(a.pInt(p)); y = 3 < w.length ? w.length % 3 : 0; f = a.pick(f,
            A.decimalPoint); r = a.pick(r, A.thousandsSep); q = (0 > q ? "-" : "") + (y ? w.substr(0, y) + r : ""); q += w.substr(y).replace(/(\d{3})(?=\d)/g, "$1" + r); n && (q += f + p.slice(-n)); c[1] && 0 !== +q && (q += "e" + c[1]); return q
    }; Math.easeInOutSine = function (a) { return -.5 * (Math.cos(Math.PI * a) - 1) }; a.getStyle = function (q, n, f) {
        if ("width" === n) return Math.max(0, Math.min(q.offsetWidth, q.scrollWidth) - a.getStyle(q, "padding-left") - a.getStyle(q, "padding-right")); if ("height" === n) return Math.max(0, Math.min(q.offsetHeight, q.scrollHeight) - a.getStyle(q, "padding-top") -
            a.getStyle(q, "padding-bottom")); E.getComputedStyle || a.error(27, !0); if (q = E.getComputedStyle(q, void 0)) q = q.getPropertyValue(n), a.pick(f, "opacity" !== n) && (q = a.pInt(q)); return q
    }; a.inArray = function (q, n, f) { return (a.indexOfPolyfill || Array.prototype.indexOf).call(n, q, f) }; a.grep = function (q, n) { return (a.filterPolyfill || Array.prototype.filter).call(q, n) }; a.find = Array.prototype.find ? function (a, n) { return a.find(n) } : function (a, n) { var f, r = a.length; for (f = 0; f < r; f++)if (n(a[f], f)) return a[f] }; a.some = function (q, n, f) {
        return (a.somePolyfill ||
            Array.prototype.some).call(q, n, f)
    }; a.map = function (a, n) { for (var f = [], r = 0, A = a.length; r < A; r++)f[r] = n.call(a[r], a[r], r, a); return f }; a.keys = function (q) { return (a.keysPolyfill || Object.keys).call(void 0, q) }; a.reduce = function (q, n, f) { return (a.reducePolyfill || Array.prototype.reduce).apply(q, 2 < arguments.length ? [n, f] : [n]) }; a.offset = function (a) {
        var n = D.documentElement; a = a.parentElement || a.parentNode ? a.getBoundingClientRect() : { top: 0, left: 0 }; return {
            top: a.top + (E.pageYOffset || n.scrollTop) - (n.clientTop || 0), left: a.left +
                (E.pageXOffset || n.scrollLeft) - (n.clientLeft || 0)
        }
    }; a.stop = function (q, n) { for (var f = a.timers.length; f--;)a.timers[f].elem !== q || n && n !== a.timers[f].prop || (a.timers[f].stopped = !0) }; a.each = function (q, n, f) { return (a.forEachPolyfill || Array.prototype.forEach).call(q, n, f) }; a.objectEach = function (a, n, f) { for (var r in a) a.hasOwnProperty(r) && n.call(f || a[r], a[r], r, a) }; a.addEvent = function (q, n, f, r) {
        var A, w = q.addEventListener || a.addEventListenerPolyfill; A = "function" === typeof q && q.prototype ? q.prototype.protoEvents = q.prototype.protoEvents ||
            {} : q.hcEvents = q.hcEvents || {}; a.Point && q instanceof a.Point && q.series && q.series.chart && (q.series.chart.runTrackerClick = !0); w && w.call(q, n, f, !1); A[n] || (A[n] = []); A[n].push(f); r && a.isNumber(r.order) && (f.order = r.order, A[n].sort(function (a, p) { return a.order - p.order })); return function () { a.removeEvent(q, n, f) }
    }; a.removeEvent = function (q, n, f) {
        function r(p, c) { var d = q.removeEventListener || a.removeEventListenerPolyfill; d && d.call(q, p, c, !1) } function A(p) {
            var c, d; q.nodeName && (n ? (c = {}, c[n] = !0) : c = p, a.objectEach(c, function (a,
                c) { if (p[c]) for (d = p[c].length; d--;)r(c, p[c][d]) }))
        } var w, y; a.each(["protoEvents", "hcEvents"], function (p) { var c = q[p]; c && (n ? (w = c[n] || [], f ? (y = a.inArray(f, w), -1 < y && (w.splice(y, 1), c[n] = w), r(n, f)) : (A(c), c[n] = [])) : (A(c), q[p] = {})) })
    }; a.fireEvent = function (q, n, f, r) {
        var A, w, y, p, c; f = f || {}; D.createEvent && (q.dispatchEvent || q.fireEvent) ? (A = D.createEvent("Events"), A.initEvent(n, !0, !0), a.extend(A, f), q.dispatchEvent ? q.dispatchEvent(A) : q.fireEvent(n, A)) : a.each(["protoEvents", "hcEvents"], function (d) {
            if (q[d]) for (w = q[d][n] ||
                [], y = w.length, f.target || a.extend(f, { preventDefault: function () { f.defaultPrevented = !0 }, target: q, type: n }), p = 0; p < y; p++)(c = w[p]) && !1 === c.call(q, f) && f.preventDefault()
        }); r && !f.defaultPrevented && r.call(q, f)
    }; a.animate = function (q, n, f) {
        var r, A = "", w, y, p; a.isObject(f) || (p = arguments, f = { duration: p[2], easing: p[3], complete: p[4] }); a.isNumber(f.duration) || (f.duration = 400); f.easing = "function" === typeof f.easing ? f.easing : Math[f.easing] || Math.easeInOutSine; f.curAnim = a.merge(n); a.objectEach(n, function (c, d) {
            a.stop(q, d);
            y = new a.Fx(q, f, d); w = null; "d" === d ? (y.paths = y.initPath(q, q.d, n.d), y.toD = n.d, r = 0, w = 1) : q.attr ? r = q.attr(d) : (r = parseFloat(a.getStyle(q, d)) || 0, "opacity" !== d && (A = "px")); w || (w = c); w && w.match && w.match("px") && (w = w.replace(/px/g, "")); y.run(r, w, A)
        })
    }; a.seriesType = function (q, n, f, r, A) { var w = a.getOptions(), y = a.seriesTypes; w.plotOptions[q] = a.merge(w.plotOptions[n], f); y[q] = a.extendClass(y[n] || function () { }, r); y[q].prototype.type = q; A && (y[q].prototype.pointClass = a.extendClass(a.Point, A)); return y[q] }; a.uniqueKey = function () {
        var a =
            Math.random().toString(36).substring(2, 9), n = 0; return function () { return "highcharts-" + a + "-" + n++ }
    }(); E.jQuery && (E.jQuery.fn.highcharts = function () { var q = [].slice.call(arguments); if (this[0]) return q[0] ? (new (a[a.isString(q[0]) ? q.shift() : "Chart"])(this[0], q[0], q[1]), this) : C[a.attr(this[0], "data-highcharts-chart")] })
    })(L); (function (a) {
        var C = a.each, D = a.isNumber, E = a.map, q = a.merge, n = a.pInt; a.Color = function (f) { if (!(this instanceof a.Color)) return new a.Color(f); this.init(f) }; a.Color.prototype = {
            parsers: [{
                regex: /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/,
                parse: function (a) { return [n(a[1]), n(a[2]), n(a[3]), parseFloat(a[4], 10)] }
            }, { regex: /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/, parse: function (a) { return [n(a[1]), n(a[2]), n(a[3]), 1] } }], names: { none: "rgba(255,255,255,0)", white: "#ffffff", black: "#000000" }, init: function (f) {
                var n, A, w, y; if ((this.input = f = this.names[f && f.toLowerCase ? f.toLowerCase() : ""] || f) && f.stops) this.stops = E(f.stops, function (p) { return new a.Color(p[1]) }); else if (f && f.charAt && "#" === f.charAt() && (n = f.length, f = parseInt(f.substr(1),
                    16), 7 === n ? A = [(f & 16711680) >> 16, (f & 65280) >> 8, f & 255, 1] : 4 === n && (A = [(f & 3840) >> 4 | (f & 3840) >> 8, (f & 240) >> 4 | f & 240, (f & 15) << 4 | f & 15, 1])), !A) for (w = this.parsers.length; w-- && !A;)y = this.parsers[w], (n = y.regex.exec(f)) && (A = y.parse(n)); this.rgba = A || []
            }, get: function (a) {
                var f = this.input, n = this.rgba, w; this.stops ? (w = q(f), w.stops = [].concat(w.stops), C(this.stops, function (f, p) { w.stops[p] = [w.stops[p][0], f.get(a)] })) : w = n && D(n[0]) ? "rgb" === a || !a && 1 === n[3] ? "rgb(" + n[0] + "," + n[1] + "," + n[2] + ")" : "a" === a ? n[3] : "rgba(" + n.join(",") + ")" : f;
                return w
            }, brighten: function (a) { var f, A = this.rgba; if (this.stops) C(this.stops, function (f) { f.brighten(a) }); else if (D(a) && 0 !== a) for (f = 0; 3 > f; f++)A[f] += n(255 * a), 0 > A[f] && (A[f] = 0), 255 < A[f] && (A[f] = 255); return this }, setOpacity: function (a) { this.rgba[3] = a; return this }, tweenTo: function (a, n) {
                var f = this.rgba, w = a.rgba; w.length && f && f.length ? (a = 1 !== w[3] || 1 !== f[3], n = (a ? "rgba(" : "rgb(") + Math.round(w[0] + (f[0] - w[0]) * (1 - n)) + "," + Math.round(w[1] + (f[1] - w[1]) * (1 - n)) + "," + Math.round(w[2] + (f[2] - w[2]) * (1 - n)) + (a ? "," + (w[3] + (f[3] -
                    w[3]) * (1 - n)) : "") + ")") : n = a.input || "none"; return n
            }
        }; a.color = function (f) { return new a.Color(f) }
    })(L); (function (a) {
        var C, D, E = a.addEvent, q = a.animate, n = a.attr, f = a.charts, r = a.color, A = a.css, w = a.createElement, y = a.defined, p = a.deg2rad, c = a.destroyObjectProperties, d = a.doc, m = a.each, k = a.extend, b = a.erase, e = a.grep, h = a.hasTouch, u = a.inArray, t = a.isArray, z = a.isFirefox, I = a.isMS, v = a.isObject, G = a.isString, l = a.isWebKit, H = a.merge, K = a.noop, F = a.objectEach, B = a.pick, g = a.pInt, x = a.removeEvent, N = a.stop, O = a.svg, M = a.SVG_NS, Q = a.symbolSizes,
        P = a.win; C = a.SVGElement = function () { return this }; k(C.prototype, {
            opacity: 1, SVG_NS: M, textProps: "direction fontSize fontWeight fontFamily fontStyle color lineHeight width textAlign textDecoration textOverflow textOutline".split(" "), init: function (a, g) { this.element = "span" === g ? w(g) : d.createElementNS(this.SVG_NS, g); this.renderer = a }, animate: function (g, b, x) { b = a.animObject(B(b, this.renderer.globalAnimation, !0)); 0 !== b.duration ? (x && (b.complete = x), q(this, g, b)) : (this.attr(g, null, x), b.step && b.step.call(this)); return this },
            complexColor: function (g, b, x) {
                var J = this.renderer, c, e, h, l, k, M, B, u, d, v, z, N = [], O; a.fireEvent(this.renderer, "complexColor", { args: arguments }, function () {
                    g.radialGradient ? e = "radialGradient" : g.linearGradient && (e = "linearGradient"); e && (h = g[e], k = J.gradients, B = g.stops, v = x.radialReference, t(h) && (g[e] = h = { x1: h[0], y1: h[1], x2: h[2], y2: h[3], gradientUnits: "userSpaceOnUse" }), "radialGradient" === e && v && !y(h.gradientUnits) && (l = h, h = H(h, J.getRadialAttr(v, l), { gradientUnits: "userSpaceOnUse" })), F(h, function (a, g) {
                    "id" !== g && N.push(g,
                        a)
                    }), F(B, function (a) { N.push(a) }), N = N.join(","), k[N] ? z = k[N].attr("id") : (h.id = z = a.uniqueKey(), k[N] = M = J.createElement(e).attr(h).add(J.defs), M.radAttr = l, M.stops = [], m(B, function (g) { 0 === g[1].indexOf("rgba") ? (c = a.color(g[1]), u = c.get("rgb"), d = c.get("a")) : (u = g[1], d = 1); g = J.createElement("stop").attr({ offset: g[0], "stop-color": u, "stop-opacity": d }).add(M); M.stops.push(g) })), O = "url(" + J.url + "#" + z + ")", x.setAttribute(b, O), x.gradient = N, g.toString = function () { return O })
                })
            }, applyTextOutline: function (g) {
                var J = this.element,
                x, c, e, h, k; -1 !== g.indexOf("contrast") && (g = g.replace(/contrast/g, this.renderer.getContrast(J.style.fill))); g = g.split(" "); c = g[g.length - 1]; if ((e = g[0]) && "none" !== e && a.svg) {
                this.fakeTS = !0; g = [].slice.call(J.getElementsByTagName("tspan")); this.ySetter = this.xSetter; e = e.replace(/(^[\d\.]+)(.*?)$/g, function (a, g, J) { return 2 * g + J }); for (k = g.length; k--;)x = g[k], "highcharts-text-outline" === x.getAttribute("class") && b(g, J.removeChild(x)); h = J.firstChild; m(g, function (a, g) {
                0 === g && (a.setAttribute("x", J.getAttribute("x")),
                    g = J.getAttribute("y"), a.setAttribute("y", g || 0), null === g && J.setAttribute("y", 0)); a = a.cloneNode(1); n(a, { "class": "highcharts-text-outline", fill: c, stroke: c, "stroke-width": e, "stroke-linejoin": "round" }); J.insertBefore(a, h)
                })
                }
            }, attr: function (a, g, b, x) {
                var J, c = this.element, e, h = this, k, l; "string" === typeof a && void 0 !== g && (J = a, a = {}, a[J] = g); "string" === typeof a ? h = (this[a + "Getter"] || this._defaultGetter).call(this, a, c) : (F(a, function (g, J) {
                    k = !1; x || N(this, J); this.symbolName && /^(x|y|width|height|r|start|end|innerR|anchorX|anchorY)$/.test(J) &&
                        (e || (this.symbolAttr(a), e = !0), k = !0); !this.rotation || "x" !== J && "y" !== J || (this.doTransform = !0); k || (l = this[J + "Setter"] || this._defaultSetter, l.call(this, g, J, c), this.shadows && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(J) && this.updateShadows(J, g, l))
                }, this), this.afterSetters()); b && b.call(this); return h
            }, afterSetters: function () { this.doTransform && (this.updateTransform(), this.doTransform = !1) }, updateShadows: function (a, g, b) {
                for (var J = this.shadows, x = J.length; x--;)b.call(J[x], "height" === a ? Math.max(g -
                    (J[x].cutHeight || 0), 0) : "d" === a ? this.d : g, a, J[x])
            }, addClass: function (a, g) { var J = this.attr("class") || ""; -1 === J.indexOf(a) && (g || (a = (J + (J ? " " : "") + a).replace("  ", " ")), this.attr("class", a)); return this }, hasClass: function (a) { return -1 !== u(a, (this.attr("class") || "").split(" ")) }, removeClass: function (a) { return this.attr("class", (this.attr("class") || "").replace(a, "")) }, symbolAttr: function (a) {
                var g = this; m("x y r start end width height innerR anchorX anchorY".split(" "), function (J) { g[J] = B(a[J], g[J]) }); g.attr({
                    d: g.renderer.symbols[g.symbolName](g.x,
                        g.y, g.width, g.height, g)
                })
            }, clip: function (a) { return this.attr("clip-path", a ? "url(" + this.renderer.url + "#" + a.id + ")" : "none") }, crisp: function (a, g) { var J; g = g || a.strokeWidth || 0; J = Math.round(g) % 2 / 2; a.x = Math.floor(a.x || this.x || 0) + J; a.y = Math.floor(a.y || this.y || 0) + J; a.width = Math.floor((a.width || this.width || 0) - 2 * J); a.height = Math.floor((a.height || this.height || 0) - 2 * J); y(a.strokeWidth) && (a.strokeWidth = g); return a }, css: function (a) {
                var J = this.styles, b = {}, x = this.element, c, e = "", h, l = !J, M = ["textOutline", "textOverflow",
                    "width"]; a && a.color && (a.fill = a.color); J && F(a, function (a, g) { a !== J[g] && (b[g] = a, l = !0) }); l && (J && (a = k(J, b)), a && (null === a.width || "auto" === a.width ? delete this.textWidth : "text" === x.nodeName.toLowerCase() && a.width && (c = this.textWidth = g(a.width))), this.styles = a, c && !O && this.renderer.forExport && delete a.width, x.namespaceURI === this.SVG_NS ? (h = function (a, g) { return "-" + g.toLowerCase() }, F(a, function (a, g) { -1 === u(g, M) && (e += g.replace(/([A-Z])/g, h) + ":" + a + ";") }), e && n(x, "style", e)) : A(x, a), this.added && ("text" === this.element.nodeName &&
                        this.renderer.buildText(this), a && a.textOutline && this.applyTextOutline(a.textOutline))); return this
            }, strokeWidth: function () { return this["stroke-width"] || 0 }, on: function (a, g) { var J = this, x = J.element; h && "click" === a ? (x.ontouchstart = function (a) { J.touchEventFired = Date.now(); a.preventDefault(); g.call(x, a) }, x.onclick = function (a) { (-1 === P.navigator.userAgent.indexOf("Android") || 1100 < Date.now() - (J.touchEventFired || 0)) && g.call(x, a) }) : x["on" + a] = g; return this }, setRadialReference: function (a) {
                var g = this.renderer.gradients[this.element.gradient];
                this.element.radialReference = a; g && g.radAttr && g.animate(this.renderer.getRadialAttr(a, g.radAttr)); return this
            }, translate: function (a, g) { return this.attr({ translateX: a, translateY: g }) }, invert: function (a) { this.inverted = a; this.updateTransform(); return this }, updateTransform: function () {
                var a = this.translateX || 0, g = this.translateY || 0, x = this.scaleX, b = this.scaleY, c = this.inverted, e = this.rotation, h = this.matrix, k = this.element; c && (a += this.width, g += this.height); a = ["translate(" + a + "," + g + ")"]; y(h) && a.push("matrix(" + h.join(",") +
                    ")"); c ? a.push("rotate(90) scale(-1,1)") : e && a.push("rotate(" + e + " " + B(this.rotationOriginX, k.getAttribute("x"), 0) + " " + B(this.rotationOriginY, k.getAttribute("y") || 0) + ")"); (y(x) || y(b)) && a.push("scale(" + B(x, 1) + " " + B(b, 1) + ")"); a.length && k.setAttribute("transform", a.join(" "))
            }, toFront: function () { var a = this.element; a.parentNode.appendChild(a); return this }, align: function (a, g, x) {
                var c, e, J, h, k = {}; e = this.renderer; J = e.alignedObjects; var l, M; if (a) {
                    if (this.alignOptions = a, this.alignByTranslate = g, !x || G(x)) this.alignTo =
                        c = x || "renderer", b(J, this), J.push(this), x = null
                } else a = this.alignOptions, g = this.alignByTranslate, c = this.alignTo; x = B(x, e[c], e); c = a.align; e = a.verticalAlign; J = (x.x || 0) + (a.x || 0); h = (x.y || 0) + (a.y || 0); "right" === c ? l = 1 : "center" === c && (l = 2); l && (J += (x.width - (a.width || 0)) / l); k[g ? "translateX" : "x"] = Math.round(J); "bottom" === e ? M = 1 : "middle" === e && (M = 2); M && (h += (x.height - (a.height || 0)) / M); k[g ? "translateY" : "y"] = Math.round(h); this[this.placed ? "animate" : "attr"](k); this.placed = !0; this.alignAttr = k; return this
            }, getBBox: function (a,
                g) {
                    var x, b = this.renderer, e, c = this.element, J = this.styles, h, l = this.textStr, M, u = b.cache, d = b.cacheKeys, F; g = B(g, this.rotation); e = g * p; h = J && J.fontSize; y(l) && (F = l.toString(), -1 === F.indexOf("\x3c") && (F = F.replace(/[0-9]/g, "0")), F += ["", g || 0, h, this.textWidth, J && J.textOverflow].join()); F && !a && (x = u[F]); if (!x) {
                        if (c.namespaceURI === this.SVG_NS || b.forExport) {
                            try {
                            (M = this.fakeTS && function (a) { m(c.querySelectorAll(".highcharts-text-outline"), function (g) { g.style.display = a }) }) && M("none"), x = c.getBBox ? k({}, c.getBBox()) : {
                                width: c.offsetWidth,
                                height: c.offsetHeight
                            }, M && M("")
                            } catch (fa) { } if (!x || 0 > x.width) x = { width: 0, height: 0 }
                        } else x = this.htmlGetBBox(); b.isSVG && (a = x.width, b = x.height, J && "11px" === J.fontSize && 17 === Math.round(b) && (x.height = b = 14), g && (x.width = Math.abs(b * Math.sin(e)) + Math.abs(a * Math.cos(e)), x.height = Math.abs(b * Math.cos(e)) + Math.abs(a * Math.sin(e)))); if (F && 0 < x.height) { for (; 250 < d.length;)delete u[d.shift()]; u[F] || d.push(F); u[F] = x }
                    } return x
            }, show: function (a) { return this.attr({ visibility: a ? "inherit" : "visible" }) }, hide: function () { return this.attr({ visibility: "hidden" }) },
            fadeOut: function (a) { var g = this; g.animate({ opacity: 0 }, { duration: a || 150, complete: function () { g.attr({ y: -9999 }) } }) }, add: function (a) { var g = this.renderer, x = this.element, b; a && (this.parentGroup = a); this.parentInverted = a && a.inverted; void 0 !== this.textStr && g.buildText(this); this.added = !0; if (!a || a.handleZ || this.zIndex) b = this.zIndexSetter(); b || (a ? a.element : g.box).appendChild(x); if (this.onAdd) this.onAdd(); return this }, safeRemoveChild: function (a) { var g = a.parentNode; g && g.removeChild(a) }, destroy: function () {
                var a =
                    this, g = a.element || {}, x = a.renderer.isSVG && "SPAN" === g.nodeName && a.parentGroup, c = g.ownerSVGElement, e = a.clipPath; g.onclick = g.onmouseout = g.onmouseover = g.onmousemove = g.point = null; N(a); e && c && (m(c.querySelectorAll("[clip-path],[CLIP-PATH]"), function (a) { var g = a.getAttribute("clip-path"), x = e.element.id; (-1 < g.indexOf("(#" + x + ")") || -1 < g.indexOf('("#' + x + '")')) && a.removeAttribute("clip-path") }), a.clipPath = e.destroy()); if (a.stops) { for (c = 0; c < a.stops.length; c++)a.stops[c] = a.stops[c].destroy(); a.stops = null } a.safeRemoveChild(g);
                for (a.destroyShadows(); x && x.div && 0 === x.div.childNodes.length;)g = x.parentGroup, a.safeRemoveChild(x.div), delete x.div, x = g; a.alignTo && b(a.renderer.alignedObjects, a); F(a, function (g, x) { delete a[x] }); return null
            }, shadow: function (a, g, x) {
                var b = [], c, e, h = this.element, J, k, l, M; if (!a) this.destroyShadows(); else if (!this.shadows) {
                    k = B(a.width, 3); l = (a.opacity || .15) / k; M = this.parentInverted ? "(-1,-1)" : "(" + B(a.offsetX, 1) + ", " + B(a.offsetY, 1) + ")"; for (c = 1; c <= k; c++)e = h.cloneNode(0), J = 2 * k + 1 - 2 * c, n(e, {
                        isShadow: "true", stroke: a.color ||
                            "#000000", "stroke-opacity": l * c, "stroke-width": J, transform: "translate" + M, fill: "none"
                    }), x && (n(e, "height", Math.max(n(e, "height") - J, 0)), e.cutHeight = J), g ? g.element.appendChild(e) : h.parentNode && h.parentNode.insertBefore(e, h), b.push(e); this.shadows = b
                } return this
            }, destroyShadows: function () { m(this.shadows || [], function (a) { this.safeRemoveChild(a) }, this); this.shadows = void 0 }, xGetter: function (a) { "circle" === this.element.nodeName && ("x" === a ? a = "cx" : "y" === a && (a = "cy")); return this._defaultGetter(a) }, _defaultGetter: function (a) {
                a =
                B(this[a + "Value"], this[a], this.element ? this.element.getAttribute(a) : null, 0); /^[\-0-9\.]+$/.test(a) && (a = parseFloat(a)); return a
            }, dSetter: function (a, g, x) { a && a.join && (a = a.join(" ")); /(NaN| {2}|^$)/.test(a) && (a = "M 0 0"); this[g] !== a && (x.setAttribute(g, a), this[g] = a) }, dashstyleSetter: function (a) {
                var x, b = this["stroke-width"]; "inherit" === b && (b = 1); if (a = a && a.toLowerCase()) {
                    a = a.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash",
                        "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (x = a.length; x--;)a[x] = g(a[x]) * b; a = a.join(",").replace(/NaN/g, "none"); this.element.setAttribute("stroke-dasharray", a)
                }
            }, alignSetter: function (a) { this.alignValue = a; this.element.setAttribute("text-anchor", { left: "start", center: "middle", right: "end" }[a]) }, opacitySetter: function (a, g, x) { this[g] = a; x.setAttribute(g, a) }, titleSetter: function (a) {
                var g = this.element.getElementsByTagName("title")[0]; g || (g = d.createElementNS(this.SVG_NS,
                    "title"), this.element.appendChild(g)); g.firstChild && g.removeChild(g.firstChild); g.appendChild(d.createTextNode(String(B(a), "").replace(/<[^>]*>/g, "").replace(/&lt;/g, "\x3c").replace(/&gt;/g, "\x3e")))
            }, textSetter: function (a) { a !== this.textStr && (delete this.bBox, this.textStr = a, this.added && this.renderer.buildText(this)) }, fillSetter: function (a, g, x) { "string" === typeof a ? x.setAttribute(g, a) : a && this.complexColor(a, g, x) }, visibilitySetter: function (a, g, x) {
                "inherit" === a ? x.removeAttribute(g) : this[g] !== a && x.setAttribute(g,
                    a); this[g] = a
            }, zIndexSetter: function (a, x) {
                var b = this.renderer, c = this.parentGroup, e = (c || b).element || b.box, h, k = this.element, l, M, b = e === b.box; h = this.added; var B; y(a) ? (k.setAttribute("data-z-index", a), a = +a, this[x] === a && (h = !1)) : y(this[x]) && k.removeAttribute("data-z-index"); this[x] = a; if (h) {
                (a = this.zIndex) && c && (c.handleZ = !0); x = e.childNodes; for (B = x.length - 1; 0 <= B && !l; B--)if (c = x[B], h = c.getAttribute("data-z-index"), M = !y(h), c !== k) if (0 > a && M && !b && !B) e.insertBefore(k, x[B]), l = !0; else if (g(h) <= a || M && (!y(a) || 0 <= a)) e.insertBefore(k,
                    x[B + 1] || null), l = !0; l || (e.insertBefore(k, x[b ? 3 : 0] || null), l = !0)
                } return l
            }, _defaultSetter: function (a, g, x) { x.setAttribute(g, a) }
        }); C.prototype.yGetter = C.prototype.xGetter; C.prototype.translateXSetter = C.prototype.translateYSetter = C.prototype.rotationSetter = C.prototype.verticalAlignSetter = C.prototype.rotationOriginXSetter = C.prototype.rotationOriginYSetter = C.prototype.scaleXSetter = C.prototype.scaleYSetter = C.prototype.matrixSetter = function (a, g) { this[g] = a; this.doTransform = !0 }; C.prototype["stroke-widthSetter"] =
            C.prototype.strokeSetter = function (a, g, x) { this[g] = a; this.stroke && this["stroke-width"] ? (C.prototype.fillSetter.call(this, this.stroke, "stroke", x), x.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) : "stroke-width" === g && 0 === a && this.hasStroke && (x.removeAttribute("stroke"), this.hasStroke = !1) }; D = a.SVGRenderer = function () { this.init.apply(this, arguments) }; k(D.prototype, {
                Element: C, SVG_NS: M, init: function (a, g, x, b, c, e) {
                    var h; b = this.createElement("svg").attr({ version: "1.1", "class": "highcharts-root" }).css(this.getStyle(b));
                    h = b.element; a.appendChild(h); n(a, "dir", "ltr"); -1 === a.innerHTML.indexOf("xmlns") && n(h, "xmlns", this.SVG_NS); this.isSVG = !0; this.box = h; this.boxWrapper = b; this.alignedObjects = []; this.url = (z || l) && d.getElementsByTagName("base").length ? P.location.href.replace(/#.*?$/, "").replace(/<[^>]*>/g, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20") : ""; this.createElement("desc").add().element.appendChild(d.createTextNode("Created with Highstock 6.1.1")); this.defs = this.createElement("defs").add(); this.allowHTML = e;
                    this.forExport = c; this.gradients = {}; this.cache = {}; this.cacheKeys = []; this.imgCount = 0; this.setSize(g, x, !1); var k; z && a.getBoundingClientRect && (g = function () { A(a, { left: 0, top: 0 }); k = a.getBoundingClientRect(); A(a, { left: Math.ceil(k.left) - k.left + "px", top: Math.ceil(k.top) - k.top + "px" }) }, g(), this.unSubPixelFix = E(P, "resize", g))
                }, getStyle: function (a) { return this.style = k({ fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Arial, Helvetica, sans-serif', fontSize: "12px" }, a) }, setStyle: function (a) { this.boxWrapper.css(this.getStyle(a)) },
                isHidden: function () { return !this.boxWrapper.getBBox().width }, destroy: function () { var a = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); c(this.gradients || {}); this.gradients = null; a && (this.defs = a.destroy()); this.unSubPixelFix && this.unSubPixelFix(); return this.alignedObjects = null }, createElement: function (a) { var g = new this.Element; g.init(this, a); return g }, draw: K, getRadialAttr: function (a, g) { return { cx: a[0] - a[2] / 2 + g.cx * a[2], cy: a[1] - a[2] / 2 + g.cy * a[2], r: g.r * a[2] } }, getSpanWidth: function (a) { return a.getBBox(!0).width },
                applyEllipsis: function (a, g, x, b) { var c = a.rotation, e = x, h, k = 0, l = x.length, M = function (a) { g.removeChild(g.firstChild); a && g.appendChild(d.createTextNode(a)) }, B; a.rotation = 0; e = this.getSpanWidth(a, g); if (B = e > b) { for (; k <= l;)h = Math.ceil((k + l) / 2), e = x.substring(0, h) + "\u2026", M(e), e = this.getSpanWidth(a, g), k === l ? k = l + 1 : e > b ? l = h - 1 : k = h; 0 === l && M("") } a.rotation = c; return B }, escapes: { "\x26": "\x26amp;", "\x3c": "\x26lt;", "\x3e": "\x26gt;", "'": "\x26#39;", '"': "\x26quot;" }, buildText: function (a) {
                    var x = a.element, b = this, c = b.forExport,
                    h = B(a.textStr, "").toString(), k = -1 !== h.indexOf("\x3c"), l = x.childNodes, v, z = n(x, "x"), t = a.styles, N = a.textWidth, J = t && t.lineHeight, K = t && t.textOutline, p = t && "ellipsis" === t.textOverflow, G = t && "nowrap" === t.whiteSpace, H = t && t.fontSize, I, Q, f = l.length, t = N && !a.added && this.box, w = function (a) { var c; c = /(px|em)$/.test(a && a.style.fontSize) ? a.style.fontSize : H || b.style.fontSize || 12; return J ? g(J) : b.fontMetrics(c, a.getAttribute("style") ? a : x).h }, y = function (a, g) {
                        F(b.escapes, function (x, b) {
                        g && -1 !== u(x, g) || (a = a.toString().replace(new RegExp(x,
                            "g"), b))
                        }); return a
                    }, P = function (a, g) { var x; x = a.indexOf("\x3c"); a = a.substring(x, a.indexOf("\x3e") - x); x = a.indexOf(g + "\x3d"); if (-1 !== x && (x = x + g.length + 1, g = a.charAt(x), '"' === g || "'" === g)) return a = a.substring(x + 1), a.substring(0, a.indexOf(g)) }; I = [h, p, G, J, K, H, N].join(); if (I !== a.textCache) {
                        for (a.textCache = I; f--;)x.removeChild(l[f]); k || K || p || N || -1 !== h.indexOf(" ") ? (t && t.appendChild(x), h = k ? h.replace(/<(b|strong)>/g, '\x3cspan style\x3d"font-weight:bold"\x3e').replace(/<(i|em)>/g, '\x3cspan style\x3d"font-style:italic"\x3e').replace(/<a/g,
                            "\x3cspan").replace(/<\/(b|strong|i|em|a)>/g, "\x3c/span\x3e").split(/<br.*?>/g) : [h], h = e(h, function (a) { return "" !== a }), m(h, function (g, e) {
                                var h, k = 0; g = g.replace(/^\s+|\s+$/g, "").replace(/<span/g, "|||\x3cspan").replace(/<\/span>/g, "\x3c/span\x3e|||"); h = g.split("|||"); m(h, function (g) {
                                    if ("" !== g || 1 === h.length) {
                                        var l = {}, B = d.createElementNS(b.SVG_NS, "tspan"), u, F; (u = P(g, "class")) && n(B, "class", u); if (u = P(g, "style")) u = u.replace(/(;| |^)color([ :])/, "$1fill$2"), n(B, "style", u); (F = P(g, "href")) && !c && (n(B, "onclick",
                                            'location.href\x3d"' + F + '"'), n(B, "class", "highcharts-anchor"), A(B, { cursor: "pointer" })); g = y(g.replace(/<[a-zA-Z\/](.|\n)*?>/g, "") || " "); if (" " !== g) {
                                                B.appendChild(d.createTextNode(g)); k ? l.dx = 0 : e && null !== z && (l.x = z); n(B, l); x.appendChild(B); !k && Q && (!O && c && A(B, { display: "block" }), n(B, "dy", w(B))); if (N) {
                                                    l = g.replace(/([^\^])-/g, "$1- ").split(" "); F = 1 < h.length || e || 1 < l.length && !G; var t = [], m, J = w(B), K = a.rotation; for (p && (v = b.applyEllipsis(a, B, g, N)); !p && F && (l.length || t.length);)a.rotation = 0, m = b.getSpanWidth(a, B),
                                                        g = m > N, void 0 === v && (v = g), g && 1 !== l.length ? (B.removeChild(B.firstChild), t.unshift(l.pop())) : (l = t, t = [], l.length && !G && (B = d.createElementNS(M, "tspan"), n(B, { dy: J, x: z }), u && n(B, "style", u), x.appendChild(B)), m > N && (N = m + 1)), l.length && B.appendChild(d.createTextNode(l.join(" ").replace(/- /g, "-"))); a.rotation = K
                                                } k++
                                            }
                                    }
                                }); Q = Q || x.childNodes.length
                            }), p && v && a.attr("title", y(a.textStr, ["\x26lt;", "\x26gt;"])), t && t.removeChild(x), K && a.applyTextOutline && a.applyTextOutline(K)) : x.appendChild(d.createTextNode(y(h)))
                    }
                }, getContrast: function (a) {
                    a =
                    r(a).rgba; return 510 < a[0] + a[1] + a[2] ? "#000000" : "#FFFFFF"
                }, button: function (a, g, x, b, c, e, h, l, M) {
                    var B = this.label(a, g, x, M, null, null, null, null, "button"), u = 0; B.attr(H({ padding: 8, r: 2 }, c)); var F, d, t, v; c = H({ fill: "#f7f7f7", stroke: "#cccccc", "stroke-width": 1, style: { color: "#333333", cursor: "pointer", fontWeight: "normal" } }, c); F = c.style; delete c.style; e = H(c, { fill: "#e6e6e6" }, e); d = e.style; delete e.style; h = H(c, { fill: "#e6ebf5", style: { color: "#000000", fontWeight: "bold" } }, h); t = h.style; delete h.style; l = H(c, { style: { color: "#cccccc" } },
                        l); v = l.style; delete l.style; E(B.element, I ? "mouseover" : "mouseenter", function () { 3 !== u && B.setState(1) }); E(B.element, I ? "mouseout" : "mouseleave", function () { 3 !== u && B.setState(u) }); B.setState = function (a) { 1 !== a && (B.state = u = a); B.removeClass(/highcharts-button-(normal|hover|pressed|disabled)/).addClass("highcharts-button-" + ["normal", "hover", "pressed", "disabled"][a || 0]); B.attr([c, e, h, l][a || 0]).css([F, d, t, v][a || 0]) }; B.attr(c).css(k({ cursor: "default" }, F)); return B.on("click", function (a) { 3 !== u && b.call(B, a) })
                }, crispLine: function (a,
                    g) { a[1] === a[4] && (a[1] = a[4] = Math.round(a[1]) - g % 2 / 2); a[2] === a[5] && (a[2] = a[5] = Math.round(a[2]) + g % 2 / 2); return a }, path: function (a) { var g = { fill: "none" }; t(a) ? g.d = a : v(a) && k(g, a); return this.createElement("path").attr(g) }, circle: function (a, g, x) { a = v(a) ? a : { x: a, y: g, r: x }; g = this.createElement("circle"); g.xSetter = g.ySetter = function (a, g, x) { x.setAttribute("c" + g, a) }; return g.attr(a) }, arc: function (a, g, x, b, c, e) { v(a) ? (b = a, g = b.y, x = b.r, a = b.x) : b = { innerR: b, start: c, end: e }; a = this.symbol("arc", a, g, x, x, b); a.r = x; return a }, rect: function (a,
                        g, x, b, c, e) { c = v(a) ? a.r : c; var h = this.createElement("rect"); a = v(a) ? a : void 0 === a ? {} : { x: a, y: g, width: Math.max(x, 0), height: Math.max(b, 0) }; void 0 !== e && (a.strokeWidth = e, a = h.crisp(a)); a.fill = "none"; c && (a.r = c); h.rSetter = function (a, g, x) { n(x, { rx: a, ry: a }) }; return h.attr(a) }, setSize: function (a, g, x) {
                            var b = this.alignedObjects, c = b.length; this.width = a; this.height = g; for (this.boxWrapper.animate({ width: a, height: g }, {
                                step: function () { this.attr({ viewBox: "0 0 " + this.attr("width") + " " + this.attr("height") }) }, duration: B(x, !0) ?
                                    void 0 : 0
                            }); c--;)b[c].align()
                        }, g: function (a) { var g = this.createElement("g"); return a ? g.attr({ "class": "highcharts-" + a }) : g }, image: function (a, g, x, b, c, e) {
                            var h = { preserveAspectRatio: "none" }, l, B = function (a, g) { a.setAttributeNS ? a.setAttributeNS("http://www.w3.org/1999/xlink", "href", g) : a.setAttribute("hc-svg-href", g) }, M = function (g) { B(l.element, a); e.call(l, g) }; 1 < arguments.length && k(h, { x: g, y: x, width: b, height: c }); l = this.createElement("image").attr(h); e ? (B(l.element, "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw\x3d\x3d"),
                                h = new P.Image, E(h, "load", M), h.src = a, h.complete && M({})) : B(l.element, a); return l
                        }, symbol: function (a, g, x, b, c, e) {
                            var h = this, l, M = /^url\((.*?)\)$/, u = M.test(a), F = !u && (this.symbols[a] ? a : "circle"), t = F && this.symbols[F], v = y(g) && t && t.call(this.symbols, Math.round(g), Math.round(x), b, c, e), z, N; t ? (l = this.path(v), l.attr("fill", "none"), k(l, { symbolName: F, x: g, y: x, width: b, height: c }), e && k(l, e)) : u && (z = a.match(M)[1], l = this.image(z), l.imgwidth = B(Q[z] && Q[z].width, e && e.width), l.imgheight = B(Q[z] && Q[z].height, e && e.height), N = function () {
                                l.attr({
                                    width: l.width,
                                    height: l.height
                                })
                            }, m(["width", "height"], function (a) { l[a + "Setter"] = function (a, g) { var x = {}, b = this["img" + g], c = "width" === g ? "translateX" : "translateY"; this[g] = a; y(b) && (this.element && this.element.setAttribute(g, b), this.alignByTranslate || (x[c] = ((this[g] || 0) - b) / 2, this.attr(x))) } }), y(g) && l.attr({ x: g, y: x }), l.isImg = !0, y(l.imgwidth) && y(l.imgheight) ? N() : (l.attr({ width: 0, height: 0 }), w("img", {
                                onload: function () {
                                    var a = f[h.chartIndex]; 0 === this.width && (A(this, { position: "absolute", top: "-999em" }), d.body.appendChild(this));
                                    Q[z] = { width: this.width, height: this.height }; l.imgwidth = this.width; l.imgheight = this.height; l.element && N(); this.parentNode && this.parentNode.removeChild(this); h.imgCount--; if (!h.imgCount && a && a.onload) a.onload()
                                }, src: z
                            }), this.imgCount++)); return l
                        }, symbols: {
                            circle: function (a, g, x, b) { return this.arc(a + x / 2, g + b / 2, x / 2, b / 2, { start: 0, end: 2 * Math.PI, open: !1 }) }, square: function (a, g, x, b) { return ["M", a, g, "L", a + x, g, a + x, g + b, a, g + b, "Z"] }, triangle: function (a, g, x, b) { return ["M", a + x / 2, g, "L", a + x, g + b, a, g + b, "Z"] }, "triangle-down": function (a,
                                g, x, b) { return ["M", a, g, "L", a + x, g, a + x / 2, g + b, "Z"] }, diamond: function (a, g, x, b) { return ["M", a + x / 2, g, "L", a + x, g + b / 2, a + x / 2, g + b, a, g + b / 2, "Z"] }, arc: function (a, g, x, b, c) { var e = c.start, h = c.r || x, l = c.r || b || x, k = c.end - .001; x = c.innerR; b = B(c.open, .001 > Math.abs(c.end - c.start - 2 * Math.PI)); var M = Math.cos(e), u = Math.sin(e), F = Math.cos(k), k = Math.sin(k); c = .001 > c.end - e - Math.PI ? 0 : 1; h = ["M", a + h * M, g + l * u, "A", h, l, 0, c, 1, a + h * F, g + l * k]; y(x) && h.push(b ? "M" : "L", a + x * F, g + x * k, "A", x, x, 0, c, 0, a + x * M, g + x * u); h.push(b ? "" : "Z"); return h }, callout: function (a,
                                    g, x, b, c) {
                                        var e = Math.min(c && c.r || 0, x, b), h = e + 6, l = c && c.anchorX; c = c && c.anchorY; var k; k = ["M", a + e, g, "L", a + x - e, g, "C", a + x, g, a + x, g, a + x, g + e, "L", a + x, g + b - e, "C", a + x, g + b, a + x, g + b, a + x - e, g + b, "L", a + e, g + b, "C", a, g + b, a, g + b, a, g + b - e, "L", a, g + e, "C", a, g, a, g, a + e, g]; l && l > x ? c > g + h && c < g + b - h ? k.splice(13, 3, "L", a + x, c - 6, a + x + 6, c, a + x, c + 6, a + x, g + b - e) : k.splice(13, 3, "L", a + x, b / 2, l, c, a + x, b / 2, a + x, g + b - e) : l && 0 > l ? c > g + h && c < g + b - h ? k.splice(33, 3, "L", a, c + 6, a - 6, c, a, c - 6, a, g + e) : k.splice(33, 3, "L", a, b / 2, l, c, a, b / 2, a, g + e) : c && c > b && l > a + h && l < a + x - h ? k.splice(23,
                                            3, "L", l + 6, g + b, l, g + b + 6, l - 6, g + b, a + e, g + b) : c && 0 > c && l > a + h && l < a + x - h && k.splice(3, 3, "L", l - 6, g, l, g - 6, l + 6, g, x - e, g); return k
                                }
                        }, clipRect: function (g, x, b, c) { var e = a.uniqueKey(), h = this.createElement("clipPath").attr({ id: e }).add(this.defs); g = this.rect(g, x, b, c, 0).add(h); g.id = e; g.clipPath = h; g.count = 0; return g }, text: function (a, g, x, b) {
                            var c = {}; if (b && (this.allowHTML || !this.forExport)) return this.html(a, g, x); c.x = Math.round(g || 0); x && (c.y = Math.round(x)); if (a || 0 === a) c.text = a; a = this.createElement("text").attr(c); b || (a.xSetter =
                                function (a, g, x) { var b = x.getElementsByTagName("tspan"), c, e = x.getAttribute(g), h; for (h = 0; h < b.length; h++)c = b[h], c.getAttribute(g) === e && c.setAttribute(g, a); x.setAttribute(g, a) }); return a
                        }, fontMetrics: function (a, x) { a = a || x && x.style && x.style.fontSize || this.style && this.style.fontSize; a = /px/.test(a) ? g(a) : /em/.test(a) ? parseFloat(a) * (x ? this.fontMetrics(null, x.parentNode).f : 16) : 12; x = 24 > a ? a + 3 : Math.round(1.2 * a); return { h: x, b: Math.round(.8 * x), f: a } }, rotCorr: function (a, g, x) {
                            var b = a; g && x && (b = Math.max(b * Math.cos(g * p),
                                4)); return { x: -a / 3 * Math.sin(g * p), y: b }
                        }, label: function (g, b, c, e, h, l, M, B, u) {
                            var F = this, d = F.g("button" !== u && "label"), t = d.text = F.text("", 0, 0, M).attr({ zIndex: 1 }), v, z, N = 0, O = 3, K = 0, p, G, I, Q, f, n = {}, w, J, P = /^url\((.*?)\)$/.test(e), A = P, r, q, T, R; u && d.addClass("highcharts-" + u); A = P; r = function () { return (w || 0) % 2 / 2 }; q = function () {
                                var a = t.element.style, g = {}; z = (void 0 === p || void 0 === G || f) && y(t.textStr) && t.getBBox(); d.width = (p || z.width || 0) + 2 * O + K; d.height = (G || z.height || 0) + 2 * O; J = O + F.fontMetrics(a && a.fontSize, t).b; A && (v || (d.box =
                                    v = F.symbols[e] || P ? F.symbol(e) : F.rect(), v.addClass(("button" === u ? "" : "highcharts-label-box") + (u ? " highcharts-" + u + "-box" : "")), v.add(d), a = r(), g.x = a, g.y = (B ? -J : 0) + a), g.width = Math.round(d.width), g.height = Math.round(d.height), v.attr(k(g, n)), n = {})
                            }; T = function () { var a = K + O, g; g = B ? 0 : J; y(p) && z && ("center" === f || "right" === f) && (a += { center: .5, right: 1 }[f] * (p - z.width)); if (a !== t.x || g !== t.y) t.attr("x", a), t.hasBoxWidthChanged && (z = t.getBBox(!0), q()), void 0 !== g && t.attr("y", g); t.x = a; t.y = g }; R = function (a, g) {
                                v ? v.attr(a, g) : n[a] =
                                    g
                            }; d.onAdd = function () { t.add(d); d.attr({ text: g || 0 === g ? g : "", x: b, y: c }); v && y(h) && d.attr({ anchorX: h, anchorY: l }) }; d.widthSetter = function (g) { p = a.isNumber(g) ? g : null }; d.heightSetter = function (a) { G = a }; d["text-alignSetter"] = function (a) { f = a }; d.paddingSetter = function (a) { y(a) && a !== O && (O = d.padding = a, T()) }; d.paddingLeftSetter = function (a) { y(a) && a !== K && (K = a, T()) }; d.alignSetter = function (a) { a = { left: 0, center: .5, right: 1 }[a]; a !== N && (N = a, z && d.attr({ x: I })) }; d.textSetter = function (a) { void 0 !== a && t.textSetter(a); q(); T() }; d["stroke-widthSetter"] =
                                function (a, g) { a && (A = !0); w = this["stroke-width"] = a; R(g, a) }; d.strokeSetter = d.fillSetter = d.rSetter = function (a, g) { "r" !== g && ("fill" === g && a && (A = !0), d[g] = a); R(g, a) }; d.anchorXSetter = function (a, g) { h = d.anchorX = a; R(g, Math.round(a) - r() - I) }; d.anchorYSetter = function (a, g) { l = d.anchorY = a; R(g, a - Q) }; d.xSetter = function (a) { d.x = a; N && (a -= N * ((p || z.width) + 2 * O), d["forceAnimate:x"] = !0); I = Math.round(a); d.attr("translateX", I) }; d.ySetter = function (a) { Q = d.y = Math.round(a); d.attr("translateY", Q) }; var S = d.css; return k(d, {
                                    css: function (a) {
                                        if (a) {
                                            var g =
                                                {}; a = H(a); m(d.textProps, function (x) { void 0 !== a[x] && (g[x] = a[x], delete a[x]) }); t.css(g); "width" in g && q()
                                        } return S.call(d, a)
                                    }, getBBox: function () { return { width: z.width + 2 * O, height: z.height + 2 * O, x: z.x - O, y: z.y - O } }, shadow: function (a) { a && (q(), v && v.shadow(a)); return d }, destroy: function () { x(d.element, "mouseenter"); x(d.element, "mouseleave"); t && (t = t.destroy()); v && (v = v.destroy()); C.prototype.destroy.call(d); d = F = q = T = R = null }
                                })
                        }
            }); a.Renderer = D
    })(L); (function (a) {
        var C = a.attr, D = a.createElement, E = a.css, q = a.defined, n = a.each,
        f = a.extend, r = a.isFirefox, A = a.isMS, w = a.isWebKit, y = a.pick, p = a.pInt, c = a.SVGRenderer, d = a.win, m = a.wrap; f(a.SVGElement.prototype, {
            htmlCss: function (a) { var b = this.element; if (b = a && "SPAN" === b.tagName && a.width) delete a.width, this.textWidth = b, this.htmlUpdateTransform(); a && "ellipsis" === a.textOverflow && (a.whiteSpace = "nowrap", a.overflow = "hidden"); this.styles = f(this.styles, a); E(this.element, a); return this }, htmlGetBBox: function () { var a = this.element; return { x: a.offsetLeft, y: a.offsetTop, width: a.offsetWidth, height: a.offsetHeight } },
            htmlUpdateTransform: function () {
                if (this.added) {
                    var a = this.renderer, b = this.element, c = this.translateX || 0, h = this.translateY || 0, d = this.x || 0, t = this.y || 0, z = this.textAlign || "left", m = { left: 0, center: .5, right: 1 }[z], v = this.styles, G = v && v.whiteSpace; E(b, { marginLeft: c, marginTop: h }); this.shadows && n(this.shadows, function (a) { E(a, { marginLeft: c + 1, marginTop: h + 1 }) }); this.inverted && n(b.childNodes, function (c) { a.invertChild(c, b) }); if ("SPAN" === b.tagName) {
                        var v = this.rotation, l = this.textWidth && p(this.textWidth), H = [v, z, b.innerHTML,
                            this.textWidth, this.textAlign].join(), K; (K = l !== this.oldTextWidth) && !(K = l > this.oldTextWidth) && ((K = this.textPxLength) || (E(b, { width: "", whiteSpace: G || "nowrap" }), K = b.offsetWidth), K = K > l); K && /[ \-]/.test(b.textContent || b.innerText) ? (E(b, { width: l + "px", display: "block", whiteSpace: G || "normal" }), this.oldTextWidth = l, this.hasBoxWidthChanged = !0) : this.hasBoxWidthChanged = !1; H !== this.cTT && (G = a.fontMetrics(b.style.fontSize).b, q(v) && v !== (this.oldRotation || 0) && this.setSpanRotation(v, m, G), this.getSpanCorrection(!q(v) &&
                                this.textPxLength || b.offsetWidth, G, m, v, z)); E(b, { left: d + (this.xCorr || 0) + "px", top: t + (this.yCorr || 0) + "px" }); this.cTT = H; this.oldRotation = v
                    }
                } else this.alignOnAdd = !0
            }, setSpanRotation: function (a, b, c) { var e = {}, k = this.renderer.getTransformKey(); e[k] = e.transform = "rotate(" + a + "deg)"; e[k + (r ? "Origin" : "-origin")] = e.transformOrigin = 100 * b + "% " + c + "px"; E(this.element, e) }, getSpanCorrection: function (a, b, c) { this.xCorr = -a * c; this.yCorr = -b }
        }); f(c.prototype, {
            getTransformKey: function () {
                return A && !/Edge/.test(d.navigator.userAgent) ?
                    "-ms-transform" : w ? "-webkit-transform" : r ? "MozTransform" : d.opera ? "-o-transform" : ""
            }, html: function (a, b, c) {
                var e = this.createElement("span"), k = e.element, d = e.renderer, z = d.isSVG, p = function (a, b) { n(["opacity", "visibility"], function (c) { m(a, c + "Setter", function (a, c, e, l) { a.call(this, c, e, l); b[e] = c }) }); a.addedSetters = !0 }; e.textSetter = function (a) { a !== k.innerHTML && delete this.bBox; this.textStr = a; k.innerHTML = y(a, ""); e.doTransform = !0 }; z && p(e, e.element.style); e.xSetter = e.ySetter = e.alignSetter = e.rotationSetter = function (a,
                    b) { "align" === b && (b = "textAlign"); e[b] = a; e.doTransform = !0 }; e.afterSetters = function () { this.doTransform && (this.htmlUpdateTransform(), this.doTransform = !1) }; e.attr({ text: a, x: Math.round(b), y: Math.round(c) }).css({ fontFamily: this.style.fontFamily, fontSize: this.style.fontSize, position: "absolute" }); k.style.whiteSpace = "nowrap"; e.css = e.htmlCss; z && (e.add = function (a) {
                        var b, c = d.box.parentNode, h = []; if (this.parentGroup = a) {
                            if (b = a.div, !b) {
                                for (; a;)h.push(a), a = a.parentGroup; n(h.reverse(), function (a) {
                                    function l(g, b) {
                                    a[b] =
                                        g; "translateX" === b ? k.left = g + "px" : k.top = g + "px"; a.doTransform = !0
                                    } var k, g = C(a.element, "class"); g && (g = { className: g }); b = a.div = a.div || D("div", g, { position: "absolute", left: (a.translateX || 0) + "px", top: (a.translateY || 0) + "px", display: a.display, opacity: a.opacity, pointerEvents: a.styles && a.styles.pointerEvents }, b || c); k = b.style; f(a, {
                                        classSetter: function (a) { return function (g) { this.element.setAttribute("class", g); a.className = g } }(b), on: function () { h[0].div && e.on.apply({ element: h[0].div }, arguments); return a }, translateXSetter: l,
                                        translateYSetter: l
                                    }); a.addedSetters || p(a, k)
                                })
                            }
                        } else b = c; b.appendChild(k); e.added = !0; e.alignOnAdd && e.htmlUpdateTransform(); return e
                    }); return e
            }
        })
    })(L); (function (a) {
        var C = a.defined, D = a.each, E = a.extend, q = a.merge, n = a.pick, f = a.timeUnits, r = a.win; a.Time = function (a) { this.update(a, !1) }; a.Time.prototype = {
            defaultOptions: {}, update: function (f) {
                var w = n(f && f.useUTC, !0), y = this; this.options = f = q(!0, this.options || {}, f); this.Date = f.Date || r.Date; this.timezoneOffset = (this.useUTC = w) && f.timezoneOffset; this.getTimezoneOffset =
                    this.timezoneOffsetFunction(); (this.variableTimezone = !(w && !f.getTimezoneOffset && !f.timezone)) || this.timezoneOffset ? (this.get = function (a, c) { var d = c.getTime(), m = d - y.getTimezoneOffset(c); c.setTime(m); a = c["getUTC" + a](); c.setTime(d); return a }, this.set = function (p, c, d) { var m; if (-1 !== a.inArray(p, ["Milliseconds", "Seconds", "Minutes"])) c["set" + p](d); else m = y.getTimezoneOffset(c), m = c.getTime() - m, c.setTime(m), c["setUTC" + p](d), p = y.getTimezoneOffset(c), m = c.getTime() + p, c.setTime(m) }) : w ? (this.get = function (a, c) {
                        return c["getUTC" +
                            a]()
                    }, this.set = function (a, c, d) { return c["setUTC" + a](d) }) : (this.get = function (a, c) { return c["get" + a]() }, this.set = function (a, c, d) { return c["set" + a](d) })
            }, makeTime: function (f, w, y, p, c, d) { var m, k, b; this.useUTC ? (m = this.Date.UTC.apply(0, arguments), k = this.getTimezoneOffset(m), m += k, b = this.getTimezoneOffset(m), k !== b ? m += b - k : k - 36E5 !== this.getTimezoneOffset(m - 36E5) || a.isSafari || (m -= 36E5)) : m = (new this.Date(f, w, n(y, 1), n(p, 0), n(c, 0), n(d, 0))).getTime(); return m }, timezoneOffsetFunction: function () {
                var f = this, n = this.options,
                y = r.moment; if (!this.useUTC) return function (a) { return 6E4 * (new Date(a)).getTimezoneOffset() }; if (n.timezone) { if (y) return function (a) { return 6E4 * -y.tz(a, n.timezone).utcOffset() }; a.error(25) } return this.useUTC && n.getTimezoneOffset ? function (a) { return 6E4 * n.getTimezoneOffset(a) } : function () { return 6E4 * (f.timezoneOffset || 0) }
            }, dateFormat: function (f, n, y) {
                if (!a.defined(n) || isNaN(n)) return a.defaultOptions.lang.invalidDate || ""; f = a.pick(f, "%Y-%m-%d %H:%M:%S"); var p = this, c = new this.Date(n), d = this.get("Hours", c),
                    m = this.get("Day", c), k = this.get("Date", c), b = this.get("Month", c), e = this.get("FullYear", c), h = a.defaultOptions.lang, u = h.weekdays, t = h.shortWeekdays, z = a.pad, c = a.extend({ a: t ? t[m] : u[m].substr(0, 3), A: u[m], d: z(k), e: z(k, 2, " "), w: m, b: h.shortMonths[b], B: h.months[b], m: z(b + 1), o: b + 1, y: e.toString().substr(2, 2), Y: e, H: z(d), k: d, I: z(d % 12 || 12), l: d % 12 || 12, M: z(p.get("Minutes", c)), p: 12 > d ? "AM" : "PM", P: 12 > d ? "am" : "pm", S: z(c.getSeconds()), L: z(Math.round(n % 1E3), 3) }, a.dateFormats); a.objectEach(c, function (a, b) {
                        for (; -1 !== f.indexOf("%" +
                            b);)f = f.replace("%" + b, "function" === typeof a ? a.call(p, n) : a)
                    }); return y ? f.substr(0, 1).toUpperCase() + f.substr(1) : f
            }, getTimeTicks: function (a, w, y, p) {
                var c = this, d = [], m = {}, k, b = new c.Date(w), e = a.unitRange, h = a.count || 1, u; if (C(w)) {
                    c.set("Milliseconds", b, e >= f.second ? 0 : h * Math.floor(c.get("Milliseconds", b) / h)); e >= f.second && c.set("Seconds", b, e >= f.minute ? 0 : h * Math.floor(c.get("Seconds", b) / h)); e >= f.minute && c.set("Minutes", b, e >= f.hour ? 0 : h * Math.floor(c.get("Minutes", b) / h)); e >= f.hour && c.set("Hours", b, e >= f.day ? 0 : h * Math.floor(c.get("Hours",
                        b) / h)); e >= f.day && c.set("Date", b, e >= f.month ? 1 : h * Math.floor(c.get("Date", b) / h)); e >= f.month && (c.set("Month", b, e >= f.year ? 0 : h * Math.floor(c.get("Month", b) / h)), k = c.get("FullYear", b)); e >= f.year && c.set("FullYear", b, k - k % h); e === f.week && c.set("Date", b, c.get("Date", b) - c.get("Day", b) + n(p, 1)); k = c.get("FullYear", b); p = c.get("Month", b); var t = c.get("Date", b), z = c.get("Hours", b); w = b.getTime(); c.variableTimezone && (u = y - w > 4 * f.month || c.getTimezoneOffset(w) !== c.getTimezoneOffset(y)); b = b.getTime(); for (w = 1; b < y;)d.push(b), b =
                            e === f.year ? c.makeTime(k + w * h, 0) : e === f.month ? c.makeTime(k, p + w * h) : !u || e !== f.day && e !== f.week ? u && e === f.hour && 1 < h ? c.makeTime(k, p, t, z + w * h) : b + e * h : c.makeTime(k, p, t + w * h * (e === f.day ? 1 : 7)), w++; d.push(b); e <= f.hour && 1E4 > d.length && D(d, function (a) { 0 === a % 18E5 && "000000000" === c.dateFormat("%H%M%S%L", a) && (m[a] = "day") })
                } d.info = E(a, { higherRanks: m, totalRange: e * h }); return d
            }
        }
    })(L); (function (a) {
        var C = a.color, D = a.merge; a.defaultOptions = {
            colors: "#7cb5ec #434348 #90ed7d #f7a35c #8085e9 #f15c80 #e4d354 #2b908f #f45b5b #91e8e1".split(" "),
            symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: { loading: "Loading...", months: "January February March April May June July August September October November December".split(" "), shortMonths: "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "), weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "), decimalPoint: ".", numericSymbols: "kMGTPE".split(""), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: " " }, global: {}, time: a.Time.prototype.defaultOptions,
            chart: { borderRadius: 0, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacing: [10, 10, 15, 10], resetZoomButton: { theme: { zIndex: 6 }, position: { align: "right", x: -10, y: 10 } }, width: null, height: null, borderColor: "#335cad", backgroundColor: "#ffffff", plotBorderColor: "#cccccc" }, title: { text: "Chart title", align: "center", margin: 15, widthAdjust: -44 }, subtitle: { text: "", align: "center", widthAdjust: -44 }, plotOptions: {}, labels: { style: { position: "absolute", color: "#333333" } }, legend: {
                enabled: !0, align: "center", alignColumns: !0, layout: "horizontal",
                labelFormatter: function () { return this.name }, borderColor: "#999999", borderRadius: 0, navigation: { activeColor: "#003399", inactiveColor: "#cccccc" }, itemStyle: { color: "#333333", fontSize: "12px", fontWeight: "bold", textOverflow: "ellipsis" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#cccccc" }, shadow: !1, itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" }, squareSymbol: !0, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0, title: { style: { fontWeight: "bold" } }
            }, loading: {
                labelStyle: {
                    fontWeight: "bold",
                    position: "relative", top: "45%"
                }, style: { position: "absolute", backgroundColor: "#ffffff", opacity: .5, textAlign: "center" }
            }, tooltip: {
                enabled: !0, animation: a.svg, borderRadius: 3, dateTimeLabelFormats: { millisecond: "%A, %b %e, %H:%M:%S.%L", second: "%A, %b %e, %H:%M:%S", minute: "%A, %b %e, %H:%M", hour: "%A, %b %e, %H:%M", day: "%A, %b %e, %Y", week: "Week from %A, %b %e, %Y", month: "%B %Y", year: "%Y" }, footerFormat: "", padding: 8, snap: a.isTouchDevice ? 25 : 10, backgroundColor: C("#f7f7f7").setOpacity(.85).get(), borderWidth: 1, headerFormat: '\x3cspan style\x3d"font-size: 10px"\x3e{point.key}\x3c/span\x3e\x3cbr/\x3e',
                pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e {series.name}: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e', shadow: !0, style: { color: "#333333", cursor: "default", fontSize: "12px", pointerEvents: "none", whiteSpace: "nowrap" }
            }, credits: { enabled: !0, href: "http://www.highcharts.com", position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 }, style: { cursor: "pointer", color: "#999999", fontSize: "9px" }, text: "Highcharts.com" }
        }; a.setOptions = function (C) {
        a.defaultOptions = D(!0, a.defaultOptions, C); a.time.update(D(a.defaultOptions.global,
            a.defaultOptions.time), !1); return a.defaultOptions
        }; a.getOptions = function () { return a.defaultOptions }; a.defaultPlotOptions = a.defaultOptions.plotOptions; a.time = new a.Time(D(a.defaultOptions.global, a.defaultOptions.time)); a.dateFormat = function (D, q, n) { return a.time.dateFormat(D, q, n) }
    })(L); (function (a) {
        var C = a.correctFloat, D = a.defined, E = a.destroyObjectProperties, q = a.fireEvent, n = a.isNumber, f = a.merge, r = a.pick, A = a.deg2rad; a.Tick = function (a, f, p, c) {
        this.axis = a; this.pos = f; this.type = p || ""; this.isNewLabel = this.isNew =
            !0; p || c || this.addLabel()
        }; a.Tick.prototype = {
            addLabel: function () {
                var a = this.axis, n = a.options, p = a.chart, c = a.categories, d = a.names, m = this.pos, k = n.labels, b = a.tickPositions, e = m === b[0], h = m === b[b.length - 1], d = c ? r(c[m], d[m], m) : m, c = this.label, b = b.info, u; a.isDatetimeAxis && b && (u = n.dateTimeLabelFormats[b.higherRanks[m] || b.unitName]); this.isFirst = e; this.isLast = h; n = a.labelFormatter.call({ axis: a, chart: p, isFirst: e, isLast: h, dateTimeLabelFormat: u, value: a.isLog ? C(a.lin2log(d)) : d, pos: m }); if (D(c)) c && c.attr({ text: n }); else {
                    if (this.label =
                        c = D(n) && k.enabled ? p.renderer.text(n, 0, 0, k.useHTML).css(f(k.style)).add(a.labelGroup) : null) c.textPxLength = c.getBBox().width; this.rotation = 0
                }
            }, getLabelSize: function () { return this.label ? this.label.getBBox()[this.axis.horiz ? "height" : "width"] : 0 }, handleOverflow: function (a) {
                var f = this.axis, p = f.options.labels, c = a.x, d = f.chart.chartWidth, m = f.chart.spacing, k = r(f.labelLeft, Math.min(f.pos, m[3])), m = r(f.labelRight, Math.max(f.isRadial ? 0 : f.pos + f.len, d - m[1])), b = this.label, e = this.rotation, h = { left: 0, center: .5, right: 1 }[f.labelAlign ||
                    b.attr("align")], u = b.getBBox().width, t = f.getSlotWidth(this), z = t, I = 1, v, G = {}; if (e || !1 === p.overflow) 0 > e && c - h * u < k ? v = Math.round(c / Math.cos(e * A) - k) : 0 < e && c + h * u > m && (v = Math.round((d - c) / Math.cos(e * A))); else if (d = c + (1 - h) * u, c - h * u < k ? z = a.x + z * (1 - h) - k : d > m && (z = m - a.x + z * h, I = -1), z = Math.min(t, z), z < t && "center" === f.labelAlign && (a.x += I * (t - z - h * (t - Math.min(u, z)))), u > z || f.autoRotation && (b.styles || {}).width) v = z; v && (G.width = v, (p.style || {}).textOverflow || (G.textOverflow = "ellipsis"), b.css(G))
            }, getPosition: function (f, n, p, c) {
                var d =
                    this.axis, m = d.chart, k = c && m.oldChartHeight || m.chartHeight; f = { x: f ? a.correctFloat(d.translate(n + p, null, null, c) + d.transB) : d.left + d.offset + (d.opposite ? (c && m.oldChartWidth || m.chartWidth) - d.right - d.left : 0), y: f ? k - d.bottom + d.offset - (d.opposite ? d.height : 0) : a.correctFloat(k - d.translate(n + p, null, null, c) - d.transB) }; q(this, "afterGetPosition", { pos: f }); return f
            }, getLabelPosition: function (a, f, p, c, d, m, k, b) {
                var e = this.axis, h = e.transA, u = e.reversed, t = e.staggerLines, z = e.tickRotCorr || { x: 0, y: 0 }, I = d.y, v = c || e.reserveSpaceDefault ?
                    0 : -e.labelOffset * ("center" === e.labelAlign ? .5 : 1), G = {}; D(I) || (I = 0 === e.side ? p.rotation ? -8 : -p.getBBox().height : 2 === e.side ? z.y + 8 : Math.cos(p.rotation * A) * (z.y - p.getBBox(!1, 0).height / 2)); a = a + d.x + v + z.x - (m && c ? m * h * (u ? -1 : 1) : 0); f = f + I - (m && !c ? m * h * (u ? 1 : -1) : 0); t && (p = k / (b || 1) % t, e.opposite && (p = t - p - 1), f += e.labelOffset / t * p); G.x = a; G.y = Math.round(f); q(this, "afterGetLabelPosition", { pos: G }); return G
            }, getMarkPath: function (a, f, p, c, d, m) { return m.crispLine(["M", a, f, "L", a + (d ? 0 : -p), f + (d ? p : 0)], c) }, renderGridLine: function (a, f, p) {
                var c =
                    this.axis, d = c.options, m = this.gridLine, k = {}, b = this.pos, e = this.type, h = c.tickmarkOffset, u = c.chart.renderer, t = e ? e + "Grid" : "grid", z = d[t + "LineWidth"], I = d[t + "LineColor"], d = d[t + "LineDashStyle"]; m || (k.stroke = I, k["stroke-width"] = z, d && (k.dashstyle = d), e || (k.zIndex = 1), a && (k.opacity = 0), this.gridLine = m = u.path().attr(k).addClass("highcharts-" + (e ? e + "-" : "") + "grid-line").add(c.gridGroup)); if (!a && m && (a = c.getPlotLinePath(b + h, m.strokeWidth() * p, a, !0))) m[this.isNew ? "attr" : "animate"]({ d: a, opacity: f })
            }, renderMark: function (a,
                f, p) { var c = this.axis, d = c.options, m = c.chart.renderer, k = this.type, b = k ? k + "Tick" : "tick", e = c.tickSize(b), h = this.mark, u = !h, t = a.x; a = a.y; var z = r(d[b + "Width"], !k && c.isXAxis ? 1 : 0), d = d[b + "Color"]; e && (c.opposite && (e[0] = -e[0]), u && (this.mark = h = m.path().addClass("highcharts-" + (k ? k + "-" : "") + "tick").add(c.axisGroup), h.attr({ stroke: d, "stroke-width": z })), h[u ? "attr" : "animate"]({ d: this.getMarkPath(t, a, e[0], h.strokeWidth() * p, c.horiz, m), opacity: f })) }, renderLabel: function (a, f, p, c) {
                    var d = this.axis, m = d.horiz, k = d.options, b = this.label,
                    e = k.labels, h = e.step, d = d.tickmarkOffset, u = !0, t = a.x; a = a.y; b && n(t) && (b.xy = a = this.getLabelPosition(t, a, b, m, e, d, c, h), this.isFirst && !this.isLast && !r(k.showFirstLabel, 1) || this.isLast && !this.isFirst && !r(k.showLastLabel, 1) ? u = !1 : !m || e.step || e.rotation || f || 0 === p || this.handleOverflow(a), h && c % h && (u = !1), u && n(a.y) ? (a.opacity = p, b[this.isNewLabel ? "attr" : "animate"](a), this.isNewLabel = !1) : (b.attr("y", -9999), this.isNewLabel = !0))
                }, render: function (f, n, p) {
                    var c = this.axis, d = c.horiz, m = this.getPosition(d, this.pos, c.tickmarkOffset,
                        n), k = m.x, b = m.y, c = d && k === c.pos + c.len || !d && b === c.pos ? -1 : 1; p = r(p, 1); this.isActive = !0; this.renderGridLine(n, p, c); this.renderMark(m, p, c); this.renderLabel(m, n, p, f); this.isNew = !1; a.fireEvent(this, "afterRender")
                }, destroy: function () { E(this, this.axis) }
        }
    })(L); var ea = function (a) {
        var C = a.addEvent, D = a.animObject, E = a.arrayMax, q = a.arrayMin, n = a.color, f = a.correctFloat, r = a.defaultOptions, A = a.defined, w = a.deg2rad, y = a.destroyObjectProperties, p = a.each, c = a.extend, d = a.fireEvent, m = a.format, k = a.getMagnitude, b = a.grep, e = a.inArray,
        h = a.isArray, u = a.isNumber, t = a.isString, z = a.merge, I = a.normalizeTickInterval, v = a.objectEach, G = a.pick, l = a.removeEvent, H = a.splat, K = a.syncTimeout, F = a.Tick, B = function () { this.init.apply(this, arguments) }; a.extend(B.prototype, {
            defaultOptions: {
                dateTimeLabelFormats: { millisecond: "%H:%M:%S.%L", second: "%H:%M:%S", minute: "%H:%M", hour: "%H:%M", day: "%e. %b", week: "%e. %b", month: "%b '%y", year: "%Y" }, endOnTick: !1, labels: { enabled: !0, style: { color: "#666666", cursor: "default", fontSize: "11px" }, x: 0 }, maxPadding: .01, minorTickLength: 2,
                minorTickPosition: "outside", minPadding: .01, startOfWeek: 1, startOnTick: !1, tickLength: 10, tickmarkPlacement: "between", tickPixelInterval: 100, tickPosition: "outside", title: { align: "middle", style: { color: "#666666" } }, type: "linear", minorGridLineColor: "#f2f2f2", minorGridLineWidth: 1, minorTickColor: "#999999", lineColor: "#ccd6eb", lineWidth: 1, gridLineColor: "#e6e6e6", tickColor: "#ccd6eb"
            }, defaultYAxisOptions: {
                endOnTick: !0, tickPixelInterval: 72, showLastLabel: !0, labels: { x: -8 }, maxPadding: .05, minPadding: .05, startOnTick: !0,
                title: { rotation: 270, text: "Values" }, stackLabels: { allowOverlap: !1, enabled: !1, formatter: function () { return a.numberFormat(this.total, -1) }, style: { fontSize: "11px", fontWeight: "bold", color: "#000000", textOutline: "1px contrast" } }, gridLineWidth: 1, lineWidth: 0
            }, defaultLeftAxisOptions: { labels: { x: -15 }, title: { rotation: 270 } }, defaultRightAxisOptions: { labels: { x: 15 }, title: { rotation: 90 } }, defaultBottomAxisOptions: { labels: { autoRotation: [-45], x: 0 }, title: { rotation: 0 } }, defaultTopAxisOptions: {
                labels: { autoRotation: [-45], x: 0 },
                title: { rotation: 0 }
            }, init: function (a, x) {
                var g = x.isX, b = this; b.chart = a; b.horiz = a.inverted && !b.isZAxis ? !g : g; b.isXAxis = g; b.coll = b.coll || (g ? "xAxis" : "yAxis"); d(this, "init", { userOptions: x }); b.opposite = x.opposite; b.side = x.side || (b.horiz ? b.opposite ? 0 : 2 : b.opposite ? 1 : 3); b.setOptions(x); var c = this.options, l = c.type; b.labelFormatter = c.labels.formatter || b.defaultLabelFormatter; b.userOptions = x; b.minPixelPadding = 0; b.reversed = c.reversed; b.visible = !1 !== c.visible; b.zoomEnabled = !1 !== c.zoomEnabled; b.hasNames = "category" ===
                    l || !0 === c.categories; b.categories = c.categories || b.hasNames; b.names || (b.names = [], b.names.keys = {}); b.plotLinesAndBandsGroups = {}; b.isLog = "logarithmic" === l; b.isDatetimeAxis = "datetime" === l; b.positiveValuesOnly = b.isLog && !b.allowNegativeLog; b.isLinked = A(c.linkedTo); b.ticks = {}; b.labelEdge = []; b.minorTicks = {}; b.plotLinesAndBands = []; b.alternateBands = {}; b.len = 0; b.minRange = b.userMinRange = c.minRange || c.maxZoom; b.range = c.range; b.offset = c.offset || 0; b.stacks = {}; b.oldStacks = {}; b.stacksTouched = 0; b.max = null; b.min = null;
                b.crosshair = G(c.crosshair, H(a.options.tooltip.crosshairs)[g ? 0 : 1], !1); x = b.options.events; -1 === e(b, a.axes) && (g ? a.axes.splice(a.xAxis.length, 0, b) : a.axes.push(b), a[b.coll].push(b)); b.series = b.series || []; a.inverted && !b.isZAxis && g && void 0 === b.reversed && (b.reversed = !0); v(x, function (a, g) { C(b, g, a) }); b.lin2log = c.linearToLogConverter || b.lin2log; b.isLog && (b.val2lin = b.log2lin, b.lin2val = b.lin2log); d(this, "afterInit")
            }, setOptions: function (a) {
            this.options = z(this.defaultOptions, "yAxis" === this.coll && this.defaultYAxisOptions,
                [this.defaultTopAxisOptions, this.defaultRightAxisOptions, this.defaultBottomAxisOptions, this.defaultLeftAxisOptions][this.side], z(r[this.coll], a)); d(this, "afterSetOptions", { userOptions: a })
            }, defaultLabelFormatter: function () {
                var g = this.axis, b = this.value, c = g.chart.time, e = g.categories, l = this.dateTimeLabelFormat, h = r.lang, k = h.numericSymbols, h = h.numericSymbolMagnitude || 1E3, B = k && k.length, d, u = g.options.labels.format, g = g.isLog ? Math.abs(b) : g.tickInterval; if (u) d = m(u, this, c); else if (e) d = b; else if (l) d = c.dateFormat(l,
                    b); else if (B && 1E3 <= g) for (; B-- && void 0 === d;)c = Math.pow(h, B + 1), g >= c && 0 === 10 * b % c && null !== k[B] && 0 !== b && (d = a.numberFormat(b / c, -1) + k[B]); void 0 === d && (d = 1E4 <= Math.abs(b) ? a.numberFormat(b, -1) : a.numberFormat(b, -1, void 0, "")); return d
            }, getSeriesExtremes: function () {
                var a = this, x = a.chart; d(this, "getSeriesExtremes", null, function () {
                a.hasVisibleSeries = !1; a.dataMin = a.dataMax = a.threshold = null; a.softThreshold = !a.isXAxis; a.buildStacks && a.buildStacks(); p(a.series, function (g) {
                    if (g.visible || !x.options.chart.ignoreHiddenSeries) {
                        var c =
                            g.options, e = c.threshold, l; a.hasVisibleSeries = !0; a.positiveValuesOnly && 0 >= e && (e = null); if (a.isXAxis) c = g.xData, c.length && (g = q(c), l = E(c), u(g) || g instanceof Date || (c = b(c, u), g = q(c), l = E(c)), c.length && (a.dataMin = Math.min(G(a.dataMin, c[0], g), g), a.dataMax = Math.max(G(a.dataMax, c[0], l), l))); else if (g.getExtremes(), l = g.dataMax, g = g.dataMin, A(g) && A(l) && (a.dataMin = Math.min(G(a.dataMin, g), g), a.dataMax = Math.max(G(a.dataMax, l), l)), A(e) && (a.threshold = e), !c.softThreshold || a.positiveValuesOnly) a.softThreshold = !1
                    }
                })
                });
                d(this, "afterGetSeriesExtremes")
            }, translate: function (a, b, c, e, l, h) { var g = this.linkedParent || this, x = 1, k = 0, d = e ? g.oldTransA : g.transA; e = e ? g.oldMin : g.min; var B = g.minPixelPadding; l = (g.isOrdinal || g.isBroken || g.isLog && l) && g.lin2val; d || (d = g.transA); c && (x *= -1, k = g.len); g.reversed && (x *= -1, k -= x * (g.sector || g.len)); b ? (a = (a * x + k - B) / d + e, l && (a = g.lin2val(a))) : (l && (a = g.val2lin(a)), a = u(e) ? x * (a - e) * d + k + x * B + (u(h) ? d * h : 0) : void 0); return a }, toPixels: function (a, b) { return this.translate(a, !1, !this.horiz, null, !0) + (b ? 0 : this.pos) },
            toValue: function (a, b) { return this.translate(a - (b ? 0 : this.pos), !0, !this.horiz, null, !0) }, getPlotLinePath: function (a, b, c, e, l) {
                var g = this.chart, x = this.left, h = this.top, k, d, B = c && g.oldChartHeight || g.chartHeight, M = c && g.oldChartWidth || g.chartWidth, t; k = this.transB; var F = function (a, g, b) { if (a < g || a > b) e ? a = Math.min(Math.max(g, a), b) : t = !0; return a }; l = G(l, this.translate(a, null, null, c)); l = Math.min(Math.max(-1E5, l), 1E5); a = c = Math.round(l + k); k = d = Math.round(B - l - k); u(l) ? this.horiz ? (k = h, d = B - this.bottom, a = c = F(a, x, x + this.width)) :
                    (a = x, c = M - this.right, k = d = F(k, h, h + this.height)) : (t = !0, e = !1); return t && !e ? null : g.renderer.crispLine(["M", a, k, "L", c, d], b || 1)
            }, getLinearTickPositions: function (a, b, c) { var g, x = f(Math.floor(b / a) * a); c = f(Math.ceil(c / a) * a); var e = [], l; f(x + a) === x && (l = 20); if (this.single) return [b]; for (b = x; b <= c;) { e.push(b); b = f(b + a, l); if (b === g) break; g = b } return e }, getMinorTickInterval: function () { var a = this.options; return !0 === a.minorTicks ? G(a.minorTickInterval, "auto") : !1 === a.minorTicks ? null : a.minorTickInterval }, getMinorTickPositions: function () {
                var a =
                    this, b = a.options, c = a.tickPositions, e = a.minorTickInterval, l = [], h = a.pointRangePadding || 0, k = a.min - h, h = a.max + h, d = h - k; if (d && d / e < a.len / 3) if (a.isLog) p(this.paddedTicks, function (g, b, x) { b && l.push.apply(l, a.getLogTickPositions(e, x[b - 1], x[b], !0)) }); else if (a.isDatetimeAxis && "auto" === this.getMinorTickInterval()) l = l.concat(a.getTimeTicks(a.normalizeTimeTickInterval(e), k, h, b.startOfWeek)); else for (b = k + (c[0] - k) % e; b <= h && b !== l[0]; b += e)l.push(b); 0 !== l.length && a.trimTicks(l); return l
            }, adjustForMinRange: function () {
                var a =
                    this.options, b = this.min, c = this.max, e, l, h, k, d, B, u, t; this.isXAxis && void 0 === this.minRange && !this.isLog && (A(a.min) || A(a.max) ? this.minRange = null : (p(this.series, function (a) { B = a.xData; for (k = u = a.xIncrement ? 1 : B.length - 1; 0 < k; k--)if (d = B[k] - B[k - 1], void 0 === h || d < h) h = d }), this.minRange = Math.min(5 * h, this.dataMax - this.dataMin))); c - b < this.minRange && (l = this.dataMax - this.dataMin >= this.minRange, t = this.minRange, e = (t - c + b) / 2, e = [b - e, G(a.min, b - e)], l && (e[2] = this.isLog ? this.log2lin(this.dataMin) : this.dataMin), b = E(e), c = [b + t,
                    G(a.max, b + t)], l && (c[2] = this.isLog ? this.log2lin(this.dataMax) : this.dataMax), c = q(c), c - b < t && (e[0] = c - t, e[1] = G(a.min, c - t), b = E(e))); this.min = b; this.max = c
            }, getClosest: function () { var a; this.categories ? a = 1 : p(this.series, function (g) { var b = g.closestPointRange, c = g.visible || !g.chart.options.chart.ignoreHiddenSeries; !g.noSharedTooltip && A(b) && c && (a = A(a) ? Math.min(a, b) : b) }); return a }, nameToX: function (a) {
                var g = h(this.categories), b = g ? this.categories : this.names, c = a.options.x, l; a.series.requireSorting = !1; A(c) || (c = !1 ===
                    this.options.uniqueNames ? a.series.autoIncrement() : g ? e(a.name, b) : G(b.keys[a.name], -1)); -1 === c ? g || (l = b.length) : l = c; void 0 !== l && (this.names[l] = a.name, this.names.keys[a.name] = l); return l
            }, updateNames: function () {
                var g = this, b = this.names; 0 < b.length && (p(a.keys(b.keys), function (a) { delete b.keys[a] }), b.length = 0, this.minRange = this.userMinRange, p(this.series || [], function (a) {
                a.xIncrement = null; if (!a.points || a.isDirtyData) a.processData(), a.generatePoints(); p(a.points, function (b, c) {
                    var x; b.options && (x = g.nameToX(b),
                        void 0 !== x && x !== b.x && (b.x = x, a.xData[c] = x))
                })
                }))
            }, setAxisTranslation: function (a) {
                var g = this, b = g.max - g.min, c = g.axisPointRange || 0, e, l = 0, h = 0, k = g.linkedParent, B = !!g.categories, u = g.transA, F = g.isXAxis; if (F || B || c) e = g.getClosest(), k ? (l = k.minPointOffset, h = k.pointRangePadding) : p(g.series, function (a) { var b = B ? 1 : F ? G(a.options.pointRange, e, 0) : g.axisPointRange || 0; a = a.options.pointPlacement; c = Math.max(c, b); g.single || (l = Math.max(l, t(a) ? 0 : b / 2), h = Math.max(h, "on" === a ? 0 : b)) }), k = g.ordinalSlope && e ? g.ordinalSlope / e : 1, g.minPointOffset =
                    l *= k, g.pointRangePadding = h *= k, g.pointRange = Math.min(c, b), F && (g.closestPointRange = e); a && (g.oldTransA = u); g.translationSlope = g.transA = u = g.options.staticScale || g.len / (b + h || 1); g.transB = g.horiz ? g.left : g.bottom; g.minPixelPadding = u * l; d(this, "afterSetAxisTranslation")
            }, minFromRange: function () { return this.max - this.range }, setTickInterval: function (g) {
                var b = this, c = b.chart, e = b.options, l = b.isLog, h = b.isDatetimeAxis, B = b.isXAxis, t = b.isLinked, F = e.maxPadding, z = e.minPadding, v = e.tickInterval, m = e.tickPixelInterval, K = b.categories,
                H = u(b.threshold) ? b.threshold : null, n = b.softThreshold, y, r, w, q; h || K || t || this.getTickAmount(); w = G(b.userMin, e.min); q = G(b.userMax, e.max); t ? (b.linkedParent = c[b.coll][e.linkedTo], c = b.linkedParent.getExtremes(), b.min = G(c.min, c.dataMin), b.max = G(c.max, c.dataMax), e.type !== b.linkedParent.options.type && a.error(11, 1)) : (!n && A(H) && (b.dataMin >= H ? (y = H, z = 0) : b.dataMax <= H && (r = H, F = 0)), b.min = G(w, y, b.dataMin), b.max = G(q, r, b.dataMax)); l && (b.positiveValuesOnly && !g && 0 >= Math.min(b.min, G(b.dataMin, b.min)) && a.error(10, 1), b.min =
                    f(b.log2lin(b.min), 15), b.max = f(b.log2lin(b.max), 15)); b.range && A(b.max) && (b.userMin = b.min = w = Math.max(b.dataMin, b.minFromRange()), b.userMax = q = b.max, b.range = null); d(b, "foundExtremes"); b.beforePadding && b.beforePadding(); b.adjustForMinRange(); !(K || b.axisPointRange || b.usePercentage || t) && A(b.min) && A(b.max) && (c = b.max - b.min) && (!A(w) && z && (b.min -= c * z), !A(q) && F && (b.max += c * F)); u(e.softMin) && !u(b.userMin) && (b.min = Math.min(b.min, e.softMin)); u(e.softMax) && !u(b.userMax) && (b.max = Math.max(b.max, e.softMax)); u(e.floor) &&
                        (b.min = Math.max(b.min, e.floor)); u(e.ceiling) && (b.max = Math.min(b.max, e.ceiling)); n && A(b.dataMin) && (H = H || 0, !A(w) && b.min < H && b.dataMin >= H ? b.min = H : !A(q) && b.max > H && b.dataMax <= H && (b.max = H)); b.tickInterval = b.min === b.max || void 0 === b.min || void 0 === b.max ? 1 : t && !v && m === b.linkedParent.options.tickPixelInterval ? v = b.linkedParent.tickInterval : G(v, this.tickAmount ? (b.max - b.min) / Math.max(this.tickAmount - 1, 1) : void 0, K ? 1 : (b.max - b.min) * m / Math.max(b.len, m)); B && !g && p(b.series, function (a) {
                            a.processData(b.min !== b.oldMin || b.max !==
                                b.oldMax)
                        }); b.setAxisTranslation(!0); b.beforeSetTickPositions && b.beforeSetTickPositions(); b.postProcessTickInterval && (b.tickInterval = b.postProcessTickInterval(b.tickInterval)); b.pointRange && !v && (b.tickInterval = Math.max(b.pointRange, b.tickInterval)); g = G(e.minTickInterval, b.isDatetimeAxis && b.closestPointRange); !v && b.tickInterval < g && (b.tickInterval = g); h || l || v || (b.tickInterval = I(b.tickInterval, null, k(b.tickInterval), G(e.allowDecimals, !(.5 < b.tickInterval && 5 > b.tickInterval && 1E3 < b.max && 9999 > b.max)), !!this.tickAmount));
                this.tickAmount || (b.tickInterval = b.unsquish()); this.setTickPositions()
            }, setTickPositions: function () {
                var a = this.options, b, c = a.tickPositions; b = this.getMinorTickInterval(); var e = a.tickPositioner, l = a.startOnTick, h = a.endOnTick; this.tickmarkOffset = this.categories && "between" === a.tickmarkPlacement && 1 === this.tickInterval ? .5 : 0; this.minorTickInterval = "auto" === b && this.tickInterval ? this.tickInterval / 5 : b; this.single = this.min === this.max && A(this.min) && !this.tickAmount && (parseInt(this.min, 10) === this.min || !1 !== a.allowDecimals);
                this.tickPositions = b = c && c.slice(); !b && (b = this.isDatetimeAxis ? this.getTimeTicks(this.normalizeTimeTickInterval(this.tickInterval, a.units), this.min, this.max, a.startOfWeek, this.ordinalPositions, this.closestPointRange, !0) : this.isLog ? this.getLogTickPositions(this.tickInterval, this.min, this.max) : this.getLinearTickPositions(this.tickInterval, this.min, this.max), b.length > this.len && (b = [b[0], b.pop()], b[0] === b[1] && (b.length = 1)), this.tickPositions = b, e && (e = e.apply(this, [this.min, this.max]))) && (this.tickPositions =
                    b = e); this.paddedTicks = b.slice(0); this.trimTicks(b, l, h); this.isLinked || (this.single && 2 > b.length && (this.min -= .5, this.max += .5), c || e || this.adjustTickAmount()); d(this, "afterSetTickPositions")
            }, trimTicks: function (a, b, c) { var g = a[0], e = a[a.length - 1], l = this.minPointOffset || 0; if (!this.isLinked) { if (b && -Infinity !== g) this.min = g; else for (; this.min - l > a[0];)a.shift(); if (c) this.max = e; else for (; this.max + l < a[a.length - 1];)a.pop(); 0 === a.length && A(g) && !this.options.tickPositions && a.push((e + g) / 2) } }, alignToOthers: function () {
                var a =
                    {}, b, c = this.options; !1 === this.chart.options.chart.alignTicks || !1 === c.alignTicks || !1 === c.startOnTick || !1 === c.endOnTick || this.isLog || p(this.chart[this.coll], function (g) { var c = g.options, c = [g.horiz ? c.left : c.top, c.width, c.height, c.pane].join(); g.series.length && (a[c] ? b = !0 : a[c] = 1) }); return b
            }, getTickAmount: function () {
                var a = this.options, b = a.tickAmount, c = a.tickPixelInterval; !A(a.tickInterval) && this.len < c && !this.isRadial && !this.isLog && a.startOnTick && a.endOnTick && (b = 2); !b && this.alignToOthers() && (b = Math.ceil(this.len /
                    c) + 1); 4 > b && (this.finalTickAmt = b, b = 5); this.tickAmount = b
            }, adjustTickAmount: function () {
                var a = this.tickInterval, b = this.tickPositions, c = this.tickAmount, e = this.finalTickAmt, l = b && b.length, h = G(this.threshold, this.softThreshold ? 0 : null); if (this.hasData()) {
                    if (l < c) { for (; b.length < c;)b.length % 2 || this.min === h ? b.push(f(b[b.length - 1] + a)) : b.unshift(f(b[0] - a)); this.transA *= (l - 1) / (c - 1); this.min = b[0]; this.max = b[b.length - 1] } else l > c && (this.tickInterval *= 2, this.setTickPositions()); if (A(e)) {
                        for (a = c = b.length; a--;)(3 === e &&
                            1 === a % 2 || 2 >= e && 0 < a && a < c - 1) && b.splice(a, 1); this.finalTickAmt = void 0
                    }
                }
            }, setScale: function () {
                var a, b; this.oldMin = this.min; this.oldMax = this.max; this.oldAxisLength = this.len; this.setAxisSize(); b = this.len !== this.oldAxisLength; p(this.series, function (b) { if (b.isDirtyData || b.isDirty || b.xAxis.isDirty) a = !0 }); b || a || this.isLinked || this.forceRedraw || this.userMin !== this.oldUserMin || this.userMax !== this.oldUserMax || this.alignToOthers() ? (this.resetStacks && this.resetStacks(), this.forceRedraw = !1, this.getSeriesExtremes(),
                    this.setTickInterval(), this.oldUserMin = this.userMin, this.oldUserMax = this.userMax, this.isDirty || (this.isDirty = b || this.min !== this.oldMin || this.max !== this.oldMax)) : this.cleanStacks && this.cleanStacks(); d(this, "afterSetScale")
            }, setExtremes: function (a, b, e, l, h) { var g = this, k = g.chart; e = G(e, !0); p(g.series, function (a) { delete a.kdTree }); h = c(h, { min: a, max: b }); d(g, "setExtremes", h, function () { g.userMin = a; g.userMax = b; g.eventArgs = h; e && k.redraw(l) }) }, zoom: function (a, b) {
                var g = this.dataMin, c = this.dataMax, e = this.options,
                l = Math.min(g, G(e.min, g)), e = Math.max(c, G(e.max, c)); if (a !== this.min || b !== this.max) this.allowZoomOutside || (A(g) && (a < l && (a = l), a > e && (a = e)), A(c) && (b < l && (b = l), b > e && (b = e))), this.displayBtn = void 0 !== a || void 0 !== b, this.setExtremes(a, b, !1, void 0, { trigger: "zoom" }); return !0
            }, setAxisSize: function () {
                var b = this.chart, c = this.options, e = c.offsets || [0, 0, 0, 0], l = this.horiz, h = this.width = Math.round(a.relativeLength(G(c.width, b.plotWidth - e[3] + e[1]), b.plotWidth)), k = this.height = Math.round(a.relativeLength(G(c.height, b.plotHeight -
                    e[0] + e[2]), b.plotHeight)), d = this.top = Math.round(a.relativeLength(G(c.top, b.plotTop + e[0]), b.plotHeight, b.plotTop)), c = this.left = Math.round(a.relativeLength(G(c.left, b.plotLeft + e[3]), b.plotWidth, b.plotLeft)); this.bottom = b.chartHeight - k - d; this.right = b.chartWidth - h - c; this.len = Math.max(l ? h : k, 0); this.pos = l ? c : d
            }, getExtremes: function () { var a = this.isLog; return { min: a ? f(this.lin2log(this.min)) : this.min, max: a ? f(this.lin2log(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax, userMin: this.userMin, userMax: this.userMax } },
            getThreshold: function (a) { var b = this.isLog, g = b ? this.lin2log(this.min) : this.min, b = b ? this.lin2log(this.max) : this.max; null === a || -Infinity === a ? a = g : Infinity === a ? a = b : g > a ? a = g : b < a && (a = b); return this.translate(a, 0, 1, 0, 1) }, autoLabelAlign: function (a) { a = (G(a, 0) - 90 * this.side + 720) % 360; return 15 < a && 165 > a ? "right" : 195 < a && 345 > a ? "left" : "center" }, tickSize: function (a) { var b = this.options, g = b[a + "Length"], c = G(b[a + "Width"], "tick" === a && this.isXAxis ? 1 : 0); if (c && g) return "inside" === b[a + "Position"] && (g = -g), [g, c] }, labelMetrics: function () {
                var a =
                    this.tickPositions && this.tickPositions[0] || 0; return this.chart.renderer.fontMetrics(this.options.labels.style && this.options.labels.style.fontSize, this.ticks[a] && this.ticks[a].label)
            }, unsquish: function () {
                var a = this.options.labels, b = this.horiz, c = this.tickInterval, e = c, l = this.len / (((this.categories ? 1 : 0) + this.max - this.min) / c), h, k = a.rotation, d = this.labelMetrics(), B, u = Number.MAX_VALUE, t, F = function (a) { a /= l || 1; a = 1 < a ? Math.ceil(a) : 1; return f(a * c) }; b ? (t = !a.staggerLines && !a.step && (A(k) ? [k] : l < G(a.autoRotationLimit,
                    80) && a.autoRotation)) && p(t, function (a) { var b; if (a === k || a && -90 <= a && 90 >= a) B = F(Math.abs(d.h / Math.sin(w * a))), b = B + Math.abs(a / 360), b < u && (u = b, h = a, e = B) }) : a.step || (e = F(d.h)); this.autoRotation = t; this.labelRotation = G(h, k); return e
            }, getSlotWidth: function () {
                var a = this.chart, b = this.horiz, c = this.options.labels, e = Math.max(this.tickPositions.length - (this.categories ? 0 : 1), 1), l = a.margin[3]; return b && 2 > (c.step || 0) && !c.rotation && (this.staggerLines || 1) * this.len / e || !b && (c.style && parseInt(c.style.width, 10) || l && l - a.spacing[3] ||
                    .33 * a.chartWidth)
            }, renderUnsquish: function () {
                var a = this.chart, b = a.renderer, c = this.tickPositions, e = this.ticks, l = this.options.labels, h = l && l.style || {}, k = this.horiz, d = this.getSlotWidth(), B = Math.max(1, Math.round(d - 2 * (l.padding || 5))), u = {}, F = this.labelMetrics(), z = l.style && l.style.textOverflow, v, m, K = 0, H; t(l.rotation) || (u.rotation = l.rotation || 0); p(c, function (a) { (a = e[a]) && a.label && a.label.textPxLength > K && (K = a.label.textPxLength) }); this.maxLabelLength = K; if (this.autoRotation) K > B && K > F.h ? u.rotation = this.labelRotation :
                    this.labelRotation = 0; else if (d && (v = B, !z)) for (m = "clip", B = c.length; !k && B--;)if (H = c[B], H = e[H].label) H.styles && "ellipsis" === H.styles.textOverflow ? H.css({ textOverflow: "clip" }) : H.textPxLength > d && H.css({ width: d + "px" }), H.getBBox().height > this.len / c.length - (F.h - F.f) && (H.specificTextOverflow = "ellipsis"); u.rotation && (v = K > .5 * a.chartHeight ? .33 * a.chartHeight : a.chartHeight, z || (m = "ellipsis")); if (this.labelAlign = l.align || this.autoLabelAlign(this.labelRotation)) u.align = this.labelAlign; p(c, function (a) {
                        var b = (a = e[a]) &&
                            a.label, g = h.width, c = {}; b && (b.attr(u), v && !g && "nowrap" !== h.whiteSpace && (v < b.textPxLength || "SPAN" === b.element.tagName) ? (c.width = v, z || (c.textOverflow = b.specificTextOverflow || m), b.css(c)) : b.styles && b.styles.width && !c.width && !g && b.css({ width: null }), delete b.specificTextOverflow, a.rotation = u.rotation)
                    }); this.tickRotCorr = b.rotCorr(F.b, this.labelRotation || 0, 0 !== this.side)
            }, hasData: function () { return this.hasVisibleSeries || A(this.min) && A(this.max) && this.tickPositions && 0 < this.tickPositions.length }, addTitle: function (a) {
                var b =
                    this.chart.renderer, g = this.horiz, c = this.opposite, e = this.options.title, l; this.axisTitle || ((l = e.textAlign) || (l = (g ? { low: "left", middle: "center", high: "right" } : { low: c ? "right" : "left", middle: "center", high: c ? "left" : "right" })[e.align]), this.axisTitle = b.text(e.text, 0, 0, e.useHTML).attr({ zIndex: 7, rotation: e.rotation || 0, align: l }).addClass("highcharts-axis-title").css(z(e.style)).add(this.axisGroup), this.axisTitle.isNew = !0); e.style.width || this.isRadial || this.axisTitle.css({ width: this.len }); this.axisTitle[a ? "show" :
                        "hide"](!0)
            }, generateTick: function (a) { var b = this.ticks; b[a] ? b[a].addLabel() : b[a] = new F(this, a) }, getOffset: function () {
                var a = this, b = a.chart, c = b.renderer, e = a.options, l = a.tickPositions, h = a.ticks, k = a.horiz, d = a.side, B = b.inverted && !a.isZAxis ? [1, 0, 3, 2][d] : d, u, t, F = 0, z, m = 0, K = e.title, H = e.labels, f = 0, I = b.axisOffset, b = b.clipOffset, n = [-1, 1, 1, -1][d], y = e.className, w = a.axisParent, r = this.tickSize("tick"); u = a.hasData(); a.showAxis = t = u || G(e.showEmpty, !0); a.staggerLines = a.horiz && H.staggerLines; a.axisGroup || (a.gridGroup =
                    c.g("grid").attr({ zIndex: e.gridZIndex || 1 }).addClass("highcharts-" + this.coll.toLowerCase() + "-grid " + (y || "")).add(w), a.axisGroup = c.g("axis").attr({ zIndex: e.zIndex || 2 }).addClass("highcharts-" + this.coll.toLowerCase() + " " + (y || "")).add(w), a.labelGroup = c.g("axis-labels").attr({ zIndex: H.zIndex || 7 }).addClass("highcharts-" + a.coll.toLowerCase() + "-labels " + (y || "")).add(w)); u || a.isLinked ? (p(l, function (b, g) { a.generateTick(b, g) }), a.renderUnsquish(), a.reserveSpaceDefault = 0 === d || 2 === d || { 1: "left", 3: "right" }[d] === a.labelAlign,
                        G(H.reserveSpace, "center" === a.labelAlign ? !0 : null, a.reserveSpaceDefault) && p(l, function (a) { f = Math.max(h[a].getLabelSize(), f) }), a.staggerLines && (f *= a.staggerLines), a.labelOffset = f * (a.opposite ? -1 : 1)) : v(h, function (a, b) { a.destroy(); delete h[b] }); K && K.text && !1 !== K.enabled && (a.addTitle(t), t && !1 !== K.reserveSpace && (a.titleOffset = F = a.axisTitle.getBBox()[k ? "height" : "width"], z = K.offset, m = A(z) ? 0 : G(K.margin, k ? 5 : 10))); a.renderLine(); a.offset = n * G(e.offset, I[d]); a.tickRotCorr = a.tickRotCorr || { x: 0, y: 0 }; c = 0 === d ? -a.labelMetrics().h :
                            2 === d ? a.tickRotCorr.y : 0; m = Math.abs(f) + m; f && (m = m - c + n * (k ? G(H.y, a.tickRotCorr.y + 8 * n) : H.x)); a.axisTitleMargin = G(z, m); I[d] = Math.max(I[d], a.axisTitleMargin + F + n * a.offset, m, u && l.length && r ? r[0] + n * a.offset : 0); e = e.offset ? 0 : 2 * Math.floor(a.axisLine.strokeWidth() / 2); b[B] = Math.max(b[B], e)
            }, getLinePath: function (a) {
                var b = this.chart, g = this.opposite, c = this.offset, e = this.horiz, l = this.left + (g ? this.width : 0) + c, c = b.chartHeight - this.bottom - (g ? this.height : 0) + c; g && (a *= -1); return b.renderer.crispLine(["M", e ? this.left : l, e ? c :
                    this.top, "L", e ? b.chartWidth - this.right : l, e ? c : b.chartHeight - this.bottom], a)
            }, renderLine: function () { this.axisLine || (this.axisLine = this.chart.renderer.path().addClass("highcharts-axis-line").add(this.axisGroup), this.axisLine.attr({ stroke: this.options.lineColor, "stroke-width": this.options.lineWidth, zIndex: 7 })) }, getTitlePosition: function () {
                var a = this.horiz, b = this.left, c = this.top, e = this.len, l = this.options.title, h = a ? b : c, k = this.opposite, d = this.offset, B = l.x || 0, u = l.y || 0, t = this.axisTitle, F = this.chart.renderer.fontMetrics(l.style &&
                    l.style.fontSize, t), t = Math.max(t.getBBox(null, 0).height - F.h - 1, 0), e = { low: h + (a ? 0 : e), middle: h + e / 2, high: h + (a ? e : 0) }[l.align], b = (a ? c + this.height : b) + (a ? 1 : -1) * (k ? -1 : 1) * this.axisTitleMargin + [-t, t, F.f, -t][this.side]; return { x: a ? e + B : b + (k ? this.width : 0) + d + B, y: a ? b + u - (k ? this.height : 0) + d : e + u }
            }, renderMinorTick: function (a) { var b = this.chart.hasRendered && u(this.oldMin), g = this.minorTicks; g[a] || (g[a] = new F(this, a, "minor")); b && g[a].isNew && g[a].render(null, !0); g[a].render(null, !1, 1) }, renderTick: function (a, b) {
                var g = this.isLinked,
                c = this.ticks, e = this.chart.hasRendered && u(this.oldMin); if (!g || a >= this.min && a <= this.max) c[a] || (c[a] = new F(this, a)), e && c[a].isNew && c[a].render(b, !0, .1), c[a].render(b)
            }, render: function () {
                var b = this, c = b.chart, e = b.options, l = b.isLog, h = b.isLinked, k = b.tickPositions, B = b.axisTitle, t = b.ticks, z = b.minorTicks, m = b.alternateBands, H = e.stackLabels, G = e.alternateGridColor, f = b.tickmarkOffset, I = b.axisLine, n = b.showAxis, y = D(c.renderer.globalAnimation), w, r; b.labelEdge.length = 0; b.overlap = !1; p([t, z, m], function (a) {
                    v(a, function (a) {
                    a.isActive =
                        !1
                    })
                }); if (b.hasData() || h) b.minorTickInterval && !b.categories && p(b.getMinorTickPositions(), function (a) { b.renderMinorTick(a) }), k.length && (p(k, function (a, c) { b.renderTick(a, c) }), f && (0 === b.min || b.single) && (t[-1] || (t[-1] = new F(b, -1, null, !0)), t[-1].render(-1))), G && p(k, function (g, e) { r = void 0 !== k[e + 1] ? k[e + 1] + f : b.max - f; 0 === e % 2 && g < b.max && r <= b.max + (c.polar ? -f : f) && (m[g] || (m[g] = new a.PlotLineOrBand(b)), w = g + f, m[g].options = { from: l ? b.lin2log(w) : w, to: l ? b.lin2log(r) : r, color: G }, m[g].render(), m[g].isActive = !0) }), b._addedPlotLB ||
                    (p((e.plotLines || []).concat(e.plotBands || []), function (a) { b.addPlotBandOrLine(a) }), b._addedPlotLB = !0); p([t, z, m], function (a) { var b, g = [], e = y.duration; v(a, function (a, b) { a.isActive || (a.render(b, !1, 0), a.isActive = !1, g.push(b)) }); K(function () { for (b = g.length; b--;)a[g[b]] && !a[g[b]].isActive && (a[g[b]].destroy(), delete a[g[b]]) }, a !== m && c.hasRendered && e ? e : 0) }); I && (I[I.isPlaced ? "animate" : "attr"]({ d: this.getLinePath(I.strokeWidth()) }), I.isPlaced = !0, I[n ? "show" : "hide"](!0)); B && n && (e = b.getTitlePosition(), u(e.y) ? (B[B.isNew ?
                        "attr" : "animate"](e), B.isNew = !1) : (B.attr("y", -9999), B.isNew = !0)); H && H.enabled && b.renderStackTotals(); b.isDirty = !1; d(this, "afterRender")
            }, redraw: function () { this.visible && (this.render(), p(this.plotLinesAndBands, function (a) { a.render() })); p(this.series, function (a) { a.isDirty = !0 }) }, keepProps: "extKey hcEvents names series userMax userMin".split(" "), destroy: function (a) {
                var b = this, c = b.stacks, g = b.plotLinesAndBands, h; d(this, "destroy", { keepEvents: a }); a || l(b); v(c, function (a, b) { y(a); c[b] = null }); p([b.ticks, b.minorTicks,
                b.alternateBands], function (a) { y(a) }); if (g) for (a = g.length; a--;)g[a].destroy(); p("stackTotalGroup axisLine axisTitle axisGroup gridGroup labelGroup cross".split(" "), function (a) { b[a] && (b[a] = b[a].destroy()) }); for (h in b.plotLinesAndBandsGroups) b.plotLinesAndBandsGroups[h] = b.plotLinesAndBandsGroups[h].destroy(); v(b, function (a, c) { -1 === e(c, b.keepProps) && delete b[c] })
            }, drawCrosshair: function (a, b) {
                var c, g = this.crosshair, e = G(g.snap, !0), l, h = this.cross; d(this, "drawCrosshair", { e: a, point: b }); a || (a = this.cross &&
                    this.cross.e); if (this.crosshair && !1 !== (A(b) || !e)) {
                        e ? A(b) && (l = G(b.crosshairPos, this.isXAxis ? b.plotX : this.len - b.plotY)) : l = a && (this.horiz ? a.chartX - this.pos : this.len - a.chartY + this.pos); A(l) && (c = this.getPlotLinePath(b && (this.isXAxis ? b.x : G(b.stackY, b.y)), null, null, null, l) || null); if (!A(c)) { this.hideCrosshair(); return } e = this.categories && !this.isRadial; h || (this.cross = h = this.chart.renderer.path().addClass("highcharts-crosshair highcharts-crosshair-" + (e ? "category " : "thin ") + g.className).attr({
                            zIndex: G(g.zIndex,
                                2)
                        }).add(), h.attr({ stroke: g.color || (e ? n("#ccd6eb").setOpacity(.25).get() : "#cccccc"), "stroke-width": G(g.width, 1) }).css({ "pointer-events": "none" }), g.dashStyle && h.attr({ dashstyle: g.dashStyle })); h.show().attr({ d: c }); e && !g.width && h.attr({ "stroke-width": this.transA }); this.cross.e = a
                    } else this.hideCrosshair(); d(this, "afterDrawCrosshair", { e: a, point: b })
            }, hideCrosshair: function () { this.cross && this.cross.hide() }
        }); return a.Axis = B
    }(L); (function (a) {
        var C = a.Axis, D = a.getMagnitude, E = a.normalizeTickInterval, q = a.timeUnits;
        C.prototype.getTimeTicks = function () { return this.chart.time.getTimeTicks.apply(this.chart.time, arguments) }; C.prototype.normalizeTimeTickInterval = function (a, f) {
            var n = f || [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1, 2]], ["week", [1, 2]], ["month", [1, 2, 3, 4, 6]], ["year", null]]; f = n[n.length - 1]; var A = q[f[0]], w = f[1], y; for (y = 0; y < n.length && !(f = n[y], A = q[f[0]], w = f[1], n[y + 1] && a <= (A * w[w.length - 1] + q[n[y + 1][0]]) / 2); y++); A ===
                q.year && a < 5 * A && (w = [1, 2, 5]); a = E(a / A, w, "year" === f[0] ? Math.max(D(a / A), 1) : 1); return { unitRange: A, count: a, unitName: f[0] }
        }
    })(L); (function (a) {
        var C = a.Axis, D = a.getMagnitude, E = a.map, q = a.normalizeTickInterval, n = a.pick; C.prototype.getLogTickPositions = function (a, r, A, w) {
            var f = this.options, p = this.len, c = []; w || (this._minorAutoInterval = null); if (.5 <= a) a = Math.round(a), c = this.getLinearTickPositions(a, r, A); else if (.08 <= a) for (var p = Math.floor(r), d, m, k, b, e, f = .3 < a ? [1, 2, 4] : .15 < a ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; p < A + 1 && !e; p++)for (m =
                f.length, d = 0; d < m && !e; d++)k = this.log2lin(this.lin2log(p) * f[d]), k > r && (!w || b <= A) && void 0 !== b && c.push(b), b > A && (e = !0), b = k; else r = this.lin2log(r), A = this.lin2log(A), a = w ? this.getMinorTickInterval() : f.tickInterval, a = n("auto" === a ? null : a, this._minorAutoInterval, f.tickPixelInterval / (w ? 5 : 1) * (A - r) / ((w ? p / this.tickPositions.length : p) || 1)), a = q(a, null, D(a)), c = E(this.getLinearTickPositions(a, r, A), this.log2lin), w || (this._minorAutoInterval = a / 5); w || (this.tickInterval = a); return c
        }; C.prototype.log2lin = function (a) {
            return Math.log(a) /
                Math.LN10
        }; C.prototype.lin2log = function (a) { return Math.pow(10, a) }
    })(L); (function (a, C) {
        var D = a.arrayMax, E = a.arrayMin, q = a.defined, n = a.destroyObjectProperties, f = a.each, r = a.erase, A = a.merge, w = a.pick; a.PlotLineOrBand = function (a, p) { this.axis = a; p && (this.options = p, this.id = p.id) }; a.PlotLineOrBand.prototype = {
            render: function () {
                var f = this, p = f.axis, c = p.horiz, d = f.options, m = d.label, k = f.label, b = d.to, e = d.from, h = d.value, u = q(e) && q(b), t = q(h), z = f.svgElem, I = !z, v = [], G = d.color, l = w(d.zIndex, 0), H = d.events, v = {
                    "class": "highcharts-plot-" +
                        (u ? "band " : "line ") + (d.className || "")
                }, K = {}, F = p.chart.renderer, B = u ? "bands" : "lines"; p.isLog && (e = p.log2lin(e), b = p.log2lin(b), h = p.log2lin(h)); t ? (v.stroke = G, v["stroke-width"] = d.width, d.dashStyle && (v.dashstyle = d.dashStyle)) : u && (G && (v.fill = G), d.borderWidth && (v.stroke = d.borderColor, v["stroke-width"] = d.borderWidth)); K.zIndex = l; B += "-" + l; (G = p.plotLinesAndBandsGroups[B]) || (p.plotLinesAndBandsGroups[B] = G = F.g("plot-" + B).attr(K).add()); I && (f.svgElem = z = F.path().attr(v).add(G)); if (t) v = p.getPlotLinePath(h, z.strokeWidth());
                else if (u) v = p.getPlotBandPath(e, b, d); else return; I && v && v.length ? (z.attr({ d: v }), H && a.objectEach(H, function (a, b) { z.on(b, function (a) { H[b].apply(f, [a]) }) })) : z && (v ? (z.show(), z.animate({ d: v })) : (z.hide(), k && (f.label = k = k.destroy()))); m && q(m.text) && v && v.length && 0 < p.width && 0 < p.height && !v.isFlat ? (m = A({ align: c && u && "center", x: c ? !u && 4 : 10, verticalAlign: !c && u && "middle", y: c ? u ? 16 : 10 : u ? 6 : -4, rotation: c && !u && 90 }, m), this.renderLabel(m, v, u, l)) : k && k.hide(); return f
            }, renderLabel: function (a, p, c, d) {
                var m = this.label, k = this.axis.chart.renderer;
                m || (m = { align: a.textAlign || a.align, rotation: a.rotation, "class": "highcharts-plot-" + (c ? "band" : "line") + "-label " + (a.className || "") }, m.zIndex = d, this.label = m = k.text(a.text, 0, 0, a.useHTML).attr(m).add(), m.css(a.style)); d = p.xBounds || [p[1], p[4], c ? p[6] : p[1]]; p = p.yBounds || [p[2], p[5], c ? p[7] : p[2]]; c = E(d); k = E(p); m.align(a, !1, { x: c, y: k, width: D(d) - c, height: D(p) - k }); m.show()
            }, destroy: function () { r(this.axis.plotLinesAndBands, this); delete this.axis; n(this) }
        }; a.extend(C.prototype, {
            getPlotBandPath: function (a, p) {
                var c = this.getPlotLinePath(p,
                    null, null, !0), d = this.getPlotLinePath(a, null, null, !0), m = [], k = this.horiz, b = 1, e; a = a < this.min && p < this.min || a > this.max && p > this.max; if (d && c) for (a && (e = d.toString() === c.toString(), b = 0), a = 0; a < d.length; a += 6)k && c[a + 1] === d[a + 1] ? (c[a + 1] += b, c[a + 4] += b) : k || c[a + 2] !== d[a + 2] || (c[a + 2] += b, c[a + 5] += b), m.push("M", d[a + 1], d[a + 2], "L", d[a + 4], d[a + 5], c[a + 4], c[a + 5], c[a + 1], c[a + 2], "z"), m.isFlat = e; return m
            }, addPlotBand: function (a) { return this.addPlotBandOrLine(a, "plotBands") }, addPlotLine: function (a) {
                return this.addPlotBandOrLine(a,
                    "plotLines")
            }, addPlotBandOrLine: function (f, p) { var c = (new a.PlotLineOrBand(this, f)).render(), d = this.userOptions; c && (p && (d[p] = d[p] || [], d[p].push(f)), this.plotLinesAndBands.push(c)); return c }, removePlotBandOrLine: function (a) { for (var p = this.plotLinesAndBands, c = this.options, d = this.userOptions, m = p.length; m--;)p[m].id === a && p[m].destroy(); f([c.plotLines || [], d.plotLines || [], c.plotBands || [], d.plotBands || []], function (c) { for (m = c.length; m--;)c[m].id === a && r(c, c[m]) }) }, removePlotBand: function (a) { this.removePlotBandOrLine(a) },
            removePlotLine: function (a) { this.removePlotBandOrLine(a) }
        })
    })(L, ea); (function (a) {
        var C = a.doc, D = a.each, E = a.extend, q = a.format, n = a.isNumber, f = a.map, r = a.merge, A = a.pick, w = a.splat, y = a.syncTimeout, p = a.timeUnits; a.Tooltip = function () { this.init.apply(this, arguments) }; a.Tooltip.prototype = {
            init: function (a, d) { this.chart = a; this.options = d; this.crosshairs = []; this.now = { x: 0, y: 0 }; this.isHidden = !0; this.split = d.split && !a.inverted; this.shared = d.shared || this.split; this.outside = d.outside && !this.split }, cleanSplit: function (a) {
                D(this.chart.series,
                    function (c) { var d = c && c.tt; d && (!d.isActive || a ? c.tt = d.destroy() : d.isActive = !1) })
            }, getLabel: function () {
                var c = this.chart.renderer, d = this.options, m; this.label || (this.outside && (this.container = m = a.doc.createElement("div"), m.className = "highcharts-tooltip-container", a.css(m, { position: "absolute", top: "1px", pointerEvents: "none" }), a.doc.body.appendChild(m), this.renderer = c = new a.Renderer(m, 0, 0)), this.split ? this.label = c.g("tooltip") : (this.label = c.label("", 0, 0, d.shape || "callout", null, null, d.useHTML, null, "tooltip").attr({
                    padding: d.padding,
                    r: d.borderRadius
                }), this.label.attr({ fill: d.backgroundColor, "stroke-width": d.borderWidth }).css(d.style).shadow(d.shadow)), this.outside && (this.label.attr({ x: this.distance, y: this.distance }), this.label.xSetter = function (a) { m.style.left = a + "px" }, this.label.ySetter = function (a) { m.style.top = a + "px" }), this.label.attr({ zIndex: 8 }).add()); return this.label
            }, update: function (a) { this.destroy(); r(!0, this.chart.options.tooltip.userOptions, a); this.init(this.chart, r(!0, this.options, a)) }, destroy: function () {
            this.label &&
                (this.label = this.label.destroy()); this.split && this.tt && (this.cleanSplit(this.chart, !0), this.tt = this.tt.destroy()); this.renderer && (this.renderer = this.renderer.destroy(), a.discardElement(this.container)); a.clearTimeout(this.hideTimer); a.clearTimeout(this.tooltipTimeout)
            }, move: function (c, d, m, k) {
                var b = this, e = b.now, h = !1 !== b.options.animation && !b.isHidden && (1 < Math.abs(c - e.x) || 1 < Math.abs(d - e.y)), u = b.followPointer || 1 < b.len; E(e, {
                    x: h ? (2 * e.x + c) / 3 : c, y: h ? (e.y + d) / 2 : d, anchorX: u ? void 0 : h ? (2 * e.anchorX + m) / 3 : m, anchorY: u ?
                        void 0 : h ? (e.anchorY + k) / 2 : k
                }); b.getLabel().attr(e); h && (a.clearTimeout(this.tooltipTimeout), this.tooltipTimeout = setTimeout(function () { b && b.move(c, d, m, k) }, 32))
            }, hide: function (c) { var d = this; a.clearTimeout(this.hideTimer); c = A(c, this.options.hideDelay, 500); this.isHidden || (this.hideTimer = y(function () { d.getLabel()[c ? "fadeOut" : "hide"](); d.isHidden = !0 }, c)) }, getAnchor: function (a, d) {
                var c, k = this.chart, b = k.inverted, e = k.plotTop, h = k.plotLeft, u = 0, t = 0, z, p; a = w(a); c = a[0].tooltipPos; this.followPointer && d && (void 0 ===
                    d.chartX && (d = k.pointer.normalize(d)), c = [d.chartX - k.plotLeft, d.chartY - e]); c || (D(a, function (a) { z = a.series.yAxis; p = a.series.xAxis; u += a.plotX + (!b && p ? p.left - h : 0); t += (a.plotLow ? (a.plotLow + a.plotHigh) / 2 : a.plotY) + (!b && z ? z.top - e : 0) }), u /= a.length, t /= a.length, c = [b ? k.plotWidth - t : u, this.shared && !b && 1 < a.length && d ? d.chartY - e : b ? k.plotHeight - u : t]); return f(c, Math.round)
            }, getPosition: function (a, d, m) {
                var c = this.chart, b = this.distance, e = {}, h = c.inverted && m.h || 0, u, t = this.outside, z = t ? C.documentElement.clientWidth - 2 * b : c.chartWidth,
                p = t ? Math.max(C.body.scrollHeight, C.documentElement.scrollHeight, C.body.offsetHeight, C.documentElement.offsetHeight, C.documentElement.clientHeight) : c.chartHeight, v = c.pointer.chartPosition, G = ["y", p, d, (t ? v.top - b : 0) + m.plotY + c.plotTop, t ? 0 : c.plotTop, t ? p : c.plotTop + c.plotHeight], l = ["x", z, a, (t ? v.left - b : 0) + m.plotX + c.plotLeft, t ? 0 : c.plotLeft, t ? z : c.plotLeft + c.plotWidth], H = !this.followPointer && A(m.ttBelow, !c.inverted === !!m.negative), K = function (a, c, g, l, k, d) {
                    var B = g < l - b, t = l + b + g < c, u = l - b - g; l += b; if (H && t) e[a] = l; else if (!H &&
                        B) e[a] = u; else if (B) e[a] = Math.min(d - g, 0 > u - h ? u : u - h); else if (t) e[a] = Math.max(k, l + h + g > c ? l : l + h); else return !1
                }, F = function (a, c, g, l) { var h; l < b || l > c - b ? h = !1 : e[a] = l < g / 2 ? 1 : l > c - g / 2 ? c - g - 2 : l - g / 2; return h }, B = function (a) { var b = G; G = l; l = b; u = a }, g = function () { !1 !== K.apply(0, G) ? !1 !== F.apply(0, l) || u || (B(!0), g()) : u ? e.x = e.y = 0 : (B(!0), g()) }; (c.inverted || 1 < this.len) && B(); g(); return e
            }, defaultFormatter: function (a) {
                var c = this.points || w(this), m; m = [a.tooltipFooterHeaderFormatter(c[0])]; m = m.concat(a.bodyFormatter(c)); m.push(a.tooltipFooterHeaderFormatter(c[0],
                    !0)); return m
            }, refresh: function (c, d) {
                var m, k = this.options, b, e = c, h, u = {}, t = []; m = k.formatter || this.defaultFormatter; var u = this.shared, z; k.enabled && (a.clearTimeout(this.hideTimer), this.followPointer = w(e)[0].series.tooltipOptions.followPointer, h = this.getAnchor(e, d), d = h[0], b = h[1], !u || e.series && e.series.noSharedTooltip ? u = e.getLabelConfig() : (D(e, function (a) { a.setState("hover"); t.push(a.getLabelConfig()) }), u = { x: e[0].category, y: e[0].y }, u.points = t, e = e[0]), this.len = t.length, u = m.call(u, this), z = e.series, this.distance =
                    A(z.tooltipOptions.distance, 16), !1 === u ? this.hide() : (m = this.getLabel(), this.isHidden && m.attr({ opacity: 1 }).show(), this.split ? this.renderSplit(u, w(c)) : (k.style.width || m.css({ width: this.chart.spacingBox.width }), m.attr({ text: u && u.join ? u.join("") : u }), m.removeClass(/highcharts-color-[\d]+/g).addClass("highcharts-color-" + A(e.colorIndex, z.colorIndex)), m.attr({ stroke: k.borderColor || e.color || z.color || "#666666" }), this.updatePosition({ plotX: d, plotY: b, negative: e.negative, ttBelow: e.ttBelow, h: h[2] || 0 })), this.isHidden =
                        !1))
            }, renderSplit: function (c, d) {
                var m = this, k = [], b = this.chart, e = b.renderer, h = !0, u = this.options, t = 0, z = this.getLabel(); a.isString(c) && (c = [!1, c]); D(c.slice(0, d.length + 1), function (a, c) {
                    if (!1 !== a) {
                        c = d[c - 1] || { isHeader: !0, plotX: d[0].plotX }; var v = c.series || m, l = v.tt, H = c.series || {}, K = "highcharts-color-" + A(c.colorIndex, H.colorIndex, "none"); l || (v.tt = l = e.label(null, null, null, "callout", null, null, u.useHTML).addClass("highcharts-tooltip-box " + K).attr({
                            padding: u.padding, r: u.borderRadius, fill: u.backgroundColor, stroke: u.borderColor ||
                                c.color || H.color || "#333333", "stroke-width": u.borderWidth
                        }).add(z)); l.isActive = !0; l.attr({ text: a }); l.css(u.style).shadow(u.shadow); a = l.getBBox(); H = a.width + l.strokeWidth(); c.isHeader ? (t = a.height, H = Math.max(0, Math.min(c.plotX + b.plotLeft - H / 2, b.chartWidth - H))) : H = c.plotX + b.plotLeft - A(u.distance, 16) - H; 0 > H && (h = !1); a = (c.series && c.series.yAxis && c.series.yAxis.pos) + (c.plotY || 0); a -= b.plotTop; k.push({ target: c.isHeader ? b.plotHeight + t : a, rank: c.isHeader ? 1 : 0, size: v.tt.getBBox().height + 1, point: c, x: H, tt: l })
                    }
                }); this.cleanSplit();
                a.distribute(k, b.plotHeight + t); D(k, function (a) { var c = a.point, e = c.series; a.tt.attr({ visibility: void 0 === a.pos ? "hidden" : "inherit", x: h || c.isHeader ? a.x : c.plotX + b.plotLeft + A(u.distance, 16), y: a.pos + b.plotTop, anchorX: c.isHeader ? c.plotX + b.plotLeft : c.plotX + e.xAxis.pos, anchorY: c.isHeader ? a.pos + b.plotTop - 15 : c.plotY + e.yAxis.pos }) })
            }, updatePosition: function (a) {
                var c = this.chart, m = this.getLabel(), k = (this.options.positioner || this.getPosition).call(this, m.width, m.height, a), b = a.plotX + c.plotLeft; a = a.plotY + c.plotTop;
                var e; this.outside && (e = (this.options.borderWidth || 0) + 2 * this.distance, this.renderer.setSize(m.width + e, m.height + e, !1), b += c.pointer.chartPosition.left - k.x, a += c.pointer.chartPosition.top - k.y); this.move(Math.round(k.x), Math.round(k.y || 0), b, a)
            }, getDateFormat: function (a, d, m, k) {
                var b = this.chart.time, c = b.dateFormat("%m-%d %H:%M:%S.%L", d), h, u, t = { millisecond: 15, second: 12, minute: 9, hour: 6, day: 3 }, z = "millisecond"; for (u in p) {
                    if (a === p.week && +b.dateFormat("%w", d) === m && "00:00:00.000" === c.substr(6)) { u = "week"; break } if (p[u] >
                        a) { u = z; break } if (t[u] && c.substr(t[u]) !== "01-01 00:00:00.000".substr(t[u])) break; "week" !== u && (z = u)
                } u && (h = k[u]); return h
            }, getXDateFormat: function (a, d, m) { d = d.dateTimeLabelFormats; var c = m && m.closestPointRange; return (c ? this.getDateFormat(c, a.x, m.options.startOfWeek, d) : d.day) || d.year }, tooltipFooterHeaderFormatter: function (a, d) {
                d = d ? "footer" : "header"; var c = a.series, k = c.tooltipOptions, b = k.xDateFormat, e = c.xAxis, h = e && "datetime" === e.options.type && n(a.key), u = k[d + "Format"]; h && !b && (b = this.getXDateFormat(a, k, e));
                h && b && D(a.point && a.point.tooltipDateKeys || ["key"], function (a) { u = u.replace("{point." + a + "}", "{point." + a + ":" + b + "}") }); return q(u, { point: a, series: c }, this.chart.time)
            }, bodyFormatter: function (a) { return f(a, function (a) { var c = a.series.tooltipOptions; return (c[(a.point.formatPrefix || "point") + "Formatter"] || a.point.tooltipFormatter).call(a.point, c[(a.point.formatPrefix || "point") + "Format"]) }) }
        }
    })(L); (function (a) {
        var C = a.addEvent, D = a.attr, E = a.charts, q = a.color, n = a.css, f = a.defined, r = a.each, A = a.extend, w = a.find, y =
            a.fireEvent, p = a.isNumber, c = a.isObject, d = a.offset, m = a.pick, k = a.splat, b = a.Tooltip; a.Pointer = function (a, b) { this.init(a, b) }; a.Pointer.prototype = {
                init: function (a, c) { this.options = c; this.chart = a; this.runChartClick = c.chart.events && !!c.chart.events.click; this.pinchDown = []; this.lastValidTouch = {}; b && (a.tooltip = new b(a, c.tooltip), this.followTouchMove = m(c.tooltip.followTouchMove, !0)); this.setDOMEvents() }, zoomOption: function (a) {
                    var b = this.chart, c = b.options.chart, e = c.zoomType || "", b = b.inverted; /touch/.test(a.type) &&
                        (e = m(c.pinchType, e)); this.zoomX = a = /x/.test(e); this.zoomY = e = /y/.test(e); this.zoomHor = a && !b || e && b; this.zoomVert = e && !b || a && b; this.hasZoom = a || e
                }, normalize: function (a, b) { var c; c = a.touches ? a.touches.length ? a.touches.item(0) : a.changedTouches[0] : a; b || (this.chartPosition = b = d(this.chart.container)); return A(a, { chartX: Math.round(c.pageX - b.left), chartY: Math.round(c.pageY - b.top) }) }, getCoordinates: function (a) {
                    var b = { xAxis: [], yAxis: [] }; r(this.chart.axes, function (c) {
                        b[c.isXAxis ? "xAxis" : "yAxis"].push({
                            axis: c, value: c.toValue(a[c.horiz ?
                                "chartX" : "chartY"])
                        })
                    }); return b
                }, findNearestKDPoint: function (a, b, k) { var e; r(a, function (a) { var h = !(a.noSharedTooltip && b) && 0 > a.options.findNearestPointBy.indexOf("y"); a = a.searchPoint(k, h); if ((h = c(a, !0)) && !(h = !c(e, !0))) var h = e.distX - a.distX, d = e.dist - a.dist, u = (a.series.group && a.series.group.zIndex) - (e.series.group && e.series.group.zIndex), h = 0 < (0 !== h && b ? h : 0 !== d ? d : 0 !== u ? u : e.series.index > a.series.index ? -1 : 1); h && (e = a) }); return e }, getPointFromEvent: function (a) {
                    a = a.target; for (var b; a && !b;)b = a.point, a = a.parentNode;
                    return b
                }, getChartCoordinatesFromPoint: function (a, b) { var c = a.series, e = c.xAxis, c = c.yAxis, h = m(a.clientX, a.plotX), k = a.shapeArgs; if (e && c) return b ? { chartX: e.len + e.pos - h, chartY: c.len + c.pos - a.plotY } : { chartX: h + e.pos, chartY: a.plotY + c.pos }; if (k && k.x && k.y) return { chartX: k.x, chartY: k.y } }, getHoverData: function (b, h, k, d, z, p, v) {
                    var e, l = [], u = v && v.isBoosting; d = !(!d || !b); v = h && !h.stickyTracking ? [h] : a.grep(k, function (a) { return a.visible && !(!z && a.directTouch) && m(a.options.enableMouseTracking, !0) && a.stickyTracking }); h = (e =
                        d ? b : this.findNearestKDPoint(v, z, p)) && e.series; e && (z && !h.noSharedTooltip ? (v = a.grep(k, function (a) { return a.visible && !(!z && a.directTouch) && m(a.options.enableMouseTracking, !0) && !a.noSharedTooltip }), r(v, function (a) { var b = w(a.points, function (a) { return a.x === e.x && !a.isNull }); c(b) && (u && (b = a.getPoint(b)), l.push(b)) })) : l.push(e)); return { hoverPoint: e, hoverSeries: h, hoverPoints: l }
                }, runPointActions: function (b, c) {
                    var e = this.chart, h = e.tooltip && e.tooltip.options.enabled ? e.tooltip : void 0, k = h ? h.shared : !1, d = c || e.hoverPoint,
                    v = d && d.series || e.hoverSeries, v = this.getHoverData(d, v, e.series, !!c || v && v.directTouch && this.isDirectTouch, k, b, { isBoosting: e.isBoosting }), p, d = v.hoverPoint; p = v.hoverPoints; c = (v = v.hoverSeries) && v.tooltipOptions.followPointer; k = k && v && !v.noSharedTooltip; if (d && (d !== e.hoverPoint || h && h.isHidden)) {
                        r(e.hoverPoints || [], function (b) { -1 === a.inArray(b, p) && b.setState() }); r(p || [], function (a) { a.setState("hover") }); if (e.hoverSeries !== v) v.onMouseOver(); e.hoverPoint && e.hoverPoint.firePointEvent("mouseOut"); if (!d.series) return;
                        d.firePointEvent("mouseOver"); e.hoverPoints = p; e.hoverPoint = d; h && h.refresh(k ? p : d, b)
                    } else c && h && !h.isHidden && (d = h.getAnchor([{}], b), h.updatePosition({ plotX: d[0], plotY: d[1] })); this.unDocMouseMove || (this.unDocMouseMove = C(e.container.ownerDocument, "mousemove", function (b) { var c = E[a.hoverChartIndex]; if (c) c.pointer.onDocumentMouseMove(b) })); r(e.axes, function (c) { var e = m(c.crosshair.snap, !0), l = e ? a.find(p, function (a) { return a.series[c.coll] === c }) : void 0; l || !e ? c.drawCrosshair(b, l) : c.hideCrosshair() })
                }, reset: function (a,
                    b) {
                        var c = this.chart, e = c.hoverSeries, h = c.hoverPoint, d = c.hoverPoints, m = c.tooltip, p = m && m.shared ? d : h; a && p && r(k(p), function (b) { b.series.isCartesian && void 0 === b.plotX && (a = !1) }); if (a) m && p && (m.refresh(p), h && (h.setState(h.state, !0), r(c.axes, function (a) { a.crosshair && a.drawCrosshair(null, h) }))); else {
                            if (h) h.onMouseOut(); d && r(d, function (a) { a.setState() }); if (e) e.onMouseOut(); m && m.hide(b); this.unDocMouseMove && (this.unDocMouseMove = this.unDocMouseMove()); r(c.axes, function (a) { a.hideCrosshair() }); this.hoverX = c.hoverPoints =
                                c.hoverPoint = null
                        }
                }, scaleGroups: function (a, b) { var c = this.chart, e; r(c.series, function (h) { e = a || h.getPlotBox(); h.xAxis && h.xAxis.zoomEnabled && h.group && (h.group.attr(e), h.markerGroup && (h.markerGroup.attr(e), h.markerGroup.clip(b ? c.clipRect : null)), h.dataLabelsGroup && h.dataLabelsGroup.attr(e)) }); c.clipRect.attr(b || c.clipBox) }, dragStart: function (a) { var b = this.chart; b.mouseIsDown = a.type; b.cancelClick = !1; b.mouseDownX = this.mouseDownX = a.chartX; b.mouseDownY = this.mouseDownY = a.chartY }, drag: function (a) {
                    var b = this.chart,
                    c = b.options.chart, e = a.chartX, k = a.chartY, d = this.zoomHor, m = this.zoomVert, p = b.plotLeft, l = b.plotTop, H = b.plotWidth, K = b.plotHeight, F, B = this.selectionMarker, g = this.mouseDownX, x = this.mouseDownY, f = c.panKey && a[c.panKey + "Key"]; B && B.touch || (e < p ? e = p : e > p + H && (e = p + H), k < l ? k = l : k > l + K && (k = l + K), this.hasDragged = Math.sqrt(Math.pow(g - e, 2) + Math.pow(x - k, 2)), 10 < this.hasDragged && (F = b.isInsidePlot(g - p, x - l), b.hasCartesianSeries && (this.zoomX || this.zoomY) && F && !f && !B && (this.selectionMarker = B = b.renderer.rect(p, l, d ? 1 : H, m ? 1 : K, 0).attr({
                        fill: c.selectionMarkerFill ||
                            q("#335cad").setOpacity(.25).get(), "class": "highcharts-selection-marker", zIndex: 7
                    }).add()), B && d && (e -= g, B.attr({ width: Math.abs(e), x: (0 < e ? 0 : e) + g })), B && m && (e = k - x, B.attr({ height: Math.abs(e), y: (0 < e ? 0 : e) + x })), F && !B && c.panning && b.pan(a, c.panning)))
                }, drop: function (a) {
                    var b = this, c = this.chart, e = this.hasPinched; if (this.selectionMarker) {
                        var k = { originalEvent: a, xAxis: [], yAxis: [] }, d = this.selectionMarker, m = d.attr ? d.attr("x") : d.x, G = d.attr ? d.attr("y") : d.y, l = d.attr ? d.attr("width") : d.width, H = d.attr ? d.attr("height") :
                            d.height, K; if (this.hasDragged || e) r(c.axes, function (c) { if (c.zoomEnabled && f(c.min) && (e || b[{ xAxis: "zoomX", yAxis: "zoomY" }[c.coll]])) { var h = c.horiz, g = "touchend" === a.type ? c.minPixelPadding : 0, d = c.toValue((h ? m : G) + g), h = c.toValue((h ? m + l : G + H) - g); k[c.coll].push({ axis: c, min: Math.min(d, h), max: Math.max(d, h) }); K = !0 } }), K && y(c, "selection", k, function (a) { c.zoom(A(a, e ? { animation: !1 } : null)) }); p(c.index) && (this.selectionMarker = this.selectionMarker.destroy()); e && this.scaleGroups()
                    } c && p(c.index) && (n(c.container, { cursor: c._cursor }),
                        c.cancelClick = 10 < this.hasDragged, c.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [])
                }, onContainerMouseDown: function (a) { a = this.normalize(a); 2 !== a.button && (this.zoomOption(a), a.preventDefault && a.preventDefault(), this.dragStart(a)) }, onDocumentMouseUp: function (b) { E[a.hoverChartIndex] && E[a.hoverChartIndex].pointer.drop(b) }, onDocumentMouseMove: function (a) {
                    var b = this.chart, c = this.chartPosition; a = this.normalize(a, c); !c || this.inClass(a.target, "highcharts-tracker") || b.isInsidePlot(a.chartX -
                        b.plotLeft, a.chartY - b.plotTop) || this.reset()
                }, onContainerMouseLeave: function (b) { var c = E[a.hoverChartIndex]; c && (b.relatedTarget || b.toElement) && (c.pointer.reset(), c.pointer.chartPosition = null) }, onContainerMouseMove: function (b) {
                    var c = this.chart; f(a.hoverChartIndex) && E[a.hoverChartIndex] && E[a.hoverChartIndex].mouseIsDown || (a.hoverChartIndex = c.index); b = this.normalize(b); b.returnValue = !1; "mousedown" === c.mouseIsDown && this.drag(b); !this.inClass(b.target, "highcharts-tracker") && !c.isInsidePlot(b.chartX - c.plotLeft,
                        b.chartY - c.plotTop) || c.openMenu || this.runPointActions(b)
                }, inClass: function (a, b) { for (var c; a;) { if (c = D(a, "class")) { if (-1 !== c.indexOf(b)) return !0; if (-1 !== c.indexOf("highcharts-container")) return !1 } a = a.parentNode } }, onTrackerMouseOut: function (a) { var b = this.chart.hoverSeries; a = a.relatedTarget || a.toElement; this.isDirectTouch = !1; if (!(!b || !a || b.stickyTracking || this.inClass(a, "highcharts-tooltip") || this.inClass(a, "highcharts-series-" + b.index) && this.inClass(a, "highcharts-tracker"))) b.onMouseOut() }, onContainerClick: function (a) {
                    var b =
                        this.chart, c = b.hoverPoint, e = b.plotLeft, k = b.plotTop; a = this.normalize(a); b.cancelClick || (c && this.inClass(a.target, "highcharts-tracker") ? (y(c.series, "click", A(a, { point: c })), b.hoverPoint && c.firePointEvent("click", a)) : (A(a, this.getCoordinates(a)), b.isInsidePlot(a.chartX - e, a.chartY - k) && y(b, "click", a)))
                }, setDOMEvents: function () {
                    var b = this, c = b.chart.container, k = c.ownerDocument; c.onmousedown = function (a) { b.onContainerMouseDown(a) }; c.onmousemove = function (a) { b.onContainerMouseMove(a) }; c.onclick = function (a) { b.onContainerClick(a) };
                    this.unbindContainerMouseLeave = C(c, "mouseleave", b.onContainerMouseLeave); a.unbindDocumentMouseUp || (a.unbindDocumentMouseUp = C(k, "mouseup", b.onDocumentMouseUp)); a.hasTouch && (c.ontouchstart = function (a) { b.onContainerTouchStart(a) }, c.ontouchmove = function (a) { b.onContainerTouchMove(a) }, a.unbindDocumentTouchEnd || (a.unbindDocumentTouchEnd = C(k, "touchend", b.onDocumentTouchEnd)))
                }, destroy: function () {
                    var b = this; b.unDocMouseMove && b.unDocMouseMove(); this.unbindContainerMouseLeave(); a.chartCount || (a.unbindDocumentMouseUp &&
                        (a.unbindDocumentMouseUp = a.unbindDocumentMouseUp()), a.unbindDocumentTouchEnd && (a.unbindDocumentTouchEnd = a.unbindDocumentTouchEnd())); clearInterval(b.tooltipTimeout); a.objectEach(b, function (a, c) { b[c] = null })
                }
            }
    })(L); (function (a) {
        var C = a.charts, D = a.each, E = a.extend, q = a.map, n = a.noop, f = a.pick; E(a.Pointer.prototype, {
            pinchTranslate: function (a, f, n, q, p, c) { this.zoomHor && this.pinchTranslateDirection(!0, a, f, n, q, p, c); this.zoomVert && this.pinchTranslateDirection(!1, a, f, n, q, p, c) }, pinchTranslateDirection: function (a,
                f, n, q, p, c, d, m) {
                    var k = this.chart, b = a ? "x" : "y", e = a ? "X" : "Y", h = "chart" + e, u = a ? "width" : "height", t = k["plot" + (a ? "Left" : "Top")], z, I, v = m || 1, G = k.inverted, l = k.bounds[a ? "h" : "v"], H = 1 === f.length, K = f[0][h], F = n[0][h], B = !H && f[1][h], g = !H && n[1][h], x; n = function () { !H && 20 < Math.abs(K - B) && (v = m || Math.abs(F - g) / Math.abs(K - B)); I = (t - F) / v + K; z = k["plot" + (a ? "Width" : "Height")] / v }; n(); f = I; f < l.min ? (f = l.min, x = !0) : f + z > l.max && (f = l.max - z, x = !0); x ? (F -= .8 * (F - d[b][0]), H || (g -= .8 * (g - d[b][1])), n()) : d[b] = [F, g]; G || (c[b] = I - t, c[u] = z); c = G ? 1 / v : v; p[u] =
                        z; p[b] = f; q[G ? a ? "scaleY" : "scaleX" : "scale" + e] = v; q["translate" + e] = c * t + (F - c * K)
            }, pinch: function (a) {
                var r = this, w = r.chart, y = r.pinchDown, p = a.touches, c = p.length, d = r.lastValidTouch, m = r.hasZoom, k = r.selectionMarker, b = {}, e = 1 === c && (r.inClass(a.target, "highcharts-tracker") && w.runTrackerClick || r.runChartClick), h = {}; 1 < c && (r.initiated = !0); m && r.initiated && !e && a.preventDefault(); q(p, function (a) { return r.normalize(a) }); "touchstart" === a.type ? (D(p, function (a, b) { y[b] = { chartX: a.chartX, chartY: a.chartY } }), d.x = [y[0].chartX,
                y[1] && y[1].chartX], d.y = [y[0].chartY, y[1] && y[1].chartY], D(w.axes, function (a) { if (a.zoomEnabled) { var b = w.bounds[a.horiz ? "h" : "v"], c = a.minPixelPadding, e = a.toPixels(f(a.options.min, a.dataMin)), k = a.toPixels(f(a.options.max, a.dataMax)), h = Math.max(e, k); b.min = Math.min(a.pos, Math.min(e, k) - c); b.max = Math.max(a.pos + a.len, h + c) } }), r.res = !0) : r.followTouchMove && 1 === c ? this.runPointActions(r.normalize(a)) : y.length && (k || (r.selectionMarker = k = E({ destroy: n, touch: !0 }, w.plotBox)), r.pinchTranslate(y, p, b, k, h, d), r.hasPinched =
                    m, r.scaleGroups(b, h), r.res && (r.res = !1, this.reset(!1, 0)))
            }, touch: function (n, q) {
                var w = this.chart, r, p; if (w.index !== a.hoverChartIndex) this.onContainerMouseLeave({ relatedTarget: !0 }); a.hoverChartIndex = w.index; 1 === n.touches.length ? (n = this.normalize(n), (p = w.isInsidePlot(n.chartX - w.plotLeft, n.chartY - w.plotTop)) && !w.openMenu ? (q && this.runPointActions(n), "touchmove" === n.type && (q = this.pinchDown, r = q[0] ? 4 <= Math.sqrt(Math.pow(q[0].chartX - n.chartX, 2) + Math.pow(q[0].chartY - n.chartY, 2)) : !1), f(r, !0) && this.pinch(n)) : q &&
                    this.reset()) : 2 === n.touches.length && this.pinch(n)
            }, onContainerTouchStart: function (a) { this.zoomOption(a); this.touch(a, !0) }, onContainerTouchMove: function (a) { this.touch(a) }, onDocumentTouchEnd: function (f) { C[a.hoverChartIndex] && C[a.hoverChartIndex].pointer.drop(f) }
        })
    })(L); (function (a) {
        var C = a.addEvent, D = a.charts, E = a.css, q = a.doc, n = a.extend, f = a.noop, r = a.Pointer, A = a.removeEvent, w = a.win, y = a.wrap; if (!a.hasTouch && (w.PointerEvent || w.MSPointerEvent)) {
            var p = {}, c = !!w.PointerEvent, d = function () {
                var c = []; c.item = function (a) { return this[a] };
                a.objectEach(p, function (a) { c.push({ pageX: a.pageX, pageY: a.pageY, target: a.target }) }); return c
            }, m = function (c, b, e, h) { "touch" !== c.pointerType && c.pointerType !== c.MSPOINTER_TYPE_TOUCH || !D[a.hoverChartIndex] || (h(c), h = D[a.hoverChartIndex].pointer, h[b]({ type: e, target: c.currentTarget, preventDefault: f, touches: d() })) }; n(r.prototype, {
                onContainerPointerDown: function (a) { m(a, "onContainerTouchStart", "touchstart", function (a) { p[a.pointerId] = { pageX: a.pageX, pageY: a.pageY, target: a.currentTarget } }) }, onContainerPointerMove: function (a) {
                    m(a,
                        "onContainerTouchMove", "touchmove", function (a) { p[a.pointerId] = { pageX: a.pageX, pageY: a.pageY }; p[a.pointerId].target || (p[a.pointerId].target = a.currentTarget) })
                }, onDocumentPointerUp: function (a) { m(a, "onDocumentTouchEnd", "touchend", function (a) { delete p[a.pointerId] }) }, batchMSEvents: function (a) { a(this.chart.container, c ? "pointerdown" : "MSPointerDown", this.onContainerPointerDown); a(this.chart.container, c ? "pointermove" : "MSPointerMove", this.onContainerPointerMove); a(q, c ? "pointerup" : "MSPointerUp", this.onDocumentPointerUp) }
            });
            y(r.prototype, "init", function (a, b, c) { a.call(this, b, c); this.hasZoom && E(b.container, { "-ms-touch-action": "none", "touch-action": "none" }) }); y(r.prototype, "setDOMEvents", function (a) { a.apply(this); (this.hasZoom || this.followTouchMove) && this.batchMSEvents(C) }); y(r.prototype, "destroy", function (a) { this.batchMSEvents(A); a.call(this) })
        }
    })(L); (function (a) {
        var C = a.addEvent, D = a.css, E = a.discardElement, q = a.defined, n = a.each, f = a.fireEvent, r = a.isFirefox, A = a.marginNames, w = a.merge, y = a.pick, p = a.setAnimation, c = a.stableSort,
        d = a.win, m = a.wrap; a.Legend = function (a, b) { this.init(a, b) }; a.Legend.prototype = {
            init: function (a, b) { this.chart = a; this.setOptions(b); b.enabled && (this.render(), C(this.chart, "endResize", function () { this.legend.positionCheckboxes() }), this.proximate ? this.unchartrender = C(this.chart, "render", function () { this.legend.proximatePositions(); this.legend.positionItems() }) : this.unchartrender && this.unchartrender()) }, setOptions: function (a) {
                var b = y(a.padding, 8); this.options = a; this.itemStyle = a.itemStyle; this.itemHiddenStyle =
                    w(this.itemStyle, a.itemHiddenStyle); this.itemMarginTop = a.itemMarginTop || 0; this.padding = b; this.initialItemY = b - 5; this.symbolWidth = y(a.symbolWidth, 16); this.pages = []; this.proximate = "proximate" === a.layout && !this.chart.inverted
            }, update: function (a, b) { var c = this.chart; this.setOptions(w(!0, this.options, a)); this.destroy(); c.isDirtyLegend = c.isDirtyBox = !0; y(b, !0) && c.redraw(); f(this, "afterUpdate") }, colorizeItem: function (a, b) {
            a.legendGroup[b ? "removeClass" : "addClass"]("highcharts-legend-item-hidden"); var c = this.options,
                h = a.legendItem, d = a.legendLine, k = a.legendSymbol, m = this.itemHiddenStyle.color, c = b ? c.itemStyle.color : m, p = b ? a.color || m : m, v = a.options && a.options.marker, G = { fill: p }; h && h.css({ fill: c, color: c }); d && d.attr({ stroke: p }); k && (v && k.isMarker && (G = a.pointAttribs(), b || (G.stroke = G.fill = m)), k.attr(G)); f(this, "afterColorizeItem", { item: a, visible: b })
            }, positionItems: function () { n(this.allItems, this.positionItem, this); this.chart.isResizing || this.positionCheckboxes() }, positionItem: function (a) {
                var b = this.options, c = b.symbolPadding,
                b = !b.rtl, h = a._legendItemPos, d = h[0], h = h[1], k = a.checkbox; if ((a = a.legendGroup) && a.element) a[q(a.translateY) ? "animate" : "attr"]({ translateX: b ? d : this.legendWidth - d - 2 * c - 4, translateY: h }); k && (k.x = d, k.y = h)
            }, destroyItem: function (a) { var b = a.checkbox; n(["legendItem", "legendLine", "legendSymbol", "legendGroup"], function (b) { a[b] && (a[b] = a[b].destroy()) }); b && E(a.checkbox) }, destroy: function () {
                function a(a) { this[a] && (this[a] = this[a].destroy()) } n(this.getAllItems(), function (b) { n(["legendItem", "legendGroup"], a, b) }); n("clipRect up down pager nav box title group".split(" "),
                    a, this); this.display = null
            }, positionCheckboxes: function () { var a = this.group && this.group.alignAttr, b, c = this.clipHeight || this.legendHeight, h = this.titleHeight; a && (b = a.translateY, n(this.allItems, function (e) { var d = e.checkbox, k; d && (k = b + h + d.y + (this.scrollOffset || 0) + 3, D(d, { left: a.translateX + e.checkboxOffset + d.x - 20 + "px", top: k + "px", display: k > b - 6 && k < b + c - 6 ? "" : "none" })) }, this)) }, renderTitle: function () {
                var a = this.options, b = this.padding, c = a.title, d = 0; c.text && (this.title || (this.title = this.chart.renderer.label(c.text,
                    b - 3, b - 4, null, null, null, a.useHTML, null, "legend-title").attr({ zIndex: 1 }).css(c.style).add(this.group)), a = this.title.getBBox(), d = a.height, this.offsetWidth = a.width, this.contentGroup.attr({ translateY: d })); this.titleHeight = d
            }, setText: function (c) { var b = this.options; c.legendItem.attr({ text: b.labelFormat ? a.format(b.labelFormat, c, this.chart.time) : b.labelFormatter.call(c) }) }, renderItem: function (a) {
                var b = this.chart, c = b.renderer, d = this.options, k = this.symbolWidth, m = d.symbolPadding, z = this.itemStyle, p = this.itemHiddenStyle,
                v = "horizontal" === d.layout ? y(d.itemDistance, 20) : 0, f = !d.rtl, l = a.legendItem, H = !a.series, K = !H && a.series.drawLegendSymbol ? a.series : a, F = K.options, F = this.createCheckboxForItem && F && F.showCheckbox, v = k + m + v + (F ? 20 : 0), B = d.useHTML, g = a.options.className; l || (a.legendGroup = c.g("legend-item").addClass("highcharts-" + K.type + "-series highcharts-color-" + a.colorIndex + (g ? " " + g : "") + (H ? " highcharts-series-" + a.index : "")).attr({ zIndex: 1 }).add(this.scrollGroup), a.legendItem = l = c.text("", f ? k + m : -m, this.baseline || 0, B).css(w(a.visible ?
                    z : p)).attr({ align: f ? "left" : "right", zIndex: 2 }).add(a.legendGroup), this.baseline || (k = z.fontSize, this.fontMetrics = c.fontMetrics(k, l), this.baseline = this.fontMetrics.f + 3 + this.itemMarginTop, l.attr("y", this.baseline)), this.symbolHeight = d.symbolHeight || this.fontMetrics.f, K.drawLegendSymbol(this, a), this.setItemEvents && this.setItemEvents(a, l, B), F && this.createCheckboxForItem(a)); this.colorizeItem(a, a.visible); z.width || l.css({ width: (d.itemWidth || d.width || b.spacingBox.width) - v }); this.setText(a); b = l.getBBox();
                a.itemWidth = a.checkboxOffset = d.itemWidth || a.legendItemWidth || b.width + v; this.maxItemWidth = Math.max(this.maxItemWidth, a.itemWidth); this.totalItemWidth += a.itemWidth; this.itemHeight = a.itemHeight = Math.round(a.legendItemHeight || b.height || this.symbolHeight)
            }, layoutItem: function (a) {
                var b = this.options, c = this.padding, d = "horizontal" === b.layout, k = a.itemHeight, m = b.itemMarginBottom || 0, z = this.itemMarginTop, p = d ? y(b.itemDistance, 20) : 0, v = b.width, f = v || this.chart.spacingBox.width - 2 * c - b.x, b = b.alignColumns && this.totalItemWidth >
                    f ? this.maxItemWidth : a.itemWidth; d && this.itemX - c + b > f && (this.itemX = c, this.itemY += z + this.lastLineHeight + m, this.lastLineHeight = 0); this.lastItemY = z + this.itemY + m; this.lastLineHeight = Math.max(k, this.lastLineHeight); a._legendItemPos = [this.itemX, this.itemY]; d ? this.itemX += b : (this.itemY += z + k + m, this.lastLineHeight = k); this.offsetWidth = v || Math.max((d ? this.itemX - c - (a.checkbox ? 0 : p) : b) + c, this.offsetWidth)
            }, getAllItems: function () {
                var a = []; n(this.chart.series, function (b) {
                    var c = b && b.options; b && y(c.showInLegend, q(c.linkedTo) ?
                        !1 : void 0, !0) && (a = a.concat(b.legendItems || ("point" === c.legendType ? b.data : b)))
                }); f(this, "afterGetAllItems", { allItems: a }); return a
            }, getAlignment: function () { var a = this.options; return this.proximate ? a.align.charAt(0) + "tv" : a.floating ? "" : a.align.charAt(0) + a.verticalAlign.charAt(0) + a.layout.charAt(0) }, adjustMargins: function (a, b) {
                var c = this.chart, d = this.options, k = this.getAlignment(); k && n([/(lth|ct|rth)/, /(rtv|rm|rbv)/, /(rbh|cb|lbh)/, /(lbv|lm|ltv)/], function (e, h) {
                    e.test(k) && !q(a[h]) && (c[A[h]] = Math.max(c[A[h]],
                        c.legend[(h + 1) % 2 ? "legendHeight" : "legendWidth"] + [1, -1, -1, 1][h] * d[h % 2 ? "x" : "y"] + y(d.margin, 12) + b[h] + (0 === h && void 0 !== c.options.title.margin ? c.titleOffset + c.options.title.margin : 0)))
                })
            }, proximatePositions: function () {
                var c = this.chart, b = [], e = "left" === this.options.align; n(this.allItems, function (d) {
                    var h, k; h = e; d.xAxis && d.points && (d.xAxis.options.reversed && (h = !h), h = a.find(h ? d.points : d.points.slice(0).reverse(), function (b) { return a.isNumber(b.plotY) }), k = d.legendGroup.getBBox().height, b.push({
                        target: d.visible ?
                            h.plotY - .3 * k : c.plotHeight, size: k, item: d
                    }))
                }, this); a.distribute(b, c.plotHeight); n(b, function (a) { a.item._legendItemPos[1] = c.plotTop - c.spacing[0] + a.pos })
            }, render: function () {
                var a = this.chart, b = a.renderer, e = this.group, d, m, t, z = this.box, p = this.options, v = this.padding; this.itemX = v; this.itemY = this.initialItemY; this.lastItemY = this.offsetWidth = 0; e || (this.group = e = b.g("legend").attr({ zIndex: 7 }).add(), this.contentGroup = b.g().attr({ zIndex: 1 }).add(e), this.scrollGroup = b.g().add(this.contentGroup)); this.renderTitle();
                d = this.getAllItems(); c(d, function (a, b) { return (a.options && a.options.legendIndex || 0) - (b.options && b.options.legendIndex || 0) }); p.reversed && d.reverse(); this.allItems = d; this.display = m = !!d.length; this.itemHeight = this.totalItemWidth = this.maxItemWidth = this.lastLineHeight = 0; n(d, this.renderItem, this); n(d, this.layoutItem, this); d = (p.width || this.offsetWidth) + v; t = this.lastItemY + this.lastLineHeight + this.titleHeight; t = this.handleOverflow(t); t += v; z || (this.box = z = b.rect().addClass("highcharts-legend-box").attr({ r: p.borderRadius }).add(e),
                    z.isNew = !0); z.attr({ stroke: p.borderColor, "stroke-width": p.borderWidth || 0, fill: p.backgroundColor || "none" }).shadow(p.shadow); 0 < d && 0 < t && (z[z.isNew ? "attr" : "animate"](z.crisp.call({}, { x: 0, y: 0, width: d, height: t }, z.strokeWidth())), z.isNew = !1); z[m ? "show" : "hide"](); this.legendWidth = d; this.legendHeight = t; m && (b = a.spacingBox, /(lth|ct|rth)/.test(this.getAlignment()) && (b = w(b, { y: b.y + a.titleOffset + a.options.title.margin })), e.align(w(p, { width: d, height: t, verticalAlign: this.proximate ? "top" : p.verticalAlign }), !0, b)); this.proximate ||
                        this.positionItems()
            }, handleOverflow: function (a) {
                var b = this, c = this.chart, d = c.renderer, k = this.options, m = k.y, p = this.padding, c = c.spacingBox.height + ("top" === k.verticalAlign ? -m : m) - p, m = k.maxHeight, f, v = this.clipRect, G = k.navigation, l = y(G.animation, !0), H = G.arrowSize || 12, K = this.nav, F = this.pages, B, g = this.allItems, x = function (a) { "number" === typeof a ? v.attr({ height: a }) : v && (b.clipRect = v.destroy(), b.contentGroup.clip()); b.contentGroup.div && (b.contentGroup.div.style.clip = a ? "rect(" + p + "px,9999px," + (p + a) + "px,0)" : "auto") };
                "horizontal" !== k.layout || "middle" === k.verticalAlign || k.floating || (c /= 2); m && (c = Math.min(c, m)); F.length = 0; a > c && !1 !== G.enabled ? (this.clipHeight = f = Math.max(c - 20 - this.titleHeight - p, 0), this.currentPage = y(this.currentPage, 1), this.fullHeight = a, n(g, function (a, b) { var c = a._legendItemPos[1], e = Math.round(a.legendItem.getBBox().height), l = F.length; if (!l || c - F[l - 1] > f && (B || c) !== F[l - 1]) F.push(B || c), l++; a.pageIx = l - 1; B && (g[b - 1].pageIx = l - 1); b === g.length - 1 && c + e - F[l - 1] > f && (F.push(c), a.pageIx = l); c !== B && (B = c) }), v || (v = b.clipRect =
                    d.clipRect(0, p, 9999, 0), b.contentGroup.clip(v)), x(f), K || (this.nav = K = d.g().attr({ zIndex: 1 }).add(this.group), this.up = d.symbol("triangle", 0, 0, H, H).on("click", function () { b.scroll(-1, l) }).add(K), this.pager = d.text("", 15, 10).addClass("highcharts-legend-navigation").css(G.style).add(K), this.down = d.symbol("triangle-down", 0, 0, H, H).on("click", function () { b.scroll(1, l) }).add(K)), b.scroll(0), a = c) : K && (x(), this.nav = K.destroy(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight = 0); return a
            }, scroll: function (a, b) {
                var c =
                    this.pages, d = c.length; a = this.currentPage + a; var k = this.clipHeight, m = this.options.navigation, z = this.pager, f = this.padding; a > d && (a = d); 0 < a && (void 0 !== b && p(b, this.chart), this.nav.attr({ translateX: f, translateY: k + this.padding + 7 + this.titleHeight, visibility: "visible" }), this.up.attr({ "class": 1 === a ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }), z.attr({ text: a + "/" + d }), this.down.attr({ x: 18 + this.pager.getBBox().width, "class": a === d ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }),
                        this.up.attr({ fill: 1 === a ? m.inactiveColor : m.activeColor }).css({ cursor: 1 === a ? "default" : "pointer" }), this.down.attr({ fill: a === d ? m.inactiveColor : m.activeColor }).css({ cursor: a === d ? "default" : "pointer" }), this.scrollOffset = -c[a - 1] + this.initialItemY, this.scrollGroup.animate({ translateY: this.scrollOffset }), this.currentPage = a, this.positionCheckboxes())
            }
        }; a.LegendSymbolMixin = {
            drawRectangle: function (a, b) {
                var c = a.symbolHeight, d = a.options.squareSymbol; b.legendSymbol = this.chart.renderer.rect(d ? (a.symbolWidth - c) / 2 :
                    0, a.baseline - c + 1, d ? c : a.symbolWidth, c, y(a.options.symbolRadius, c / 2)).addClass("highcharts-point").attr({ zIndex: 3 }).add(b.legendGroup)
            }, drawLineMarker: function (a) {
                var b = this.options, c = b.marker, d = a.symbolWidth, m = a.symbolHeight, k = m / 2, p = this.chart.renderer, f = this.legendGroup; a = a.baseline - Math.round(.3 * a.fontMetrics.b); var v; v = { "stroke-width": b.lineWidth || 0 }; b.dashStyle && (v.dashstyle = b.dashStyle); this.legendLine = p.path(["M", 0, a, "L", d, a]).addClass("highcharts-graph").attr(v).add(f); c && !1 !== c.enabled && d &&
                    (b = Math.min(y(c.radius, k), k), 0 === this.symbol.indexOf("url") && (c = w(c, { width: m, height: m }), b = 0), this.legendSymbol = c = p.symbol(this.symbol, d / 2 - b, a - b, 2 * b, 2 * b, c).addClass("highcharts-point").add(f), c.isMarker = !0)
            }
        }; (/Trident\/7\.0/.test(d.navigator.userAgent) || r) && m(a.Legend.prototype, "positionItem", function (a, b) { var c = this, d = function () { b._legendItemPos && a.call(c, b) }; d(); setTimeout(d) })
    })(L); (function (a) {
        var C = a.addEvent, D = a.animate, E = a.animObject, q = a.attr, n = a.doc, f = a.Axis, r = a.createElement, A = a.defaultOptions,
        w = a.discardElement, y = a.charts, p = a.css, c = a.defined, d = a.each, m = a.extend, k = a.find, b = a.fireEvent, e = a.grep, h = a.isNumber, u = a.isObject, t = a.isString, z = a.Legend, I = a.marginNames, v = a.merge, G = a.objectEach, l = a.Pointer, H = a.pick, K = a.pInt, F = a.removeEvent, B = a.seriesTypes, g = a.splat, x = a.syncTimeout, N = a.win, O = a.Chart = function () { this.getArgs.apply(this, arguments) }; a.chart = function (a, b, c) { return new O(a, b, c) }; m(O.prototype, {
            callbacks: [], getArgs: function () {
                var a = [].slice.call(arguments); if (t(a[0]) || a[0].nodeName) this.renderTo =
                    a.shift(); this.init(a[0], a[1])
            }, init: function (c, g) {
                var l, e, d = c.series, B = c.plotOptions || {}; b(this, "init", { args: arguments }, function () {
                c.series = null; l = v(A, c); for (e in l.plotOptions) l.plotOptions[e].tooltip = B[e] && v(B[e].tooltip) || void 0; l.tooltip.userOptions = c.chart && c.chart.forExport && c.tooltip.userOptions || c.tooltip; l.series = c.series = d; this.userOptions = c; var h = l.chart, m = h.events; this.margin = []; this.spacing = []; this.bounds = { h: {}, v: {} }; this.labelCollectors = []; this.callback = g; this.isResizing = 0; this.options =
                    l; this.axes = []; this.series = []; this.time = c.time && a.keys(c.time).length ? new a.Time(c.time) : a.time; this.hasCartesianSeries = h.showAxes; var k = this; k.index = y.length; y.push(k); a.chartCount++; m && G(m, function (a, b) { C(k, b, a) }); k.xAxis = []; k.yAxis = []; k.pointCount = k.colorCounter = k.symbolCounter = 0; b(k, "afterInit"); k.firstRender()
                })
            }, initSeries: function (b) { var c = this.options.chart; (c = B[b.type || c.type || c.defaultSeriesType]) || a.error(17, !0); c = new c; c.init(this, b); return c }, orderSeries: function (a) {
                var b = this.series;
                for (a = a || 0; a < b.length; a++)b[a] && (b[a].index = a, b[a].name = b[a].getName())
            }, isInsidePlot: function (a, b, c) { var g = c ? b : a; a = c ? a : b; return 0 <= g && g <= this.plotWidth && 0 <= a && a <= this.plotHeight }, redraw: function (c) {
                b(this, "beforeRedraw"); var g = this.axes, l = this.series, e = this.pointer, B = this.legend, h = this.isDirtyLegend, k, F, x = this.hasCartesianSeries, p = this.isDirtyBox, v, z = this.renderer, t = z.isHidden(), f = []; this.setResponsive && this.setResponsive(!1); a.setAnimation(c, this); t && this.temporaryDisplay(); this.layOutTitles();
                for (c = l.length; c--;)if (v = l[c], v.options.stacking && (k = !0, v.isDirty)) { F = !0; break } if (F) for (c = l.length; c--;)v = l[c], v.options.stacking && (v.isDirty = !0); d(l, function (a) { a.isDirty && "point" === a.options.legendType && (a.updateTotals && a.updateTotals(), h = !0); a.isDirtyData && b(a, "updatedData") }); h && B.options.enabled && (B.render(), this.isDirtyLegend = !1); k && this.getStacks(); x && d(g, function (a) { a.updateNames(); a.setScale() }); this.getMargins(); x && (d(g, function (a) { a.isDirty && (p = !0) }), d(g, function (a) {
                    var c = a.min + "," + a.max;
                    a.extKey !== c && (a.extKey = c, f.push(function () { b(a, "afterSetExtremes", m(a.eventArgs, a.getExtremes())); delete a.eventArgs })); (p || k) && a.redraw()
                })); p && this.drawChartBox(); b(this, "predraw"); d(l, function (a) { (p || a.isDirty) && a.visible && a.redraw(); a.isDirtyData = !1 }); e && e.reset(!0); z.draw(); b(this, "redraw"); b(this, "render"); t && this.temporaryDisplay(!0); d(f, function (a) { a.call() })
            }, get: function (a) {
                function b(b) { return b.id === a || b.options && b.options.id === a } var c, g = this.series, l; c = k(this.axes, b) || k(this.series,
                    b); for (l = 0; !c && l < g.length; l++)c = k(g[l].points || [], b); return c
            }, getAxes: function () { var a = this, c = this.options, l = c.xAxis = g(c.xAxis || {}), c = c.yAxis = g(c.yAxis || {}); b(this, "getAxes"); d(l, function (a, b) { a.index = b; a.isX = !0 }); d(c, function (a, b) { a.index = b }); l = l.concat(c); d(l, function (b) { new f(a, b) }); b(this, "afterGetAxes") }, getSelectedPoints: function () { var a = []; d(this.series, function (b) { a = a.concat(e(b.data || [], function (a) { return a.selected })) }); return a }, getSelectedSeries: function () { return e(this.series, function (a) { return a.selected }) },
            setTitle: function (a, b, c) { var g = this, l = g.options, e; e = l.title = v({ style: { color: "#333333", fontSize: l.isStock ? "16px" : "18px" } }, l.title, a); l = l.subtitle = v({ style: { color: "#666666" } }, l.subtitle, b); d([["title", a, e], ["subtitle", b, l]], function (a, b) { var c = a[0], l = g[c], e = a[1]; a = a[2]; l && e && (g[c] = l = l.destroy()); a && !l && (g[c] = g.renderer.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": "highcharts-" + c, zIndex: a.zIndex || 4 }).add(), g[c].update = function (a) { g.setTitle(!b && a, b && a) }, g[c].css(a.style)) }); g.layOutTitles(c) },
            layOutTitles: function (a) {
                var b = 0, c, g = this.renderer, l = this.spacingBox; d(["title", "subtitle"], function (a) { var c = this[a], e = this.options[a]; a = "title" === a ? -3 : e.verticalAlign ? 0 : b + 2; var d; c && (d = e.style.fontSize, d = g.fontMetrics(d, c).b, c.css({ width: (e.width || l.width + e.widthAdjust) + "px" }).align(m({ y: a + d }, e), !1, "spacingBox"), e.floating || e.verticalAlign || (b = Math.ceil(b + c.getBBox(e.useHTML).height))) }, this); c = this.titleOffset !== b; this.titleOffset = b; !this.isDirtyBox && c && (this.isDirtyBox = this.isDirtyLegend = c, this.hasRendered &&
                    H(a, !0) && this.isDirtyBox && this.redraw())
            }, getChartSize: function () { var b = this.options.chart, g = b.width, b = b.height, l = this.renderTo; c(g) || (this.containerWidth = a.getStyle(l, "width")); c(b) || (this.containerHeight = a.getStyle(l, "height")); this.chartWidth = Math.max(0, g || this.containerWidth || 600); this.chartHeight = Math.max(0, a.relativeLength(b, this.chartWidth) || (1 < this.containerHeight ? this.containerHeight : 400)) }, temporaryDisplay: function (b) {
                var c = this.renderTo; if (b) for (; c && c.style;)c.hcOrigStyle && (a.css(c, c.hcOrigStyle),
                    delete c.hcOrigStyle), c.hcOrigDetached && (n.body.removeChild(c), c.hcOrigDetached = !1), c = c.parentNode; else for (; c && c.style;) {
                        n.body.contains(c) || c.parentNode || (c.hcOrigDetached = !0, n.body.appendChild(c)); if ("none" === a.getStyle(c, "display", !1) || c.hcOricDetached) c.hcOrigStyle = { display: c.style.display, height: c.style.height, overflow: c.style.overflow }, b = { display: "block", overflow: "hidden" }, c !== this.renderTo && (b.height = 0), a.css(c, b), c.offsetWidth || c.style.setProperty("display", "block", "important"); c = c.parentNode;
                        if (c === n.body) break
                    }
            }, setClassName: function (a) { this.container.className = "highcharts-container " + (a || "") }, getContainer: function () {
                var c, g = this.options, l = g.chart, e, d; c = this.renderTo; var B = a.uniqueKey(), k; c || (this.renderTo = c = l.renderTo); t(c) && (this.renderTo = c = n.getElementById(c)); c || a.error(13, !0); e = K(q(c, "data-highcharts-chart")); h(e) && y[e] && y[e].hasRendered && y[e].destroy(); q(c, "data-highcharts-chart", this.index); c.innerHTML = ""; l.skipClone || c.offsetWidth || this.temporaryDisplay(); this.getChartSize();
                e = this.chartWidth; d = this.chartHeight; k = m({ position: "relative", overflow: "hidden", width: e + "px", height: d + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)" }, l.style); this.container = c = r("div", { id: B }, k, c); this._cursor = c.style.cursor; this.renderer = new (a[l.renderer] || a.Renderer)(c, e, d, null, l.forExport, g.exporting && g.exporting.allowHTML); this.setClassName(l.className); this.renderer.setStyle(l.style); this.renderer.chartIndex = this.index; b(this, "afterGetContainer")
            },
            getMargins: function (a) { var g = this.spacing, l = this.margin, e = this.titleOffset; this.resetMargins(); e && !c(l[0]) && (this.plotTop = Math.max(this.plotTop, e + this.options.title.margin + g[0])); this.legend && this.legend.display && this.legend.adjustMargins(l, g); b(this, "getMargins"); a || this.getAxisMargins() }, getAxisMargins: function () { var a = this, b = a.axisOffset = [0, 0, 0, 0], g = a.margin; a.hasCartesianSeries && d(a.axes, function (a) { a.visible && a.getOffset() }); d(I, function (l, e) { c(g[e]) || (a[l] += b[e]) }); a.setChartSize() }, reflow: function (b) {
                var g =
                    this, l = g.options.chart, e = g.renderTo, d = c(l.width) && c(l.height), B = l.width || a.getStyle(e, "width"), l = l.height || a.getStyle(e, "height"), e = b ? b.target : N; if (!d && !g.isPrinting && B && l && (e === N || e === n)) { if (B !== g.containerWidth || l !== g.containerHeight) a.clearTimeout(g.reflowTimeout), g.reflowTimeout = x(function () { g.container && g.setSize(void 0, void 0, !1) }, b ? 100 : 0); g.containerWidth = B; g.containerHeight = l }
            }, setReflow: function (a) {
                var b = this; !1 === a || this.unbindReflow ? !1 === a && this.unbindReflow && (this.unbindReflow = this.unbindReflow()) :
                    (this.unbindReflow = C(N, "resize", function (a) { b.reflow(a) }), C(this, "destroy", this.unbindReflow))
            }, setSize: function (c, g, l) {
                var e = this, B = e.renderer; e.isResizing += 1; a.setAnimation(l, e); e.oldChartHeight = e.chartHeight; e.oldChartWidth = e.chartWidth; void 0 !== c && (e.options.chart.width = c); void 0 !== g && (e.options.chart.height = g); e.getChartSize(); c = B.globalAnimation; (c ? D : p)(e.container, { width: e.chartWidth + "px", height: e.chartHeight + "px" }, c); e.setChartSize(!0); B.setSize(e.chartWidth, e.chartHeight, l); d(e.axes, function (a) {
                a.isDirty =
                    !0; a.setScale()
                }); e.isDirtyLegend = !0; e.isDirtyBox = !0; e.layOutTitles(); e.getMargins(); e.redraw(l); e.oldChartHeight = null; b(e, "resize"); x(function () { e && b(e, "endResize", null, function () { --e.isResizing }) }, E(c).duration)
            }, setChartSize: function (a) {
                var c = this.inverted, g = this.renderer, e = this.chartWidth, l = this.chartHeight, B = this.options.chart, h = this.spacing, k = this.clipOffset, m, F, x, p; this.plotLeft = m = Math.round(this.plotLeft); this.plotTop = F = Math.round(this.plotTop); this.plotWidth = x = Math.max(0, Math.round(e - m - this.marginRight));
                this.plotHeight = p = Math.max(0, Math.round(l - F - this.marginBottom)); this.plotSizeX = c ? p : x; this.plotSizeY = c ? x : p; this.plotBorderWidth = B.plotBorderWidth || 0; this.spacingBox = g.spacingBox = { x: h[3], y: h[0], width: e - h[3] - h[1], height: l - h[0] - h[2] }; this.plotBox = g.plotBox = { x: m, y: F, width: x, height: p }; e = 2 * Math.floor(this.plotBorderWidth / 2); c = Math.ceil(Math.max(e, k[3]) / 2); g = Math.ceil(Math.max(e, k[0]) / 2); this.clipBox = {
                    x: c, y: g, width: Math.floor(this.plotSizeX - Math.max(e, k[1]) / 2 - c), height: Math.max(0, Math.floor(this.plotSizeY -
                        Math.max(e, k[2]) / 2 - g))
                }; a || d(this.axes, function (a) { a.setAxisSize(); a.setAxisTranslation() }); b(this, "afterSetChartSize", { skipAxes: a })
            }, resetMargins: function () { var a = this, b = a.options.chart; d(["margin", "spacing"], function (c) { var g = b[c], e = u(g) ? g : [g, g, g, g]; d(["Top", "Right", "Bottom", "Left"], function (g, l) { a[c][l] = H(b[c + g], e[l]) }) }); d(I, function (b, c) { a[b] = H(a.margin[c], a.spacing[c]) }); a.axisOffset = [0, 0, 0, 0]; a.clipOffset = [0, 0, 0, 0] }, drawChartBox: function () {
                var a = this.options.chart, c = this.renderer, g = this.chartWidth,
                e = this.chartHeight, l = this.chartBackground, d = this.plotBackground, B = this.plotBorder, h, k = this.plotBGImage, m = a.backgroundColor, F = a.plotBackgroundColor, x = a.plotBackgroundImage, p, v = this.plotLeft, z = this.plotTop, t = this.plotWidth, f = this.plotHeight, H = this.plotBox, K = this.clipRect, u = this.clipBox, n = "animate"; l || (this.chartBackground = l = c.rect().addClass("highcharts-background").add(), n = "attr"); h = a.borderWidth || 0; p = h + (a.shadow ? 8 : 0); m = { fill: m || "none" }; if (h || l["stroke-width"]) m.stroke = a.borderColor, m["stroke-width"] =
                    h; l.attr(m).shadow(a.shadow); l[n]({ x: p / 2, y: p / 2, width: g - p - h % 2, height: e - p - h % 2, r: a.borderRadius }); n = "animate"; d || (n = "attr", this.plotBackground = d = c.rect().addClass("highcharts-plot-background").add()); d[n](H); d.attr({ fill: F || "none" }).shadow(a.plotShadow); x && (k ? k.animate(H) : this.plotBGImage = c.image(x, v, z, t, f).add()); K ? K.animate({ width: u.width, height: u.height }) : this.clipRect = c.clipRect(u); n = "animate"; B || (n = "attr", this.plotBorder = B = c.rect().addClass("highcharts-plot-border").attr({ zIndex: 1 }).add()); B.attr({
                        stroke: a.plotBorderColor,
                        "stroke-width": a.plotBorderWidth || 0, fill: "none"
                    }); B[n](B.crisp({ x: v, y: z, width: t, height: f }, -B.strokeWidth())); this.isDirtyBox = !1; b(this, "afterDrawChartBox")
            }, propFromSeries: function () { var a = this, b = a.options.chart, c, g = a.options.series, e, l; d(["inverted", "angular", "polar"], function (d) { c = B[b.type || b.defaultSeriesType]; l = b[d] || c && c.prototype[d]; for (e = g && g.length; !l && e--;)(c = B[g[e].type]) && c.prototype[d] && (l = !0); a[d] = l }) }, linkSeries: function () {
                var a = this, c = a.series; d(c, function (a) {
                    a.linkedSeries.length =
                    0
                }); d(c, function (b) { var c = b.options.linkedTo; t(c) && (c = ":previous" === c ? a.series[b.index - 1] : a.get(c)) && c.linkedParent !== b && (c.linkedSeries.push(b), b.linkedParent = c, b.visible = H(b.options.visible, c.options.visible, b.visible)) }); b(this, "afterLinkSeries")
            }, renderSeries: function () { d(this.series, function (a) { a.translate(); a.render() }) }, renderLabels: function () {
                var a = this, b = a.options.labels; b.items && d(b.items, function (c) {
                    var g = m(b.style, c.style), e = K(g.left) + a.plotLeft, l = K(g.top) + a.plotTop + 12; delete g.left; delete g.top;
                    a.renderer.text(c.html, e, l).attr({ zIndex: 2 }).css(g).add()
                })
            }, render: function () {
                var a = this.axes, b = this.renderer, c = this.options, g, e, l; this.setTitle(); this.legend = new z(this, c.legend); this.getStacks && this.getStacks(); this.getMargins(!0); this.setChartSize(); c = this.plotWidth; g = this.plotHeight = Math.max(this.plotHeight - 21, 0); d(a, function (a) { a.setScale() }); this.getAxisMargins(); e = 1.1 < c / this.plotWidth; l = 1.05 < g / this.plotHeight; if (e || l) d(a, function (a) { (a.horiz && e || !a.horiz && l) && a.setTickInterval(!0) }), this.getMargins();
                this.drawChartBox(); this.hasCartesianSeries && d(a, function (a) { a.visible && a.render() }); this.seriesGroup || (this.seriesGroup = b.g("series-group").attr({ zIndex: 3 }).add()); this.renderSeries(); this.renderLabels(); this.addCredits(); this.setResponsive && this.setResponsive(); this.hasRendered = !0
            }, addCredits: function (a) {
                var b = this; a = v(!0, this.options.credits, a); a.enabled && !this.credits && (this.credits = this.renderer.text(a.text + (this.mapCredits || ""), 0, 0).addClass("highcharts-credits").on("click", function () {
                a.href &&
                    (N.location.href = a.href)
                }).attr({ align: a.position.align, zIndex: 8 }).css(a.style).add().align(a.position), this.credits.update = function (a) { b.credits = b.credits.destroy(); b.addCredits(a) })
            }, destroy: function () {
                var c = this, g = c.axes, e = c.series, l = c.container, B, h = l && l.parentNode; b(c, "destroy"); c.renderer.forExport ? a.erase(y, c) : y[c.index] = void 0; a.chartCount--; c.renderTo.removeAttribute("data-highcharts-chart"); F(c); for (B = g.length; B--;)g[B] = g[B].destroy(); this.scroller && this.scroller.destroy && this.scroller.destroy();
                for (B = e.length; B--;)e[B] = e[B].destroy(); d("title subtitle chartBackground plotBackground plotBGImage plotBorder seriesGroup clipRect credits pointer rangeSelector legend resetZoomButton tooltip renderer".split(" "), function (a) { var b = c[a]; b && b.destroy && (c[a] = b.destroy()) }); l && (l.innerHTML = "", F(l), h && w(l)); G(c, function (a, b) { delete c[b] })
            }, firstRender: function () {
                var a = this, c = a.options; if (!a.isReadyToRender || a.isReadyToRender()) {
                    a.getContainer(); a.resetMargins(); a.setChartSize(); a.propFromSeries(); a.getAxes();
                    d(c.series || [], function (b) { a.initSeries(b) }); a.linkSeries(); b(a, "beforeRender"); l && (a.pointer = new l(a, c)); a.render(); if (!a.renderer.imgCount && a.onload) a.onload(); a.temporaryDisplay(!0)
                }
            }, onload: function () { d([this.callback].concat(this.callbacks), function (a) { a && void 0 !== this.index && a.apply(this, [this]) }, this); b(this, "load"); b(this, "render"); c(this.index) && this.setReflow(this.options.chart.reflow); this.onload = null }
        })
    })(L); (function (a) {
        var C = a.addEvent, D = a.Chart, E = a.each; C(D, "afterSetChartSize", function (q) {
            var n =
                this.options.chart.scrollablePlotArea; (n = n && n.minWidth) && !this.renderer.forExport && (this.scrollablePixels = n = Math.max(0, n - this.chartWidth)) && (this.plotWidth += n, this.clipBox.width += n, q.skipAxes || E(this.axes, function (f) { 1 === f.side ? f.getPlotLinePath = function () { var n = this.right, q; this.right = n - f.chart.scrollablePixels; q = a.Axis.prototype.getPlotLinePath.apply(this, arguments); this.right = n; return q } : (f.setAxisSize(), f.setAxisTranslation()) }))
        }); C(D, "render", function () {
            this.scrollablePixels ? (this.setUpScrolling &&
                this.setUpScrolling(), this.applyFixed()) : this.fixedDiv && this.applyFixed()
        }); D.prototype.setUpScrolling = function () { this.scrollingContainer = a.createElement("div", { className: "highcharts-scrolling" }, { overflowX: "auto", WebkitOverflowScrolling: "touch" }, this.renderTo); this.innerContainer = a.createElement("div", { className: "highcharts-inner-container" }, null, this.scrollingContainer); this.innerContainer.appendChild(this.container); this.setUpScrolling = null }; D.prototype.applyFixed = function () {
            var q = this.container,
            n, f, r = !this.fixedDiv; r && (this.fixedDiv = a.createElement("div", { className: "highcharts-fixed" }, { position: "absolute", overflow: "hidden", pointerEvents: "none", zIndex: 2 }, null, !0), this.renderTo.insertBefore(this.fixedDiv, this.renderTo.firstChild), this.fixedRenderer = n = new a.Renderer(this.fixedDiv, 0, 0), this.scrollableMask = n.path().attr({ fill: a.color(this.options.chart.backgroundColor || "#fff").setOpacity(.85).get(), zIndex: -1 }).addClass("highcharts-scrollable-mask").add(), a.each([this.inverted ? ".highcharts-xaxis" :
                ".highcharts-yaxis", this.inverted ? ".highcharts-xaxis-labels" : ".highcharts-yaxis-labels", ".highcharts-contextbutton", ".highcharts-credits", ".highcharts-legend", ".highcharts-subtitle", ".highcharts-title"], function (f) { a.each(q.querySelectorAll(f), function (a) { n.box.appendChild(a); a.style.pointerEvents = "auto" }) })); this.fixedRenderer.setSize(this.chartWidth, this.chartHeight); f = this.chartWidth + this.scrollablePixels; this.container.style.width = f + "px"; this.renderer.boxWrapper.attr({
                    width: f, height: this.chartHeight,
                    viewBox: [0, 0, f, this.chartHeight].join(" ")
                }); r && (f = this.options.chart.scrollablePlotArea, f.scrollPositionX && (this.scrollingContainer.scrollLeft = this.scrollablePixels * f.scrollPositionX)); r = this.axisOffset; f = this.plotTop - r[0] - 1; var r = this.plotTop + this.plotHeight + r[2], A = this.plotLeft + this.plotWidth - this.scrollablePixels; this.scrollableMask.attr({
                    d: this.scrollablePixels ? ["M", 0, f, "L", this.plotLeft - 1, f, "L", this.plotLeft - 1, r, "L", 0, r, "Z", "M", A, f, "L", this.chartWidth, f, "L", this.chartWidth, r, "L", A, r, "Z"] : ["M",
                        0, 0]
                })
        }
    })(L); (function (a) {
        var C, D = a.each, E = a.extend, q = a.erase, n = a.fireEvent, f = a.format, r = a.isArray, A = a.isNumber, w = a.pick, y = a.removeEvent; a.Point = C = function () { }; a.Point.prototype = {
            init: function (a, c, d) {
            this.series = a; this.color = a.color; this.applyOptions(c, d); a.options.colorByPoint ? (c = a.options.colors || a.chart.options.colors, this.color = this.color || c[a.colorCounter], c = c.length, d = a.colorCounter, a.colorCounter++ , a.colorCounter === c && (a.colorCounter = 0)) : d = a.colorIndex; this.colorIndex = w(this.colorIndex, d);
                a.chart.pointCount++; n(this, "afterInit"); return this
            }, applyOptions: function (a, c) {
                var d = this.series, m = d.options.pointValKey || d.pointValKey; a = C.prototype.optionsToObject.call(this, a); E(this, a); this.options = this.options ? E(this.options, a) : a; a.group && delete this.group; m && (this.y = this[m]); this.isNull = w(this.isValid && !this.isValid(), null === this.x || !A(this.y, !0)); this.selected && (this.state = "select"); "name" in this && void 0 === c && d.xAxis && d.xAxis.hasNames && (this.x = d.xAxis.nameToX(this)); void 0 === this.x && d && (this.x =
                    void 0 === c ? d.autoIncrement(this) : c); return this
            }, setNestedProperty: function (p, c, d) { d = d.split("."); a.reduce(d, function (d, k, b, e) { d[k] = e.length - 1 === b ? c : a.isObject(d[k], !0) ? d[k] : {}; return d[k] }, p); return p }, optionsToObject: function (p) {
                var c = {}, d = this.series, m = d.options.keys, k = m || d.pointArrayMap || ["y"], b = k.length, e = 0, h = 0; if (A(p) || null === p) c[k[0]] = p; else if (r(p)) for (!m && p.length > b && (d = typeof p[0], "string" === d ? c.name = p[0] : "number" === d && (c.x = p[0]), e++); h < b;)m && void 0 === p[e] || (0 < k[h].indexOf(".") ? a.Point.prototype.setNestedProperty(c,
                    p[e], k[h]) : c[k[h]] = p[e]), e++ , h++; else "object" === typeof p && (c = p, p.dataLabels && (d._hasPointLabels = !0), p.marker && (d._hasPointMarkers = !0)); return c
            }, getClassName: function () {
                return "highcharts-point" + (this.selected ? " highcharts-point-select" : "") + (this.negative ? " highcharts-negative" : "") + (this.isNull ? " highcharts-null-point" : "") + (void 0 !== this.colorIndex ? " highcharts-color-" + this.colorIndex : "") + (this.options.className ? " " + this.options.className : "") + (this.zone && this.zone.className ? " " + this.zone.className.replace("highcharts-negative",
                    "") : "")
            }, getZone: function () { var a = this.series, c = a.zones, a = a.zoneAxis || "y", d = 0, m; for (m = c[d]; this[a] >= m.value;)m = c[++d]; this.nonZonedColor || (this.nonZonedColor = this.color); this.color = m && m.color && !this.options.color ? m.color : this.nonZonedColor; return m }, destroy: function () {
                var a = this.series.chart, c = a.hoverPoints, d; a.pointCount--; c && (this.setState(), q(c, this), c.length || (a.hoverPoints = null)); if (this === a.hoverPoint) this.onMouseOut(); if (this.graphic || this.dataLabel) y(this), this.destroyElements(); this.legendItem &&
                    a.legend.destroyItem(this); for (d in this) this[d] = null
            }, destroyElements: function () { for (var a = ["graphic", "dataLabel", "dataLabelUpper", "connector", "shadowGroup"], c, d = 6; d--;)c = a[d], this[c] && (this[c] = this[c].destroy()) }, getLabelConfig: function () { return { x: this.category, y: this.y, color: this.color, colorIndex: this.colorIndex, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal } }, tooltipFormatter: function (a) {
                var c = this.series, d = c.tooltipOptions,
                m = w(d.valueDecimals, ""), k = d.valuePrefix || "", b = d.valueSuffix || ""; D(c.pointArrayMap || ["y"], function (c) { c = "{point." + c; if (k || b) a = a.replace(RegExp(c + "}", "g"), k + c + "}" + b); a = a.replace(RegExp(c + "}", "g"), c + ":,." + m + "f}") }); return f(a, { point: this, series: this.series }, c.chart.time)
            }, firePointEvent: function (a, c, d) {
                var m = this, k = this.series.options; (k.point.events[a] || m.options && m.options.events && m.options.events[a]) && this.importEvents(); "click" === a && k.allowPointSelect && (d = function (a) {
                m.select && m.select(null, a.ctrlKey ||
                    a.metaKey || a.shiftKey)
                }); n(this, a, c, d)
            }, visible: !0
        }
    })(L); (function (a) {
        var C = a.addEvent, D = a.animObject, E = a.arrayMax, q = a.arrayMin, n = a.correctFloat, f = a.defaultOptions, r = a.defaultPlotOptions, A = a.defined, w = a.each, y = a.erase, p = a.extend, c = a.fireEvent, d = a.grep, m = a.isArray, k = a.isNumber, b = a.isString, e = a.merge, h = a.objectEach, u = a.pick, t = a.removeEvent, z = a.splat, I = a.SVGElement, v = a.syncTimeout, G = a.win; a.Series = a.seriesType("line", null, {
            lineWidth: 2, allowPointSelect: !1, showCheckbox: !1, animation: { duration: 1E3 }, events: {},
            marker: { lineWidth: 0, lineColor: "#ffffff", enabledThreshold: 2, radius: 4, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, enabled: !0, radiusPlus: 2, lineWidthPlus: 1 }, select: { fillColor: "#cccccc", lineColor: "#000000", lineWidth: 2 } } }, point: { events: {} }, dataLabels: { align: "center", formatter: function () { return null === this.y ? "" : a.numberFormat(this.y, -1) }, style: { fontSize: "11px", fontWeight: "bold", color: "contrast", textOutline: "1px contrast" }, verticalAlign: "bottom", x: 0, y: 0, padding: 5 }, cropThreshold: 300, pointRange: 0,
            softThreshold: !0, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, lineWidthPlus: 1, marker: {}, halo: { size: 10, opacity: .25 } }, select: { marker: {} } }, stickyTracking: !0, turboThreshold: 1E3, findNearestPointBy: "x"
        }, {
            isCartesian: !0, pointClass: a.Point, sorted: !0, requireSorting: !0, directTouch: !1, axisTypes: ["xAxis", "yAxis"], colorCounter: 0, parallelArrays: ["x", "y"], coll: "series", init: function (a, b) {
                var l = this, e, d = a.series, g; l.chart = a; l.options = b = l.setOptions(b); l.linkedSeries = []; l.bindAxes(); p(l, {
                    name: b.name,
                    state: "", visible: !1 !== b.visible, selected: !0 === b.selected
                }); e = b.events; h(e, function (a, b) { C(l, b, a) }); if (e && e.click || b.point && b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0; l.getColor(); l.getSymbol(); w(l.parallelArrays, function (a) { l[a + "Data"] = [] }); l.setData(b.data, !1); l.isCartesian && (a.hasCartesianSeries = !0); d.length && (g = d[d.length - 1]); l._i = u(g && g._i, -1) + 1; a.orderSeries(this.insert(d)); c(this, "afterInit")
            }, insert: function (a) {
                var b = this.options.index, c; if (k(b)) {
                    for (c = a.length; c--;)if (b >=
                        u(a[c].options.index, a[c]._i)) { a.splice(c + 1, 0, this); break } -1 === c && a.unshift(this); c += 1
                } else a.push(this); return u(c, a.length - 1)
            }, bindAxes: function () { var b = this, c = b.options, e = b.chart, d; w(b.axisTypes || [], function (l) { w(e[l], function (a) { d = a.options; if (c[l] === d.index || void 0 !== c[l] && c[l] === d.id || void 0 === c[l] && 0 === d.index) b.insert(a.series), b[l] = a, a.isDirty = !0 }); b[l] || b.optionalAxis === l || a.error(18, !0) }) }, updateParallelArrays: function (a, b) {
                var c = a.series, l = arguments, e = k(b) ? function (g) {
                    var l = "y" === g && c.toYData ?
                        c.toYData(a) : a[g]; c[g + "Data"][b] = l
                } : function (a) { Array.prototype[b].apply(c[a + "Data"], Array.prototype.slice.call(l, 2)) }; w(c.parallelArrays, e)
            }, autoIncrement: function () {
                var a = this.options, b = this.xIncrement, c, e = a.pointIntervalUnit, d = this.chart.time, b = u(b, a.pointStart, 0); this.pointInterval = c = u(this.pointInterval, a.pointInterval, 1); e && (a = new d.Date(b), "day" === e ? d.set("Date", a, d.get("Date", a) + c) : "month" === e ? d.set("Month", a, d.get("Month", a) + c) : "year" === e && d.set("FullYear", a, d.get("FullYear", a) + c), c = a.getTime() -
                    b); this.xIncrement = b + c; return b
            }, setOptions: function (a) {
                var b = this.chart, l = b.options, d = l.plotOptions, h = (b.userOptions || {}).plotOptions || {}, g = d[this.type]; this.userOptions = a; b = e(g, d.series, a); this.tooltipOptions = e(f.tooltip, f.plotOptions.series && f.plotOptions.series.tooltip, f.plotOptions[this.type].tooltip, l.tooltip.userOptions, d.series && d.series.tooltip, d[this.type].tooltip, a.tooltip); this.stickyTracking = u(a.stickyTracking, h[this.type] && h[this.type].stickyTracking, h.series && h.series.stickyTracking,
                    this.tooltipOptions.shared && !this.noSharedTooltip ? !0 : b.stickyTracking); null === g.marker && delete b.marker; this.zoneAxis = b.zoneAxis; a = this.zones = (b.zones || []).slice(); !b.negativeColor && !b.negativeFillColor || b.zones || a.push({ value: b[this.zoneAxis + "Threshold"] || b.threshold || 0, className: "highcharts-negative", color: b.negativeColor, fillColor: b.negativeFillColor }); a.length && A(a[a.length - 1].value) && a.push({ color: this.color, fillColor: this.fillColor }); c(this, "afterSetOptions", { options: b }); return b
            }, getName: function () {
                return this.name ||
                    "Series " + (this.index + 1)
            }, getCyclic: function (a, b, c) { var e, l = this.chart, g = this.userOptions, d = a + "Index", h = a + "Counter", k = c ? c.length : u(l.options.chart[a + "Count"], l[a + "Count"]); b || (e = u(g[d], g["_" + d]), A(e) || (l.series.length || (l[h] = 0), g["_" + d] = e = l[h] % k, l[h] += 1), c && (b = c[e])); void 0 !== e && (this[d] = e); this[a] = b }, getColor: function () { this.options.colorByPoint ? this.options.color = null : this.getCyclic("color", this.options.color || r[this.type].color, this.chart.options.colors) }, getSymbol: function () {
                this.getCyclic("symbol",
                    this.options.marker.symbol, this.chart.options.symbols)
            }, drawLegendSymbol: a.LegendSymbolMixin.drawLineMarker, updateData: function (b) {
                var c = this.options, e = this.points, l = [], d, g, h, m = this.requireSorting; w(b, function (b) { var g; g = a.defined(b) && this.pointClass.prototype.optionsToObject.call({ series: this }, b).x; k(g) && (g = a.inArray(g, this.xData, h), -1 === g ? l.push(b) : b !== c.data[g] ? (e[g].update(b, !1, null, !1), e[g].touched = !0, m && (h = g)) : e[g] && (e[g].touched = !0), d = !0) }, this); if (d) for (b = e.length; b--;)g = e[b], g.touched || g.remove(!1),
                    g.touched = !1; else if (b.length === e.length) w(b, function (a, b) { e[b].update && a !== c.data[b] && e[b].update(a, !1, null, !1) }); else return !1; w(l, function (a) { this.addPoint(a, !1) }, this); return !0
            }, setData: function (c, e, d, h) {
                var l = this, g = l.points, F = g && g.length || 0, v, z = l.options, t = l.chart, f = null, p = l.xAxis, n = z.turboThreshold, H = this.xData, G = this.yData, K = (v = l.pointArrayMap) && v.length, I; c = c || []; v = c.length; e = u(e, !0); !1 !== h && v && F && !l.cropped && !l.hasGroupedData && l.visible && (I = this.updateData(c)); if (!I) {
                l.xIncrement = null; l.colorCounter =
                    0; w(this.parallelArrays, function (a) { l[a + "Data"].length = 0 }); if (n && v > n) { for (d = 0; null === f && d < v;)f = c[d], d++; if (k(f)) for (d = 0; d < v; d++)H[d] = this.autoIncrement(), G[d] = c[d]; else if (m(f)) if (K) for (d = 0; d < v; d++)f = c[d], H[d] = f[0], G[d] = f.slice(1, K + 1); else for (d = 0; d < v; d++)f = c[d], H[d] = f[0], G[d] = f[1]; else a.error(12) } else for (d = 0; d < v; d++)void 0 !== c[d] && (f = { series: l }, l.pointClass.prototype.applyOptions.apply(f, [c[d]]), l.updateParallelArrays(f, d)); G && b(G[0]) && a.error(14, !0); l.data = []; l.options.data = l.userOptions.data =
                        c; for (d = F; d--;)g[d] && g[d].destroy && g[d].destroy(); p && (p.minRange = p.userMinRange); l.isDirty = t.isDirtyBox = !0; l.isDirtyData = !!g; d = !1
                } "point" === z.legendType && (this.processData(), this.generatePoints()); e && t.redraw(d)
            }, processData: function (b) {
                var c = this.xData, e = this.yData, l = c.length, d; d = 0; var g, h, k = this.xAxis, m, v = this.options; m = v.cropThreshold; var f = this.getExtremesFromAll || v.getExtremesFromAll, z = this.isCartesian, v = k && k.val2lin, t = k && k.isLog, p = this.requireSorting, u, n; if (z && !this.isDirty && !k.isDirty && !this.yAxis.isDirty &&
                    !b) return !1; k && (b = k.getExtremes(), u = b.min, n = b.max); if (z && this.sorted && !f && (!m || l > m || this.forceCrop)) if (c[l - 1] < u || c[0] > n) c = [], e = []; else if (c[0] < u || c[l - 1] > n) d = this.cropData(this.xData, this.yData, u, n), c = d.xData, e = d.yData, d = d.start, g = !0; for (m = c.length || 1; --m;)l = t ? v(c[m]) - v(c[m - 1]) : c[m] - c[m - 1], 0 < l && (void 0 === h || l < h) ? h = l : 0 > l && p && (a.error(15), p = !1); this.cropped = g; this.cropStart = d; this.processedXData = c; this.processedYData = e; this.closestPointRange = h
            }, cropData: function (a, b, c, e, d) {
                var g = a.length, l = 0, h = g, k; d =
                    u(d, this.cropShoulder, 1); for (k = 0; k < g; k++)if (a[k] >= c) { l = Math.max(0, k - d); break } for (c = k; c < g; c++)if (a[c] > e) { h = c + d; break } return { xData: a.slice(l, h), yData: b.slice(l, h), start: l, end: h }
            }, generatePoints: function () {
                var a = this.options, b = a.data, c = this.data, e, d = this.processedXData, g = this.processedYData, h = this.pointClass, k = d.length, m = this.cropStart || 0, v, f = this.hasGroupedData, a = a.keys, t, p = [], u; c || f || (c = [], c.length = b.length, c = this.data = c); a && f && (this.options.keys = !1); for (u = 0; u < k; u++)v = m + u, f ? (t = (new h).init(this, [d[u]].concat(z(g[u]))),
                    t.dataGroup = this.groupMap[u]) : (t = c[v]) || void 0 === b[v] || (c[v] = t = (new h).init(this, b[v], d[u])), t && (t.index = v, p[u] = t); this.options.keys = a; if (c && (k !== (e = c.length) || f)) for (u = 0; u < e; u++)u !== m || f || (u += k), c[u] && (c[u].destroyElements(), c[u].plotX = void 0); this.data = c; this.points = p
            }, getExtremes: function (a) {
                var b = this.yAxis, c = this.processedXData, e, l = [], g = 0; e = this.xAxis.getExtremes(); var d = e.min, h = e.max, v, f, t = this.requireSorting ? 1 : 0, z, p; a = a || this.stackedYData || this.processedYData || []; e = a.length; for (p = 0; p < e; p++)if (f =
                    c[p], z = a[p], v = (k(z, !0) || m(z)) && (!b.positiveValuesOnly || z.length || 0 < z), f = this.getExtremesFromAll || this.options.getExtremesFromAll || this.cropped || (c[p + t] || f) >= d && (c[p - t] || f) <= h, v && f) if (v = z.length) for (; v--;)"number" === typeof z[v] && (l[g++] = z[v]); else l[g++] = z; this.dataMin = q(l); this.dataMax = E(l)
            }, translate: function () {
            this.processedXData || this.processData(); this.generatePoints(); var a = this.options, b = a.stacking, e = this.xAxis, d = e.categories, h = this.yAxis, g = this.points, m = g.length, v = !!this.modifyValue, f = a.pointPlacement,
                z = "between" === f || k(f), t = a.threshold, p = a.startFromThreshold ? t : 0, G, I, w, q, r = Number.MAX_VALUE; "between" === f && (f = .5); k(f) && (f *= u(a.pointRange || e.pointRange)); for (a = 0; a < m; a++) {
                    var y = g[a], D = y.x, C = y.y; I = y.low; var E = b && h.stacks[(this.negStacks && C < (p ? 0 : t) ? "-" : "") + this.stackKey], L; h.positiveValuesOnly && null !== C && 0 >= C && (y.isNull = !0); y.plotX = G = n(Math.min(Math.max(-1E5, e.translate(D, 0, 0, 0, 1, f, "flags" === this.type)), 1E5)); b && this.visible && !y.isNull && E && E[D] && (q = this.getStackIndicator(q, D, this.index), L = E[D], C = L.points[q.key],
                        I = C[0], C = C[1], I === p && q.key === E[D].base && (I = u(k(t) && t, h.min)), h.positiveValuesOnly && 0 >= I && (I = null), y.total = y.stackTotal = L.total, y.percentage = L.total && y.y / L.total * 100, y.stackY = C, L.setOffset(this.pointXOffset || 0, this.barW || 0)); y.yBottom = A(I) ? Math.min(Math.max(-1E5, h.translate(I, 0, 1, 0, 1)), 1E5) : null; v && (C = this.modifyValue(C, y)); y.plotY = I = "number" === typeof C && Infinity !== C ? Math.min(Math.max(-1E5, h.translate(C, 0, 1, 0, 1)), 1E5) : void 0; y.isInside = void 0 !== I && 0 <= I && I <= h.len && 0 <= G && G <= e.len; y.clientX = z ? n(e.translate(D,
                            0, 0, 0, 1, f)) : G; y.negative = y.y < (t || 0); y.category = d && void 0 !== d[y.x] ? d[y.x] : y.x; y.isNull || (void 0 !== w && (r = Math.min(r, Math.abs(G - w))), w = G); y.zone = this.zones.length && y.getZone()
                } this.closestPointRangePx = r; c(this, "afterTranslate")
            }, getValidPoints: function (a, b) { var c = this.chart; return d(a || this.points || [], function (a) { return b && !c.isInsidePlot(a.plotX, a.plotY, c.inverted) ? !1 : !a.isNull }) }, setClip: function (a) {
                var b = this.chart, c = this.options, e = b.renderer, d = b.inverted, g = this.clipBox, l = g || b.clipBox, h = this.sharedClipKey ||
                    ["_sharedClip", a && a.duration, a && a.easing, l.height, c.xAxis, c.yAxis].join(), k = b[h], m = b[h + "m"]; k || (a && (l.width = 0, d && (l.x = b.plotSizeX), b[h + "m"] = m = e.clipRect(d ? b.plotSizeX + 99 : -99, d ? -b.plotLeft : -b.plotTop, 99, d ? b.chartWidth : b.chartHeight)), b[h] = k = e.clipRect(l), k.count = { length: 0 }); a && !k.count[this.index] && (k.count[this.index] = !0, k.count.length += 1); !1 !== c.clip && (this.group.clip(a || g ? k : b.clipRect), this.markerGroup.clip(m), this.sharedClipKey = h); a || (k.count[this.index] && (delete k.count[this.index], --k.count.length),
                        0 === k.count.length && h && b[h] && (g || (b[h] = b[h].destroy()), b[h + "m"] && (b[h + "m"] = b[h + "m"].destroy())))
            }, animate: function (a) { var b = this.chart, c = D(this.options.animation), e; a ? this.setClip(c) : (e = this.sharedClipKey, (a = b[e]) && a.animate({ width: b.plotSizeX, x: 0 }, c), b[e + "m"] && b[e + "m"].animate({ width: b.plotSizeX + 99, x: 0 }, c), this.animate = null) }, afterAnimate: function () { this.setClip(); c(this, "afterAnimate"); this.finishedAnimating = !0 }, drawPoints: function () {
                var a = this.points, b = this.chart, c, e, d, g, h = this.options.marker,
                k, m, v, f = this[this.specialGroup] || this.markerGroup, t, z = u(h.enabled, this.xAxis.isRadial ? !0 : null, this.closestPointRangePx >= h.enabledThreshold * h.radius); if (!1 !== h.enabled || this._hasPointMarkers) for (c = 0; c < a.length; c++)e = a[c], g = e.graphic, k = e.marker || {}, m = !!e.marker, d = z && void 0 === k.enabled || k.enabled, v = e.isInside, d && !e.isNull ? (d = u(k.symbol, this.symbol), t = this.markerAttribs(e, e.selected && "select"), g ? g[v ? "show" : "hide"](!0).animate(t) : v && (0 < t.width || e.hasImage) && (e.graphic = g = b.renderer.symbol(d, t.x, t.y, t.width,
                    t.height, m ? k : h).add(f)), g && g.attr(this.pointAttribs(e, e.selected && "select")), g && g.addClass(e.getClassName(), !0)) : g && (e.graphic = g.destroy())
            }, markerAttribs: function (a, b) { var c = this.options.marker, e = a.marker || {}, d = e.symbol || c.symbol, g = u(e.radius, c.radius); b && (c = c.states[b], b = e.states && e.states[b], g = u(b && b.radius, c && c.radius, g + (c && c.radiusPlus || 0))); a.hasImage = d && 0 === d.indexOf("url"); a.hasImage && (g = 0); a = { x: Math.floor(a.plotX) - g, y: a.plotY - g }; g && (a.width = a.height = 2 * g); return a }, pointAttribs: function (a,
                b) { var c = this.options.marker, e = a && a.options, d = e && e.marker || {}, g = this.color, l = e && e.color, h = a && a.color, e = u(d.lineWidth, c.lineWidth); a = a && a.zone && a.zone.color; g = l || a || h || g; a = d.fillColor || c.fillColor || g; g = d.lineColor || c.lineColor || g; b && (c = c.states[b], b = d.states && d.states[b] || {}, e = u(b.lineWidth, c.lineWidth, e + u(b.lineWidthPlus, c.lineWidthPlus, 0)), a = b.fillColor || c.fillColor || a, g = b.lineColor || c.lineColor || g); return { stroke: g, "stroke-width": e, fill: a } }, destroy: function () {
                    var b = this, e = b.chart, d = /AppleWebKit\/533/.test(G.navigator.userAgent),
                    k, m, g = b.data || [], v, f; c(b, "destroy"); t(b); w(b.axisTypes || [], function (a) { (f = b[a]) && f.series && (y(f.series, b), f.isDirty = f.forceRedraw = !0) }); b.legendItem && b.chart.legend.destroyItem(b); for (m = g.length; m--;)(v = g[m]) && v.destroy && v.destroy(); b.points = null; a.clearTimeout(b.animationTimeout); h(b, function (a, b) { a instanceof I && !a.survive && (k = d && "group" === b ? "hide" : "destroy", a[k]()) }); e.hoverSeries === b && (e.hoverSeries = null); y(e.series, b); e.orderSeries(); h(b, function (a, c) { delete b[c] })
                }, getGraphPath: function (a, b,
                    c) {
                        var e = this, d = e.options, g = d.step, l, h = [], k = [], m; a = a || e.points; (l = a.reversed) && a.reverse(); (g = { right: 1, center: 2 }[g] || g && 3) && l && (g = 4 - g); !d.connectNulls || b || c || (a = this.getValidPoints(a)); w(a, function (l, B) {
                            var v = l.plotX, f = l.plotY, t = a[B - 1]; (l.leftCliff || t && t.rightCliff) && !c && (m = !0); l.isNull && !A(b) && 0 < B ? m = !d.connectNulls : l.isNull && !b ? m = !0 : (0 === B || m ? B = ["M", l.plotX, l.plotY] : e.getPointSpline ? B = e.getPointSpline(a, l, B) : g ? (B = 1 === g ? ["L", t.plotX, f] : 2 === g ? ["L", (t.plotX + v) / 2, t.plotY, "L", (t.plotX + v) / 2, f] : ["L", v,
                                t.plotY], B.push("L", v, f)) : B = ["L", v, f], k.push(l.x), g && (k.push(l.x), 2 === g && k.push(l.x)), h.push.apply(h, B), m = !1)
                        }); h.xMap = k; return e.graphPath = h
                }, drawGraph: function () {
                    var a = this, b = this.options, c = (this.gappedPath || this.getGraphPath).call(this), e = [["graph", "highcharts-graph", b.lineColor || this.color, b.dashStyle]], e = a.getZonesGraphs(e); w(e, function (e, g) {
                        var d = e[0], l = a[d]; l ? (l.endX = a.preventGraphAnimation ? null : c.xMap, l.animate({ d: c })) : c.length && (a[d] = a.chart.renderer.path(c).addClass(e[1]).attr({ zIndex: 1 }).add(a.group),
                            l = { stroke: e[2], "stroke-width": b.lineWidth, fill: a.fillGraph && a.color || "none" }, e[3] ? l.dashstyle = e[3] : "square" !== b.linecap && (l["stroke-linecap"] = l["stroke-linejoin"] = "round"), l = a[d].attr(l).shadow(2 > g && b.shadow)); l && (l.startX = c.xMap, l.isArea = c.isArea)
                    })
                }, getZonesGraphs: function (a) { w(this.zones, function (b, c) { a.push(["zone-graph-" + c, "highcharts-graph highcharts-zone-graph-" + c + " " + (b.className || ""), b.color || this.color, b.dashStyle || this.options.dashStyle]) }, this); return a }, applyZones: function () {
                    var a = this,
                    b = this.chart, c = b.renderer, e = this.zones, d, g, h = this.clips || [], k, m = this.graph, v = this.area, f = Math.max(b.chartWidth, b.chartHeight), t = this[(this.zoneAxis || "y") + "Axis"], z, p, n = b.inverted, G, I, q, r, y = !1; e.length && (m || v) && t && void 0 !== t.min && (p = t.reversed, G = t.horiz, m && !this.showLine && m.hide(), v && v.hide(), z = t.getExtremes(), w(e, function (e, l) {
                        d = p ? G ? b.plotWidth : 0 : G ? 0 : t.toPixels(z.min); d = Math.min(Math.max(u(g, d), 0), f); g = Math.min(Math.max(Math.round(t.toPixels(u(e.value, z.max), !0)), 0), f); y && (d = g = t.toPixels(z.max));
                        I = Math.abs(d - g); q = Math.min(d, g); r = Math.max(d, g); t.isXAxis ? (k = { x: n ? r : q, y: 0, width: I, height: f }, G || (k.x = b.plotHeight - k.x)) : (k = { x: 0, y: n ? r : q, width: f, height: I }, G && (k.y = b.plotWidth - k.y)); n && c.isVML && (k = t.isXAxis ? { x: 0, y: p ? q : r, height: k.width, width: b.chartWidth } : { x: k.y - b.plotLeft - b.spacingBox.x, y: 0, width: k.height, height: b.chartHeight }); h[l] ? h[l].animate(k) : (h[l] = c.clipRect(k), m && a["zone-graph-" + l].clip(h[l]), v && a["zone-area-" + l].clip(h[l])); y = e.value > z.max; a.resetZones && 0 === g && (g = void 0)
                    }), this.clips = h)
                }, invertGroups: function (a) {
                    function b() {
                        w(["group",
                            "markerGroup"], function (b) { c[b] && (e.renderer.isVML && c[b].attr({ width: c.yAxis.len, height: c.xAxis.len }), c[b].width = c.yAxis.len, c[b].height = c.xAxis.len, c[b].invert(a)) })
                    } var c = this, e = c.chart, d; c.xAxis && (d = C(e, "resize", b), C(c, "destroy", d), b(a), c.invertGroups = b)
                }, plotGroup: function (a, b, c, e, d) {
                    var g = this[a], l = !g; l && (this[a] = g = this.chart.renderer.g().attr({ zIndex: e || .1 }).add(d)); g.addClass("highcharts-" + b + " highcharts-series-" + this.index + " highcharts-" + this.type + "-series " + (A(this.colorIndex) ? "highcharts-color-" +
                        this.colorIndex + " " : "") + (this.options.className || "") + (g.hasClass("highcharts-tracker") ? " highcharts-tracker" : ""), !0); g.attr({ visibility: c })[l ? "attr" : "animate"](this.getPlotBox()); return g
                }, getPlotBox: function () { var a = this.chart, b = this.xAxis, c = this.yAxis; a.inverted && (b = c, c = this.xAxis); return { translateX: b ? b.left : a.plotLeft, translateY: c ? c.top : a.plotTop, scaleX: 1, scaleY: 1 } }, render: function () {
                    var a = this, b = a.chart, e, d = a.options, h = !!a.animate && b.renderer.isSVG && D(d.animation).duration, g = a.visible ? "inherit" :
                        "hidden", k = d.zIndex, m = a.hasRendered, t = b.seriesGroup, f = b.inverted; e = a.plotGroup("group", "series", g, k, t); a.markerGroup = a.plotGroup("markerGroup", "markers", g, k, t); h && a.animate(!0); e.inverted = a.isCartesian ? f : !1; a.drawGraph && (a.drawGraph(), a.applyZones()); a.drawDataLabels && a.drawDataLabels(); a.visible && a.drawPoints(); a.drawTracker && !1 !== a.options.enableMouseTracking && a.drawTracker(); a.invertGroups(f); !1 === d.clip || a.sharedClipKey || m || e.clip(b.clipRect); h && a.animate(); m || (a.animationTimeout = v(function () { a.afterAnimate() },
                            h)); a.isDirty = !1; a.hasRendered = !0; c(a, "afterRender")
                }, redraw: function () { var a = this.chart, b = this.isDirty || this.isDirtyData, c = this.group, e = this.xAxis, d = this.yAxis; c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({ translateX: u(e && e.left, a.plotLeft), translateY: u(d && d.top, a.plotTop) })); this.translate(); this.render(); b && delete this.kdTree }, kdAxisArray: ["clientX", "plotY"], searchPoint: function (a, b) {
                    var c = this.xAxis, e = this.yAxis, d = this.chart.inverted; return this.searchKDTree({
                        clientX: d ?
                            c.len - a.chartY + c.pos : a.chartX - c.pos, plotY: d ? e.len - a.chartX + e.pos : a.chartY - e.pos
                    }, b)
                }, buildKDTree: function () {
                    function a(c, e, g) { var d, l; if (l = c && c.length) return d = b.kdAxisArray[e % g], c.sort(function (a, b) { return a[d] - b[d] }), l = Math.floor(l / 2), { point: c[l], left: a(c.slice(0, l), e + 1, g), right: a(c.slice(l + 1), e + 1, g) } } this.buildingKdTree = !0; var b = this, c = -1 < b.options.findNearestPointBy.indexOf("y") ? 2 : 1; delete b.kdTree; v(function () { b.kdTree = a(b.getValidPoints(null, !b.directTouch), c, c); b.buildingKdTree = !1 }, b.options.kdNow ?
                        0 : 1)
                }, searchKDTree: function (a, b) {
                    function c(a, b, h, k) { var m = b.point, v = e.kdAxisArray[h % k], t, f, B = m; f = A(a[d]) && A(m[d]) ? Math.pow(a[d] - m[d], 2) : null; t = A(a[g]) && A(m[g]) ? Math.pow(a[g] - m[g], 2) : null; t = (f || 0) + (t || 0); m.dist = A(t) ? Math.sqrt(t) : Number.MAX_VALUE; m.distX = A(f) ? Math.sqrt(f) : Number.MAX_VALUE; v = a[v] - m[v]; t = 0 > v ? "left" : "right"; f = 0 > v ? "right" : "left"; b[t] && (t = c(a, b[t], h + 1, k), B = t[l] < B[l] ? t : m); b[f] && Math.sqrt(v * v) < B[l] && (a = c(a, b[f], h + 1, k), B = a[l] < B[l] ? a : B); return B } var e = this, d = this.kdAxisArray[0], g = this.kdAxisArray[1],
                        l = b ? "distX" : "dist"; b = -1 < e.options.findNearestPointBy.indexOf("y") ? 2 : 1; this.kdTree || this.buildingKdTree || this.buildKDTree(); if (this.kdTree) return c(a, this.kdTree, b, b)
                }
            })
    })(L); (function (a) {
        var C = a.Axis, D = a.Chart, E = a.correctFloat, q = a.defined, n = a.destroyObjectProperties, f = a.each, r = a.format, A = a.objectEach, w = a.pick, y = a.Series; a.StackItem = function (a, c, d, m, k) {
            var b = a.chart.inverted; this.axis = a; this.isNegative = d; this.options = c; this.x = m; this.total = null; this.points = {}; this.stack = k; this.rightCliff = this.leftCliff =
                0; this.alignOptions = { align: c.align || (b ? d ? "left" : "right" : "center"), verticalAlign: c.verticalAlign || (b ? "middle" : d ? "bottom" : "top"), y: w(c.y, b ? 4 : d ? 14 : -6), x: w(c.x, b ? d ? -6 : 6 : 0) }; this.textAlign = c.textAlign || (b ? d ? "right" : "left" : "center")
        }; a.StackItem.prototype = {
            destroy: function () { n(this, this.axis) }, render: function (a) {
                var c = this.axis.chart, d = this.options, m = d.format, m = m ? r(m, this, c.time) : d.formatter.call(this); this.label ? this.label.attr({ text: m, visibility: "hidden" }) : this.label = c.renderer.text(m, null, null, d.useHTML).css(d.style).attr({
                    align: this.textAlign,
                    rotation: d.rotation, visibility: "hidden"
                }).add(a)
            }, setOffset: function (a, c) { var d = this.axis, m = d.chart, k = d.translate(d.usePercentage ? 100 : this.total, 0, 0, 0, 1), b = d.translate(0), b = Math.abs(k - b); a = m.xAxis[0].translate(this.x) + a; d = this.getStackBox(m, this, a, k, c, b, d); if (c = this.label) c.align(this.alignOptions, null, d), d = c.alignAttr, c[!1 === this.options.crop || m.isInsidePlot(d.x, d.y) ? "show" : "hide"](!0) }, getStackBox: function (a, c, d, m, k, b, e) {
                var h = c.axis.reversed, f = a.inverted; a = e.height + e.pos - (f ? a.plotLeft : a.plotTop);
                c = c.isNegative && !h || !c.isNegative && h; return { x: f ? c ? m : m - b : d, y: f ? a - d - k : c ? a - m - b : a - m, width: f ? b : k, height: f ? k : b }
            }
        }; D.prototype.getStacks = function () { var a = this; f(a.yAxis, function (a) { a.stacks && a.hasVisibleSeries && (a.oldStacks = a.stacks) }); f(a.series, function (c) { !c.options.stacking || !0 !== c.visible && !1 !== a.options.chart.ignoreHiddenSeries || (c.stackKey = c.type + w(c.options.stack, "")) }) }; C.prototype.buildStacks = function () {
            var a = this.series, c = w(this.options.reversedStacks, !0), d = a.length, m; if (!this.isXAxis) {
            this.usePercentage =
                !1; for (m = d; m--;)a[c ? m : d - m - 1].setStackedPoints(); for (m = 0; m < d; m++)a[m].modifyStacks()
            }
        }; C.prototype.renderStackTotals = function () { var a = this.chart, c = a.renderer, d = this.stacks, m = this.stackTotalGroup; m || (this.stackTotalGroup = m = c.g("stack-labels").attr({ visibility: "visible", zIndex: 6 }).add()); m.translate(a.plotLeft, a.plotTop); A(d, function (a) { A(a, function (a) { a.render(m) }) }) }; C.prototype.resetStacks = function () {
            var a = this, c = a.stacks; a.isXAxis || A(c, function (c) {
                A(c, function (d, k) {
                d.touched < a.stacksTouched ? (d.destroy(),
                    delete c[k]) : (d.total = null, d.cumulative = null)
                })
            })
        }; C.prototype.cleanStacks = function () { var a; this.isXAxis || (this.oldStacks && (a = this.stacks = this.oldStacks), A(a, function (a) { A(a, function (a) { a.cumulative = a.total }) })) }; y.prototype.setStackedPoints = function () {
            if (this.options.stacking && (!0 === this.visible || !1 === this.chart.options.chart.ignoreHiddenSeries)) {
                var f = this.processedXData, c = this.processedYData, d = [], m = c.length, k = this.options, b = k.threshold, e = w(k.startFromThreshold && b, 0), h = k.stack, k = k.stacking, u = this.stackKey,
                t = "-" + u, z = this.negStacks, n = this.yAxis, v = n.stacks, G = n.oldStacks, l, H, r, F, B, g, x; n.stacksTouched += 1; for (B = 0; B < m; B++)g = f[B], x = c[B], l = this.getStackIndicator(l, g, this.index), F = l.key, r = (H = z && x < (e ? 0 : b)) ? t : u, v[r] || (v[r] = {}), v[r][g] || (G[r] && G[r][g] ? (v[r][g] = G[r][g], v[r][g].total = null) : v[r][g] = new a.StackItem(n, n.options.stackLabels, H, g, h)), r = v[r][g], null !== x ? (r.points[F] = r.points[this.index] = [w(r.cumulative, e)], q(r.cumulative) || (r.base = F), r.touched = n.stacksTouched, 0 < l.index && !1 === this.singleStacks && (r.points[F][0] =
                    r.points[this.index + "," + g + ",0"][0])) : r.points[F] = r.points[this.index] = null, "percent" === k ? (H = H ? u : t, z && v[H] && v[H][g] ? (H = v[H][g], r.total = H.total = Math.max(H.total, r.total) + Math.abs(x) || 0) : r.total = E(r.total + (Math.abs(x) || 0))) : r.total = E(r.total + (x || 0)), r.cumulative = w(r.cumulative, e) + (x || 0), null !== x && (r.points[F].push(r.cumulative), d[B] = r.cumulative); "percent" === k && (n.usePercentage = !0); this.stackedYData = d; n.oldStacks = {}
            }
        }; y.prototype.modifyStacks = function () {
            var a = this, c = a.stackKey, d = a.yAxis.stacks, m = a.processedXData,
            k, b = a.options.stacking; a[b + "Stacker"] && f([c, "-" + c], function (c) { for (var e = m.length, f, t; e--;)if (f = m[e], k = a.getStackIndicator(k, f, a.index, c), t = (f = d[c] && d[c][f]) && f.points[k.key]) a[b + "Stacker"](t, f, e) })
        }; y.prototype.percentStacker = function (a, c, d) { c = c.total ? 100 / c.total : 0; a[0] = E(a[0] * c); a[1] = E(a[1] * c); this.stackedYData[d] = a[1] }; y.prototype.getStackIndicator = function (a, c, d, m) { !q(a) || a.x !== c || m && a.key !== m ? a = { x: c, index: 0, key: m } : a.index++; a.key = [d, c, a.index].join(); return a }
    })(L); (function (a) {
        var C = a.addEvent,
        D = a.animate, E = a.Axis, q = a.createElement, n = a.css, f = a.defined, r = a.each, A = a.erase, w = a.extend, y = a.fireEvent, p = a.inArray, c = a.isNumber, d = a.isObject, m = a.isArray, k = a.merge, b = a.objectEach, e = a.pick, h = a.Point, u = a.Series, t = a.seriesTypes, z = a.setAnimation, I = a.splat; w(a.Chart.prototype, {
            addSeries: function (a, b, c) { var d, l = this; a && (b = e(b, !0), y(l, "addSeries", { options: a }, function () { d = l.initSeries(a); l.isDirtyLegend = !0; l.linkSeries(); y(l, "afterAddSeries"); b && l.redraw(c) })); return d }, addAxis: function (a, b, c, d) {
                var l = b ? "xAxis" :
                    "yAxis", h = this.options; a = k(a, { index: this[l].length, isX: b }); b = new E(this, a); h[l] = I(h[l] || {}); h[l].push(a); e(c, !0) && this.redraw(d); return b
            }, showLoading: function (a) {
                var b = this, c = b.options, e = b.loadingDiv, d = c.loading, h = function () { e && n(e, { left: b.plotLeft + "px", top: b.plotTop + "px", width: b.plotWidth + "px", height: b.plotHeight + "px" }) }; e || (b.loadingDiv = e = q("div", { className: "highcharts-loading highcharts-loading-hidden" }, null, b.container), b.loadingSpan = q("span", { className: "highcharts-loading-inner" }, null, e), C(b,
                    "redraw", h)); e.className = "highcharts-loading"; b.loadingSpan.innerHTML = a || c.lang.loading; n(e, w(d.style, { zIndex: 10 })); n(b.loadingSpan, d.labelStyle); b.loadingShown || (n(e, { opacity: 0, display: "" }), D(e, { opacity: d.style.opacity || .5 }, { duration: d.showDuration || 0 })); b.loadingShown = !0; h()
            }, hideLoading: function () {
                var a = this.options, b = this.loadingDiv; b && (b.className = "highcharts-loading highcharts-loading-hidden", D(b, { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function () { n(b, { display: "none" }) } }));
                this.loadingShown = !1
            }, propsRequireDirtyBox: "backgroundColor borderColor borderWidth margin marginTop marginRight marginBottom marginLeft spacing spacingTop spacingRight spacingBottom spacingLeft borderRadius plotBackgroundColor plotBackgroundImage plotBorderColor plotBorderWidth plotShadow shadow".split(" "), propsRequireUpdateSeries: "chart.inverted chart.polar chart.ignoreHiddenSeries chart.type colors plotOptions time tooltip".split(" "), update: function (a, d, l, h) {
                var m = this, t = {
                    credits: "addCredits",
                    title: "setTitle", subtitle: "setSubtitle"
                }, v = a.chart, g, z, u = []; y(m, "update", { options: a }); if (v) { k(!0, m.options.chart, v); "className" in v && m.setClassName(v.className); "reflow" in v && m.setReflow(v.reflow); if ("inverted" in v || "polar" in v || "type" in v) m.propFromSeries(), g = !0; "alignTicks" in v && (g = !0); b(v, function (a, b) { -1 !== p("chart." + b, m.propsRequireUpdateSeries) && (z = !0); -1 !== p(b, m.propsRequireDirtyBox) && (m.isDirtyBox = !0) }); "style" in v && m.renderer.setStyle(v.style) } a.colors && (this.options.colors = a.colors); a.plotOptions &&
                    k(!0, this.options.plotOptions, a.plotOptions); b(a, function (a, b) { if (m[b] && "function" === typeof m[b].update) m[b].update(a, !1); else if ("function" === typeof m[t[b]]) m[t[b]](a); "chart" !== b && -1 !== p(b, m.propsRequireUpdateSeries) && (z = !0) }); r("xAxis yAxis zAxis series colorAxis pane".split(" "), function (b) {
                        var c; a[b] && ("series" === b && (c = [], r(m[b], function (a, b) { a.options.isInternal || c.push(b) })), r(I(a[b]), function (a, e) {
                        (e = f(a.id) && m.get(a.id) || m[b][c ? c[e] : e]) && e.coll === b && (e.update(a, !1), l && (e.touched = !0)); if (!e &&
                            l) if ("series" === b) m.addSeries(a, !1).touched = !0; else if ("xAxis" === b || "yAxis" === b) m.addAxis(a, "xAxis" === b, !1).touched = !0
                        }), l && r(m[b], function (a) { a.touched || a.options.isInternal ? delete a.touched : u.push(a) }))
                    }); r(u, function (a) { a.remove(!1) }); g && r(m.axes, function (a) { a.update({}, !1) }); z && r(m.series, function (a) { a.update({}, !1) }); a.loading && k(!0, m.options.loading, a.loading); g = v && v.width; v = v && v.height; c(g) && g !== m.chartWidth || c(v) && v !== m.chartHeight ? m.setSize(g, v, h) : e(d, !0) && m.redraw(h); y(m, "afterUpdate", { options: a })
            },
            setSubtitle: function (a) { this.setTitle(void 0, a) }
        }); w(h.prototype, {
            update: function (a, b, c, h) {
                function l() {
                    k.applyOptions(a); null === k.y && g && (k.graphic = g.destroy()); d(a, !0) && (g && g.element && a && a.marker && void 0 !== a.marker.symbol && (k.graphic = g.destroy()), a && a.dataLabels && k.dataLabel && (k.dataLabel = k.dataLabel.destroy()), k.connector && (k.connector = k.connector.destroy())); f = k.index; m.updateParallelArrays(k, f); v.data[f] = d(v.data[f], !0) || d(a, !0) ? k.options : e(a, v.data[f]); m.isDirty = m.isDirtyData = !0; !m.fixedBox &&
                        m.hasCartesianSeries && (t.isDirtyBox = !0); "point" === v.legendType && (t.isDirtyLegend = !0); b && t.redraw(c)
                } var k = this, m = k.series, g = k.graphic, f, t = m.chart, v = m.options; b = e(b, !0); !1 === h ? l() : k.firePointEvent("update", { options: a }, l)
            }, remove: function (a, b) { this.series.removePoint(p(this, this.series.data), a, b) }
        }); w(u.prototype, {
            addPoint: function (a, b, c, d) {
                var l = this.options, h = this.data, k = this.chart, g = this.xAxis, g = g && g.hasNames && g.names, m = l.data, f, t, v = this.xData, z, u; b = e(b, !0); f = { series: this }; this.pointClass.prototype.applyOptions.apply(f,
                    [a]); u = f.x; z = v.length; if (this.requireSorting && u < v[z - 1]) for (t = !0; z && v[z - 1] > u;)z--; this.updateParallelArrays(f, "splice", z, 0, 0); this.updateParallelArrays(f, z); g && f.name && (g[u] = f.name); m.splice(z, 0, a); t && (this.data.splice(z, 0, null), this.processData()); "point" === l.legendType && this.generatePoints(); c && (h[0] && h[0].remove ? h[0].remove(!1) : (h.shift(), this.updateParallelArrays(f, "shift"), m.shift())); this.isDirtyData = this.isDirty = !0; b && k.redraw(d)
            }, removePoint: function (a, b, c) {
                var d = this, l = d.data, h = l[a], k = d.points,
                g = d.chart, m = function () { k && k.length === l.length && k.splice(a, 1); l.splice(a, 1); d.options.data.splice(a, 1); d.updateParallelArrays(h || { series: d }, "splice", a, 1); h && h.destroy(); d.isDirty = !0; d.isDirtyData = !0; b && g.redraw() }; z(c, g); b = e(b, !0); h ? h.firePointEvent("remove", null, m) : m()
            }, remove: function (a, b, c) { function d() { l.destroy(); h.isDirtyLegend = h.isDirtyBox = !0; h.linkSeries(); e(a, !0) && h.redraw(b) } var l = this, h = l.chart; !1 !== c ? y(l, "remove", null, d) : d() }, update: function (b, c) {
                var d = this, h = d.chart, m = d.userOptions, f =
                    d.oldType || d.type, v = b.type || m.type || h.options.chart.type, g = t[f].prototype, z, u = ["group", "markerGroup", "dataLabelsGroup"], n = ["navigatorSeries", "baseSeries"], G = d.finishedAnimating && { animation: !1 }, I = ["data", "name", "turboThreshold"], q = a.keys(b), A = 0 < q.length; r(q, function (a) { -1 === p(a, I) && (A = !1) }); if (A) b.data && this.setData(b.data, !1), b.name && this.setName(b.name, !1); else {
                        n = u.concat(n); r(n, function (a) { n[a] = d[a]; delete d[a] }); b = k(m, G, { index: d.index, pointStart: e(m.pointStart, d.xData[0]) }, { data: d.options.data },
                            b); d.remove(!1, null, !1); for (z in g) d[z] = void 0; t[v || f] ? w(d, t[v || f].prototype) : a.error(17, !0); r(n, function (a) { d[a] = n[a] }); d.init(h, b); b.zIndex !== m.zIndex && r(u, function (a) { d[a] && d[a].attr({ zIndex: b.zIndex }) }); d.oldType = f; h.linkSeries()
                    } y(this, "afterUpdate"); e(c, !0) && h.redraw(!1)
            }, setName: function (a) { this.name = this.options.name = this.userOptions.name = a; this.chart.isDirtyLegend = !0 }
        }); w(E.prototype, {
            update: function (a, c) {
                var d = this.chart, h = a && a.events || {}; a = k(this.userOptions, a); d.options[this.coll].indexOf &&
                    (d.options[this.coll][d.options[this.coll].indexOf(this.userOptions)] = a); b(d.options[this.coll].events, function (a, b) { "undefined" === typeof h[b] && (h[b] = void 0) }); this.destroy(!0); this.init(d, w(a, { events: h })); d.isDirtyBox = !0; e(c, !0) && d.redraw()
            }, remove: function (a) {
                for (var b = this.chart, c = this.coll, d = this.series, h = d.length; h--;)d[h] && d[h].remove(!1); A(b.axes, this); A(b[c], this); m(b.options[c]) ? b.options[c].splice(this.options.index, 1) : delete b.options[c]; r(b[c], function (a, b) {
                    a.options.index = a.userOptions.index =
                        b
                }); this.destroy(); b.isDirtyBox = !0; e(a, !0) && b.redraw()
            }, setTitle: function (a, b) { this.update({ title: a }, b) }, setCategories: function (a, b) { this.update({ categories: a }, b) }
        })
    })(L); (function (a) {
        var C = a.color, D = a.each, E = a.map, q = a.pick, n = a.Series, f = a.seriesType; f("area", "line", { softThreshold: !1, threshold: 0 }, {
            singleStacks: !1, getStackPoints: function (f) {
                var n = [], r = [], y = this.xAxis, p = this.yAxis, c = p.stacks[this.stackKey], d = {}, m = this.index, k = p.series, b = k.length, e, h = q(p.options.reversedStacks, !0) ? 1 : -1, u; f = f || this.points;
                if (this.options.stacking) {
                    for (u = 0; u < f.length; u++)f[u].leftNull = f[u].rightNull = null, d[f[u].x] = f[u]; a.objectEach(c, function (a, b) { null !== a.total && r.push(b) }); r.sort(function (a, b) { return a - b }); e = E(k, function () { return this.visible }); D(r, function (a, k) {
                        var f = 0, t, z; if (d[a] && !d[a].isNull) n.push(d[a]), D([-1, 1], function (l) {
                            var f = 1 === l ? "rightNull" : "leftNull", v = 0, n = c[r[k + l]]; if (n) for (u = m; 0 <= u && u < b;)t = n.points[u], t || (u === m ? d[a][f] = !0 : e[u] && (z = c[a].points[u]) && (v -= z[1] - z[0])), u += h; d[a][1 === l ? "rightCliff" : "leftCliff"] =
                                v
                        }); else { for (u = m; 0 <= u && u < b;) { if (t = c[a].points[u]) { f = t[1]; break } u += h } f = p.translate(f, 0, 1, 0, 1); n.push({ isNull: !0, plotX: y.translate(a, 0, 0, 0, 1), x: a, plotY: f, yBottom: f }) }
                    })
                } return n
            }, getGraphPath: function (a) {
                var f = n.prototype.getGraphPath, r = this.options, y = r.stacking, p = this.yAxis, c, d, m = [], k = [], b = this.index, e, h = p.stacks[this.stackKey], u = r.threshold, t = p.getThreshold(r.threshold), z, r = r.connectNulls || "percent" === y, I = function (c, d, l) {
                    var f = a[c]; c = y && h[f.x].points[b]; var z = f[l + "Null"] || 0; l = f[l + "Cliff"] || 0; var v,
                        B, f = !0; l || z ? (v = (z ? c[0] : c[1]) + l, B = c[0] + l, f = !!z) : !y && a[d] && a[d].isNull && (v = B = u); void 0 !== v && (k.push({ plotX: e, plotY: null === v ? t : p.getThreshold(v), isNull: f, isCliff: !0 }), m.push({ plotX: e, plotY: null === B ? t : p.getThreshold(B), doCurve: !1 }))
                }; a = a || this.points; y && (a = this.getStackPoints(a)); for (c = 0; c < a.length; c++)if (d = a[c].isNull, e = q(a[c].rectPlotX, a[c].plotX), z = q(a[c].yBottom, t), !d || r) r || I(c, c - 1, "left"), d && !y && r || (k.push(a[c]), m.push({ x: c, plotX: e, plotY: z })), r || I(c, c + 1, "right"); c = f.call(this, k, !0, !0); m.reversed =
                    !0; d = f.call(this, m, !0, !0); d.length && (d[0] = "L"); d = c.concat(d); f = f.call(this, k, !1, r); d.xMap = c.xMap; this.areaPath = d; return f
            }, drawGraph: function () {
            this.areaPath = []; n.prototype.drawGraph.apply(this); var a = this, f = this.areaPath, w = this.options, y = [["area", "highcharts-area", this.color, w.fillColor]]; D(this.zones, function (f, c) { y.push(["zone-area-" + c, "highcharts-area highcharts-zone-area-" + c + " " + f.className, f.color || a.color, f.fillColor || w.fillColor]) }); D(y, function (n) {
                var c = n[0], d = a[c]; d ? (d.endX = a.preventGraphAnimation ?
                    null : f.xMap, d.animate({ d: f })) : (d = a[c] = a.chart.renderer.path(f).addClass(n[1]).attr({ fill: q(n[3], C(n[2]).setOpacity(q(w.fillOpacity, .75)).get()), zIndex: 0 }).add(a.group), d.isArea = !0); d.startX = f.xMap; d.shiftUnit = w.step ? 2 : 1
            })
            }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle
        })
    })(L); (function (a) {
        var C = a.pick; a = a.seriesType; a("spline", "line", {}, {
            getPointSpline: function (a, E, q) {
                var n = E.plotX, f = E.plotY, r = a[q - 1]; q = a[q + 1]; var A, w, y, p; if (r && !r.isNull && !1 !== r.doCurve && !E.isCliff && q && !q.isNull && !1 !== q.doCurve &&
                    !E.isCliff) { a = r.plotY; y = q.plotX; q = q.plotY; var c = 0; A = (1.5 * n + r.plotX) / 2.5; w = (1.5 * f + a) / 2.5; y = (1.5 * n + y) / 2.5; p = (1.5 * f + q) / 2.5; y !== A && (c = (p - w) * (y - n) / (y - A) + f - p); w += c; p += c; w > a && w > f ? (w = Math.max(a, f), p = 2 * f - w) : w < a && w < f && (w = Math.min(a, f), p = 2 * f - w); p > q && p > f ? (p = Math.max(q, f), w = 2 * f - p) : p < q && p < f && (p = Math.min(q, f), w = 2 * f - p); E.rightContX = y; E.rightContY = p } E = ["C", C(r.rightContX, r.plotX), C(r.rightContY, r.plotY), C(A, n), C(w, f), n, f]; r.rightContX = r.rightContY = null; return E
            }
        })
    })(L); (function (a) {
        var C = a.seriesTypes.area.prototype,
        D = a.seriesType; D("areaspline", "spline", a.defaultPlotOptions.area, { getStackPoints: C.getStackPoints, getGraphPath: C.getGraphPath, drawGraph: C.drawGraph, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle })
    })(L); (function (a) {
        var C = a.animObject, D = a.color, E = a.each, q = a.extend, n = a.isNumber, f = a.merge, r = a.pick, A = a.Series, w = a.seriesType, y = a.svg; w("column", "line", {
            borderRadius: 0, crisp: !0, groupPadding: .2, marker: null, pointPadding: .1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: {
                hover: { halo: !1, brightness: .1 },
                select: { color: "#cccccc", borderColor: "#000000" }
            }, dataLabels: { align: null, verticalAlign: null, y: null }, softThreshold: !1, startFromThreshold: !0, stickyTracking: !1, tooltip: { distance: 6 }, threshold: 0, borderColor: "#ffffff"
        }, {
            cropShoulder: 0, directTouch: !0, trackerGroups: ["group", "dataLabelsGroup"], negStacks: !0, init: function () { A.prototype.init.apply(this, arguments); var a = this, c = a.chart; c.hasRendered && E(c.series, function (c) { c.type === a.type && (c.isDirty = !0) }) }, getColumnMetrics: function () {
                var a = this, c = a.options, d = a.xAxis,
                m = a.yAxis, k = d.options.reversedStacks, k = d.reversed && !k || !d.reversed && k, b, e = {}, h = 0; !1 === c.grouping ? h = 1 : E(a.chart.series, function (c) { var d = c.options, k = c.yAxis, l; c.type !== a.type || !c.visible && a.chart.options.chart.ignoreHiddenSeries || m.len !== k.len || m.pos !== k.pos || (d.stacking ? (b = c.stackKey, void 0 === e[b] && (e[b] = h++), l = e[b]) : !1 !== d.grouping && (l = h++), c.columnIndex = l) }); var f = Math.min(Math.abs(d.transA) * (d.ordinalSlope || c.pointRange || d.closestPointRange || d.tickInterval || 1), d.len), t = f * c.groupPadding, z = (f - 2 *
                    t) / (h || 1), c = Math.min(c.maxPointWidth || d.len, r(c.pointWidth, z * (1 - 2 * c.pointPadding))); a.columnMetrics = { width: c, offset: (z - c) / 2 + (t + ((a.columnIndex || 0) + (k ? 1 : 0)) * z - f / 2) * (k ? -1 : 1) }; return a.columnMetrics
            }, crispCol: function (a, c, d, m) { var k = this.chart, b = this.borderWidth, e = -(b % 2 ? .5 : 0), b = b % 2 ? .5 : 1; k.inverted && k.renderer.isVML && (b += 1); this.options.crisp && (d = Math.round(a + d) + e, a = Math.round(a) + e, d -= a); m = Math.round(c + m) + b; e = .5 >= Math.abs(c) && .5 < m; c = Math.round(c) + b; m -= c; e && m && (--c, m += 1); return { x: a, y: c, width: d, height: m } },
                translate: function () {
                    var a = this, c = a.chart, d = a.options, m = a.dense = 2 > a.closestPointRange * a.xAxis.transA, m = a.borderWidth = r(d.borderWidth, m ? 0 : 1), k = a.yAxis, b = d.threshold, e = a.translatedThreshold = k.getThreshold(b), h = r(d.minPointLength, 5), f = a.getColumnMetrics(), t = f.width, z = a.barW = Math.max(t, 1 + 2 * m), n = a.pointXOffset = f.offset; c.inverted && (e -= .5); d.pointPadding && (z = Math.ceil(z)); A.prototype.translate.apply(a); E(a.points, function (d) {
                        var m = r(d.yBottom, e), l = 999 + Math.abs(m), l = Math.min(Math.max(-l, d.plotY), k.len + l),
                        f = d.plotX + n, v = z, u = Math.min(l, m), B, g = Math.max(l, m) - u; h && Math.abs(g) < h && (g = h, B = !k.reversed && !d.negative || k.reversed && d.negative, d.y === b && a.dataMax <= b && k.min < b && (B = !B), u = Math.abs(u - e) > h ? m - h : e - (B ? h : 0)); d.barX = f; d.pointWidth = t; d.tooltipPos = c.inverted ? [k.len + k.pos - c.plotLeft - l, a.xAxis.len - f - v / 2, g] : [f + v / 2, l + k.pos - c.plotTop, g]; d.shapeType = "rect"; d.shapeArgs = a.crispCol.apply(a, d.isNull ? [f, e, v, 0] : [f, u, v, g])
                    })
                }, getSymbol: a.noop, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, drawGraph: function () {
                this.group[this.dense ?
                    "addClass" : "removeClass"]("highcharts-dense-data")
                }, pointAttribs: function (a, c) {
                    var d = this.options, m, k = this.pointAttrToOptions || {}; m = k.stroke || "borderColor"; var b = k["stroke-width"] || "borderWidth", e = a && a.color || this.color, h = a && a[m] || d[m] || this.color || e, u = a && a[b] || d[b] || this[b] || 0, k = d.dashStyle; a && this.zones.length && (e = a.getZone(), e = a.options.color || e && e.color || this.color); c && (a = f(d.states[c], a.options.states && a.options.states[c] || {}), c = a.brightness, e = a.color || void 0 !== c && D(e).brighten(a.brightness).get() ||
                        e, h = a[m] || h, u = a[b] || u, k = a.dashStyle || k); m = { fill: e, stroke: h, "stroke-width": u }; k && (m.dashstyle = k); return m
                }, drawPoints: function () {
                    var a = this, c = this.chart, d = a.options, m = c.renderer, k = d.animationLimit || 250, b; E(a.points, function (e) {
                        var h = e.graphic, u = h && c.pointCount < k ? "animate" : "attr"; if (n(e.plotY) && null !== e.y) {
                            b = e.shapeArgs; if (h) h[u](f(b)); else e.graphic = h = m[e.shapeType](b).add(e.group || a.group); d.borderRadius && h.attr({ r: d.borderRadius }); h[u](a.pointAttribs(e, e.selected && "select")).shadow(d.shadow, null,
                                d.stacking && !d.borderRadius); h.addClass(e.getClassName(), !0)
                        } else h && (e.graphic = h.destroy())
                    })
                }, animate: function (a) { var c = this, d = this.yAxis, m = c.options, k = this.chart.inverted, b = {}, e = k ? "translateX" : "translateY", h; y && (a ? (b.scaleY = .001, a = Math.min(d.pos + d.len, Math.max(d.pos, d.toPixels(m.threshold))), k ? b.translateX = a - d.len : b.translateY = a, c.group.attr(b)) : (h = c.group.attr(e), c.group.animate({ scaleY: 1 }, q(C(c.options.animation), { step: function (a, k) { b[e] = h + k.pos * (d.pos - h); c.group.attr(b) } })), c.animate = null)) },
                remove: function () { var a = this, c = a.chart; c.hasRendered && E(c.series, function (c) { c.type === a.type && (c.isDirty = !0) }); A.prototype.remove.apply(a, arguments) }
            })
    })(L); (function (a) { a = a.seriesType; a("bar", "column", null, { inverted: !0 }) })(L); (function (a) {
        var C = a.Series; a = a.seriesType; a("scatter", "line", {
            lineWidth: 0, findNearestPointBy: "xy", marker: { enabled: !0 }, tooltip: {
                headerFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e \x3cspan style\x3d"font-size: 0.85em"\x3e {series.name}\x3c/span\x3e\x3cbr/\x3e',
                pointFormat: "x: \x3cb\x3e{point.x}\x3c/b\x3e\x3cbr/\x3ey: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e"
            }
        }, { sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], takeOrdinalPosition: !1, drawGraph: function () { this.options.lineWidth && C.prototype.drawGraph.call(this) } })
    })(L); (function (a) {
        var C = a.deg2rad, D = a.isNumber, E = a.pick, q = a.relativeLength; a.CenteredSeriesMixin = {
            getCenter: function () {
                var a = this.options, f = this.chart, r = 2 * (a.slicedOffset || 0), A = f.plotWidth - 2 * r,
                f = f.plotHeight - 2 * r, w = a.center, w = [E(w[0], "50%"), E(w[1], "50%"), a.size || "100%", a.innerSize || 0], y = Math.min(A, f), p, c; for (p = 0; 4 > p; ++p)c = w[p], a = 2 > p || 2 === p && /%$/.test(c), w[p] = q(c, [A, f, y, w[2]][p]) + (a ? r : 0); w[3] > w[2] && (w[3] = w[2]); return w
            }, getStartAndEndRadians: function (a, f) { a = D(a) ? a : 0; f = D(f) && f > a && 360 > f - a ? f : a + 360; return { start: C * (a + -90), end: C * (f + -90) } }
        }
    })(L); (function (a) {
        var C = a.addEvent, D = a.CenteredSeriesMixin, E = a.defined, q = a.each, n = a.extend, f = D.getStartAndEndRadians, r = a.inArray, A = a.noop, w = a.pick, y = a.Point,
        p = a.Series, c = a.seriesType, d = a.setAnimation; c("pie", "line", { center: [null, null], clip: !1, colorByPoint: !0, dataLabels: { allowOverlap: !0, distance: 30, enabled: !0, formatter: function () { return this.point.isNull ? void 0 : this.point.name }, x: 0 }, ignoreHiddenPoint: !0, legendType: "point", marker: null, size: null, showInLegend: !1, slicedOffset: 10, stickyTracking: !1, tooltip: { followPointer: !0 }, borderColor: "#ffffff", borderWidth: 1, states: { hover: { brightness: .1 } } }, {
            isCartesian: !1, requireSorting: !1, directTouch: !0, noSharedTooltip: !0,
            trackerGroups: ["group", "dataLabelsGroup"], axisTypes: [], pointAttribs: a.seriesTypes.column.prototype.pointAttribs, animate: function (a) { var c = this, b = c.points, e = c.startAngleRad; a || (q(b, function (a) { var b = a.graphic, d = a.shapeArgs; b && (b.attr({ r: a.startR || c.center[3] / 2, start: e, end: e }), b.animate({ r: d.r, start: d.start, end: d.end }, c.options.animation)) }), c.animate = null) }, updateTotals: function () {
                var a, c = 0, b = this.points, e = b.length, d, f = this.options.ignoreHiddenPoint; for (a = 0; a < e; a++)d = b[a], c += f && !d.visible ? 0 : d.isNull ?
                    0 : d.y; this.total = c; for (a = 0; a < e; a++)d = b[a], d.percentage = 0 < c && (d.visible || !f) ? d.y / c * 100 : 0, d.total = c
            }, generatePoints: function () { p.prototype.generatePoints.call(this); this.updateTotals() }, translate: function (a) {
                this.generatePoints(); var c = 0, b = this.options, e = b.slicedOffset, d = e + (b.borderWidth || 0), m, t, z, n = f(b.startAngle, b.endAngle), v = this.startAngleRad = n.start, n = (this.endAngleRad = n.end) - v, p = this.points, l, r = b.dataLabels.distance, b = b.ignoreHiddenPoint, q, F = p.length, B; a || (this.center = a = this.getCenter()); this.getX =
                    function (b, c, e) { z = Math.asin(Math.min((b - a[1]) / (a[2] / 2 + e.labelDistance), 1)); return a[0] + (c ? -1 : 1) * Math.cos(z) * (a[2] / 2 + e.labelDistance) }; for (q = 0; q < F; q++) {
                        B = p[q]; B.labelDistance = w(B.options.dataLabels && B.options.dataLabels.distance, r); this.maxLabelDistance = Math.max(this.maxLabelDistance || 0, B.labelDistance); m = v + c * n; if (!b || B.visible) c += B.percentage / 100; t = v + c * n; B.shapeType = "arc"; B.shapeArgs = { x: a[0], y: a[1], r: a[2] / 2, innerR: a[3] / 2, start: Math.round(1E3 * m) / 1E3, end: Math.round(1E3 * t) / 1E3 }; z = (t + m) / 2; z > 1.5 * Math.PI ?
                            z -= 2 * Math.PI : z < -Math.PI / 2 && (z += 2 * Math.PI); B.slicedTranslation = { translateX: Math.round(Math.cos(z) * e), translateY: Math.round(Math.sin(z) * e) }; t = Math.cos(z) * a[2] / 2; l = Math.sin(z) * a[2] / 2; B.tooltipPos = [a[0] + .7 * t, a[1] + .7 * l]; B.half = z < -Math.PI / 2 || z > Math.PI / 2 ? 1 : 0; B.angle = z; m = Math.min(d, B.labelDistance / 5); B.labelPos = [a[0] + t + Math.cos(z) * B.labelDistance, a[1] + l + Math.sin(z) * B.labelDistance, a[0] + t + Math.cos(z) * m, a[1] + l + Math.sin(z) * m, a[0] + t, a[1] + l, 0 > B.labelDistance ? "center" : B.half ? "right" : "left", z]
                    }
            }, drawGraph: null,
            drawPoints: function () {
                var a = this, c = a.chart.renderer, b, e, d, f, t = a.options.shadow; t && !a.shadowGroup && (a.shadowGroup = c.g("shadow").add(a.group)); q(a.points, function (h) {
                    e = h.graphic; if (h.isNull) e && (h.graphic = e.destroy()); else {
                        f = h.shapeArgs; b = h.getTranslate(); var k = h.shadowGroup; t && !k && (k = h.shadowGroup = c.g("shadow").add(a.shadowGroup)); k && k.attr(b); d = a.pointAttribs(h, h.selected && "select"); e ? e.setRadialReference(a.center).attr(d).animate(n(f, b)) : (h.graphic = e = c[h.shapeType](f).setRadialReference(a.center).attr(b).add(a.group),
                            e.attr(d).attr({ "stroke-linejoin": "round" }).shadow(t, k)); e.attr({ visibility: h.visible ? "inherit" : "hidden" }); e.addClass(h.getClassName())
                    }
                })
            }, searchPoint: A, sortByAngle: function (a, c) { a.sort(function (a, e) { return void 0 !== a.angle && (e.angle - a.angle) * c }) }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, getCenter: D.getCenter, getSymbol: A
        }, {
            init: function () {
                y.prototype.init.apply(this, arguments); var a = this, c; a.name = w(a.name, "Slice"); c = function (b) { a.slice("select" === b.type) }; C(a, "select", c); C(a, "unselect",
                    c); return a
            }, isValid: function () { return a.isNumber(this.y, !0) && 0 <= this.y }, setVisible: function (a, c) { var b = this, e = b.series, d = e.chart, k = e.options.ignoreHiddenPoint; c = w(c, k); a !== b.visible && (b.visible = b.options.visible = a = void 0 === a ? !b.visible : a, e.options.data[r(b, e.data)] = b.options, q(["graphic", "dataLabel", "connector", "shadowGroup"], function (c) { if (b[c]) b[c][a ? "show" : "hide"](!0) }), b.legendItem && d.legend.colorizeItem(b, a), a || "hover" !== b.state || b.setState(""), k && (e.isDirty = !0), c && d.redraw()) }, slice: function (a,
                c, b) { var e = this.series; d(b, e.chart); w(c, !0); this.sliced = this.options.sliced = E(a) ? a : !this.sliced; e.options.data[r(this, e.data)] = this.options; this.graphic.animate(this.getTranslate()); this.shadowGroup && this.shadowGroup.animate(this.getTranslate()) }, getTranslate: function () { return this.sliced ? this.slicedTranslation : { translateX: 0, translateY: 0 } }, haloPath: function (a) {
                    var c = this.shapeArgs; return this.sliced || !this.visible ? [] : this.series.chart.renderer.symbols.arc(c.x, c.y, c.r + a, c.r + a, {
                        innerR: this.shapeArgs.r -
                            1, start: c.start, end: c.end
                    })
                }
            })
    })(L); (function (a) {
        var C = a.addEvent, D = a.arrayMax, E = a.defined, q = a.each, n = a.extend, f = a.format, r = a.map, A = a.merge, w = a.noop, y = a.pick, p = a.relativeLength, c = a.Series, d = a.seriesTypes, m = a.some, k = a.stableSort; a.distribute = function (b, c, d) {
            function e(a, b) { return a.target - b.target } var h, f = !0, n = b, v = [], p; p = 0; var l = n.reducedLen || c; for (h = b.length; h--;)p += b[h].size; if (p > l) { k(b, function (a, b) { return (b.rank || 0) - (a.rank || 0) }); for (p = h = 0; p <= l;)p += b[h].size, h++; v = b.splice(h - 1, b.length) } k(b, e);
            for (b = r(b, function (a) { return { size: a.size, targets: [a.target], align: y(a.align, .5) } }); f;) { for (h = b.length; h--;)f = b[h], p = (Math.min.apply(0, f.targets) + Math.max.apply(0, f.targets)) / 2, f.pos = Math.min(Math.max(0, p - f.size * f.align), c - f.size); h = b.length; for (f = !1; h--;)0 < h && b[h - 1].pos + b[h - 1].size > b[h].pos && (b[h - 1].size += b[h].size, b[h - 1].targets = b[h - 1].targets.concat(b[h].targets), b[h - 1].align = .5, b[h - 1].pos + b[h - 1].size > c && (b[h - 1].pos = c - b[h - 1].size), b.splice(h, 1), f = !0) } n.push.apply(n, v); h = 0; m(b, function (b) {
                var e =
                    0; if (m(b.targets, function () { n[h].pos = b.pos + e; if (Math.abs(n[h].pos - n[h].target) > d) return q(n.slice(0, h + 1), function (a) { delete a.pos }), n.reducedLen = (n.reducedLen || c) - .1 * c, n.reducedLen > .1 * c && a.distribute(n, c, d), !0; e += n[h].size; h++ })) return !0
            }); k(n, e)
        }; c.prototype.drawDataLabels = function () {
            function b(a, b) { var c = b.filter; return c ? (b = c.operator, a = a[c.property], c = c.value, "\x3e" === b && a > c || "\x3c" === b && a < c || "\x3e\x3d" === b && a >= c || "\x3c\x3d" === b && a <= c || "\x3d\x3d" === b && a == c || "\x3d\x3d\x3d" === b && a === c ? !0 : !1) : !0 }
            var c = this, d = c.chart, k = c.options, m = k.dataLabels, z = c.points, n, v, p = c.hasRendered || 0, l, r, w = y(m.defer, !!k.animation), F = d.renderer; if (m.enabled || c._hasPointLabels) c.dlProcessOptions && c.dlProcessOptions(m), r = c.plotGroup("dataLabelsGroup", "data-labels", w && !p ? "hidden" : "visible", m.zIndex || 6), w && (r.attr({ opacity: +p }), p || C(c, "afterAnimate", function () { c.visible && r.show(!0); r[k.animation ? "animate" : "attr"]({ opacity: 1 }, { duration: 200 }) })), v = m, q(z, function (e) {
                var g, h = e.dataLabel, z, t, B = e.connector, u = !h, p; n = e.dlOptions ||
                    e.options && e.options.dataLabels; (g = y(n && n.enabled, v.enabled) && !e.isNull) && (g = !0 === b(e, n || m)); g && (m = A(v, n), z = e.getLabelConfig(), p = m[e.formatPrefix + "Format"] || m.format, l = E(p) ? f(p, z, d.time) : (m[e.formatPrefix + "Formatter"] || m.formatter).call(z, m), p = m.style, z = m.rotation, p.color = y(m.color, p.color, c.color, "#000000"), "contrast" === p.color && (e.contrastColor = F.getContrast(e.color || c.color), p.color = m.inside || 0 > y(e.labelDistance, m.distance) || k.stacking ? e.contrastColor : "#000000"), k.cursor && (p.cursor = k.cursor), t =
                        { fill: m.backgroundColor, stroke: m.borderColor, "stroke-width": m.borderWidth, r: m.borderRadius || 0, rotation: z, padding: m.padding, zIndex: 1 }, a.objectEach(t, function (a, b) { void 0 === a && delete t[b] })); !h || g && E(l) ? g && E(l) && (h ? t.text = l : (h = e.dataLabel = z ? F.text(l, 0, -9999).addClass("highcharts-data-label") : F.label(l, 0, -9999, m.shape, null, null, m.useHTML, null, "data-label"), h.addClass(" highcharts-data-label-color-" + e.colorIndex + " " + (m.className || "") + (m.useHTML ? " highcharts-tracker" : ""))), h.attr(t), h.css(p).shadow(m.shadow),
                            h.added || h.add(r), c.alignDataLabel(e, h, m, null, u)) : (e.dataLabel = h = h.destroy(), B && (e.connector = B.destroy()))
            }); a.fireEvent(this, "afterDrawDataLabels")
        }; c.prototype.alignDataLabel = function (a, c, d, k, m) {
            var b = this.chart, e = b.inverted, h = y(a.dlBox && a.dlBox.centerX, a.plotX, -9999), f = y(a.plotY, -9999), l = c.getBBox(), t, u = d.rotation, p = d.align, B = this.visible && (a.series.forceDL || b.isInsidePlot(h, Math.round(f), e) || k && b.isInsidePlot(h, e ? k.x + 1 : k.y + k.height - 1, e)), g = "justify" === y(d.overflow, "justify"); if (B && (t = d.style.fontSize,
                t = b.renderer.fontMetrics(t, c).b, k = n({ x: e ? this.yAxis.len - f : h, y: Math.round(e ? this.xAxis.len - h : f), width: 0, height: 0 }, k), n(d, { width: l.width, height: l.height }), u ? (g = !1, h = b.renderer.rotCorr(t, u), h = { x: k.x + d.x + k.width / 2 + h.x, y: k.y + d.y + { top: 0, middle: .5, bottom: 1 }[d.verticalAlign] * k.height }, c[m ? "attr" : "animate"](h).attr({ align: p }), f = (u + 720) % 360, f = 180 < f && 360 > f, "left" === p ? h.y -= f ? l.height : 0 : "center" === p ? (h.x -= l.width / 2, h.y -= l.height / 2) : "right" === p && (h.x -= l.width, h.y -= f ? 0 : l.height), c.placed = !0, c.alignAttr = h) : (c.align(d,
                    null, k), h = c.alignAttr), g ? a.isLabelJustified = this.justifyDataLabel(c, d, h, l, k, m) : y(d.crop, !0) && (B = b.isInsidePlot(h.x, h.y) && b.isInsidePlot(h.x + l.width, h.y + l.height)), d.shape && !u)) c[m ? "attr" : "animate"]({ anchorX: e ? b.plotWidth - a.plotY : a.plotX, anchorY: e ? b.plotHeight - a.plotX : a.plotY }); B || (c.attr({ y: -9999 }), c.placed = !1)
        }; c.prototype.justifyDataLabel = function (a, c, d, k, f, m) {
            var b = this.chart, e = c.align, h = c.verticalAlign, l, z, t = a.box ? 0 : a.padding || 0; l = d.x + t; 0 > l && ("right" === e ? c.align = "left" : c.x = -l, z = !0); l = d.x + k.width -
                t; l > b.plotWidth && ("left" === e ? c.align = "right" : c.x = b.plotWidth - l, z = !0); l = d.y + t; 0 > l && ("bottom" === h ? c.verticalAlign = "top" : c.y = -l, z = !0); l = d.y + k.height - t; l > b.plotHeight && ("top" === h ? c.verticalAlign = "bottom" : c.y = b.plotHeight - l, z = !0); z && (a.placed = !m, a.align(c, null, f)); return z
        }; d.pie && (d.pie.prototype.drawDataLabels = function () {
            var b = this, d = b.data, h, k = b.chart, f = b.options.dataLabels, m = y(f.connectorPadding, 10), n = y(f.connectorWidth, 1), v = k.plotWidth, p = k.plotHeight, l = Math.round(k.chartWidth / 3), r, w = b.center, F = w[2] /
                2, B = w[1], g, x, A, O, M = [[], []], C, P, J, T, S = [0, 0, 0, 0]; b.visible && (f.enabled || b._hasPointLabels) && (q(d, function (a) { a.dataLabel && a.visible && a.dataLabel.shortened && (a.dataLabel.attr({ width: "auto" }).css({ width: "auto", textOverflow: "clip" }), a.dataLabel.shortened = !1) }), c.prototype.drawDataLabels.apply(b), q(d, function (a) {
                a.dataLabel && (a.visible ? (M[a.half].push(a), a.dataLabel._pos = null, !E(f.style.width) && !E(a.options.dataLabels && a.options.dataLabels.style && a.options.dataLabels.style.width) && a.dataLabel.getBBox().width >
                    l && (a.dataLabel.css({ width: .7 * l }), a.dataLabel.shortened = !0)) : a.dataLabel = a.dataLabel.destroy())
                }), q(M, function (c, d) {
                    var e, l, z = c.length, t = [], n; if (z) for (b.sortByAngle(c, d - .5), 0 < b.maxLabelDistance && (e = Math.max(0, B - F - b.maxLabelDistance), l = Math.min(B + F + b.maxLabelDistance, k.plotHeight), q(c, function (a) {
                    0 < a.labelDistance && a.dataLabel && (a.top = Math.max(0, B - F - a.labelDistance), a.bottom = Math.min(B + F + a.labelDistance, k.plotHeight), n = a.dataLabel.getBBox().height || 21, a.distributeBox = {
                        target: a.labelPos[1] - a.top + n /
                            2, size: n, rank: a.y
                    }, t.push(a.distributeBox))
                    }), e = l + n - e, a.distribute(t, e, e / 5)), T = 0; T < z; T++)h = c[T], A = h.labelPos, g = h.dataLabel, J = !1 === h.visible ? "hidden" : "inherit", P = e = A[1], t && E(h.distributeBox) && (void 0 === h.distributeBox.pos ? J = "hidden" : (O = h.distributeBox.size, P = h.top + h.distributeBox.pos)), delete h.positionIndex, C = f.justify ? w[0] + (d ? -1 : 1) * (F + h.labelDistance) : b.getX(P < h.top + 2 || P > h.bottom - 2 ? e : P, d, h), g._attr = { visibility: J, align: A[6] }, g._pos = { x: C + f.x + ({ left: m, right: -m }[A[6]] || 0), y: P + f.y - 10 }, A.x = C, A.y = P, y(f.crop,
                        !0) && (x = g.getBBox().width, e = null, C - x < m && 1 === d ? (e = Math.round(x - C + m), S[3] = Math.max(e, S[3])) : C + x > v - m && 0 === d && (e = Math.round(C + x - v + m), S[1] = Math.max(e, S[1])), 0 > P - O / 2 ? S[0] = Math.max(Math.round(-P + O / 2), S[0]) : P + O / 2 > p && (S[2] = Math.max(Math.round(P + O / 2 - p), S[2])), g.sideOverflow = e)
                }), 0 === D(S) || this.verifyDataLabelOverflow(S)) && (this.placeDataLabels(), n && q(this.points, function (a) {
                    var c; r = a.connector; if ((g = a.dataLabel) && g._pos && a.visible && 0 < a.labelDistance) {
                        J = g._attr.visibility; if (c = !r) a.connector = r = k.renderer.path().addClass("highcharts-data-label-connector  highcharts-color-" +
                            a.colorIndex + (a.className ? " " + a.className : "")).add(b.dataLabelsGroup), r.attr({ "stroke-width": n, stroke: f.connectorColor || a.color || "#666666" }); r[c ? "attr" : "animate"]({ d: b.connectorPath(a.labelPos) }); r.attr("visibility", J)
                    } else r && (a.connector = r.destroy())
                }))
        }, d.pie.prototype.connectorPath = function (a) {
            var b = a.x, c = a.y; return y(this.options.dataLabels.softConnector, !0) ? ["M", b + ("left" === a[6] ? 5 : -5), c, "C", b, c, 2 * a[2] - a[4], 2 * a[3] - a[5], a[2], a[3], "L", a[4], a[5]] : ["M", b + ("left" === a[6] ? 5 : -5), c, "L", a[2], a[3], "L",
                a[4], a[5]]
        }, d.pie.prototype.placeDataLabels = function () { q(this.points, function (a) { var b = a.dataLabel; b && a.visible && ((a = b._pos) ? (b.sideOverflow && (b._attr.width = b.getBBox().width - b.sideOverflow, b.css({ width: b._attr.width + "px", textOverflow: this.options.dataLabels.style.textOverflow || "ellipsis" }), b.shortened = !0), b.attr(b._attr), b[b.moved ? "animate" : "attr"](a), b.moved = !0) : b && b.attr({ y: -9999 })) }, this) }, d.pie.prototype.alignDataLabel = w, d.pie.prototype.verifyDataLabelOverflow = function (a) {
            var b = this.center, c =
                this.options, d = c.center, k = c.minSize || 80, f, m = null !== c.size; m || (null !== d[0] ? f = Math.max(b[2] - Math.max(a[1], a[3]), k) : (f = Math.max(b[2] - a[1] - a[3], k), b[0] += (a[3] - a[1]) / 2), null !== d[1] ? f = Math.max(Math.min(f, b[2] - Math.max(a[0], a[2])), k) : (f = Math.max(Math.min(f, b[2] - a[0] - a[2]), k), b[1] += (a[0] - a[2]) / 2), f < b[2] ? (b[2] = f, b[3] = Math.min(p(c.innerSize || 0, f), f), this.translate(b), this.drawDataLabels && this.drawDataLabels()) : m = !0); return m
        }); d.column && (d.column.prototype.alignDataLabel = function (a, d, h, k, f) {
            var b = this.chart.inverted,
            e = a.series, m = a.dlBox || a.shapeArgs, n = y(a.below, a.plotY > y(this.translatedThreshold, e.yAxis.len)), l = y(h.inside, !!this.options.stacking); m && (k = A(m), 0 > k.y && (k.height += k.y, k.y = 0), m = k.y + k.height - e.yAxis.len, 0 < m && (k.height -= m), b && (k = { x: e.yAxis.len - k.y - k.height, y: e.xAxis.len - k.x - k.width, width: k.height, height: k.width }), l || (b ? (k.x += n ? 0 : k.width, k.width = 0) : (k.y += n ? k.height : 0, k.height = 0))); h.align = y(h.align, !b || l ? "center" : n ? "right" : "left"); h.verticalAlign = y(h.verticalAlign, b || l ? "middle" : n ? "top" : "bottom"); c.prototype.alignDataLabel.call(this,
                a, d, h, k, f); a.isLabelJustified && a.contrastColor && a.dataLabel.css({ color: a.contrastColor })
        })
    })(L); (function (a) {
        var C = a.Chart, D = a.each, E = a.objectEach, q = a.pick; a = a.addEvent; a(C, "render", function () {
            var a = []; D(this.labelCollectors || [], function (f) { a = a.concat(f()) }); D(this.yAxis || [], function (f) { f.options.stackLabels && !f.options.stackLabels.allowOverlap && E(f.stacks, function (f) { E(f, function (f) { a.push(f.label) }) }) }); D(this.series || [], function (f) {
                var n = f.options.dataLabels, A = f.dataLabelCollections || ["dataLabel"];
                (n.enabled || f._hasPointLabels) && !n.allowOverlap && f.visible && D(A, function (n) { D(f.points, function (f) { f[n] && (f[n].labelrank = q(f.labelrank, f.shapeArgs && f.shapeArgs.height), a.push(f[n])) }) })
            }); this.hideOverlappingLabels(a)
        }); C.prototype.hideOverlappingLabels = function (a) {
            var f = a.length, n, q, w, y, p, c, d = function (a, c, b, d, h, f, n, z) { return !(h > a + b || h + n < a || f > c + d || f + z < c) }; w = function (a) {
                var c, b, d, h = 2 * (a.box ? 0 : a.padding || 0); if (a && (!a.alignAttr || a.placed)) return c = a.alignAttr || { x: a.attr("x"), y: a.attr("y") }, b = a.parentGroup,
                    a.width || (d = a.getBBox(), a.width = d.width, a.height = d.height), { x: c.x + (b.translateX || 0), y: c.y + (b.translateY || 0), width: a.width - h, height: a.height - h }
            }; for (q = 0; q < f; q++)if (n = a[q]) n.oldOpacity = n.opacity, n.newOpacity = 1, n.absoluteBox = w(n); a.sort(function (a, c) { return (c.labelrank || 0) - (a.labelrank || 0) }); for (q = 0; q < f; q++)for (c = (w = a[q]) && w.absoluteBox, n = q + 1; n < f; ++n)if (p = (y = a[n]) && y.absoluteBox, c && p && w !== y && 0 !== w.newOpacity && 0 !== y.newOpacity && (p = d(c.x, c.y, c.width, c.height, p.x, p.y, p.width, p.height))) (w.labelrank < y.labelrank ?
                w : y).newOpacity = 0; D(a, function (a) { var c, b; a && (b = a.newOpacity, a.oldOpacity !== b && (a.alignAttr && a.placed ? (b ? a.show(!0) : c = function () { a.hide() }, a.alignAttr.opacity = b, a[a.isOld ? "animate" : "attr"](a.alignAttr, null, c)) : a.attr({ opacity: b })), a.isOld = !0) })
        }
    })(L); (function (a) {
        var C = a.addEvent, D = a.Chart, E = a.createElement, q = a.css, n = a.defaultOptions, f = a.defaultPlotOptions, r = a.each, A = a.extend, w = a.fireEvent, y = a.hasTouch, p = a.inArray, c = a.isObject, d = a.Legend, m = a.merge, k = a.pick, b = a.Point, e = a.Series, h = a.seriesTypes, u = a.svg,
        t; t = a.TrackerMixin = {
            drawTrackerPoint: function () {
                var a = this, b = a.chart.pointer, c = function (a) { var c = b.getPointFromEvent(a); void 0 !== c && (b.isDirectTouch = !0, c.onMouseOver(a)) }; r(a.points, function (a) { a.graphic && (a.graphic.element.point = a); a.dataLabel && (a.dataLabel.div ? a.dataLabel.div.point = a : a.dataLabel.element.point = a) }); a._hasTracking || (r(a.trackerGroups, function (d) {
                    if (a[d]) {
                        a[d].addClass("highcharts-tracker").on("mouseover", c).on("mouseout", function (a) { b.onTrackerMouseOut(a) }); if (y) a[d].on("touchstart",
                            c); a.options.cursor && a[d].css(q).css({ cursor: a.options.cursor })
                    }
                }), a._hasTracking = !0); w(this, "afterDrawTracker")
            }, drawTrackerGraph: function () {
                var a = this, b = a.options, c = b.trackByArea, d = [].concat(c ? a.areaPath : a.graphPath), e = d.length, h = a.chart, k = h.pointer, f = h.renderer, m = h.options.tooltip.snap, g = a.tracker, n, t = function () { if (h.hoverSeries !== a) a.onMouseOver() }, p = "rgba(192,192,192," + (u ? .0001 : .002) + ")"; if (e && !c) for (n = e + 1; n--;)"M" === d[n] && d.splice(n + 1, 0, d[n + 1] - m, d[n + 2], "L"), (n && "M" === d[n] || n === e) && d.splice(n,
                    0, "L", d[n - 2] + m, d[n - 1]); g ? g.attr({ d: d }) : a.graph && (a.tracker = f.path(d).attr({ "stroke-linejoin": "round", visibility: a.visible ? "visible" : "hidden", stroke: p, fill: c ? p : "none", "stroke-width": a.graph.strokeWidth() + (c ? 0 : 2 * m), zIndex: 2 }).add(a.group), r([a.tracker, a.markerGroup], function (a) { a.addClass("highcharts-tracker").on("mouseover", t).on("mouseout", function (a) { k.onTrackerMouseOut(a) }); b.cursor && a.css({ cursor: b.cursor }); if (y) a.on("touchstart", t) })); w(this, "afterDrawTracker")
            }
        }; h.column && (h.column.prototype.drawTracker =
            t.drawTrackerPoint); h.pie && (h.pie.prototype.drawTracker = t.drawTrackerPoint); h.scatter && (h.scatter.prototype.drawTracker = t.drawTrackerPoint); A(d.prototype, {
                setItemEvents: function (a, c, d) {
                    var e = this, h = e.chart.renderer.boxWrapper, k = "highcharts-legend-" + (a instanceof b ? "point" : "series") + "-active"; (d ? c : a.legendGroup).on("mouseover", function () { a.setState("hover"); h.addClass(k); c.css(e.options.itemHoverStyle) }).on("mouseout", function () { c.css(m(a.visible ? e.itemStyle : e.itemHiddenStyle)); h.removeClass(k); a.setState() }).on("click",
                        function (b) { var c = function () { a.setVisible && a.setVisible() }; h.removeClass(k); b = { browserEvent: b }; a.firePointEvent ? a.firePointEvent("legendItemClick", b, c) : w(a, "legendItemClick", b, c) })
                }, createCheckboxForItem: function (a) { a.checkbox = E("input", { type: "checkbox", checked: a.selected, defaultChecked: a.selected }, this.options.itemCheckboxStyle, this.chart.container); C(a.checkbox, "click", function (b) { w(a.series || a, "checkboxClick", { checked: b.target.checked, item: a }, function () { a.select() }) }) }
            }); n.legend.itemStyle.cursor =
                "pointer"; A(D.prototype, {
                    showResetZoom: function () { function a() { b.zoomOut() } var b = this, c = n.lang, d = b.options.chart.resetZoomButton, e = d.theme, h = e.states, k = "chart" === d.relativeTo ? null : "plotBox"; w(this, "beforeShowResetZoom", null, function () { b.resetZoomButton = b.renderer.button(c.resetZoom, null, null, a, e, h && h.hover).attr({ align: d.position.align, title: c.resetZoomTitle }).addClass("highcharts-reset-zoom").add().align(d.position, !1, k) }) }, zoomOut: function () { w(this, "selection", { resetSelection: !0 }, this.zoom) }, zoom: function (a) {
                        var b,
                        d = this.pointer, e = !1, h; !a || a.resetSelection ? (r(this.axes, function (a) { b = a.zoom() }), d.initiated = !1) : r(a.xAxis.concat(a.yAxis), function (a) { var c = a.axis; d[c.isXAxis ? "zoomX" : "zoomY"] && (b = c.zoom(a.min, a.max), c.displayBtn && (e = !0)) }); h = this.resetZoomButton; e && !h ? this.showResetZoom() : !e && c(h) && (this.resetZoomButton = h.destroy()); b && this.redraw(k(this.options.chart.animation, a && a.animation, 100 > this.pointCount))
                    }, pan: function (a, b) {
                        var c = this, d = c.hoverPoints, e; d && r(d, function (a) { a.setState() }); r("xy" === b ? [1, 0] :
                            [1], function (b) {
                                b = c[b ? "xAxis" : "yAxis"][0]; var d = b.horiz, h = a[d ? "chartX" : "chartY"], d = d ? "mouseDownX" : "mouseDownY", l = c[d], g = (b.pointRange || 0) / 2, k = b.reversed && !c.inverted || !b.reversed && c.inverted ? -1 : 1, f = b.getExtremes(), m = b.toValue(l - h, !0) + g * k, k = b.toValue(l + b.len - h, !0) - g * k, n = k < m, l = n ? k : m, m = n ? m : k, k = Math.min(f.dataMin, g ? f.min : b.toValue(b.toPixels(f.min) - b.minPixelPadding)), g = Math.max(f.dataMax, g ? f.max : b.toValue(b.toPixels(f.max) + b.minPixelPadding)), n = k - l; 0 < n && (m += n, l = k); n = m - g; 0 < n && (m = g, l -= n); b.series.length &&
                                    l !== f.min && m !== f.max && (b.setExtremes(l, m, !1, !1, { trigger: "pan" }), e = !0); c[d] = h
                            }); e && c.redraw(!1); q(c.container, { cursor: "move" })
                    }
                }); A(b.prototype, {
                    select: function (a, b) {
                        var c = this, d = c.series, e = d.chart; a = k(a, !c.selected); c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function () {
                        c.selected = c.options.selected = a; d.options.data[p(c, d.data)] = c.options; c.setState(a && "select"); b || r(e.getSelectedPoints(), function (a) {
                        a.selected && a !== c && (a.selected = a.options.selected = !1, d.options.data[p(a, d.data)] = a.options,
                            a.setState(""), a.firePointEvent("unselect"))
                        })
                        })
                    }, onMouseOver: function (a) { var b = this.series.chart, c = b.pointer; a = a ? c.normalize(a) : c.getChartCoordinatesFromPoint(this, b.inverted); c.runPointActions(a, this) }, onMouseOut: function () { var a = this.series.chart; this.firePointEvent("mouseOut"); r(a.hoverPoints || [], function (a) { a.setState() }); a.hoverPoints = a.hoverPoint = null }, importEvents: function () {
                        if (!this.hasImportedEvents) {
                            var b = this, c = m(b.series.options.point, b.options).events; b.events = c; a.objectEach(c, function (a,
                                c) { C(b, c, a) }); this.hasImportedEvents = !0
                        }
                    }, setState: function (a, b) {
                        var c = Math.floor(this.plotX), d = this.plotY, e = this.series, h = e.options.states[a || "normal"] || {}, m = f[e.type].marker && e.options.marker, n = m && !1 === m.enabled, t = m && m.states && m.states[a || "normal"] || {}, g = !1 === t.enabled, u = e.stateMarkerGraphic, p = this.marker || {}, z = e.chart, q = e.halo, r, y = m && e.markerAttribs; a = a || ""; if (!(a === this.state && !b || this.selected && "select" !== a || !1 === h.enabled || a && (g || n && !1 === t.enabled) || a && p.states && p.states[a] && !1 === p.states[a].enabled)) {
                            y &&
                            (r = e.markerAttribs(this, a)); if (this.graphic) this.state && this.graphic.removeClass("highcharts-point-" + this.state), a && this.graphic.addClass("highcharts-point-" + a), this.graphic.animate(e.pointAttribs(this, a), k(z.options.chart.animation, h.animation)), r && this.graphic.animate(r, k(z.options.chart.animation, t.animation, m.animation)), u && u.hide(); else {
                                if (a && t) {
                                    m = p.symbol || e.symbol; u && u.currentSymbol !== m && (u = u.destroy()); if (u) u[b ? "animate" : "attr"]({ x: r.x, y: r.y }); else m && (e.stateMarkerGraphic = u = z.renderer.symbol(m,
                                        r.x, r.y, r.width, r.height).add(e.markerGroup), u.currentSymbol = m); u && u.attr(e.pointAttribs(this, a))
                                } u && (u[a && z.isInsidePlot(c, d, z.inverted) ? "show" : "hide"](), u.element.point = this)
                            } (c = h.halo) && c.size ? (q || (e.halo = q = z.renderer.path().add((this.graphic || u).parentGroup)), q.show()[b ? "animate" : "attr"]({ d: this.haloPath(c.size) }), q.attr({ "class": "highcharts-halo highcharts-color-" + k(this.colorIndex, e.colorIndex) + (this.className ? " " + this.className : ""), zIndex: -1 }), q.point = this, q.attr(A({
                                fill: this.color || e.color,
                                "fill-opacity": c.opacity
                            }, c.attributes))) : q && q.point && q.point.haloPath && q.animate({ d: q.point.haloPath(0) }, null, q.hide); this.state = a; w(this, "afterSetState")
                        }
                    }, haloPath: function (a) { return this.series.chart.renderer.symbols.circle(Math.floor(this.plotX) - a, this.plotY - a, 2 * a, 2 * a) }
                }); A(e.prototype, {
                    onMouseOver: function () { var a = this.chart, b = a.hoverSeries; if (b && b !== this) b.onMouseOut(); this.options.events.mouseOver && w(this, "mouseOver"); this.setState("hover"); a.hoverSeries = this }, onMouseOut: function () {
                        var a =
                            this.options, b = this.chart, c = b.tooltip, d = b.hoverPoint; b.hoverSeries = null; if (d) d.onMouseOut(); this && a.events.mouseOut && w(this, "mouseOut"); !c || this.stickyTracking || c.shared && !this.noSharedTooltip || c.hide(); this.setState()
                    }, setState: function (a) {
                        var b = this, c = b.options, d = b.graph, e = c.states, h = c.lineWidth, c = 0; a = a || ""; if (b.state !== a && (r([b.group, b.markerGroup, b.dataLabelsGroup], function (c) { c && (b.state && c.removeClass("highcharts-series-" + b.state), a && c.addClass("highcharts-series-" + a)) }), b.state = a, !e[a] || !1 !==
                            e[a].enabled) && (a && (h = e[a].lineWidth || h + (e[a].lineWidthPlus || 0)), d && !d.dashstyle)) for (h = { "stroke-width": h }, d.animate(h, k(e[a || "normal"] && e[a || "normal"].animation, b.chart.options.chart.animation)); b["zone-graph-" + c];)b["zone-graph-" + c].attr(h), c += 1
                    }, setVisible: function (a, b) {
                        var c = this, d = c.chart, e = c.legendItem, h, k = d.options.chart.ignoreHiddenSeries, f = c.visible; h = (c.visible = a = c.options.visible = c.userOptions.visible = void 0 === a ? !f : a) ? "show" : "hide"; r(["group", "dataLabelsGroup", "markerGroup", "tracker", "tt"],
                            function (a) { if (c[a]) c[a][h]() }); if (d.hoverSeries === c || (d.hoverPoint && d.hoverPoint.series) === c) c.onMouseOut(); e && d.legend.colorizeItem(c, a); c.isDirty = !0; c.options.stacking && r(d.series, function (a) { a.options.stacking && a.visible && (a.isDirty = !0) }); r(c.linkedSeries, function (b) { b.setVisible(a, !1) }); k && (d.isDirtyBox = !0); w(c, h); !1 !== b && d.redraw()
                    }, show: function () { this.setVisible(!0) }, hide: function () { this.setVisible(!1) }, select: function (a) {
                    this.selected = a = void 0 === a ? !this.selected : a; this.checkbox && (this.checkbox.checked =
                        a); w(this, a ? "select" : "unselect")
                    }, drawTracker: t.drawTrackerGraph
                })
    })(L); (function (a) {
        var C = a.Chart, D = a.each, E = a.inArray, q = a.isArray, n = a.isObject, f = a.pick, r = a.splat; C.prototype.setResponsive = function (f) {
            var n = this.options.responsive, q = [], p = this.currentResponsive; n && n.rules && D(n.rules, function (c) { void 0 === c._id && (c._id = a.uniqueKey()); this.matchResponsiveRule(c, q, f) }, this); var c = a.merge.apply(0, a.map(q, function (c) { return a.find(n.rules, function (a) { return a._id === c }).chartOptions })), q = q.toString() || void 0;
            q !== (p && p.ruleIds) && (p && this.update(p.undoOptions, f), q ? (this.currentResponsive = { ruleIds: q, mergedOptions: c, undoOptions: this.currentOptions(c) }, this.update(c, f)) : this.currentResponsive = void 0)
        }; C.prototype.matchResponsiveRule = function (a, n) { var q = a.condition; (q.callback || function () { return this.chartWidth <= f(q.maxWidth, Number.MAX_VALUE) && this.chartHeight <= f(q.maxHeight, Number.MAX_VALUE) && this.chartWidth >= f(q.minWidth, 0) && this.chartHeight >= f(q.minHeight, 0) }).call(this) && n.push(a._id) }; C.prototype.currentOptions =
            function (f) { function w(f, c, d, m) { var k; a.objectEach(f, function (a, e) { if (!m && -1 < E(e, ["series", "xAxis", "yAxis"])) for (a = r(a), d[e] = [], k = 0; k < a.length; k++)c[e][k] && (d[e][k] = {}, w(a[k], c[e][k], d[e][k], m + 1)); else n(a) ? (d[e] = q(a) ? [] : {}, w(a, c[e] || {}, d[e], m + 1)) : d[e] = c[e] || null }) } var y = {}; w(f, this.options, y, 0); return y }
    })(L); (function (a) {
        var C = a.addEvent, D = a.Axis, E = a.Chart, q = a.css, n = a.defined, f = a.each, r = a.extend, A = a.noop, w = a.pick, y = a.timeUnits, p = a.wrap; p(a.Series.prototype, "init", function (a) {
            var c; a.apply(this,
                Array.prototype.slice.call(arguments, 1)); (c = this.xAxis) && c.options.ordinal && C(this, "updatedData", function () { delete c.ordinalIndex })
        }); p(D.prototype, "getTimeTicks", function (a, d, f, k, b, e, h, u) {
            var c = 0, m, p, v = {}, q, l, r, w = [], F = -Number.MAX_VALUE, B = this.options.tickPixelInterval, g = this.chart.time; if (!this.options.ordinal && !this.options.breaks || !e || 3 > e.length || void 0 === f) return a.call(this, d, f, k, b); l = e.length; for (m = 0; m < l; m++) {
                r = m && e[m - 1] > k; e[m] < f && (c = m); if (m === l - 1 || e[m + 1] - e[m] > 5 * h || r) {
                    if (e[m] > F) {
                        for (p = a.call(this,
                            d, e[c], e[m], b); p.length && p[0] <= F;)p.shift(); p.length && (F = p[p.length - 1]); w = w.concat(p)
                    } c = m + 1
                } if (r) break
            } a = p.info; if (u && a.unitRange <= y.hour) { m = w.length - 1; for (c = 1; c < m; c++)g.dateFormat("%d", w[c]) !== g.dateFormat("%d", w[c - 1]) && (v[w[c]] = "day", q = !0); q && (v[w[0]] = "day"); a.higherRanks = v } w.info = a; if (u && n(B)) {
                u = g = w.length; m = []; var x; for (q = []; u--;)c = this.translate(w[u]), x && (q[u] = x - c), m[u] = x = c; q.sort(); q = q[Math.floor(q.length / 2)]; q < .6 * B && (q = null); u = w[g - 1] > k ? g - 1 : g; for (x = void 0; u--;)c = m[u], k = Math.abs(x - c), x && k < .8 *
                    B && (null === q || k < .8 * q) ? (v[w[u]] && !v[w[u + 1]] ? (k = u + 1, x = c) : k = u, w.splice(k, 1)) : x = c
            } return w
        }); r(D.prototype, {
            beforeSetTickPositions: function () {
                var a, d = [], m, k = !1, b, e = this.getExtremes(), h = e.min, u = e.max, t, p = this.isXAxis && !!this.options.breaks, e = this.options.ordinal, q = Number.MAX_VALUE, v = this.chart.options.chart.ignoreHiddenSeries; b = "highcharts-navigator-xaxis" === this.options.className; !this.options.overscroll || this.max !== this.dataMax || this.chart.mouseIsDown && !b || this.eventArgs && (!this.eventArgs || "navigator" ===
                    this.eventArgs.trigger) || (this.max += this.options.overscroll, !b && n(this.userMin) && (this.min += this.options.overscroll)); if (e || p) {
                        f(this.series, function (b, c) { m = []; if (!(v && !1 === b.visible || !1 === b.takeOrdinalPosition && !p) && (d = d.concat(b.processedXData), a = d.length, d.sort(function (a, b) { return a - b }), q = Math.min(q, w(b.closestPointRange, q)), a)) { for (c = 0; c < a - 1;)d[c] !== d[c + 1] && m.push(d[c + 1]), c++; m[0] !== d[0] && m.unshift(d[0]); d = m } }); a = d.length; if (2 < a) {
                            b = d[1] - d[0]; for (t = a - 1; t-- && !k;)d[t + 1] - d[t] !== b && (k = !0); !this.options.keepOrdinalPadding &&
                                (d[0] - h > b || u - d[d.length - 1] > b) && (k = !0)
                        } else this.options.overscroll && (2 === a ? q = d[1] - d[0] : 1 === a ? (q = this.options.overscroll, d = [d[0], d[0] + q]) : q = this.overscrollPointsRange); k ? (this.options.overscroll && (this.overscrollPointsRange = q, d = d.concat(this.getOverscrollPositions())), this.ordinalPositions = d, b = this.ordinal2lin(Math.max(h, d[0]), !0), t = Math.max(this.ordinal2lin(Math.min(u, d[d.length - 1]), !0), 1), this.ordinalSlope = u = (u - h) / (t - b), this.ordinalOffset = h - b * u) : (this.overscrollPointsRange = w(this.closestPointRange,
                            this.overscrollPointsRange), this.ordinalPositions = this.ordinalSlope = this.ordinalOffset = void 0)
                    } this.isOrdinal = e && k; this.groupIntervalFactor = null
            }, val2lin: function (a, d) { var c = this.ordinalPositions; if (c) { var k = c.length, b, e; for (b = k; b--;)if (c[b] === a) { e = b; break } for (b = k - 1; b--;)if (a > c[b] || 0 === b) { a = (a - c[b]) / (c[b + 1] - c[b]); e = b + a; break } d = d ? e : this.ordinalSlope * (e || 0) + this.ordinalOffset } else d = a; return d }, lin2val: function (a, d) {
                var c = this.ordinalPositions; if (c) {
                    var k = this.ordinalSlope, b = this.ordinalOffset, e = c.length -
                        1, h; if (d) 0 > a ? a = c[0] : a > e ? a = c[e] : (e = Math.floor(a), h = a - e); else for (; e--;)if (d = k * e + b, a >= d) { k = k * (e + 1) + b; h = (a - d) / (k - d); break } return void 0 !== h && void 0 !== c[e] ? c[e] + (h ? h * (c[e + 1] - c[e]) : 0) : a
                } return a
            }, getExtendedPositions: function () {
                var a = this, d = a.chart, m = a.series[0].currentDataGrouping, k = a.ordinalIndex, b = m ? m.count + m.unitName : "raw", e = a.options.overscroll, h = a.getExtremes(), n, t; k || (k = a.ordinalIndex = {}); k[b] || (n = {
                    series: [], chart: d, getExtremes: function () { return { min: h.dataMin, max: h.dataMax + e } }, options: { ordinal: !0 },
                    val2lin: D.prototype.val2lin, ordinal2lin: D.prototype.ordinal2lin
                }, f(a.series, function (b) { t = { xAxis: n, xData: b.xData.slice(), chart: d, destroyGroupedData: A }; t.xData = t.xData.concat(a.getOverscrollPositions()); t.options = { dataGrouping: m ? { enabled: !0, forced: !0, approximation: "open", units: [[m.unitName, [m.count]]] } : { enabled: !1 } }; b.processData.apply(t); n.series.push(t) }), a.beforeSetTickPositions.apply(n), k[b] = n.ordinalPositions); return k[b]
            }, getOverscrollPositions: function () {
                var c = this.options.overscroll, d = this.overscrollPointsRange,
                f = [], k = this.dataMax; if (a.defined(d)) for (f.push(k); k <= this.dataMax + c;)k += d, f.push(k); return f
            }, getGroupIntervalFactor: function (a, d, f) { var c; f = f.processedXData; var b = f.length, e = []; c = this.groupIntervalFactor; if (!c) { for (c = 0; c < b - 1; c++)e[c] = f[c + 1] - f[c]; e.sort(function (a, b) { return a - b }); e = e[Math.floor(b / 2)]; a = Math.max(a, f[0]); d = Math.min(d, f[b - 1]); this.groupIntervalFactor = c = b * e / (d - a) } return c }, postProcessTickInterval: function (a) {
                var c = this.ordinalSlope; return c ? this.options.breaks ? this.closestPointRange ||
                    a : a / (c / this.closestPointRange) : a
            }
        }); D.prototype.ordinal2lin = D.prototype.val2lin; p(E.prototype, "pan", function (a, d) {
            var c = this.xAxis[0], k = c.options.overscroll, b = d.chartX, e = !1; if (c.options.ordinal && c.series.length) {
                var h = this.mouseDownX, n = c.getExtremes(), t = n.dataMax, p = n.min, r = n.max, v = this.hoverPoints, w = c.closestPointRange || c.overscrollPointsRange, h = (h - b) / (c.translationSlope * (c.ordinalSlope || w)), l = { ordinalPositions: c.getExtendedPositions() }, w = c.lin2val, y = c.val2lin, A; l.ordinalPositions ? 1 < Math.abs(h) &&
                    (v && f(v, function (a) { a.setState() }), 0 > h ? (v = l, A = c.ordinalPositions ? c : l) : (v = c.ordinalPositions ? c : l, A = l), l = A.ordinalPositions, t > l[l.length - 1] && l.push(t), this.fixedRange = r - p, h = c.toFixedRange(null, null, w.apply(v, [y.apply(v, [p, !0]) + h, !0]), w.apply(A, [y.apply(A, [r, !0]) + h, !0])), h.min >= Math.min(n.dataMin, p) && h.max <= Math.max(t, r) + k && c.setExtremes(h.min, h.max, !0, !1, { trigger: "pan" }), this.mouseDownX = b, q(this.container, { cursor: "move" })) : e = !0
            } else e = !0; e && (k && (c.max = c.dataMax + k), a.apply(this, Array.prototype.slice.call(arguments,
                1)))
        })
    })(L); (function (a) {
        function C() { return Array.prototype.slice.call(arguments, 1) } function D(a) { a.apply(this); this.drawBreaks(this.xAxis, ["x"]); this.drawBreaks(this.yAxis, q(this.pointArrayMap, ["y"])) } var E = a.addEvent, q = a.pick, n = a.wrap, f = a.each, r = a.extend, A = a.isArray, w = a.fireEvent, y = a.Axis, p = a.Series; r(y.prototype, {
            isInBreak: function (a, d) { var c = a.repeat || Infinity, f = a.from, b = a.to - a.from; d = d >= f ? (d - f) % c : c - (f - d) % c; return a.inclusive ? d <= b : d < b && 0 !== d }, isInAnyBreak: function (a, d) {
                var c = this.options.breaks,
                f = c && c.length, b, e, h; if (f) { for (; f--;)this.isInBreak(c[f], a) && (b = !0, e || (e = q(c[f].showPoints, this.isXAxis ? !1 : !0))); h = b && d ? b && !e : b } return h
            }
        }); E(y, "afterSetTickPositions", function () { if (this.options.breaks) { var a = this.tickPositions, d = this.tickPositions.info, f = [], k; for (k = 0; k < a.length; k++)this.isInAnyBreak(a[k]) || f.push(a[k]); this.tickPositions = f; this.tickPositions.info = d } }); E(y, "afterSetOptions", function () { this.options.breaks && this.options.breaks.length && (this.options.ordinal = !1) }); E(y, "afterInit", function () {
            var a =
                this, d; d = this.options.breaks; a.isBroken = A(d) && !!d.length; a.isBroken && (a.val2lin = function (c) { var d = c, b, e; for (e = 0; e < a.breakArray.length; e++)if (b = a.breakArray[e], b.to <= c) d -= b.len; else if (b.from >= c) break; else if (a.isInBreak(b, c)) { d -= c - b.from; break } return d }, a.lin2val = function (c) { var d, b; for (b = 0; b < a.breakArray.length && !(d = a.breakArray[b], d.from >= c); b++)d.to < c ? c += d.len : a.isInBreak(d, c) && (c += d.len); return c }, a.setExtremes = function (a, c, b, d, h) {
                    for (; this.isInAnyBreak(a);)a -= this.closestPointRange; for (; this.isInAnyBreak(c);)c -=
                        this.closestPointRange; y.prototype.setExtremes.call(this, a, c, b, d, h)
                }, a.setAxisTranslation = function (c) {
                    y.prototype.setAxisTranslation.call(this, c); c = a.options.breaks; var d = [], b = [], e = 0, h, m, n = a.userMin || a.min, p = a.userMax || a.max, r = q(a.pointRangePadding, 0), v, G; f(c, function (b) { m = b.repeat || Infinity; a.isInBreak(b, n) && (n += b.to % m - n % m); a.isInBreak(b, p) && (p -= p % m - b.from % m) }); f(c, function (a) {
                        v = a.from; for (m = a.repeat || Infinity; v - m > n;)v -= m; for (; v < n;)v += m; for (G = v; G < p; G += m)d.push({ value: G, move: "in" }), d.push({
                            value: G +
                                (a.to - a.from), move: "out", size: a.breakSize
                        })
                    }); d.sort(function (a, b) { return a.value === b.value ? ("in" === a.move ? 0 : 1) - ("in" === b.move ? 0 : 1) : a.value - b.value }); h = 0; v = n; f(d, function (a) { h += "in" === a.move ? 1 : -1; 1 === h && "in" === a.move && (v = a.value); 0 === h && (b.push({ from: v, to: a.value, len: a.value - v - (a.size || 0) }), e += a.value - v - (a.size || 0)) }); a.breakArray = b; a.unitLength = p - n - e + r; w(a, "afterBreaks"); a.options.staticScale ? a.transA = a.options.staticScale : a.unitLength && (a.transA *= (p - a.min + r) / a.unitLength); r && (a.minPixelPadding =
                        a.transA * a.minPointOffset); a.min = n; a.max = p
                })
        }); n(p.prototype, "generatePoints", function (a) { a.apply(this, C(arguments)); var c = this.xAxis, f = this.yAxis, k = this.points, b, e = k.length, h = this.options.connectNulls, n; if (c && f && (c.options.breaks || f.options.breaks)) for (; e--;)b = k[e], n = null === b.y && !1 === h, n || !c.isInAnyBreak(b.x, !0) && !f.isInAnyBreak(b.y, !0) || (k.splice(e, 1), this.data[e] && this.data[e].destroyElements()) }); a.Series.prototype.drawBreaks = function (a, d) {
            var c = this, k = c.points, b, e, h, n; a && f(d, function (d) {
                b = a.breakArray ||
                []; e = a.isXAxis ? a.min : q(c.options.threshold, a.min); f(k, function (c) { n = q(c["stack" + d.toUpperCase()], c[d]); f(b, function (b) { h = !1; if (e < b.from && n > b.to || e > b.from && n < b.from) h = "pointBreak"; else if (e < b.from && n > b.from && n < b.to || e > b.from && n > b.to && n < b.from) h = "pointInBreak"; h && w(a, h, { point: c, brk: b }) }) })
            })
        }; a.Series.prototype.gappedPath = function () {
            var c = this.currentDataGrouping, d = c && c.totalRange, c = this.options.gapSize, f = this.points.slice(), k = f.length - 1, b = this.yAxis; if (c && 0 < k) for ("value" !== this.options.gapUnit && (c *=
                this.closestPointRange), d && d > c && (c = d); k--;)f[k + 1].x - f[k].x > c && (d = (f[k].x + f[k + 1].x) / 2, f.splice(k + 1, 0, { isNull: !0, x: d }), this.options.stacking && (d = b.stacks[this.stackKey][d] = new a.StackItem(b, b.options.stackLabels, !1, d, this.stack), d.total = 0)); return this.getGraphPath(f)
        }; n(a.seriesTypes.column.prototype, "drawPoints", D); n(a.Series.prototype, "drawPoints", D)
    })(L); (function (a) {
        var C = a.addEvent, D = a.arrayMax, E = a.arrayMin, q = a.Axis, n = a.defaultPlotOptions, f = a.defined, r = a.each, A = a.extend, w = a.format, y = a.isNumber,
        p = a.merge, c = a.pick, d = a.Point, m = a.Series, k = a.Tooltip, b = a.wrap, e = m.prototype, h = e.processData, u = e.generatePoints, t = {
            approximation: "average", groupPixelWidth: 2, dateTimeLabelFormats: {
                millisecond: ["%A, %b %e, %H:%M:%S.%L", "%A, %b %e, %H:%M:%S.%L", "-%H:%M:%S.%L"], second: ["%A, %b %e, %H:%M:%S", "%A, %b %e, %H:%M:%S", "-%H:%M:%S"], minute: ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], hour: ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], day: ["%A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], week: ["Week from %A, %b %e, %Y",
                    "%A, %b %e", "-%A, %b %e, %Y"], month: ["%B %Y", "%B", "-%B %Y"], year: ["%Y", "%Y", "-%Y"]
            }
        }, z = { line: {}, spline: {}, area: {}, areaspline: {}, column: { approximation: "sum", groupPixelWidth: 10 }, arearange: { approximation: "range" }, areasplinerange: { approximation: "range" }, columnrange: { approximation: "range", groupPixelWidth: 10 }, candlestick: { approximation: "ohlc", groupPixelWidth: 10 }, ohlc: { approximation: "ohlc", groupPixelWidth: 5 } }, I = a.defaultDataGroupingUnits = [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5,
            10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1]], ["week", [1]], ["month", [1, 3, 6]], ["year", null]], v = a.approximations = {
                sum: function (a) { var b = a.length, c; if (!b && a.hasNulls) c = null; else if (b) for (c = 0; b--;)c += a[b]; return c }, average: function (a) { var b = a.length; a = v.sum(a); y(a) && b && (a /= b); return a }, averages: function () { var a = []; r(arguments, function (b) { a.push(v.average(b)) }); return void 0 === a[0] ? void 0 : a }, open: function (a) { return a.length ? a[0] : a.hasNulls ? null : void 0 }, high: function (a) {
                    return a.length ?
                        D(a) : a.hasNulls ? null : void 0
                }, low: function (a) { return a.length ? E(a) : a.hasNulls ? null : void 0 }, close: function (a) { return a.length ? a[a.length - 1] : a.hasNulls ? null : void 0 }, ohlc: function (a, b, c, d) { a = v.open(a); b = v.high(b); c = v.low(c); d = v.close(d); if (y(a) || y(b) || y(c) || y(d)) return [a, b, c, d] }, range: function (a, b) { a = v.low(a); b = v.high(b); if (y(a) || y(b)) return [a, b]; if (null === a && null === b) return null }
            }; e.groupData = function (a, b, c, d) {
                var e = this.data, h = this.options.data, g = [], f = [], k = [], l = a.length, m, n, p = !!b, u = []; d = "function" ===
                    typeof d ? d : v[d] || z[this.type] && v[z[this.type].approximation] || v[t.approximation]; var q = this.pointArrayMap, w = q && q.length, A = 0; n = 0; var G, I; w ? r(q, function () { u.push([]) }) : u.push([]); G = w || 1; for (I = 0; I <= l && !(a[I] >= c[0]); I++); for (I; I <= l; I++) {
                        for (; void 0 !== c[A + 1] && a[I] >= c[A + 1] || I === l;) { m = c[A]; this.dataGroupInfo = { start: n, length: u[0].length }; n = d.apply(this, u); void 0 !== n && (g.push(m), f.push(n), k.push(this.dataGroupInfo)); n = I; for (m = 0; m < G; m++)u[m].length = 0, u[m].hasNulls = !1; A += 1; if (I === l) break } if (I === l) break; if (q) {
                            m =
                            this.cropStart + I; var H = e && e[m] || this.pointClass.prototype.applyOptions.apply({ series: this }, [h[m]]), C; for (m = 0; m < w; m++)C = H[q[m]], y(C) ? u[m].push(C) : null === C && (u[m].hasNulls = !0)
                        } else m = p ? b[I] : null, y(m) ? u[0].push(m) : null === m && (u[0].hasNulls = !0)
                    } return [g, f, k]
            }; e.processData = function () {
                var a = this.chart, b = this.options.dataGrouping, d = !1 !== this.allowDG && b && c(b.enabled, a.options.isStock), k = this.visible || !a.options.chart.ignoreHiddenSeries, m, n = this.currentDataGrouping, g; this.forceCrop = d; this.groupPixelWidth =
                    null; this.hasProcessed = !0; if (!1 !== h.apply(this, arguments) && d) {
                        this.destroyGroupedData(); var t, p = b.groupAll ? this.xData : this.processedXData, v = b.groupAll ? this.yData : this.processedYData, u = a.plotSizeX, a = this.xAxis, q = a.options.ordinal, z = this.groupPixelWidth = a.getGroupPixelWidth && a.getGroupPixelWidth(); if (z) {
                        this.isDirty = m = !0; this.points = null; d = a.getExtremes(); g = d.min; d = d.max; q = q && a.getGroupIntervalFactor(g, d, this) || 1; z = z * (d - g) / u * q; u = a.getTimeTicks(a.normalizeTimeTickInterval(z, b.units || I), Math.min(g, p[0]),
                            Math.max(d, p[p.length - 1]), a.options.startOfWeek, p, this.closestPointRange); v = e.groupData.apply(this, [p, v, u, b.approximation]); p = v[0]; q = v[1]; if (b.smoothed && p.length) { t = p.length - 1; for (p[t] = Math.min(p[t], d); t-- && 0 < t;)p[t] += z / 2; p[0] = Math.max(p[0], g) } g = u.info; this.closestPointRange = u.info.totalRange; this.groupMap = v[2]; f(p[0]) && p[0] < a.dataMin && k && (a.min <= a.dataMin && (a.min = p[0]), a.dataMin = p[0]); b.groupAll && (b = this.cropData(p, q, a.min, a.max, 1), p = b.xData, q = b.yData); this.processedXData = p; this.processedYData = q
                        } else this.groupMap =
                            null; this.hasGroupedData = m; this.currentDataGrouping = g; this.preventGraphAnimation = (n && n.totalRange) !== (g && g.totalRange)
                    }
            }; e.destroyGroupedData = function () { var a = this.groupedData; r(a || [], function (b, c) { b && (a[c] = b.destroy ? b.destroy() : null) }); this.groupedData = null }; e.generatePoints = function () { u.apply(this); this.destroyGroupedData(); this.groupedData = this.hasGroupedData ? this.points : null }; C(d, "update", function () { if (this.dataGroup) return a.error(24), !1 }); b(k.prototype, "tooltipFooterHeaderFormatter", function (a,
                b, c) { var d = this.chart.time, e = b.series, h = e.tooltipOptions, g = e.options.dataGrouping, f = h.xDateFormat, k, l = e.xAxis; return l && "datetime" === l.options.type && g && y(b.key) ? (a = e.currentDataGrouping, g = g.dateTimeLabelFormats, a ? (l = g[a.unitName], 1 === a.count ? f = l[0] : (f = l[1], k = l[2])) : !f && g && (f = this.getXDateFormat(b, h, l)), f = d.dateFormat(f, b.key), k && (f += d.dateFormat(k, b.key + a.totalRange - 1)), w(h[(c ? "footer" : "header") + "Format"], { point: A(b.point, { key: f }), series: e }, d)) : a.call(this, b, c) }); C(m, "destroy", e.destroyGroupedData);
        C(m, "afterSetOptions", function (a) { a = a.options; var b = this.type, c = this.chart.options.plotOptions, d = n[b].dataGrouping, e = this.useCommonDataGrouping && t; if (z[b] || e) d || (d = p(t, z[b])), a.dataGrouping = p(e, d, c.series && c.series.dataGrouping, c[b].dataGrouping, this.userOptions.dataGrouping); this.chart.options.isStock && (this.requireSorting = !0) }); C(q, "afterSetScale", function () { r(this.series, function (a) { a.hasProcessed = !1 }) }); q.prototype.getGroupPixelWidth = function () {
            var a = this.series, b = a.length, c, d = 0, e = !1, h; for (c =
                b; c--;)(h = a[c].options.dataGrouping) && (d = Math.max(d, h.groupPixelWidth)); for (c = b; c--;)(h = a[c].options.dataGrouping) && a[c].hasProcessed && (b = (a[c].processedXData || a[c].data).length, a[c].groupPixelWidth || b > this.chart.plotSizeX / d || b && h.forced) && (e = !0); return e ? d : 0
        }; q.prototype.setDataGrouping = function (a, b) {
            var d; b = c(b, !0); a || (a = { forced: !1, units: null }); if (this instanceof q) for (d = this.series.length; d--;)this.series[d].update({ dataGrouping: a }, !1); else r(this.chart.options.series, function (b) {
            b.dataGrouping =
                a
            }, !1); this.ordinalSlope = null; b && this.chart.redraw()
        }
    })(L); (function (a) {
        var C = a.each, D = a.Point, E = a.seriesType, q = a.seriesTypes; E("ohlc", "column", { lineWidth: 1, tooltip: { pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e \x3cb\x3e {series.name}\x3c/b\x3e\x3cbr/\x3eOpen: {point.open}\x3cbr/\x3eHigh: {point.high}\x3cbr/\x3eLow: {point.low}\x3cbr/\x3eClose: {point.close}\x3cbr/\x3e' }, threshold: null, states: { hover: { lineWidth: 3 } }, stickyTracking: !0 }, {
            directTouch: !1, pointArrayMap: ["open",
                "high", "low", "close"], toYData: function (a) { return [a.open, a.high, a.low, a.close] }, pointValKey: "close", pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" }, pointAttribs: function (a, f) { f = q.column.prototype.pointAttribs.call(this, a, f); var n = this.options; delete f.fill; !a.options.color && n.upColor && a.open < a.close && (f.stroke = n.upColor); return f }, translate: function () {
                    var a = this, f = a.yAxis, r = !!a.modifyValue, A = ["plotOpen", "plotHigh", "plotLow", "plotClose", "yBottom"]; q.column.prototype.translate.apply(a);
                    C(a.points, function (n) { C([n.open, n.high, n.low, n.close, n.low], function (q, p) { null !== q && (r && (q = a.modifyValue(q)), n[A[p]] = f.toPixels(q, !0)) }); n.tooltipPos[1] = n.plotHigh + f.pos - a.chart.plotTop })
                }, drawPoints: function () {
                    var a = this, f = a.chart; C(a.points, function (n) {
                        var q, r, y, p, c = n.graphic, d, m = !c; void 0 !== n.plotY && (c || (n.graphic = c = f.renderer.path().add(a.group)), c.attr(a.pointAttribs(n, n.selected && "select")), r = c.strokeWidth() % 2 / 2, d = Math.round(n.plotX) - r, y = Math.round(n.shapeArgs.width / 2), p = ["M", d, Math.round(n.yBottom),
                            "L", d, Math.round(n.plotHigh)], null !== n.open && (q = Math.round(n.plotOpen) + r, p.push("M", d, q, "L", d - y, q)), null !== n.close && (q = Math.round(n.plotClose) + r, p.push("M", d, q, "L", d + y, q)), c[m ? "attr" : "animate"]({ d: p }).addClass(n.getClassName(), !0))
                    })
                }, animate: null
        }, { getClassName: function () { return D.prototype.getClassName.call(this) + (this.open < this.close ? " highcharts-point-up" : " highcharts-point-down") } })
    })(L); (function (a) {
        var C = a.defaultPlotOptions, D = a.each, E = a.merge, q = a.seriesType, n = a.seriesTypes; q("candlestick",
            "ohlc", E(C.column, { states: { hover: { lineWidth: 2 } }, tooltip: C.ohlc.tooltip, threshold: null, lineColor: "#000000", lineWidth: 1, upColor: "#ffffff", stickyTracking: !0 }), {
                pointAttribs: function (a, q) {
                    var f = n.column.prototype.pointAttribs.call(this, a, q), r = this.options, y = a.open < a.close, p = r.lineColor || this.color; f["stroke-width"] = r.lineWidth; f.fill = a.options.color || (y ? r.upColor || this.color : this.color); f.stroke = a.lineColor || (y ? r.upLineColor || p : p); q && (a = r.states[q], f.fill = a.color || f.fill, f.stroke = a.lineColor || f.stroke,
                        f["stroke-width"] = a.lineWidth || f["stroke-width"]); return f
                }, drawPoints: function () {
                    var a = this, n = a.chart; D(a.points, function (f) {
                        var q = f.graphic, r, p, c, d, m, k, b, e = !q; void 0 !== f.plotY && (q || (f.graphic = q = n.renderer.path().add(a.group)), q.attr(a.pointAttribs(f, f.selected && "select")).shadow(a.options.shadow), m = q.strokeWidth() % 2 / 2, k = Math.round(f.plotX) - m, r = f.plotOpen, p = f.plotClose, c = Math.min(r, p), r = Math.max(r, p), b = Math.round(f.shapeArgs.width / 2), p = Math.round(c) !== Math.round(f.plotHigh), d = r !== f.yBottom, c = Math.round(c) +
                            m, r = Math.round(r) + m, m = [], m.push("M", k - b, r, "L", k - b, c, "L", k + b, c, "L", k + b, r, "Z", "M", k, c, "L", k, p ? Math.round(f.plotHigh) : c, "M", k, r, "L", k, d ? Math.round(f.yBottom) : r), q[e ? "attr" : "animate"]({ d: m }).addClass(f.getClassName(), !0))
                    })
                }
            })
    })(L); ea = function (a) {
        var C = a.each, D = a.defined, E = a.seriesTypes, q = a.stableSort; return {
            getPlotBox: function () { return a.Series.prototype.getPlotBox.call(this.options.onSeries && this.chart.get(this.options.onSeries) || this) }, translate: function () {
                E.column.prototype.translate.apply(this);
                var a = this.options, f = this.chart, r = this.points, A = r.length - 1, w, y, p = a.onSeries, p = p && f.get(p), a = a.onKey || "y", c = p && p.options.step, d = p && p.points, m = d && d.length, k = f.inverted, b = this.xAxis, e = this.yAxis, h = 0, u, t, z, I; if (p && p.visible && m) for (h = (p.pointXOffset || 0) + (p.barW || 0) / 2, w = p.currentDataGrouping, t = d[m - 1].x + (w ? w.totalRange : 0), q(r, function (a, b) { return a.x - b.x }), a = "plot" + a[0].toUpperCase() + a.substr(1); m-- && r[A] && !(u = d[m], w = r[A], w.y = u.y, u.x <= w.x && void 0 !== u[a] && (w.x <= t && (w.plotY = u[a], u.x < w.x && !c && (z = d[m + 1]) && void 0 !==
                    z[a] && (I = (w.x - u.x) / (z.x - u.x), w.plotY += I * (z[a] - u[a]), w.y += I * (z.y - u.y))), A-- , m++ , 0 > A));); C(r, function (a, c) { var d; a.plotX += h; if (void 0 === a.plotY || k) 0 <= a.plotX && a.plotX <= b.len ? k ? (a.plotY = b.translate(a.x, 0, 1, 0, 1), a.plotX = D(a.y) ? e.translate(a.y, 0, 0, 0, 1) : 0) : a.plotY = f.chartHeight - b.bottom - (b.opposite ? b.height : 0) + b.offset - e.top : a.shapeArgs = {}; (y = r[c - 1]) && y.plotX === a.plotX && (void 0 === y.stackIndex && (y.stackIndex = 0), d = y.stackIndex + 1); a.stackIndex = d }); this.onSeries = p
            }
        }
    }(L); (function (a, C) {
        function D(a) {
        p[a + "pin"] =
            function (c, f, k, b, e) { var d = e && e.anchorX; e = e && e.anchorY; "circle" === a && b > k && (c -= Math.round((b - k) / 2), k = b); c = p[a](c, f, k, b); d && e && (c.push("M", "circle" === a ? c[1] - c[4] : c[1] + c[4] / 2, f > e ? f : f + b, "L", d, e), c = c.concat(p.circle(d - 1, e - 1, 2, 2))); return c }
        } var E = a.addEvent, q = a.each, n = a.merge, f = a.noop, r = a.Renderer, A = a.seriesType, w = a.TrackerMixin, y = a.VMLRenderer, p = a.SVGRenderer.prototype.symbols; A("flags", "column", {
            pointRange: 0, allowOverlapX: !1, shape: "flag", stackDistance: 12, textAlign: "center", tooltip: { pointFormat: "{point.text}\x3cbr/\x3e" },
            threshold: null, y: -30, fillColor: "#ffffff", lineWidth: 1, states: { hover: { lineColor: "#000000", fillColor: "#ccd6eb" } }, style: { fontSize: "11px", fontWeight: "bold" }
        }, {
            sorted: !1, noSharedTooltip: !0, allowDG: !1, takeOrdinalPosition: !1, trackerGroups: ["markerGroup"], forceCrop: !0, init: a.Series.prototype.init, pointAttribs: function (a, d) {
                var c = this.options, f = a && a.color || this.color, b = c.lineColor, e = a && a.lineWidth; a = a && a.fillColor || c.fillColor; d && (a = c.states[d].fillColor, b = c.states[d].lineColor, e = c.states[d].lineWidth); return {
                    fill: a ||
                        f, stroke: b || f, "stroke-width": e || c.lineWidth || 0
                }
            }, translate: C.translate, getPlotBox: C.getPlotBox, drawPoints: function () {
                var c = this.points, d = this.chart, f = d.renderer, k, b, e = d.inverted, h = this.options, p = h.y, t, z, r, v, w, l, y = this.yAxis, A = {}, F = []; for (z = c.length; z--;)r = c[z], l = (e ? r.plotY : r.plotX) > this.xAxis.len, k = r.plotX, v = r.stackIndex, t = r.options.shape || h.shape, b = r.plotY, void 0 !== b && (b = r.plotY + p - (void 0 !== v && v * h.stackDistance)), r.anchorX = v ? void 0 : r.plotX, w = v ? void 0 : r.plotY, v = r.graphic, void 0 !== b && 0 <= k && !l ? (v ||
                    (v = r.graphic = f.label("", null, null, t, null, null, h.useHTML).attr(this.pointAttribs(r)).css(n(h.style, r.style)).attr({ align: "flag" === t ? "left" : "center", width: h.width, height: h.height, "text-align": h.textAlign }).addClass("highcharts-point").add(this.markerGroup), r.graphic.div && (r.graphic.div.point = r), v.shadow(h.shadow), v.isNew = !0), 0 < k && (k -= v.strokeWidth() % 2), t = { y: b, anchorY: w }, h.allowOverlapX && (t.x = k, t.anchorX = r.anchorX), v.attr({ text: r.options.title || h.title || "A" })[v.isNew ? "attr" : "animate"](t), h.allowOverlapX ||
                    (A[r.plotX] ? A[r.plotX].size = Math.max(A[r.plotX].size, v.width) : A[r.plotX] = { align: 0, size: v.width, target: k, anchorX: k }), r.tooltipPos = [k, b + y.pos - d.plotTop]) : v && (r.graphic = v.destroy()); h.allowOverlapX || (a.objectEach(A, function (a) { a.plotX = a.anchorX; F.push(a) }), a.distribute(F, e ? y.len : this.xAxis.len, 100), q(c, function (a) { var b = a.graphic && A[a.plotX]; b && (a.graphic[a.graphic.isNew ? "attr" : "animate"]({ x: b.pos, anchorX: a.anchorX }), a.graphic.isNew = !1) })); h.useHTML && a.wrap(this.markerGroup, "on", function (b) {
                        return a.SVGElement.prototype.on.apply(b.apply(this,
                            [].slice.call(arguments, 1)), [].slice.call(arguments, 1))
                    })
            }, drawTracker: function () { var a = this.points; w.drawTrackerPoint.apply(this); q(a, function (c) { var d = c.graphic; d && E(d.element, "mouseover", function () { 0 < c.stackIndex && !c.raised && (c._y = d.y, d.attr({ y: c._y - 8 }), c.raised = !0); q(a, function (a) { a !== c && a.raised && a.graphic && (a.graphic.attr({ y: a._y }), a.raised = !1) }) }) }) }, animate: f, buildKDTree: f, setClip: f, invertGroups: f
            }); p.flag = function (a, d, f, k, b) {
                var c = b && b.anchorX || a; b = b && b.anchorY || d; return p.circle(c - 1, b - 1,
                    2, 2).concat(["M", c, b, "L", a, d + k, a, d, a + f, d, a + f, d + k, a, d + k, "Z"])
            }; D("circle"); D("square"); r === y && q(["flag", "circlepin", "squarepin"], function (a) { y.prototype.symbols[a] = p[a] })
    })(L, ea); (function (a) {
        function C(a, b, c) { this.init(a, b, c) } var D = a.addEvent, E = a.Axis, q = a.correctFloat, n = a.defaultOptions, f = a.defined, r = a.destroyObjectProperties, A = a.each, w = a.fireEvent, y = a.hasTouch, p = a.isTouchDevice, c = a.merge, d = a.pick, m = a.removeEvent, k = a.wrap, b, e = {
            height: p ? 20 : 14, barBorderRadius: 0, buttonBorderRadius: 0, liveRedraw: a.svg &&
                !p, margin: 10, minWidth: 6, step: .2, zIndex: 3, barBackgroundColor: "#cccccc", barBorderWidth: 1, barBorderColor: "#cccccc", buttonArrowColor: "#333333", buttonBackgroundColor: "#e6e6e6", buttonBorderColor: "#cccccc", buttonBorderWidth: 1, rifleColor: "#333333", trackBackgroundColor: "#f2f2f2", trackBorderColor: "#f2f2f2", trackBorderWidth: 1
        }; n.scrollbar = c(!0, e, n.scrollbar); a.swapXY = b = function (a, b) { var c = a.length, d; if (b) for (b = 0; b < c; b += 3)d = a[b + 1], a[b + 1] = a[b + 2], a[b + 2] = d; return a }; C.prototype = {
            init: function (a, b, f) {
            this.scrollbarButtons =
                []; this.renderer = a; this.userOptions = b; this.options = c(e, b); this.chart = f; this.size = d(this.options.size, this.options.height); b.enabled && (this.render(), this.initEvents(), this.addEvents())
            }, render: function () {
                var a = this.renderer, c = this.options, d = this.size, e; this.group = e = a.g("scrollbar").attr({ zIndex: c.zIndex, translateY: -99999 }).add(); this.track = a.rect().addClass("highcharts-scrollbar-track").attr({ x: 0, r: c.trackBorderRadius || 0, height: d, width: d }).add(e); this.track.attr({
                    fill: c.trackBackgroundColor, stroke: c.trackBorderColor,
                    "stroke-width": c.trackBorderWidth
                }); this.trackBorderWidth = this.track.strokeWidth(); this.track.attr({ y: -this.trackBorderWidth % 2 / 2 }); this.scrollbarGroup = a.g().add(e); this.scrollbar = a.rect().addClass("highcharts-scrollbar-thumb").attr({ height: d, width: d, r: c.barBorderRadius || 0 }).add(this.scrollbarGroup); this.scrollbarRifles = a.path(b(["M", -3, d / 4, "L", -3, 2 * d / 3, "M", 0, d / 4, "L", 0, 2 * d / 3, "M", 3, d / 4, "L", 3, 2 * d / 3], c.vertical)).addClass("highcharts-scrollbar-rifles").add(this.scrollbarGroup); this.scrollbar.attr({
                    fill: c.barBackgroundColor,
                    stroke: c.barBorderColor, "stroke-width": c.barBorderWidth
                }); this.scrollbarRifles.attr({ stroke: c.rifleColor, "stroke-width": 1 }); this.scrollbarStrokeWidth = this.scrollbar.strokeWidth(); this.scrollbarGroup.translate(-this.scrollbarStrokeWidth % 2 / 2, -this.scrollbarStrokeWidth % 2 / 2); this.drawScrollbarButton(0); this.drawScrollbarButton(1)
            }, position: function (a, b, c, d) {
                var e = this.options.vertical, h = 0, f = this.rendered ? "animate" : "attr"; this.x = a; this.y = b + this.trackBorderWidth; this.width = c; this.xOffset = this.height = d;
                this.yOffset = h; e ? (this.width = this.yOffset = c = h = this.size, this.xOffset = b = 0, this.barWidth = d - 2 * c, this.x = a += this.options.margin) : (this.height = this.xOffset = d = b = this.size, this.barWidth = c - 2 * d, this.y += this.options.margin); this.group[f]({ translateX: a, translateY: this.y }); this.track[f]({ width: c, height: d }); this.scrollbarButtons[1][f]({ translateX: e ? 0 : c - b, translateY: e ? d - h : 0 })
            }, drawScrollbarButton: function (a) {
                var c = this.renderer, d = this.scrollbarButtons, e = this.options, h = this.size, f; f = c.g().add(this.group); d.push(f);
                f = c.rect().addClass("highcharts-scrollbar-button").add(f); f.attr({ stroke: e.buttonBorderColor, "stroke-width": e.buttonBorderWidth, fill: e.buttonBackgroundColor }); f.attr(f.crisp({ x: -.5, y: -.5, width: h + 1, height: h + 1, r: e.buttonBorderRadius }, f.strokeWidth())); f = c.path(b(["M", h / 2 + (a ? -1 : 1), h / 2 - 3, "L", h / 2 + (a ? -1 : 1), h / 2 + 3, "L", h / 2 + (a ? 2 : -2), h / 2], e.vertical)).addClass("highcharts-scrollbar-arrow").add(d[a]); f.attr({ fill: e.buttonArrowColor })
            }, setRange: function (a, b) {
                var c = this.options, d = c.vertical, e = c.minWidth, h = this.barWidth,
                k, l, m = this.rendered && !this.hasDragged ? "animate" : "attr"; f(h) && (a = Math.max(a, 0), k = Math.ceil(h * a), this.calculatedWidth = l = q(h * Math.min(b, 1) - k), l < e && (k = (h - e + l) * a, l = e), e = Math.floor(k + this.xOffset + this.yOffset), h = l / 2 - .5, this.from = a, this.to = b, d ? (this.scrollbarGroup[m]({ translateY: e }), this.scrollbar[m]({ height: l }), this.scrollbarRifles[m]({ translateY: h }), this.scrollbarTop = e, this.scrollbarLeft = 0) : (this.scrollbarGroup[m]({ translateX: e }), this.scrollbar[m]({ width: l }), this.scrollbarRifles[m]({ translateX: h }), this.scrollbarLeft =
                    e, this.scrollbarTop = 0), 12 >= l ? this.scrollbarRifles.hide() : this.scrollbarRifles.show(!0), !1 === c.showFull && (0 >= a && 1 <= b ? this.group.hide() : this.group.show()), this.rendered = !0)
            }, initEvents: function () {
                var a = this; a.mouseMoveHandler = function (b) {
                    var c = a.chart.pointer.normalize(b), d = a.options.vertical ? "chartY" : "chartX", e = a.initPositions; !a.grabbedCenter || b.touches && 0 === b.touches[0][d] || (c = a.cursorToScrollbarPosition(c)[d], d = a[d], d = c - d, a.hasDragged = !0, a.updatePosition(e[0] + d, e[1] + d), a.hasDragged && w(a, "changed",
                        { from: a.from, to: a.to, trigger: "scrollbar", DOMType: b.type, DOMEvent: b }))
                }; a.mouseUpHandler = function (b) { a.hasDragged && w(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMType: b.type, DOMEvent: b }); a.grabbedCenter = a.hasDragged = a.chartX = a.chartY = null }; a.mouseDownHandler = function (b) { b = a.chart.pointer.normalize(b); b = a.cursorToScrollbarPosition(b); a.chartX = b.chartX; a.chartY = b.chartY; a.initPositions = [a.from, a.to]; a.grabbedCenter = !0 }; a.buttonToMinClick = function (b) {
                    var c = q(a.to - a.from) * a.options.step; a.updatePosition(q(a.from -
                        c), q(a.to - c)); w(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMEvent: b })
                }; a.buttonToMaxClick = function (b) { var c = (a.to - a.from) * a.options.step; a.updatePosition(a.from + c, a.to + c); w(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMEvent: b }) }; a.trackClick = function (b) {
                    var c = a.chart.pointer.normalize(b), d = a.to - a.from, e = a.y + a.scrollbarTop, f = a.x + a.scrollbarLeft; a.options.vertical && c.chartY > e || !a.options.vertical && c.chartX > f ? a.updatePosition(a.from + d, a.to + d) : a.updatePosition(a.from - d, a.to - d);
                    w(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMEvent: b })
                }
            }, cursorToScrollbarPosition: function (a) { var b = this.options, b = b.minWidth > this.calculatedWidth ? b.minWidth : 0; return { chartX: (a.chartX - this.x - this.xOffset) / (this.barWidth - b), chartY: (a.chartY - this.y - this.yOffset) / (this.barWidth - b) } }, updatePosition: function (a, b) { 1 < b && (a = q(1 - q(b - a)), b = 1); 0 > a && (b = q(b - a), a = 0); this.from = a; this.to = b }, update: function (a) { this.destroy(); this.init(this.chart.renderer, c(!0, this.options, a), this.chart) }, addEvents: function () {
                var a =
                    this.options.inverted ? [1, 0] : [0, 1], b = this.scrollbarButtons, c = this.scrollbarGroup.element, d = this.mouseDownHandler, e = this.mouseMoveHandler, f = this.mouseUpHandler, a = [[b[a[0]].element, "click", this.buttonToMinClick], [b[a[1]].element, "click", this.buttonToMaxClick], [this.track.element, "click", this.trackClick], [c, "mousedown", d], [c.ownerDocument, "mousemove", e], [c.ownerDocument, "mouseup", f]]; y && a.push([c, "touchstart", d], [c.ownerDocument, "touchmove", e], [c.ownerDocument, "touchend", f]); A(a, function (a) {
                        D.apply(null,
                            a)
                    }); this._events = a
            }, removeEvents: function () { A(this._events, function (a) { m.apply(null, a) }); this._events.length = 0 }, destroy: function () { var a = this.chart.scroller; this.removeEvents(); A(["track", "scrollbarRifles", "scrollbar", "scrollbarGroup", "group"], function (a) { this[a] && this[a].destroy && (this[a] = this[a].destroy()) }, this); a && this === a.scrollbar && (a.scrollbar = null, r(a.scrollbarButtons)) }
        }; k(E.prototype, "init", function (a) {
            var b = this; a.apply(b, Array.prototype.slice.call(arguments, 1)); b.options.scrollbar && b.options.scrollbar.enabled &&
                (b.options.scrollbar.vertical = !b.horiz, b.options.startOnTick = b.options.endOnTick = !1, b.scrollbar = new C(b.chart.renderer, b.options.scrollbar, b.chart), D(b.scrollbar, "changed", function (a) { var c = Math.min(d(b.options.min, b.min), b.min, b.dataMin), e = Math.max(d(b.options.max, b.max), b.max, b.dataMax) - c, f; b.horiz && !b.reversed || !b.horiz && b.reversed ? (f = c + e * this.to, c += e * this.from) : (f = c + e * (1 - this.from), c += e * (1 - this.to)); b.setExtremes(c, f, !0, !1, a) }))
        }); k(E.prototype, "render", function (a) {
            var b = Math.min(d(this.options.min,
                this.min), this.min, d(this.dataMin, this.min)), c = Math.max(d(this.options.max, this.max), this.max, d(this.dataMax, this.max)), e = this.scrollbar, h = this.titleOffset || 0; a.apply(this, Array.prototype.slice.call(arguments, 1)); if (e) {
                    this.horiz ? (e.position(this.left, this.top + this.height + 2 + this.chart.scrollbarsOffsets[1] + (this.opposite ? 0 : h + this.axisTitleMargin + this.offset), this.width, this.height), h = 1) : (e.position(this.left + this.width + 2 + this.chart.scrollbarsOffsets[0] + (this.opposite ? h + this.axisTitleMargin + this.offset :
                        0), this.top, this.width, this.height), h = 0); if (!this.opposite && !this.horiz || this.opposite && this.horiz) this.chart.scrollbarsOffsets[h] += this.scrollbar.size + this.scrollbar.options.margin; isNaN(b) || isNaN(c) || !f(this.min) || !f(this.max) ? e.setRange(0, 0) : (h = (this.min - b) / (c - b), b = (this.max - b) / (c - b), this.horiz && !this.reversed || !this.horiz && this.reversed ? e.setRange(h, b) : e.setRange(1 - b, 1 - h))
                }
        }); k(E.prototype, "getOffset", function (a) {
            var b = this.horiz ? 2 : 1, c = this.scrollbar; a.apply(this, Array.prototype.slice.call(arguments,
                1)); c && (this.chart.scrollbarsOffsets = [0, 0], this.chart.axisOffset[b] += c.size + c.options.margin)
        }); k(E.prototype, "destroy", function (a) { this.scrollbar && (this.scrollbar = this.scrollbar.destroy()); a.apply(this, Array.prototype.slice.call(arguments, 1)) }); a.Scrollbar = C
    })(L); (function (a) {
        function C(a) { this.init(a) } var D = a.addEvent, E = a.Axis, q = a.Chart, n = a.color, f = a.defaultOptions, r = a.defined, A = a.destroyObjectProperties, w = a.each, y = a.erase, p = a.error, c = a.extend, d = a.grep, m = a.hasTouch, k = a.isArray, b = a.isNumber, e = a.isObject,
            h = a.isTouchDevice, u = a.merge, t = a.pick, z = a.removeEvent, I = a.Scrollbar, v = a.Series, G = a.seriesTypes, l = a.wrap, H = [].concat(a.defaultDataGroupingUnits), K = function (a) { var c = d(arguments, b); if (c.length) return Math[a].apply(0, c) }; H[4] = ["day", [1, 2, 3, 4]]; H[5] = ["week", [1, 2, 3]]; G = void 0 === G.areaspline ? "line" : "areaspline"; c(f, {
                navigator: {
                    height: 40, margin: 25, maskInside: !0, handles: { width: 7, height: 15, symbols: ["navigator-handle", "navigator-handle"], enabled: !0, lineWidth: 1, backgroundColor: "#f2f2f2", borderColor: "#999999" },
                    maskFill: n("#6685c2").setOpacity(.3).get(), outlineColor: "#cccccc", outlineWidth: 1, series: { type: G, fillOpacity: .05, lineWidth: 1, compare: null, dataGrouping: { approximation: "average", enabled: !0, groupPixelWidth: 2, smoothed: !0, units: H }, dataLabels: { enabled: !1, zIndex: 2 }, id: "highcharts-navigator-series", className: "highcharts-navigator-series", lineColor: null, marker: { enabled: !1 }, pointRange: 0, threshold: null }, xAxis: {
                        overscroll: 0, className: "highcharts-navigator-xaxis", tickLength: 0, lineWidth: 0, gridLineColor: "#e6e6e6",
                        gridLineWidth: 1, tickPixelInterval: 200, labels: { align: "left", style: { color: "#999999" }, x: 3, y: -4 }, crosshair: !1
                    }, yAxis: { className: "highcharts-navigator-yaxis", gridLineWidth: 0, startOnTick: !1, endOnTick: !1, minPadding: .1, maxPadding: .1, labels: { enabled: !1 }, crosshair: !1, title: { text: null }, tickLength: 0, tickWidth: 0 }
                }
            }); a.Renderer.prototype.symbols["navigator-handle"] = function (a, b, c, d, e) {
                a = e.width / 2; b = Math.round(a / 3) + .5; e = e.height; return ["M", -a - 1, .5, "L", a, .5, "L", a, e + .5, "L", -a - 1, e + .5, "L", -a - 1, .5, "M", -b, 4, "L", -b, e -
                    3, "M", b - 1, 4, "L", b - 1, e - 3]
            }; C.prototype = {
                drawHandle: function (a, b, c, d) { var e = this.navigatorOptions.handles.height; this.handles[b][d](c ? { translateX: Math.round(this.left + this.height / 2), translateY: Math.round(this.top + parseInt(a, 10) + .5 - e) } : { translateX: Math.round(this.left + parseInt(a, 10)), translateY: Math.round(this.top + this.height / 2 - e / 2 - 1) }) }, drawOutline: function (a, b, c, d) {
                    var e = this.navigatorOptions.maskInside, g = this.outline.strokeWidth(), f = g / 2, g = g % 2 / 2, h = this.outlineHeight, k = this.scrollbarHeight, l = this.size,
                    m = this.left - k, n = this.top; c ? (m -= f, c = n + b + g, b = n + a + g, a = ["M", m + h, n - k - g, "L", m + h, c, "L", m, c, "L", m, b, "L", m + h, b, "L", m + h, n + l + k].concat(e ? ["M", m + h, c - f, "L", m + h, b + f] : [])) : (a += m + k - g, b += m + k - g, n += f, a = ["M", m, n, "L", a, n, "L", a, n + h, "L", b, n + h, "L", b, n, "L", m + l + 2 * k, n].concat(e ? ["M", a - f, n, "L", b + f, n] : [])); this.outline[d]({ d: a })
                }, drawMasks: function (a, b, c, d) {
                    var e = this.left, g = this.top, f = this.height, h, k, l, m; c ? (l = [e, e, e], m = [g, g + a, g + b], k = [f, f, f], h = [a, b - a, this.size - b]) : (l = [e, e + a, e + b], m = [g, g, g], k = [a, b - a, this.size - b], h = [f, f, f]); w(this.shades,
                        function (a, b) { a[d]({ x: l[b], y: m[b], width: k[b], height: h[b] }) })
                }, renderElements: function () {
                    var a = this, b = a.navigatorOptions, c = b.maskInside, d = a.chart, e = d.inverted, f = d.renderer, h; a.navigatorGroup = h = f.g("navigator").attr({ zIndex: 8, visibility: "hidden" }).add(); var k = { cursor: e ? "ns-resize" : "ew-resize" }; w([!c, c, !c], function (c, d) { a.shades[d] = f.rect().addClass("highcharts-navigator-mask" + (1 === d ? "-inside" : "-outside")).attr({ fill: c ? b.maskFill : "rgba(0,0,0,0)" }).css(1 === d && k).add(h) }); a.outline = f.path().addClass("highcharts-navigator-outline").attr({
                        "stroke-width": b.outlineWidth,
                        stroke: b.outlineColor
                    }).add(h); b.handles.enabled && w([0, 1], function (c) { b.handles.inverted = d.inverted; a.handles[c] = f.symbol(b.handles.symbols[c], -b.handles.width / 2 - 1, 0, b.handles.width, b.handles.height, b.handles); a.handles[c].attr({ zIndex: 7 - c }).addClass("highcharts-navigator-handle highcharts-navigator-handle-" + ["left", "right"][c]).add(h); var e = b.handles; a.handles[c].attr({ fill: e.backgroundColor, stroke: e.borderColor, "stroke-width": e.lineWidth }).css(k) })
                }, update: function (a) {
                    w(this.series || [], function (a) {
                    a.baseSeries &&
                        delete a.baseSeries.navigatorSeries
                    }); this.destroy(); u(!0, this.chart.options.navigator, this.options, a); this.init(this.chart)
                }, render: function (c, d, e, f) {
                    var g = this.chart, h, k, l = this.scrollbarHeight, m, n = this.xAxis; h = n.fake ? g.xAxis[0] : n; var p = this.navigatorEnabled, q, B = this.rendered; k = g.inverted; var v, x = g.xAxis[0].minRange, F = g.xAxis[0].options.maxRange; if (!this.hasDragged || r(e)) {
                        if (!b(c) || !b(d)) if (B) e = 0, f = t(n.width, h.width); else return; this.left = t(n.left, g.plotLeft + l + (k ? g.plotWidth : 0)); this.size = q = m = t(n.len,
                            (k ? g.plotHeight : g.plotWidth) - 2 * l); g = k ? l : m + 2 * l; e = t(e, n.toPixels(c, !0)); f = t(f, n.toPixels(d, !0)); b(e) && Infinity !== Math.abs(e) || (e = 0, f = g); c = n.toValue(e, !0); d = n.toValue(f, !0); v = Math.abs(a.correctFloat(d - c)); v < x ? this.grabbedLeft ? e = n.toPixels(d - x, !0) : this.grabbedRight && (f = n.toPixels(c + x, !0)) : r(F) && v > F && (this.grabbedLeft ? e = n.toPixels(d - F, !0) : this.grabbedRight && (f = n.toPixels(c + F, !0))); this.zoomedMax = Math.min(Math.max(e, f, 0), q); this.zoomedMin = Math.min(Math.max(this.fixedWidth ? this.zoomedMax - this.fixedWidth :
                                Math.min(e, f), 0), q); this.range = this.zoomedMax - this.zoomedMin; q = Math.round(this.zoomedMax); e = Math.round(this.zoomedMin); p && (this.navigatorGroup.attr({ visibility: "visible" }), B = B && !this.hasDragged ? "animate" : "attr", this.drawMasks(e, q, k, B), this.drawOutline(e, q, k, B), this.navigatorOptions.handles.enabled && (this.drawHandle(e, 0, k, B), this.drawHandle(q, 1, k, B))); this.scrollbar && (k ? (k = this.top - l, h = this.left - l + (p || !h.opposite ? 0 : (h.titleOffset || 0) + h.axisTitleMargin), l = m + 2 * l) : (k = this.top + (p ? this.height : -l), h = this.left -
                                    l), this.scrollbar.position(h, k, g, l), this.scrollbar.setRange(this.zoomedMin / m, this.zoomedMax / m)); this.rendered = !0
                    }
                }, addMouseEvents: function () {
                    var a = this, b = a.chart, c = b.container, d = [], e, f; a.mouseMoveHandler = e = function (b) { a.onMouseMove(b) }; a.mouseUpHandler = f = function (b) { a.onMouseUp(b) }; d = a.getPartsEvents("mousedown"); d.push(D(c, "mousemove", e), D(c.ownerDocument, "mouseup", f)); m && (d.push(D(c, "touchmove", e), D(c.ownerDocument, "touchend", f)), d.concat(a.getPartsEvents("touchstart"))); a.eventsToUnbind = d; a.series &&
                        a.series[0] && d.push(D(a.series[0].xAxis, "foundExtremes", function () { b.navigator.modifyNavigatorAxisExtremes() }))
                }, getPartsEvents: function (a) { var b = this, c = []; w(["shades", "handles"], function (d) { w(b[d], function (e, g) { c.push(D(e.element, a, function (a) { b[d + "Mousedown"](a, g) })) }) }); return c }, shadesMousedown: function (a, b) {
                    a = this.chart.pointer.normalize(a); var c = this.chart, d = this.xAxis, e = this.zoomedMin, f = this.left, h = this.size, k = this.range, l = a.chartX, m, n; c.inverted && (l = a.chartY, f = this.top); 1 === b ? (this.grabbedCenter =
                        l, this.fixedWidth = k, this.dragOffset = l - e) : (a = l - f - k / 2, 0 === b ? a = Math.max(0, a) : 2 === b && a + k >= h && (a = h - k, d.reversed ? (a -= k, n = this.getUnionExtremes().dataMin) : m = this.getUnionExtremes().dataMax), a !== e && (this.fixedWidth = k, b = d.toFixedRange(a, a + k, n, m), r(b.min) && c.xAxis[0].setExtremes(Math.min(b.min, b.max), Math.max(b.min, b.max), !0, null, { trigger: "navigator" })))
                }, handlesMousedown: function (a, b) {
                    this.chart.pointer.normalize(a); a = this.chart; var c = a.xAxis[0], d = a.inverted && !c.reversed || !a.inverted && c.reversed; 0 === b ? (this.grabbedLeft =
                        !0, this.otherHandlePos = this.zoomedMax, this.fixedExtreme = d ? c.min : c.max) : (this.grabbedRight = !0, this.otherHandlePos = this.zoomedMin, this.fixedExtreme = d ? c.max : c.min); a.fixedRange = null
                }, onMouseMove: function (a) {
                    var b = this, c = b.chart, d = b.left, e = b.navigatorSize, f = b.range, h = b.dragOffset, k = c.inverted; a.touches && 0 === a.touches[0].pageX || (a = c.pointer.normalize(a), c = a.chartX, k && (d = b.top, c = a.chartY), b.grabbedLeft ? (b.hasDragged = !0, b.render(0, 0, c - d, b.otherHandlePos)) : b.grabbedRight ? (b.hasDragged = !0, b.render(0, 0, b.otherHandlePos,
                        c - d)) : b.grabbedCenter && (b.hasDragged = !0, c < h ? c = h : c > e + h - f && (c = e + h - f), b.render(0, 0, c - h, c - h + f)), b.hasDragged && b.scrollbar && b.scrollbar.options.liveRedraw && (a.DOMType = a.type, setTimeout(function () { b.onMouseUp(a) }, 0)))
                }, onMouseUp: function (a) {
                    var b = this.chart, c = this.xAxis, d = c && c.reversed, e = this.scrollbar, f, h, k = a.DOMEvent || a; (!this.hasDragged || e && e.hasDragged) && "scrollbar" !== a.trigger || (e = this.getUnionExtremes(), this.zoomedMin === this.otherHandlePos ? f = this.fixedExtreme : this.zoomedMax === this.otherHandlePos &&
                        (h = this.fixedExtreme), this.zoomedMax === this.size && (h = d ? e.dataMin : e.dataMax), 0 === this.zoomedMin && (f = d ? e.dataMax : e.dataMin), c = c.toFixedRange(this.zoomedMin, this.zoomedMax, f, h), r(c.min) && b.xAxis[0].setExtremes(Math.min(c.min, c.max), Math.max(c.min, c.max), !0, this.hasDragged ? !1 : null, { trigger: "navigator", triggerOp: "navigator-drag", DOMEvent: k })); "mousemove" !== a.DOMType && (this.grabbedLeft = this.grabbedRight = this.grabbedCenter = this.fixedWidth = this.fixedExtreme = this.otherHandlePos = this.hasDragged = this.dragOffset =
                            null)
                }, removeEvents: function () { this.eventsToUnbind && (w(this.eventsToUnbind, function (a) { a() }), this.eventsToUnbind = void 0); this.removeBaseSeriesEvents() }, removeBaseSeriesEvents: function () { var a = this.baseSeries || []; this.navigatorEnabled && a[0] && (!1 !== this.navigatorOptions.adaptToUpdatedData && w(a, function (a) { z(a, "updatedData", this.updatedDataHandler) }, this), a[0].xAxis && z(a[0].xAxis, "foundExtremes", this.modifyBaseAxisExtremes)) }, init: function (a) {
                    var b = a.options, c = b.navigator, d = c.enabled, e = b.scrollbar, f =
                        e.enabled, b = d ? c.height : 0, h = f ? e.height : 0; this.handles = []; this.shades = []; this.chart = a; this.setBaseSeries(); this.height = b; this.scrollbarHeight = h; this.scrollbarEnabled = f; this.navigatorEnabled = d; this.navigatorOptions = c; this.scrollbarOptions = e; this.outlineHeight = b + h; this.opposite = t(c.opposite, !d && a.inverted); var k = this, e = k.baseSeries, f = a.xAxis.length, l = a.yAxis.length, m = e && e[0] && e[0].xAxis || a.xAxis[0] || { options: {} }; D(a, "getMargins", function () {
                            var b = k.opposite ? "plotTop" : "marginBottom"; a.inverted && (b = k.opposite ?
                                "marginRight" : "plotLeft"); a[b] = (a[b] || 0) + (d || !a.inverted ? k.outlineHeight : 0) + c.margin
                        }); a.isDirtyBox = !0; k.navigatorEnabled ? (k.xAxis = new E(a, u({ breaks: m.options.breaks, ordinal: m.options.ordinal }, c.xAxis, { id: "navigator-x-axis", yAxis: "navigator-y-axis", isX: !0, type: "datetime", index: f, offset: 0, keepOrdinalPadding: !0, startOnTick: !1, endOnTick: !1, minPadding: 0, maxPadding: 0, zoomEnabled: !1 }, a.inverted ? { offsets: [h, 0, -h, 0], width: b } : { offsets: [0, -h, 0, h], height: b })), k.yAxis = new E(a, u(c.yAxis, {
                            id: "navigator-y-axis",
                            alignTicks: !1, offset: 0, index: l, zoomEnabled: !1
                        }, a.inverted ? { width: b } : { height: b })), e || c.series.data ? k.updateNavigatorSeries(!1) : 0 === a.series.length && (k.unbindRedraw = D(a, "beforeRedraw", function () { 0 < a.series.length && !k.series && (k.setBaseSeries(), k.unbindRedraw()) })), k.renderElements(), k.addMouseEvents()) : k.xAxis = {
                            translate: function (b, c) { var d = a.xAxis[0], e = d.getExtremes(), g = d.len - 2 * h, f = K("min", d.options.min, e.dataMin), d = K("max", d.options.max, e.dataMax) - f; return c ? b * d / g + f : g * (b - f) / d }, toPixels: function (a) { return this.translate(a) },
                            toValue: function (a) { return this.translate(a, !0) }, toFixedRange: E.prototype.toFixedRange, fake: !0
                        }; a.options.scrollbar.enabled && (a.scrollbar = k.scrollbar = new I(a.renderer, u(a.options.scrollbar, { margin: k.navigatorEnabled ? 0 : 10, vertical: a.inverted }), a), D(k.scrollbar, "changed", function (b) { var c = k.size, d = c * this.to, c = c * this.from; k.hasDragged = k.scrollbar.hasDragged; k.render(0, 0, c, d); (a.options.scrollbar.liveRedraw || "mousemove" !== b.DOMType && "touchmove" !== b.DOMType) && setTimeout(function () { k.onMouseUp(b) }) }));
                    k.addBaseSeriesEvents(); k.addChartEvents()
                }, getUnionExtremes: function (a) { var b = this.chart.xAxis[0], c = this.xAxis, d = c.options, e = b.options, f; a && null === b.dataMin || (f = { dataMin: t(d && d.min, K("min", e.min, b.dataMin, c.dataMin, c.min)), dataMax: t(d && d.max, K("max", e.max, b.dataMax, c.dataMax, c.max)) }); return f }, setBaseSeries: function (a, b) {
                    var c = this.chart, d = this.baseSeries = []; a = a || c.options && c.options.navigator.baseSeries || 0; w(c.series || [], function (b, c) {
                        b.options.isInternal || !b.options.showInNavigator && (c !== a && b.options.id !==
                            a || !1 === b.options.showInNavigator) || d.push(b)
                    }); this.xAxis && !this.xAxis.fake && this.updateNavigatorSeries(!0, b)
                }, updateNavigatorSeries: function (b, d) {
                    var e = this, h = e.chart, l = e.baseSeries, m, n, p = e.navigatorOptions.series, t, q = { enableMouseTracking: !1, index: null, linkedTo: null, group: "nav", padXAxis: !1, xAxis: "navigator-x-axis", yAxis: "navigator-y-axis", showInLegend: !1, stacking: !1, isInternal: !0 }, v = e.series = a.grep(e.series || [], function (b) {
                        var c = b.baseSeries; return 0 > a.inArray(c, l) ? (c && (z(c, "updatedData", e.updatedDataHandler),
                            delete c.navigatorSeries), b.destroy(), !1) : !0
                    }); l && l.length && w(l, function (a) { var b = a.navigatorSeries, g = c({ color: a.color, visible: a.visible }, k(p) ? f.navigator.series : p); b && !1 === e.navigatorOptions.adaptToUpdatedData || (q.name = "Navigator " + l.length, m = a.options || {}, t = m.navigatorOptions || {}, n = u(m, q, g, t), g = t.data || g.data, e.hasNavigatorData = e.hasNavigatorData || !!g, n.data = g || m.data && m.data.slice(0), b && b.options ? b.update(n, d) : (a.navigatorSeries = h.initSeries(n), a.navigatorSeries.baseSeries = a, v.push(a.navigatorSeries))) });
                    if (p.data && (!l || !l.length) || k(p)) e.hasNavigatorData = !1, p = a.splat(p), w(p, function (a, b) { q.name = "Navigator " + (v.length + 1); n = u(f.navigator.series, { color: h.series[b] && !h.series[b].options.isInternal && h.series[b].color || h.options.colors[b] || h.options.colors[0] }, q, a); n.data = a.data; n.data && (e.hasNavigatorData = !0, v.push(h.initSeries(n))) }); b && this.addBaseSeriesEvents()
                }, addBaseSeriesEvents: function () {
                    var a = this, b = a.baseSeries || []; b[0] && b[0].xAxis && D(b[0].xAxis, "foundExtremes", this.modifyBaseAxisExtremes);
                    w(b, function (b) { D(b, "show", function () { this.navigatorSeries && this.navigatorSeries.setVisible(!0, !1) }); D(b, "hide", function () { this.navigatorSeries && this.navigatorSeries.setVisible(!1, !1) }); !1 !== this.navigatorOptions.adaptToUpdatedData && b.xAxis && D(b, "updatedData", this.updatedDataHandler); D(b, "remove", function () { this.navigatorSeries && (y(a.series, this.navigatorSeries), r(this.navigatorSeries.options) && this.navigatorSeries.remove(!1), delete this.navigatorSeries) }) }, this)
                }, modifyNavigatorAxisExtremes: function () {
                    var a =
                        this.xAxis, b; a.getExtremes && (!(b = this.getUnionExtremes(!0)) || b.dataMin === a.min && b.dataMax === a.max || (a.min = b.dataMin, a.max = b.dataMax))
                }, modifyBaseAxisExtremes: function () {
                    var a = this.chart.navigator, c = this.getExtremes(), d = c.dataMin, e = c.dataMax, c = c.max - c.min, f = a.stickToMin, h = a.stickToMax, k = t(this.options.overscroll, 0), l, m, n = a.series && a.series[0], p = !!this.setExtremes; this.eventArgs && "rangeSelectorButton" === this.eventArgs.trigger || (f && (m = d, l = m + c), h && (l = e + k, f || (m = Math.max(l - c, n && n.xData ? n.xData[0] : -Number.MAX_VALUE))),
                        p && (f || h) && b(m) && (this.min = this.userMin = m, this.max = this.userMax = l)); a.stickToMin = a.stickToMax = null
                }, updatedDataHandler: function () { var a = this.chart.navigator, c = this.navigatorSeries; a.stickToMax = a.xAxis.reversed ? 0 === Math.round(a.zoomedMin) : Math.round(a.zoomedMax) >= Math.round(a.size); a.stickToMin = b(this.xAxis.min) && this.xAxis.min <= this.xData[0] && (!this.chart.fixedRange || !a.stickToMax); c && !a.hasNavigatorData && (c.options.pointStart = this.xData[0], c.setData(this.options.data, !1, null, !1)) }, addChartEvents: function () {
                    D(this.chart,
                        "redraw", function () { var a = this.navigator, b = a && (a.baseSeries && a.baseSeries[0] && a.baseSeries[0].xAxis || a.scrollbar && this.xAxis[0]); b && a.render(b.min, b.max) })
                }, destroy: function () {
                    this.removeEvents(); this.xAxis && (y(this.chart.xAxis, this.xAxis), y(this.chart.axes, this.xAxis)); this.yAxis && (y(this.chart.yAxis, this.yAxis), y(this.chart.axes, this.yAxis)); w(this.series || [], function (a) { a.destroy && a.destroy() }); w("series xAxis yAxis shades outline scrollbarTrack scrollbarRifles scrollbarGroup scrollbar navigatorGroup rendered".split(" "),
                        function (a) { this[a] && this[a].destroy && this[a].destroy(); this[a] = null }, this); w([this.handles], function (a) { A(a) }, this)
                }
            }; a.Navigator = C; l(E.prototype, "zoom", function (a, b, c) {
                var d = this.chart, e = d.options, g = e.chart.zoomType, f = e.chart.pinchType, k = e.navigator, e = e.rangeSelector, l; this.isXAxis && (k && k.enabled || e && e.enabled) && (!h && "x" === g || h && "x" === f ? d.resetZoomButton = "blocked" : "y" === g ? l = !1 : (!h && "xy" === g || h && "xy" === f) && this.options.range && (d = this.previousZoom, r(b) ? this.previousZoom = [this.min, this.max] : d && (b =
                    d[0], c = d[1], delete this.previousZoom))); return void 0 !== l ? l : a.call(this, b, c)
            }); D(q, "beforeRender", function () { var a = this.options; if (a.navigator.enabled || a.scrollbar.enabled) this.scroller = this.navigator = new C(this) }); D(q, "afterSetChartSize", function () {
                var a = this.legend, b = this.navigator, c, d, e, f; b && (d = a && a.options, e = b.xAxis, f = b.yAxis, c = b.scrollbarHeight, this.inverted ? (b.left = b.opposite ? this.chartWidth - c - b.height : this.spacing[3] + c, b.top = this.plotTop + c) : (b.left = this.plotLeft + c, b.top = b.navigatorOptions.top ||
                    this.chartHeight - b.height - c - this.spacing[2] - (this.rangeSelector && this.extraBottomMargin ? this.rangeSelector.getHeight() : 0) - (d && "bottom" === d.verticalAlign && d.enabled && !d.floating ? a.legendHeight + t(d.margin, 10) : 0)), e && f && (this.inverted ? e.options.left = f.options.left = b.left : e.options.top = f.options.top = b.top, e.setAxisSize(), f.setAxisSize()))
            }); D(q, "update", function (a) {
                var b = a.options.navigator || {}, c = a.options.scrollbar || {}; this.navigator || this.scroller || !b.enabled && !c.enabled || (u(!0, this.options.navigator,
                    b), u(!0, this.options.scrollbar, c), delete a.options.navigator, delete a.options.scrollbar)
            }); D(q, "afterUpdate", function () { this.navigator || this.scroller || !this.options.navigator.enabled && !this.options.scrollbar.enabled || (this.scroller = this.navigator = new C(this)) }); l(v.prototype, "addPoint", function (a, b, c, d, f) { var g = this.options.turboThreshold; g && this.xData.length > g && e(b, !0) && this.chart.navigator && p(20, !0); a.call(this, b, c, d, f) }); D(q, "afterAddSeries", function () {
            this.navigator && this.navigator.setBaseSeries(null,
                !1)
            }); D(v, "afterUpdate", function () { this.chart.navigator && !this.options.isInternal && this.chart.navigator.setBaseSeries(null, !1) }); q.prototype.callbacks.push(function (a) { var b = a.navigator; b && a.xAxis[0] && (a = a.xAxis[0].getExtremes(), b.render(a.min, a.max)) })
    })(L); (function (a) {
        function C(a) { this.init(a) } var D = a.addEvent, E = a.Axis, q = a.Chart, n = a.css, f = a.createElement, r = a.defaultOptions, A = a.defined, w = a.destroyObjectProperties, y = a.discardElement, p = a.each, c = a.extend, d = a.fireEvent, m = a.isNumber, k = a.merge, b = a.pick,
            e = a.pInt, h = a.splat, u = a.wrap; c(r, { rangeSelector: { verticalAlign: "top", buttonTheme: { "stroke-width": 0, width: 28, height: 18, padding: 2, zIndex: 7 }, floating: !1, x: 0, y: 0, height: void 0, inputPosition: { align: "right", x: 0, y: 0 }, buttonPosition: { align: "left", x: 0, y: 0 }, labelStyle: { color: "#666666" } } }); r.lang = k(r.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "From", rangeSelectorTo: "To" }); C.prototype = {
                clickButton: function (a, c) {
                    var d = this, e = d.chart, f = d.buttonOptions[a], k = e.xAxis[0], n = e.scroller && e.scroller.getUnionExtremes() ||
                        k || {}, t = n.dataMin, q = n.dataMax, r, g = k && Math.round(Math.min(k.max, b(q, k.max))), u = f.type, z, n = f._range, w, y, A, C = f.dataGrouping; if (null !== t && null !== q) {
                        e.fixedRange = n; C && (this.forcedDataGrouping = !0, E.prototype.setDataGrouping.call(k || { chart: this.chart }, C, !1)); if ("month" === u || "year" === u) k ? (u = { range: f, max: g, chart: e, dataMin: t, dataMax: q }, r = k.minFromRange.call(u), m(u.newMax) && (g = u.newMax)) : n = f; else if (n) r = Math.max(g - n, t), g = Math.min(r + n, q); else if ("ytd" === u) if (k) void 0 === q && (t = Number.MAX_VALUE, q = Number.MIN_VALUE,
                            p(e.series, function (a) { a = a.xData; t = Math.min(a[0], t); q = Math.max(a[a.length - 1], q) }), c = !1), g = d.getYTDExtremes(q, t, e.time.useUTC), r = w = g.min, g = g.max; else { D(e, "beforeRender", function () { d.clickButton(a) }); return } else "all" === u && k && (r = t, g = q); r += f._offsetMin; g += f._offsetMax; d.setSelected(a); k ? k.setExtremes(r, g, b(c, 1), null, { trigger: "rangeSelectorButton", rangeSelectorButton: f }) : (z = h(e.options.xAxis)[0], A = z.range, z.range = n, y = z.min, z.min = w, D(e, "load", function () { z.range = A; z.min = y }))
                        }
                }, setSelected: function (a) {
                this.selected =
                    this.options.selected = a
                }, defaultButtons: [{ type: "month", count: 1, text: "1m" }, { type: "month", count: 3, text: "3m" }, { type: "month", count: 6, text: "6m" }, { type: "ytd", text: "YTD" }, { type: "year", count: 1, text: "1y" }, { type: "all", text: "All" }], init: function (a) {
                    var b = this, c = a.options.rangeSelector, e = c.buttons || [].concat(b.defaultButtons), f = c.selected, h = function () { var a = b.minInput, c = b.maxInput; a && a.blur && d(a, "blur"); c && c.blur && d(c, "blur") }; b.chart = a; b.options = c; b.buttons = []; a.extraTopMargin = c.height; b.buttonOptions = e; this.unMouseDown =
                        D(a.container, "mousedown", h); this.unResize = D(a, "resize", h); p(e, b.computeButtonRange); void 0 !== f && e[f] && this.clickButton(f, !1); D(a, "load", function () { a.xAxis && a.xAxis[0] && D(a.xAxis[0], "setExtremes", function (c) { this.max - this.min !== a.fixedRange && "rangeSelectorButton" !== c.trigger && "updatedData" !== c.trigger && b.forcedDataGrouping && this.setDataGrouping(!1, !1) }) })
                }, updateButtonStates: function () {
                    var a = this.chart, b = a.xAxis[0], c = Math.round(b.max - b.min), d = !b.hasVisibleSeries, e = a.scroller && a.scroller.getUnionExtremes() ||
                        b, f = e.dataMin, h = e.dataMax, a = this.getYTDExtremes(h, f, a.time.useUTC), k = a.min, n = a.max, q = this.selected, g = m(q), r = this.options.allButtonsEnabled, u = this.buttons; p(this.buttonOptions, function (a, e) {
                            var l = a._range, m = a.type, p = a.count || 1, t = u[e], v = 0; a = a._offsetMax - a._offsetMin; e = e === q; var z = l > h - f, w = l < b.minRange, x = !1, B = !1, l = l === c; ("month" === m || "year" === m) && c + 36E5 >= 864E5 * { month: 28, year: 365 }[m] * p - a && c - 36E5 <= 864E5 * { month: 31, year: 366 }[m] * p + a ? l = !0 : "ytd" === m ? (l = n - k + a === c, x = !e) : "all" === m && (l = b.max - b.min >= h - f, B = !e && g &&
                                l); m = !r && (z || w || B || d); p = e && l || l && !g && !x; m ? v = 3 : p && (g = !0, v = 2); t.state !== v && t.setState(v)
                        })
                }, computeButtonRange: function (a) { var c = a.type, d = a.count || 1, e = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5 }; if (e[c]) a._range = e[c] * d; else if ("month" === c || "year" === c) a._range = 864E5 * { month: 30, year: 365 }[c] * d; a._offsetMin = b(a.offsetMin, 0); a._offsetMax = b(a.offsetMax, 0); a._range += a._offsetMax - a._offsetMin }, setInputValue: function (a, b) {
                    var c = this.chart.options.rangeSelector, d = this.chart.time, e = this[a +
                        "Input"]; A(b) && (e.previousValue = e.HCTime, e.HCTime = b); e.value = d.dateFormat(c.inputEditDateFormat || "%Y-%m-%d", e.HCTime); this[a + "DateBox"].attr({ text: d.dateFormat(c.inputDateFormat || "%b %e, %Y", e.HCTime) })
                }, showInput: function (a) { var b = this.inputGroup, c = this[a + "DateBox"]; n(this[a + "Input"], { left: b.translateX + c.x + "px", top: b.translateY + "px", width: c.width - 2 + "px", height: c.height - 2 + "px", border: "2px solid silver" }) }, hideInput: function (a) { n(this[a + "Input"], { border: 0, width: "1px", height: "1px" }); this.setInputValue(a) },
                drawInput: function (a) {
                    function b() { var a = w.value, b = (q.inputDateParser || Date.parse)(a), c = h.xAxis[0], g = h.scroller && h.scroller.xAxis ? h.scroller.xAxis : c, f = g.dataMin, g = g.dataMax; b !== w.previousValue && (w.previousValue = b, m(b) || (b = a.split("-"), b = Date.UTC(e(b[0]), e(b[1]) - 1, e(b[2]))), m(b) && (h.time.useUTC || (b += 6E4 * (new Date).getTimezoneOffset()), u ? b > d.maxInput.HCTime ? b = void 0 : b < f && (b = f) : b < d.minInput.HCTime ? b = void 0 : b > g && (b = g), void 0 !== b && c.setExtremes(u ? b : c.min, u ? c.max : b, void 0, void 0, { trigger: "rangeSelectorInput" }))) }
                    var d = this, h = d.chart, p = h.renderer.style || {}, l = h.renderer, q = h.options.rangeSelector, t = d.div, u = "min" === a, w, g, x = this.inputGroup; this[a + "Label"] = g = l.label(r.lang[u ? "rangeSelectorFrom" : "rangeSelectorTo"], this.inputGroup.offset).addClass("highcharts-range-label").attr({ padding: 2 }).add(x); x.offset += g.width + 5; this[a + "DateBox"] = l = l.label("", x.offset).addClass("highcharts-range-input").attr({
                        padding: 2, width: q.inputBoxWidth || 90, height: q.inputBoxHeight || 17, stroke: q.inputBoxBorderColor || "#cccccc", "stroke-width": 1,
                        "text-align": "center"
                    }).on("click", function () { d.showInput(a); d[a + "Input"].focus() }).add(x); x.offset += l.width + (u ? 10 : 0); this[a + "Input"] = w = f("input", { name: a, className: "highcharts-range-selector", type: "text" }, { top: h.plotTop + "px" }, t); g.css(k(p, q.labelStyle)); l.css(k({ color: "#333333" }, p, q.inputStyle)); n(w, c({ position: "absolute", border: 0, width: "1px", height: "1px", padding: 0, textAlign: "center", fontSize: p.fontSize, fontFamily: p.fontFamily, top: "-9999em" }, q.inputStyle)); w.onfocus = function () { d.showInput(a) }; w.onblur =
                        function () { d.hideInput(a) }; w.onchange = b; w.onkeypress = function (a) { 13 === a.keyCode && b() }
                }, getPosition: function () { var a = this.chart, b = a.options.rangeSelector, a = "top" === b.verticalAlign ? a.plotTop - a.axisOffset[0] : 0; return { buttonTop: a + b.buttonPosition.y, inputTop: a + b.inputPosition.y - 10 } }, getYTDExtremes: function (a, b, c) { var d = this.chart.time, e = new d.Date(a), f = d.get("FullYear", e); c = c ? d.Date.UTC(f, 0, 1) : +new d.Date(f, 0, 1); b = Math.max(b || 0, c); e = e.getTime(); return { max: Math.min(a || e, e), min: b } }, render: function (a, c) {
                    var d =
                        this, e = d.chart, h = e.renderer, k = e.container, m = e.options, n = m.exporting && !1 !== m.exporting.enabled && m.navigation && m.navigation.buttonOptions, q = r.lang, t = d.div, g = m.rangeSelector, m = g.floating, u = d.buttons, t = d.inputGroup, w = g.buttonTheme, z = g.buttonPosition, y = g.inputPosition, A = g.inputEnabled, C = w && w.states, D = e.plotLeft, E, L = d.buttonGroup, R; R = d.rendered; var Y = d.options.verticalAlign, aa = e.legend, ba = aa && aa.options, ca = z.y, Z = y.y, da = R || !1, X = 0, U = 0, V; if (!1 !== g.enabled) {
                            R || (d.group = R = h.g("range-selector-group").attr({ zIndex: 7 }).add(),
                                d.buttonGroup = L = h.g("range-selector-buttons").add(R), d.zoomText = h.text(q.rangeSelectorZoom, b(D + z.x, D), 15).css(g.labelStyle).add(L), E = b(D + z.x, D) + d.zoomText.getBBox().width + 5, p(d.buttonOptions, function (a, c) { u[c] = h.button(a.text, E, 0, function () { var b = a.events && a.events.click, e; b && (e = b.call(a)); !1 !== e && d.clickButton(c); d.isActive = !0 }, w, C && C.hover, C && C.select, C && C.disabled).attr({ "text-align": "center" }).add(L); E += u[c].width + b(g.buttonSpacing, 5) }), !1 !== A && (d.div = t = f("div", null, {
                                    position: "relative", height: 0,
                                    zIndex: 1
                                }), k.parentNode.insertBefore(t, k), d.inputGroup = t = h.g("input-group").add(R), t.offset = 0, d.drawInput("min"), d.drawInput("max"))); D = e.plotLeft - e.spacing[3]; d.updateButtonStates(); n && this.titleCollision(e) && "top" === Y && "right" === z.align && z.y + L.getBBox().height - 12 < (n.y || 0) + n.height && (X = -40); "left" === z.align ? V = z.x - e.spacing[3] : "right" === z.align && (V = z.x + X - e.spacing[1]); L.align({ y: z.y, width: L.getBBox().width, align: z.align, x: V }, !0, e.spacingBox); d.group.placed = da; d.buttonGroup.placed = da; !1 !== A && (X = n &&
                                    this.titleCollision(e) && "top" === Y && "right" === y.align && y.y - t.getBBox().height - 12 < (n.y || 0) + n.height + e.spacing[0] ? -40 : 0, "left" === y.align ? V = D : "right" === y.align && (V = -Math.max(e.axisOffset[1], -X)), t.align({ y: y.y, width: t.getBBox().width, align: y.align, x: y.x + V - 2 }, !0, e.spacingBox), k = t.alignAttr.translateX + t.alignOptions.x - X + t.getBBox().x + 2, n = t.alignOptions.width, q = L.alignAttr.translateX + L.getBBox().x, V = L.getBBox().width + 20, (y.align === z.align || q + V > k && k + n > q && ca < Z + t.getBBox().height) && t.attr({
                                        translateX: t.alignAttr.translateX +
                                            (e.axisOffset[1] >= -X ? 0 : -X), translateY: t.alignAttr.translateY + L.getBBox().height + 10
                                    }), d.setInputValue("min", a), d.setInputValue("max", c), d.inputGroup.placed = da); d.group.align({ verticalAlign: Y }, !0, e.spacingBox); a = d.group.getBBox().height + 20; c = d.group.alignAttr.translateY; "bottom" === Y && (aa = ba && "bottom" === ba.verticalAlign && ba.enabled && !ba.floating ? aa.legendHeight + b(ba.margin, 10) : 0, a = a + aa - 20, U = c - a - (m ? 0 : g.y) - 10); if ("top" === Y) m && (U = 0), e.titleOffset && (U = e.titleOffset + e.options.title.margin), U += e.margin[0] -
                                        e.spacing[0] || 0; else if ("middle" === Y) if (Z === ca) U = 0 > Z ? c + void 0 : c; else if (Z || ca) U = 0 > Z || 0 > ca ? U - Math.min(Z, ca) : c - a + NaN; d.group.translate(g.x, g.y + Math.floor(U)); !1 !== A && (d.minInput.style.marginTop = d.group.translateY + "px", d.maxInput.style.marginTop = d.group.translateY + "px"); d.rendered = !0
                        }
                }, getHeight: function () { var a = this.options, b = this.group, c = a.y, d = a.buttonPosition.y, a = a.inputPosition.y, b = b ? b.getBBox(!0).height + 13 + c : 0, c = Math.min(a, d); if (0 > a && 0 > d || 0 < a && 0 < d) b += Math.abs(c); return b }, titleCollision: function (a) {
                    return !(a.options.title.text ||
                        a.options.subtitle.text)
                }, update: function (a) { var b = this.chart; k(!0, b.options.rangeSelector, a); this.destroy(); this.init(b); b.rangeSelector.render() }, destroy: function () { var b = this, c = b.minInput, d = b.maxInput; b.unMouseDown(); b.unResize(); w(b.buttons); c && (c.onfocus = c.onblur = c.onchange = null); d && (d.onfocus = d.onblur = d.onchange = null); a.objectEach(b, function (a, c) { a && "chart" !== c && (a.destroy ? a.destroy() : a.nodeType && y(this[c])); a !== C.prototype[c] && (b[c] = null) }, this) }
            }; E.prototype.toFixedRange = function (a, c, d, e) {
                var f =
                    this.chart && this.chart.fixedRange; a = b(d, this.translate(a, !0, !this.horiz)); c = b(e, this.translate(c, !0, !this.horiz)); d = f && (c - a) / f; .7 < d && 1.3 > d && (e ? a = c - f : c = a + f); m(a) && m(c) || (a = c = void 0); return { min: a, max: c }
            }; E.prototype.minFromRange = function () {
                var a = this.range, c = { month: "Month", year: "FullYear" }[a.type], d, e = this.max, f, h, k = function (a, b) { var d = new Date(a), e = d["get" + c](); d["set" + c](e + b); e === d["get" + c]() && d.setDate(0); return d.getTime() - a }; m(a) ? (d = e - a, h = a) : (d = e + k(e, -a.count), this.chart && (this.chart.fixedRange =
                    e - d)); f = b(this.dataMin, Number.MIN_VALUE); m(d) || (d = f); d <= f && (d = f, void 0 === h && (h = k(d, a.count)), this.newMax = Math.min(d + h, this.dataMax)); m(e) || (d = void 0); return d
            }; D(q, "afterGetContainer", function () { this.options.rangeSelector.enabled && (this.rangeSelector = new C(this)) }); u(q.prototype, "render", function (a, b, c) {
                var d = this.axes, e = this.rangeSelector; e && (p(d, function (a) { a.updateNames(); a.setScale() }), this.getAxisMargins(), e.render(), d = e.options.verticalAlign, e.options.floating || ("bottom" === d ? this.extraBottomMargin =
                    !0 : "middle" !== d && (this.extraTopMargin = !0))); a.call(this, b, c)
            }); D(q, "update", function (a) { var b = a.options; a = this.rangeSelector; this.extraTopMargin = this.extraBottomMargin = !1; this.isDirtyBox = !0; a && (a.render(), b = b.rangeSelector && b.rangeSelector.verticalAlign || a.options && a.options.verticalAlign, a.options.floating || ("bottom" === b ? this.extraBottomMargin = !0 : "middle" !== b && (this.extraTopMargin = !0))) }); u(q.prototype, "redraw", function (a, b, c) {
                var d = this.rangeSelector; d && !d.options.floating && (d.render(), d = d.options.verticalAlign,
                    "bottom" === d ? this.extraBottomMargin = !0 : "middle" !== d && (this.extraTopMargin = !0)); a.call(this, b, c)
            }); D(q, "getMargins", function () { var a = this.rangeSelector; a && (a = a.getHeight(), this.extraTopMargin && (this.plotTop += a), this.extraBottomMargin && (this.marginBottom += a)) }); q.prototype.callbacks.push(function (a) {
                function b() { c = a.xAxis[0].getExtremes(); m(c.min) && d.render(c.min, c.max) } var c, d = a.rangeSelector, e, f; d && (f = D(a.xAxis[0], "afterSetExtremes", function (a) { d.render(a.min, a.max) }), e = D(a, "redraw", b), b()); D(a, "destroy",
                    function () { d && (e(), f()) })
            }); a.RangeSelector = C
    })(L); (function (a) {
        var C = a.addEvent, D = a.arrayMax, E = a.arrayMin, q = a.Axis, n = a.Chart, f = a.defined, r = a.each, A = a.extend, w = a.format, y = a.grep, p = a.inArray, c = a.isNumber, d = a.isString, m = a.map, k = a.merge, b = a.pick, e = a.Point, h = a.Renderer, u = a.Series, t = a.splat, z = a.SVGRenderer, I = a.VMLRenderer, v = a.wrap, G = u.prototype, l = G.init, H = G.processData, K = e.prototype.tooltipFormatter; a.StockChart = a.stockChart = function (c, e, g) {
            var f = d(c) || c.nodeName, h = arguments[f ? 1 : 0], l = h.series, p = a.getOptions(),
            q, r = b(h.navigator && h.navigator.enabled, p.navigator.enabled, !0), u = r ? { startOnTick: !1, endOnTick: !1 } : null, w = { marker: { enabled: !1, radius: 2 } }, v = { shadow: !1, borderWidth: 0 }; h.xAxis = m(t(h.xAxis || {}), function (a, b) { return k({ minPadding: 0, maxPadding: 0, overscroll: 0, ordinal: !0, title: { text: null }, labels: { overflow: "justify" }, showLastLabel: !0 }, p.xAxis, p.xAxis && p.xAxis[b], a, { type: "datetime", categories: null }, u) }); h.yAxis = m(t(h.yAxis || {}), function (a, c) {
                q = b(a.opposite, !0); return k({
                    labels: { y: -2 }, opposite: q, showLastLabel: !(!a.categories &&
                        "category" !== a.type), title: { text: null }
                }, p.yAxis, p.yAxis && p.yAxis[c], a)
            }); h.series = null; h = k({ chart: { panning: !0, pinchType: "x" }, navigator: { enabled: r }, scrollbar: { enabled: b(p.scrollbar.enabled, !0) }, rangeSelector: { enabled: b(p.rangeSelector.enabled, !0) }, title: { text: null }, tooltip: { split: b(p.tooltip.split, !0), crosshairs: !0 }, legend: { enabled: !1 }, plotOptions: { line: w, spline: w, area: w, areaspline: w, arearange: w, areasplinerange: w, column: v, columnrange: v, candlestick: v, ohlc: v } }, h, { isStock: !0 }); h.series = l; return f ? new n(c,
                h, g) : new n(h, e)
        }; v(q.prototype, "autoLabelAlign", function (a) { var b = this.chart, c = this.options, b = b._labelPanes = b._labelPanes || {}, d = this.options.labels; return this.chart.options.isStock && "yAxis" === this.coll && (c = c.top + "," + c.height, !b[c] && d.enabled) ? (15 === d.x && (d.x = 0), void 0 === d.align && (d.align = "right"), b[c] = this, "right") : a.apply(this, [].slice.call(arguments, 1)) }); C(q, "destroy", function () {
            var a = this.chart, b = this.options && this.options.top + "," + this.options.height; b && a._labelPanes && a._labelPanes[b] === this &&
                delete a._labelPanes[b]
        }); v(q.prototype, "getPlotLinePath", function (e, h, g, k, l, n) {
            var q = this, u = this.isLinked && !this.series ? this.linkedParent.series : this.series, t = q.chart, w = t.renderer, v = q.left, x = q.top, y, z, B, A, F = [], C = [], D, E; if ("xAxis" !== q.coll && "yAxis" !== q.coll) return e.apply(this, [].slice.call(arguments, 1)); C = function (a) { var b = "xAxis" === a ? "yAxis" : "xAxis"; a = q.options[b]; return c(a) ? [t[b][a]] : d(a) ? [t.get(a)] : m(u, function (a) { return a[b] }) }(q.coll); r(q.isXAxis ? t.yAxis : t.xAxis, function (a) {
                if (f(a.options.id) ?
                    -1 === a.options.id.indexOf("navigator") : 1) { var b = a.isXAxis ? "yAxis" : "xAxis", b = f(a.options[b]) ? t[b][a.options[b]] : t[b][0]; q === b && C.push(a) }
            }); D = C.length ? [] : [q.isXAxis ? t.yAxis[0] : t.xAxis[0]]; r(C, function (b) { -1 !== p(b, D) || a.find(D, function (a) { return a.pos === b.pos && a.len && b.len }) || D.push(b) }); E = b(n, q.translate(h, null, null, k)); c(E) && (q.horiz ? r(D, function (a) { var b; z = a.pos; A = z + a.len; y = B = Math.round(E + q.transB); if (y < v || y > v + q.width) l ? y = B = Math.min(Math.max(v, y), v + q.width) : b = !0; b || F.push("M", y, z, "L", B, A) }) : r(D,
                function (a) { var b; y = a.pos; B = y + a.len; z = A = Math.round(x + q.height - E); if (z < x || z > x + q.height) l ? z = A = Math.min(Math.max(x, z), q.top + q.height) : b = !0; b || F.push("M", y, z, "L", B, A) })); return 0 < F.length ? w.crispPolyLine(F, g || 1) : null
        }); z.prototype.crispPolyLine = function (a, b) { var c; for (c = 0; c < a.length; c += 6)a[c + 1] === a[c + 4] && (a[c + 1] = a[c + 4] = Math.round(a[c + 1]) - b % 2 / 2), a[c + 2] === a[c + 5] && (a[c + 2] = a[c + 5] = Math.round(a[c + 2]) + b % 2 / 2); return a }; h === I && (I.prototype.crispPolyLine = z.prototype.crispPolyLine); v(q.prototype, "hideCrosshair",
            function (a, b) { a.call(this, b); this.crossLabel && (this.crossLabel = this.crossLabel.hide()) }); C(q, "afterDrawCrosshair", function (a) {
                var c, d; if (f(this.crosshair.label) && this.crosshair.label.enabled && this.cross) {
                    var e = this.chart, h = this.options.crosshair.label, k = this.horiz; c = this.opposite; d = this.left; var l = this.top, m = this.crossLabel, n = h.format, p = "", q = "inside" === this.options.tickPosition, r = !1 !== this.crosshair.snap, t = 0, u = a.e || this.cross && this.cross.e, v = a.point; a = k ? "center" : c ? "right" === this.labelAlign ? "right" :
                        "left" : "left" === this.labelAlign ? "left" : "center"; m || (m = this.crossLabel = e.renderer.label(null, null, null, h.shape || "callout").addClass("highcharts-crosshair-label" + (this.series[0] && " highcharts-color-" + this.series[0].colorIndex)).attr({ align: h.align || a, padding: b(h.padding, 8), r: b(h.borderRadius, 3), zIndex: 2 }).add(this.labelGroup), m.attr({ fill: h.backgroundColor || this.series[0] && this.series[0].color || "#666666", stroke: h.borderColor || "", "stroke-width": h.borderWidth || 0 }).css(A({
                            color: "#ffffff", fontWeight: "normal",
                            fontSize: "11px", textAlign: "center"
                        }, h.style))); k ? (a = r ? v.plotX + d : u.chartX, l += c ? 0 : this.height) : (a = c ? this.width + d : 0, l = r ? v.plotY + l : u.chartY); n || h.formatter || (this.isDatetimeAxis && (p = "%b %d, %Y"), n = "{value" + (p ? ":" + p : "") + "}"); p = r ? v[this.isXAxis ? "x" : "y"] : this.toValue(k ? u.chartX : u.chartY); m.attr({ text: n ? w(n, { value: p }, e.time) : h.formatter.call(this, p), x: a, y: l, visibility: p < this.min || p > this.max ? "hidden" : "visible" }); h = m.getBBox(); if (k) { if (q && !c || !q && c) l = m.y - h.height } else l = m.y - h.height / 2; k ? (c = d - h.x, d = d + this.width -
                            h.x) : (c = "left" === this.labelAlign ? d : 0, d = "right" === this.labelAlign ? d + this.width : e.chartWidth); m.translateX < c && (t = c - m.translateX); m.translateX + h.width >= d && (t = -(m.translateX + h.width - d)); m.attr({ x: a + t, y: l, anchorX: k ? a : this.opposite ? 0 : e.chartWidth, anchorY: k ? this.opposite ? e.chartHeight : 0 : l + h.height / 2 })
                }
            }); G.init = function () { l.apply(this, arguments); this.setCompare(this.options.compare) }; G.setCompare = function (a) {
            this.modifyValue = "value" === a || "percent" === a ? function (b, c) {
                var d = this.compareValue; if (void 0 !== b &&
                    void 0 !== d) return b = "value" === a ? b - d : b / d * 100 - (100 === this.options.compareBase ? 0 : 100), c && (c.change = b), b
            } : null; this.userOptions.compare = a; this.chart.hasRendered && (this.isDirty = !0)
            }; G.processData = function () {
                var a, b = -1, d, e, f = !0 === this.options.compareStart ? 0 : 1, h, k; H.apply(this, arguments); if (this.xAxis && this.processedYData) for (d = this.processedXData, e = this.processedYData, h = e.length, this.pointArrayMap && (b = p("close", this.pointArrayMap), -1 === b && (b = p(this.pointValKey || "y", this.pointArrayMap))), a = 0; a < h - f; a++)if (k =
                    e[a] && -1 < b ? e[a][b] : e[a], c(k) && d[a + f] >= this.xAxis.min && 0 !== k) { this.compareValue = k; break }
            }; v(G, "getExtremes", function (a) { var b; a.apply(this, [].slice.call(arguments, 1)); this.modifyValue && (b = [this.modifyValue(this.dataMin), this.modifyValue(this.dataMax)], this.dataMin = E(b), this.dataMax = D(b)) }); q.prototype.setCompare = function (a, c) { this.isXAxis || (r(this.series, function (b) { b.setCompare(a) }), b(c, !0) && this.chart.redraw()) }; e.prototype.tooltipFormatter = function (c) {
                c = c.replace("{point.change}", (0 < this.change ?
                    "+" : "") + a.numberFormat(this.change, b(this.series.tooltipOptions.changeDecimals, 2))); return K.apply(this, [c])
            }; v(u.prototype, "render", function (a) {
                var b; this.chart.is3d && this.chart.is3d() || this.chart.polar || !this.xAxis || this.xAxis.isRadial || (b = this.yAxis.len - (this.xAxis.axisLine ? Math.floor(this.xAxis.axisLine.strokeWidth() / 2) : 0), !this.clipBox && this.animate ? (this.clipBox = k(this.chart.clipBox), this.clipBox.width = this.xAxis.len, this.clipBox.height = b) : this.chart[this.sharedClipKey] ? this.chart[this.sharedClipKey].attr({
                    width: this.xAxis.len,
                    height: b
                }) : this.clipBox && (this.clipBox.width = this.xAxis.len, this.clipBox.height = b)); a.call(this)
            }); v(n.prototype, "getSelectedPoints", function (a) { var b = a.call(this); r(this.series, function (a) { a.hasGroupedData && (b = b.concat(y(a.points || [], function (a) { return a.selected }))) }); return b }); C(n, "update", function (a) { a = a.options; "scrollbar" in a && this.navigator && (k(!0, this.options.scrollbar, a.scrollbar), this.navigator.update({}, !1), delete a.scrollbar) })
    })(L); return L
});