export function registerNewAppVersionCallback(onInstalledAndWaiting, onActivated) {
    window['swUpdateStatus']
        .then(status => {
            switch (status) {
                case 'installedAndWaiting':
                    onInstalledAndWaiting.invokeMethodAsync('Client.Blazor', 'InvokeAction')
                        .then(r => console.log(r));
                    break;
                case 'activated':
                    onActivated.invokeMethodAsync('Client.Blazor', 'InvokeAction')
                        .then(r => console.log(r));
                    break;
                default:
            }
        });
}