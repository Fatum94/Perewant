function request(url,renderToBlock,data){


    $.ajax({
        data: data,
        url: url,
        type: "GET",
        success: function (response) {
            var store = Ext.create('Ext.data.Store', {

                storeId: 'simpsonsStore',
                fields: ['Uname', 'LastActivity', 'ActivityType'],
                data: response.result,
                proxy: {
                    type: 'ajax',
                    reader: {
                        type: 'json'
                    }
                }
            });
            Ext.create('Ext.grid.Panel', {
                store: store,
                renderTo: Ext.getBody(),
                viewConfig: {
                    plugins: {
                        ddGroup: 'people-group',
                        ptype: 'gridviewdragdrop',
                        enableDrop: true
                    }
                },
                title: 'Your needed compressor',
                width: '100%',
                columns: [

                { header: 'USER NAME', dataIndex: 'Uname', flex: 1 },
                { header: 'Activity time', dataIndex: 'LastActivity', flex: 1 },
                { header: 'Type of activity', dataIndex: 'ActivityType', flex: 1 }
            ]

            });
        }
    });
}