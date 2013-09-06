Ext.define('Pies.model.Pie', {
    extend: 'Ext.data.Model',
    config: {
        fields: ['id', 'caption'],
        //hasMany: { model: 'Pies.model.Ingredient', name: 'allIngredients' }
    }    
});