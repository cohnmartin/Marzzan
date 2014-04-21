var slideShow = function() {
    var bxs, bxe, fxs, fxe, ys, ye, ta, ia, ie, st, ss, ft, fs, xp, yp, ci, t, tar, tarl;
    ta = document.getElementById(thumbid);
    ia = document.getElementById(imgid);
    t = ta.getElementsByTagName('li');
    ie = document.all ? true : false;
    st = 3; ss = 2; ft = 10; fs = 30; xp, yp = 0;
    stop = false;
    direccion = 1;
    return {
        init: function() {
            //document.onmousemove = this.pos;
            window.onresize = function() { setTimeout("slideShow.lim()", 500) };

            ys = this.toppos(ta); ye = ys + ta.offsetHeight;

            len = t.length; tar = [];
            for (i = 0; i < len; i++) {
                var id = t[i].value;
                tar[i] = id;


                t[i].onclick = new Function("slideShow.getimg('" + id + "')");
                //t[i].onmousemove = slideShow.stopnav;
                //t[i].onmouseout = slideShow.starnav;

            }

            ta.carusel = setInterval(function() { slideShow.scrl(direccion); }, 100);
            tarl = tar.length;
        },
        scrl: function(d) {
            clearInterval(ta.timer);
            var l;
            if (d == -1) {
                l = 0;

            }
            else {
                l = (t[tarl - 1].offsetLeft - (ta.parentNode.offsetWidth - t[tarl - 1].offsetWidth) + 10)
            }

            ta.timer = setInterval(function() { slideShow.mv(d, l) }, st);
        },

        mvefecto: function(fin, desplazamiento) {

            var left = ta.style.left.replace('px', '');
            if (ta.countEfecto < 4) {
                /// Hago un desplazamimiento en sentido contrario
                ta.style.left = (parseInt(left) + parseInt((desplazamiento / 4) * -1)) + 'px';
                ta.countEfecto += 1;
            }
            else {
                /// Paro el Efecto de desplazamiento
                clearInterval(ta.timerefecto);
                // Inicio el movimiento completo
                ta.timerBlock = setInterval(function() { slideShow.mvblockcontinue(fin, desplazamiento) }, 50);
            }

        },

        mvblockcontinue: function(fin, l, btn) {
            var left = ta.style.left.replace('px', '');

            //// Disparo el efecto de desplazamiento
            //if (ta.countEfecto == 0) {
            //  clearInterval(ta.timerBlock);
            //  ta.timerefecto = setInterval(function() { slideShow.mvefecto(fin, l) }, 70);
            //  return;
            //}


            if (l > 0) {

                if ((parseInt(left) + parseInt(l)) < fin)
                    ta.style.left = (parseInt(left) + parseInt(l)) + 'px';
                else {
                    clearInterval(ta.timerBlock);
                    if (parseInt(left) == 0 && parseInt(l) > 0) {
                        $("#previmg").css("display", "none");
                    }
                    $("#nextimg").css("display", "inline");
                }
            }
            else {
                if ((parseInt(left) + parseInt(l)) > fin) {
                    ta.style.left = (parseInt(left) + parseInt(l)) + 'px';
                }
                else {
                    clearInterval(ta.timerBlock);
                    // -4: por las 4 imagenes que se muestran inicialmente
                    if (Math.abs(parseInt(left) + parseInt(l)) > Math.abs((t.length) * 179)) {
                        $("#nextimg").css("display", "none");
                    }
                    $("#previmg").css("display", "inline");
                }
            }
        },

        mvblock: function(l, btn) {
            ta.style.left = ta.style.left || '0px';
            var left = ta.style.left.replace('px', '');
            if (parseInt(left) == 0 && parseInt(l) > 0)
                return
            else if (Math.abs(parseInt(left) + parseInt(l)) > Math.abs(t.length * 179))
                return
            else {
                var fin = parseInt(left) + parseInt(l);
                var desplazamiento = 92;
                ta.countEfecto = 0;
                if (l > 0)
                    ta.timerBlock = setInterval(function() { slideShow.mvblockcontinue(fin, desplazamiento, btn) }, 50);
                else
                    ta.timerBlock = setInterval(function() { slideShow.mvblockcontinue(fin, desplazamiento * -1, btn) }, 50);




            }
        },

        mv: function(d, l) {

            ta.style.left = ta.style.left || '0px';
            var left = ta.style.left.replace('px', '');
            if (d == 1) {



                if (l - Math.abs(left) <= ss) {

                    this.cncl(ta.id);
                    if (l > 0) {

                        ta.style.left = '-' + l + 'px';

                    }


                    /// Si llego al final voy para atras
                    if (l == Math.abs(left)) {
                        clearTimeout(ta.carusel);
                        clearTimeout(ta.timer);
                        direccion = -1;
                        ta.carusel = setInterval(function() { slideShow.scrl(direccion); }, 100);
                    }

                }

                else {

                    ta.style.left = left - ss + 'px'

                }


            } else {
                if (Math.abs(left) - l <= ss) {
                    this.cncl(ta.id);
                    ta.style.left = l + 'px';

                    /// Si llego al final voy para el otro lado
                    if (0 == Math.abs(left)) {
                        clearTimeout(ta.carusel);
                        clearTimeout(ta.timer);
                        direccion = 1;
                        ta.carusel = setInterval(function() { slideShow.scrl(direccion); }, 100);
                    }


                }
                else {
                    ta.style.left = parseInt(left) + ss + 'px'
                }
            }
        },
        cncl: function() {

            clearTimeout(ta.timer)
        },
        getimg: function(id) {
            if (auto) { clearTimeout(ia.timer) }
            if (ci != null) {
                var ts, tsl, x;
                ts = ia.getElementsByTagName('img'); tsl = ts.length; x = 0;
                for (x; x < tsl; x++) {
                    if (ci.id != id) {
                        var o = ts[x]; clearInterval(o.timer);
                        o.timer = setInterval(function() { slideShow.fdout(o) }, fs)
                    }
                }
            }


            if (!document.getElementById(id)) {
                var i = document.createElement('img');
                ia.appendChild(i);
                i.id = id; i.av = 0; i.style.opacity = 0;
                i.style.filter = 'alpha(opacity=0)';
                i.style.width = "675px";
                i.style.height = "477px";
                i.src = imgdir + '/' + id + imgext;
                //i.onmousemove = slideShow.stopnav;
                //i.onmouseout = slideShow.starnav;
            }
            else {
                i = document.getElementById(id);
                clearInterval(i.timer);
            }

            i.timer = setInterval(function() { slideShow.fdin(i) }, fs);
        },
        stopnav: function() {
            clearTimeout(ta.carusel);
            clearTimeout(ta.timer);
        },
        starnav: function() {
            ta.carusel = setInterval(function() { slideShow.scrl(direccion); }, 100);
        },
        nav: function(d) {
            var c = 0;
            for (key in tar) { if (tar[key] == ci.id) { c = key } }
            if (tar[parseInt(c) + d]) {
                this.getimg(tar[parseInt(c) + d]);
            } else {
                if (d == 1) {
                    this.getimg(tar[0]);
                } else { this.getimg(tar[tarl - 1]) }
            }
        },
        auto: function() {
            if (!stop)
                ia.timer = setInterval(function() { slideShow.nav(1) }, autodelay * 1000)
        },

        fdin: function(i) {
            if (i.complete) { i.av = i.av + fs; i.style.opacity = i.av / 100; i.style.filter = 'alpha(opacity=' + i.av + ')' }
            if (i.av >= 100) {
                //if (auto) { this.auto() };
                clearInterval(i.timer);
                ci = i
            }
        },
        fdout: function(i) {
            i.av = i.av - fs; i.style.opacity = i.av / 100;
            i.style.filter = 'alpha(opacity=' + i.av + ')';
            if (i.av <= 0) { clearInterval(i.timer); if (i.parentNode) { i.parentNode.removeChild(i) } }
        },
        lim: function() {
            var taw, taa, len; taw = ta.parentNode.offsetWidth; taa = taw / 4;
            bxs = slideShow.leftpos(ta); bxe = bxs + taa; fxe = bxs + taw; fxs = fxe - taa;
        },
        pos: function(e) {
            xp = ie ? event.clientX + document.documentElement.scrollLeft : e.pageX;
            yp = ie ? event.clientY + document.documentElement.scrollTop : e.pageY;

            ys = 581;
            ye = 681;
            if (xp > bxs && xp < bxe && yp > ys && yp < ye) {
                slideShow.scrl(-1);
            } else if (xp > fxs && xp < fxe && yp > ys && yp < ye) {
                slideShow.scrl(1);

            } else {
                slideShow.cncl()
            }
        },
        leftpos: function(t) {
            var l = 0;
            if (t.offsetParent) {
                while (1) { l += t.offsetLeft; if (!t.offsetParent) { break }; t = t.offsetParent }
            } else if (t.x) { l += t.x }
            return l;
        },
        toppos: function(t) {
            var p = 0;
            if (t.offsetParent) {
                while (1) { p += t.offsetTop; if (!t.offsetParent) { break }; t = t.offsetParent }
            } else if (t.y) { p += t.y }
            return p;
        }
    };
} ();

window.onload = function() { slideShow.getimg(1); };