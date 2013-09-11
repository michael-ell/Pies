Ext.define('Pies.view.Main', {
    extend: 'Ext.tab.Panel',
    xtype: 'main',
    requires: [
        'Ext.TitleBar',
        'Chart.ux.Highcharts'
    ],
    config: {
        tabBarPosition: 'bottom',
        items: [
            {
                id: 'home',
                title: 'Home',
                iconCls: 'home',
                styleHtmlContent: true,
                scrollable: true,
                items:[
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'Home'
                    },
                    //{
                    //    xtype: 'highchart',
                    //    series: [{
                    //        dashStyle: 'DashDot',
                    //        dataIndex: 'value'
                    //    }],
                    //    xField: 'month',
                    //    store: store,
                    //    chartConfig: {
                    //        chart: {
                    //            type: 'spline'
                    //        },
                    //        title: {
                    //            text: 'A simple graph'
                    //        }
                    //    }
                    //}
                ],
                listeners: {
                    activate: function () {
                        this.fireEvent('onLoadPies', this);
                    }
                }
            },
            {
                title: 'Bake',
                iconCls: 'action',
                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'Bake'
                    }
                ]
            },
            {
                title: 'My Pies',
                iconCls: 'user',
                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'My Pies'
                    }
                ]
            }
        ]
    }
});
