Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.model.Pie', 'Ext.data.reader.Json'],
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
            scope: this,            
            success: function (resp) {              
                var created = Ext.JSON.decode(resp.responseText);
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
        //debugger;
        console.log('updating caption to ' + caption + '...');
        Ext.Ajax.request({
            url: '/sencha/pie/updateCaption',
            jsonData: { id: this.getPie().id, caption: caption }
        });
    },
    updateTags: function() {
        
    }
});