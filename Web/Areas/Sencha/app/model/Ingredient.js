Ext.define('Pies.model.Ingredient', {
    extend: 'Ext.data.Model',
    config: {
        fields: ['id', 'percent', 'description', 'color'],
        belongsTo: 'Pies.model.Pie'
    }
});