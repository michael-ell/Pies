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
        main.setViews(views);
        this._show(this.getHomeView());
        Pies.app.on(Pies.hub.Messages.editPie, this.editPie, this);
        Pies.app.fireEvent(Pies.hub.Messages.getPies);
    },
    getViews: function () {
        return [this.getHomeView(), this.getBakeView(), {title: 'My Pies', view: this.getMine()}];
    },
    getHomeView: function () {
        return { title: 'Home', view: this.getHome() };
    },
    getBakeView: function() {
        return { title: 'Bake', view: this.getBake() };
    },
    toggleMenu: function () {
        Ext.Viewport.toggleMenu(this.menuSide);
    },
    showView: function (scope, index, target, record) {
        this.toggleMenu();
        this._show(record.data);
    },
    _show: function (data) {
        var me = this;
        var main = me.getMain();
        var view = data.view;
        if (me._current != view) {
            setTimeout(function () {
                main.setActiveItem(view);
                main.setTitle(data.title);
                me._tbBtn = view.getTitlebarButton ? view.getTitlebarButton() : null;
                main.setTitlebarButton(me._tbBtn);
            }, 0);
            me._current = view;
        }
    },
    menuShown: function () {
        this._current.addCls('blur');
        this._disableTbBtn(true);
    },
    menuHidden: function () {
        this._current.removeCls('blur');
        this._disableTbBtn(false);
    },
    _disableTbBtn: function(val) {
        if (this._tbBtn) {
            this._tbBtn.setDisabled(val);
        }
    },
    editPie: function (pie) {
        this._show(this.getBakeView());
    }
});