Ext.define('Pies.view.Bake', {
    extend: 'Ext.Container',
    requires: ['Pies.view.BakeActions', 'Pies.view.EditPie', 'Pies.view.Pie', 'Ext.Panel'],
    xtype: 'pies-bake',
    config: {
    layout: 'card',
        items: [
            {
                xtype: 'pies-edit'
            },
            {
                xtype: 'pies-pie'
            }
        ],
        title: 'Bake'
    },
    getTitlebarButton: function () {
        if (!this._actions) {
            this._actions = Ext.create('Pies.view.BakeActions');
        }
        this._actions.hide();

        var btn = Ext.create('Ext.Button', {
            iconCls: 'actions',
            ui: 'plain',
            listeners: {
                tap: {
                    scope: this,
                    fn: function() {
                        this._actions.isHidden() ? this.showActions() : this.hideActions();
                    }
                }
            }
        });

        this._tbBtn = btn;
        return btn;
    },
    togglePreview: function() {
        this.animateActiveItem(this.items.indexOf(this.getActiveItem()) == 0 ? 1 : 0, { duration: 500, type: 'fade' });
    },
    showActions: function () {
        this._actions.showBy(this._tbBtn);
    },
    hideActions: function() {
        this._actions.hide();
    }
});
