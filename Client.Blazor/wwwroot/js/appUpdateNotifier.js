export function registerNewAppVersionCallback(dotnetHelper) {
    window['updateAvailable']
        .then(newVersionAvailable => {
            if (newVersionAvailable) {
                dotnetHelper.invokeMethodAsync('Client.Blazor', 'InvokeAction')
                    .then(r => console.log(r));
            }
        });
}