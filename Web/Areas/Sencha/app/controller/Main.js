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
            home: {
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
            },
            menu: {
                show: 'menuShown',
                hide: 'menuHidden'
            }
        }
    },
    menuSide: 'top',
    launch: function () {
        var me = this, main = me.getMain(), menu = me.getMenu(), views = me.getViews();
        Ext.Viewport.add(main);
        Ext.Viewport.setMenu(menu, { side: me.menuSide, cover: true });
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
        var me = this;
        var main = me.getMain();
        var view = record.data.view;       
        me.hideMenu();
        if (main.getActiveItem() != view) {
            setTimeout(function() {
                main.setActiveItem(view);
                main.setTitle(record.data.title);
                me._tbBtn = view.getTitlebarButton ? view.getTitlebarButton() : null;
                main.setTitlebarButton(me._tbBtn);
            }, 0);
        }
    },
    menuShown: function () {
        this._disableTbBtn(true);
    },
    menuHidden: function () {
        this._disableTbBtn(false);
    },
    _disableTbBtn: function(val) {
        if (this._tbBtn) {
            this._tbBtn.setDisabled(val);
        }
    }
});