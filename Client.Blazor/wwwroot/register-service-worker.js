window.swUpdateStatus = new Promise(function (resolve, reject) {
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('service-worker.js')
            .then(registration => {
                console.log('Registration successful, scope is:', registration.scope);
                registration.onupdatefound = () => {
                    const newServiceWorker = registration.installing;
                    newServiceWorker.onstatechange = () => {
                        switch (newServiceWorker.state) {
                        case 'installed': // latest version of service-worker is installed and waiting to activate (a.k.a. 'new version of the app available')
                            console.log('installing sw is in the "installed" state');
                            if (navigator.serviceWorker.controller) {
                                resolve('installedAndWaiting');
                            } else {
                                resolve('noControllingServiceWorker');
                            }
                            break;
                        case 'activated': // new service-worker is has activated
                            console.log('installing sw is in the "activated" state');
                            resolve('activated');
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