Ext.define('Pies.view.Home', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar', 'Ext.dataview.DataView', 'Pies.store.RecentPies', 'Pies.view.PieItem'],
    xtype: 'homecard',
    config: {
        id: 'home',
        title: 'Home',
        iconCls: 'home',
        layout: 'fit',
        items: [
            {
                docked: 'top',
                xtype: 'titlebar',
                title: 'Home'
            },           
            {
                xtype: 'dataview',
                scrollable: 'vertical',
                //store: Ext.getStore('recentPies'), // get or create store?
                store: Ext.create('Pies.store.RecentPies'), // get or create store?
                useComponents: true,
                defaultType: 'pieItem'
            }
        ],
        listeners: {
            activate: function () {

                //this.fireEvent('onLoadPies', this);
            }
        }
    }
});
