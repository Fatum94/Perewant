function request(url,renderToBlock,data){


    $.ajax({
        data: data,
        url: url,
        type: "GET",
        success: function (response) {
            var store = Ext.create('Ext.data.Store', {

                storeId: 'simpsonsStore',
                fields: ['PressIn', 'PressOut', 'Performance', 'Drive', 'Power', 'DegreesOfPressure', 'NumberOfCylinders', 'Bore', 'LengthOfStroke', 'SpeedOfRotation'],
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

                { header: 'Press Out', dataIndex: 'PressOut', width: 100 },
                { header: 'Press In', dataIndex: 'PressIn', flex: 1 },
                { header: 'Performance', dataIndex: 'Performance', flex: 1 },
                { header: 'Drive', dataIndex: 'Drive', flex: 1 },
                { header: 'Power', dataIndex: 'Power', flex: 1 },
                { header: 'Degrees Of Pressure', dataIndex: 'DegreesOfPressure', flex: 1 },
                { header: 'Number Of Cylinders', dataIndex: 'NumberOfCylinders', flex: 1 },
                { header: 'Bore', dataIndex: 'Bore', flex: 1 },
                { header: 'Length Of Stroke', dataIndex: 'LengthOfStroke', flex: 1 },
                { header: 'Speed Of Rotation', dataIndex: 'SpeedOfRotation', flex: 1 }

            ]

            });
        }
    });
}