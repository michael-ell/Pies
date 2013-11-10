Ext.define('Pies.store.Ingredients', {
    extend: 'Ext.data.Store',
    requires: ['Pies.model.Ingredient'],
    config: {
        //data: [ {description: 'a', percent: 20}, {description: 'b', percent: 30}, {description: 'c', percent: 50}]
        storeId: 'ingredients',
        model: 'Pies.model.Ingredient'
    }
});