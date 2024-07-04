Ext.define('Ext.ux.RowBodyExtend',
{
    extend: 'Ext.grid.feature.RowBody',
    alias: 'feature.rowbodyextend',
    rowBodyHiddenCls: Ext.baseCSSPrefix + 'grid-row-body-hidden',
    rowBodyTrCls: Ext.baseCSSPrefix + 'grid-rowbody-tr rowbodyexpander-row-body',
    rowBodyTdCls: Ext.baseCSSPrefix + 'grid-cell-rowbody',
    rowBodyDivCls: Ext.baseCSSPrefix + 'grid-rowbody',

    eventPrefix: 'rowbody',
    eventSelector: '.' + Ext.baseCSSPrefix + 'grid-rowbody-tr',

    checkcolumn: false,

    readMoreText: 'Read more...',
    readLessText: 'Less...',

    getRowBodyCheck: function (values) {
        return [
            '<tr class="' + this.rowBodyTrCls + ' {rowBodyCls}">',
                '<td class="' + this.rowBodyTdCls + '" colspan="{rowBodyColspan}">',
                    '<div class="' + this.rowBodyDivCls + '"><div class="rowbodyexpander-row-container"><div class="rowbodyexpander-row-prebody"></div></div><div class="rowbodyexpander-row-body-text">{rowBody}</div><div class="rowbody-extend show">' + this.readMoreText + '</div><div class="rowbody-extend hide">' + this.readLessText + '</div></div>',
                '</td>',
            '</tr>'
        ].join('');
    },

    getRowBody: function (values) {
        return [
            '<tr class="' + this.rowBodyTrCls + ' {rowBodyCls}">',
                '<td class="' + this.rowBodyTdCls + '" colspan="{rowBodyColspan}">',
                    '<div class="' + this.rowBodyDivCls + '"><div class="rowbodyexpander-row-body-text">{rowBody}</div><div class="rowbody-extend show">' + this.readMoreText + '</div><div class="rowbody-extend hide">' + this.readLessText + '</div></div>',
                '</td>',
            '</tr>'
        ].join('');
    },


    reloadBody: function (cpt) {
        var elems = cpt.getEl().query('.rowbodyexpander-row-body-text');
        for (var i = 0, j = elems.length; i < j; i++) {
            var elem = Ext.get(elems[i]);
            var height = elem.getHeight();

            if (height > 20) {
                elem.addCls('collapsed');
            }
            else {
                elem.parent().addCls('noshow');
            }
        }
    },
    init: function (config) {
        var me = this;
        this.callParent(arguments);

        var store = me.grid.store,
            cpt = me.grid;

        store.on('beforeload', function () {
            cpt.un('afterlayout', me.afterShow);
        });

        store.on('load', function () {
            me.reloadBody(cpt);
        });

        cpt.on('rowbodyclick', function (view, nesto, e) {
            var el = e.getTarget(null, 10, true);
            if (el.hasCls('rowbody-extend')) {
                if (el.prev('.rowbodyexpander-row-body-text').hasCls('collapsed')) {
                    el.prev('.rowbodyexpander-row-body-text').removeCls('collapsed');
                    el.removeCls('show').addCls('hide');
                    el.next('.rowbody-extend').removeCls('hide').addCls('show');
                }
                else {
                    el.prev('.rowbodyexpander-row-body-text').addCls('collapsed');
                    el.removeCls('show').addCls('hide');
                    el.prev('.rowbody-extend').removeCls('hide').addCls('show');
                }
            }
        });

        cpt.on('afterlayout', me.afterShow, me);

        cpt.on('boxready', function (cpt) {
            cpt.getEl().addCls('rowbodyexpander-table');
        });
    },

    afterShow: function (cpt) {
        var me = this;
        cpt.getEl().addCls('rowbodyexpander-table');
        me.reloadBody(cpt);
    },

    // injects getRowBody into the metaRowTpl. 
    getMetaRowTplFragments: function () {
        return {
            getRowBody: (this.checkcolumn) ? this.getRowBodyCheck : this.getRowBody,
            rowBodyTrCls: this.rowBodyTrCls,
            rowBodyTdCls: this.rowBodyTdCls,
            rowBodyDivCls: this.rowBodyDivCls,
            readMoreText: this.readMoreText,
            readLessText: this.readLessText
        };
    },

    mutateMetaRowTpl: function (metaRowTpl) {
        metaRowTpl.push('{[this.getRowBody(values)]}');
    },

    /** 
     * Provides additional data to the prepareData call within the grid view. 
     * The rowbody feature adds 3 additional variables into the grid view's template. 
     * These are rowBodyCls, rowBodyColspan, and rowBody. 
     * @param {Object} data The data for this particular record. 
     * @param {Number} idx The row index for this record. 
     * @param {Ext.data.Model} record The record instance 
     * @param {Object} orig The original result from the prepareData call to massage. 
     */
    getAdditionalData: function (data, idx, record, orig) {
        var headerCt = this.view.headerCt,
            colspan = headerCt.getColumnCount();

        return {
            rowBody: "",
            rowBodyCls: this.rowBodyCls,
            rowBodyColspan: colspan
        };
    }
});

Ext.define('Ext.ux.CheckCombo',
{
    extend: 'Ext.form.field.ComboBox',
    alias: 'widget.checkcombo',
    multiSelect: true,
    allSelector: false,
    noData: false,
    noDataText: 'No combo data',
    addAllSelector: false,
    allSelectorHidden: false,
    enableKeyEvents: true,
    afterExpandCheck: false,
    allText: 'All',
    oldValue: '',
    listeners:
    {
        /* uncomment if you want to reload store on every combo expand
                beforequery: function(qe)
                {
                    this.store.removeAll();
                    delete qe.combo.lastQuery;
                },
        */
        focus: function (cpt) {
            cpt.oldValue = cpt.getValue();
        },
        keydown: function (cpt, e, eOpts) {
            var value = cpt.getRawValue(),
                oldValue = cpt.oldValue;

            if (value != oldValue) cpt.setValue('');
        }
    },
    createPicker: function () {
        var me = this,
            picker,
            menuCls = Ext.baseCSSPrefix + 'menu',
            opts = Ext.apply(
            {
                pickerField: me,
                selModel:
                {
                    mode: me.multiSelect ? 'SIMPLE' : 'SINGLE'
                },
                floating: true,
                hidden: true,
                ownerCt: me.ownerCt,
                cls: me.el.up('.' + menuCls) ? menuCls : '',
                store: me.store,
                displayField: me.displayField,
                focusOnToFront: false,
                pageSize: me.pageSize,
                tpl:
                [
                    '<ul><tpl for=".">',
                        '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item"><span class="x-combo-checker">&nbsp;</span> {' + me.displayField + '}</li>',
                    '</tpl></ul>'
                ]
            }, me.listConfig, me.defaultListConfig);


        picker = me.picker = Ext.create('Ext.view.BoundList', opts);
        if (me.pageSize) {
            picker.pagingToolbar.on('beforechange', me.onPageChange, me);
        }


        me.mon(picker,
        {
            itemclick: me.onItemClick,
            refresh: me.onListRefresh,
            scope: me
        });


        me.mon(picker.getSelectionModel(),
        {
            'beforeselect': me.onBeforeSelect,
            'beforedeselect': me.onBeforeDeselect,
            'selectionchange': me.onListSelectionChange,
            scope: me
        });


        me.store.on('load', function (store) {
            if (store.getTotalCount() == 0) {
                me.allSelectorHidden = true;
                if (me.allSelector != false) me.allSelector.setStyle('display', 'none');
                if (me.noData != false) me.noData.setStyle('display', 'block');
            }
            else {
                me.allSelectorHidden = false;
                if (me.allSelector != false) me.allSelector.setStyle('display', 'block');
                if (me.noData != false) me.noData.setStyle('display', 'none');
            }
        });


        return picker;
    },
    reset: function () {
        var me = this;


        me.setValue('');
    },
    setValue: function (value) {
        this.value = value;
        if (!value) {
            if (this.allSelector != false) this.allSelector.removeCls('x-boundlist-selected');
            return this.callParent(arguments);
        }


        if (typeof value == 'string') {
            var me = this,
                records = [],
                vals = value.split(',');


            if (value == '') {
                if (me.allSelector != false) me.allSelector.removeCls('x-boundlist-selected');
            }
            else {
                if (vals.length == me.store.getCount() && vals.length != 0) {
                    if (me.allSelector != false) me.allSelector.addCls('x-boundlist-selected');
                    else me.afterExpandCheck = true;
                }
            }


            Ext.each(vals, function (val) {
                var record = me.store.getById(parseInt(val));
                if (record) records.push(record);
            });


            return me.setValue(records);
        }
        else return this.callParent(arguments);
    },
    getValue: function () {
        if (typeof this.value == 'object') return this.value.join(',');
        else return this.value;
    },
    getSubmitValue: function () {
        return this.getValue();
    },
    expand: function () {
        var me = this,
            bodyEl, picker, collapseIf;


        if (me.rendered && !me.isExpanded && !me.isDestroyed) {
            bodyEl = me.bodyEl;
            picker = me.getPicker();
            collapseIf = me.collapseIf;


            // show the picker and set isExpanded flag
            picker.show();
            me.isExpanded = true;
            me.alignPicker();
            bodyEl.addCls(me.openCls);


            if (me.noData == false) me.noData = picker.getEl().down('.x-boundlist-list-ct').insertHtml('beforeBegin', '<div class="x-boundlist-item" role="option">' + me.noDataText + '</div>', true);


            if (me.addAllSelector == true && me.allSelector == false) {
                me.allSelector = picker.getEl().down('.x-boundlist-list-ct').insertHtml('beforeBegin', '<div class="x-boundlist-item" role="option"><span class="x-combo-checker">&nbsp;</span> ' + me.allText + '</div>', true);
                me.allSelector.on('click', function (e) {
                    if (me.allSelector.hasCls('x-boundlist-selected')) {
                        me.allSelector.removeCls('x-boundlist-selected');
                        me.setValue('');
                        me.fireEvent('select', me, []);
                    }
                    else {
                        var records = [];
                        me.store.each(function (record) {
                            records.push(record);
                        });
                        me.allSelector.addCls('x-boundlist-selected');
                        me.select(records);
                        me.fireEvent('select', me, records);
                    }
                });


                if (me.allSelectorHidden == true) me.allSelector.hide();
                else me.allSelector.show();

                if (me.afterExpandCheck == true) {
                    me.allSelector.addCls('x-boundlist-selected');
                    me.afterExpandCheck = false;
                }
            }


            // monitor clicking and mousewheel
            me.mon(Ext.getDoc(),
            {
                mousewheel: collapseIf,
                mousedown: collapseIf,
                scope: me
            });
            Ext.EventManager.onWindowResize(me.alignPicker, me);
            me.fireEvent('expand', me);
            me.onExpand();
        }
        else {
            me.fireEvent('expand', me);
            me.onExpand();
        }
    },
    alignPicker: function () {
        var me = this,
            picker = me.getPicker();


        me.callParent();

        if (me.addAllSelector == true) {
            var height = picker.getHeight();
            height = parseInt(height) + 20;
            picker.setHeight(height);
            picker.getEl().setStyle('height', height + 'px');
        }
    },
    onListSelectionChange: function (list, selectedRecords) {
        var me = this,
            isMulti = me.multiSelect,
            hasRecords = selectedRecords.length > 0;
        // Only react to selection if it is not called from setValue, and if our list is
        // expanded (ignores changes to the selection model triggered elsewhere)
        if (!me.ignoreSelection && me.isExpanded) {
            if (!isMulti) {
                Ext.defer(me.collapse, 1, me);
            }
            /*
            * Only set the value here if we're in multi selection mode or we have
            * a selection. Otherwise setValue will be called with an empty value
            * which will cause the change event to fire twice.
            */
            if (isMulti || hasRecords) {
                me.setValue(selectedRecords, false);
            }
            if (hasRecords) {
                me.fireEvent('select', me, selectedRecords);
            }
            me.inputEl.focus();


            if (me.addAllSelector == true && me.allSelector != false) {
                if (selectedRecords.length == me.store.getTotalCount()) me.allSelector.addCls('x-boundlist-selected');
                else me.allSelector.removeCls('x-boundlist-selected');
            }
        }
    }
});