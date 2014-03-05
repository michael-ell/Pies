Ext.application({
    name: 'Pies',

    requires: ['Pies.view.Main', 'Ext.MessageBox', 'Ext.LoadMask'],

    views: ['Main'],
    
    controllers: ['Home', 'Bake'],

    icon: {},

    isIconPrecomposed: true,

    startupImage: {},

    launch: function() {
        Ext.fly('appLoadingIndicator').destroy();
        Ext.Viewport.add(Ext.create('Pies.view.Main'));
    },

    onUpdated: function() {
        Ext.Msg.confirm(
            "Application Update",
            "This application has just successfully been updated to the latest version. Reload now?",
            function(buttonId) {
                if (buttonId === 'yes') {
                    window.location.reload();
                }
            }
        );
    },
    
    listeners: {
        busy: function (msg) {
            setTimeout(function () {
                Ext.Viewport.setMasked({ xtype: 'loadmask', message: (typeof msg === "string") ? msg : '' });
            }, 0);
        },
        notBusy: function () {
            Ext.Viewport.setMasked(false);
        }
    }
});
