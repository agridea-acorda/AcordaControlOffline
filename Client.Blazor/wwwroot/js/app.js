function initAdminlte() {
    console.log('initializing adminLTE...');
    $('[data-widget="pushmenu"]').PushMenu();
    $('[data-card-widget="collapse"]').CardWidget('toggle');
    $('.data-toggle').dropdown();
    $('[data-toggle="tooltip"]').tooltip();
    console.log('Done.');
}