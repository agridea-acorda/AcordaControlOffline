function initAdminlte() {
    console.log('initializing adminLTE...');
    $('[data-widget="pushmenu"]').PushMenu();
    $('[data-card-widget="collapse"]').CardWidget('toggle');
    $('.data-toggle').dropdown();
    $('[data-toggle="tooltip"]').tooltip();
    //$('[data-toggle="collapse"]').collapse();
    console.log('Done.');
}

window.blazorExtensions = {

    SetCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },

    RemoveCookie: function(name) {
        document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    },

    ReadCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
}