Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.store.RecentPies'],
    config: {
        refs: { Home: '#home' },
        control: {
            Home: {
                onLoadPies: 'loadPies'
            }
        },
    },
    loadPies: function () {
        var store = Ext.getStore('recentPies');
        //var store = Ext.create('Pies.store.RecentPies', {
        //    listeners: {
        //        load: function (s, r) {
        //            console.log('** Loaded-' + r.length + '-pies');
        //            //var ingredients = Ext.getStore('ingredients'); // get or create store?
        //            //ingredients.data = r[0].data.allIngredients;
        //            //var data = Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: r[0].data.allIngredients });                  
        //    }}
        //});
        store.load();
    }
});