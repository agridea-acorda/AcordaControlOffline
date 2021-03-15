//navigator.serviceWorker.register('service-worker.js');

window.updateAvailable = new Promise(function (resolve, reject) {
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('service-worker.js')
            .then(registration => {
                console.log('Registration successful, scope is:', registration.scope);
                registration.onupdatefound = () => {
                    const installingWorker = registration.installing;
                    installingWorker.onstatechange = () => {
                        switch (installingWorker.state) {
                        case 'installed': // latest version of service-worker is installed and waiting to activate (a.k.a. 'new version of the app available')
                            if (navigator.serviceWorker.controller) {
                                resolve(true);
                            } else {
                                resolve(false);
                            }
                            break;
                        default:
                        }
                    };
                };
            })
            .catch(error =>
                console.log('Service worker registration failed, error:', error));
    }
});