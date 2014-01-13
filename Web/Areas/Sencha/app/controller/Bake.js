Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.model.Pie', 'Pies.view.Pie', 'Ext.data.reader.Json'],
    config: {
        views: ['Bake'],
        refs: { bake: '#bake', caption: '#caption', tags: '#tags' },
        control: {
            bake: {
                activate: 'createPie'
            },
            caption: {
                onCaptionChanged: 'updateCaption'
            },
            tags: {
                onTagsChanged: 'updateTags'
            }
        },
        pie: null
    },   
    createPie: function () {
        Ext.Ajax.request({         
            url: '/sencha/pie/create',
            scope: this,            
            success: function (resp) {              
                var created = Ext.JSON.decode(resp.responseText);
                this.getBake().add({ xtype: 'pie', pie: {data: created} });

                var hub = $.connection.pie;
                hub.client.captionUpdated = function (data) {
                    Pies.app.getController('Bake').getCaption().setValue(data);
                };
                $.connection.hub.start().done(function() {
                    hub.server.join(created.id);
                    console.log('joined hub: ' + created.id);
                });
                console.log('created pie ' + created.id + '...');
                this.setPie(created);
            }
        });
    },
    updateCaption: function (caption) {
        console.log('updating caption to ' + caption + '...');
        Ext.Ajax.request({
            url: '/sencha/pie/updateCaption',
            jsonData: { id: this.getPie().id, caption: caption }
        });
    },
    updateTags: function (tags) {
        console.log('updating tags to ' + tags + '...');
        Ext.Ajax.request({
            url: '/sencha/pie/updateTags',
            jsonData: { id: this.getPie().id, tags: tags }
        });
    }
});