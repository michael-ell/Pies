Ext.define("Pies.controller.Main", {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.Main', 'Pies.view.Menu', 'Pies.view.Home', 'Pies.view.Bake', 'Pies.view.MyPies'],
    config: {
        refs: {
            main: {
                selector: '.pies-main',
                xtype: 'pies-main',
                autoCreate: true
            },
            menu: {
                selector: '.pies-menu',
                xtype: 'pies-menu',
                autoCreate: true
            },            
            home:{
                  selector: '.pies-home',
                  xtype: 'pies-home',
                  autoCreate: true
            },
            bake: {
                selector: '.pies-bake',
                xtype: 'pies-bake',
                autoCreate: true
            },
            mine: {
                selector: '.pies-mine',
                xtype: 'pies-mine',
                autoCreate: true
            }
        },
        control: {
            '.button[action=toggleMenu]': {
                tap: 'toggleMenu'
            },
            '.pies-menu #menu-items': {
                itemtap: 'showView'
            }
        }
    },
    menuSide: 'left',
    launch: function () {
        var me = this, main = me.getMain(), menu = me.getMenu(), views = me.getViews();
        Ext.Viewport.add(main);
        Ext.Viewport.setMenu(menu, { side: me.menuSide, cover: false });
        menu.setData(views);
        main.setViews(views.map(function (item) { return item.view; }));
        Pies.app.fireEvent('getPies');
    },
    getViews: function () {
        return [{ title: 'Home', view: this.getHome() }, { title: 'Bake', view: this.getBake() }, { title: 'My Pies', view: this.getMine() }];
    },
    toggleMenu: function() {
        Ext.Viewport.toggleMenu(this.menuSide);
    },
    hideMenu: function () {
        Ext.Viewport.hideMenu(this.menuSide);
    },
    showView: function (scope, index, target, record) {
        var main = this.getMain();
        this.hideMenu();
        setTimeout(function () {
            var view = record.data.view;
            main.setActiveItem(view);
            main.setTitle(record.data.title);
            //main.setTitlebarButton(view.getTitlebarButton ? view.getTitlebarButton() : null);
        }, 0);
    }
});