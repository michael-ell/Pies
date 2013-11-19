Ext.define('Pies.model.Pie', {
    extend: 'Ext.data.Model',
    requires: 'Pies.model.Ingredient',
    config: {
        fields: ['id', 'caption', { name: 'allIngredients', type: 'Pies.model.Ingredient' }],
    }
});