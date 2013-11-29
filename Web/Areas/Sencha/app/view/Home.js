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
            //{
            //    docked: 'top',
            //    xtype: 'titlebar',
            //    title: 'Home'
            //},           
            //{
            //    xtype: 'dataview',
            //    scrollable: 'vertical',
            //    store: Ext.create('Pies.store.RecentPies'),
            //    useComponents: true,
            //    defaultType: 'pieItem'
            //}
            {
                xtype: 'carousel',
                id: 'pies',
                store: Ext.create('Pies.store.RecentPies')
            }
        ]
        //listeners: {
        //    //Controller ref / control listener
        //    activate: function () {
        //        Ext.getStore('recentPies').load();
        //        //this.fireEvent('onLoadPies');
        //    }
        //}
    }
});
