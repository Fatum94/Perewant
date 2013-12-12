function request(url,renderToBlock,data){


$.ajax({
    data: data,
    url: url,
    type: "GET",
    success: function (result) {
          var store = Ext.create('Ext.data.Store', {
            
                    storeId:'simpsonsStore',
                    fields:['PressIn', 'PressOut', 'Performance','Drive','Power','DegreesOfPressure','NumberOfCylinders','Bore','LengthOfStroke','SpeedOfRotation'],
                    data:{'items':result},
                    proxy: {
                        type: 'memory',
                        reader: {
                            type: 'json',
                            root: 'items'
                        }
                    }
                });
            Ext.create('Ext.grid.Panel', {
            store: Ext.data.StoreManager.lookup('simpsonsStore'),
            renderTo:renderToBlock,
            viewConfig: {
                plugins: {
                    ddGroup: 'people-group',
                    ptype: 'gridviewdragdrop',
                    enableDrop: true
                }
            },
            title: 'Your needed compressor',
            columns: [
       
                { header: 'Press Out',  dataIndex: 'PressOut' ,flex:1},
                { header: 'Press In', dataIndex: 'PressIn',flex:1},
                { header: 'Performance', dataIndex: 'Performance',flex:1 },
                { header: 'Drive', dataIndex: 'Drive',flex:1 },
                { header: 'Power', dataIndex: 'Power',flex:1 },
                { header: 'Degrees Of Pressure', dataIndex: 'DegreesOfPressure',flex:1 },
                { header: 'Number Of Cylinders', dataIndex: 'NumberOfCylinders',flex:1 },
                { header: 'Bore', dataIndex: 'Bore',flex:1 },
                { header: 'Length Of Stroke', dataIndex: 'LengthOfStroke',flex:1 },
                { header: 'Speed Of Rotation', dataIndex: 'SpeedOfRotation',flex:1 }

            ]

          });
         }  
        });
}