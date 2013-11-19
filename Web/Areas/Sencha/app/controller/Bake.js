Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    config: {
        views: ['Bake'],
        refs: { bake: '#bake', caption: '#caption' },
        control: {
            bake: {
                activate: 'createPie'
            },
            caption: {
                onCaptionChanged: 'updateCaption'
            }
        },
        pie: null
    },
    createPie: function () {
        window.console.log('creating pie...');
        Ext.Ajax.request({
            url: '/sencha/pie/create',
            success: function(resp) {
                console.log('created pie...');
            }
        });
    },
    updateCaption: function (caption) {
        console.log('updating caption to ' + caption + '...');
    },
    updateTags: function() {
        
    }
});