function runApp() {
    Ext.application({
        name: 'MyApp',
        buildButtons: function() {
            if (getRole.userRole == 'a') {
                return [
                    {
                        xtype: 'button',
                        margin: '20',
                        text: 'Переглянути історію',
                        handler: function() {
                            location.href = "/Home/History"
                        }
                    }, {
                        xtype: 'button',
                        margin: '20',
                        text: 'Завантажити дані з файлу',
                        handler: function() {
                            uploadWindow.show();
                        }
                    }
                ]
            } else return [{}, {}];
        },
        launch: function() {
            Ext.create('Ext.tab.Panel', {
                id: 'tabpanel',
                style: 'margin:100px auto; ',
                width: 600,
                tabBarPosition: 'bottom',
                renderTo: Ext.getBody(),
                items: [
                    {
                        title: 'Введення даних',
                        bodyStyle: 'background: rgb(70, 64, 83)',
                        items: [
                            this.buildButtons()[0],
                            this.buildButtons()[1],
                            {
                                xtype: 'button',
                                margin: '20',
                                text: 'Logout',
                                handler: function() {
                                    location.href = "/Home/LogOut";
                                }
                            },
                            {
                                xtype: 'form',
                                bodyStyle: 'background: rgb(123, 105, 134)',
                                padding: '10',
                                layout: 'hbox',
                                items: [
                                    {
                                        xtype: 'container',
                                        margin: '20',
                                        defaults: {
                                            allowBlank: false,
                                            xtype: 'textfield'
                                        },
                                        items: [
                                            {
                                                fieldLabel: 'Тиск на вході',
                                                name: 'first.pressIn'
                                            }, {
                                                fieldLabel: 'Тиск на виході',
                                                name: 'first.pressOut'
                                            }, {
                                                fieldLabel: 'Enter smth',
                                                name: 'first.power'
                                            }, {
                                                fieldLabel: 'Enter smth',
                                                name: 'first.performance'
                                            }
                                        ]
                                    }, {
                                        xtype: 'container',
                                        margin: '20',
                                        defaults: {
                                            allowBlank: false
                                        },
                                        items: [
                                            {
                                                xtype: 'textfield',
                                                fieldLabel: 'Тиск на вході',
                                                name: 'first.pressIn'
                                            }, {
                                                xtype: 'textfield',
                                                fieldLabel: 'Тиск на виході',
                                                name: 'first.pressOut'
                                            }, {
                                                xtype: 'combo',
                                                displayField: 'name',
                                                name: 'second.chooseRodo',
                                                fieldLabel: 'Виберіть родовище',
                                                store: Ext.create('Ext.data.Store', {
                                                    fields: ['value', 'name'],
                                                    data: [{ value: 1, name: 'First' }, { value: 1, name: 'First' }]
                                                })
                                            }, {
                                                xtype: 'container',
                                                width: 200,
                                                fieldLabel: 'Size',
                                                defaultType: 'radiofield',
                                                defaults: {
                                                    flex: 1
                                                },
                                                layout: 'hbox',
                                                items: [
                                                    {
                                                        boxLabel: 'Кущ',
                                                        name: 'second.bushSverd',
                                                        inputValue: 'kusch',
                                                        id: 'radio1'
                                                    }, {
                                                        boxLabel: 'Свердловина',
                                                        name: 'second.bushSverd',
                                                        inputValue: 'sver',
                                                        id: 'radio2'
                                                    }
                                                ]

                                            }
                                        ]
                                    }
                                ],
                                buttons: [
                                    {
                                        text: 'Submit',
                                        handler: function(btn) {
                                            var form = this.up('form');
                                            if (form.isValid()) {
                                                Ext.getBody().mask('Please wait...');
                                                form.submit({
                                                    method: 'POST',
                                                    url: '/Home/SelectCompressor',
                                                    success: function(response, res, btn) {
                                                        Ext.getCmp('tabpanel').setActiveTab(1);
                                                        Ext.getCmp("resultPanel").removeAll(true);
                                                        var dataItems = new Array();
                                                        for (var i = 0; i < res.result.resp.length; i++) {
                                                            dataItems[i] = {
                                                                html: res.result.resp[i].PressOut,
                                                                width: 580,
                                                                height: 200,
                                                                layout: {
                                                                    type: 'hbox'
                                                                },
                                                                bodyStyle: { lineHeight: '160px' },
                                                                items: [{ xtype: 'panel', html: 'Type of the compressor', height: 180, width: 180, style: { color: '#000', backgroundColor: '#BBB', borderRadius: 20, textAlign: 'center', border: '15px solid #6699CC', margin: 5 }, bodyStyle: { background: '#BBB', lineHeight: '120px' } }],
                                                                defaultMargins: 10,
                                                                flex: 1,
                                                                split: true,
                                                                style: { color: '#000', backgroundColor: '#FFF', borderRadius: 20, textAlign: 'center', border: '5px solid', margin: 5 },


                                                            };
                                                        }
                                                        //                                           
                                                        Ext.getCmp('resultPanel').add(dataItems);
                                                        Ext.getBody().unmask();
                                                    }

                                                });
                                            }
                                        }
                                    },
                                    { text: 'Cancel', handler: function() { this.up('form').getForm().reset(); } },
                                ]

                            }
                        ]
                    }, {
                        tabIndex: 2,
                        title: 'Результат',
                        items: [{ xtype: 'label', text: 'Спочатку заповніть форму..' }],
                        id: 'resultPanel',
                        iconCls: 'user'
                    }
                ]
            });
        } // launch
    })
}