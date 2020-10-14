function initAdminlte() {
    console.log('initializing adminLTE...');
    $('[data-widget="pushmenu"]').PushMenu();
    $('[data-card-widget="collapse"]').CardWidget('toggle');
    console.log('Done.');
}