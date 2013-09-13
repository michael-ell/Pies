Ext.define('Pies.controller.Main', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.store.RecentPies', 'Pies.model.Ingredient'],
    config: {
        refs: { Home: '#home', Chart: '#chart' },
        control: {
            Home: {
                onLoadPies: 'loadPies'
            }
        },
    },
    loadPies: function () {
        var store = Ext.create('Pies.store.RecentPies', {
            listeners: {
                load: function (s, r) {
                    alert('pies loaded-' + r.length);
                    var data = Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: r[0].data.allIngredients });
                    //this.getChart().bindStore(Ext.create('Pies.store.RecentPies'));                    
            }}
        });
        //this.getChart().bindStore(Ext.create('Pies.store.RecentPies'));
    }
});