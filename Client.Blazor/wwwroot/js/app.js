function initAdminlte() {
    console.log('initializing adminLTE...');
    $('[data-widget="pushmenu"]').PushMenu();
    $('[data-card-widget="collapse"]').CardWidget('toggle');
    $('.data-toggle').dropdown();
    $('[data-toggle="tooltip"]').tooltip();
    console.log('Done.');
}

window.blazorExtensions = {

    SetCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },

    RemoveCookie: function(name) {
        document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    },

    ReadCookie: function(cname) {
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
    },

    IsOnline: function() {
        console.log('Checking navigator.onLine property.');
        if (navigator.onLine) {
            return true;
        } else {
            return false;
        }
    }

}

function setItemUnmarshalled(key, json) {
    const keyStr = BINDING.conv_string(key);
    const jsonStr = BINDING.conv_string(json);
    localStorage.setItem(keyStr, jsonStr);
}

function getItemUnmarshalled(key) {
    const keyStr = BINDING.conv_string(key);
    var item = localStorage.getItem(keyStr);
    return BINDING.js_to_mono_obj(item);
}

function removeItemUnmarshalled(key) {
    const keyStr = BINDING.conv_string(key);
    localStorage.removeItem(keyStr);
}

// This does not work, not used by dotnet repository
function containsKeyUnmarshalled(key) {
    const keyStr = BINDING.conv_string(key);
    var containsKey = localStorage.hasOwnProperty(keyStr);
    return BINDING.js_to_mono_obj(containsKey);
}

//source: https://www.meziantou.net/generating-and-downloading-a-file-in-a-blazor-webassembly-application.htm
function BlazorDownloadFile(filename, contentType, content) {
    // Blazor marshall byte[] to a base64 string, so we first need to convert the string (content) to a Uint8Array to create the File
    const data = base64DecToArr(content);

    // Create the URL
    const file = new File([data], filename, { type: contentType });
    const exportUrl = URL.createObjectURL(file);

    // Create the <a> element and click on it
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();

    // We don't need to keep the url, let's release the memory
    URL.revokeObjectURL(exportUrl);
}

function BlazorDownloadFileFast(name, contentType, content) {
    // Convert the parameters to actual JS types
    const nameStr = BINDING.conv_string(name);
    const contentTypeStr = BINDING.conv_string(contentType);
    const contentArray = Blazor.platform.toUint8Array(content);

    // Create the URL
    const file = new File([contentArray], nameStr, { type: contentTypeStr });
    const exportUrl = URL.createObjectURL(file);

    // Create the <a> element and click on it
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = nameStr;
    a.target = "_self";
    a.click();

    // We don't need to keep the url, let's release the memory
    // On Safari it seems you need to comment this line... (please let me know if you know why)
    URL.revokeObjectURL(exportUrl);
}

function base64DecToArr(sBase64, nBlocksSize) {
    var
        sB64Enc = sBase64.replace(/[^A-Za-z0-9\+\/]/g, ""),
        nInLen = sB64Enc.length,
        nOutLen = nBlocksSize ? Math.ceil((nInLen * 3 + 1 >> 2) / nBlocksSize) * nBlocksSize : nInLen * 3 + 1 >> 2,
        taBytes = new Uint8Array(nOutLen);

    for (var nMod3, nMod4, nUint24 = 0, nOutIdx = 0, nInIdx = 0; nInIdx < nInLen; nInIdx++) {
        nMod4 = nInIdx & 3;
        nUint24 |= b64ToUint6(sB64Enc.charCodeAt(nInIdx)) << 18 - 6 * nMod4;
        if (nMod4 === 3 || nInLen - nInIdx === 1) {
            for (nMod3 = 0; nMod3 < 3 && nOutIdx < nOutLen; nMod3++, nOutIdx++) {
                taBytes[nOutIdx] = nUint24 >>> (16 >>> nMod3 & 24) & 255;
            }
            nUint24 = 0;
        }
    }
    return taBytes;
}

// Convert a base64 string to a Uint8Array. This is needed to create a blob object from the base64 string.
// The code comes from: https://developer.mozilla.org/fr/docs/Web/API/WindowBase64/D%C3%A9coder_encoder_en_base64
function b64ToUint6(nChr) {
    return nChr > 64 && nChr < 91 ? nChr - 65 : nChr > 96 && nChr < 123 ? nChr - 71 : nChr > 47 && nChr < 58 ? nChr + 4 : nChr === 43 ? 62 : nChr === 47 ? 63 : 0;
}

// Fix select2, this is needed to let the user choose the order of the tags in the multiple select
$("select").select2();
$("select").on("select2:select", function (evt) {
    var element = evt.params.data.element;
    var $element = $(element);

    $element.detach();
    $(this).append($element);
    $(this).trigger("change");
});

// Saves last scrolling position
window.setMandatesScrollPosition = () => {
    localStorage.setItem("mandatesScrollPosition", $(window).scrollTop());
};
window.getMandatesScrollPosition = () => {
    $(window).scrollTop(localStorage.getItem("mandatesScrollPosition"));
};

// Store ordering of mandates list
window.getSortListItems = () => {
    return localStorage.getItem("sortListItems");
};
window.setSortListItems = (sortListItems) => {
    localStorage.setItem("sortListItems", sortListItems);
};

// Store filter of mandates list
window.getFilterMandates = () => {
    return localStorage.getItem("filterMandates");
};
window.setFilterMandates = (filterMandates) => {
    localStorage.setItem("filterMandates", filterMandates);
};