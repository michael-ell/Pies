Ext.define('Pies.store.Ingredients', {
    extend: 'Ext.data.Store',
    requires: ['Pies.model.Ingredient'],
    config: {
        storeId: 'ingredients',
        model: 'Pies.model.Ingredient'
    }
});